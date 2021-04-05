using AutoMapper;
using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Abstractions;
using PokerApi.Repositories.Interfaces;
using PokerApi.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Services
{
    public class RankAssignmentService : IRankAssignmentService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICardConverter _cardConverter;
        private readonly IRankAssigner _assigner;
        private readonly ICardSorter _cardSorter;
        private readonly IPokerHandRecognizer _pokerHandRecognizer;

        public RankAssignmentService(
            ILogger logger,
            IUnitOfWork uow,
            IMapper mapper,
            ICardConverter cardConverter,
            IRankAssigner assigner,
            ICardSorter cardSorter,
            IPokerHandRecognizer pokerHandRecognizer
            )
        {
            _logger = logger.ForContext("SourceContext", nameof(RankAssignmentService));
            _uow = uow;
            _mapper = mapper;
            _cardConverter = cardConverter;
            _assigner = assigner;
            _cardSorter = cardSorter;
            _pokerHandRecognizer = pokerHandRecognizer;
        }

        public async Task<IEnumerable<Ranking>> AssignRanks(IEnumerable<PokerHandDto> pokerHandDtos)
        {
            var projected = pokerHandDtos.Select(i => new PokerHandDomainModel
            {
                PokerHandId = i.PokerHandId,
                Player = i.Player,
                Card1 = _cardConverter.ConvertNumberAndSuitToEnum(i.Card1),
                Card2 = _cardConverter.ConvertNumberAndSuitToEnum(i.Card2),
                Card3 = _cardConverter.ConvertNumberAndSuitToEnum(i.Card3),
                Card4 = _cardConverter.ConvertNumberAndSuitToEnum(i.Card4),
                Card5 = _cardConverter.ConvertNumberAndSuitToEnum(i.Card5)
            });

            return await PrivateAssignRanks(projected);
        }

        private async Task<IEnumerable<Ranking>> PrivateAssignRanks(IEnumerable<PokerHandDomainModel> pokerHandDomainModels)
        {
            _logger.Debug("Started Assigning Ranks:{Method} Params:{@Params}", nameof(AssignRanks), pokerHandDomainModels);
            var rankings = await UpsertEntitiesAndComputeRankings(pokerHandDomainModels);
            _logger.Debug("Finished Assigning Ranks:{Method} RankingsResult:{@RankingsResult}", nameof(AssignRanks), rankings);
            return rankings;
        }

        private async Task<List<Ranking>> UpsertEntitiesAndComputeRankings(
            IEnumerable<PokerHandDomainModel> pokerHands)
        {
            var updatedPokerHandDomainModels = new List<PokerHandDomainModel>();
            var count = _uow.GetRepository<Repositories.Entities.Hand>().GetAll().Count();
            foreach (var hand in pokerHands)
            {
                var addPlayer = false;
                if (string.IsNullOrEmpty(hand.Player.UserId))
                {
                    hand.Player.UserId = Guid.NewGuid().ToString();
                    addPlayer = true;
                } else
                {
                    var player = await _uow.GetRepository<Repositories.Entities.Player>().Single(c => c.UserId == hand.Player.UserId);
                    if (player == null)
                    {
                        throw new Exception($"Player User ID: '{hand.Player.UserId}' is not found in database"); 
                    } else
                    {
                        if (hand.Player.UserName != player.UserName)
                        {
                            throw new Exception($"For Player User ID: '{hand.Player.UserId}', the User Name: '{hand.Player.UserName}' does not match that stored in database: '{player.UserName}'");
                        }
                    }
                }
                if (addPlayer)
                {
                    var entity = new Repositories.Entities.Player(hand.Player.UserName, hand.Player.UserId);
                    await _uow.GetRepository<Repositories.Entities.Player>().Add(entity);
                }

                if (hand.PokerHandId == 0)
                {
                    count++;
                    hand.PokerHandId = count;
                } else
                {
                    if (_uow.GetRepository<Repositories.Entities.Hand>().Get(i => i.HandId == hand.PokerHandId).Any())
                    {
                        throw new Exception($"Poker Hand ID: {hand.PokerHandId} is already used.");
                    }
                }
                var mapped = _mapper.Map<Repositories.Entities.Hand>(hand);
                await _uow.GetRepository<Repositories.Entities.Hand>().Add(mapped);
                updatedPokerHandDomainModels.Add(hand);
            }
            await _uow.Commit();
            return _assigner.AssignRanks(
                updatedPokerHandDomainModels,
                _logger,
                _cardConverter, 
                _cardSorter,
                _pokerHandRecognizer).ToList();
        }

        private async Task<List<Ranking>> GetDummyAssignedRanks()
        {
            await Task.Delay(10);
            return new List<Ranking>
            {
                new Ranking { Player = new Player { UserName = "Sam", UserId = "8383" }, Rank = 1 }
            };
        }
    }
}

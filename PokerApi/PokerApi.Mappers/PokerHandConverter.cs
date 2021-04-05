using AutoMapper;
using PokerApi.Common.Models;
using PokerApi.Models;
using PokerApi.Repositories.Entities;
using System;

namespace PokerApi.Mappers
{
    public class PokerHandConverter : ITypeConverter<Hand, PokerHandDomainModel>
    {
        public PokerHandDomainModel Convert(Hand source, PokerHandDomainModel destination, ResolutionContext context)
        {
            destination = new PokerHandDomainModel
            {
                PokerHandId = source.HandId,
                Player = GetPlayer(source),
                Card1 = Convert(source.CardId1),
                Card2 = Convert(source.CardId2),
                Card3 = Convert(source.CardId3),
                Card4 = Convert(source.CardId4),
                Card5 = Convert(source.CardId5)
            };
            return destination;
        }

        private CardEnum Convert(int cardId)
        {
            if (Enum.IsDefined(typeof(CardEnum), cardId))
            {
                var enumValue = (CardEnum)cardId;
                return enumValue;
            }
            else
            {
                throw new Exception($"CardEnum not found for CardId={cardId}");
            }
        }

        private Models.Player GetPlayer(Hand source)
        {
            var player = new Models.Player
            {
                UserId = source.PlayerUserId
            };
            return player;
        }
    }
}

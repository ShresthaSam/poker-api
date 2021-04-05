using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Interfaces;
using PokerApi.RankAssignment.Simple.Models;
using Serilog;
using System.Collections.Generic;

namespace PokerApi.RankAssignment.Simple.ScoreKeepers
{
    public class HighCardScoreKeeper : IScoreKeeper
    {
        public int Order => 9;

        public IEnumerable<ScoreRecord> GenerateScores(
            IEnumerable<PokerHandDomainModel> pokerHands, 
            ICardConverter cardConverter, 
            ICardSorter cardSorter, 
            IPokerHandRecognizer pokerHandRecognizer, 
            ILogger logger)
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            foreach (var hand in pokerHands)
            {
                var cards = Helper.GetCardItems(hand, cardConverter);
                var evaluation = pokerHandRecognizer.IsHighCard(cards);
                playerEvaluation.Add(hand.Player, evaluation);
            }
            logger.Debug("ScoreKeeper:{ScoreKeeper} PlayerEvaluation:{@PlayerEvaluation}", 
                nameof(HighCardScoreKeeper), 
                playerEvaluation);
            return Helper.GenerateScores(CategoryEnum.HighCard, playerEvaluation, logger);
        }
    }
}

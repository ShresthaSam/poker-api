using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Interfaces;
using PokerApi.RankAssignment.Simple.Models;
using Serilog;
using System.Collections.Generic;

namespace PokerApi.RankAssignment.Simple.ScoreKeepers
{
    public class ThreeOfAKindScoreKeeper : IScoreKeeper
    {
        public int Order => 6;

        public IEnumerable<ScoreRecord> GenerateScores(
            IEnumerable<PokerHandDomainModel> pokerHands, 
            ICardConverter cardConverter, 
            ICardSorter cardSorter, 
            IPokerHandRecognizer pokerHandRecognizer, 
            ILogger logger)
        {
            var playerEvaluation = new Dictionary<Player, (bool, int)>();
            foreach (var hand in pokerHands)
            {
                var cards = Helper.GetCardItems(hand, cardConverter);
                var evaluation = pokerHandRecognizer.IsThreeOfAKind(cards);
                playerEvaluation.Add(hand.Player, evaluation);
            }
            logger.Debug("ScoreKeeper:{ScoreKeeper} PlayerEvaluation:{@PlayerEvaluation}", 
                nameof(ThreeOfAKindScoreKeeper), 
                playerEvaluation);
            return Helper.GenerateScores(CategoryEnum.ThreeOfAKind, playerEvaluation, logger);
        }
    }
}

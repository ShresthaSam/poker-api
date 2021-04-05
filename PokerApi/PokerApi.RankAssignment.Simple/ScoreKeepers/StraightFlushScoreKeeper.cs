using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Interfaces;
using PokerApi.RankAssignment.Simple.Models;
using Serilog;
using System.Collections.Generic;

namespace PokerApi.RankAssignment.Simple.ScoreKeepers
{
    public class StraightFlushScoreKeeper : IScoreKeeper
    {
        public int Order => 1;
        public IEnumerable<ScoreRecord> GenerateScores(
            IEnumerable<PokerHandDomainModel> pokerHands,
            ICardConverter cardConverter,
            ICardSorter cardSorter,
            IPokerHandRecognizer pokerHandRecognizer,
            ILogger logger)
        {
            var playerEvaluation = new Dictionary<Player, (bool, int)>();
            foreach(var hand in pokerHands)
            {
                var cards = Helper.GetCardItems(hand, cardConverter);
                var sortedCards = cardSorter.Sort(cards);
                var evaluation = pokerHandRecognizer.IsStraightFlush(sortedCards);
                playerEvaluation.Add(hand.Player, evaluation);
            }
            logger.Debug("ScoreKeeper:{ScoreKeeper} PlayerEvaluation:{@PlayerEvaluation}", 
                nameof(StraightFlushScoreKeeper), 
                playerEvaluation);
            return Helper.GenerateScores(CategoryEnum.StraightFlush, playerEvaluation, logger);
        }
    }
}

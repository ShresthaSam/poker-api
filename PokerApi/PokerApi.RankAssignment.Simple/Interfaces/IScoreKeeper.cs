using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Models;
using Serilog;
using System.Collections.Generic;

namespace PokerApi.RankAssignment.Simple.Interfaces
{
    /// <summary>
    /// Each score keeper will inspect the poker hand passed to it.
    /// If it finds that the hand of a given player is eligible/matched for what 
    /// it is designed to keep the score for, then it will compute and assign the score.
    /// If the hand is ineligible, the scrore keeper will assign zero.
    /// </summary>
    public interface IScoreKeeper
    {
        public int Order { get; }
        public IEnumerable<ScoreRecord> GenerateScores(
            IEnumerable<PokerHandDomainModel> pokerHands,
            ICardConverter cardConverter,
            ICardSorter cardSorter,
            IPokerHandRecognizer pokerHandRecognizer,
            ILogger logger);
    }
}

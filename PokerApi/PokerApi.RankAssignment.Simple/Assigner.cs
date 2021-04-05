using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Abstractions;
using PokerApi.RankAssignment.Simple.Interfaces;
using PokerApi.RankAssignment.Simple.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PokerApi.RankAssignment.Simple
{
    public class Assigner : IRankAssigner
    {
        private readonly bool _enableRankGaps;

        public Assigner() { _enableRankGaps = false; }
        public Assigner(bool enableRankGaps){ _enableRankGaps = enableRankGaps; }

        public IEnumerable<Ranking> AssignRanks(IEnumerable<PokerHandDomainModel> pokerHands)
        {
            throw new System.NotImplementedException("Use the method overload that provides helper classes");
        }

        /// <summary>
        /// Algorithm of this Assigner is based on evaluating every player's cards
        /// against the known poker hands of StraightFlush, FourOfAKind, FullHouse, Flush, 
        /// Straight, ThreeOfAKind, TwoPairs, Pair and HighCard in that order.
        /// So the order is important in this algorithm. 
        /// When all the players have been matched against a known hand (which may well occur 
        /// before all the known hands are evaluated against), the algorithm generates scores
        /// and applies ranks and exits.
        /// </summary>
        /// <param name="pokerHands"></param>
        /// <param name="logger"></param>
        /// <param name="cardConverter"></param>
        /// <param name="cardSorter"></param>
        /// <param name="pokerHandRecognizer"></param>
        /// <returns></returns>
        public IEnumerable<Ranking> AssignRanks(
            IEnumerable<PokerHandDomainModel> pokerHands,
            ILogger logger,
            ICardConverter cardConverter,
            ICardSorter cardSorter,
            IPokerHandRecognizer pokerHandRecognizer)
        {
            var rankings = new List<Ranking>();
            int rankIncrement = 0;
            var remainingPokerHands = pokerHands;
            var pokerHandsCount = pokerHands.Count();
            foreach (var scorekeeper in GetOrderedScoreKeeperInstances())
            {
                var generateScoresMethod = scorekeeper.Value.GetType().GetMethod(Constants.SCORE_KEEPER_METHOD_ORDER);
                var scores = (IEnumerable<ScoreRecord>) generateScoresMethod.Invoke(
                    scorekeeper.Value, 
                    new object[] { remainingPokerHands, cardConverter, cardSorter, pokerHandRecognizer, logger });

                rankings.AddRange(ExtractRankings(scores, rankIncrement));

                remainingPokerHands = GetRemainingPokerHands(remainingPokerHands, scores);
                if (remainingPokerHands.Count() == 0) return rankings;
                rankIncrement = pokerHandsCount - remainingPokerHands.Count();
            }

            return rankings;
        }

        private SortedDictionary<int, object> GetOrderedScoreKeeperInstances()
        {
            var types = typeof(Assigner).Assembly.GetTypes()
                .Where(p => typeof(IScoreKeeper).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .ToList();
            var sortedInstances = new SortedDictionary<int, object>();
            foreach (var t in types)
            {
                var instance = Activator.CreateInstance(t);
                PropertyInfo pinfo = t.GetProperty(Constants.SCORE_KEEPER_PROPERTY_ORDER);
                var value = pinfo.GetValue(instance, null);
                sortedInstances.Add((int)value, instance);
            };
            return sortedInstances;
        }

        private IEnumerable<Ranking> ExtractRankings(IEnumerable<Models.ScoreRecord> scores, int rankIncrement)
        {
            var rankings = scores.Where(i => i.MatchedForCategory)
                .Select(i => new Ranking
                {
                    Rank = i.Rank + rankIncrement,
                    Player = i.Player,
                    Hand = i.Category.ToString(),
                    RankReason = i.RankReason
                });
            return rankings;
        }

        private IEnumerable<PokerHandDomainModel> GetRemainingPokerHands(IEnumerable<PokerHandDomainModel> pokerHands, IEnumerable<Models.ScoreRecord> scores)
        {
            var remainingPlayers = scores.Where(i => !i.MatchedForCategory).Select(i => i.Player);
            return pokerHands.Where(p => remainingPlayers.Contains(p.Player));
        }
    }
}

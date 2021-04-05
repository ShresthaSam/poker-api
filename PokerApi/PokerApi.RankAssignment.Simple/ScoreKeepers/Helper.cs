using PokerApi.Common.Interfaces;
using PokerApi.Common.Models;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.RankAssignment.Simple.ScoreKeepers
{
    public class Helper
    {
        public static List<CardItem> GetCardItems(PokerHandDomainModel hand, ICardConverter cardConverter)
        {
            return new List<CardItem>
            {
                cardConverter.ConvertEnumToItem(hand.Card1),
                cardConverter.ConvertEnumToItem(hand.Card2),
                cardConverter.ConvertEnumToItem(hand.Card3),
                cardConverter.ConvertEnumToItem(hand.Card4),
                cardConverter.ConvertEnumToItem(hand.Card5)
            };
        }

        public static IEnumerable<ScoreRecord> GenerateScores(
            CategoryEnum category,
            Dictionary<Player, (bool, int)> playerEvaluation, 
            Serilog.ILogger logger)
        {
            var scores = new List<ScoreRecord>();
            var groups = playerEvaluation.GroupBy(i => i.Value.Item2).OrderByDescending(i => i.Key);
            int rank = 0;
            foreach (var g in groups)
            {
                rank++;
                var transformed = g.Select(i => new ScoreRecord
                {
                    Rank = g.Key > 0 ? rank : 0,
                    Category = category,
                    MatchedForCategory = g.Key > 0,
                    Player = i.Key,
                    RankReason = g.Key > 0
                        ? $"Card value of {g.Key} in {category}"
                        : $"Not a {category}"
                });
                scores.AddRange(transformed);
            }
            logger.Debug("ScoreCategory:{ScoreCategory} Scores:{@Scores}", category, scores);
            return scores;
        }

        public static IEnumerable<ScoreRecord> GenerateScores(
            CategoryEnum category,
            Dictionary<Player, (bool, List<int>)> playerEvaluation,
            Serilog.ILogger logger)
        {
            var scores = new List<ScoreRecord>();

            var unmatchedScoreRecords = playerEvaluation
                .Where(i => !i.Value.Item1)
                .Select(i =>
                    new ScoreRecord
                    {
                        Rank = 0,
                        Category = category,
                        MatchedForCategory = false,
                        Player = i.Key,
                        RankReason = $"Not a {category}"
                    });
            scores.AddRange(unmatchedScoreRecords);

            var matchedGroups = playerEvaluation
                .Where(i => i.Value.Item1)
                .GroupBy(i => i.Value.Item2[0])
                .OrderByDescending(i => i.Key);

            int rank = 0;
            foreach (var g in matchedGroups)
            {
                rank++;

                if (g.Count() > 1)
                {
                    var subScores = GenerateScoresForSameTops(g, category);
                    var first = true;
                    int diff = 0;
                    foreach (var score in subScores.OrderBy(i => i.Rank))
                    {
                        if (first)
                        {
                            diff = rank - score.Rank;
                            score.Rank = rank;
                            scores.Add(score);
                            first = false;
                        } else
                        {
                            score.Rank = score.Rank + diff;
                            scores.Add(score);
                            rank = score.Rank;
                        }
                    }
                }
                else
                {
                    var records = g.Select(i => new ScoreRecord
                    {
                        Rank = rank,
                        Category = category,
                        MatchedForCategory = true,
                        Player = i.Key,
                        RankReason = $"Card value of {g.Key} in {category}"
                    });

                    scores.AddRange(records);
                }
            }
            logger.Debug("ScoreCategory:{ScoreCategory} Scores:{@Scores}", category, scores);
            return scores;
        }

        /// <summary>
        /// When invoked by TwoPairsScoreKeeper, KeyValuePair.Value.Item2 is the list of 
        /// card values of pairs in descending order and KeValuePair.Value.Item3 is the 
        /// remaining card value.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="playerEvaluation"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IEnumerable<ScoreRecord> GenerateScores(
            CategoryEnum category,
            Dictionary<Player, (bool, List<int>, int)> playerEvaluation,
            Serilog.ILogger logger)
        {
            var scores = new List<ScoreRecord>();

            var unmatchedScoreRecords = playerEvaluation
                .Where(i => !i.Value.Item1)
                .Select(i =>
                    new ScoreRecord
                    {
                        Rank = 0,
                        Category = category,
                        MatchedForCategory = false,
                        Player = i.Key,
                        RankReason = $"Not a {category}"
                    });
            scores.AddRange(unmatchedScoreRecords);

            var matchedGroups = playerEvaluation
                .Where(i => i.Value.Item1)
                .GroupBy(i => i.Value.Item2[0])
                .OrderByDescending(i => i.Key);

            int rank = 0;
            foreach (var g in matchedGroups)
            {
                rank++;

                if (g.Count() > 1)
                {
                    var subScores = GenerateScoresForSameTops(g, category);
                    var first = true;
                    int diff = 0;
                    foreach (var score in subScores.OrderBy(i => i.Rank))
                    {
                        if (first)
                        {
                            diff = rank - score.Rank;
                            score.Rank = rank;
                            scores.Add(score);
                            first = false;
                        }
                        else
                        {
                            score.Rank = score.Rank + diff;
                            scores.Add(score);
                        }
                    }
                }
                else
                {
                    var records = g.Select(i => new ScoreRecord
                    {
                        Rank = rank,
                        Category = category,
                        MatchedForCategory = true,
                        Player = i.Key,
                        RankReason = $"Card value of {g.Key} in {category}"
                    });

                    scores.AddRange(records);
                }
            }
            logger.Debug("ScoreCategory:{ScoreCategory} Scores:{@Scores}", category, scores);
            return scores;
        }

        /// <summary>
        /// When invoked by PairScoreKeeper, KeyValuePair.Value.Item2 is card value of the pair
        /// and KeValuePair.Value.Item3 is the remaining card values in descending order.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="playerEvaluation"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IEnumerable<ScoreRecord> GenerateScores(
            CategoryEnum category,
            Dictionary<Player, (bool, int, List<int>)> playerEvaluation,
            Serilog.ILogger logger)
        {
            var scores = new List<ScoreRecord>();

            var unmatchedScoreRecords = playerEvaluation
                .Where(i => !i.Value.Item1)
                .Select(i =>
                    new ScoreRecord
                    {
                        Rank = 0,
                        Category = category,
                        MatchedForCategory = false,
                        Player = i.Key,
                        RankReason = $"Not a {category}"
                    });
            scores.AddRange(unmatchedScoreRecords);

            var matchedGroups = playerEvaluation
                .Where(i => i.Value.Item1)
                .GroupBy(i => i.Value.Item2)
                .OrderByDescending(i => i.Key);

            int rank = 0;
            foreach (var g in matchedGroups)
            {
                rank++;

                if (g.Count() > 1)
                {
                    var subScores = GenerateScoresForSameTops(g, category);
                    var first = true;
                    int diff = 0;
                    foreach (var score in subScores.OrderBy(i => i.Rank))
                    {
                        if (first)
                        {
                            diff = rank - score.Rank;
                            score.Rank = rank;
                            scores.Add(score);
                            first = false;
                        }
                        else
                        {
                            score.Rank = score.Rank + diff;
                            scores.Add(score);
                        }
                    }
                }
                else
                {
                    var records = g.Select(i => new ScoreRecord
                    {
                        Rank = rank,
                        Category = category,
                        MatchedForCategory = true,
                        Player = i.Key,
                        RankReason = $"Card value of {g.Key} in {category}"
                    });

                    scores.AddRange(records);
                }
            }
            logger.Debug("ScoreCategory:{ScoreCategory} Scores:{@Scores}", category, scores);
            return scores;
        }


        #region private methods
        private static IEnumerable<ScoreRecord> GenerateScoresForSameTops(
            IGrouping<int, KeyValuePair<Player, (bool, List<int>)>> group,
            CategoryEnum category)
        {
            var scores = new List<ScoreRecord>();
            var rankedPlayers = RankPlayersBasedonOnCardValues(
                group.Select(i => i).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Item2));

            foreach (var ranking in rankedPlayers)
            {
                var cards = string.Join(",", group
                    .FirstOrDefault(i => i.Key == ranking.Key).Value.Item2);
                scores.Add(new ScoreRecord
                {
                    Category = category,
                    MatchedForCategory = true,
                    Player = ranking.Key,
                    Rank = ranking.Value,
                    RankReason = $"A {category} hand of [{cards}]"
                });
            }

            return scores;
        }

        /// <summary>
        /// For Pairs -> KeyValuePair.Value.Item2 is the card value of pair in 
        /// and KeValuePair.Value.Item3 is the list of remaining card values.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private static IEnumerable<ScoreRecord> GenerateScoresForSameTops(
            IGrouping<int, KeyValuePair<Player, (bool, int, List<int>)>> group,
            CategoryEnum category)
        {
            var scores = new List<ScoreRecord>();
            var rankedPlayers = RankPlayersBasedonOnCardValues(
                group.Select(i => i).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Item3));

            foreach (var ranking in rankedPlayers)
            {
                var handValue = group
                    .FirstOrDefault(i => i.Key == ranking.Key).Value.Item2;

                var highCardValues = string.Join(",", group
                    .FirstOrDefault(i => i.Key == ranking.Key).Value.Item3);
                scores.Add(new ScoreRecord
                {
                    Category = category,
                    MatchedForCategory = true,
                    Player = ranking.Key,
                    Rank = ranking.Value,
                    RankReason = $"A {category} hand value of {handValue} and high card values of [{highCardValues}]"
                });
            }

            return scores;
        }

        /// <summary>
        /// For TwoPairs -> KeyValuePair.Value.Item2 is the list of card values of pairs in 
        /// descending order and KeValuePair.Value.Item3 is the remaining card value.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private static IEnumerable<ScoreRecord> GenerateScoresForSameTops(
            IGrouping<int, KeyValuePair<Player, (bool, List<int>, int)>> group,
            CategoryEnum category)
        {
            var scores = new List<ScoreRecord>();
            var rankedPlayers = RankPlayersBasedonOnCardValues(
                group.Select(i => i).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Item2));

            // Further rank the players with the remaining card value if the pairs were the same
            int rank = 0;
            var remaingCardsOfSamePairsPlayers = new Dictionary<Player, int>();
            foreach (var samePairGroup in rankedPlayers
                .GroupBy(i => i.Value)
                .OrderBy(i => i.Key))
            {
                if (samePairGroup.Count() > 1)
                {
                    foreach (var kvp in samePairGroup.Select(i => i))
                    {
                        var remaingCardValue = group.FirstOrDefault(i => i.Key == kvp.Key).Value.Item3;
                        remaingCardsOfSamePairsPlayers.Add(kvp.Key, remaingCardValue);
                    }
                    int index = 0;
                    int cardValue = 0;
                    foreach(var kvpOrdered in remaingCardsOfSamePairsPlayers.OrderByDescending(i => i.Value))
                    {
                        index++;
                        var highCards = string.Join(",", group.FirstOrDefault(i => i.Key == kvpOrdered.Key).Value.Item2);
                        var remainingCard = kvpOrdered.Value;
                        if (index == 1)
                        {
                            cardValue = kvpOrdered.Value;
                            rank = rank + 1;
                            scores.Add(new ScoreRecord
                            {
                                Category = category,
                                MatchedForCategory = true,
                                Player = kvpOrdered.Key,
                                Rank = rank,
                                RankReason = $"A {category} hand with high card values of [{highCards}] and remaining card value of {remainingCard}"
                            });
                        } else
                        {
                            rank = kvpOrdered.Value == cardValue ? rank : rank + 1;
                            scores.Add(new ScoreRecord
                            {
                                Category = category,
                                MatchedForCategory = true,
                                Player = kvpOrdered.Key,
                                Rank = rank,
                                RankReason = $"A {category} hand with high card values of [{highCards}] and remaining card value of {remainingCard}"
                            });
                            cardValue = kvpOrdered.Value;
                        }
                        
                    }
                } else
                {
                    foreach (var kvp in samePairGroup.Select(i => i))
                    {
                        var highCards = string.Join(",", group.FirstOrDefault(i => i.Key == kvp.Key).Value.Item2);
                        rank = rank + 1;
                        scores.Add(new ScoreRecord
                        {
                            Category = category,
                            MatchedForCategory = true,
                            Player = kvp.Key,
                            Rank = rank,
                            RankReason = $"A {category} hand with high card values of [{highCards}]"
                        });
                    } 
                }
            }
            return scores;
        }

        private static Dictionary<Player, int> RankPlayersBasedonOnCardValues(
            IDictionary<Player, List<int>> playerCards)
        {
            var scores = new List<ScoreRecord>();
            var overall = new Dictionary<Player, int>();
            var players = new List<Player>();

            foreach (var kvp in playerCards)
            {
                players.Add(kvp.Key);
            }

            var differingRanks = new Dictionary<string, string>();
            var equalRanks = new Dictionary<string, string>();

            for (int i = 0; i < players.Count; i++)
            {
                var currentPlayer = players[i];
                foreach (var otherPlayer in players)
                {
                    if (currentPlayer != otherPlayer)
                    {
                        var added = false;
                        for (int j = 0; j < playerCards[players[0]].Count; j++)
                        {
                            if (added) break;
                            var currV = playerCards[currentPlayer][j];
                            var otherV = playerCards[otherPlayer][j];
                            if (currV > otherV)
                            {
                                var key = $"{currentPlayer.UserId}|{otherPlayer.UserId}";
                                var val = $"{currentPlayer.UserId}";
                                var keyCheck = $"{otherPlayer.UserId}|{currentPlayer.UserId}";
                                if (!differingRanks.ContainsKey(key))
                                {
                                    if (!differingRanks.ContainsKey(keyCheck))
                                        differingRanks.Add(key, val);
                                    added = true;
                                }
                            }
                            if (currV < otherV)
                            {
                                var key = $"{currentPlayer.UserId}|{otherPlayer.UserId}";
                                var val = $"{otherPlayer.UserId}";
                                var keyCheck = $"{otherPlayer.UserId}|{currentPlayer.UserId}";
                                if (!differingRanks.ContainsKey(key))
                                {
                                    if (!differingRanks.ContainsKey(keyCheck))
                                        differingRanks.Add(key, val);
                                    added = true;
                                }
                            }
                            if (!added && j == playerCards[players[0]].Count - 1)
                            {
                                var key = $"{currentPlayer.UserId}|{otherPlayer.UserId}";
                                var val = "equal";
                                var keyCheck = $"{otherPlayer.UserId}|{currentPlayer.UserId}";
                                if (!equalRanks.ContainsKey(keyCheck) && !equalRanks.ContainsKey(key))
                                {
                                    equalRanks.Add(key, val);
                                }
                            }
                        }
                    }
                }
            }

            // prune differingRanks
            foreach (var kvpEqual in equalRanks)
            {
                var arrEqual = kvpEqual.Key.Split("|".ToCharArray());
                var remove = arrEqual[1];
                foreach (var kvpDiffering in differingRanks)
                {
                    var arrDiffering = kvpDiffering.Key.Split("|".ToCharArray());
                    if (arrDiffering[0] == remove || arrDiffering[1] == remove)
                        differingRanks.Remove(kvpDiffering.Key);
                }
            }

            // get number of distinct players
            var distinctPlayers = new List<Player>();
            foreach (var kvpDiffering in differingRanks)
            {
                var arr = kvpDiffering.Key.Split("|".ToCharArray());
                var player1 = players.FirstOrDefault(i => i.UserId == arr[0]);
                var player2 = players.FirstOrDefault(i => i.UserId == arr[1]);
                if (!distinctPlayers.Contains(player1))
                {
                    distinctPlayers.Add(player1);
                }
                if (!distinctPlayers.Contains(player2))
                {
                    distinctPlayers.Add(player2);
                }
            }

            // init with the lowest rank
            foreach (var kvpDiffering in differingRanks)
            {
                var arr = kvpDiffering.Key.Split("|".ToCharArray());
                var player1 = players.FirstOrDefault(i => i.UserId == arr[0]);
                var player2 = players.FirstOrDefault(i => i.UserId == arr[1]);
                if (!overall.ContainsKey(player1))
                    overall.Add(player1, distinctPlayers.Count);
                if (!overall.ContainsKey(player2))
                    overall.Add(player2, distinctPlayers.Count);
            }

            // identify players with highest rank to lowest
            var rank = 1;
            var index = 0;
            foreach (var kvpDiffering in differingRanks)
            {
                var arrDiffering = kvpDiffering.Key.Split("|".ToCharArray());
                var currentPlayer = arrDiffering[index];
                var currentPlayerWinner = false;
                foreach (var kvp in differingRanks)
                {
                    var arr = kvp.Key.Split("|".ToCharArray());
                    if ((arr[0] == currentPlayer || arr[1] == currentPlayer))
                    {
                        if (kvp.Value == currentPlayer) currentPlayerWinner = true;
                        if (kvp.Value != currentPlayer)
                        {
                            currentPlayerWinner = false;
                            break;
                        }
                    }
                }
                if (currentPlayerWinner)
                {
                    overall[players.FirstOrDefault(i => i.UserId == currentPlayer)] = rank;
                    var keys = differingRanks.Where(i => i.Value == currentPlayer).Select(i => i.Key);
                    foreach (var key in keys)
                    {
                        differingRanks.Remove(key);
                    }
                    rank++;
                }

                index = index == 0 ? 1 : 0;

                arrDiffering = kvpDiffering.Key.Split("|".ToCharArray());
                currentPlayer = arrDiffering[index];
                currentPlayerWinner = false;
                foreach (var kvp in differingRanks)
                {
                    var arr = kvp.Key.Split("|".ToCharArray());
                    if ((arr[0] == currentPlayer || arr[1] == currentPlayer))
                    {
                        if (kvp.Value == currentPlayer) currentPlayerWinner = true;
                        if (kvp.Value != currentPlayer)
                        {
                            currentPlayerWinner = false;
                            break;
                        }
                    }
                }
                if (currentPlayerWinner)
                {
                    overall[players.FirstOrDefault(i => i.UserId == currentPlayer)] = rank;
                    var keys = differingRanks.Where(i => i.Value == currentPlayer).Select(i => i.Key);
                    foreach (var key in keys)
                    {
                        differingRanks.Remove(key);
                    }
                    rank++;
                }

                index = 0;
            }

            if (differingRanks.Count == 1)
            {
                foreach (var kvp in differingRanks)
                {
                    overall[players.FirstOrDefault(i => i.UserId == kvp.Value)] = rank;
                }
            }

            // special case - equal ranks only
            if (differingRanks.Count == 0)
            {
                foreach (var kvp in equalRanks)
                {
                    var arr = kvp.Key.Split("|".ToCharArray());
                    var player1 = players.FirstOrDefault(p => p.UserId == arr[0]);
                    if (!overall.ContainsKey(player1))
                        overall.Add(player1, 1);
                }
            }

            // all cases
            foreach (var kvp in equalRanks)
            {
                var arr = kvp.Key.Split("|".ToCharArray());
                var player = players.FirstOrDefault(i => i.UserId == arr[0]);
                if (overall.Keys.Contains(player))
                {
                    var equalPlayer = players.FirstOrDefault(i => i.UserId == arr[1]);
                    if (!overall.ContainsKey(equalPlayer))
                        overall.Add(equalPlayer, overall[player]);
                }
            }
            return overall;
        }
        #endregion
    }
}

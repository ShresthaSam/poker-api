using PokerApi.Common.Interfaces;
using PokerApi.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Common
{
    public class PokerHandRecognizer : IPokerHandRecognizer
    {
        Dictionary<string, int> keyedCardNumbers = new Dictionary<string, int>
        {
            {"2", 1}, {"3", 2 }, {"4", 3 }, {"5", 4 }, {"6", 5 }, { "7", 6 },
            {"8", 7 }, {"9", 8 }, {"10", 9 }, {"J", 10 }, {"Q", 11 }, {"K", 12 },
            {"A", 13 }
        };

        private readonly bool _enableAceLowFiveHigh;

        public PokerHandRecognizer() { _enableAceLowFiveHigh = false;  }
        public PokerHandRecognizer(bool enableAceLowFiveHigh){ _enableAceLowFiveHigh = enableAceLowFiveHigh; }

        /// <summary>
        /// If true, returns (true, highest card value of the StraightFlush)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="sortedCards"></param>
        /// <returns></returns>
        public (bool, int) IsStraightFlush(IEnumerable<CardItem> sortedCards)
        {
            return IsSequence(sortedCards, _enableAceLowFiveHigh, true);
        }

        /// <summary>
        /// If true, returns (true, card value of the FourOfAKind)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsFourOfAKind(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var kvpList = numberTracker.Where(kvp => kvp.Value == 4).ToList();
            return (kvpList.Count() > 0) ? (true, keyedCardNumbers[kvpList[0].Key] + 1) : (false, 0);
        }

        /// <summary>
        /// If true, returns (true, card value of the ThreeOfAKind within the FullHouse)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsFullHouse(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var kvpList3 = numberTracker.Where(kvp => kvp.Value == 3).ToList();
            var kvpList2 = numberTracker.Where(kvp => kvp.Value == 2).ToList();
            return (kvpList3.Count() > 0 && kvpList2.Count() > 0) ? (true, keyedCardNumbers[kvpList3[0].Key] + 1) : (false, 0);
        }

        /// <summary>
        /// If true, returns (true, list of card values in descending order of the Flush)
        /// If false, returns (false, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>) IsFlush(IEnumerable<CardItem> hand)
        {
            var tuple = GetSuitTrackerAndNumbersInDescendingOrder(hand);
            var kvpList = tuple.Item1.Where(kvp => kvp.Value == 5).ToList();
            var numbers = tuple.Item2;
            
            // Find number of gaps (if no gaps, this would be StraightFlush)
            var gaps = Enumerable.Range(numbers.Min(), numbers.Count).Except(numbers).Count();
            return (kvpList.Count() > 0 && gaps > 0) ? (true, numbers) : (false, new List<int>());
        }

        /// <summary>
        /// If true, returns (true, highest card value of the Straight)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsStraight(IEnumerable<CardItem> hand)
        {
            return IsSequence(hand, _enableAceLowFiveHigh, false);
        }

        /// <summary>
        /// If true, returns (true, card value of the ThreeOfAKind)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsThreeOfAKind(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var kvpList3 = numberTracker.Where(kvp => kvp.Value == 3).ToList();
            var kvpList2 = numberTracker.Where(kvp => kvp.Value == 2).ToList();
            return (kvpList3.Count() > 0 && kvpList2.Count() == 0) ? (true, keyedCardNumbers[kvpList3[0].Key]+1) : (false, 0);
        }

        /// <summary>
        /// If true, returns (true, list of two card values in descending order corresponding to the TwoPairs, the remaining card value)
        /// If false, returns (false, empty list, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>, int) IsTwoPairs(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var highCardsOfPair = new List<int>();
            var kvpList2 = numberTracker.Where(kvp => kvp.Value == 2).ToList();
            var kvpList1 = numberTracker.Where(kvp => kvp.Value == 1).ToList();
            if (kvpList2.Count == 2)
            {
                highCardsOfPair.Add(keyedCardNumbers[kvpList2[0].Key]+1);
                highCardsOfPair.Add(keyedCardNumbers[kvpList2[1].Key]+1);
                highCardsOfPair = highCardsOfPair.OrderByDescending(i => i).ToList();
            }
            return (kvpList2.Count() == 2) ? (true, highCardsOfPair, keyedCardNumbers[kvpList1[0].Key]+1) : (false, new List<int>(), 0);
        }

        /// <summary>
        /// If true, returns (true, card value of the Pair, list of remaining card values in descending order)
        /// If false, returns (false, 0, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int, List<int>) IsPair(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var highCardsOutsidePair = new List<int>();
            var kvpList2 = numberTracker.Where(kvp => kvp.Value == 2).ToList();
            var kvpList1 = numberTracker.Where(kvp => kvp.Value == 1).ToList();
            var kvpList3 = numberTracker.Where(kvp => kvp.Value == 3).ToList();
            if (kvpList2.Count == 1 && kvpList3.Count == 0)
            {
                highCardsOutsidePair.Add(keyedCardNumbers[kvpList1[0].Key]+1);
                highCardsOutsidePair.Add(keyedCardNumbers[kvpList1[1].Key]+1);
                highCardsOutsidePair.Add(keyedCardNumbers[kvpList1[2].Key]+1);
                highCardsOutsidePair = highCardsOutsidePair.OrderByDescending(i => i).ToList();
            }
            return (kvpList2.Count() == 1 && kvpList3.Count() == 0) ? (true, keyedCardNumbers[kvpList2[0].Key]+1, highCardsOutsidePair) : (false, 0, new List<int>());
        }

        /// <summary>
        /// If true, returns (true, list of card values in descending order)
        /// If false, returns (false, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>) IsHighCard(IEnumerable<CardItem> hand)
        {
            var numberTracker = GetNumberTracker(hand);
            var highCards = new List<int>();
            var kvpList1 = numberTracker.Where(kvp => kvp.Value == 1).ToList();
            if (kvpList1.Count == 5)
            {
                highCards.Add(keyedCardNumbers[kvpList1[0].Key]+1);
                highCards.Add(keyedCardNumbers[kvpList1[1].Key]+1);
                highCards.Add(keyedCardNumbers[kvpList1[2].Key]+1);
                highCards.Add(keyedCardNumbers[kvpList1[3].Key]+1);
                highCards.Add(keyedCardNumbers[kvpList1[4].Key]+1);
                highCards = highCards.OrderByDescending(i => i).ToList();
            }
            var isStraightFlush = IsStraightFlush(hand);
            var isFlush = IsFlush(hand);
            var isStraight = IsStraight(hand);

            return (kvpList1.Count() == 5 
                && !isStraightFlush.Item1 
                && !isFlush.Item1
                && !isStraight.Item1) ? (true, highCards) : (false, new List<int>());
        }


        private Dictionary<string, int> GetNumberTracker(IEnumerable<CardItem> hand)
        {
            var numberTracker = new Dictionary<string, int>();
            foreach (var c in hand)
            {
                if (!numberTracker.ContainsKey(c.Number))
                {
                    numberTracker.Add(c.Number, 1);
                }
                else
                {
                    var occurrence = numberTracker[c.Number];
                    numberTracker[c.Number] = occurrence + 1;
                }
            }
            return numberTracker;
        }

        private (Dictionary<string, int>, List<int>) GetSuitTrackerAndNumbersInDescendingOrder(IEnumerable<CardItem> hand)
        {
            var numbers = new List<int>();
            var suitTracker = new Dictionary<string, int>();
            foreach (var c in hand)
            {
                numbers.Add(keyedCardNumbers[c.Number] + 1);
                if (!suitTracker.ContainsKey(c.Suit))
                {
                    suitTracker.Add(c.Suit, 1);
                }
                else
                {
                    var occurrence = suitTracker[c.Suit];
                    suitTracker[c.Suit] = occurrence + 1;
                }
            }
            numbers = numbers.OrderByDescending(i => i).ToList();
            return (suitTracker, numbers);
        }

        private (bool, int) IsSequence(
            IEnumerable<CardItem> hand,
            bool enableAceLowFiveHigh = false,
            bool suitsMustBeSame = false)
        {
            var isAceLowFiveHigh = false;
            var tuple = GetSuitTrackerAndNumbersInDescendingOrder(hand);
            var numbers = tuple.Item2;
            var kvpList = tuple.Item1.Where(kvp => kvp.Value == 5).ToList();
            if (enableAceLowFiveHigh)
            {
                var isAceLowFiveHighTuple = IsAceLowFiveHigh(numbers);
                isAceLowFiveHigh = isAceLowFiveHighTuple.Item1;
                numbers = isAceLowFiveHigh ? isAceLowFiveHighTuple.Item2 : numbers;
            }
            var gaps = isAceLowFiveHigh
                ? 0
                : Enumerable.Range(numbers.Min(), numbers.Count).Except(numbers).Count();
            return (kvpList.Count() == (suitsMustBeSame ? 1 : 0) && gaps == 0) ? (true, numbers[0]) : (false, 0);
        }

        /// <summary>
        /// Identifies if the list of numbers represents Ace-Low-Five-High and
        /// if so, switches the positions of Ace and Five and returns the switched list.
        /// </summary>
        /// <param name="sortedNumbers">Must be passed in descending order where Ace is always assumed a high card</param>
        /// <returns></returns>
        private (bool, List<int>) IsAceLowFiveHigh(List<int> sortedNumbers)
        {
            var isAceLowFiveHigh = false;
            if (sortedNumbers[0] == 14 
                && sortedNumbers[1] == 5
                && sortedNumbers[2] == 4
                && sortedNumbers[3] == 3
                && sortedNumbers[4] == 2)
            {
                sortedNumbers[0] = 5;
                sortedNumbers[1] = 4;
                sortedNumbers[2] = 3;
                sortedNumbers[3] = 2;
                sortedNumbers[4] = 0;
                isAceLowFiveHigh = true;
            }
            return (isAceLowFiveHigh, sortedNumbers);
        }

    }
}

using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.Common.Interfaces
{
    public interface IPokerHandRecognizer
    {
        /// <summary>
        /// If true, returns (true, highest card value of the StraightFlush)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="sortedCards"></param>
        /// <returns></returns>
        public (bool, int) IsStraightFlush(IEnumerable<CardItem> sortedCards);
        /// <summary>
        /// If true, returns (true, card value of the FourOfAKind)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsFourOfAKind(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, card value of the ThreeOfAKind within the FullHouse)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsFullHouse(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, list of card values in descending order of the Flush)
        /// If false, returns (false, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>) IsFlush(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, highest card value of the Straight)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsStraight(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, card value of the ThreeOfAKind)
        /// If false, returns (false, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int) IsThreeOfAKind(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, list of two card values in descending order corresponding to the TwoPairs, the remaining card value)
        /// If false, returns (false, empty list, 0)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>, int) IsTwoPairs(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, card value of the Pair, list of remaining card values in descending order)
        /// If false, returns (false, 0, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, int, List<int>) IsPair(IEnumerable<CardItem> hand);
        /// <summary>
        /// If true, returns (true, list of card values in descending order)
        /// If false, returns (false, empty list)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public (bool, List<int>) IsHighCard(IEnumerable<CardItem> hand);
    }
}

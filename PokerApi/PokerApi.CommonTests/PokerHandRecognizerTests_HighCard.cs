using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class PokerHandRecognizerTests_HighCard
    {
        [TestMethod]
        public void IsHighCard_Should_Return_True()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 2, Number = "A", Suit = "D" },
                new CardItem { Id = 15, Number = "5", Suit = "C" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 36, Number = "10", Suit = "S" }
            };
            var result = recognizer.IsHighCard(sortedCardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2[0] == 14);
            Assert.IsTrue(result.Item2[1] == 10);
            Assert.IsTrue(result.Item2[2] == 8);
            Assert.IsTrue(result.Item2[3] == 5);
            Assert.IsTrue(result.Item2[4] == 2);
        }

        [TestMethod]
        public void IsHighCard_Should_Return_False_For_Flush()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "10", Suit = "D" },
                new CardItem { Id = 0, Number = "J", Suit = "D" },
                new CardItem { Id = 0, Number = "4", Suit = "D" },
                new CardItem { Id = 0, Number = "K", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "D" }
            };
            var result = recognizer.IsHighCard(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2.Count == 0);
        }

        [TestMethod]
        public void IsHighCard_Should_Return_False_For_Straight()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "10", Suit = "D" },
                new CardItem { Id = 0, Number = "J", Suit = "S" },
                new CardItem { Id = 0, Number = "9", Suit = "H" },
                new CardItem { Id = 0, Number = "K", Suit = "D" },
                new CardItem { Id = 0, Number = "Q", Suit = "C" }
            };
            var result = recognizer.IsHighCard(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2.Count == 0);
        }

        [TestMethod]
        public void IsHighCard_Should_Return_False_For_StraightFlush()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "10", Suit = "D" },
                new CardItem { Id = 0, Number = "J", Suit = "D" },
                new CardItem { Id = 0, Number = "Q", Suit = "D" },
                new CardItem { Id = 0, Number = "K", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "D" }
            };
            var result = recognizer.IsHighCard(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2.Count == 0);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class PokerHandRecognizerTests_Pair
    {
        [TestMethod]
        public void IsPair_Should_Return_False()
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
            var result = recognizer.IsPair(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
            Assert.IsTrue(result.Item3.Count == 0);
        }

        [TestMethod]
        public void IsPair_Should_Return_False_For_FullHouse()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "3", Suit = "H" },
                new CardItem { Id = 0, Number = "3", Suit = "C" },
                new CardItem { Id = 0, Number = "A", Suit = "H" },
                new CardItem { Id = 0, Number = "3", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "S" }
            };
            var result = recognizer.IsPair(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
            Assert.IsTrue(result.Item3.Count == 0);
        }

        [TestMethod]
        public void IsPair_Should_Return_False_For_TwoPairs()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "10", Suit = "D" },
                new CardItem { Id = 0, Number = "J", Suit = "S" },
                new CardItem { Id = 0, Number = "10", Suit = "H" },
                new CardItem { Id = 0, Number = "J", Suit = "C" },
                new CardItem { Id = 0, Number = "Q", Suit = "C" }
            };
            var result = recognizer.IsPair(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
            Assert.IsTrue(result.Item3.Count == 0);
        }

        [TestMethod]
        public void IsPair_Should_Return_True_Case1()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "10", Suit = "D" },
                new CardItem { Id = 0, Number = "J", Suit = "S" },
                new CardItem { Id = 0, Number = "10", Suit = "H" },
                new CardItem { Id = 0, Number = "3", Suit = "C" },
                new CardItem { Id = 0, Number = "Q", Suit = "C" }
            };
            var result = recognizer.IsPair(sortedCardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2 == 10);
            Assert.IsTrue(result.Item3[0] == 12);
            Assert.IsTrue(result.Item3[1] == 11);
            Assert.IsTrue(result.Item3[2] == 3);
        }
    }
}

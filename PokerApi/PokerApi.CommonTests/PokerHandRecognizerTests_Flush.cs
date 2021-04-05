using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class PokerHandRecognizerTests_Flush
    {
        [TestMethod]
        public void IsFlush_Should_Return_False()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 2, Number = "2", Suit = "D" },
                new CardItem { Id = 15, Number = "5", Suit = "C" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 36, Number = "10", Suit = "S" }
            };
            var result = recognizer.IsFlush(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2.Count == 0);
        }

        [TestMethod]
        public void IsFlush_Should_Return_False_For_StraightFlush()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 5, Number = "3", Suit = "H" },
                new CardItem { Id = 9, Number = "4", Suit = "H" },
                new CardItem { Id = 13, Number = "5", Suit = "H" },
                new CardItem { Id = 17, Number = "6", Suit = "H" }
            };
            var result = recognizer.IsFlush(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2.Count == 0);
        }

        [TestMethod]
        public void IsFlush_Should_Return_True_Case1()
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
            var result = recognizer.IsFlush(sortedCardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2[0] == 14);
            Assert.IsTrue(result.Item2[1] == 13);
            Assert.IsTrue(result.Item2[2] == 11);
            Assert.IsTrue(result.Item2[3] == 10);
            Assert.IsTrue(result.Item2[4] == 4);
        }
    }
}

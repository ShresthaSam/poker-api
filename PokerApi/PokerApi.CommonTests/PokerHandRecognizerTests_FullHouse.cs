using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class PokerHandRecognizerTests_FullHouse
    {
        [TestMethod]
        public void IsFullHouse_Should_Return_False()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "2", Suit = "H" },
                new CardItem { Id = 0, Number = "2", Suit = "D" },
                new CardItem { Id = 0, Number = "5", Suit = "C" },
                new CardItem { Id = 0, Number = "8", Suit = "H" },
                new CardItem { Id = 0, Number = "10", Suit = "S" }
            };
            var result = recognizer.IsFullHouse(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }

        [TestMethod]
        public void IsFullHouse_Should_Return_true_case1()
        {
            var recognizer = new PokerHandRecognizer();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "2", Suit = "H" },
                new CardItem { Id = 0, Number = "5", Suit = "D" },
                new CardItem { Id = 0, Number = "2", Suit = "C" },
                new CardItem { Id = 0, Number = "2", Suit = "S" },
                new CardItem { Id = 0, Number = "5", Suit = "S" }
            };
            var result = recognizer.IsFullHouse(cardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2 == 2);
        }

        [TestMethod]
        public void IsFullHouse_Should_Return_true_case2()
        {
            var recognizer = new PokerHandRecognizer();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "A", Suit = "D" },
                new CardItem { Id = 0, Number = "10", Suit = "H" },
                new CardItem { Id = 0, Number = "10", Suit = "C" },
                new CardItem { Id = 0, Number = "A", Suit = "S" },
                new CardItem { Id = 0, Number = "A", Suit = "C" }
            };
            var result = recognizer.IsFullHouse(cardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2 == 14);
        }
    }
}

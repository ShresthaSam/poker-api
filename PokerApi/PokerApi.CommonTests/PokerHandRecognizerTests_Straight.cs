using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class PokerHandRecognizerTests_Straight
    {
        [TestMethod]
        public void IsStraight_Should_Return_False()
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
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }

        [TestMethod]
        public void IsStraight_Should_Return_False_For_StraightFlush()
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
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }

        [TestMethod]
        public void IsStraight_Should_Return_True_Case1()
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
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2 == 13);
        }

        [TestMethod]
        public void IsStraight_Should_Return_False_When_AceLowFiveHigh_Is_Implicitly_Disabled()
        {
            var recognizer = new PokerHandRecognizer();
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "3", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "S" },
                new CardItem { Id = 0, Number = "2", Suit = "H" },
                new CardItem { Id = 0, Number = "4", Suit = "D" },
                new CardItem { Id = 0, Number = "5", Suit = "C" }
            };
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }

        [TestMethod]
        public void IsStraight_Should_Return_False_When_AceLowFiveHigh_Is_Explicitly_Disabled()
        {
            var recognizer = new PokerHandRecognizer(false);
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "3", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "S" },
                new CardItem { Id = 0, Number = "2", Suit = "H" },
                new CardItem { Id = 0, Number = "4", Suit = "D" },
                new CardItem { Id = 0, Number = "5", Suit = "C" }
            };
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }

        [TestMethod]
        public void IsStraight_Should_Return_True_When_AceLowFiveHigh_Is_Enabled()
        {
            var recognizer = new PokerHandRecognizer(true);
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "3", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "S" },
                new CardItem { Id = 0, Number = "2", Suit = "H" },
                new CardItem { Id = 0, Number = "4", Suit = "D" },
                new CardItem { Id = 0, Number = "5", Suit = "C" }
            };
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == true);
            Assert.IsTrue(result.Item2 == 5);
        }

        [TestMethod]
        public void IsStraight_Should_Return_False_For_StraightFlush_When_AceLowFiveHigh_Is_Enabled()
        {
            var recognizer = new PokerHandRecognizer(true);
            var sortedCardItems = new List<CardItem>
            {
                new CardItem { Id = 0, Number = "3", Suit = "D" },
                new CardItem { Id = 0, Number = "A", Suit = "D" },
                new CardItem { Id = 0, Number = "2", Suit = "D" },
                new CardItem { Id = 0, Number = "4", Suit = "D" },
                new CardItem { Id = 0, Number = "5", Suit = "D" }
            };
            var result = recognizer.IsStraight(sortedCardItems);

            Assert.IsTrue(result.Item1 == false);
            Assert.IsTrue(result.Item2 == 0);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class SimpleCardSorterTests
    {
        [TestMethod]
        public void Sort_Should_Return_OrderedItems_Case1()
        {
            var cardSorter = new SimpleCardSorter();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 15, Number = "5", Suit = "C" },
                new CardItem { Id = 36, Number = "10", Suit = "S" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 2, Number = "2", Suit = "D" }
            };
            var sorted = cardSorter.Sort(cardItems).ToList();

            Assert.IsTrue(sorted[0].Number == "2");
            Assert.IsTrue(sorted[1].Number == "2");
            Assert.IsTrue(sorted[2].Number == "5");
            Assert.IsTrue(sorted[3].Number == "8");
            Assert.IsTrue(sorted[4].Number == "10");
        }

        [TestMethod]
        public void Sort_Should_Return_OrderedItems_Case2()
        {
            var cardSorter = new SimpleCardSorter();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 15, Number = "5", Suit = "C" },
                new CardItem { Id = 38, Number = "J", Suit = "D" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 2, Number = "2", Suit = "D" }
            };
            var sorted = cardSorter.Sort(cardItems).ToList();

            Assert.IsTrue(sorted[0].Number == "2");
            Assert.IsTrue(sorted[1].Number == "2");
            Assert.IsTrue(sorted[2].Number == "5");
            Assert.IsTrue(sorted[3].Number == "8");
            Assert.IsTrue(sorted[4].Number == "J");
        }

        [TestMethod]
        public void Sort_Should_Treat_Ace_As_High_Card_Always_Case1()
        {
            var cardSorter = new SimpleCardSorter();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 1, Number = "2", Suit = "H" },
                new CardItem { Id = 52, Number = "A", Suit = "S" },
                new CardItem { Id = 38, Number = "J", Suit = "D" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 2, Number = "2", Suit = "D" }
            };
            var sorted = cardSorter.Sort(cardItems).ToList();

            Assert.IsTrue(sorted[0].Number == "2");
            Assert.IsTrue(sorted[1].Number == "2");
            Assert.IsTrue(sorted[2].Number == "8");
            Assert.IsTrue(sorted[3].Number == "J");
            Assert.IsTrue(sorted[4].Number == "A");
        }

        [TestMethod]
        public void Sort_Should_Treat_Ace_As_High_Card_Always_Case2()
        {
            var cardSorter = new SimpleCardSorter();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 37, Number = "J", Suit = "H" },
                new CardItem { Id = 52, Number = "A", Suit = "S" },
                new CardItem { Id = 38, Number = "J", Suit = "D" },
                new CardItem { Id = 25, Number = "8", Suit = "H" },
                new CardItem { Id = 43, Number = "Q", Suit = "C" }
            };
            var sorted = cardSorter.Sort(cardItems).ToList();

            Assert.IsTrue(sorted[0].Number == "8");
            Assert.IsTrue(sorted[1].Number == "J");
            Assert.IsTrue(sorted[2].Number == "J");
            Assert.IsTrue(sorted[3].Number == "Q");
            Assert.IsTrue(sorted[4].Number == "A");
        }

        [TestMethod]
        public void Sort_Should_Treat_Ace_As_High_Card_Always_Case3()
        {
            var cardSorter = new SimpleCardSorter();
            var cardItems = new List<CardItem>
            {
                new CardItem { Id = 37, Number = "2", Suit = "H" },
                new CardItem { Id = 52, Number = "A", Suit = "S" },
                new CardItem { Id = 38, Number = "4", Suit = "D" },
                new CardItem { Id = 25, Number = "3", Suit = "H" },
                new CardItem { Id = 43, Number = "5", Suit = "C" }
            };
            var sorted = cardSorter.Sort(cardItems).ToList();

            Assert.IsTrue(sorted[0].Number == "2");
            Assert.IsTrue(sorted[1].Number == "3");
            Assert.IsTrue(sorted[2].Number == "4");
            Assert.IsTrue(sorted[3].Number == "5");
            Assert.IsTrue(sorted[4].Number == "A");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;
using PokerApi.Common.Models;
using System;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class CardConverterTests
    {
        [TestMethod]
        public void ConvertItemToEnum_Should_Return_Enum()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D3;
            var cardItem = new CardItem { Id = 6, Number = "3", Suit = "D" };
            var converted = cardConverter.ConvertItemToEnum(cardItem);
            Assert.IsTrue(converted == cardEnum);
        }

        [TestMethod]
        public void ConvertItemToEnum_Should_Return_Enum_3Char()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D10;
            var cardItem = new CardItem { Id = 34, Number = "10", Suit = "D" };
            var converted = cardConverter.ConvertItemToEnum(cardItem);
            Assert.IsTrue(converted == cardEnum);
        }

        [TestMethod]
        public void Throw_When_ItemId_Is_Correct_But_Number_IsNot()
        {
            var cardConverter = new CardConverter();
            var cardItem = new CardItem { Id = 6, Number = "4", Suit = "D" };
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertItemToEnum(cardItem));
        }

        [TestMethod]
        public void Throw_When_ItemId_Is_Correct_But_Suit_IsNot()
        {
            var cardConverter = new CardConverter();
            var cardItem = new CardItem { Id = 6, Number = "3", Suit = "H" };
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertItemToEnum(cardItem));
        }

        [TestMethod]
        public void Throw_When_ItemId_Is_Not_Valid()
        {
            var cardConverter = new CardConverter();
            var cardItem = new CardItem { Id = 234, Number = "3", Suit = "H" };
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertItemToEnum(cardItem));
        }

        [TestMethod]
        public void ConvertItemIdToEnum_Should_Return_Enum()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D3;
            var converted = cardConverter.ConvertIdToEnum(6);
            Assert.IsTrue(converted == cardEnum);
        }

        [TestMethod]
        public void ConvertItemIdToEnum_Should_Throw_When_ItemId_Is_Not_Valid()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertIdToEnum(567));
        }

        [TestMethod]
        public void ConvertEnumToItem_Should_Return_Valid_Card_Item()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D3;
            var cardItem = cardConverter.ConvertEnumToItem(cardEnum);
            Assert.IsTrue(cardItem.Id == 6);
            Assert.IsTrue(cardItem.Suit == "D");
            Assert.IsTrue(cardItem.Number == "3");
        }

        [TestMethod]
        public void ConvertSuitAndNumberToEnum_Should_Return_Valid_CardEnum()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D3;
            var returnedEnum = cardConverter.ConvertSuitAndNumberToEnum("D3");
            Assert.IsTrue(returnedEnum == cardEnum);
        }

        [TestMethod]
        public void ConvertSuitAndNumberToEnum_Should_Return_Valid_CardEnum_Char3()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.H10;
            var returnedEnum = cardConverter.ConvertSuitAndNumberToEnum("H10");
            Assert.IsTrue(returnedEnum == cardEnum);
        }

        [TestMethod]
        public void ConvertSuitAndNumberToEnum_Should_Error_When_Input_String_Does_Not_Match_Case1()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertSuitAndNumberToEnum("H1"));
        }

        [TestMethod]
        public void ConvertSuitAndNumberToEnum_Should_Error_When_Input_String_Does_Not_Match_Case2()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertSuitAndNumberToEnum("D101"));
        }

        [TestMethod]
        public void ConvertSuitAndNumberToEnum_Should_Error_When_Input_String_Does_Not_Match_Case3()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertSuitAndNumberToEnum("2S"));
        }

        [TestMethod]
        public void ConvertNumberAndSuitToEnum_Should_Return_Valid_CardEnum()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.D3;
            var returnedEnum = cardConverter.ConvertNumberAndSuitToEnum("3D");
            Assert.IsTrue(returnedEnum == cardEnum);
        }

        [TestMethod]
        public void ConvertNumberAndSuitToEnum_Should_Return_Valid_CardEnum_Char3()
        {
            var cardConverter = new CardConverter();
            var cardEnum = CardEnum.C10;
            var returnedEnum = cardConverter.ConvertNumberAndSuitToEnum("10C");
            Assert.IsTrue(returnedEnum == cardEnum);
        }

        [TestMethod]
        public void ConvertNumberAndSuitToEnum_Should_Error_When_Input_String_Does_Not_Match_Case1()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertNumberAndSuitToEnum("100C"));
        }

        [TestMethod]
        public void ConvertNumberAndSuitToEnum_Should_Error_When_Input_String_Does_Not_Match_Case2()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertNumberAndSuitToEnum("1C"));
        }

        [TestMethod]
        public void ConvertNumberAndSuitToEnum_Should_Error_When_Input_String_Does_Not_Match_Case3()
        {
            var cardConverter = new CardConverter();
            Assert.ThrowsException<Exception>(() => cardConverter.ConvertNumberAndSuitToEnum("D1"));
        }
    }
}

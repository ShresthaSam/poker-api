using PokerApi.Common.Interfaces;
using PokerApi.Common.Models;
using System;

namespace PokerApi.Common
{
    public class CardConverter : ICardConverter
    {
        public CardItem ConvertEnumToItem(CardEnum cardEnum)
        {
            return new CardItem
            {
                Id = (int)cardEnum,
                Suit = cardEnum.ToString().Substring(0, 1),
                Number = cardEnum.ToString().Substring(1, cardEnum.ToString().Length-1)
            };
        }

        public CardEnum ConvertIdToEnum(int cardId)
        {
            if (Enum.IsDefined(typeof(CardEnum), cardId))
            {
                var enumValue = (CardEnum)cardId;
                return enumValue;
            }
            else
            {
                throw new Exception($"CardEnum not found for CardItem's Id={cardId}");
            }
        }

        public CardEnum ConvertItemToEnum(CardItem cardItem)
        {
            var id = cardItem.Id;
            var cardItemSuitAndNumber = $"{cardItem.Suit}{cardItem.Number}";
            if (Enum.IsDefined(typeof(CardEnum), id))
            {
                var enumValue = (CardEnum)id;
                if (enumValue.ToString() != cardItemSuitAndNumber)
                {
                    throw new Exception($"CardEnum is matched by CardItem's Id but the expected enum string of '{cardItemSuitAndNumber}' is instead '{enumValue.ToString()}'");
                }
                return enumValue;
            }
            else
            {
                throw new Exception($"CardEnum not found for CardItem's Id={id}");
            }
        }

        public CardEnum ConvertSuitAndNumberToEnum(string cardSuitAndNumber)
        {
            if (Enum.TryParse(typeof(CardEnum), cardSuitAndNumber, out var enumVal))
            {
                return (CardEnum)enumVal;
            }
            else
            {
                throw new Exception($"CardEnum not found for {cardSuitAndNumber}");
            }
        }

        public CardEnum ConvertNumberAndSuitToEnum(string cardNumberAndSuit)
        {
            var suit = cardNumberAndSuit.Substring(cardNumberAndSuit.Length - 1, 1);
            var cardSuitAndNumber = $"{suit}{cardNumberAndSuit.Substring(0, cardNumberAndSuit.Length - 1)}";
            return ConvertSuitAndNumberToEnum(cardSuitAndNumber);
        }
    }
}

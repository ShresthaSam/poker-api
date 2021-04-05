using AutoMapper;
using PokerApi.Common.Models;
using PokerApi.Repositories.Entities;
using System;

namespace PokerApi.Mappers
{
    public class CardToCardEnumConverter : ITypeConverter<Card, CardEnum>
    {
        public CardEnum Convert(Card source, CardEnum destination, ResolutionContext context)
        {
            var cardId = source.CardId;
            var cardSuitAndNumber = $"{source.Suit}{source.Number}";
            if (Enum.IsDefined(typeof(CardEnum), cardId))
            {
                var enumValue = (CardEnum)cardId;
                if (enumValue.ToString() != cardSuitAndNumber)
                {
                    throw new Exception($"CardEnum is matched by Card entity's Id but the expected enum string of '{cardSuitAndNumber}' is instead '{enumValue.ToString()}'");
                }
                return enumValue;
            } else
            {
                throw new Exception($"CardEnum not found for CardId={cardId}");
            }
        }
    }
}

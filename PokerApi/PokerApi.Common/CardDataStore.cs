using PokerApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Common
{
    public static class CardDataStore
    {
        public static List<CardItem> GetAllCardItems()
        {
            var cardItems = Enum.GetValues(typeof(CardEnum))
                .Cast<CardEnum>()
                .ToDictionary(k => k.ToString(), v => (int)v)
                .Select(kvp => new CardItem { Id = kvp.Value, Suit = kvp.Key.Substring(0, 1), Number = kvp.Key.Substring(1, kvp.Key.Length - 1) })
                .ToList();
            return cardItems;
        }
    }
}

using PokerApi.Common.Interfaces;
using PokerApi.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Common
{
    public class SimpleCardSorter : ICardSorter
    {
        public IEnumerable<CardItem> Sort(IEnumerable<CardItem> cardItems)
        {
            var sorted = new List<CardItem>();
            sorted.AddRange(
                cardItems
                    .Where(c => c.Number != "J" && c.Number != "Q" && c.Number != "K" & c.Number != "A" & c.Number != "10")
                    .OrderBy(c => c.Number)
            );
            sorted.AddRange(cardItems.Where(c => c.Number == "10"));
            sorted.AddRange(cardItems.Where(c => c.Number == "J"));
            sorted.AddRange(cardItems.Where(c => c.Number == "Q"));
            sorted.AddRange(cardItems.Where(c => c.Number == "K"));
            sorted.AddRange(cardItems.Where(c => c.Number == "A"));
            return sorted;
        } 
    }
}

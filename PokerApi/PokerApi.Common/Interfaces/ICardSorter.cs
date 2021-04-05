using PokerApi.Common.Models;
using System.Collections.Generic;

namespace PokerApi.Common.Interfaces
{
    public interface ICardSorter
    {
        public IEnumerable<CardItem> Sort(IEnumerable<CardItem> cardItems);
    }
}

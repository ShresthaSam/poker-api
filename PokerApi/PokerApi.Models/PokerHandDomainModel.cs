using PokerApi.Common.Models;

namespace PokerApi.Models
{
    /// <summary>
    /// This model is mapped from PokerDto. 
    /// CardEnum, being an enum, cannot start with a number which is what PokerDto
    /// has i.e. its Card values have "XY" format where "X" can be a number 2 thru 10.
    /// This mapped model switches out PokerDto's "XY" format with "YX" format.
    /// </summary>
    public class PokerHandDomainModel
    {
        public int PokerHandId { get; set; }
        public CardEnum Card1 { get; set; }
        public CardEnum Card2 { get; set; }
        public CardEnum Card3 { get; set; }
        public CardEnum Card4 { get; set; }
        public CardEnum Card5 { get; set; }
        public Player Player { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PokerApi.Models
{
    /// <summary>
    /// Describes a poker hand consisting of 5 cards and a user who is holding those cards.
    /// Format of a card is "XY" where "X" denotes card number i.e. 2 thru 10, J, Q, K, A
    /// and "Y" denotes card suit i.e. H, D, C, S.
    /// </summary>
    public class PokerHandDto
    {
        public int PokerHandId { get; set; }
        [Display(Name = "Card 1")]
        public string Card1 { get; set; }
        [Display(Name = "Card 2")]
        public string Card2 { get; set; }
        [Display(Name = "Card 3")]
        public string Card3 { get; set; }
        [Display(Name = "Card 4")]
        public string Card4 { get; set; }
        [Display(Name = "Card 5")]
        public string Card5 { get; set; }
        [Display(Name = "Player")]
        public Player Player { get; set; }
    }
}

namespace PokerApi.Models
{
    public class Ranking
    {
        public int Rank { get; set; }
        public Player Player { get; set; }
        public string Hand { get; set; }
        public string RankReason { get; set; }
    }
}

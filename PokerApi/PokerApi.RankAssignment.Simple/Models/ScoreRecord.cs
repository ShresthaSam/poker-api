using PokerApi.Models;

namespace PokerApi.RankAssignment.Simple.Models
{
    public class ScoreRecord
    {
        public CategoryEnum Category { get; set; }
        public Player Player { get; set; }
        public bool MatchedForCategory { get; set; }
        public int Rank { get; set; }
        public string RankReason { get; set; }
    }
}

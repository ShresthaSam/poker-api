namespace PokerApi.Repositories.Entities
{
    public class Hand
    {
        public int HandId { get; set; }
        public int CardId1 { get; set; }
        public int CardId2 { get; set; }
        public int CardId3 { get; set; }
        public int CardId4 { get; set; }
        public int CardId5 { get; set; }
        public string PlayerUserId { get; set; }
    }
}

namespace PokerApi.Models
{
    public class Constants
    {
        public const string CARD_INVALID_MESSAGE = "Card is invalid, it must be in a format 'XY' where 'X' can be 2 thru 10, J, Q, K or A and 'Y' must be a suit of D, H, C, or S";
        public const string CARD_MUST_BE_UNIQUE_ACROSS_PLAYERS_MESSAGE = "Each card must be unique across all players, no duplicates allowed";

        public const string PLAYER_USERID_MUST_BE_UNIQUE_MESSAGE = "Player's UserId must be unique, no dups allowed";

        public const string CARD_MUST_BE_UNIQUE_MESSAGE = "Each card must be unique, no duplicates allowed";
    }
}

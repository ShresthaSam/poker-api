using System;

namespace PokerApi.Repositories.Entities
{
    public class Player
    {
        public string UserName { get; }
        public string UserId { get; }
        public Player(string userName, string userId)
        {
            UserId = userId;
            UserName = userName;
        }
        public Player(string userName)
        {
            UserName = userName;
            UserId = Guid.NewGuid().ToString();
        }
    }
}

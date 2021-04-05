using System.ComponentModel.DataAnnotations;

namespace PokerApi.Models
{
    /// <summary>
    /// Describes a player in the card game
    /// </summary>
    public class Player
    {
        /// <summary>
        /// UserName is a given name or a nick name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Unique ID of a user in the card game (will be assigned by API if left empty or null)
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}

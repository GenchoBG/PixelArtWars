using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Theme { get; set; }

        public int PlayersCount { get; set; }

        public bool IsActive { get; set; }

        public ICollection<GameUser> Players { get; set; } = new List<GameUser>();
    }
}

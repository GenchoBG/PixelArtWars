using Microsoft.AspNetCore.Identity;
using PixelArtWars.Data.Models.Relations;
using System.Collections.Generic;

namespace PixelArtWars.Data.Models
{
    public class User : IdentityUser
    {
        public bool IsBanned { get; set; }

        public int TotalScore { get; set; }

        public int TotalKarma { get; set; }

        public ICollection<GameUser> Games { get; set; } = new List<GameUser>();
    }
}

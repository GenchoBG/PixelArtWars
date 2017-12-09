using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Data.Models
{
    public class User : IdentityUser
    {
        public bool IsBanned { get; set; }

        public int TotalScore { get; set; } = 0;

        public ICollection<GameUser> Games { get; set; } = new List<GameUser>();
    }
}

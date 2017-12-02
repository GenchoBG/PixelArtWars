using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Data.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public ICollection<GameUser> Games { get; set; } = new List<GameUser>();
    }
}

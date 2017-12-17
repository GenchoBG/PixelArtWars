using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Models.UserViewModels
{
    public class UserProfilePageViewModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public bool IsBanned { get; set; }

        public int TotalScore { get; set; }

        public string ProfilePicture { get; set; }
    }
}

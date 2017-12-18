using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Admin.Models.UserViewModels
{
    public class UserListViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int TotalKarma { get; set; }
    }
}

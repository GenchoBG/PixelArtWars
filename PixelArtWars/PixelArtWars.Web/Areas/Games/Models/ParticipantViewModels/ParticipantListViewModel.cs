using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Games.Models.ParticipantViewModels
{
    public class ParticipantListViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public int TotalScore { get; set; }
    }
}

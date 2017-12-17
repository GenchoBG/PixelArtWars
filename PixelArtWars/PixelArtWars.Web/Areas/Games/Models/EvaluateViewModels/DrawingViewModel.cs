using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Games.Models.EvaluateViewModels
{
    public class DrawingViewModel : IMapFrom<GameUser>
    {
        public string UserId { get; set; }

        public string ImageUrl { get; set; }
    }
}

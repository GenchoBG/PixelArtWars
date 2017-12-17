using System.Collections.Generic;
using PixelArtWars.Web.Areas.Games.Models.GameViewModels;

namespace PixelArtWars.Web.Areas.Games.Models
{
    public class GamesHomepageViewModel
    {
        public IEnumerable<GameListViewModel> Games { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public string Search { get; set; }

        public string CurrentUserId { get; set; }
    }
}

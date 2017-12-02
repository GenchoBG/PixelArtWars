using System.ComponentModel.DataAnnotations;

namespace PixelArtWars.Web.Areas.Games.Models.GameViewModels
{
    public class GameAddViewModel
    {
        [Required]
        public string Theme { get; set; }

        [Range(2, 8)]
        public int PlayersCount { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

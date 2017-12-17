using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.KeyVault.Models;

namespace PixelArtWars.Web.Areas.Games.Models.GameViewModels
{
    public class GameAddViewModel
    {
        [Required(ErrorMessage = "You cannot add a game without a theme!")]
        public string Theme { get; set; }

        [Range(2, 8)]
        public int PlayersCount { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

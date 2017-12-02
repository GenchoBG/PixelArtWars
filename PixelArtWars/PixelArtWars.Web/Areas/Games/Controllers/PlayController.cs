using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Games.Models.GameViewModels;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    public class PlayController : GamesBaseController
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public PlayController(IGameService gameService, IMapper mapper, UserManager<User> userManager)
        {
            this.gameService = gameService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var game = this.gameService.Get(id);

            var model = this.mapper.Map<GameDetailsViewModel>(game);

            return this.View(model);
        }

        public IActionResult Join(int id)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            this.gameService.JoinGame(currentUserId, id);

            return this.RedirectToAction("Details", new { id });
        }

        public IActionResult Leave(int id)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            this.gameService.LeaveGame(currentUserId, id);

            return this.RedirectToAction("Details", new { id });
        }
    }
}

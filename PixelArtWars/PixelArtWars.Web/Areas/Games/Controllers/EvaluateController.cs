using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Games.Models.EvaluateViewModels;
using PixelArtWars.Web.Areas.Games.Models.GameViewModels;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    public class EvaluateController : GamesBaseController
    {
        private readonly IGameService gameService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public EvaluateController(IGameService gameService, UserManager<User> userManager, IMapper mapper)
        {
            this.gameService = gameService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var games = this.gameService
                .GetAll()
                .Where(g => g.Status == GameStauts.PendingForEvaluation)
                .ProjectTo<GameListViewModel>()
                .ToList();

            return this.View(games);
        }

        public IActionResult Details(int id)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            var game = this.gameService.Get(id);

            if (game.Players.Any(p => p.UserId == currentUserId))
            {
                this.TempData["error"] = "You are not allowed to evaluate a game in which you are a participant!";
                return this.RedirectToAction("Index");
            }

            var model = this.mapper.Map<EvaluateGameViewModel>(game);

            return this.View(model);
        }

        public IActionResult Choose(string userId, int gameId)
        {
            this.gameService.SelectWinner(userId, gameId);

            return this.RedirectToAction("Index");
        }
    }
}

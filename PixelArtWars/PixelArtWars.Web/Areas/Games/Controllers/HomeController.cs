﻿using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Games.Models.GameViewModels;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    public class HomeController : GamesBaseController
    {
        private readonly IGameService gameService;
        private readonly UserManager<User> userManager;

        public HomeController(IGameService gameService, UserManager<User> userManager)
        {
            this.gameService = gameService;
            this.userManager = userManager;
        }

        public IActionResult Index(string search = "")
        {
            this.ViewData["userId"] = this.userManager.GetUserId(this.User);

            var games = this.gameService
                .GetAll(search)
                .ProjectTo<GameListViewModel>()
                .ToList();

            return this.View(games);
        }

        [HttpPost]
        public IActionResult Create(GameAddViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Error"] = string.Join(Environment.NewLine, this.ModelState.Values);
                return new RedirectResult("/games");
            }

            this.gameService.Create(model.Theme, model.PlayersCount, model.UserId);

            return new RedirectResult($"/games?search={model.Theme}");
        }
    }
}

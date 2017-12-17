﻿using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Games.Models.GameViewModels;
using PixelArtWars.Web.Areas.Games.Models.ParticipantViewModels;
using System;
using System.Linq;
using PixelArtWars.Web.Areas.Games.Models;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    public class HomeController : GamesBaseController
    {
        private readonly IGameService gameService;
        private readonly UserManager<User> userManager;

        private const int GamesPerPage = 5;

        public HomeController(IGameService gameService, UserManager<User> userManager)
        {
            this.gameService = gameService;
            this.userManager = userManager;
        }

        public IActionResult Index(string search = "", int page = 0)
        {
            if (page < 0)
            {
                page = 0;
            }

            var pagesCount =
                this.gameService.GetAll().Count(g => g.Status == GameStauts.Active) / GamesPerPage;

            if (page > pagesCount)
            {
                page = pagesCount;
            }

            var games = this.gameService
                .GetAll(search)
                .Where(g => g.Status == GameStauts.Active)
                .Skip(GamesPerPage * page)
                .Take(GamesPerPage)
                .ProjectTo<GameListViewModel>()
                .ToList();
           
            var model = new GamesHomepageViewModel()
            {
                Games = games,
                Page = page,
                TotalPages = pagesCount,
                Search = search,
                CurrentUserId = this.userManager.GetUserId(this.User)
        };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(GameAddViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[WebConstants.TempDataErrorMessageKey] = string.Join(Environment.NewLine, this.ModelState.Values);
                return this.RedirectToAction("Index");
            }

            this.gameService.Create(model.Theme, model.PlayersCount, model.UserId);

            return this.RedirectToAction("Index", new { search = model.Theme });
        }

        public IActionResult Leaderboards()
        {
            var topUsers = this.userManager
                .Users
                .OrderByDescending(u => u.TotalScore)
                .Take(10)
                .ProjectTo<ParticipantListViewModel>()
                .ToList();

            return this.View(topUsers);
        }
    }
}


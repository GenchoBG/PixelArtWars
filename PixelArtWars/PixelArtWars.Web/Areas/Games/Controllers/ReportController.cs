using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    public class ReportController : GamesBaseController
    {
        private readonly IReportService reportService;
        private readonly UserManager<User> userManager;

        public ReportController(IReportService reportService, UserManager<User> userManager)
        {
            this.reportService = reportService;
            this.userManager = userManager;
        }

        public IActionResult Index(int gameId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            this.reportService.CreateReport(gameId, currentUserId);

            this.TempData[WebConstants.TempDataSuccessMessageKey] = "Game has been reported!";

            return this.RedirectToAction("Index", "Evaluate", new { id = gameId });
        }
    }
}

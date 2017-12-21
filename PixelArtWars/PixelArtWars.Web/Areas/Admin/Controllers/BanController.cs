using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Admin.Models.UserViewModels;
using PixelArtWars.Web.Infrastructure.Extensions;

namespace PixelArtWars.Web.Areas.Admin.Controllers
{
    public class BanController : AdminBaseController
    {
        private readonly IBanService banService;

        private const string UserUnbannedSuccessMessage ="User successfully unbanned!";

        public BanController(IBanService banService)
        {
            this.banService = banService;
        }

        public IActionResult ViewBannedUsers()
        {
            var users = this.banService.GetBannedUsers().ProjectTo<UserListViewModel>();

            return this.View(users);
        }

        public IActionResult BanFromReport(string banId, string reporterId, int reportId)
        {
            this.banService.Ban(banId, reporterId);

            return this.RedirectToAction("Details", "Report", new { id = reportId });
        }

        public IActionResult Unban(string id)
        {
            this.banService.Unban(id);

            this.TempData.AddSuccessMessage(UserUnbannedSuccessMessage);
            return this.RedirectToAction("ViewBannedUsers");
        }
    }
}

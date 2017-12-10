using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Web.Areas.Admin.Controllers
{
    public class BanController : AdminBaseController
    {
        private readonly IBanService banService;

        public BanController(IBanService banService)
        {
            this.banService = banService;
        }

        public IActionResult BanFromReport(string banId, string reporterId, int reportId)
        {
            this.banService.Ban(banId, reporterId);

            return this.RedirectToAction("Details", "Report", new { id = reportId });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    [Area("Games")]
    [Authorize]
    public class GamesBaseController : Controller
    {
    }
}
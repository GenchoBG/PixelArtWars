using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PixelArtWars.Web.Areas.Games.Controllers
{
    [Area(WebConstants.GameArea)]
    [Authorize]
    public class GamesBaseController : Controller
    {
    }
}
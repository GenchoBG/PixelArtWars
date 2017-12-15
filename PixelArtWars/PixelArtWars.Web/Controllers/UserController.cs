using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Models.UserViewModels;
using System.Threading.Tasks;

namespace PixelArtWars.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> ViewProfile(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return this.BadRequest();
            }

            var model = this.mapper.Map<UserProfilePageViewModel>(user);

            return this.View(model);
        }

        public IActionResult MyProfile() => this.RedirectToAction("ViewProfile", new { id = this.userManager.GetUserId(this.User) });
    }
}

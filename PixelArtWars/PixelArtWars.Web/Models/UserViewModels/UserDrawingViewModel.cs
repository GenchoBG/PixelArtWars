using AutoMapper;
using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Web.Infrastrucuture.Mapping;

namespace PixelArtWars.Web.Models.UserViewModels
{
    public class UserDrawingViewModel : IHaveCustomMapping
    {
        public string Theme { get; set; }

        public string ImageUrl { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile.CreateMap<GameUser, UserDrawingViewModel>()
                .ForMember(m => m.Theme, opts => opts.MapFrom(gu => gu.Game.Theme));
    }
}

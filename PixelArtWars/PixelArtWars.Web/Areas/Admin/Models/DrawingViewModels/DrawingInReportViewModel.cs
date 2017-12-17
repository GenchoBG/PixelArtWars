using AutoMapper;
using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Admin.Models.DrawingViewModels
{
    public class DrawingInReportViewModel : IHaveCustomMapping
    {
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public bool IsBanned { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile
                .CreateMap<GameUser, DrawingInReportViewModel>()
                .ForMember(m => m.IsBanned, opts => opts.MapFrom(gu => gu.User.IsBanned));
    }
}

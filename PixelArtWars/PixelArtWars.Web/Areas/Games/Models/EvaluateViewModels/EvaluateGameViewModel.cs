using System.Collections.Generic;
using AutoMapper;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Games.Models.EvaluateViewModels
{
    public class EvaluateGameViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public IEnumerable<DrawingViewModel> Drawings { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile.CreateMap<Game, EvaluateGameViewModel>()
            .ForMember(m => m.Drawings, opts => opts.MapFrom(g => g.Players));
    }
}

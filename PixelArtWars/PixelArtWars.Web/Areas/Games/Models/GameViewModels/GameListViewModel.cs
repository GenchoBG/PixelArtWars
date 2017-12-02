using AutoMapper;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastrucuture.Mapping;

namespace PixelArtWars.Web.Areas.Games.Models.GameViewModels
{
    public class GameListViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public int PlayersCount { get; set; }

        public int SignedUpCount { get; set; }

        public void ConfigureMapping(Profile profile) =>
            profile.CreateMap<Game, GameListViewModel>()
                .ForMember(m => m.SignedUpCount, opts => opts.MapFrom(g => g.Players.Count));
    }
}

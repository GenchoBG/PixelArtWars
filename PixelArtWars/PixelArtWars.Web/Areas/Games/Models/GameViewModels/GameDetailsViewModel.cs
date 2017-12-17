using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Areas.Games.Models.ParticipantViewModels;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Games.Models.GameViewModels
{
    public class GameDetailsViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public int PlayersCount { get; set; }

        public ICollection<ParticipantListInGameDetailsViewModel> Participants { get; set; }

        public void ConfigureMapping(Profile profile) =>
            profile.CreateMap<Game, GameDetailsViewModel>()
                .ForMember(m => m.Participants, opts => opts.MapFrom(g => g.Players.Select(p => new ParticipantListInGameDetailsViewModel()
                {
                    Id = p.UserId,
                    UserName = p.User.UserName,
                    HasDrawn = p.HasDrawn
                })));
    }
}

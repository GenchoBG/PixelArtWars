using System;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Infrastrucuture.Mapping;

namespace PixelArtWars.Web.Areas.Admin.Models.ReportViewModels
{
    public class ReportListViewModel : IMapFrom<Report>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public DateTime Date { get; set; }
    }
}

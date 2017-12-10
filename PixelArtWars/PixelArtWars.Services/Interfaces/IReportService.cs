using System.Linq;
using PixelArtWars.Data.Models;

namespace PixelArtWars.Services.Interfaces
{
    public interface IReportService
    {
        void CreateReport(int gameId, string reporterId);
        IQueryable<Report> All();
        Report Get(int id);
        void Close(int id);
    }
}

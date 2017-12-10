using System;
using PixelArtWars.Data.Models.Enums;

namespace PixelArtWars.Data.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public string ReporterId { get; set; }

        public User Reporter { get; set; }

        public DateTime Date { get; set; }

        public ReportStatus Status { get; set; }
    }
}

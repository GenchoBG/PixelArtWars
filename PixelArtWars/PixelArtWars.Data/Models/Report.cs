namespace PixelArtWars.Data.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string ImageUrl { get; set; }
    }
}

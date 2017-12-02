namespace PixelArtWars.Data.Models.Relations
{
    public class GameUser
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}

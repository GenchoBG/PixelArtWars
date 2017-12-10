namespace PixelArtWars.Services.Interfaces
{
    public interface IBanService
    {
        void Ban(string banId, string reporterId);
        void Unban(string id);
    }
}

using AutoMapper;

namespace PixelArtWars.Web.Infrastrucuture.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}

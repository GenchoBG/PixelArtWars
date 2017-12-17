using AutoMapper;

namespace PixelArtWars.Web.Infrastructure.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}

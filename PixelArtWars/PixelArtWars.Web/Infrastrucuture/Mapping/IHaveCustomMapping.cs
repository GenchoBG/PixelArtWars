using AutoMapper;

namespace LearningSystem.Infrastrucuture.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}

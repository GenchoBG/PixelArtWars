using System;
using System.Linq;
using AutoMapper;

namespace LearningSystem.Infrastrucuture.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("PixelArtWars"))
                .SelectMany(a => a.GetTypes())
                .ToList();

            var mappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            t
                                .GetInterfaces()
                                .Any(i => i.IsGenericType &&
                                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    .GetGenericArguments()
                    .First()
                })
                .ToList();


            foreach (var mapping in mappings)
            {
                this.CreateMap(mapping.Source, mapping.Destination);
            }

            var customMappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList();

            foreach (var customMapping in customMappings)
            {
                customMapping.ConfigureMapping(this);
            }
        }
    }
}

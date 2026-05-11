using System.Runtime.CompilerServices;
using Mapster;

namespace PurchaseManagement.Mappers
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMaps(this  IServiceCollection services)
        {
            services.AddMapster();
            
            return services;
        }
    }
}

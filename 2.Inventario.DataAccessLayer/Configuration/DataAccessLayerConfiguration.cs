using Microsoft.Extensions.DependencyInjection;
using _2.Inventario.DataAccessLayer.Repositories;
using _2.Inventario.DataAccessLayer.Repositories.Interfaces;

namespace _2.Inventario.DataAccessLayer.Configuration
{
    public static class DataAccessLayerConfiguration
    {
        public static IServiceCollection ConfigureDataAccessLayer(this IServiceCollection services)
        {
            // Registrar repositorios
            services.AddScoped<IMovInventarioRepository, MovInventarioRepository>();

            return services;
        }
    }
}
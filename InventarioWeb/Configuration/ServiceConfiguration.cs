using Microsoft.EntityFrameworkCore;
using _2.Inventario.DataAccessLayer.Data;
using _2.Inventario.DataAccessLayer.Repositories.Interfaces;
using _2.Inventario.DataAccessLayer.Repositories;
using Inventario.BusinessLogicLayer.Services.Interfaces;
using Inventario.BusinessLogicLayer.Services;
using Inventario.BusinessLogicLayer.Mappers;
using Inventario.BusinessLogicLayer.Validators;
using Inventario.BusinessLogicLayer.DTOs;
using FluentValidation;

namespace InventarioWeb.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureInventarioServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar Entity Framework
            services.AddDbContext<InventarioDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registrar repositorios
            services.AddScoped<IMovInventarioRepository, MovInventarioRepository>();

            // Registrar servicios de negocio
            services.AddScoped<IMovInventarioService, MovInventarioService>();

            // Registrar mappers
            services.AddScoped<IMovInventarioMapper, MovInventarioMapper>();

            // Registrar validadores
            services.AddScoped<IValidator<MovInventarioBusinessDto>, MovInventarioBusinessValidator>();
            services.AddScoped<IValidator<MovInventarioSearchBusinessDto>, MovInventarioSearchValidator>();

            return services;
        }
    }
}
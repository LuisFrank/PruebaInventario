using FluentValidation;
using Inventario.BusinessLogicLayer.DTOs;
using Inventario.BusinessLogicLayer.Mappers;
using Inventario.BusinessLogicLayer.Services;
using Inventario.BusinessLogicLayer.Services.Interfaces;
using Inventario.BusinessLogicLayer.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventario.BusinessLogicLayer.Configuration
{
    /// <summary>
    /// Configuración de dependencias para la capa de lógica de negocio
    /// </summary>
    public static class BusinessLogicLayerConfiguration
    {
        /// <summary>
        /// Registra todos los servicios de la capa de lógica de negocio
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <returns>Colección de servicios configurada</returns>
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Registrar servicios de negocio
            services.AddScoped<IMovInventarioService, MovInventarioService>();

            // Registrar mappers
            services.AddScoped<IMovInventarioMapper, MovInventarioMapper>();

            // Registrar validadores de FluentValidation
            services.AddScoped<IValidator<MovInventarioBusinessDto>, MovInventarioBusinessValidator>();
            services.AddScoped<IValidator<MovInventarioSearchBusinessDto>, MovInventarioSearchValidator>();

            return services;
        }

        /// <summary>
        /// Registra solo los servicios principales (sin validadores automáticos)
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <returns>Colección de servicios configurada</returns>
        public static IServiceCollection AddBusinessLogicLayerCore(this IServiceCollection services)
        {
            // Registrar servicios de negocio
            services.AddScoped<IMovInventarioService, MovInventarioService>();

            // Registrar mappers
            services.AddScoped<IMovInventarioMapper, MovInventarioMapper>();

            return services;
        }

        /// <summary>
        /// Registra solo los validadores
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <returns>Colección de servicios configurada</returns>
        public static IServiceCollection AddBusinessLogicLayerValidators(this IServiceCollection services)
        {
            // Registrar validadores específicos
            services.AddScoped<IValidator<MovInventarioBusinessDto>, MovInventarioBusinessValidator>();
            services.AddScoped<IValidator<MovInventarioSearchBusinessDto>, MovInventarioSearchValidator>();

            return services;
        }
    }
}
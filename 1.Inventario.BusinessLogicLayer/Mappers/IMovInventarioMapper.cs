using _2.Inventario.DataAccessLayer.Models.DTOs;
using Inventario.BusinessLogicLayer.DTOs;
using System.Collections.Generic;

namespace Inventario.BusinessLogicLayer.Mappers
{
    /// <summary>
    /// Interfaz para el mapeo entre DTOs de datos y DTOs de negocio
    /// </summary>
    public interface IMovInventarioMapper
    {
        // Mapeo de Business DTO a Data DTO
        MovInventarioDto ToDataDto(MovInventarioBusinessDto businessDto);
        MovInventarioFilterDto ToDataFilterDto(MovInventarioSearchBusinessDto searchDto);

        // Mapeo de Data DTO a Business DTO
        MovInventarioBusinessDto ToBusinessDto(MovInventarioDto dataDto);
        MovInventarioResultBusinessDto ToResultBusinessDto(MovInventarioDto dataDto);

        // Mapeo de colecciones
        List<MovInventarioResultBusinessDto> ToResultBusinessDtoList(List<MovInventarioDto> dataDtoList);
        List<MovInventarioDto> ToDataDtoList(List<MovInventarioBusinessDto> businessDtoList);

        // Mapeo para resultados paginados
        MovInventarioPagedResultBusinessDto ToPagedResultBusinessDto(
            List<MovInventarioDto> dataDtoList, 
            int totalRegistros, 
            int paginaActual, 
            int tamanoPagina);

        // Mapeo para actualización (preserva campos no modificables)
        MovInventarioDto ToDataDtoForUpdate(MovInventarioBusinessDto businessDto, MovInventarioDto existingDataDto);

        // Mapeo específico para inserción
        MovInventarioDto ToDataDtoForInsert(MovInventarioBusinessDto businessDto);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using _2.Inventario.DataAccessLayer.Data;
using _2.Inventario.DataAccessLayer.Models;
using _2.Inventario.DataAccessLayer.Models.DTOs;
using _2.Inventario.DataAccessLayer.Repositories.Interfaces;
using System.Data;

namespace _2.Inventario.DataAccessLayer.Repositories
{
    public class MovInventarioRepository : IMovInventarioRepository
    {
        private readonly InventarioDbContext _context;

        public MovInventarioRepository(InventarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovInventarioDto>> ConsultarMovInventarioAsync(MovInventarioFilterDto filtros)
        {
            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FechaInicio", filtros.FechaInicio ?? (object)DBNull.Value),
                new SqlParameter("@FechaFin", filtros.FechaFin ?? (object)DBNull.Value),
                new SqlParameter("@TipoMovimiento", filtros.TipoMovimiento ?? (object)DBNull.Value),
                new SqlParameter("@NroDocumento", filtros.NroDocumento ?? (object)DBNull.Value),
                new SqlParameter("@CodCia", filtros.CodCia ?? (object)DBNull.Value),
                new SqlParameter("@CompaniaVenta3", filtros.CompaniaVenta3 ?? (object)DBNull.Value),
                new SqlParameter("@AlmacenVenta", filtros.AlmacenVenta ?? (object)DBNull.Value),
                new SqlParameter("@TipoDocumento", filtros.TipoDocumento ?? (object)DBNull.Value),
                new SqlParameter("@CodItem2", filtros.CodItem2 ?? (object)DBNull.Value),
                new SqlParameter("@Proveedor", filtros.Proveedor ?? (object)DBNull.Value),
                new SqlParameter("@AlmacenDestino", filtros.AlmacenDestino ?? (object)DBNull.Value),
                new SqlParameter("@DocRef1", filtros.DocRef1 ?? (object)DBNull.Value),
                new SqlParameter("@DocRef2", filtros.DocRef2 ?? (object)DBNull.Value),
                new SqlParameter("@DocRef3", filtros.DocRef3 ?? (object)DBNull.Value),
                new SqlParameter("@DocRef4", filtros.DocRef4 ?? (object)DBNull.Value),
                new SqlParameter("@DocRef5", filtros.DocRef5 ?? (object)DBNull.Value)
            };

                var sql = "EXEC SP_ConsultarMovInventario @FechaInicio, @FechaFin, @TipoMovimiento, @NroDocumento, " +
                         "@CodCia, @CompaniaVenta3, @AlmacenVenta, @TipoDocumento, @CodItem2, @Proveedor, " +
                         "@AlmacenDestino, @DocRef1, @DocRef2, @DocRef3, @DocRef4, @DocRef5";

                var movimientos = await _context.Database
                    .SqlQueryRaw<MovInventarioDto>(sql, parameters.ToArray())
                    .ToListAsync();

                return movimientos;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        public async Task<bool> InsertarMovInventarioAsync(MovInventarioDto movInventario)
        {
            try
            {
                // Log de los valores que se van a insertar
                Console.WriteLine($"=== INSERTAR MOV INVENTARIO ===");
                Console.WriteLine($"CodCia: '{movInventario.CodCia}'");
                Console.WriteLine($"CompaniaVenta3: '{movInventario.CompaniaVenta3}'");
                Console.WriteLine($"AlmacenVenta: '{movInventario.AlmacenVenta}'");
                Console.WriteLine($"TipoMovimiento: '{movInventario.TipoMovimiento}'");
                Console.WriteLine($"TipoDocumento: '{movInventario.TipoDocumento}'");
                Console.WriteLine($"NroDocumento: '{movInventario.NroDocumento}'");
                Console.WriteLine($"CodItem2: '{movInventario.CodItem2}'");
                Console.WriteLine($"Proveedor: '{movInventario.Proveedor}'");
                Console.WriteLine($"AlmacenDestino: '{movInventario.AlmacenDestino}'");
                Console.WriteLine($"Cantidad: '{movInventario.Cantidad}'");
                Console.WriteLine($"DocRef1: '{movInventario.DocRef1}'");
                Console.WriteLine($"FechaTransaccion: '{movInventario.FechaTransaccion}'");
                Console.WriteLine($"=================================");

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CodCia", movInventario.CodCia),
                    new SqlParameter("@CompaniaVenta3", movInventario.CompaniaVenta3),
                    new SqlParameter("@AlmacenVenta", movInventario.AlmacenVenta),
                    new SqlParameter("@TipoMovimiento", movInventario.TipoMovimiento),
                    new SqlParameter("@TipoDocumento", movInventario.TipoDocumento),
                    new SqlParameter("@NroDocumento", movInventario.NroDocumento),
                    new SqlParameter("@CodItem2", movInventario.CodItem2),
                    new SqlParameter("@Proveedor", movInventario.Proveedor ?? (object)DBNull.Value),
                    new SqlParameter("@AlmacenDestino", movInventario.AlmacenDestino ?? (object)DBNull.Value),
                    new SqlParameter("@Cantidad", movInventario.Cantidad ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef1", movInventario.DocRef1 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef2", movInventario.DocRef2 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef3", movInventario.DocRef3 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef4", movInventario.DocRef4 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef5", movInventario.DocRef5 ?? (object)DBNull.Value),
                    new SqlParameter("@FechaTransaccion", movInventario.FechaTransaccion ?? (object)DBNull.Value)
                };

                var sql = "EXEC SP_InsertarMovInventario @CodCia, @CompaniaVenta3, @AlmacenVenta, @TipoMovimiento, " +
                         "@TipoDocumento, @NroDocumento, @CodItem2, @Proveedor, @AlmacenDestino, @Cantidad, " +
                         "@DocRef1, @DocRef2, @DocRef3, @DocRef4, @DocRef5, @FechaTransaccion";

                var result = await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());
                Console.WriteLine($"Resultado de ExecuteSqlRawAsync: {result}");
                return true; // Si llegamos aquí sin excepción, la operación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error específico
                Console.WriteLine($"Error en InsertarMovInventarioAsync: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; // Re-lanzar la excepción para que sea manejada por el servicio
            }
        }

        public async Task<bool> ActualizarMovInventarioAsync(MovInventarioDto movInventario)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CodCia", movInventario.CodCia),
                    new SqlParameter("@CompaniaVenta3", movInventario.CompaniaVenta3),
                    new SqlParameter("@AlmacenVenta", movInventario.AlmacenVenta),
                    new SqlParameter("@TipoMovimiento", movInventario.TipoMovimiento),
                    new SqlParameter("@TipoDocumento", movInventario.TipoDocumento),
                    new SqlParameter("@NroDocumento", movInventario.NroDocumento),
                    new SqlParameter("@CodItem2", movInventario.CodItem2),
                    new SqlParameter("@Proveedor", movInventario.Proveedor ?? (object)DBNull.Value),
                    new SqlParameter("@AlmacenDestino", movInventario.AlmacenDestino ?? (object)DBNull.Value),
                    new SqlParameter("@Cantidad", movInventario.Cantidad ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef1", movInventario.DocRef1 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef2", movInventario.DocRef2 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef3", movInventario.DocRef3 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef4", movInventario.DocRef4 ?? (object)DBNull.Value),
                    new SqlParameter("@DocRef5", movInventario.DocRef5 ?? (object)DBNull.Value),
                    new SqlParameter("@FechaTransaccion", movInventario.FechaTransaccion ?? (object)DBNull.Value)
                };

                var sql = "EXEC SP_ActualizarMovInventario @CodCia, @CompaniaVenta3, @AlmacenVenta, @TipoMovimiento, " +
                         "@TipoDocumento, @NroDocumento, @CodItem2, @Proveedor, @AlmacenDestino, @Cantidad, " +
                         "@DocRef1, @DocRef2, @DocRef3, @DocRef4, @DocRef5, @FechaTransaccion";

                await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());
                // Si llegamos aquí sin excepción, la actualización fue exitosa
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarMovInventarioAsync(string codCia, string companiaVenta3, string almacenVenta,
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CodCia", codCia),
                    new SqlParameter("@CompaniaVenta3", companiaVenta3),
                    new SqlParameter("@AlmacenVenta", almacenVenta),
                    new SqlParameter("@TipoMovimiento", tipoMovimiento),
                    new SqlParameter("@TipoDocumento", tipoDocumento),
                    new SqlParameter("@NroDocumento", nroDocumento),
                    new SqlParameter("@CodItem2", codItem2)
                };

                var sql = "EXEC SP_EliminarMovInventario @CodCia, @CompaniaVenta3, @AlmacenVenta, @TipoMovimiento, " +
                         "@TipoDocumento, @NroDocumento, @CodItem2";

                await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());
                // Si llegamos aquí sin excepción, la eliminación fue exitosa
                // El SP lanza una excepción si el registro no existe
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<MovInventarioDto?> ObtenerMovInventarioPorIdAsync(string codCia, string companiaVenta3, string almacenVenta,
            string tipoMovimiento, string tipoDocumento, string nroDocumento, string codItem2)
        {
            try
            {
                var movimiento = await _context.MovInventarios
                    .Where(m => m.CodCia == codCia &&
                               m.CompaniaVenta3 == companiaVenta3 &&
                               m.AlmacenVenta == almacenVenta &&
                               m.TipoMovimiento == tipoMovimiento &&
                               m.TipoDocumento == tipoDocumento &&
                               m.NroDocumento == nroDocumento &&
                               m.CodItem2 == codItem2)
                    .Select(m => new MovInventarioDto
                    {
                        CodCia = m.CodCia,
                        CompaniaVenta3 = m.CompaniaVenta3,
                        AlmacenVenta = m.AlmacenVenta,
                        TipoMovimiento = m.TipoMovimiento,
                        TipoDocumento = m.TipoDocumento,
                        NroDocumento = m.NroDocumento,
                        CodItem2 = m.CodItem2,
                        Proveedor = m.Proveedor,
                        AlmacenDestino = m.AlmacenDestino,
                        Cantidad = m.Cantidad,
                        DocRef1 = m.DocRef1,
                        DocRef2 = m.DocRef2,
                        DocRef3 = m.DocRef3,
                        DocRef4 = m.DocRef4,
                        DocRef5 = m.DocRef5,
                        FechaTransaccion = m.FechaTransaccion
                    })
                    .FirstOrDefaultAsync();

                return movimiento;
            }
            catch
            {
                return null;
            }
        }
    }
}
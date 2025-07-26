using System.Data;
using APITienda.Models;
using Dapper;
using FacturasTienda.Context;
using APITienda.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace APITienda.Repository;

public class TiendaRepository : ITiendaRepository
{
    private readonly string _connectionString;
    private readonly ILogger<TiendaRepository> _logger;

    public TiendaRepository(IOptions<DatabaseSettings> opt, ILogger<TiendaRepository> logger)
    {
        _logger = logger;
        _connectionString = opt.Value.Connection ?? throw new ArgumentNullException("Connection string is null");
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<ProductoDto>> ObtenerProductos()
    {
        try
        {
            using var connection = CreateConnection();
            const string query = "EXEC ProductosDetallados";

            var productos = await connection.QueryAsync<ProductoDto>(query);
            return productos.ToList();
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al obtener productos en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    public async Task<bool> ExisteProducto(string referencia)
    {
        try
        {
            using var connection = CreateConnection();
            const string query = "SELECT COUNT(*) FROM [dbo].[Productos] WHERE Referencia = @Referencia;";

            var count = await connection.QuerySingleAsync<int>(query, new { Referencia = referencia });
            return count == 1;
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al verificar si existe el producto en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    public async Task<IEnumerable<Categoria>> ObtenerCategorias()
    {
        try
        {
            using var connection = CreateConnection();
            const string query = "SELECT * FROM Categorias";

            var categorias = await connection.QueryAsync<Categoria>(query);
            return categorias.ToList();
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al obtener las categorías en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    public async Task<IEnumerable<Marca>> ObtenerMarcas()
    {
        try
        {
            using var connection = CreateConnection();
            const string query = "SELECT * FROM Marcas";

            var marcas = await connection.QueryAsync<Marca>(query);
            return marcas.ToList();
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al obtener marcas en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    public async Task<bool> GuardarProducto(NuevoProductoDto producto)
    {
        if (await ExisteProducto(producto.Referencia))
            throw new InvalidOperationException("La referencia ya existe, por favor cámbiela");

        const string query = @"INSERT INTO Productos (
            Nombre, IdCategoria, IdMarca, Descripcion, Imagen, Cantidad, Precio, FechaCreacion, Referencia)
            VALUES (@Nombre, @IdCategoria, @IdMarca, @Descripcion, @Imagen, @Cantidad, @Precio, @FechaCreacion, @Referencia)";
        try
        {
            using var connection = CreateConnection();

            var result = await connection.ExecuteAsync(query, new
            {
                Nombre = producto.Nombre,
                IdCategoria = producto.IdCategoria,
                IdMarca = producto.IdMarca,
                Descripcion = producto.Descripcion,
                Imagen = producto.Imagen,
                Cantidad = producto.Cantidad,
                Precio = producto.Precio,
                FechaCreacion = DateTime.Now,
                Referencia = producto.Referencia
            });

            return result > 0;
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al guardar producto en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }

    }



    public async Task<bool> ActualizarProducto(NuevoProductoDto producto)
    {
        if (!await ExisteProducto(producto.Referencia))
            throw new KeyNotFoundException("El producto no existe para realizar su modificiación");

        const string query = @"UPDATE Productos
                    SET 
                        Nombre = @Nombre, 
                        IdCategoria = @IdCategoria, 
                        IdMarca = @IdMarca, 
                        Descripcion = @Descripcion, 
                        Imagen = @Imagen, 
                        Cantidad = @Cantidad, 
                        Precio = @Precio, 
                        FechaCreacion = @FechaCreacion,
                        Referencia = @Referencia
                    WHERE Referencia = @Referencia";
        try
        {
            using var connection = CreateConnection();

            var parametros = new Producto
            {
                Nombre = producto.Nombre,
                IdCategoria = producto.IdCategoria,
                IdMarca = producto.IdMarca,
                Descripcion = producto.Descripcion,
                Imagen = producto.Imagen,
                Cantidad = producto.Cantidad,
                Precio = producto.Precio,
                Referencia = producto.Referencia
            };

            var filasEditadas = await connection.ExecuteAsync(query, parametros);

            return filasEditadas > 0;
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al actualizar producto en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    public async Task<bool> EliminarProducto(string referencia)
    {
        const string query = "DELETE FROM Productos WHERE Referencia = @Referencia";
        try
        {
            using var connection = CreateConnection();
            var filasEliminadas = await connection.ExecuteAsync(query, new { Referencia = referencia });
            return true;
        }
        catch (SqlException ex)
        {
            LogSqlException(ex);
            throw new Exception("Error al eliminar producto en la base de datos", ex);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
    }

    private void LogSqlException(SqlException ex)
    {
        _logger.LogError(ex, "SQL Error: {Message}, Código: {Code}", ex.Message, ex.Number);
    }
    
    private void LogException(Exception ex)
    {
        _logger.LogError(ex, "Error: {Message}", ex.Message);
    }
}
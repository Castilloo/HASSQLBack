using APITienda.Models;
using APITienda.Dto;

namespace APITienda.Repository;

public interface ITiendaRepository
{
    Task<IEnumerable<ProductoDto>> ObtenerProductos();
    Task<ProductoDto> ObtenerProductoPorRef(string referencia);
    Task<bool> ExisteProducto(string referencia);
    Task<bool> GuardarProducto(NuevoProductoDto producto);
    Task<bool> ActualizarProducto(NuevoProductoDto producto);
    Task<bool> EliminarProducto(string referencia);

    Task<IEnumerable<Marca>> ObtenerMarcas();
    Task<IEnumerable<Categoria>> ObtenerCategorias();
}
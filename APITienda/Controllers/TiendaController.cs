// using APITienda.Dto;
using APITienda.Models;
using APITienda.Repository;
using APITienda.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using APITienda.Responses;

namespace APITienda.Controllers;

[ApiController]
[Route("api/v1/")]
public class TiendaController : ControllerBase
{
    private readonly ILogger<TiendaController> _logger;
    private readonly ITiendaRepository _repository;

    public TiendaController(ILogger<TiendaController> logger, ITiendaRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("productos")]
    [ProducesResponseType(typeof(IEnumerable<ProductoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObtenerProductos()
    {
        try
        {
            _logger.LogInformation("Obteniendo productos desde el controlador");
            var productos = await _repository.ObtenerProductos();

            return Ok(new ApiResponse<IEnumerable<ProductoDto>>(productos)
            {
                Mensaje = "Productos obtenidos correctamente."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }

    }

    [HttpGet("producto/{referencia}")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObtenerProductos(string referencia)
    {
        try
        {
            _logger.LogInformation("Obteniendo producto desde el controlador");
            var producto = await _repository.ObtenerProductoPorRef(referencia.Trim());

            return Ok(new ApiResponse<ProductoDto>(producto)
            {
                Mensaje = "Producto obtenido correctamente."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }

    }

    [HttpGet("marcas")]
    [ProducesResponseType(typeof(IEnumerable<Marca>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObtenerMarcas()
    {
        try
        {
            _logger.LogInformation("Obteniendo marcas desde el controlador");
            var marcas = await _repository.ObtenerMarcas();

            return Ok(new ApiResponse<IEnumerable<Marca>>(marcas)
            {
                Mensaje = "Productos obtenidos correctamente."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener marcas");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }

    }

    [HttpGet("categorias")]
    [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObtenerCategorias()
    {
        try
        {
            _logger.LogInformation("Obteniendo categorías desde el controlador");
            var categorias = await _repository.ObtenerCategorias();

            return Ok(new ApiResponse<IEnumerable<Categoria>>(categorias)
            {
                Mensaje = "Categorías obtenidas correctamente."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener categorías");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }

    }

    [HttpGet("existe-producto/{referencia}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExisteProducto(string referencia)
    {
        try
        {
            var result = await _repository.ExisteProducto(referencia);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error verificando producto: {referencia}");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }

    }

    [HttpPut("editar-producto/{referencia}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarProducto([FromForm] NuevoProductoConImagenDto producto, [FromRoute] string referencia)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productoAnterior = await _repository.ObtenerProductoPorRef(referencia);

            if (producto.Imagen != null && producto.Imagen.Name != productoAnterior.Imagen)
            {
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

                var rutaArchivo = Path.Combine(rutaCarpeta, producto.Imagen.FileName.Replace(' ', '-'));
                using var stream = new FileStream(rutaArchivo, FileMode.Create);
                await producto.Imagen.CopyToAsync(stream);
            }

            var nuevoProducto = new NuevoProductoDto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Imagen = producto is null
                        ? "/uploads/" + producto!.Imagen!.FileName 
                        : productoAnterior.Imagen,
                Cantidad = producto.Cantidad,
                Descripcion = producto.Descripcion,
                IdCategoria = producto.IdCategoria,
                IdMarca = producto.IdMarca,
                Referencia = producto.Referencia
            };

            var actualizado = await _repository.ActualizarProducto(nuevoProducto);
            return Ok(new { mensaje = "Actualización exitosa", data = actualizado });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                mensaje = "Error en los datos de entrada",
                detalle = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                mensaje = "Ocurrió un error inesperado.",
                detalle = ex.Message
            });
        }
    }


    [HttpPost("guardar-producto")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GuardarProducto([FromForm] NuevoProductoConImagenDto producto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (producto.Imagen != null && producto.Imagen.Length > 0)
            {
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

                var rutaArchivo = Path.Combine(rutaCarpeta, producto.Imagen.FileName);
                using var stream = new FileStream(rutaArchivo, FileMode.Create);
                await producto.Imagen.CopyToAsync(stream);
            }

            var nuevoProducto = new NuevoProductoDto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Imagen = "/uploads/" + producto.Imagen!.FileName,
                Cantidad = producto.Cantidad,
                Descripcion = producto.Descripcion,
                IdCategoria = producto.IdCategoria,
                IdMarca = producto.IdMarca,
                Referencia = producto.Referencia
            };

            var guardado = await _repository.GuardarProducto(nuevoProducto);
            if (guardado)
                return Created("Success", new
                {
                    Mensaje = "La factura fue registrada exitosamente.",
                    Fecha = DateTime.Now
                });
            else
                return StatusCode(500, new
                {
                    Mensaje = "No se pudo guardar la factura, verificar los datos."
                });


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos");
            return StatusCode(500, new ApiResponse<string>("Error interno del servidor", false));
        }
    }

    [HttpDelete("borrar-producto/{referencia}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EliminarProducto(string referencia)
    {
        try
        {
            var result = await _repository.EliminarProducto(referencia);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}
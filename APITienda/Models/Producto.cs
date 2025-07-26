using System.ComponentModel.DataAnnotations;

namespace APITienda.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string Referencia { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty!;
    public int IdCategoria { get; set; }
    public int IdMarca { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}

namespace APITienda.Dto;

public class ProductoDto
{
    public int? IdProducto { get; set; }
    public string Referencia { get; set; } = null!;
    public string Nombre { get; set; } = string.Empty!;
    public string Categoria { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

}
using System.ComponentModel.DataAnnotations;

namespace APITienda.Dto;

public class NuevoProductoDto
{
    [Required(ErrorMessage = "La referencia es obligatoria.")]
    public string Referencia { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty!;

    [Required(ErrorMessage = "Debe seleccionar una categoría.")]
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una marca.")]
    public int IdMarca { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La imagen es obligatoria.")]
    public string Imagen { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

}
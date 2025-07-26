using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APITienda.Models;

public class Marca 
{
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = string.Empty;

}

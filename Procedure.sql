CREATE PROCEDURE ProductosDetallados
	@Referencia VARCHAR(100) = NULL
AS
BEGIN
	SELECT 
		p.IdProducto,
		p.Nombre,
		p.Referencia,
		m.Nombre AS Marca,
		c.Nombre AS Categoria,
		p.Imagen,
		p.Descripcion,
		p.Cantidad,
		p.Precio,
		p.FechaCreacion
	FROM Productos p
	JOIN Categorias c ON c.IdCategoria = p.IdCategoria
	JOIN Marcas m ON m.IdMarca = p.IdMarca
	WHERE (@Referencia IS NULL OR Referencia = @Referencia);
END;
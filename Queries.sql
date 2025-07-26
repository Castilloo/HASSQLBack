CREATE DATABASE HASSQL;

USE HASSQL;

CREATE TABLE Categorias(
	IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
	Nombre NVARCHAR(100)
);

CREATE TABLE Marcas (
	IdMarca INT IDENTITY(1,1) PRIMARY KEY,
	Nombre NVARCHAR(100)
);

CREATE TABLE Productos (
	IdProducto INT IDENTITY(1,1) PRIMARY KEY,
	Referencia VARCHAR(100) NOT NULL,
	Nombre NVARCHAR(200) NOT NULL,
	IdCategoria INT,
	IdMarca INT, 
	Descripcion NVARCHAR(500) NOT NULL,
	Imagen VARCHAR(200) NOT NULL,
	Cantidad INT NOT NULL,
	Precio DECIMAL(10,2) NOT NULL,
	FechaCreacion DATE NOT NULL,
	
	CONSTRAINT FK_Productos_Categoria FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria),
	CONSTRAINT FK_Productos_Marca FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca),
);

--Inserción de valores 

INSERT INTO Categorias (Nombre) VALUES
('Balones'),
('Accesorios de Entrenamiento'),
('Ropa Deportiva'),
('Calzado Deportivo'),
('Protección'),
('Fitness'),
('Natación'),
('Ciclismo'),
('Tenis'),
('Boxeo');

INSERT INTO Marcas (Nombre) VALUES
('Nike'),
('Adidas'),
('Under Armour'),
('Wilson'),
('Spalding'),
('Everlast'),
('Speedo'),
('Reebok'),
('Puma'),
('Decathlon');

INSERT INTO Productos (
    Nombre, IdCategoria, IdMarca, Descripcion, Imagen, Cantidad, Precio, FechaCreacion, Referencia
) VALUES
('Balón de fútbol Adidas', 1, 2, 'Balón oficial tamaño 5', 'balon_futbol.jpg', 50, 79.99, GETDATE(), 'ADFB-001'),
('Balón de baloncesto Wilson', 1, 5, 'Balón NBA réplica', 'balon_basket.jpg', 30, 89.99, GETDATE(), 'WIBB-002'),
('Zapatos de running Nike', 2, 1, 'Calzado ligero para corredores', 'nike_running.jpg', 40, 129.99, GETDATE(), 'NIRU-003'),
('Tenis Adidas deportivos', 2, 2, 'Tenis para entrenamiento', 'adidas_tenis.jpg', 35, 119.50, GETDATE(), 'ADDR-004'),
('Camiseta deportiva Puma', 3, 3, 'Camiseta transpirable dry-fit', 'camiseta_puma.jpg', 60, 39.99, GETDATE(), 'PUDR-005'),
('Pantalón de compresión UA', 3, 4, 'Pantalón deportivo de compresión', 'pant_ua.jpg', 25, 59.99, GETDATE(), 'UACM-006'),
('Guantes de gimnasio Reebok', 4, 6, 'Guantes para levantamiento de pesas', 'guantes_reebok.jpg', 45, 24.99, GETDATE(), 'REGL-007'),
('Cinturón lumbar Nike', 4, 1, 'Cinturón de soporte lumbar', 'cinturon_nike.jpg', 20, 34.99, GETDATE(), 'NIBK-008'),
('Rodilleras Adidas', 5, 2, 'Rodilleras de protección deportiva', 'rodilleras_adidas.jpg', 50, 19.99, GETDATE(), 'ADRD-009'),
('Coderas Puma', 5, 3, 'Coderas con gel para impacto', 'coderas_puma.jpg', 30, 22.50, GETDATE(), 'PUCD-010'),
('Raqueta de tenis Wilson', 6, 5, 'Raqueta profesional de grafito', 'raqueta_wilson.jpg', 15, 199.99, GETDATE(), 'WIRT-011'),
('Raqueta de squash Head', 6, 5, 'Raqueta ligera para squash', 'raqueta_head.jpg', 10, 149.99, GETDATE(), 'HESQ-012'),
('Gorro de natación Speedo', 7, 7, 'Gorro de silicona resistente', 'gorro_speedo.jpg', 80, 14.99, GETDATE(), 'SPGC-013'),
('Gafas de natación Speedo', 7, 7, 'Gafas antiempañantes', 'gafas_speedo.jpg', 60, 24.99, GETDATE(), 'SPGN-014'),
('Bicicleta de montaña Asics', 8, 8, 'Bici con suspensión doble', 'bici_asics.jpg', 8, 999.99, GETDATE(), 'ASMT-015'),
('Casco de ciclismo Nike', 8, 1, 'Casco aerodinámico para ciclismo', 'casco_nike.jpg', 22, 89.99, GETDATE(), 'NICS-016'),
('Guantes de ciclismo UA', 8, 4, 'Guantes acolchados', 'guantes_ua.jpg', 40, 29.99, GETDATE(), 'UAGC-017'),
('Sudadera Adidas', 3, 2, 'Sudadera con cierre frontal', 'sudadera_adidas.jpg', 25, 59.90, GETDATE(), 'ADSU-018'),
('Short deportivo Puma', 3, 3, 'Short con malla interior', 'short_puma.jpg', 30, 29.90, GETDATE(), 'PUSH-019'),
('Balón de voleibol Mikasa', 1, 5, 'Balón profesional para voleibol', 'balon_voley.jpg', 35, 74.99, GETDATE(), 'MIVB-020');


EXEC ProductosDetallados;
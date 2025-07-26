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
	Imagen VARCHAR(500) NOT NULL,
	Cantidad INT NOT NULL,
	Precio DECIMAL(10,2) NOT NULL,
	FechaCreacion DATE NOT NULL,
	
	CONSTRAINT FK_Productos_Categoria FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria),
	CONSTRAINT FK_Productos_Marca FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca),
);

--Inserci�n de valores 

INSERT INTO Categorias (Nombre) VALUES
('Balones'),
('Accesorios de Entrenamiento'),
('Ropa Deportiva'),
('Calzado Deportivo'),
('Protecci�n'),
('Fitness'),
('Nataci�n'),
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
('Bal�n de f�tbol Adidas', 1, 2, 'Bal�n oficial tama�o 5', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_z-M9FUf6IG0ZwydEzTsK6f4Z4Ue8NYVVsQ&s', 50, 79.99, GETDATE(), 'ADFB-001'),
('Bal�n de baloncesto Wilson', 1, 4, 'Bal�n NBA r�plica', 'https://wilsonstore.com.co/wp-content/uploads/2025/02/5ff8adcd920c0270df55dfcc_thumbnail.jpg', 30, 89.99, GETDATE(), 'WIBB-002'),
('Zapatos de running Nike', 2, 1, 'Calzado ligero para corredores', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSf82pCvLYIddYIqV2b97roC4cAyOwwoD8AIQ&s', 40, 129.99, GETDATE(), 'NIRU-003'),
('Tenis Adidas deportivos', 2, 2, 'Tenis para entrenamiento', 'https://assets.adidas.com/images/w_600,f_auto,q_auto/aa0bd45abac541d29ff40b492e15cfa4_9366/Tenis_de_Running_Runfalcon_5_Negro_IH7758_HM1.jpg', 35, 119.50, GETDATE(), 'ADDR-004'),
('Camiseta deportiva Puma', 3, 9, 'Camiseta transpirable dry-fit', 'https://static.dafiti.com.co/p/puma-4173-7007821-1-zoom.jpg', 60, 39.99, GETDATE(), 'PUDR-005'),
('Pantal�n de compresi�n UA', 3, 4, 'Pantal�n deportivo de compresi�n', 'https://underarmour.scene7.com/is/image/Underarmour/V5-1366075-100_FC?rp=standard-0pad%7CgridTileDesktop&scl=1&fmt=jpg&qlt=50&resMode=sharp2&cache=on%2Con&bgc=F0F0F0&wid=512&hei=640&size=512%2C640', 25, 59.99, GETDATE(), 'UACM-006'),
('Guantes de gimnasio Reebok', 4, 8, 'Guantes para levantamiento de pesas', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQR6gbUjDAzi1vde6AsiRdFIiplTg-SaLw9Mg&s', 45, 24.99, GETDATE(), 'REGL-007'),
('Cintur�n lumbar Nike', 4, 1, 'Cintur�n de soporte lumbar', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIrzjMG5qdJ6SfogFNECoT-AJpkQK2pvgEfw&s', 20, 34.99, GETDATE(), 'NIBK-008'),
('Rodilleras Adidas', 5, 2, 'Rodilleras de protecci�n deportiva', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1ebZnPWINsIzcmCiUxhvODbAJdmaP4xd07g&s', 50, 19.99, GETDATE(), 'ADRD-009'),
('Coderas Puma', 5, 9, 'Coderas con gel para impacto', 'https://resources.sears.com.mx/medios-plazavip/mkt/630a4475a2d2e_1661551433268png.jpg?scale=500&qlty=75', 30, 22.50, GETDATE(), 'PUCD-010'),
('Raqueta de tenis Wilson', 6, 4, 'Raqueta profesional de grafito', 'https://www.tennis-point.es/wilson-clash-100l-v3.0-raquetas-de-competicion-00707504152000.html', 15, 199.99, GETDATE(), 'WIRT-011'),
('Camiseta Tottenham Under Armour', 3, 3, 'Camiseta Tottenham Premier League', 'https://i.ebayimg.com/thumbs/images/g/GGsAAOSwJ2NoVovE/s-l1200.jpg', 10, 149.99, GETDATE(), 'HESQ-012'),
('Gorro de nataci�n Speedo', 7, 7, 'Gorro de silicona resistente', 'https://speedoco.vteximg.com.br/arquivos/ids/165369-292-292/gorros-gorros-negro-8-720640001-1.jpg?v=637226435072470000', 80, 14.99, GETDATE(), 'SPGC-013'),
('Gafas de nataci�n Speedo', 7, 7, 'Gafas antiempa�antes', 'https://speedoco.vteximg.com.br/arquivos/ids/164905-292-292/gafas-gafas-azul-blanco-8-092978577-1.jpg?v=637197134555430000', 60, 24.99, GETDATE(), 'SPGN-014'),
('Guantes de boxeo Everlast', 10, 6, 'Guantes de boxeo profesional para todas las tallas', 'https://http2.mlstatic.com/D_NQ_NP_778599-MCO79892994774_102024-O.webp', 8, 999.99, GETDATE(), 'ASMT-015'),
('Casco de ciclismo Nike', 8, 1, 'Casco aerodin�mico para ciclismo', 'https://poseidonbogota.com/wp-content/uploads/2023/03/ontrail-sports-bikes-accesorios-helmets-ARMOR-CAS0486-MTB-URBAN_5-1.jpg', 22, 89.99, GETDATE(), 'NICS-016'),
('Pesas Rusas Kettlebell Everlast PVC', 6, 6, 'Pesas Rusas Kettlebell Everlast PVC para ejercicios de mucha exigencia', 'https://www.maoz29.cl/wp-content/uploads/2020/09/Everlast-Maoz29-Kettlebell-Everlast-PVC-y-Arena.jpg', 40, 29.99, GETDATE(), 'UAGC-017'),
('Sudadera Adidas', 3, 2, 'Sudadera con cierre frontal', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQEO71wXobcR7ZqJ469YDwLQ-tCouetPO4t6w&s', 25, 59.90, GETDATE(), 'ADSU-018'),
('Short deportivo Puma', 3, 9, 'Short con malla interior', 'https://static.dafiti.com.co/p/puma-0844-5051662-1-zoom.jpg', 30, 29.90, GETDATE(), 'PUSH-019'),
('Bal�n Baloncesto Spalding', 1, 5, 'Bal�n Baloncesto Spalding NBA Gold N7', 'https://deportesregol.com/cdn/shop/files/0010_Balon_Baloncesto_Spalding_gold_Opcion_A.jpg?v=1742199848', 35, 74.99, GETDATE(), 'MIVB-020');


EXEC ProductosDetallados WHERE @Referencia = 'ADFB-001';
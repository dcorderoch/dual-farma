--******************************************************************************************************************************************
--**
--**                                     Azure configuration and user account creation
--**
--******************************************************************************************************************************************

--Creates login user in SQL
CREATE LOGIN FarmaTicaDBUser 
    WITH PASSWORD = 'FarmaTica';
GO

--Creates user in DB for Windows and Azure connections
CREATE USER FarmaTicaDBUser FOR LOGIN FarmaTicaDBUser;
GO

--Grant permissions
EXEC sp_addrolemember 'db_owner', 'FarmaTicaDBUser';
GO

--Creating FarmaTicaDB schema
CREATE database FarmaTicaDB;
GO

--Grant user operations into Azure SQL database
Grant ALTER to FarmaTicaDBUser;
GO


--******************************************************************************************************************************************
--**
--**                                      Main definition of tables in FarmaTica
--**
--******************************************************************************************************************************************

--Creates table User. 

GO
CREATE TABLE Usuario
	(
	ID_Usuario nvarchar(20) NOT NULL,
	Pass nvarchar(30) NOT NULL,
	Nombre nvarchar(30) NOT NULL,
	PrimerApellido nvarchar(50) NOT NULL,
	SegundoApellido nvarchar (50) NOT NULL,
	Email nvarchar(50) NOT NULL,
	Compañia nvarchar(50) NOT NULL,
	Rol_Usuario Integer NOT NULL,
	ID_Sucursal uniqueidentifier NOT NULL 
	)

--Creates table Role.
GO
CREATE TABLE Rol
	(
	ID_Rol Integer NOT NULL,
	Rol nvarchar(30) NOT NULL
	)

--Creates table Client.
GO
CREATE TABLE Cliente
(
 NumeroCedula CHAR(9) NOT NULL,
 Nombre nvarchar(30) NOT NULL,
 PrimerApellido nvarchar(50) NOT NULL,
 SegundoApellido nvarchar (50) NOT NULL,
 CantidadMultas Integer NOT NULL,
 LugarResidencia nvarchar(50) NOT NULL,
 Historial nvarchar(max) NOT NULL,
 FechaNacimiento DATE NOT NULL,
 NumeroTelefono nvarchar(25) NOT NULL,
 Pass nvarchar(30) NOT NULL,
)

--Creates table Doctor.
GO
CREATE TABLE Doctor
	(
	ID_Doctor nvarchar(15) NOT NULL,
	NumeroCedula nvarchar(15) NOT NULL,
	Nombre nvarchar(30) NOT NULL,
	PrimerApellido nvarchar(50) NOT NULL,
	SegundoApellido nvarchar (50) NOT NULL,
	LugarResidencia nvarchar(50) NOT NULL
 	)

--Creates table Order.
GO
CREATE TABLE Pedido
	(
	NumeroPedido uniqueidentifier NOT NULL,
	ID_Cliente char(9) NOT NULL,
	ID_Receta uniqueidentifier,
	Sucursal_Recojo uniqueidentifier NOT NULL,
	ImagenFactura VARBINARY(MAX), 
	Prescripcion Bit NOT NULL,
	Estado Integer NOT NULL,
	Prioridad nchar(9) NOT NULL,
	TelefonoPreferido nvarchar(20) NOT NULL,
	FechaRecojo DATETIME NOT NULL,
	Tipo_Pedido Bit NOT NULL
	)	

--Creates table Medicine.
GO
CREATE TABLE Medicamento
	(
	ID_Medicamento uniqueidentifier NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Prescripcion Bit NOT NULL
	)	

-- Creates table Medicamentos_Por_Receta
GO
CREATE TABLE Medicamentos_Por_Receta
	(
	NumeroReceta uniqueidentifier NOT NULL,
	ID_Medicamento uniqueidentifier NOT NULL
	)

-- Creates table Medicamentos_Por_Pedido
GO
CREATE TABLE Medicamentos_Por_Pedido
	(
	NumeroPedido uniqueidentifier NOT NULL,
	ID_Medicamento uniqueidentifier NOT NULL,
	ID_Sucursal uniqueidentifier NOT NULL
	)

-- Creates table Medicamentos_Por_Sucursal
GO
CREATE TABLE Medicamentos_Por_Sucursal
	(
	ID_Sucursal uniqueidentifier NOT NULL,
	ID_Medicamento uniqueidentifier NOT NULL,
	CantidadDisponible Integer NOT NULL,
	CantidadVentas Integer NOT NULL,
	Precio decimal(10,2) NOT NULL
	)

--Creates table Prescription.
GO
CREATE TABLE Receta
	(
	NumeroReceta uniqueidentifier NOT NULL,
	Doctor nvarchar(15) NOT NULL,
	Imagen VARBINARY(max) NOT NULL,
	)

--Creates table Branch office.
GO
CREATE TABLE Sucursal
	(
	ID_Sucursal uniqueidentifier NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Telefono nvarchar(20) NOT NULL,
	Ubicacion nvarchar(100) NOT NULL,
	Compañia nvarchar(20) NOT NULL
	)	

-- Defines User primary key.
GO
ALTER TABLE Usuario
	ADD CONSTRAINT PK_Usuario
		PRIMARY KEY (ID_Usuario)

-- Defines Role primary key.
GO
ALTER TABLE Rol
	ADD CONSTRAINT PK_Rol
		PRIMARY KEY (ID_Rol)

-- Defines Client primary key.
GO
ALTER TABLE Cliente 
	ADD CONSTRAINT PK_Cliente
		PRIMARY KEY (NumeroCedula)

-- Defines Doctor primary key.
GO 
ALTER TABLE Doctor 
	ADD CONSTRAINT PK_Doctor
		PRIMARY KEY (ID_Doctor)

-- Defines Order primary key.
GO
ALTER TABLE Pedido 
	ADD CONSTRAINT PK_Pedido
		PRIMARY KEY (NumeroPedido)

-- Defines Medicine primary key.
GO
ALTER TABLE Medicamento 
	ADD CONSTRAINT PK_Medicamento
		PRIMARY KEY (ID_Medicamento)

-- Defines Prescription primary key.
GO
ALTER TABLE Receta
	ADD CONSTRAINT PK_Receta
		PRIMARY KEY (NumeroReceta)

-- Defines Branch office primary key.
GO
ALTER TABLE Sucursal
	ADD CONSTRAINT PK_Sucursal
		PRIMARY KEY (ID_Sucursal)

--Defines Medicamentos_Por_Receta primary keys.
GO
ALTER TABLE Medicamentos_Por_Receta
	ADD CONSTRAINT PK_Numero_Receta_ID_Medicamento
		PRIMARY KEY (NumeroReceta, ID_Medicamento)

--Defines Medicamentos_Por_Receta foreign keys.
GO
ALTER TABLE Medicamentos_Por_Receta
	ADD CONSTRAINT FK_Numero_Receta_MPR
		FOREIGN KEY (NumeroReceta)
			REFERENCES Receta(NumeroReceta)

GO
ALTER TABLE Medicamentos_Por_Receta
	ADD CONSTRAINT FK_ID_Medicamento_MPR
		FOREIGN KEY (ID_Medicamento)
			REFERENCES Medicamento(ID_Medicamento)

--Defines Medicamentos_Por_Pedido primary keys.
GO
ALTER TABLE Medicamentos_Por_Pedido
	ADD CONSTRAINT PK_Numero_Pedido_ID_Medicamento
		PRIMARY KEY (NumeroPedido, ID_Medicamento, ID_Sucursal)

--Defines Medicamentos_Por_Pedido foreign keys.
GO
ALTER TABLE Medicamentos_Por_Pedido
	ADD CONSTRAINT FK_Numero_Pedido_MPP
		FOREIGN KEY (NumeroPedido)
			REFERENCES Pedido(NumeroPedido)

GO
ALTER TABLE Medicamentos_Por_Pedido
	ADD CONSTRAINT FK_ID_Medicamento_MPP
		FOREIGN KEY (ID_Medicamento)
			REFERENCES Medicamento(ID_Medicamento)

GO
ALTER TABLE Medicamentos_Por_Pedido
	ADD CONSTRAINT FK_ID_Sucursal_MPP
		FOREIGN KEY (ID_Sucursal)
			REFERENCES Sucursal(ID_Sucursal)

--Defines Medicamentos_Por_Sucursal primary keys.
GO
ALTER TABLE Medicamentos_Por_Sucursal
	ADD CONSTRAINT PK_ID_Sucursal_ID_Medicamento
		PRIMARY KEY (ID_Sucursal, ID_Medicamento)

--Defines Medicamentos_Por_Sucursal foreign keys.
GO
ALTER TABLE Medicamentos_Por_Sucursal
	ADD CONSTRAINT FK_ID_Sucursal_MPS
		FOREIGN KEY (ID_Sucursal)
			REFERENCES Sucursal(ID_Sucursal)

GO
ALTER TABLE Medicamentos_Por_Sucursal
	ADD CONSTRAINT FK_ID_Medicamento_MPS
		FOREIGN KEY (ID_Medicamento)
			REFERENCES Medicamento(ID_Medicamento)

-- Sets a relationship between columns Rol_Usuario in User table and ID_Rol in Rol table by creating a Foreign Key.
GO
ALTER TABLE Usuario 
	ADD CONSTRAINT FK_Rol_Usuario
		FOREIGN KEY (Rol_Usuario)
			REFERENCES Rol(ID_Rol)

-- Sets a relationship between columns Rol_Usuario in User table and ID_Rol in Rol table by creating a Foreign Key.
GO
ALTER TABLE Usuario 
	ADD CONSTRAINT FK_ID_Sucursal_Usuario
		FOREIGN KEY (ID_Sucursal)
			REFERENCES Sucursal(ID_Sucursal)

-- Sets a relationship between columns ID_Cliente in Order table and NumeroCedula in Client table by creating a Foreign Key.
GO			
ALTER TABLE Pedido
	ADD CONSTRAINT FK_Cliente
		FOREIGN KEY (ID_Cliente)
			REFERENCES Cliente(NumeroCedula) 

-- Sets a relationship between columns ID_Receta in Order table and NumeroReceta in Prescription table by creating a Foreign Key.	
GO			
ALTER TABLE Pedido
	ADD CONSTRAINT FK_Numero_Receta
		FOREIGN KEY (ID_Receta)
			REFERENCES Receta(NumeroReceta) 

-- Sets a relationship between columns Sucursal_Recojo in Order table and ID_Sucursal in Branch office table by creating a Foreign Key.
GO
ALTER TABLE Pedido
	ADD CONSTRAINT FK_ID_Sucursal_Recojo
		FOREIGN KEY (Sucursal_Recojo)
			REFERENCES Sucursal(ID_Sucursal)

-- Sets a relationship between columns Doctor in Prescription table and ID_Doctor in Doctor table by creating a Foreign Key.	
GO
ALTER TABLE Receta
	ADD CONSTRAINT FK_ID_Doctor
		FOREIGN KEY (Doctor)
			REFERENCES Doctor(ID_Doctor)

--******************************************************************************************************************************************
--**
--**                                     Populating DB 
--**
--******************************************************************************************************************************************	

-- Insertion of Roles
GO
INSERT INTO Rol
VALUES (1, 'Administrador'),
	   (2, 'Dependiente')
	   ;

-- Insertion of Branch offices.
GO
INSERT INTO Sucursal
VALUES (NEWID(), 'Medio Queso', '27849596', 'Los Chiles', 'Farmatica'),
	   (NEWID(), 'Manuel Antonio', '26709596', 'Quepos', 'Phischel'),
	   (NEWID(), 'Cariari', '25325960', 'Pococi','Farmatica'),
	   (NEWID(), 'San Antonio', '22395960', 'Belen', 'Phischel'),
	   (NEWID(), 'La Aurora', '22934364', 'Heredia', 'Phischel'),
	   (NEWID(), 'Chomes', '28734364', 'Puntarenas','Farmatica'),
	   (NEWID(), 'Miami', '1998293451', 'Florida','Farmatica'),
	   (NEWID(), 'Escazu', '22157084', 'San Jose', 'Farmatica')
	   ;

-- Insertion of Users.
GO
INSERT INTO Usuario
VALUES ('kevuo', 'moradodecorazon32', 'Kevin', 'Umaña', 'Ortega', 'kevgiso@hotmail.com', 'Phischel', '1', 'f507a394-1adf-4783-96b6-f9838fb3af0d'),
	   ('manu3193', 'frenteampliorocks', 'Manuel', 'Zumbado', 'Corrales', 'manu3193@gmail.com', 'Farmatica', '1', '9bc74d68-cfda-4529-bd6e-1241ded80332'),
	   ('majesco', 'miamigohelo', 'Nicolas', 'Jimenez', 'Garcia', 'n.jimenez@gmail.com', 'Farmatica', '2', '20fa0743-8f74-4031-b2bb-1818a72a8d60'),
	   ('eldavid', 'soypoliglota', 'David', 'Cordero', 'Chavarria', 'dacoch@outlook.com', 'Farmatica', '2', '9b5d52be-f4a6-4603-8cd2-73bcf326ee6c')
	   ;

--Insertion of Clients
GO
INSERT INTO Cliente
VALUES ('123456789', 'Alonso', 'Huertas', 'Vargas', '1', 'Guapiles', 'Constantes dolores de espalda', '1958-09-19','85479587','alonhuervar'),
	   ('143676785', 'Arnoldo', 'Martinez', 'Perez', '5', 'San Jose', 'Migrañas', '1967-09-02','85509480','armape'),
	   ('403341797', 'Marta', 'Peña', 'Angulo', '2', 'Heredia', 'Problemas en el colon', '1948-12-13','87475581','marpean'),
	   ('106720579', 'Rosa', 'Aguilar', 'Lizano', '2', 'Ft. Lauderdale', 'Diabetes', '1970-12-15','1755356814','roagli')
	   ;

-- Insertion of Medicine.
GO
INSERT INTO Medicamento
VALUES (NEWID(), 'Acetaminofén', '0'),
	   (NEWID(), 'Ibuprofeno', '0'),
	   (NEWID(), 'Dorival', '0'),
	   (NEWID(), 'Panadol', '0'),
	   (NEWID(), 'Espasmo Canulase', '0'),
	   (NEWID(), 'Ritalina', '1'),
	   (NEWID(), 'Concerta', '1'),
	   (NEWID(), 'Selfemra', '1')
	   ;

--Insertion in Medicines-per-Branch Office

--Miami
GO
INSERT INTO Medicamentos_Por_Sucursal
VALUES ('9bc74d68-cfda-4529-bd6e-1241ded80332', '67d563fc-9fb9-47bc-a6fa-2ff656df7c3e', '20', '11', '13.50'),
	   ('9bc74d68-cfda-4529-bd6e-1241ded80332', 'f46a4627-3e2d-4083-bd8a-321331f92b48', '50', '25', '2.00'),
	   ('9bc74d68-cfda-4529-bd6e-1241ded80332', '7a68b1fa-220e-4caa-ac49-636c00fef0cd', '30', '2',  '5.00'),
	   ('9bc74d68-cfda-4529-bd6e-1241ded80332', '0f2d6a63-0fc9-492a-9d9a-ef4d2f04d51b', '95', '80', '3.00')
	   ;

--Cariari
GO
INSERT INTO Medicamentos_Por_Sucursal
VALUES ('20fa0743-8f74-4031-b2bb-1818a72a8d60', '67d563fc-9fb9-47bc-a6fa-2ff656df7c3e', '20', '15', '10000'),
	   ('20fa0743-8f74-4031-b2bb-1818a72a8d60', 'f46a4627-3e2d-4083-bd8a-321331f92b48', '40', '35', '1500'),
	   ('20fa0743-8f74-4031-b2bb-1818a72a8d60', '7a68b1fa-220e-4caa-ac49-636c00fef0cd', '30', '26',  '3000'),
	   ('20fa0743-8f74-4031-b2bb-1818a72a8d60', '0f2d6a63-0fc9-492a-9d9a-ef4d2f04d51b', '95', '74', '2000'),
	   ('20fa0743-8f74-4031-b2bb-1818a72a8d60', 'c71da982-ddf8-4f6a-b7ed-e099318e2561', '15', '10', '1850'),
	   ('20fa0743-8f74-4031-b2bb-1818a72a8d60', 'd4111e82-3cb2-4fcc-ab97-8d57c2fd5ad5', '20', '8', '12500')
	   ;

--Chomes
GO
INSERT INTO Medicamentos_Por_Sucursal
VALUES ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', '67d563fc-9fb9-47bc-a6fa-2ff656df7c3e', '20', '15', '10000'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', 'f46a4627-3e2d-4083-bd8a-321331f92b48', '40', '35', '1500'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', '7a68b1fa-220e-4caa-ac49-636c00fef0cd', '30', '26',  '3000'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', '0f2d6a63-0fc9-492a-9d9a-ef4d2f04d51b', '95', '74', '2000'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', 'c71da982-ddf8-4f6a-b7ed-e099318e2561', '15', '10', '1850'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', 'd4111e82-3cb2-4fcc-ab97-8d57c2fd5ad5', '20', '8', '12500'),
	   ('9b5d52be-f4a6-4603-8cd2-73bcf326ee6c', '06fb5c48-ede2-45fb-be2e-a1e7c0c66c6f', '30', '24', '950')
	   ;

GO
INSERT INTO Doctor
VALUES ('ABC001', '101230456','Alberto', 'Del Rio', ' ', 'Puntarenas'),
	   ('ABC005', '991230456','Ben', 'Smith', ' ', 'Miami'),
	   ('CR003', '322304561','Gerardo', 'Guzman', 'Lopez', 'San Jose'),
	   ('CR023', '517902233','Alejandra', 'Saenz', 'Cardenas', 'Rio Frio')
	   ;




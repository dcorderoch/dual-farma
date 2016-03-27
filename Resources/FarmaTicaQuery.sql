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
	SegundoApellido nvarchar (50),
	Compañia nvarchar(50),
	Email nvarchar(50),
	Rol_Usuario Integer NOT NULL 
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
 SegundoApellido nvarchar (50),
 CantidadMultas Integer,
 LugarResidencia nvarchar(50),
 Historial nvarchar(max),
 FechaNacimiento DATE,
 NumeroTelefono nvarchar(25)
)

--Creates table Doctor.
GO
CREATE TABLE Doctor
	(
	ID_Doctor nvarchar(15) NOT NULL,
	NumeroCedula nvarchar(15) NOT NULL,
	Nombre nvarchar(30) NOT NULL,
	PrimerApellido nvarchar(50) NOT NULL,
	SegundoApellido nvarchar (50),
	LugarResidencia nvarchar(50)
 	)

--Creates table Order.
GO
CREATE TABLE Pedido
	(
	NumeroPedido uniqueidentifier NOT NULL,
	ID_Cliente char(9) NOT NULL,
	ID_Receta Integer,
	ID_Medicamento uniqueidentifier NOT NULL,
	Sucursal_Recojo Integer NOT NULL,
	Codigo_Factura uniqueidentifier NOT NULL, 
	Prescripcion Bit NOT NULL,
	Estado Integer NOT NULL,
	Prioridad nchar(9) NOT NULL,
	TelefonoPreferido nvarchar(20),
	FechaRecojo DATETIME
	)	

--Creates table Medicine.
GO
CREATE TABLE Medicamento
	(
	ID_Medicamento uniqueidentifier NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Prescripcion Bit NOT NULL,
	Precio decimal(10,2) NOT NULL,
	Sucursal_Origen Integer NOT NULL,
	CasaFarmaceutica nvarchar(30),
	CantidadDisponible Integer NOT NULL,
	CantidadVentas Integer NOT NULL,
	)	

-- Creates table Medicamentos_Por_Receta
GO
CREATE TABLE Medicamentos_Por_Receta
	(
	NumeroReceta Integer NOT NULL,
	ID_Medicamento uniqueidentifier NOT NULL
	)


--Creates table Prescription.
GO
CREATE TABLE Receta
	(
	NumeroReceta Integer NOT NULL,
	Doctor nvarchar(15) NOT NULL,
	Imagen Binary,
	)
GO
ALTER TABLE Receta
	DROP CONSTRAINT FK_ID_Medicamento_Receta
GO
ALTER TABLE Receta
	DROP COLUMN Medicamento
--Creates table Branch office.
GO
CREATE TABLE Sucursal
	(
	ID_Sucursal Integer NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Telefono nvarchar(20) NOT NULL,
	Ubicacion nvarchar(100)
	)	
--Creates table Bill.
GO
CREATE TABLE Factura
	(
	ID_Factura uniqueidentifier NOT NULL,
	Imagen Binary,
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

-- Defines Bill primary key.
GO
ALTER TABLE Factura
	ADD CONSTRAINT PK_Factura
		PRIMARY KEY (ID_Factura)

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

-- Sets a relationship between columns Rol_Usuario in User table and ID_Rol in Rol table by creating a Foreign Key.
GO
ALTER TABLE Usuario 
	ADD CONSTRAINT FK_Rol_Usuario
		FOREIGN KEY (Rol_Usuario)
			REFERENCES Rol(ID_Rol)

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

-- Sets a relationship between columns ID_Medicamento in Order table and ID_Medicamento in Medicine table by creating a Foreign Key.
GO			
ALTER TABLE Pedido
	ADD CONSTRAINT FK_ID_Medicamento
		FOREIGN KEY (ID_Medicamento)
			REFERENCES Medicamento(ID_Medicamento) 

-- Sets a relationship between columns Sucursal_Recojo in Order table and ID_Sucursal in Branch office table by creating a Foreign Key.
GO
ALTER TABLE Pedido
	ADD CONSTRAINT FK_ID_Sucursal_Recojo
		FOREIGN KEY (Sucursal_Recojo)
			REFERENCES Sucursal(ID_Sucursal)

-- Sets a relationship between columns Codigo_Factura in Order table and ID_Factura in Bill table by creating a Foreign Key.
GO
ALTER TABLE Pedido
	ADD CONSTRAINT FK_Codigo_Factura
		FOREIGN KEY (Codigo_Factura)
			REFERENCES Factura(ID_Factura)

-- Sets a relationship between columns Doctor in Prescription table and ID_Doctor in Doctor table by creating a Foreign Key.	
GO
ALTER TABLE Receta
	ADD CONSTRAINT FK_ID_Doctor
		FOREIGN KEY (Doctor)
			REFERENCES Doctor(ID_Doctor)

-- Sets a relationship between columns Medicine in Prescription table and ID_Medicine in Medicine table by creating a Foreign Key.	
GO
ALTER TABLE Receta
	ADD CONSTRAINT FK_ID_Medicamento_Receta
		FOREIGN KEY (Medicamento)
			REFERENCES Medicamento(ID_Medicamento)

-- Sets a relationship between columns Sucursal_Origen in Medicine table and ID_Sucursal in Branch office table by creating a Foreign Key.	
GO
ALTER TABLE Medicamento
	ADD CONSTRAINT FK_Sucursal_Origen
		FOREIGN KEY (Sucursal_Origen)
			REFERENCES Sucursal(ID_Sucursal)

			--********************* Correcting Mistakes from here on. Not to take into account in database creation for the first time***************************

-- Modifies the allowed password length in the Pass column from User table.
GO
ALTER TABLE Usuario 
	ALTER COLUMN Pass nvarchar(30) NOT NULL	 				

-- Modifies the record max value from Client table.
GO
ALTER TABLE Cliente
	ALTER COLUMN Historial nvarchar(max)

-- Deletes column HoraRecojo from Order table.
GO
ALTER TABLE Pedido
	DROP COLUMN HoraRecojo

-- Sets column FechaRecojo as DATETIME instead of DATE in Order table.
GO
ALTER TABLE Pedido
	ALTER COLUMN FechaRecojo DATETIME

-- Sets Prescripcion column as NOT NULL in table Pedido.
GO
ALTER TABLE Pedido
	ALTER COLUMN Prescripcion Bit NOT NULL

-- Sets Prescripcion column as NOT NULL in table Medicamento.
GO
ALTER TABLE Medicamento
	ALTER COLUMN Prescripcion Bit NOT NULL

-- Removes Not Null constraint from ID_Receta in table Pedido.
GO
ALTER TABLE Pedido
	ALTER COLUMN ID_Receta Integer

--Removes foreign key from Order table.
GO
ALTER TABLE Pedido
	DROP CONSTRAINT FK_Estado_Pedido

--Drops Estado_Pedido table. 
GO
DROP TABLE Estado_Pedido

--Adds a column Company to User table.
GO
ALTER TABLE Usuario
	ADD Compañia nvarchar(50)

CREATE DATABASE PROJECTAPP


USE PROJECTAPP

CREATE TABLE REGISTRO (
IdUsuario int primary key  identity(1,1),
Nombre varchar(30),
Identificacion varchar(20),
Telefono varchar(20),
Correo varchar(50),
Edad varchar(50),
Peso_Corporal varchar(50),
Plan_Entreno varchar(50),
Usuario varchar(50),
Contrasena varchar(50)
)

CREATE TABLE RESERVAS(
IdUser int primary key  identity(1,1),
Nombre varchar(30),
Identificacion varchar(20),
Reserva varchar(100),
Fecha_Registro datetime default getdate(),
)

--ALTER TABLE [dbo].[REGISTRO] ALTER COLUMN Identificacion varchar(20)
--ALTER TABLE [dbo].[RESERVAS] ALTER COLUMN Identificacion varchar(20)

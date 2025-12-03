CREATE TABLE [dbo].[TB_Usuario]
(
    [IdUsuario] INT IDENTITY(1,1) NOT NULL,
    [Usuario] NVARCHAR(50) NOT NULL,
    [Contrasena] NVARCHAR(200) NOT NULL,
    [NombreCompleto] NVARCHAR(120) NOT NULL,
    [Activo] BIT NOT NULL CONSTRAINT [DF_TB_Usuario_Activo] DEFAULT (1),
    CONSTRAINT [PK_TB_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [UQ_TB_Usuario_Usuario] UNIQUE ([Usuario])
);
GO

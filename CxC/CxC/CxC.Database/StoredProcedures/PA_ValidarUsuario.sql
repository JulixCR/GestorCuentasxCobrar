CREATE PROCEDURE [dbo].[PA_ValidarUsuario]
    @Usuario NVARCHAR(50),
    @Contrasena NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [IdUsuario], [Usuario], [NombreCompleto], [Activo]
    FROM [dbo].[TB_Usuario]
    WHERE [Usuario] = @Usuario
      AND [Contrasena] = @Contrasena
      AND [Activo] = 1;
END;
GO

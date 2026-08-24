CREATE PROCEDURE [dbo].spUsers_Authenticate
    @Username nvarchar(50),
    @Password nvarchar(36)
AS
begin
    set nocount on;

    SELECT [Id], [UserName], [FirstName], [LastName], [Password]
    FROM dbo.Users
    WHERE UserName = @Username
      AND [Password] = @Password;
end
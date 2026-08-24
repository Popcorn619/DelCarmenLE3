DROP PROCEDURE IF EXISTS dbo.spPosts_Detail;
GO

CREATE PROCEDURE dbo.spPosts_Detail
    @id int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.[Id],
        p.[Title],
        p.[Body],
        p.[DateCreated],
        u.[UserName],
        u.[FirstName],
        u.[LastName]
    FROM dbo.Posts p
    INNER JOIN dbo.Users u ON p.UserId = u.Id
    WHERE p.Id = @id;
END
GO

-- TEST IT
EXEC spPosts_Detail @id = 1;
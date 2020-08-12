DECLARE @userId NVARCHAR(256), @userName NVARCHAR(50), @oldname NVARCHAR(256), @newname NVARCHAR(256)

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT
	CAST([UserId] AS NVARCHAR(256))
	,[UserName]
FROM [AppUser]

OPEN cur
FETCH NEXT FROM cur INTO @userId, @userName
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @userId = LOWER(REPLACE(@userId,'-',''))

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact_'+@userName+']') AND type in (N'U'))
	BEGIN
		SET @oldname = 'Contact_'+@userName
		SET @newname = 'Contact_'+@userId
		EXEC sp_rename @oldname, @newname
	END

	FETCH NEXT FROM cur INTO @userId, @userName
END

CLOSE cur
DEALLOCATE cur
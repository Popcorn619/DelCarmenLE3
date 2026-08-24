INSERT INTO dbo.Posts (UserId, Title, Body, DateCreated)
VALUES 
(3, 'My Second Post', 'This is my second blog post. Hello everyone!', GETDATE()),
(4, 'Tips for Beginners', 'Here are some useful tips for new developers...', GETDATE());
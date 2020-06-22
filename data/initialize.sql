USE [AccountDB]
GO

INSERT INTO [dbo].[Accounts]
           ([Id]
           ,[Balance]
           ,[Agency]
           ,[AccountNumber])
     VALUES
           (NEWID(), 1000.99,'1234','4444'),
		   (NEWID(), 800.99,'4321','1111'),
		   (NEWID(), 500.99,'5678','8888')
		   
GO



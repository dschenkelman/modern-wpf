CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Email] NCHAR(50) NOT NULL, 
    [AddressLine1] NCHAR(100) NOT NULL, 
    [AddressLine2] NCHAR(50) NULL, 
    [IsActive] BIT NOT NULL
)

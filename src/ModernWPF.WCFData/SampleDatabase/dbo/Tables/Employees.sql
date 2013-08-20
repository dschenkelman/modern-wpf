CREATE TABLE [dbo].[Employees] (
    [EmployeeId]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [AddressLine1] NVARCHAR (MAX) NULL,
    [AddressLine2] NVARCHAR (MAX) NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);


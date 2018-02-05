CREATE TABLE [dbo].[Departments] (
    [Id]     VARCHAR (50)   NOT NULL,
    [HeadId] VARCHAR (50)   NULL,
    [IsHead] BIT            NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([Id] ASC)
);


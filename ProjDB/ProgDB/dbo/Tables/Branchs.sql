CREATE TABLE [dbo].[Branchs] (
    [Id]          VARCHAR (50)   NOT NULL,
    [DprtmntId]   NVARCHAR (MAX) NOT NULL,
    [HeadId]      VARCHAR (50)   NULL,
    [IsHead]      BIT            NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [DprtmntName] NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Branchs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


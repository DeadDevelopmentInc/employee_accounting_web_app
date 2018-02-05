CREATE TABLE [dbo].[Sectors] (
    [Id]          VARCHAR (50)   NOT NULL,
    [BrnchId]     NVARCHAR (MAX) NOT NULL,
    [DprtmntId]   VARCHAR (50)   NOT NULL,
    [HeadId]      VARCHAR (50)   NULL,
    [IsHead]      BIT            NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [BrnchName]   NVARCHAR (MAX) NOT NULL,
    [DprtmntName] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Sectors] PRIMARY KEY CLUSTERED ([Id] ASC)
);


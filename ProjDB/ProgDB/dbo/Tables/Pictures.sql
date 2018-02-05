CREATE TABLE [dbo].[Pictures] (
    [Id]   NVARCHAR (450) NOT NULL,
    [Path] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED ([Id] ASC)
);


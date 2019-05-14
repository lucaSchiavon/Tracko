CREATE TABLE [Contatti].[Contatti]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ClienteId] [int] NOT NULL,
[Contatto] [nvarchar] (250) COLLATE Latin1_General_CI_AS NOT NULL,
[GuidKey] [uniqueidentifier] NOT NULL,
[IsAnonimized] [bit] NOT NULL,
[IsDeleted] [bit] NOT NULL,
[CreazioneData] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[Contatti] ADD CONSTRAINT [PK_Contatti] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[Contatti] ADD CONSTRAINT [FK_Contatti_Clienti] FOREIGN KEY ([ClienteId]) REFERENCES [Utenti].[Clienti] ([Id])
GO

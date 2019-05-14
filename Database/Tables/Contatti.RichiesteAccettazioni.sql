CREATE TABLE [Contatti].[RichiesteAccettazioni]
(
[Id] [int] NOT NULL,
[ClienteId] [int] NOT NULL,
[Nome] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL,
[SystemName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[RichiesteAccettazioni] ADD CONSTRAINT [PK_RichiesteAccettazioni] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[RichiesteAccettazioni] ADD CONSTRAINT [FK_RichiesteAccettazioni_Clienti] FOREIGN KEY ([ClienteId]) REFERENCES [Utenti].[Clienti] ([Id])
GO

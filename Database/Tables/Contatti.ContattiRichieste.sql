CREATE TABLE [Contatti].[ContattiRichieste]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContattoId] [int] NOT NULL,
[SorgenteId] [int] NULL,
[RichiestaSerialized] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NOT NULL,
[CreazioneData] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiRichieste] ADD CONSTRAINT [PK_ContattiRichieste] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiRichieste] ADD CONSTRAINT [FK_ContattiRichieste_Contatti] FOREIGN KEY ([ContattoId]) REFERENCES [Contatti].[Contatti] ([Id])
GO
ALTER TABLE [Contatti].[ContattiRichieste] ADD CONSTRAINT [FK_ContattiRichieste_Sorgenti] FOREIGN KEY ([SorgenteId]) REFERENCES [Utenti].[Sorgenti] ([Id])
GO

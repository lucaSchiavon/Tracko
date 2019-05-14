CREATE TABLE [Contatti].[ContattiAccettazioniStorico]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContattoId] [int] NOT NULL,
[ContattoRichiestaId] [int] NULL,
[RichiestaAccettazioneId] [int] NOT NULL,
[Value] [bit] NOT NULL,
[DataInserimento] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiAccettazioniStorico] ADD CONSTRAINT [PK_ContattiAccettazioniStorico] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiAccettazioniStorico] ADD CONSTRAINT [FK_ContattiAccettazioniStorico_Contatti] FOREIGN KEY ([ContattoId]) REFERENCES [Contatti].[Contatti] ([Id])
GO
ALTER TABLE [Contatti].[ContattiAccettazioniStorico] ADD CONSTRAINT [FK_ContattiAccettazioniStorico_ContattiRichieste] FOREIGN KEY ([ContattoRichiestaId]) REFERENCES [Contatti].[ContattiRichieste] ([Id])
GO
ALTER TABLE [Contatti].[ContattiAccettazioniStorico] ADD CONSTRAINT [FK_ContattiAccettazioniStorico_RichiesteAccettazioni] FOREIGN KEY ([RichiestaAccettazioneId]) REFERENCES [Contatti].[RichiesteAccettazioni] ([Id])
GO

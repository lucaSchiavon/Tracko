CREATE TABLE [Contatti].[ContattiRichieste_Tags_Rel]
(
[ContattoRichiestaId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[DataInserimento] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiRichieste_Tags_Rel] ADD CONSTRAINT [PK_ContattiRichieste_Tags_Rel] PRIMARY KEY CLUSTERED  ([ContattoRichiestaId], [TagId]) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[ContattiRichieste_Tags_Rel] ADD CONSTRAINT [FK_ContattiRichieste_Tags_Rel_ContattiRichieste] FOREIGN KEY ([ContattoRichiestaId]) REFERENCES [Contatti].[ContattiRichieste] ([Id])
GO
ALTER TABLE [Contatti].[ContattiRichieste_Tags_Rel] ADD CONSTRAINT [FK_ContattiRichieste_Tags_Rel_Tags] FOREIGN KEY ([TagId]) REFERENCES [Contatti].[Tags] ([Id])
GO

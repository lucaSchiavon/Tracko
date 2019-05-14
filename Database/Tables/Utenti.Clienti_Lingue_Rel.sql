CREATE TABLE [Utenti].[Clienti_Lingue_Rel]
(
[ClienteId] [int] NOT NULL,
[LinguaId] [int] NOT NULL,
[IsDefault] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Clienti_Lingue_Rel] ADD CONSTRAINT [PK_Clienti_Lingue_Rel] PRIMARY KEY CLUSTERED  ([ClienteId], [LinguaId]) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Clienti_Lingue_Rel] ADD CONSTRAINT [FK_Clienti_Lingue_Rel_Clienti] FOREIGN KEY ([ClienteId]) REFERENCES [Utenti].[Clienti] ([Id])
GO
ALTER TABLE [Utenti].[Clienti_Lingue_Rel] ADD CONSTRAINT [FK_Clienti_Lingue_Rel_Lingue] FOREIGN KEY ([LinguaId]) REFERENCES [General].[Lingue] ([Id])
GO

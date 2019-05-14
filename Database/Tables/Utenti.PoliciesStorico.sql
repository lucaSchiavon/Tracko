CREATE TABLE [Utenti].[PoliciesStorico]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PolicyId] [int] NOT NULL,
[TipologiaId] [int] NOT NULL,
[LinguaId] [int] NOT NULL,
[Testo] [nvarchar] (max) COLLATE Latin1_General_CI_AS NOT NULL,
[CreazioneData] [datetime] NOT NULL CONSTRAINT [DF_Table_1_UltimoAggiornamentoData] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[PoliciesStorico] ADD CONSTRAINT [PK_PoliciesStorico] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[PoliciesStorico] ADD CONSTRAINT [FK_PoliciesStorico_Lingue] FOREIGN KEY ([LinguaId]) REFERENCES [General].[Lingue] ([Id])
GO
ALTER TABLE [Utenti].[PoliciesStorico] ADD CONSTRAINT [FK_PoliciesStorico_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [Utenti].[Policies] ([Id])
GO

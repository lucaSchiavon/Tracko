CREATE TABLE [Utenti].[Policies]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SorgenteId] [int] NOT NULL,
[TipologiaId] [int] NOT NULL,
[LinguaId] [int] NOT NULL,
[Testo] [nvarchar] (max) COLLATE Latin1_General_CI_AS NOT NULL,
[UltimoAggiornamentoData] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DataCreazione] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Policies] ADD CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Policies] ADD CONSTRAINT [FK_Policies_Lingue] FOREIGN KEY ([LinguaId]) REFERENCES [General].[Lingue] ([Id])
GO
ALTER TABLE [Utenti].[Policies] ADD CONSTRAINT [FK_Policies_Sorgenti] FOREIGN KEY ([SorgenteId]) REFERENCES [Utenti].[Sorgenti] ([Id])
GO

CREATE TABLE [General].[VariabiliTraduzioni]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Nome] [nvarchar] (300) COLLATE Latin1_General_CI_AS NOT NULL,
[LinguaId] [int] NOT NULL,
[Testo] [nvarchar] (max) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [General].[VariabiliTraduzioni] ADD CONSTRAINT [PK_VariabiliTraduzioni] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [General].[VariabiliTraduzioni] ADD CONSTRAINT [FK_VariabiliTraduzioni_Lingue] FOREIGN KEY ([LinguaId]) REFERENCES [General].[Lingue] ([Id])
GO

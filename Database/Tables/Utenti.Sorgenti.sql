CREATE TABLE [Utenti].[Sorgenti]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ClienteId] [int] NOT NULL,
[Nome] [nvarchar] (300) COLLATE Latin1_General_CI_AS NOT NULL,
[SystemName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
[SettingMask] [smallint] NOT NULL CONSTRAINT [DF_Sorgenti_SettingMask] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Sorgenti] ADD CONSTRAINT [PK_Sorgenti] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Sorgenti] ADD CONSTRAINT [FK_Sorgenti_Clienti] FOREIGN KEY ([ClienteId]) REFERENCES [Utenti].[Clienti] ([Id])
GO

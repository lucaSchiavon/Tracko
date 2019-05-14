CREATE TABLE [Utenti].[Clienti]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Nome] [nvarchar] (300) COLLATE Latin1_General_CI_AS NOT NULL,
[APIKey] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NOT NULL,
[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Clienti] ADD CONSTRAINT [PK_Clienti] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

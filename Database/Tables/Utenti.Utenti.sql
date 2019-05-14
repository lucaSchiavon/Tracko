CREATE TABLE [Utenti].[Utenti]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [nvarchar] (128) COLLATE Latin1_General_CI_AS NOT NULL,
[ClienteId] [int] NULL,
[Nome] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[Cognome] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[CreateDate] [datetime] NOT NULL,
[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Utenti] ADD CONSTRAINT [PK_Utenti] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [Utenti].[Utenti] ADD CONSTRAINT [FK_Utenti_AspNetUsers1] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [Utenti].[Utenti] ADD CONSTRAINT [FK_Utenti_Clienti] FOREIGN KEY ([ClienteId]) REFERENCES [Utenti].[Clienti] ([Id])
GO

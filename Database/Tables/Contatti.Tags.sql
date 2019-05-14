CREATE TABLE [Contatti].[Tags]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Nome] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Contatti].[Tags] ADD CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

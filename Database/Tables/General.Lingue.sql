CREATE TABLE [General].[Lingue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Nome] [nvarchar] (300) COLLATE Latin1_General_CI_AS NOT NULL,
[Codice] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[Codifica] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[IsEnable] [bit] NOT NULL,
[IsDelete] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [General].[Lingue] ADD CONSTRAINT [PK_Lingue] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

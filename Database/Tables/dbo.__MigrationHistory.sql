CREATE TABLE [dbo].[__MigrationHistory]
(
[MigrationId] [nvarchar] (150) COLLATE Latin1_General_CI_AS NOT NULL,
[ContextKey] [nvarchar] (300) COLLATE Latin1_General_CI_AS NOT NULL,
[Model] [varbinary] (max) NOT NULL,
[ProductVersion] [nvarchar] (32) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[__MigrationHistory] ADD CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED  ([MigrationId], [ContextKey]) ON [PRIMARY]
GO

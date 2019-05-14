CREATE TABLE [dbo].[AspNetRoles]
(
[Id] [nvarchar] (128) COLLATE Latin1_General_CI_AS NOT NULL,
[Name] [nvarchar] (256) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoles] ADD CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([Name]) ON [PRIMARY]
GO

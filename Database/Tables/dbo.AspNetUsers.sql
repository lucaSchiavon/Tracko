CREATE TABLE [dbo].[AspNetUsers]
(
[Id] [nvarchar] (128) COLLATE Latin1_General_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE Latin1_General_CI_AS NULL,
[EmailConfirmed] [bit] NOT NULL,
[PasswordHash] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[SecurityStamp] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[PhoneNumber] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[PhoneNumberConfirmed] [bit] NOT NULL,
[TwoFactorEnabled] [bit] NOT NULL,
[LockoutEndDateUtc] [datetime] NULL,
[LockoutEnabled] [bit] NOT NULL,
[AccessFailedCount] [int] NOT NULL,
[UserName] [nvarchar] (256) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([UserName]) ON [PRIMARY]
GO

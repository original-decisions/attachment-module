begin tran 
IF schema_id('attach') IS NULL
    EXECUTE('CREATE SCHEMA [attach]')

	IF schema_id('security') IS NULL
    EXECUTE('CREATE SCHEMA [security]')
IF schema_id('AspNet') IS NULL
    EXECUTE('CREATE SCHEMA [AspNet]')

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[security].[AttachmentPermissions]') AND type in (N'U'))
	begin
		CREATE TABLE [security].[AttachmentPermissions] (
			[UserId] [int] NOT NULL,
			[AttachmentId] [int] NOT NULL,
			[PermissionId] [int] NOT NULL,
			[IsOwner] [bit] NOT NULL,
			CONSTRAINT [PK_security.AttachmentPermissions] PRIMARY KEY ([UserId], [AttachmentId], [PermissionId])
		)
		
	CREATE INDEX [IX_UserId] ON [security].[AttachmentPermissions]([UserId])
	CREATE INDEX [IX_AttachmentId] ON [security].[AttachmentPermissions]([AttachmentId])
	CREATE INDEX [IX_PermissionId] ON [security].[AttachmentPermissions]([PermissionId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[Attachments]') AND type in (N'U'))
	begin
CREATE TABLE [attach].[Attachments] (
    [Id] [int] NOT NULL IDENTITY,
    [AttachmentTypeId] [int] NOT NULL,
    [ExtensionId] [int] NOT NULL,
    [Content] [varbinary](max),
    [IsShared] [bit] NOT NULL,
    [PublicUri] [nvarchar](max),
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.Attachments] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttachmentTypeId] ON [attach].[Attachments]([AttachmentTypeId])
CREATE INDEX [IX_ExtensionId] ON [attach].[Attachments]([ExtensionId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[AttachmentTypes]') AND type in (N'U'))
	begin
CREATE TABLE [attach].[AttachmentTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.AttachmentTypes] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[Extensions]') AND type in (N'U'))
	begin
CREATE TABLE [attach].[Extensions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.Extensions] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[security].[Permissions]') AND type in (N'U'))
	begin
CREATE TABLE [security].[Permissions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_security.Permissions] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Users]') AND type in (N'U'))
	begin
CREATE TABLE [AspNet].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [ProfilePicturePath] [nvarchar](max),
    [FirstName] [nvarchar](max),
    [LastName] [nvarchar](max),
    [Patronymic] [nvarchar](max),
    [DateUpdated] [datetime],
    [LastActivityDate] [datetime],
    [LastLogin] [datetime],
    [RemindInDays] [int] NOT NULL,
    [DateRegistration] [datetime] NOT NULL,
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](max),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Users] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [UserNameIndex] ON [AspNet].[Users]([UserName])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserClaims]') AND type in (N'U'))
	begin
	CREATE TABLE [AspNet].[UserClaims] (
		[Id] [int] NOT NULL IDENTITY,
		[UserId] [int] NOT NULL,
		[ClaimType] [nvarchar](max),
		[ClaimValue] [nvarchar](max),
		CONSTRAINT [PK_AspNet.UserClaims] PRIMARY KEY ([Id])
	)

CREATE INDEX [IX_UserId] ON [AspNet].[UserClaims]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserLogins]') AND type in (N'U'))
	begin
CREATE TABLE [AspNet].[UserLogins] (
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserLogins]([UserId])
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserRoles]') AND type in (N'U'))
	begin
CREATE TABLE [AspNet].[UserRoles] (
    [UserId] [int] NOT NULL,
    [RoleId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [AspNet].[UserRoles]([RoleId])
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[AttachmentTypeExtensions]') AND type in (N'U'))
	begin
CREATE TABLE [attach].[AttachmentTypeExtensions] (
    [AttachmentTypeId] [int] NOT NULL,
    [ExtensionId] [int] NOT NULL,
    CONSTRAINT [PK_attach.AttachmentTypeExtensions] PRIMARY KEY ([AttachmentTypeId], [ExtensionId])
)
CREATE INDEX [IX_AttachmentTypeId] ON [attach].[AttachmentTypeExtensions]([AttachmentTypeId])
CREATE INDEX [IX_ExtensionId] ON [attach].[AttachmentTypeExtensions]([ExtensionId])
end 

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[security].[AttachmentTypePermissions]') AND type in (N'U'))
	begin
CREATE TABLE [security].[AttachmentTypePermissions] (
    [RoleId] [int] NOT NULL,
    [AttachmentTypeId] [int] NOT NULL,
    [PermissionId] [int] NOT NULL,
    CONSTRAINT [PK_security.AttachmentTypePermissions] PRIMARY KEY ([RoleId], [AttachmentTypeId], [PermissionId])
)
CREATE INDEX [IX_RoleId] ON [security].[AttachmentTypePermissions]([RoleId])
CREATE INDEX [IX_AttachmentTypeId] ON [security].[AttachmentTypePermissions]([AttachmentTypeId])
CREATE INDEX [IX_PermissionId] ON [security].[AttachmentTypePermissions]([PermissionId])
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Roles]') AND type in (N'U'))
	begin
CREATE TABLE [AspNet].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [InRoleId] [int],
    [Scope] [nvarchar](max),
    [Name] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Roles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InRoleId] ON [AspNet].[Roles]([InRoleId])
CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNet].[Roles]([Name])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentPermissions_attach.Attachments_AttachmentId')
begin
	ALTER TABLE [security].[AttachmentPermissions] ADD CONSTRAINT [FK_security.AttachmentPermissions_attach.Attachments_AttachmentId] FOREIGN KEY ([AttachmentId]) REFERENCES [attach].[Attachments] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentPermissions_security.Permissions_PermissionId')
begin
	ALTER TABLE [security].[AttachmentPermissions] ADD CONSTRAINT [FK_security.AttachmentPermissions_security.Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [security].[Permissions] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentPermissions_AspNet.Users_UserId')
begin
	ALTER TABLE [security].[AttachmentPermissions] ADD CONSTRAINT [FK_security.AttachmentPermissions_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.Attachments_attach.AttachmentTypes_AttachmentTypeId')
begin
	ALTER TABLE [attach].[Attachments] ADD CONSTRAINT [FK_attach.Attachments_attach.AttachmentTypes_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [attach].[AttachmentTypes] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.Attachments_attach.Extensions_ExtensionId')
begin
	ALTER TABLE [attach].[Attachments] ADD CONSTRAINT [FK_attach.Attachments_attach.Extensions_ExtensionId] FOREIGN KEY ([ExtensionId]) REFERENCES [attach].[Extensions] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserClaims_AspNet.Users_UserId')
begin
	ALTER TABLE [AspNet].[UserClaims] ADD CONSTRAINT [FK_AspNet.UserClaims_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserLogins_AspNet.Users_UserId')
begin
	ALTER TABLE [AspNet].[UserLogins] ADD CONSTRAINT [FK_AspNet.UserLogins_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Users_UserId')
begin
	ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Roles_RoleId')
begin
	ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNet].[Roles] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.AttachmentTypeExtensions_attach.AttachmentTypes_AttachmentTypeId]')
begin
	ALTER TABLE [attach].[AttachmentTypeExtensions] ADD CONSTRAINT [FK_attach.AttachmentTypeExtensions_attach.AttachmentTypes_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [attach].[AttachmentTypes] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.AttachmentTypeExtensions_attach.Extensions_ExtensionId')
begin
	ALTER TABLE [attach].[AttachmentTypeExtensions] ADD CONSTRAINT [FK_attach.AttachmentTypeExtensions_attach.Extensions_ExtensionId] FOREIGN KEY ([ExtensionId]) REFERENCES [attach].[Extensions] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentTypePermissions_attach.AttachmentTypes_AttachmentTypeId')
begin
	ALTER TABLE [security].[AttachmentTypePermissions] ADD CONSTRAINT [FK_security.AttachmentTypePermissions_attach.AttachmentTypes_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [attach].[AttachmentTypes] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentTypePermissions_security.Permissions_PermissionId')
begin
	ALTER TABLE [security].[AttachmentTypePermissions] ADD CONSTRAINT [FK_security.AttachmentTypePermissions_security.Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [security].[Permissions] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_security.AttachmentTypePermissions_AspNet.Roles_RoleId')
begin
	ALTER TABLE [security].[AttachmentTypePermissions] ADD CONSTRAINT [FK_security.AttachmentTypePermissions_AspNet.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNet].[Roles] ([Id])
end

if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.Roles_AspNet.Roles_InRoleId')
begin
	ALTER TABLE [AspNet].[Roles] ADD CONSTRAINT [FK_AspNet.Roles_AspNet.Roles_InRoleId] FOREIGN KEY ([InRoleId]) REFERENCES [AspNet].[Roles] ([Id])
end

commit tran

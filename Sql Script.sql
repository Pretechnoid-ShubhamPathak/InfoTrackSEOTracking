--Connection String is present at location "Backend/appsettings.Development.json"

CREATE DATABASE itSeoTrack;

CREATE TABLE [itSeoTrack].[dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[EmailId] [varchar](255) NULL,
	[UserRole] [varchar](50) NULL,
	PRIMARY KEY (Id),
	)

Insert into [itSeoTrack].[dbo].[Users] values ('Admin','Administrator','shubhampathak2896@gmail.com','admin',
'$2a$11$6AL1Sm55/C4sZNSvuuOrjeeuhG3ZhHXD8wCPVRZJWWUnhKeWfaKdC','admin')

CREATE TABLE [itSeoTrack].[dbo].[SearchHistoryManager](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Keywords] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[Ranking] [nvarchar](max) NOT NULL,
	[SearchEngine] [nvarchar](max) NOT NULL,
	[SearchedAt] [datetime] NOT NULL,
	PRIMARY KEY (Id),
	)


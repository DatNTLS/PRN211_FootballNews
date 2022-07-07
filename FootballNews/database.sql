USE [master]
GO
--DROP DATABASE [FootballNews]
CREATE DATABASE [FootballNews]
GO

USE [FootballNews]
GO

CREATE TABLE [Role](
	RoleId		int	NOT NULL identity(1,1) PRIMARY KEY,
	RoleName	nvarchar(max) NULL,	
)

CREATE TABLE [Category](
	CategoryId		int	NOT NULL identity(1,1) PRIMARY KEY,
	CategoryName	nvarchar(max) NULL,	
)

CREATE TABLE [User](
	UserId				int				NOT NULL identity(1,1) PRIMARY KEY,
	UserName			nvarchar(max)	NULL,
	Email				nvarchar(max)	NULL,
	[Password]			nvarchar(max)   NULL,
	Avatar				nvarchar(max)	NULL,
	RoleId				int				NULL,
	Otp					nvarchar(max)	NULL,
	[Status]			bit				NULL,
	FOREIGN KEY (roleId) REFERENCES dbo.[Role](roleId),
)

CREATE TABLE [News](
	NewsId				int				NOT NULL identity(1,1) PRIMARY KEY,
	AuthorId            int				NULL,
	Title				nvarchar(max)	NULL,
	ShortDescription    nvarchar(max)	NULL,
	Thumbnail			nvarchar(max)	NULL,
	CategoryId			int				NULL,
	DatePublished		datetime        NULL,
	[Status]			bit				NULL,
	FOREIGN KEY (AuthorId)	    REFERENCES [User]([UserId]),
	FOREIGN KEY (CategoryId)	REFERENCES [Category](CategoryId),
)

CREATE TABLE [Image](
	ImageId				int				NOT NULL identity(1,1) PRIMARY KEY,
	ImageUrl		    nvarchar(max)	NULL,
	NewsId			    int				NULL,
	FOREIGN KEY (NewsId)	    REFERENCES [News](NewsId),
)

CREATE TABLE [Content](
	ContentId		    int				NOT NULL identity(1,1) PRIMARY KEY,
	Content		        nvarchar(max)	NULL,
	ImageId			    int				NULL,
	FOREIGN KEY (ImageId)	    REFERENCES [Image]([ImageId]),
)

CREATE TABLE [Comment](
	CommentId			int				NOT NULL identity(1,1) PRIMARY KEY,
	UserId				int				NULL,
	NewsId				int				NULL,
	Content				nvarchar(max)	NULL,
	[Time]				datetime		NULL,
	FOREIGN KEY (UserId)	    REFERENCES [User]([UserId]),
	FOREIGN KEY (NewsId)		REFERENCES [News](NewsId),
)





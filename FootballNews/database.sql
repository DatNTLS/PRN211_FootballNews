USE [master]
GO
--DROP DATABASE [FootballNews]
CREATE DATABASE [FootballNews]
GO

USE [FootballNews]
GO

CREATE TABLE [Role](
	RoleId		int	NOT NULL identity(1,1) PRIMARY KEY,
	RoleName	nvarchar(256) NULL,	
)

CREATE TABLE [Category](
	CategoryId		int	NOT NULL identity(1,1) PRIMARY KEY,
	CategoryName	nvarchar(256) NULL,	
)

CREATE TABLE [User](
	UserId				int				NOT NULL identity(1,1) PRIMARY KEY,
	UserName			nvarchar(256)	NULL,
	Email				nvarchar(256)	NULL,
	[Password]			nvarchar(256)   NULL,
	Avatar				ntext			NULL,
	RoleId				int				NULL,
	Otp					nvarchar(256)	NULL,
	[Status]			bit				NULL,
	FOREIGN KEY (roleId) REFERENCES dbo.[Role](roleId),
)

CREATE TABLE [News](
	NewsId				int				NOT NULL identity(1,1) PRIMARY KEY,
	AuthorId            int				NULL,
	Title				ntext			NULL,
	ShortDescription    ntext			NULL,
	Thumbnail			ntext			NULL,
	CategoryId			int				NULL,
	DatePublished		datetime        NULL,
	[Status]			bit				NULL,
	FOREIGN KEY (AuthorId)	    REFERENCES [User]([UserId]),
	FOREIGN KEY (CategoryId)	REFERENCES [Category](CategoryId),
)

CREATE TABLE [Image](
	ImageId				int				NOT NULL identity(1,1) PRIMARY KEY,
	ImageUrl		    ntext			NULL,
	NewsId			    int				NULL,
	FOREIGN KEY (NewsId)	    REFERENCES [News](NewsId),
)

CREATE TABLE [Content](
	ContentId		    int				NOT NULL identity(1,1) PRIMARY KEY,
	Content		        ntext			NULL,
	ImageId			    int				NULL,
	FOREIGN KEY (ImageId)	    REFERENCES [Image]([ImageId]),
)

CREATE TABLE [Comment](
	CommentId			int				NOT NULL identity(1,1) PRIMARY KEY,
	UserId				int				NULL,
	NewsId				int				NULL,
	Content				ntext			NULL,
	FOREIGN KEY (UserId)	    REFERENCES [User]([UserId]),
	FOREIGN KEY (NewsId)		REFERENCES [News](NewsId),
)




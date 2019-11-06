CREATE TABLE [dbo].[VideoTimeStampedTags] (
	[VideoTimeStampedTagID] [int] NOT NULL IDENTITY,
	[CultureWarsVideoID] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[CultureWarsTagID] [int] NOT NULL,
	CONSTRAINT [PK_dbo.VideoTimeStampedTags]
		PRIMARY KEY ([VideoTimeStampedTagID])
)

CREATE TABLE [dbo].[CultureWarsAuthors] (
	[CultureWarsAuthorID] [int] NOT NULL IDENTITY,
	[AuthorEmail] [varchar](300) NULL,
	[AuthorLogin] [varchar](300) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
    [MiddleName] [nvarchar](200) NULL,
    [LastName] [nvarchar](200) NOT NULL,
    [FullName] [nvarchar](600) NOT NULL,
	CONSTRAINT [PK_dbo.CultureWarsAuthors]
		PRIMARY KEY ([CultureWarsAuthorID])
)

CREATE TABLE [dbo].[CultureWarsCategories] (
	[CultureWarsCategoryID] [int] NOT NULL IDENTITY,
	[CategoryName] [varchar](300) NULL,
	[CategoryFriendlyName] [varchar](300) NOT NULL,
	[CategoryParent] [nvarchar](300) NULL,
	CONSTRAINT [PK_dbo.CultureWarsCategories]
		PRIMARY KEY ([CultureWarsCategoryID])
)

CREATE TABLE [dbo].[CultureWarsTags] (
	[CultureWarsTagID] [int] NOT NULL IDENTITY,
	[TagName] [varchar](300) NULL,
	[TagFriendlyName] [varchar](300) NOT NULL,
	[HtmlEncodedTagName] [nvarchar](300) NOT NULL,
	CONSTRAINT [PK_dbo.CultureWarsTags]
		PRIMARY KEY ([CultureWarsTagID])
)

CREATE TABLE [dbo].[CultureWarsVideos] (
	[CultureWarsVideoID] [int] NOT NULL IDENTITY,
	[VideoSourceUrl] [varchar](500) NOT NULL,
	CONSTRAINT [PK_dbo.CultureWarsVideos]
		PRIMARY KEY ([CultureWarsVideoID])
)

CREATE TABLE [dbo].[CultureWarsTags] (
	[CultureWarsTagID] [int] NOT NULL IDENTITY,
	[TagName] [varchar](300) NULL,
	[TagFriendlyName] [varchar](300) NOT NULL,
	[HtmlEncodedTagName] [nvarchar](300) NOT NULL,
	CONSTRAINT [PK_dbo.CultureWarsTags]
		PRIMARY KEY ([CultureWarsTagID])
)
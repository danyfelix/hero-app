USE [DataBaseFirst]
GO

DROP TABLE IF EXISTS [dbo].[RatingHistorial]
DROP TABLE IF EXISTS [dbo].[Hero]
DROP TABLE IF EXISTS [dbo].[ExceptionLog]
GO

/****** Object:  Table [dbo].[Hero]    Script Date: 6/13/2020 7:05:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Hero](
	[HeroId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Age] [int] NULL,
	[City] [varchar](50) NULL,
	[Rating] [int] NULL,
	[Picture] [varchar](50) NULL,
 CONSTRAINT [PK_Hero] PRIMARY KEY CLUSTERED 
(
	[HeroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[RatingHistorial]    Script Date: 6/13/2020 7:07:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RatingHistorial](
	[RatingHistorialId] [int] NOT NULL,
	[HeroId] [int] NOT NULL,
	[Rating] [int] NULL,
	[RaterName] [varchar](50) NULL,
 CONSTRAINT [PK_RatingHistorial] PRIMARY KEY CLUSTERED 
(
	[RatingHistorialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RatingHistorial]  WITH CHECK ADD FOREIGN KEY([HeroId])
REFERENCES [dbo].[Hero] ([HeroId])
GO

USE [DataBaseFirst]
GO

/****** Object:  Table [dbo].[ExceptionLog]    Script Date: 6/14/2020 2:03:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExceptionLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Exception] [varchar](1000) NULL,
	[InnerException] [varchar](1000) NULL,
	[StackTrace] [varchar](5000) NULL,
 CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



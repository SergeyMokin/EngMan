﻿-- admin account
-- login: admin
-- password: DefaultAdmin9856
USE [master]
GO
CREATE DATABASE [EngMan]
GO
ALTER DATABASE [EngMan] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EngMan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EngMan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EngMan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EngMan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EngMan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EngMan] SET ARITHABORT OFF 
GO
ALTER DATABASE [EngMan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EngMan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EngMan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EngMan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EngMan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EngMan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EngMan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EngMan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EngMan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EngMan] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EngMan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EngMan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EngMan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EngMan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EngMan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EngMan] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EngMan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EngMan] SET RECOVERY FULL 
GO
ALTER DATABASE [EngMan] SET  MULTI_USER 
GO
ALTER DATABASE [EngMan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EngMan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EngMan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EngMan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EngMan] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EngMan', N'ON'
GO
ALTER DATABASE [EngMan] SET QUERY_STORE = OFF
GO
USE [EngMan]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [EngMan]
GO
/****** Object:  Table [dbo].[GuessesTheImages]    Script Date: 2/20/2018 9:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuessesTheImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WordId] [int] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.GuessesTheImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[BeneficiaryId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Time] [datetime] NOT NULL,
	[CheckReadMes] [bit] NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RuleModels]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuleModels](
	[RuleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.RuleModels] PRIMARY KEY CLUSTERED 
(
	[RuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SentenceTasks]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SentenceTasks](
	[SentenceTaskId] [int] IDENTITY(1,1) NOT NULL,
	[Sentence] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Translate] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SentenceTasks] PRIMARY KEY CLUSTERED 
(
	[SentenceTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserWords]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserWords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[WordId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Words]    Script Date: 2/20/2018 9:38:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Words](
	[WordId] [int] IDENTITY(1,1) NOT NULL,
	[Original] [nvarchar](max) NULL,
	[Translate] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Transcription] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Words] PRIMARY KEY CLUSTERED 
(
	[WordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (1, N'Admin', N'Creator', N'admin', N'APyr7kUR7BnDfYPpo7o+XKUAKdf9i9EnG0+JautroWN4WP/Vi2e4dWZP0nXN9ttxig==', N'admin')

SET IDENTITY_INSERT [dbo].[Users] OFF

ALTER TABLE [dbo].[GuessesTheImages]  WITH CHECK ADD FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordId]) ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([BeneficiaryId])
REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordId]) ON UPDATE CASCADE ON DELETE CASCADE
GO
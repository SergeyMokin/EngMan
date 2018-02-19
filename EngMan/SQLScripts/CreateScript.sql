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
/****** Object:  Table [dbo].[GuessesTheImages]    Script Date: 2/19/2018 8:47:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuessesTheImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Word] [nvarchar](max) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.GuessesTheImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2/19/2018 8:47:38 PM ******/
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
/****** Object:  Table [dbo].[RuleModels]    Script Date: 2/19/2018 8:47:38 PM ******/
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
/****** Object:  Table [dbo].[SentenceTasks]    Script Date: 2/19/2018 8:47:38 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2/19/2018 8:47:38 PM ******/
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
/****** Object:  Table [dbo].[UserWords]    Script Date: 2/19/2018 8:47:39 PM ******/
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
/****** Object:  Table [dbo].[Words]    Script Date: 2/19/2018 8:47:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Words](
	[WordId] [int] IDENTITY(1,1) NOT NULL,
	[Original] [nvarchar](max) NULL,
	[Translate] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Words] PRIMARY KEY CLUSTERED 
(
	[WordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GuessesTheImages] ON 

INSERT [dbo].[GuessesTheImages] ([Id], [Word], [Path]) VALUES (4, N'dog', N'https://www.what-dog.net/Images/faces2/scroll001.jpg')
INSERT [dbo].[GuessesTheImages] ([Id], [Word], [Path]) VALUES (5, N'cat', N'https://www.petmd.com/sites/default/files/petmd-cat-happy-10.jpg')
INSERT [dbo].[GuessesTheImages] ([Id], [Word], [Path]) VALUES (7, N'human', N'http://localhost:58099/uploads/63653798768.4869logo.png')
SET IDENTITY_INSERT [dbo].[GuessesTheImages] OFF
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (13, 1, 2, N'asdasda', CAST(N'2018-02-08T10:11:58.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (14, 1, 2, N'фывафыавыфав', CAST(N'2018-02-08T11:39:23.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (15, 1, 2, N'йцукцйукйцук', CAST(N'2018-02-08T11:39:25.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (16, 2, 1, N'фывфывф', CAST(N'2018-02-08T11:45:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (17, 2, 1, N'фывафываф', CAST(N'2018-02-08T11:45:57.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (18, 2, 1, N'йцукйцук', CAST(N'2018-02-08T11:46:01.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (19, 2, 1, N'фывафывафыва', CAST(N'2018-02-08T11:48:14.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (20, 1, 2, N'dafasdf', CAST(N'2018-02-09T19:20:49.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (21, 1, 2, N'adsfsafd', CAST(N'2018-02-09T19:22:45.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (22, 1, 2, N'are you sure?', CAST(N'2018-02-09T19:22:58.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (28, 3, 1, N'hello dear', CAST(N'2018-02-12T20:40:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (29, 3, 2, N'hello friend', CAST(N'2018-02-12T20:40:18.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (30, 3, 1, N'hello', CAST(N'2018-02-12T20:42:14.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (31, 3, 1, N'hello', CAST(N'2018-02-13T09:43:41.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (32, 1, 3, N'hello', CAST(N'2018-02-13T10:11:19.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (33, 3, 1, N'hello', CAST(N'2018-02-13T10:15:45.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (34, 1, 3, N'asdfasdf', CAST(N'2018-02-13T10:49:25.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (35, 2, 3, N'asdsafd', CAST(N'2018-02-13T10:49:35.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (36, 3, 1, N'asdfasdf', CAST(N'2018-02-13T11:00:11.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (37, 1, 3, N'hello', CAST(N'2018-02-14T13:26:47.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (38, 3, 1, N'hello', CAST(N'2018-02-14T13:27:04.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (39, 1, 3, N'hello', CAST(N'2018-02-14T13:27:43.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (40, 3, 1, N'hello', CAST(N'2018-02-14T13:27:55.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (41, 3, 4, N'hello', CAST(N'2018-02-14T18:42:01.000' AS DateTime), 0)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (42, 3, 2, N'asdfasdf', CAST(N'2018-02-14T19:10:01.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (43, 3, 2, N'jjjjj', CAST(N'2018-02-14T19:10:09.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (44, 3, 2, N'jjjjj', CAST(N'2018-02-14T19:10:14.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (45, 3, 2, N'jjjj', CAST(N'2018-02-14T19:10:18.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (46, 3, 2, N'
', CAST(N'2018-02-16T17:43:48.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (49, 3, 1, N'Gelle
', CAST(N'2018-02-16T17:45:09.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (52, 1, 3, N'gelle', CAST(N'2018-02-16T17:46:19.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (53, 3, 1, N'sadfsafdsafsafd', CAST(N'2018-02-16T17:47:53.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (54, 2, 1, N'qwerwqerqwr', CAST(N'2018-02-16T17:48:08.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (55, 1, 3, N'adsfsadfsafdafa', CAST(N'2018-02-16T17:49:39.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (56, 1, 3, N'asdfdfasdf', CAST(N'2018-02-16T17:49:40.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (57, 1, 3, N'adsfasfdsafd', CAST(N'2018-02-16T17:49:41.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (58, 2, 3, N'sdafsafd', CAST(N'2018-02-16T20:11:33.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (59, 2, 3, N'asdfasfdsadf', CAST(N'2018-02-16T20:12:48.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (60, 3, 2, N'asfedsadsafd', CAST(N'2018-02-16T20:15:22.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (61, 3, 1, N'asdfsadf', CAST(N'2018-02-16T20:16:06.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (62, 2, 3, N'dsafsafds', CAST(N'2018-02-16T20:19:02.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (63, 2, 3, N'sadfasfda', CAST(N'2018-02-16T20:19:23.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (64, 3, 2, N'asdfsafd', CAST(N'2018-02-16T20:20:27.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (65, 3, 2, N'asdfasdfasfd', CAST(N'2018-02-16T20:21:08.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (66, 3, 1, N'asdfsaf', CAST(N'2018-02-16T20:21:44.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (67, 3, 2, N'asdfsadfasfd', CAST(N'2018-02-16T20:22:11.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (68, 3, 2, N'asdfsafadfs', CAST(N'2018-02-16T20:23:14.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (69, 2, 3, N'sadfsadfsafda', CAST(N'2018-02-16T20:23:27.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (70, 2, 3, N'hello dear', CAST(N'2018-02-16T20:23:35.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (71, 2, 3, N'asdsafdsa', CAST(N'2018-02-16T20:23:54.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (72, 2, 3, N'qweqwerwqer', CAST(N'2018-02-16T20:23:57.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (73, 3, 2, N'wqewqerqwer', CAST(N'2018-02-16T20:24:12.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (74, 3, 2, N'asdfsadfsa', CAST(N'2018-02-16T20:24:18.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (75, 3, 1, N'adsfsafd', CAST(N'2018-02-16T20:24:46.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (76, 3, 2, N'qwerqwer', CAST(N'2018-02-16T20:25:12.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (77, 3, 1, N'sadfsafdsa', CAST(N'2018-02-16T20:25:46.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (78, 3, 2, N'asdfsafdsafdasf', CAST(N'2018-02-16T20:26:57.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (79, 2, 3, N'qwerqwerqwerqwer', CAST(N'2018-02-16T20:28:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (80, 3, 2, N'qwerqwerqewrqr', CAST(N'2018-02-16T20:28:56.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (81, 3, 2, N'adsfasfd', CAST(N'2018-02-16T20:33:52.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (82, 3, 1, N'adssadf', CAST(N'2018-02-16T20:34:15.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (83, 3, 1, N'asdfasfd', CAST(N'2018-02-16T20:35:54.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (84, 3, 1, N'qweqwe', CAST(N'2018-02-16T20:35:57.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (85, 3, 1, N'sd', CAST(N'2018-02-16T20:35:59.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (86, 2, 3, N'dsafsadfsafd', CAST(N'2018-02-16T20:36:16.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (87, 3, 1, N'adsfsadf', CAST(N'2018-02-16T20:43:47.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (88, 3, 1, N'asdfsafd', CAST(N'2018-02-16T20:45:28.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (89, 3, 4, N'asd', CAST(N'2018-02-16T20:51:18.000' AS DateTime), 0)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (90, 3, 2, N'adfsafd', CAST(N'2018-02-16T20:51:49.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (91, 3, 1, N'sadfsadf', CAST(N'2018-02-16T20:55:11.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (92, 3, 2, N'asdfsadfsa', CAST(N'2018-02-16T20:55:28.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (93, 3, 1, N'dsafdf', CAST(N'2018-02-16T20:57:23.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (94, 3, 1, N'asdfsadf', CAST(N'2018-02-16T20:57:44.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (95, 3, 1, N'qwerqwerwqerqerwqre', CAST(N'2018-02-16T21:01:05.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (96, 2, 3, N'hello', CAST(N'2018-02-16T21:02:07.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (97, 3, 2, N'adsfsafdafd', CAST(N'2018-02-16T21:02:37.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (98, 2, 3, N'asdfasdfafd', CAST(N'2018-02-19T16:28:40.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (99, 2, 3, N'qwerqwerq', CAST(N'2018-02-19T16:31:45.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (100, 2, 3, N'asdfasdf', CAST(N'2018-02-19T16:32:03.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (101, 2, 3, N'qwerqwer', CAST(N'2018-02-19T16:34:26.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (102, 3, 2, N'asdfaqwerqw', CAST(N'2018-02-19T16:35:42.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (103, 2, 3, N'qwerqwer', CAST(N'2018-02-19T16:36:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (104, 2, 3, N'adwrqyu', CAST(N'2018-02-19T16:36:57.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (105, 3, 2, N'qwer', CAST(N'2018-02-19T16:37:39.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (106, 2, 3, N'qwer', CAST(N'2018-02-19T16:39:52.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (107, 2, 3, N'dasfa', CAST(N'2018-02-19T16:40:12.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (108, 2, 3, N'asdfa', CAST(N'2018-02-19T16:42:56.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (109, 2, 3, N'qwerqwer', CAST(N'2018-02-19T16:43:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (111, 3, 2, N'qwerqwer', CAST(N'2018-02-19T16:44:34.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (112, 2, 3, N'adsfasdfasd', CAST(N'2018-02-19T16:44:42.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (113, 2, 3, N'asdfasdfa', CAST(N'2018-02-19T16:47:00.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (114, 3, 2, N'asdfasdf', CAST(N'2018-02-19T16:50:30.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (115, 2, 3, N'asdfasfdas', CAST(N'2018-02-19T16:51:32.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (116, 3, 2, N'adsfasdf', CAST(N'2018-02-19T16:52:46.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (117, 2, 3, N'asdfsadfa', CAST(N'2018-02-19T16:52:56.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (118, 2, 3, N'asdfasdfsaf', CAST(N'2018-02-19T17:03:53.000' AS DateTime), 1)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [BeneficiaryId], [Text], [Time], [CheckReadMes]) VALUES (119, 3, 2, N'qwerwqer', CAST(N'2018-02-19T17:03:58.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Messages] OFF
SET IDENTITY_INSERT [dbo].[RuleModels] ON 

INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (8, N'Present Simple', N'<b>Утверждение</b>
Present Simple обозначает действия, которые происходят в настоящее время, но не привязаны к моменту речи.

⇒ Чтобы составить утвердительное предложение с местоимениями I/we/you/they или с существительными, необходимо:

1.Поставить на первое место I/we/you/they или существительное(-ые) во множественном числе;
2. Глагол;
3. Остальные слова.

⇒ Чтобы составить утвердительное предложение с местоимениями he/she/it или с существительным, необходимо:

1.Поставить на первое место he/she/it или существительное в единственном числе;
2. Глагол с окончанием -s, -es;
3. Остальные слова.

⇒ Обратите внимание на формы to be и to have в Present Simple.

Глагол to be – быть, существовать, являться, находиться. Его формы: am/is/are.
- I употребляется только с am;
- He/she/it или существительное в единственном числе употребляются с is;
- We/you/they или существительное во множественном числе употребляются с are.

Глагол to have – иметь, обладать. Его формы: have/has.
- I/we/you/they или существительное во множественном числе употребляются с have;
- He/she/it или существительное в единственном числе употребляются с has.

I know something. ⇒ Я знаю что-то.

You look fine. ⇒ Ты выглядишь прекрасно.

I prefer cash. ⇒ Я предпочитаю наличные.

You need help. ⇒ Вы нуждаетесь в помощи.

I love lasagna. ⇒ Я люблю лазанью.

I need inspiration. ⇒ Мне нужно вдохновение.

We do nothing. ⇒ Мы не делаем ничего.

I like pizza. ⇒ Мне нравится пицца.

I feel something. ⇒ Я чувствую что-то.

I like everything. ⇒ Мне нравится всё.

<b>Вопрос</b>
Вопрос в Present Simple образуется с помощью Do или Does. Рассмотрим образование вопроса с помощью Do.

1. Поставьте на первое место Do (если вопрос начинается с When/Where и т.д., то do ставим после них);
2. Потом I/we/you/they или существительное(-ые) во множественном числе;
3. Глагол (всегда без окончания -s; - es);
4. Остальные слова.

Чтобы задать вопрос с помощью Does, используйте следующий порядок:

1. Поставьте на первое место Does (если вопрос начинается с When/Where и т.д., то does ставим после них);
2. Потом he/she/it или существительное единственном числе;
3. Глагол (всегда без окончания -s; - es);
4. Остальные слова.

⇒ В вопросительных предложениях формы глагола to be и to have got ставятся перед местоимением(-ями) или существительным(-ми).

- Am употребляется перед I;
- Is употребляется перед he/she/it или перед существительным в единственном числе;
- Are употребляется перед we/you/they или перед существительным(-ми) во множественном числе.

- Has употребляется перед he/she/it или перед существительным в единственном числе.
- Have употребляется перед I/we/you/they или перед существительным(-ми) во множественном числе;
- Если вопрос начинается с Where/When/What и т.д., то после них ставим am/is/are или have/has, затем употребляется местоимение(-ия) или существительное(-ые) и все остальные слова.

Where does he sleep? ⇒ Где он спит?

How does she look? ⇒ Как она выглядит?

How does it work? ⇒ Как это работает?

How does it happen? ⇒ Как это происходит?

Why does it rain? ⇒ Почему идет дождь?

Where does she live? ⇒ Где она живёт?

How does it begin? ⇒ Как это начинается?

Where does she work? ⇒ Где она работает?

How does he look? ⇒ Как он выглядит?

Why does it happen? ⇒ Почему это происходит?

How does she know? ⇒ Откуда она знает?

Where does he work? ⇒ Где он работает?

Where does it end? ⇒ Где это заканчивается?

How does it sound? ⇒ Как это звучит?

Why does she stay? ⇒ Почему она остаётся?

Why do you lie? ⇒ Почему ты врёшь?

When do you begin? ⇒ Когда ты начинаешь?

Where do you sleep? ⇒ Где вы спите?

Why do you cry? ⇒ Почему ты плачешь?

Where do I go? ⇒ Куда я иду?

<b>Отрицание</b>
Отрицательные предложения в Present Simple образуются с помощью do not или does not. Они часто сокращаются до don’t и doesn’t. Давайте рассмотрим их употребление.


1. На первое место поставьте I/we/you/they или существительное(-ые) во множественном числе;
2. do not или сокращённую форму don’t;
3. Глагол (всегда без окончания -s; - es);
4. Остальные слова.

Чтобы составить отрицательное предложение с does not или doesn’t, необходимо:

1. На первое место поставить he/she/it или существительное в единственном числе;
2. does not или их сокращённую форму doesn’t;
3. Глагол (всегда без окончания -s; - es);
4. Остальные слова.

⇒ В отрицательных предложениях к форме глагола to be прибавляют частицу not.

- I am not или I''m not;
- He/she/it is not или isn''t;
- We/you/they are not или aren''t.

⇒ В отрицательных предложениях к форме глагола to have прибавляют частицу not в сочетании с got.

- I/we/you/they haven''t got;
- He/she/it hasn''t got.

He does not need protection. ⇒ Он не нуждается в защите.

It doesn''t mean anything. ⇒ Это не значит ничего.

He doesn''t expect anything. ⇒ Он не ожидает ничего.

It doesn''t prove anything. ⇒ Это не доказывает ничего.

He doesn''t know anything. ⇒ Он не знает ничего.

It doesn''t change anything. ⇒ Это не меняет ничего.

He doesn''t say anything. ⇒ Он не говорит ничего.

I do not think so. ⇒ Я не думаю так.

We don''t know yet. ⇒ Мы не знаем еще.

You don''t live here. ⇒ Вы не живете здесь.

We don''t think so. ⇒ Мы не считаем так.

I don''t know much. ⇒ Я не знаю многого.

I don''t understand it. ⇒ Я не понимаю этого.

I don''t trust him. ⇒ Я не доверяю ему.

I don''t understand you. ⇒ Я не понимаю тебя.

I don''t see it. ⇒ Я не вижу этого.

They do not recognize him. ⇒ Они не узнают его.

I don''t want it. ⇒ Я не хочу этого.

You don''t answer me. ⇒ Ты не отвечаешь мне.

You don''t trust me. ⇒ Ты не доверяешь мне.', N'Present Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (9, N'Past Simple', N'<b>Утверждение</b>
Past Simple употребляется для выражения действия, которое произошло в определённое время в прошлом, при этом промежуток времени уже закончился.

⇒ Чтобы составить утвердительное предложение в Past Simple, необходимо:

1. Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. К глаголу добавить окончание -ed или использовать его 2-ю форму (для неправильных глаголов);
3. Остальные слова.

⇒ Обратите внимание на формы to be в Past Simple.

I/she/he/it или существительное в единственном числе употребляются с was.
We/you/they или существительное(-ые) во множественном числе употребляются с were.

The room was cold. ⇒ Комната была холодной.

The answer was evasive. ⇒ Ответ был уклончивым.

The bed was empty. ⇒ Кровать была пуста.

The report was good. ⇒ Доклад был хорошим.

The air was oppressive. ⇒ Воздух был тяжёлый.

The darkness was intense. ⇒ Темнота была непроглядной.

The weather was magnificent. ⇒ Погода была великолепной.

The weather was fine. ⇒ Погода была прекрасной.

The place was impassable. ⇒ Место было непроходимым.

The idea was attractive. ⇒ Идея была привлекательной.

<b>Вопрос</b>
Вопрос в Past Simple образуется с помощью Did. Рассмотрим его образование.

1. Поставьте на первое место Did (если вопрос начинается с Where/When и т.д., то did ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. Глагол;
4. Остальные слова.

⇒ В вопросительных предложениях формы глагола to be ставятся перед местоимением(-ями) или существительным(-ми).

- Was ставится перед I/he/she/it или перед существительным в единственном числе;
- Were ставится перед we/you/they или перед существительным(-ми) во множественном числе.

Where did I go? ⇒ Куда я ходил?

How did you proceed? ⇒ Как ты действовал?

Why did he go? ⇒ Почему он ушел?

How did she know? ⇒ Как она узнала?

How did he come? ⇒ Как он пришёл?

When did you come? ⇒ Когда ты приехал?

Where did he live? ⇒ Где он жил?

When did it start? ⇒ Когда это началось?

How did you know? ⇒ Как вы узнали?

How did he win? ⇒ Как он выиграл?

Why did he leave? ⇒ Почему он ушел?

How did you sleep? ⇒ Как ты спала?

Where did they go? ⇒ Куда они ходили?

Why did he run? ⇒ Почему он бежал?

When did you arrive? ⇒ Когда ты приехал?

Where did it happen? ⇒ Где это произошло?

How did you escape? ⇒ Как ты сбежал?

How did you survive? ⇒ Как ты выжил?

Why did you come? ⇒ Почему ты пришла?

Why did you stay? ⇒ Почему вы остались?

<b>Отрицание</b>
Отрицательные предложения в Past Simple образуются с помощью did not. Он часто сокращаются до didn’t. Давайте рассмотрим его употребление.

1. Поставьте на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. did not (или сокращенную форму - didn’t);
3. Глагол;
4. Остальные слова.

⇒ Обратите внимание на употребление to be в Past Simple в отрицательных предложениях. Его формы: was, were.

I/she/he/it или существительное в единственном числе употребляются с was not или wasn’t;
We/you/they или существительное(-ые) во множественном числе употребляются с were not или weren’t.

I didn''t believe him. ⇒ Я не поверила ему.

He didn''t mean it! ⇒ Он не имел в виду это!

I did not know it. ⇒ Я не знала этого.

I did not open it. ⇒ Я не открывала это.

I didn''t encourage him. ⇒ Я не поощрял его.

I didn''t know it. ⇒ Я не знал этого.

I did not seek him! ⇒ Я не искал его!

He did not see me. ⇒ Он не видел меня.

He didn''t blame them. ⇒ Он не винил их.

I did not understand it. ⇒ Я не понял этого.

I did not understand him. ⇒ Я не понял его.

He didn''t buy it. ⇒ Он не покупал это.

I didn''t say it. ⇒ Я не говорил этого.

I didn''t hear you. ⇒ Я не слышал тебя.

I did not believe them. ⇒ Я не поверила им.

I didn''t understand it. ⇒ Я не понял это.

He didn''t believe me. ⇒ Он не поверил мне.

I didn''t mean it. ⇒ Я не имел в виду это.

It did not bother him. ⇒ Это не беспокоило его.

He did not mention it. ⇒ Он не упоминал этого.', N'Past Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (10, N'Future Simple', N'<b>Утверждение</b>
Future Simple употребляется для обозначения какого-либо факта, решения или намерения в будущем, принятого в момент речи.

⇒ Чтобы составить утвердительное предложение во Future Simple, необходимо:

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. Добавить will;
3. Глагол;
4. Остальные слова.

⇒ Обратите внимание, will часто сокращают до ''ll: I''ll, she''ll, they''ll и т.д.

I''ll follow you there. ⇒ Я последую за вами туда.

They will store them there. ⇒ Они будут хранить их там.

I''ll drive you there. ⇒ Я отвезу тебя туда.

We''ll see you there. ⇒ Мы увидим вас там.

He will catch us there. ⇒ Он поймает нас там.

I''ll tell you there. ⇒ Я расскажу тебе там.

I''ll join you there. ⇒ Я присоединюсь к вам там.

I will show you there. ⇒ Я покажу вам там.

They''ll meet us there. ⇒ Они встретят нас там.

I''ll contact him there. ⇒ Я свяжусь с ним там.

<b>Вопрос</b>
Вопрос во Future Simple образуется с помощью Will. Рассмотрим его образование.

1. Поставьте на первое место Will (если вопрос начинается с Where/When и т.д., то will ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. Глагол;
4. Остальные слова.

⇒ Кроме will, в английском языке используется и shall. При этом shall сохраняется в вопросах (просьбах), чтобы дать указание, совет или разрешение, а также в случаях, когда говорящий вызывается сделать что-либо.

Will you be silent? ⇒ Вы будете молчаливыми?

Will you be ready? ⇒ Вы будете готовы?

Will it be cool? ⇒ Это будет круто?

Will you be okay? ⇒ Вы будете в порядке?

Will you be afraid? ⇒ Вы будете напуганы?

Will you be comfortable? ⇒ Вам будет удобно?

Will it be safe? ⇒ Это будет безопасно?

Will she be okay? ⇒ Она будет в порядке?

Will it be dangerous? ⇒ Это будет опасно?

Will I be handsome? ⇒ Я буду красивым?

Will we be safe? ⇒ Мы будем в безопасности?

Will you be serious? ⇒ Ты будешь серьезным?

Will it look real? ⇒ Будет ли это выглядеть реально?

Will you be free? ⇒ Вы будете свободными?

Will it be difficult? ⇒ Это будет сложно?

Will it look normal? ⇒ Это будет выглядеть нормально?

Will they be alive? ⇒ Они будут живы?

Will you be wise? ⇒ Ты будешь мудрым?

Will you be happy? ⇒ Будешь ли ты счастлив?

Will you be reasonable? ⇒ Ты будешь разумным?

<b>Отрицание</b>
Отрицательные предложения во Future Simple образуются с помощью will not. Он часто сокращаются до won’t. Давайте рассмотрим его употребление.

1. Поставьте на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. will not (или сокращенную форму - won’t);
3. Глагол;
4. Остальные слова.

This will not work. ⇒ Это не будет работать.

This won''t help. ⇒ Это не поможет.

This won''t hurt! ⇒ Это не будет больно.

This won''t last. ⇒ Это не продлится.

I will not argue about it. ⇒ Я не буду спорить об этом.

I will not speak with her. ⇒ Я не буду разговаривать с ней.

I won''t look at her. ⇒ Я не буду смотреть на неё.

You won''t look at him. ⇒ Ты не будешь смотреть на него.

We won''t forget about you. ⇒ Мы не забудем о вас.

She won''t look at me. ⇒ Она не будет смотреть на меня.

They won''t forget about you. ⇒ Они не забудут о тебе.

I won''t look at you. ⇒ Я не буду смотреть на тебя.

I won''t leave without him. ⇒ Я не уйду без него.

I won''t go through them. ⇒ Я не пройду через них.

He will not go without you. ⇒ Он не пойдёт без тебя.

We won''t argue about it. ⇒ Мы не будем спорить об этом.

He won''t look at me. ⇒ Он не посмотрит на меня.

I will not live without her. ⇒ Я не буду жить без неё.

We won''t leave without you. ⇒ Мы не уедем без вас.

I will not choose between them. ⇒ Я не выберу между ними.', N'Future Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (11, N'Numerals', N'<b>Числительные</b>
Числительным называется слово, обозначающее количество или порядок предметов по счету. Числительные делятся на количественные и порядковые.

- Количественные числительные обозначают количество предметов и отвечают на вопрос How many - Сколько? 
- Порядковые числительные обозначают порядок предметов при счете и отвечают на вопрос Which? - Который?

⇒ По строению они подразделяются на простые, производные и составные:

1. К простым относятся числительные от 1 до 12.
2. Производными являются числительные от 13 до 19. Они образуются при помощи суффикса -teen: six шесть - sixteen шестнадцать. К производным относятся также числительные, обозначающие десятки. Они образуются при помощи суффикса -ty: six шесть - sixty шестьдесят.
3. К составным относятся числительные, обозначающие десятки с единицами, начиная со второго десятка. Они пишутся через дефис (черточку). Например: twenty-two двадцать два.

This is one of the best ways to learn English quickly. ⇒ Это один из самых лучших способов выучить английский язык быстро.

It''s 20 thousand dollars. ⇒ Это 20 тысяч долларов.

He has ten thousand men. ⇒ У него есть десять тысяч человек.

It costs 4 million dollars ⇒ Это стоит 4 миллиона долларов.

It''s two thousand bucks. ⇒ Это стоит две тысячи баксов.

It holds five million books. ⇒ Это вмещает в себя пять миллионов книг.

It''s one hundred francs. ⇒ Это сто франков.

It''s 10 million miles. ⇒ Это 10 миллионов миль.

It''s ten thousand pounds. ⇒ Это стоит десять тысяч фунтов.

It''s five thousand dollars. ⇒ Это пять тысяч долларов.', N'Numerals')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (12, N'Present Continuous', N'<b>Утверждение</b>
Present Continuous употребляется, когда нужно рассказать, что вы делаете в настоящий момент.

⇒ Чтобы составить утвердительное предложение в Present Continuous, необходимо: 

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. am/is/are; 
3. Глагол с окончанием - ing;
4. Остальные слова.

⇒ Обратите внимание на употребление to be в Present Continuous.

- I употребляется только с am;
- He/she/it или существительное в единственном числе употребляются с is;
- We/you/they или существительное во множественном числе употребляются с are.

I''m marrying that girl. ⇒ Я женюсь на той девушке.

I''m searching for something ⇒ Я ищу кое-что.

You''re looking for support ⇒ Вы ищете поддержку.

I''m waiting for someone. ⇒ Я жду кого-то.

I''m looking for something. ⇒ Я ищу кое-что.

You''re searching for proof ⇒ Вы ищете доказательство.

We''re falling in love ⇒ Мы влюбляемся.

I''m talking about freedom ⇒ Я говорю о свободе.

I''m bankrolling this party. ⇒ Я финансирую эту вечеринку.

You are making a mistake. ⇒ Вы делаете ошибку.

<b>Вопрос</b>
Вопросительные предложения в Present Continuous образуются с помощью Am/Is/Are. Рассмотрим их образование.

- Am употребляется с I и глаголом с - ing.
- Is употребляется с he/she/it и глаголом с -ing.
- Are употребляется с you/we/they и с глаголом с - ing.

⇒ Если в вопросе есть слова What?, Where?, When? и т.д., то am/is/are стоят после них.

Are you going home? ⇒ Вы идете домой?

Are you having fun? ⇒ Вам весело?

Are you seeing anybody? ⇒ Ты встречаешься с кем-то?

Are they having fun? ⇒ Они веселятся?

Are you wearing makeup? ⇒ Вы пользуетесь косметикой?

Are you working tonight? ⇒ Вы работаете сегодня вечером?

Are you doing anything? ⇒ Ты делаешь что-нибудь?

Are you dating anyone? ⇒ Вы встречаетесь с кем-нибудь?

Am I interrupting anything? ⇒ Я прерываю что-то?

Are you expecting anyone? ⇒ Ты ждешь кого-то?

Are we going home? ⇒ Мы едем домой?

Are you wearing cologne? ⇒ Ты пользуешься духами?

Are you wearing lipstick? ⇒ Ты пользуешься помадой?

Am I doing this right? ⇒ Я делаю это правильно?

Are you expecting a letter? ⇒ Вы ждете письмо?

Are you avoiding the question? ⇒ Вы уходите от ответа?

Are you building a boat? ⇒ Вы строите лодку?

Are you having a party? ⇒ У вас вечеринка?

Are you telling the truth? ⇒ Ты говоришь правду?

Are you having a stroke? ⇒ У вас инсульт?

<b>Отрицание</b>
Отрицательные предложения в Present Continuous образуются с помощью am/is/are и not. Они часто сокращаются до isn’t и aren’t. Давайте рассмотрим их употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. am/is/are not или сокращённую форму isn’t/aren’t;
3. Глагол с -ing;
4. Остальные слова.

⇒ В отрицательных предложениях к форме глагола to be прибавляют частицу not.

- I am not или I''m not;
- He/she/it is not или isn''t;
- We/you/they are not или aren''t.

They''re not coming back. ⇒ Они не возвращаются.

I''m not going anywhere. ⇒ Я не иду никуда.

I''m not asking much. ⇒ Я не прошу многого.

We''re not going anywhere. ⇒ Мы не идём никуда.

You''re not coming back. ⇒ Ты не возвращаешься.

You''re not staying here. ⇒ Ты не останешься здесь.

You''re not going anywhere! ⇒ Ты не идешь никуда!

I''m not feeling well. ⇒ Я плохо себя чувствую.

We''re not living together. ⇒ Мы не живем вместе.

I''m not coming back. ⇒ Я не возвращаюсь.

I''m not staying here. ⇒ Я не останусь здесь.

I''m not running away. ⇒ Я не убегаю.

You''re not thinking clearly. ⇒ Ты не мыслишь ясно.

You''re not going alone. ⇒ Ты не идёшь один.

I am not going anywhere. ⇒ Я не иду никуда.

You''re not missing much. ⇒ Ты не теряешь многого.

I''m not walking away. ⇒ Я не ухожу.

I''m not going there. ⇒ Я не иду туда.

I''m not going away. ⇒ Я не сбегаю.

I''m not turning back. ⇒ Я не отступаю.', N'Present Continuous')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (13, N'Past Continuous', N'<b>Утверждение</b>
Past Continuous указывает на процесс, длившийся в определенный момент или период в прошлом.

⇒ Чтобы составить утвердительное предложение в Past Continuous, необходимо: 

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном числе или во множественном;
2. was/were; 
3. Глагол с окончанием -ing;
4. Остальные слова.

⇒ Обратите внимание на употребление to be в Past Continuous.

- I/he/she/it или существительное в единственном числе употребляются только с was;
- We/you/they или существительное(-ые) во множественном числе употребляются с were.

We were sitting in the library. ⇒ Мы сидели в библиотеке.

He was looking at the ground. ⇒ Он смотрел на землю.

I was standing on a hill. ⇒ Я стоял на холме.

She was coming in the room. ⇒ Она входила в комнату.

She was talking about the party. ⇒ Она говорила об этой вечеринке.

He was talking about the weather. ⇒ Он говорил о погоде.

He was sitting by a wall. ⇒ Он сидел у стены.

She was waiting for a bus. ⇒ Она ждала автобус.

He was yelling into the phone. ⇒ Он кричал в телефон.

They were running along the beach. ⇒ Они бежали вдоль пляжа.

<b>Отрицание</b>
Отрицательные предложения в Past Continuous образуются с помощью was/were и not. Они часто сокращаются до wasn’t и weren’t. Давайте рассмотрим их употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. was not/were not или сокращённую форму wasn’t/weren’t;
3. Глагол с -ing;
4. Остальные слова.

⇒ Обратите внимание на употребление to be в Past Continuous в отрицательных предложениях. Его формы: was, were.

I/she/he/it или существительное в единственном числе употребляются с was not или wasn’t;
We/you/they или существительное(-ые) во множественном числе употребляются с were not или weren’t.

He was not studying medicine. ⇒ Он не изучал медицину.

They weren''t working today. ⇒ Они не работали сегодня.

I wasn''t bothering anybody. ⇒ Я не беспокоил никого.

She wasn''t going home. ⇒ Она не шла домой.

I wasn''t doing anything! ⇒ Я не делал ничего!

He wasn''t doing anything. ⇒ Он не делал ничего.

He wasn''t attacking anyone. ⇒ Он не нападал ни на кого.

I wasn''t meeting anyone. ⇒ Я не встречался ни с кем.

We weren''t avoiding anything. ⇒ Мы не избегали ничего.

She wasn''t dating anyone. ⇒ Она не встречалась ни с кем.

They weren''t doing anything. ⇒ Они не делали ничего.

I wasn''t planning anything. ⇒ Я не планировал ничего.

He wasn''t saying anything. ⇒ Он не говорил ничего.

He wasn''t sharing information. ⇒ Он не делился информацией.

I wasn''t asking anything. ⇒ Я не спрашивал ничего.

It wasn''t raining yesterday. ⇒ Дождь не шёл вчера.

We weren''t having fun. ⇒ Мы не веселились.

You weren''t saying anything. ⇒ Вы не говорили ничего.

I wasn''t going home. ⇒ Я не шёл домой.

We weren''t watching TV. ⇒ Мы не смотрели телевизор.

<b>Вопрос</b>
Вопросительные предложения в Past Continuous образуются с помощью Was/Were.

- Was ставится перед I/he/she/it или перед существительным в единственном числе и глаголом с - ing.
- Were ставится перед you/we/they или перед существительным(-ми) во множественном числе и глаголом с -ing.

⇒ Если в вопросе есть слова What?, Where?, When? и т.д., то was/were стоят после них.

What was she doing there? ⇒ Что она делала там?

What was he saying then? ⇒ Что он говорил тогда?

What was he doing there? ⇒ Что он делал там?

What were you doing here? ⇒ Что вы делали здесь?

What were you doing before? ⇒ Что вы делали раньше?

What were you doing then? ⇒ Что ты делал тогда?

What were we defending there? ⇒ Что мы защищали там?

Who were you calling then? ⇒ Кому вы звонили тогда?

What was he seeking? ⇒ Что он искал?

What were you doing? ⇒ Что вы делали?

What was she doing? ⇒ Что она делала?

What were you thinking? ⇒ Что вы думали?

What were you saying? ⇒ Что ты говорил?

What were you expecting? ⇒ Чего вы ожидали?

What was he doing ? ⇒ Что он делал?

What was she thinking? ⇒ Что она думала?

What was I doing ? ⇒ Что я делал?

What was she hiding? ⇒ Что она скрывала?

What were they eating? ⇒ Что они ели?

What was he planning? ⇒ Что он планировал?', N'Past Continuous')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (14, N'Future Continuous', N'<b>Утверждение</b>
Future Continuous указывает на процесс, который начнется до определенного момента в будущем и все еще будет продолжаться в этот момент, который может быть обозначен словами: at noon, at midnight, at that moment или быть очевидным из контекста.

⇒ Чтобы составить утвердительное предложение в Future Continuous, необходимо: 

1. На первое место поставить местоимение I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. will be; 
3. Глагол с окончанием -ing;
4. Остальные слова.

⇒ Обратите внимание, will be часто сокращают до ''ll be: I''ll be, she''ll be, they''ll be и т.д.

I''ll be arriving in an hour. ⇒ Я приеду через час.

I will be waiting in the car. ⇒ Я буду ждать в машине.

I''ll be waiting for a call. ⇒ Я буду ждать звонка.

We''ll be sitting at a table ⇒ Мы будем сидеть за столом.

We''ll be swimming in the sea ⇒ Мы будем плавать в море.

You''ll be sleeping on the floor. ⇒ Вы будете спать на полу.

He''ll be waiting for a response. ⇒ Он будет ждать ответа.

I''ll be starting in the fall. ⇒ Я начну осенью.

He''ll be walking around this town ⇒ Он будет гулять по этому городу.

We''ll be dining on the terrace. ⇒ Мы будем обедать на террасе.

<b>Вопрос</b>
Вопросительные предложения в Future Continuous образуются с помощью Will.

⇒ Чтобы задать вопрос в Future Continuous, необходимо: 

1. На первое место поставить Will (если вопрос начинается с What/When и т.д., то will ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. be
4. Глагол с окончанием -ing;
5. Остальные слова.

Will he be leaving soon? ⇒ Он уезжает скоро?

Will you be going back? ⇒ Вы будете возвращаться?

Will you be staying there? ⇒ Вы будете останавливаться там?

Will you be staying long? ⇒ Вы останетесь надолго?

Will you be coming back? ⇒ Ты приедешь обратно?

Will you be going soon? ⇒ Ты поедешь скоро?

Will she be coming alone? ⇒ Она придёт одна?

Will you be leaving soon? ⇒ Ты уезжаешь скоро?

Will I be staying there? ⇒ Я остановлюсь там?

Will you be going there? ⇒ Вы пойдёте туда?

Will you be leaving too? ⇒ Вы уезжаете тоже?

What will we be playing? ⇒ Что мы будем играть?

What will I be doing? ⇒ Что я буду делать?

What will we be eating? ⇒ Что мы будем есть?

What will you be doing? ⇒ Что ты будешь делать?

Who will I be playing? ⇒ Кого я буду играть?

Who will we be fighting? ⇒ С кем мы будем драться?

And what will I be doing? ⇒ И что я буду делать?

And what will you be serving? ⇒ И что вы будете подавать?

And what will you be doing? ⇒ И что вы будете делать?

<b>Отрицание</b>
Отрицательные предложения в Future Continuous образуются с помощью will и not. Он часто сокращается до won’t. Давайте рассмотрим его употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. will not или его сокращённую форму won’t;
3. be
4. Глагол с окончанием -ing;
5. Остальные слова.

I won''t be seeing them again. ⇒ Я не буду видеться с ними снова.

I won''t be chasing you anymore. ⇒ Я не буду преследовать вас больше.

I won''t be seeing you again. ⇒ Я не буду видеться с тобой снова.

He won''t be bothering you again. ⇒ Он не побеспокоит вас снова.

He won''t be bothering me again! ⇒ Он не побеспокоит меня снова!

We won''t be troubling you again. ⇒ Мы не будем беспокоить вас снова.

We won''t be seeing him again. ⇒ Мы не будем встречаться с ним снова.

I won''t be seeing you later. ⇒ Я не буду видеться с вами позже.

You won''t be getting it back. ⇒ Вы не получите это обратно.

I won''t be seeing him again. ⇒ Я не буду видеться с ним снова.

He won''t be wearing it again. ⇒ Он не будет носить это снова.

You won''t be seeing him again. ⇒ Вы не будете видеться с ним снова.

I won''t be kissing you again. ⇒ Я не буду целовать вас снова.

I won''t be keeping you anymore. ⇒ Я не буду задерживать вас больше.

I won''t be asking him again. ⇒ не буду спрашивать его снова.

I won''t be using them again. ⇒ Я не буду пользоваться ими снова.

He won''t be hitting you again. ⇒ Он не будет бить вас снова.

I won''t be doing it again. ⇒ Я не буду делать это снова.

I won''t be eating. ⇒ Я не буду есть.

We won''t be starving. ⇒ Мы не будем голодать.', N'Future Continuous')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (15, N'Present Perfect', N'<b>Утверждение</b>
Present Perfect используется для выражения действия, которое уже завершилось (есть результат), но промежуток времени не закончился.

⇒ Чтобы составить утвердительное предложение с местоимениями I/we/you/they или с существительными, необходимо:

1.Поставить на первое место I/we/you/they или существительное(-ые) во множественном числе;
2. have;
3. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ Чтобы составить утвердительное предложение с местоимениями he/she/it или с существительным, необходимо:

1. Поставить на первое место he/she/it или существительное в единственном числе;
2. has;
3. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

I have asked him. ⇒ Я спросил его.

I have solved it. ⇒ Я решил это.

You have guessed it. ⇒ Вы угадали это.

I''ve tried it. ⇒ Я пробовал это.

I have helped you. ⇒ Я помог тебе.

I''ve seen them. ⇒ Я видел их.

You''ve guessed it! ⇒ Вы угадали это!

You have forgotten me. ⇒ Ты забыл меня.

You''ve convinced me. ⇒ Вы убедили меня.

I have returned it. ⇒ Я вернул это.

<b>Вопрос</b>
Вопрос в Present Perfect образуется с помощью have или has.

⇒ Чтобы задать вопрос с помощью have, используйте этот порядок:

1. Поставьте на первое место have (если вопрос начинается с вопросительного слова, то have идёт после него);
2. Потом I/we/you/they или существительное(-ые) во множественном числе;
3. Глагол с -ed или третья форма неправильного глагола;
4. Остальные слова.

⇒ Чтобы задать вопрос с помощью has, используйте следующий порядок: 

1. Поставьте на первое место has (если вопрос начинается с When/Where и т.д., то has ставим после них);
2. Потом he/she/it или существительное единственном числе;
3. Глагол с -ed или третья форма неправильного глагола;
4. Остальные слова.

What have you seen? ⇒ Что вы видели?

What have we lost? ⇒ Что мы потеряли?

What have you remembered? ⇒ Что ты вспомнил?

What have you eaten? ⇒ Что ты ел?

What have I missed? ⇒ Что я пропустил?

What have you written? ⇒ Что ты написал?

What have we missed? ⇒ Что мы пропустили?

What have you discovered? ⇒ Что вы обнаружили?

What have you planned? ⇒ Что ты cпланировал?

What have they achieved? ⇒ Чего они достигли?

What have you experienced? ⇒ Что вы испытали?

What have we won? ⇒ Что мы выиграли?

What have I learned? ⇒ Что я выучил?

What have you noticed? ⇒ Что ты заметил?

What have I created? ⇒ Что я создал?

What have you brought? ⇒ Что ты принёс?

What have you forgotten? ⇒ Что ты забыл?

What have you arranged? ⇒ Что вы организовали?

What have you chosen? ⇒ Что ты выбрал?

What have I written? ⇒ Что я написал?

<b>Отрицание</b>
В отрицаниях используют have not (haven’t) /has not (hasn’t)

⇒ Чтобы составить отрицательное предложение с местоимениями I/we/you/they или с существительными, необходимо:

1. Поставить на первое место I/we/you/they или существительное(-ые) во множественном числе;
2. have not;
3. Глагол с -ed или третья форма неправильного глагола.
4. Остальные слова.

⇒ Чтобы составить отрицательное предложение с местоимениями he/she/it или с существительным, необходимо:

1.Поставить на первое место he/she/it или существительное в единственном числе;
2. has not;
3. Глагол с -ed или третья форма неправильного глагола.
4. Остальные слова.

I haven''t prepared anything. ⇒ Я не приготовил ничего.

You haven''t heard anything ⇒ Ты не слышал ничего.

We haven''t changed anything. ⇒ Мы не меняли ничего.

You haven''t achieved anything. ⇒ Вы не достигли ничего.

I haven''t told anyone. ⇒ Я не говорил никому.

You haven''t missed anything. ⇒ Вы не пропустили ничего.

I haven''t asked anyone. ⇒ Я не спрашивал никого.

I''ve never used magic. ⇒ Я никогда не использовал магию.

I haven''t shown anyone. ⇒ Я не показывал никому.

You haven''t seen anything. ⇒ Вы не видели ничего.

We haven''t ordered anything. ⇒ Мы не заказывали ничего.

You''ve never lost anything. ⇒ Вы никогда не теряли ничего.

I have not hidden anything. ⇒ Я не скрывал ничего.

I haven''t noticed anything. ⇒ Я не заметил ничего.

I''ve never understood music. ⇒ Я никогда не понимал музыку.

We''ve never grown wheat. ⇒ Мы никогда не выращивали пшеницу.

I haven''t met anybody. ⇒ Я не встречал никого.

You haven''t done anything. ⇒ Вы не делали ничего.

We haven''t told anyone. ⇒ Мы не говорили никому.

I''ve never stolen anything. ⇒ Я никогда не крал ничего.', N'Present Perfect')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (16, N'Present Perfect Continuous', N'<b>Утверждение</b>
Present Perfect Continuous указывает на то, что действие началось в прошлом и продолжается в настоящем. Present Perfect Continuous используют, когда нужно подчеркнуть продолжительность действия.

⇒ Чтобы составить утвердительное предложение с местоимениями I/we/you/they или с существительными, необходимо:

1.Поставить на первое место I/we/you/they или существительное(-ые) во множественном числе;
2. have;
3. been;
4. К глаголу добавить окончание -ing;
5. Остальные слова.

⇒ Чтобы составить утвердительное предложение с местоимениями he/she/it или с существительным, необходимо:

1. Поставить на первое место he/she/it или существительное в единственном числе;
2. has;
3. been;
4. К глаголу добавить окончание -ing;
5. Остальные слова.

We''ve been reading. ⇒ Мы читаем.

I have been thinking. ⇒ Я думаю.

We''ve been dancing ⇒ Мы танцуем.

You''ve been hiding. ⇒ Вы скрываетесь.

They''ve been waiting. ⇒ Они ждут.

I''ve been listening. ⇒ Я слушаю.

You''ve been reading ⇒ Вы читаете.

I''ve been worrying ⇒ Я беспокоюсь.

They''ve been hiding ⇒ Они прячутся.

We''ve been discussing ⇒ Мы обсуждаем.

<b>Вопрос</b>
Вопрос в Present Perfect Continuous образуется с помощью have или has.
Рассмотрим образование вопроса с помощью have.

1. Поставьте на первое место have (если вопрос начинается с вопросительного слова, то have идёт после него);
2. Потом I/we/you/they или существительное(-ые) во множественном числе;
3. been;
4. Глагол с -ing;
5. Остальные слова.

Рассмотрим образование вопроса с помощью has.

1. Поставьте на первое место has (если вопрос начинается с When/Where и т.д., то has идёт после них);
2. Потом he/she/it или существительное в единственном числе;
3. been;
4. Глагол с -ing;
5. Остальные слова.

How long have I been sleeping? ⇒ Как долго я сплю?

How long have I been dreaming? ⇒ Как долго я мечтаю?

How long have you been writing? ⇒ Как долго вы пишете?

How long have you been trying? ⇒ Как долго вы пытаетесь?

How long have you been talking? ⇒ Как долго вы разговариваете?

How long have you been waiting? ⇒ Как долго ты ждёшь?

How long have we been waiting? ⇒ Как долго мы ждём?

How long have you been acting? ⇒ Как долго вы играете?

How long have you been drawing? ⇒ Как долго ты рисуешь?

How long have you been playing? ⇒ Как долго вы играете?

How long have you been listening? ⇒ Как долго ты слушаешь?

How long have you been working? ⇒ Как долго ты работаешь?

How long have you been chasing? ⇒ Как долго вы преследуете?

How long have you been hiding? ⇒ Как долго вы прячетесь?

How long have we been driving? ⇒ Как долго мы едем?

How long have you been teaching? ⇒ Как долго вы преподаёте?

How long have you been doing? ⇒ Как долго вы делаете?

How long have we been walking? ⇒ Как долго мы гуляем?

How long have they been playing? ⇒ Как долго они играют?

How long have you been standing? ⇒ Как долго вы стоите?

<b>Отрицание</b>
В отрицаниях используют have not (haven’t) /has not (hasn’t).

⇒ Чтобы составить отрицательное предложение с местоимениями I/we/you/they или с существительными, необходимо:

1.Поставить на первое место I/we/you/they или существительное(-ые) во множественном числе;
2. have not;
3. been; 
4. Глагол с -ing. 
5. Остальные слова.

⇒ Чтобы составить отрицательное предложение с местоимениями he/she/it или с существительным, необходимо:

1.Поставить на первое место he/she/it или существительное в единственном числе;
2. has not;
3. been;
4. Глагол с -ing. 
5. Остальные слова.

I haven''t been exercising. ⇒ Я не тренируюсь.

We haven''t been lying. ⇒ не лжём.

You haven''t been practicing ⇒ не практикуетесь.

I haven''t been dreaming. ⇒ Я не мечтаю.

You haven''t been listening. ⇒ Вы не слушаете.

I haven''t been fighting. ⇒ Я не борюсь.

I haven''t been swimming ⇒ Я не плаваю.

I haven''t been lying! ⇒ Я не вру!

You haven''t been listening ⇒ Вы не слушаете.

I haven''t been hiding. ⇒ Я не прячусь.

I haven''t been running. ⇒ Я не бегаю.

I haven''t been sitting. ⇒ Я не сижу.

I have not been working ⇒ Я не работаю.

I haven''t been spying. ⇒ Я не шпионю.

You haven''t been reading ⇒ Ты не читаешь.

I haven''t been trying ⇒ Я не пытаюсь.

You haven''t been sleeping. ⇒ Вы не спите.

I haven''t been listening. ⇒ Я не слушаю.

I''ve not been drinking. ⇒ Я не пью.

We haven''t been trying ⇒ Мы не пытаемся.', N'Present Perfect Continuous')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (17, N'Past Perfect', N'<b>Утверждение</b>
Past Perfect употребляется для выражения прошедшего действия, которое уже совершилось до определенного момента в прошлом. По смыслу эта форма представляет собой "предпрошедшее" время, так как она описывает уже совершенное прошедшее действие по отношению к моменту, также являющемуся прошедшим.
⇒ Чтобы составить предложение в Past Perfect используют глагол had. Форма had используется с любыми существительными и местоимениями.

1. Поставьте на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. had;
3. Глагол с -ed или третья форма неправильного глагола;
4. Остальные слова.

She had deceived him again. ⇒ Она обманула его снова.

That girl had given her money. ⇒ Та девочка отдала ей её деньги.

All energy had left his body. ⇒ Вся энергия покинула его тело.

She had suffered from depression. ⇒ Она страдала от депрессии.

They had sung of love. ⇒ Они спели о любви.

He had forgotten that voice. ⇒ Он забыл тот голос.

They had discussed that question. ⇒ Они обсудили тот вопрос.

She had earned that fate. ⇒ Она заслужила ту судьбу.

It had been someone else. ⇒ Это был кто-то другой.

They had lost everything else. ⇒ Они потеряли всё остальное.

<b>Вопрос</b>
Вопрос в Past Perfect образуется с помощью had.

1. Поставьте на первое место had (если вопрос начинается с вопросительного слова, то had идёт после него);
2. Потом I/we/you/they/he/she/it или существительное в единственном или множественном числе;
3. Глагол с -ed или третья форма неправильного глагола;
4. Остальные слова.

What had she done? ⇒ Что она сделала?

What had he expected? ⇒ Чего он ожидал?

What had they done? ⇒ Что они сделали?

What had it been? ⇒ Что это было?

What had you done? ⇒ Что ты сделал?

What had she worn? ⇒ Что она носила?

What had he observed? ⇒ Что он наблюдал?

What had he forgotten? ⇒ Что он забыл?

What had we done? ⇒ Что мы сделали?

What had they forgotten? ⇒ Что они забыли?

Where had they gone? ⇒ Куда они ушли?

Where had she been? ⇒ Где она была?

Where had it been? ⇒ Где оно было?

Why had it altered? ⇒ Почему оно изменилось?

Why had I bothered? ⇒ Почему я беспокоился?

Why had he hidden? ⇒ Почему он спрятался?

Why had he bothered? ⇒ Почему он беспокоился?

When had he fallen? ⇒ Когда он упал?

Where had it begun? ⇒ Где это началось?

When had it begun? ⇒ Когда это началось?

<b>Отрицание</b>
Для отрицания в Past Perfect используют had not (hadn’t).
⇒ Чтобы составить отрицательное предложение, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или множественном числе;
2. had not;
3. Глагол с -ed или третья форма неправильного глагола;
4. Остальные слова.

They had not forgotten him. ⇒ Они не забыли его.

She had not noticed it. ⇒ Она не заметила этого.

They had not written me ⇒ Они не написали мне.

They had not changed it. ⇒ Они не изменили это.

He hadn''t believed them. ⇒ Он не поверил им.

He hadn''t recognized her. ⇒ Он не узнал её.

They hadn''t seen her. ⇒ Они не видели её.

She had not believed him. ⇒ Она не поверила ему.

He had not realized it. ⇒ Он не осознал это.

She hadn''t found him. ⇒ Она не нашла его.

He hadn''t expected it. ⇒ Он не ожидал этого.

He hadn''t signed it. ⇒ Он не подписал это.

He had not introduced himself. ⇒ Он не представился.

It hadn''t bothered him. ⇒ Это не побеспокоило его.

I hadn''t brought it. ⇒ Я не приносил это.

He had never considered it. ⇒ Он никогда не рассматривал это.

She hadn''t seen them ⇒ Она не видела их.

He hadn''t saved her. ⇒ Он не спас её.

She had not heard it. ⇒ Она не услышала это.

She had not told him. ⇒ Она не сказала ему.', N'Past Perfect')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (18, N'Past Perfect Continuous', N'<b>Утверждение</b>
Past Perfect Continuous указывает на действие, которое началось в прошлом, продолжалось в течение некоторого времени и либо закончилось непосредственно перед неким моментом в прошлом или все еще не закончилось к какому-то моменту в прошлом.

⇒ Чтобы составить утвердительное предложение, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или множественном числе;
2. had;
3. been;
4. К глаголу добавить окончание -ing;
5. Остальные слова.

They had been dancing all night. ⇒ Они танцевали всю ночь.

She had been working all night. ⇒ Она работала всю ночь.

She had been telling the truth. ⇒ Она говорила правду.

He had been walking some time. ⇒ Он гулял какое-то время.

He had been chasing a fantasy. ⇒ Он гонялся за фантазией.

She had been preparing this speech. ⇒ Она готовила эту речь.

She had been expecting the explosion. ⇒ Она ожидала взрыва.

He''d been expecting this day. ⇒ Он ожидал этого дня.

He had been telling the truth. ⇒ Он говорил правду.

We''d been walking all day. ⇒ Мы гуляли весь день.

<b>Вопрос</b>
Вопрос в Past Perfect Continuous образуется с помощью had.

1. Поставьте на первое место had (если вопрос начинается с вопросительного слова, то had идёт после него);
2. Потом I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. been;
4. К глаголу добавить окончание -ing;
5. Остальные слова.

Had I been dreaming? ⇒ Я мечтал?

Had I been shouting? ⇒ Я кричал?

Had you been arguing? ⇒ Вы спорили?

Had she been lying? ⇒ Она обманывала?

Had I been drinking? ⇒ Я пил?

Had she been listening? ⇒ Она слушала?

Had he been sleeping? ⇒ Он спал?

Had she been crying? ⇒ Она плакала?

Had he been listening? ⇒ Он слушал?

Had he been cheating? ⇒ Он обманывал?

Had he been watching? ⇒ Он смотрел?

Had he been crying? ⇒ Он плакал?

Had I been screaming? ⇒ Я кричала?

Had he been practicing? ⇒ Он практиковался?

What had they been counting on? ⇒ На что они рассчитывали?

What had I been thinking of? ⇒ О чём я думал?

What had I been worrying about? ⇒ О чём я беспокоился?

What had they been talking about? ⇒ О чём они говорили?

What had we been talking about? ⇒ О чём мы говорили?

How long had he been watching? ⇒ Как долго он смотрел?

<b>Отрицание</b>
Для отрицания в Past Perfect Continuous используют had not (hadn’t).

⇒ Чтобы составить отрицательное предложение, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или множественном числе;
2. had not/hadn’t;
3. been;
4. К глаголу добавить окончание -ing;
5. Остальные слова.

She had not been walking a minute ⇒ Она не гуляла ни минуты.

I hadn''t been following the conversation. ⇒ Я не следил за разговором.

He had not been expecting a kick. ⇒ Он не ожидал пинка.

He had not been chasing the kid. ⇒ Он не преследовал ребёнка.

Jennifer had not been listening. ⇒ Дженнифер не слушала.

Charles had not been attending. ⇒ Чарльз не присутствовал.

Dad hadn''t been listening. ⇒ Папа не слушал.

They had not been playing ⇒ Они не играли.

They had not been crying. ⇒ Они не плакали.

He had not been running. ⇒ Он не бегал.

He hadn''t been sleeping. ⇒ Он не спал.

She had not been working. ⇒ Она не работала.

She hadn''t been winning. ⇒ Она не побеждала.

I hadn''t been listening. ⇒ Я не слушал.

He hadn''t been joking. ⇒ Он не шутил.

She had not been attending. ⇒ Она не присутствовала.

We hadn''t been waiting ⇒ Мы не ждали.

He hadn''t been working. ⇒ Он не работал.

He hadn''t been counting ⇒ Он не считал.

He hadn''t been listening. ⇒ Он не слушал.', N'Past Perfect Continuous')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (19, N'Future in the past', N'<b>Употребление</b>
Future in the past употребляется, когда мы говорим о действии, которое являлось будущим для определенного момента в прошлом.

⇒ Future in the Past чаще всего употребляется после слов said (that), told (that), thought (that). В основном, когда вы передаете свои или чужие мысли, слова. В таких случаях Future in the Past является заменой Future Simple:

They will go to France -> I thought they would go to France.
Они поедут во Францию. -> Я думал, что они поедут во Францию.

⇒ Чтобы составить утвердительное предложение в Future in the past, необходимо:

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. Добавить would или сокращенную форму ''d;
3. Глагол;
4. Остальные слова.

They said he would feel better. ⇒ Они сказали, что он будет чувствовать себя лучше.

They knew we would be here. ⇒ Они знали, что мы будем здесь.

I forgot you''d come. ⇒ Я забыл, что вы придёте.

I expected he would leave. ⇒ Я ожидал, что он уедет.

I knew he''d call back. ⇒ Я знал, что он перезвонит.

I knew he would hear. ⇒ Я знала, что он услышит.

I thought it would work. ⇒ Я думал, что это сработает.

I knew you would be here. ⇒ Я знала, что вы будете здесь.

I thought it would end. ⇒ Я думал, что это закончится.

We knew we would win. ⇒ Мы знали, что мы победим.', N'Future in the past')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (20, N'To be going to', N'<b>Утверждение</b>
To be going to дословно означает «собираться что-то сделать». To be going to используется для выражения намерений и планов. В отличие от will, действие было запланировано до момента речи.

⇒ Чтобы составить утвердительное предложение с to be going to, необходимо: 

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. am/is/are/was/were/will be; 
3. going to;
4. Остальные слова.

⇒ Обратите внимание на употребление to be в Present, Past и Future.

- I употребляется с am, was или will be;
- He/she/it или существительное в единственном числе употребляются с is, was, will be;
- We/you/they или существительное во множественном числе употребляются с are, were, will be. 

We are going to release you. ⇒ Мы собираемся освободить вас.

I am going to tell you. ⇒ Я собираюсь сказать вам.

I''m going to criticize them. ⇒ Я собираюсь критиковать их.

I''m going to catch them! ⇒ Я собираюсь поймать их!

I''m going to read it. ⇒ Я собираюсь прочитать это.

I''m going to find them. ⇒ Я собираюсь найти их.

I''m going to return it. ⇒ Я собираюсь вернуть это.

I''m going to correct it. ⇒ Я собираюсь исправить это.

I''m going to punish you. ⇒ Я собираюсь наказать тебя.

I''m going to sell it. ⇒ Я собираюсь продать это.

<b>Вопрос</b>
Вопросительные предложения с оборотом to be going to образуются с помощью Am/Is/Are в настоящем времени. Рассмотрим их образование.

1. Поставьте на первое место Am/Is/Are (если вопрос начинается с When/Where и т.д., то am/is/are идёт после них);
2. Потом поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. going to;
4. Глагол;
5. Остальные слова.

⇒ Обратите внимание на образование глагола to be в Present, Past и Future:
- Am/Was/Will употребляется с I;
- Is/Was/Will употребляется с he/she/it;
- Are/Were/Will употребляется с you/we/they.

What are you going to say? ⇒ Что вы собираетесь сказать?

What are you going to give? ⇒ Что ты собираешься дать?

Who''re we going to tell? ⇒ Кому мы собираемся рассказать?

What are you going to use? ⇒ ты собираешься использовать?

What are we going to achieve? ⇒ Чего мы собираемся достичь?

What are we going to lose? ⇒ Что мы собираемся потерять?

What are you going to find? ⇒ Что ты собираешься найти?

What are you going to study? ⇒ Что вы собираетесь изучать?

Who am I going to call? ⇒ Кому я собираюсь позвонить?

Who are you going to believe? ⇒ Кому вы собираетесь поверить?

What are we going to say? ⇒ Что мы собираемся сказать?

What are you going to eat? ⇒ Что ты собираешься съесть?

What are you going to write? ⇒ Что ты собираешься написать?

Who am I going to tell? ⇒ Кому я собираюсь сказать?

What are you going to sing? ⇒ Что ты собираешься спеть?

What are you going to try? ⇒ Что ты собираешься попробовать?

What are you going to print? ⇒ Что ты собираешься распечатать?

What are you going to make? ⇒ Что ты собираешься создать?

Who am I going to be? ⇒ Кем я собираюсь быть?

What are they going to say? ⇒ Что они собираются сказать?

<b>Отрицание</b>
Отрицательные предложения c оборотом going to образуются с помощью am/is/are и not в настоящем времени. Они часто сокращаются до isn’t и aren’t. Давайте рассмотрим их употребление.

⇒ Чтобы составить отрицательное предложение с местоимениями I/you/we/they или с существительными, необходимо:

1.На первое место поставьтеI/we/you/they или существительное(-ые) во множественном числе;
2. am not (''m not) или are not (сокращённую форму aren’t);
3. going to;
4. Глагол;
5. Остальные слова.

⇒ Чтобы составить отрицательное предложение с местоимениями he/she/it или с существительным, необходимо:

1.Поставить на первое место he/she/it или существительное в единственном числе;
2. is not или сокращённую форму isn’t;
3. going to;
4. Глагол;
5. Остальные слова.

⇒ Обратите внимание на формы глагола to be в прошедшем и будущем времени.
- I употребляется с wasn''t или won''t be;
- He/she/it или существительное в единственном числе употребляются с wasn''t, won''t be;
- We/you/they или существительное во множественном числе употребляются с weren''t, won''t be.

I''m not going to sell this company. ⇒ Я не собираюсь продавать эту компанию.

I''m not going to wear a hat. ⇒ Я не собираюсь носить шляпу.

I''m not going to drink this poison. ⇒ Я не собираюсь пить этот яд.

You''re not going to catch any disease. ⇒ Вы не собираетесь подхватить какую-нибудь болезнь.

I''m not going to marry the prince! ⇒ Я не собираюсь выходить замуж за принца!

I''m not going to rob the shop. ⇒ Я не собираюсь грабить магазин.

I''m not going to take the scholarship. ⇒ Я не собираюсь принимать стипендию.

You''re not going to save the world. ⇒ Вы не собираетесь спасать мир.

I''m not going to lose any time. ⇒ Я не собираюсь терять время.

I''m not going to serve any dinner. ⇒ Я не собираюсь накрывать какой-либо ужин.

I''m not going to discuss the matter. ⇒ Я не собираюсь обсуждать вопрос.

I''m not going to be a doctor. ⇒ Я не собираюсь быть врачом.

I''m not going to leave the car. ⇒ Я не собираюсь оставлять машину.

We''re not going to see the palace. ⇒ Мы не собираемся осматривать дворец.

You are not going to make the mistake ⇒ Ты не собираешься совершать ошибку.

You are not going to believe this story. ⇒ Ты не собираешься поверить этой истории.

I''m not going to be a coward. ⇒ Я не собираюсь быть трусом.

I''m not going to do any work. ⇒ Я не собираюсь делать какую-либо работу.

I am not going to be a spy. ⇒ Я не собираюсь быть шпионом.

You aren''t going to lose any power. ⇒ Вы не собираетесь терять какую-либо власть.', N'To be going to')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (21, N'Passive Voice - Present Simple', N'<b>Утверждение</b>
Пассивный залог в настоящем времени используется, когда нам более важно лицо или предмет, над которым совершается действие, а не тот, кто его совершил.

⇒ Чтобы составить утвердительное предложение в Passive Voice, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. am/is/are;
3. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ Обратите внимание на формы to be в Passive Voice.

- I употребляется только с am;
- He/she/it или существительное в единственном числе употребляются с is;
- We/you/they или существительное(-ые) во множественном числе употребляются с are.

The house is deserted. ⇒ Дом заброшен.

The battle is won. ⇒ Битва выиграна.

The matter is closed. ⇒ Вопрос закрыт.

The decision is made. ⇒ Решение принимается.

The way is shut. ⇒ Путь закрыт.

The meeting is adjourned. ⇒ Заседание откладывается.

The picture is finished. ⇒ Картина закончена.

The world is changed. ⇒ Мир меняется.

This meeting is adjourned. ⇒ Эта встреча перенесена.

The door is locked. ⇒ Дверь закрыта.

<b>Вопрос</b>
Вопросительные предложения в Passive Voice образуются с помощью Am/Is/Are. Рассмотрим их образование.

- Am употребляется с I и глаголом с - ed или используется его 3-я форма (для неправильных глаголов);
- Is употребляется с he/she/it и глаголом с - ed или используется его 3-я форма (для неправильных глаголов).
- Are употребляется с you/we/they и глаголом с - ed или используется его 3-я форма (для неправильных глаголов).

⇒ Если в вопросе есть слова What?, Where?, When? и т.д., то am/is/are стоят после них.

Am I understood? ⇒ Меня понимают?

Am I fired? ⇒ Я уволен?

Are you armed? ⇒ Вы вооружены?

Are you shot? ⇒ Ты ранен?

Are you wounded? ⇒ Ты ранен?

Am I forgiven? ⇒ Я прощён?

Am I invited? ⇒ Я приглашён?

Is it broken? ⇒ Это сломано?

Is she injured? ⇒ Она ранена?

Is it fixed? ⇒ Это закреплено?

Is it locked? ⇒ Оно заперто?

Is breakfast included? ⇒ Завтрак включён?

Is anything broken? ⇒ Что-то разбито?

Is war declared? ⇒ Война объявлена?

Why is the door locked? ⇒ Почему дверь закрыта?

How is this word pronounced? ⇒ Как это слово произносят?

Where is the rifle pointed? ⇒ Куда винтовка направлена?

Where is the rope attached? ⇒ Куда верёвку крепят?

Why is this door locked? ⇒ Почему эта дверь заперта?

Why is this room hidden? ⇒ Почему эта комната спрятана?

<b>Отрицание</b>
Отрицательные предложения в Passive Voice образуются с помощью am/is/are и not. Они часто сокращаются до isn’t и aren’t. Давайте рассмотрим их употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. am/is/are not или сокращённую форму isn’t/aren’t;
3. Глагол с окончанием -ed или используйте его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ В отрицательных предложениях к форме глагола to be прибавляют частицу not.

- I am not или I''m not;
- He/she/it is not или isn''t;
- We/you/they are not или aren''t.

That ship is not built for speed. ⇒ Тот корабль не строится для скорости.

This booth is not equipped for microsurgery. ⇒ Эта кабина не оборудована для микрохирургии.

This idea is not based on anything ⇒ Эта мысль не основывается ни на чём.

The stove isn''t lit till evening. ⇒ Печь не зажигают до вечера.

This jam is not made with feijoa. ⇒ Этот джем не делают с фейхоа.

It''s not derived from any theory. ⇒ Это не происходит из какой-либо теории.

It''s not needed in the sentence. ⇒ Это не нужно в предложении.

It''s not needed in the conversation. ⇒ Это не нужно в этой беседе.

He''s not prepared for this situation. ⇒ Он не подготовлен для этой ситуации.

It''s not taught as a science. ⇒ Это не преподают как науку.

It is not marked upon the map. ⇒ Это не отмечено на карте.

I hope they''re not ruined. ⇒ Я надеюсь, они не разрушены.

I think I''m not fired. ⇒ Я думаю, меня не увольняют.

You know you''re not allowed ⇒ Вы знаете, что вам не разрешается.

I guess we''re not invited ⇒ Я догадываюсь, что нас не пригласили.

But I''m not paid for it. ⇒ Но мне не платят за это.

I''m not often invited to parties. ⇒ Меня нечасто приглашают на вечеринки.

Details are not provided in this example. ⇒ Подробности не представлены в этом примере.

Stamps aren''t sold in a supermarket. ⇒ Марки не продаются в супермаркете.

This situation is not observed in the private sector. ⇒ Эта ситуация не наблюдается в частном секторе.', N'Passive Voice - Present Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (22, N'Passive Voice - Past Simple', N'<b>Утверждение</b>
Пассивный залог в прошедшем используется, когда нам более важно лицо или предмет, над которым совершилось действие, а не тот, кто его совершил.

⇒ Чтобы составить утвердительное предложение в Passive Voice - Past Simple, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. was/were;
3. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ Обратите внимание на формы to be в прошедшем пассивном залоге.

- I/He/she/it или существительное в единственном числе употребляются с was;
- We/you/they или существительное(-ые) во множественном числе употребляются с were.

I was chosen. ⇒ Меня выбрали.

It was done. ⇒ Оно было сделано.

He was poisoned. ⇒ Его отравили.

They were separated. ⇒ Они были разделены.

I was told. ⇒ Мне сказали.

It was confiscated. ⇒ Оно было конфисковано.

We were betrayed. ⇒ Нас предали.

It was accomplished. ⇒ Оно было достигнуто.

It was shut. ⇒ Оно было заперто.

He was destroyed. ⇒ Он был уничтожен.

<b>Вопрос</b>
Вопросительные предложения в Passive Voice - Past Simple образуются с помощью Was/Were. Рассмотрим их образование.

- Was употребляется с I/he/she/it и глаголом с - ed или используется его 3-я форма (для неправильных глаголов).
- Were употребляется с you/we/they и глаголом с - ed или используется его 3-я форма (для неправильных глаголов).

⇒ Если в вопросе есть слова What?, Where?, When? и т.д., то was/were стоят после них.

What poison was injected? ⇒ Какой яд был введён?

What magic was used? ⇒ Какая магия использовалась?

What time was it found? ⇒ В какое время оно было найдено?

What year were you born? ⇒ В каком году ты родился?

Was it agreed? ⇒ Это было согласовано?

Were you asked? ⇒ Тебя спрашивали?

Was she forced? ⇒ Её заставили?

Was he poisoned? ⇒ Он был отравлен?

Were you offended? ⇒ Вы были оскорблены?

Was it advertised? ⇒ Оно рекламировалось?

Was he disturbed? ⇒ Его побеспокоили?

Were we loved? ⇒ Мы были любимы?

Was it fulfilled? ⇒ Оно было выполнено?

Were they rescued? ⇒ Они были спасены?

Were they launched? ⇒ Они были запущены?

Were they paid? ⇒ Им заплатили?

Were we followed? ⇒ За нами следовали?

Was it poisoned? ⇒ Оно было отравлено?

Were you fired? ⇒ Тебя уволили?

Were we baptized? ⇒ Нас крестили?

<b>Отрицание</b>
Отрицательные предложения в Passive Voice - Past Simple образуются с помощью was/were и not. Они часто сокращаются до wasn’t и weren’t. Давайте рассмотрим их употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. was/were not или сокращённую форму wasn’t/weren’t;
3. Глагол с окончанием -ed или используйте его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ В отрицательных предложениях к форме глагола to be прибавляют частицу not.

- I/He/she/it was not или wasn''t;
- We/you/they were not или weren''t.

It wasn''t locked. ⇒ Оно не было заперто.

They were not forgotten. ⇒ Они не были забыты.

I wasn''t invited. ⇒ Я не был приглашён.

It wasn''t planned. ⇒ Это не было запланировано.

He wasn''t invited. ⇒ Он не был приглашён.

I was not told. ⇒ Мне не сказали.

We weren''t told much. ⇒ Нам не рассказали много.

They weren''t treated badly. ⇒ С ними не обращались плохо.

I wasn''t born there. ⇒ Я не родился там.

He wasn''t blown away. ⇒ Его не сдуло.

She wasn''t born here. ⇒ Она не родилась здесь.

I wasn''t invited anyway. ⇒ Меня не приглашали всё равно.

It wasn''t done yet. ⇒ Оно не было сделано ещё.

She wasn''t medicated anymore. ⇒ Её не лечили больше.

We weren''t brought here. ⇒ Нас не привезли сюда.

We weren''t raised together. ⇒ Мы не воспитывались вместе.

He was not invited there. ⇒ Его не приглашали туда.

It wasn''t healed yet. ⇒ Оно не было вылечено ещё.

He wasn''t expected anywhere. ⇒ Его не ожидали нигде.

They were not made here. ⇒ Они не были сделаны здесь.', N'Passive Voice - Past Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (23, N'Passive Voice - Future Simple', N'<b>Утверждение</b>
Пассивный залог в будущем используется, когда нам более важно лицо или предмет, над которым будет совершаться действие, а не тот, кто его будет совершать.

⇒ Чтобы составить утвердительное предложение в Passive Voice - Future Simple, необходимо:

1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. will be;
3. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

His promises will be fulfilled. ⇒ Его обещания будут выполнены.

Your sins will be forgiven. ⇒ Ваши грехи будут прощены.

Her interests will be protected. ⇒ Её интересы будут защищать.

Your words will be heard ⇒ Ваши слова будут услышаны.

Our prisoners will be released. ⇒ Наши заключённые будут освобождены.

Your goods will be confiscated. ⇒ Ваши товары будут конфискованы.

Their names will be lost. ⇒ Их имена будут потеряны.

Your thoughts will be forgotten. ⇒ Твои мысли будут забыты.

Your photos will be returned ⇒ Ваши фотографии вернут.

Your questions will be answered. ⇒ Ваши вопросы будут отвечены.

<b>Вопрос</b>
Вопросительные предложения в Passive Voice - Future Simple образуются с помощью Will. Рассмотрим его образование.
1. Поставьте на первое место Will (если вопрос начинается с Where/When и т.д., то will ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или множественном числе;
3. be;
4. Глаголом с - ed или используется его 3-я форма (для неправильных глаголов).

Will I be known as the philosopher? ⇒ Меня будут знать как философа?

Will we be arrested on the spot? ⇒ Нас арестуют на месте?

Will you be recognized? ⇒ Вас узнают?

Will it be closed? ⇒ Оно будет закрыто?

Will they be solved? ⇒ Они будут решены?

Will we be invited? ⇒ Мы будем приглашены?

Will he be recognized? ⇒ Его узнают?

Will he be arrested? ⇒ Он будет арестован?

Will he be released? ⇒ Он будет освобождён?

Will I be forgiven? ⇒ Я буду прощён?

Will we be remembered? ⇒ Нас будут помнить?

Will it be found? ⇒ Оно будет найдено?

Will he be punished? ⇒ Он будет наказан?

Will I be punished? ⇒ Я буду наказан?

How will the money be spent? ⇒ Как будут потрачены деньги?

How will the information be used? ⇒ Как будет использоваться информация?

How will this material be distributed? ⇒ Как этот материал будет распространяться?

When will the film be released? ⇒ Когда будет выпущен фильм?

When will this feature be used? ⇒ Когда будет использоваться эта функция?

Will we be permitted to go, Father? ⇒ Нам разрешат идти, отец?

<b>Отрицание</b>
Отрицательные предложения в Passive Voice - Future Simple образуется с помощью will и not. Он часто сокращается до won’t. Давайте рассмотрим его употребление.

1. На первое место поставьте I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. will not или сокращённую форму won’t;
3. be;
4. Глагол с окончанием -ed или используйте его 3-ю форму (для неправильных глаголов);
5. Остальные слова.

I will not be interrupted. ⇒ Меня не будут перебивать.

He won''t be used. ⇒ Его не будут использовать.

I will not be cheated! ⇒ Я не буду обманут!

We will not be cheated. ⇒ Нас не обманут.

They will not be found ⇒ Их не найдут.

I will not be questioned. ⇒ Я не буду допрошен.

You will not be harmed. ⇒ Вам не навредят.

They will not be remembered. ⇒ Их не вспомнят.

I will not be refused. ⇒ Мне не откажут.

I will not be offended. ⇒ Я не буду обижен.

He won''t be offended. ⇒ Он не будет обижен.

I will not be threatened. ⇒ Меня не запугают.

It will not be repeated. ⇒ Это не будут повторять.

You won''t be understood. ⇒ Тебя не поймут.

We won''t be bothered. ⇒ Нас не побеспокоят.

You will never be replaced ⇒ Вас никогда не заменят.

They won''t be noticed. ⇒ Их не заметят.

It will not be delayed. ⇒ Оно не будет отложено.

I will not be blackmailed. ⇒ Меня не будут шантажировать.

We''ll never be divided ⇒ Нас никогда не разделят.', N'Passive Voice - Future Simple')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (24, N'Passive Voice - Present Perfect', N'<b>Утверждение</b>
Чтобы составить утвердительное предложение в Passive Voice - Present Perfect, необходимо:
1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. have/has;
3. been;
4. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ Обратите внимание на формы to have в перфектом пассиве.

- He/she/it или существительное в единственном числе употребляются с has;
- I/we/you/they или существительное(-ые) во множественном числе употребляются с have.

My purse has been stolen. ⇒ Мой кошелёк украли.

His student has been attacked. ⇒ Его студента атаковали.

Your car has been stolen. ⇒ Вашу машину украли.

Your absence has been noted. ⇒ Ваше отсутствие отметили.

My decision has been made. ⇒ Моё решение принято.

Your book has been published! ⇒ Ваша книга опубликована!

My son has been betrayed. ⇒ Моего сына предали.

Your schedule has been changed. ⇒ Ваше расписание изменено.

Their home has been destroyed ⇒ Их дом уничтожили.

My husband has been found. ⇒ Моего мужа нашли.

<b>Вопрос</b>
Вопросительные предложения в Passive Voice - Present Perfect образуются с помощью Have/Has. Рассмотрим их образование.

1. Поставьте на первое место Have/Has (если вопрос начинается с Where/When и т.д., то have/has ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или множественном числе;
3. been;
4. Глаголом с - ed или используется его 3-я форма (для неправильных глаголов).

⇒ Обратите внимание на формы to have в Passive Voice - Present Perfect.
- Has употребляется с he/she/it или существительным в единственном числе;
- Have употребляется с I/we/you/they или существительным(-ыми) во множественном числе.

Has he been warned? ⇒ Его предупредили?

Has he been threatened? ⇒ Ему угрожали?

Has he been caught? ⇒ Его поймали?

Has she been bitten? ⇒ Её укусили?

Has he been found? ⇒ Его нашли?

Has he been arrested? ⇒ Его арестовали?

Has she been transferred? ⇒ Её перевели?

Has she been harmed? ⇒ Ей навредили?

Has it been replaced? ⇒ Оно заменено?

Has he been chosen? ⇒ Его выбрали?

Has he been poisoned? ⇒ Его отравили?

Has he been released? ⇒ Его освободили?

Has it been proved? ⇒ Это было доказано?

Has he been called? ⇒ Ему позвонили?

Has he been told? ⇒ Ему сказали?

Has he been fired? ⇒ Его уволили?

Has he ever been arrested? ⇒ Его когда-нибудь арестовывали?

Has he ever been published? ⇒ Его когда-нибудь публиковали?

Has it ever been used? ⇒ Оно когда-нибудь использовалось?

Has it ever been accomplished? ⇒ Это когда-либо было достигнуто?

<b>Отрицание</b>
Чтобы составить отрицательное предложение в Passive Voice - Present Perfect, необходимо:
1.Поставить на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. have not/has not или haven''t/hasn''t ;
3. been;
4. К глаголу добавить окончание -ed или использовать его 3-ю форму (для неправильных глаголов);
4. Остальные слова.

⇒ Обратите внимание на формы to have в перфектом пассиве.

- He/she/it или существительное в единственном числе употребляются с has;
- I/we/you/they или существительное(-ые) во множественном числе употребляются с have.

It has not been used ⇒ Оно не использовалось.

She hasn''t been told. ⇒ Ей не сказали.

He hasn''t been found. ⇒ Его не нашли.

She hasn''t been arrested ⇒ Её не арестовали.

She hasn''t been asked. ⇒ Её не спросили.

It hasn''t been confirmed. ⇒ Это не было подтверждено.

It hasn''t been washed. ⇒ Оно не было вымыто.

It hasn''t been scheduled. ⇒ Оно не было назначено.

She hasn''t been trained. ⇒ Её не обучали.

She has not been forgotten. ⇒ Её не забыли.

It has not been announced. ⇒ Это не объявили.

It''s not been assigned. ⇒ Это не было установлено.

It hasn''t been stolen. ⇒ Оно не было украдено.

The lawn hasn''t been mowed. ⇒ Газон не подстригли.

The story has not been changed. ⇒ Историю не изменили.

This room hasn''t been used ⇒ Эта комната не использовалась.

The future has not been written. ⇒ Будущее не написали.

The winner hasn''t been announced. ⇒ Победителя не объявили.

This information has not been confirmed. ⇒ Эта информация не подтвердилась.

This place hasn''t been photographed. ⇒ Это место не фотографировали.', N'Passive Voice - Present Perfect')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (25, N'Participles', N'<b>Причастия</b>
Английские причастия делятся на причастие настоящего времени (Participle I) и причастие прошедшего времени (Participle II).
1. Participle I образуется с помощью - ing и употребляется:

- Для образования Present, Past, Future Continuous: I was saving your life.
- В пассивном залоге: I don''t like being followed.
- Перед существительными в качестве определения: a dancing girl
- В причастных и деепричастных оборотах: He felt his panic growing.

2. Participle II образуется с помощью - ed и употребляется:

- В Present, Past, Future Perfect и пассивном залоге: I have received it today.
- Перед существительными в качестве определения: a written letter.
- В причастных оборотах: I found a book left by my friend.

I was expecting your husband. ⇒ Я ожидал вашего мужа.

I was washing my hair. ⇒ Я мыл свои волосы.

I was enjoying our talk. ⇒ Я наслаждался нашей беседой.

I was visiting my sister. ⇒ Я навещал свою сестру.

I was practising my answer. ⇒ Я практиковал свой ответ.

She was helping her sister. ⇒ Она помогала своей сестре.

He was sipping his coffee. ⇒ Он пил маленькими глотками свой кофе.

I was depositing my paycheck. ⇒ Я клал в банк свою зарплату.

I was cleaning your room. ⇒ Я убирал твою комнату.

I was painting my apartment. ⇒ Я красила свою квартиру.', N'Participles')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (26, N'Modal Verbs (Can/Could)', N'<b>Утверждение</b>
Can выражает возможность или способность выполнить действие и переводится как "могу, умею". Could переводится как “мог, имел возможность”.

⇒ Чтобы образовать предложение с модальными глаголами can или could, необходимо:

1. На первое место поставить I/we/you/they/he/she/it или существительное в единственном или множественном числе;
2. can или could;
3. Глагол;
4. Остальные слова.

You could feel the man coming. ⇒ Вы могли почувствовать, что мужчина приближается.

He could feel the dog shivering. ⇒ Он мог почувствовать, что собака дрожит.

He could feel the table trembling ⇒ Он мог почувствовать, как стол вибрирует.

She could feel the sub moving. ⇒ Она могла почувствовать, как субмарина движется.

She could feel the girl trembling. ⇒ Она могла почувствовать, как девочка дрожит.

He could feel the ship rocking. ⇒ Он мог почувствовать, как корабль качает.

You can go to a movie theater ⇒ Вы можете сходить в кинотеатр.

We can go to the game tonight! ⇒ Мы можем пойти на игру сегодня вечером!

We could go to an art gallery. ⇒ Мы могли бы пойти в картинную галерею.

You can go to the gas station ⇒ Вы можете сходить на заправку.

<b>Вопрос</b>
Can, could употребляются в вопросах, чтобы попросить вежливо о чем-либо. Чтобы составить вопрос с помощью can или could, необходимо:

1. На первое место поставить Can или Could (если вопрос начинается с Where/When и т.д., то can или could ставим после них);
2. Потом I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
3. Глагол;
4. Остальные слова.

⇒ Can переводится как "могу", "можно". У could более вежливое значение. Он переводится "не мог бы", "не могли бы" и т.д..

Can I show you something? ⇒ Могу я показать тебе кое-что?

Can I offer you anything? ⇒ Могу я предложить вам что-нибудь?

Can you do it tomorrow? ⇒ Можете ли вы сделать это завтра?

Can I see you tomorrow? ⇒ Могу я увидеть тебя завтра?

Can I call you tomorrow? ⇒ Могу я позвонить тебе завтра?

Can I bring you anything? ⇒ Могу я принести вам что-нибудь?

Can we get you anything? ⇒ Можем ли мы принести вам что-нибудь?

Can I borrow it tomorrow? ⇒ Можно я позаимствую это завтра?

Can I offer you coffee? ⇒ Могу я предложить тебе кофе?

Could you call me tomorrow? ⇒ Не мог бы ты позвонить мне завтра?

Could you help me upstairs? ⇒ Не могли бы вы мне помочь наверху?

Can you give me coffee? ⇒ Можешь дать мне кофе?

Can I buy you breakfast? ⇒ Можно я куплю тебе завтрак?

Can you meet me tonight? ⇒ Можешь ли ты встретить меня сегодня вечером?

Can we discuss it tomorrow? ⇒ Можем ли мы обсудить это завтра?

Could we do it tomorrow? ⇒ Не могли бы мы сделать это завтра?

Can I get you water? ⇒ Могу я принести тебе воды?

Can I walk you home? ⇒ Могу я проводить вас домой?

Can I sell you glass? ⇒ Можно я продам вам стекло?

Can I pay you tomorrow? ⇒ Могу я заплатить вам завтра?

<b>Отрицание</b>
Отрицательные предложения образуются с помощью cannot или could not. Они часто сокращаются до can’t или couldn''t. Давайте рассмотрим их употребление.

1. Поставьте на первое место I/we/you/they/he/she/it или существительное в единственном или во множественном числе;
2. cannot (could not) или сокращенную форму can’t (couldn''t);
3. Глагол;
4. Остальные слова.

I couldn''t pick a career. ⇒ Я не мог определиться с карьерой.

I could not sleep that night. ⇒ Я не мог уснуть той ночью.

I could not bear the disgrace. ⇒ Я не мог вынести позор.

I can''t stay all night. ⇒ Я не могу оставаться всю ночь.

You can''t keep a secret. ⇒ Вы не можете хранить секрет.

She could not speak another word. ⇒ Она не могла сказать ни слова.

He couldn''t say a word ⇒ Он не мог сказать ни слова.

We can''t change that history. ⇒ Мы не можем изменить ту историю.

She could not believe the news. ⇒ Она не могла поверить новостям.

She could not tolerate a rebuke. ⇒ Она не могла вытерпеть упрёк.

He couldn''t find an explanation. ⇒ Он не мог найти объяснение.

I couldn''t paint a fence. ⇒ Я не мог покрасить забор.

I can''t wait another minute. ⇒ Я не могу ждать еще минуту.

He couldn''t move a muscle. ⇒ Он не мог пошевелить ни одним мускулом.

I can''t find a shirt ⇒ Я не могу найти рубашку.

He could not be the villain. ⇒ Он не мог быть злодеем.

I couldn''t keep that promise. ⇒ Я не мог сдержать то обещание.

I couldn''t sleep that night. ⇒ Я не могла спать той ночью.

It can''t do any harm. ⇒ Это не может причинить никакого вреда.

I could not touch the boy. ⇒ Я не мог прикоснуться к мальчику.', N'Modal Verbs (Can/Could)')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (27, N'Infinitive', N'<b>Инфинитив</b>
Инфинитив - это форма глагола с частицей to, которая обычно указывается в словаре. Например, to go, to be, to swim. В русском языке это форма на -ть: ходить, быть, плавать.
⇒ Инфинитив употребляется после глаголов и сочетаний:
want - хотеть
decide - решать
learn - учиться, узнавать
refuse - отказываться
offer - предлагать
would like/would love to - хотел бы
try - в значении пытаться
plan - планировать
agree - соглашаться
Запомните эти глаголы! Они будут использоваться в тренировке.

⇒ Инфинитив употребляется для указания причины и соответствует союзу “чтобы” в русском языке.

She goes to the supermarket to buy food. ⇒ Она ходит в супермаркет, чтобы покупать еду.

⇒ Инфинитив употребляется после прилагательных, таких как glad, afraid, happy, ready, в предложениях с to be.

I am glad to see you. ⇒ Я рад видеть вас.

He promised to follow my advice. ⇒ Он пообещал, что последует моему совету.

He tried to raise his head. ⇒ Он попытался поднять свою голову.

He offered to buy my company. ⇒ Он предложил купить мою компанию.

She decided to test her theory. ⇒ Она решила проверить свою теорию.

He wanted to check his mail. ⇒ Он хотел проверить свою почту.

They wanted to hear his story. ⇒ Они хотели услышать его историю.

She wanted to share her story. ⇒ Она хотела поделиться своей историей.

I wanted to be your friend. ⇒ Я хотел быть твоим другом.

I decided to express my opinion. ⇒ Я решила выразить своё мнение.

She refused to lose her brother. ⇒ Она отказалась терять своего брата.', N'Infinitive')
INSERT [dbo].[RuleModels] ([RuleId], [Title], [Text], [Category]) VALUES (28, N'Gerund', N'<b>Герундий</b>
Герундий, как часть речи, можно описать как «что-то среднее между существительным и глаголом», то есть он обладает признаками и того, и другого. Чтобы образовать герундий, к глаголу добавляется окончание -ing.
singing - пение
skating - катание на коньках
traveling (travelling) - путешествия

⇒ Герундий употребляется в следующих случаях:
- После предлогов (for, before, without, by, about, of, from, in).
I’m interested in drawing. ⇒ Я интересуюсь рисованием.

⇒ Герундий употребляется после некоторых глаголов:
like - нравиться
mind - возражать
enjoy - наслаждаться
hate - ненавидеть
love - любить
I like swimming. ⇒ Мне нравится плавать.

⇒ После некоторых глаголов употребляется и герундий, и инфинитив, но при этом значение меняется:
Try + герундий означает попробовать, пытаться что-то сделать.
Try + инфинитив употребляется, когда говорящий хочет сделать что-то, но на самом деле у него ничего не получается. 
Stop - в значении "прекратить" употребляется в качестве герундия.
Stop - в значении "прекратить одно действие, чтобы начать другое" употребляется в качестве инфинитива.

⇒ Герундий употребляется после некоторых фразовых глаголов. Запомните эти глаголы! Они будут использоваться в тренировке.
go on - продолжать
feel like - хотеть
carry on - продолжать
insist on - настаивать на чём-либо
keep (on) - продолжать
plan on - собираться
I plan on meeting my relatives. ⇒ Я собираюсь навестить своих родственников.

Thank you for calling. ⇒ Спасибо, что звоните.

Thank you for participating! ⇒ Спасибо за участие!

Thank you for trying. ⇒ Спасибо, что пытаетесь.

Thank you for cooperating. ⇒ Спасибо за сотрудничество.

Thank you for noticing. ⇒ Спасибо, что заметили.

Thank you for listening! ⇒ Спасибо за то, что слушали!

Thank you for reading! ⇒ Спасибо за чтение!

Thank you for replying. ⇒ Спасибо за ответ.

Do you like racing? ⇒ Вам нравятся гонки?

Thank you for asking! ⇒ Спасибо, что спрашиваете!

Forgive me for interrupting. ⇒ Простите меня за то, что перебиваю.

Thank you for joining. ⇒ Спасибо, что присоединились.

Do you like sleeping? ⇒ Тебе нравится спать?

Thank you for visiting. ⇒ Спасибо, что навестили.

Thank you for watching! ⇒ Спасибо, что смотрите!

Forgive me for complaining. ⇒ Простите меня за то, что жалуюсь.

Do you like travelling? ⇒ Вам нравится путешествовать?

Do you like writing? ⇒ Тебе нравится писать?

Do you like studying? ⇒ Тебе нравится учиться?

Do you like driving? ⇒ Вам нравится водить машину?', N'Gerund')
SET IDENTITY_INSERT [dbo].[RuleModels] OFF
SET IDENTITY_INSERT [dbo].[SentenceTasks] ON 

INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (7, N'I know something.', N'Present Simple', N'Я знаю что-то.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (8, N'You look fine.', N'Present Simple', N'Ты выглядишь прекрасно.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (9, N'I prefer cash.', N'Present Simple', N'Я предпочитаю наличные.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (10, N'You need help.', N'Present Simple', N'Вы нуждаетесь в помощи.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (11, N'I love lasagna.', N'Present Simple', N'Я люблю лазанью.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (12, N'I need inspiration.', N'Present Simple', N'Мне нужно вдохновение.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (13, N'We do nothing.', N'Present Simple', N'Мы не делаем ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (14, N'I like pizza.', N'Present Simple', N'Мне нравится пицца.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (15, N'I feel something.', N'Present Simple', N'Я чувствую что-то.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (16, N'I like everything.', N'Present Simple', N'Мне нравится всё.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (17, N'Where does he sleep?', N'Present Simple', N'Где он спит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (18, N'How does she look?', N'Present Simple', N'Как она выглядит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (19, N'How does it work?', N'Present Simple', N'Как это работает?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (20, N'How does it happen?', N'Present Simple', N'Как это происходит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (21, N'Why does it rain?', N'Present Simple', N'Почему идет дождь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (22, N'Where does she live?', N'Present Simple', N'Где она живёт?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (23, N'How does it begin?', N'Present Simple', N'Как это начинается?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (24, N'Where does she work?', N'Present Simple', N'Где она работает?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (25, N'How does he look?', N'Present Simple', N'Как он выглядит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (26, N'Why does it happen?', N'Present Simple', N'Почему это происходит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (27, N'How does she know?', N'Present Simple', N'Откуда она знает?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (28, N'Where does he work?', N'Present Simple', N'Где он работает?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (29, N'Where does it end?', N'Present Simple', N'Где это заканчивается?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (30, N'How does it sound?', N'Present Simple', N'Как это звучит?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (31, N'Why does she stay?', N'Present Simple', N'Почему она остаётся?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (32, N'Why do you lie?', N'Present Simple', N'Почему ты врёшь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (33, N'When do you begin?', N'Present Simple', N'Когда ты начинаешь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (34, N'Where do you sleep?', N'Present Simple', N'Где вы спите?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (35, N'Why do you cry?', N'Present Simple', N'Почему ты плачешь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (36, N'Where do I go?', N'Present Simple', N'Куда я иду?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (37, N'He does not need protection.', N'Present Simple', N'Он не нуждается в защите.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (38, N'It doesn''t mean anything', N'Present Simple', N'Это не значит ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (39, N'He doesn''t expect anything.', N'Present Simple', N'Он не ожидает ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (40, N'It doesn''t prove anything.', N'Present Simple', N'Это не доказывает ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (41, N'He doesn''t know anything.', N'Present Simple', N'Он не знает ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (42, N'It doesn''t change anything.', N'Present Simple', N'Это не меняет ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (43, N'He doesn''t say anything.', N'Present Simple', N'Он не говорит ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (44, N'I do not think so.', N'Present Simple', N'Я не думаю так.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (45, N'We don''t know yet.', N'Present Simple', N'Мы не знаем еще.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (46, N'You don''t live here.', N'Present Simple', N'Вы не живете здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (47, N'We don''t think so.', N'Present Simple', N'Мы не считаем так.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (48, N'I don''t know much.', N'Present Simple', N'Я не знаю многого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (49, N'I don''t understand it.', N'Present Simple', N'Я не понимаю этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (50, N'I don''t trust him.', N'Present Simple', N'Я не доверяю ему.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (51, N'I don''t understand you.', N'Present Simple', N'Я не понимаю тебя.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (52, N'I don''t see it.', N'Present Simple', N'Я не вижу этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (53, N'They do not recognize him.', N'Present Simple', N'Они не узнают его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (54, N'I don''t want it.', N'Present Simple', N'Я не хочу этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (55, N'You don''t answer me.', N'Present Simple', N'Ты не отвечаешь мне.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (56, N'You don''t trust me.', N'Present Simple', N'Ты не доверяешь мне.')
SET IDENTITY_INSERT [dbo].[SentenceTasks] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (1, N'Sergey', N'Mokin', N'limonad-vafel@list.ru', N'admin', N'admin')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (2, N'Vasily', N'Default', N'dafault@def.ru', N'default123', N'admin')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (3, N'sergey', N'mokin', N's.a.mokin@list.ru', N'adminnew', N'admin')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (4, N'asdfsafd', N'sadfasdfasfd', N'sadfasfd@sadfasfd.ewqr', N'qwerqwer', N'admin')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (5, N'Vasiliy', N'Vasiliy', N'vasiliy@vs.com', N'vasiliy123', N'user')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[UserWords] ON 

INSERT [dbo].[UserWords] ([Id], [UserId], [WordId]) VALUES (3, 3, 9)
INSERT [dbo].[UserWords] ([Id], [UserId], [WordId]) VALUES (4, 3, 10)
INSERT [dbo].[UserWords] ([Id], [UserId], [WordId]) VALUES (5, 3, 11)
INSERT [dbo].[UserWords] ([Id], [UserId], [WordId]) VALUES (12, 3, 12)
INSERT [dbo].[UserWords] ([Id], [UserId], [WordId]) VALUES (13, 3, 13)
SET IDENTITY_INSERT [dbo].[UserWords] OFF
SET IDENTITY_INSERT [dbo].[Words] ON 

INSERT [dbo].[Words] ([WordId], [Original], [Translate], [Category]) VALUES (9, N'food', N'еда', N'food')
INSERT [dbo].[Words] ([WordId], [Original], [Translate], [Category]) VALUES (10, N'tea', N'чай', N'food')
INSERT [dbo].[Words] ([WordId], [Original], [Translate], [Category]) VALUES (11, N'yummy', N'вкусно', N'food')
INSERT [dbo].[Words] ([WordId], [Original], [Translate], [Category]) VALUES (12, N'bread', N'хлеб', N'food')
INSERT [dbo].[Words] ([WordId], [Original], [Translate], [Category]) VALUES (13, N'coffee', N'кофе', N'food')
SET IDENTITY_INSERT [dbo].[Words] OFF
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordId])
GO
/****** Object:  StoredProcedure [dbo].[INS]    Script Date: 2/19/2018 8:47:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure  [dbo].[INS]                              
(                                                          
   @Query  Varchar(MAX)                                                          
)                              

AS                              

SET nocount ON                  

DECLARE @WithStrINdex as INT                            
DECLARE @WhereStrINdex as INT                            
DECLARE @INDExtouse as INT                            

DECLARE @SchemaAndTAble VArchar(270)                            
DECLARE @Schema_name  varchar(30)                            
DECLARE @Table_name  varchar(240)                            
DECLARE @Condition  Varchar(MAX)                             

SET @WithStrINdex=0                            

SELECT @WithStrINdex=CHARINDEX('With',@Query )                            
, @WhereStrINdex=CHARINDEX('WHERE', @Query)                            

IF(@WithStrINdex!=0)                            
SELECT @INDExtouse=@WithStrINdex                            
ELSE                            
SELECT @INDExtouse=@WhereStrINdex                            

SELECT @SchemaAndTAble=Left (@Query,@INDExtouse-1)                                                     
SELECT @SchemaAndTAble=Ltrim (Rtrim( @SchemaAndTAble))                            

SELECT @Schema_name= Left (@SchemaAndTAble, CharIndex('.',@SchemaAndTAble )-1)                            
,      @Table_name = SUBSTRING(  @SchemaAndTAble , CharIndex('.',@SchemaAndTAble )+1,LEN(@SchemaAndTAble) )                            

,      @CONDITION=SUBSTRING(@Query,@WhereStrINdex+6,LEN(@Query))--27+6                            


DECLARE @COLUMNS  table (Row_number SmallINT , Column_Name VArchar(Max) )                              
DECLARE @CONDITIONS as varchar(MAX)                              
DECLARE @Total_Rows as SmallINT                              
DECLARE @Counter as SmallINT              

DECLARE @ComaCol as varchar(max)            
SELECT @ComaCol=''                   

SET @Counter=1                              
SET @CONDITIONS=''                              

INSERT INTO @COLUMNS                              
SELECT Row_number()Over (Order by ORDINAL_POSITION ) [Count], Column_Name 
FROM INformation_schema.columns 
WHERE Table_schema=@Schema_name AND table_name=@Table_name         


SELECT @Total_Rows= Count(1) 
FROM @COLUMNS                              

SELECT @Table_name= '['+@Table_name+']'                      

SELECT @Schema_name='['+@Schema_name+']'                      

While (@Counter<=@Total_Rows )                              
begin                               
--PRINT @Counter                              

SELECT @ComaCol= @ComaCol+'['+Column_Name+'],'            
FROM @COLUMNS                              
WHERE [Row_number]=@Counter                          

SELECT @CONDITIONS=@CONDITIONS+ ' + Case When ['+Column_Name+'] is null then ''Null'' Else '''''''' + Replace( Convert(varchar(Max),['+Column_Name+']  ) ,'''''''',''''  ) +'''''''' end+'+''','''                                                     
FROM @COLUMNS                              
WHERE [Row_number]=@Counter                              

SET @Counter=@Counter+1                              

End                              

SELECT @CONDITIONS=Right(@CONDITIONS,LEN(@CONDITIONS)-2)                              

SELECT @CONDITIONS=LEFT(@CONDITIONS,LEN(@CONDITIONS)-4)              
SELECT @ComaCol= substring (@ComaCol,0,  len(@ComaCol) )                            

SELECT @CONDITIONS= '''INSERT INTO '+@Schema_name+'.'+@Table_name+ '('+@ComaCol+')' +' Values( '+'''' + '+'+@CONDITIONS                              

SELECT @CONDITIONS=@CONDITIONS+'+'+ ''')'''                              

SELECT @CONDITIONS= 'Select  '+@CONDITIONS +'FRom  ' +@Schema_name+'.'+@Table_name+' With(NOLOCK) ' + ' Where '+@Condition                              
print(@CONDITIONS)                              
Exec(@CONDITIONS)  
GO
USE [master]
GO
ALTER DATABASE [EngMan] SET  READ_WRITE 
GO

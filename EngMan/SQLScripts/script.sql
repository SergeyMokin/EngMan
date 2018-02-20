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
/****** Object:  Table [dbo].[GuessesTheImages]    Script Date: 2/20/2018 2:03:57 PM ******/
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
/****** Object:  Table [dbo].[RuleModels]    Script Date: 2/20/2018 2:03:57 PM ******/
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
/****** Object:  Table [dbo].[SentenceTasks]    Script Date: 2/20/2018 2:03:57 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2/20/2018 2:03:57 PM ******/
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
/****** Object:  Table [dbo].[Messages]    Script Date: 2/20/2018 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL FOREIGN KEY REFERENCES [Users]([Id]),
	[BeneficiaryId] [int] NOT NULL FOREIGN KEY REFERENCES [Users]([Id]),
	[Text] [nvarchar](max) NULL,
	[Time] [datetime] NOT NULL,
	[CheckReadMes] [bit] NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserWords]    Script Date: 2/20/2018 2:03:58 PM ******/
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
/****** Object:  Table [dbo].[Words]    Script Date: 2/20/2018 2:03:58 PM ******/
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
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (57, N'The room was cold.', N'Past Simple', N'Комната была холодной.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (58, N'The answer was evasive.', N'Past Simple', N'Ответ был уклончивым.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (59, N'The bed was empty.', N'Past Simple', N'Кровать была пуста.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (60, N'The report was good.', N'Past Simple', N'Доклад был хорошим.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (61, N'The air was oppressive.', N'Past Simple', N'Воздух был тяжёлый.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (62, N'The darkness was intense.', N'Past Simple', N'Темнота была непроглядной.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (63, N'The weather was magnificent.', N'Past Simple', N'Погода была великолепной.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (64, N'The weather was fine.', N'Past Simple', N'Погода была прекрасной.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (65, N'The place was impassable.', N'Past Simple', N'Место было непроходимым.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (66, N'The idea was attractive.', N'Past Simple', N'Идея была привлекательной.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (67, N'Where did I go?', N'Past Simple', N'Куда я ходил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (68, N'How did you proceed?', N'Past Simple', N'Как ты действовал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (69, N'Why did he go?', N'Past Simple', N'Почему он ушел?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (70, N'How did she know?', N'Past Simple', N'Как она узнала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (71, N'How did he come?', N'Past Simple', N'Как он пришёл?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (72, N'When did you come?', N'Past Simple', N'Когда ты приехал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (73, N'Where did he live?', N'Past Simple', N'Где он жил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (74, N'When did it start?', N'Past Simple', N'Когда это началось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (75, N'How did you know?', N'Past Simple', N'Как вы узнали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (76, N'How did he win?', N'Past Simple', N'Как он выиграл?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (77, N'Why did he leave?', N'Past Simple', N'Почему он ушел?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (78, N'How did you sleep?', N'Past Simple', N'Как ты спала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (79, N'Where did they go?', N'Past Simple', N'Куда они ходили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (80, N'Why did he run?', N'Past Simple', N'Почему он бежал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (81, N'When did you arrive?', N'Past Simple', N'Когда ты приехал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (82, N'Where did it happen?', N'Past Simple', N'Где это произошло?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (83, N'How did you escape?', N'Past Simple', N'Как ты сбежал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (84, N'How did you survive?', N'Past Simple', N'Как ты выжил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (85, N'Why did you come?', N'Past Simple', N'Почему ты пришла?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (86, N'Why did you stay?', N'Past Simple', N'Почему вы остались?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (87, N'I didn''t believe him.', N'Past Simple', N'Я не поверила ему.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (88, N'He didn''t mean it!', N'Past Simple', N'Он не имел в виду это!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (89, N'I did not know it.', N'Past Simple', N'Я не знала этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (90, N'I did not open it.', N'Past Simple', N'Я не открывала это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (91, N'I didn''t encourage him.', N'Past Simple', N'Я не поощрял его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (92, N'I didn''t know it.', N'Past Simple', N'Я не знал этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (93, N'I did not seek him!', N'Past Simple', N'Я не искал его!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (94, N'He did not see me.', N'Past Simple', N'Он не видел меня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (95, N'He didn''t blame them.', N'Past Simple', N'Он не винил их.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (96, N'I did not understand it.', N'Past Simple', N'Я не понял этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (97, N'I did not understand him.', N'Past Simple', N'Я не понял его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (98, N'He didn''t buy it.', N'Past Simple', N'Он не покупал это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (99, N'I didn''t say it.', N'Past Simple', N'Я не говорил этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (100, N'I didn''t hear you.', N'Past Simple', N'Я не слышал тебя.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (101, N'I did not believe them.', N'Past Simple', N'Я не поверила им.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (102, N'I didn''t understand it.', N'Past Simple', N'Я не понял это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (103, N'He didn''t believe me.', N'Past Simple', N'Он не поверил мне.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (104, N'I didn''t mean it.', N'Past Simple', N'Я не имел в виду это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (105, N'It did not bother him.', N'Past Simple', N'Это не беспокоило его.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (106, N'He did not mention it.', N'Past Simple', N'Он не упоминал этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (107, N'I''ll follow you there.', N'Future Simple', N'Я последую за вами туда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (108, N'They will store them there.', N'Future Simple', N'Они будут хранить их там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (109, N'I''ll drive you there.', N'Future Simple', N'Я отвезу тебя туда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (110, N'We''ll see you there.', N'Future Simple', N'Мы увидим вас там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (111, N'He will catch us there.', N'Future Simple', N'Он поймает нас там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (112, N'I''ll tell you there.', N'Future Simple', N'Я расскажу тебе там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (113, N'I''ll join you there.', N'Future Simple', N'Я присоединюсь к вам там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (114, N'I will show you there.', N'Future Simple', N'Я покажу вам там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (115, N'They''ll meet us there.', N'Future Simple', N'Они встретят нас там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (116, N'I''ll contact him there.', N'Future Simple', N'Я свяжусь с ним там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (117, N'Will you be silent?', N'Future Simple', N'Вы будете молчаливыми?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (118, N'Will you be ready?', N'Future Simple', N'Вы будете готовы?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (119, N'Will it be cool?', N'Future Simple', N'Это будет круто?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (120, N'Will you be okay?', N'Future Simple', N'Вы будете в порядке?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (121, N'Will you be afraid?', N'Future Simple', N'Вы будете напуганы?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (122, N'Will you be comfortable?', N'Future Simple', N'Вам будет удобно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (123, N'Will it be safe?', N'Future Simple', N'Это будет безопасно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (124, N'Will she be okay?', N'Future Simple', N'Она будет в порядке?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (125, N'Will it be dangerous?', N'Future Simple', N'Это будет опасно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (126, N'Will I be handsome?', N'Future Simple', N'Я буду красивым?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (127, N'Will we be safe?', N'Future Simple', N'Мы будем в безопасности?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (128, N'Will you be serious?', N'Future Simple', N'Ты будешь серьезным?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (129, N'Will it look real?', N'Future Simple', N'Будет ли это выглядеть реально?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (130, N'Will you be free?', N'Future Simple', N'Вы будете свободными?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (131, N'Will it be difficult?', N'Future Simple', N'Это будет сложно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (132, N'Will it look normal?', N'Future Simple', N'Это будет выглядеть нормально?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (133, N'Will they be alive?', N'Future Simple', N'Они будут живы?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (134, N'Will you be wise?', N'Future Simple', N'Ты будешь мудрым?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (135, N'Will you be happy?', N'Future Simple', N'Будешь ли ты счастлив?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (136, N'Will you be reasonable?', N'Future Simple', N'Ты будешь разумным?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (137, N'This will not work.', N'Future Simple', N'Это не будет работать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (138, N'This won''t help.', N'Future Simple', N'Это не поможет.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (139, N'This won''t hurt!', N'Future Simple', N'Это не будет больно.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (140, N'This won''t last.', N'Future Simple', N'Это не продлится.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (141, N'I will not argue about it.', N'Future Simple', N'Я не буду спорить об этом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (142, N'I will not speak with her.', N'Future Simple', N'Я не буду разговаривать с ней.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (143, N'I won''t look at her.', N'Future Simple', N'Я не буду смотреть на неё.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (144, N'You won''t look at him.', N'Future Simple', N'Ты не будешь смотреть на него.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (145, N'We won''t forget about you.', N'Future Simple', N'Мы не забудем о вас.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (146, N'She won''t look at me.', N'Future Simple', N'Она не будет смотреть на меня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (147, N'They won''t forget about you.', N'Future Simple', N'Они не забудут о тебе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (148, N'I won''t look at you.', N'Future Simple', N'Я не буду смотреть на тебя.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (149, N'I won''t leave without him.', N'Future Simple', N'Я не уйду без него.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (150, N'I won''t go through them.', N'Future Simple', N'Я не пройду через них.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (151, N'He will not go without you.', N'Future Simple', N'Он не пойдёт без тебя.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (152, N'We won''t argue about it.', N'Future Simple', N'Мы не будем спорить об этом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (153, N'He won''t look at me.', N'Future Simple', N'Он не посмотрит на меня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (154, N'I will not live without her.', N'Future Simple', N'Я не буду жить без неё.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (155, N'We won''t leave without you.', N'Future Simple', N'Мы не уедем без вас.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (156, N'I will not choose between them.', N'Future Simple', N'Я не выберу между ними.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (157, N'It''s 20 thousand dollars.', N'Numerals', N'Это 20 тысяч долларов.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (158, N'He has ten thousand men.', N'Numerals', N'У него есть десять тысяч человек.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (159, N'It costs 4 million dollars.', N'Numerals', N'Это стоит 4 миллиона долларов.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (160, N'It''s two thousand bucks.', N'Numerals', N'Это стоит две тысячи баксов.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (161, N'It holds five million books.', N'Numerals', N'Это вмещает в себя пять миллионов книг.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (162, N'It''s one hundred francs.', N'Numerals', N'Это сто франков.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (163, N'It''s 10 million miles.', N'Numerals', N'Это 10 миллионов миль.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (164, N'It''s ten thousand pounds.', N'Numerals', N'Это стоит десять тысяч фунтов.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (165, N'It''s five thousand dollars.', N'Numerals', N'Это пять тысяч долларов.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (166, N'I''m searching for something', N'Present Continuous', N'Я ищу кое-что.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (167, N'You''re looking for support', N'Present Continuous', N'Вы ищете поддержку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (168, N'I''m waiting for someone.', N'Present Continuous', N'Я жду кого-то.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (169, N'I''m looking for something.', N'Present Continuous', N'Я ищу кое-что.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (170, N'You''re searching for proof', N'Present Continuous', N'Вы ищете доказательство.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (171, N'We''re falling in love', N'Present Continuous', N'Мы влюбляемся.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (172, N'I''m talking about freedom', N'Present Continuous', N'Я говорю о свободе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (173, N'I''m bankrolling this party.', N'Present Continuous', N'Я финансирую эту вечеринку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (174, N'You are making a mistake.', N'Present Continuous', N'Вы делаете ошибку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (175, N'Are you going home?', N'Present Continuous', N'Вы идете домой?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (176, N'Are you having fun?', N'Present Continuous', N'Вам весело?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (177, N'Are you seeing anybody?', N'Present Continuous', N'Ты встречаешься с кем-то?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (178, N'Are they having fun?', N'Present Continuous', N'Они веселятся?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (179, N'Are you wearing makeup?', N'Present Continuous', N'Вы пользуетесь косметикой?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (180, N'Are you working tonight?', N'Present Continuous', N'Вы работаете сегодня вечером?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (181, N'Are you doing anything?', N'Present Continuous', N'Ты делаешь что-нибудь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (182, N'Are you dating anyone?', N'Present Continuous', N'Вы встречаетесь с кем-нибудь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (183, N'Am I interrupting anything?', N'Present Continuous', N'Я прерываю что-то?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (184, N'Are you expecting anyone?', N'Present Continuous', N'Ты ждешь кого-то?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (185, N'Are we going home?', N'Present Continuous', N'Мы едем домой?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (186, N'Are you wearing cologne?', N'Present Continuous', N'Ты пользуешься духами?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (187, N'Are you wearing lipstick?', N'Present Continuous', N'Ты пользуешься помадой?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (188, N'Am I doing this right?', N'Present Continuous', N'Я делаю это правильно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (189, N'Are you expecting a letter?', N'Present Continuous', N'Вы ждете письмо?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (190, N'Are you avoiding the question?', N'Present Continuous', N'Вы уходите от ответа?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (191, N'Are you building a boat?', N'Present Continuous', N'Вы строите лодку?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (192, N'Are you having a party?', N'Present Continuous', N'У вас вечеринка?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (193, N'Are you telling the truth?', N'Present Continuous', N'Ты говоришь правду?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (194, N'Are you having a stroke?', N'Present Continuous', N'У вас инсульт?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (195, N'They''re not coming back.', N'Present Continuous', N'Они не возвращаются.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (196, N'I''m not going anywhere.', N'Present Continuous', N'Я не иду никуда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (197, N'I''m not asking much.', N'Present Continuous', N'Я не прошу многого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (198, N'We''re not going anywhere.', N'Present Continuous', N'Мы не идём никуда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (199, N'You''re not coming back.', N'Present Continuous', N'Ты не возвращаешься.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (200, N'You''re not staying here.', N'Present Continuous', N'Ты не останешься здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (201, N'You''re not going anywhere!', N'Present Continuous', N'Ты не идешь никуда!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (202, N'I''m not feeling well.', N'Present Continuous', N'Я плохо себя чувствую.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (203, N'We''re not living together.', N'Present Continuous', N'Мы не живем вместе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (204, N'I''m not coming back.', N'Present Continuous', N'Я не возвращаюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (205, N'I''m not staying here.', N'Present Continuous', N'Я не останусь здесь.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (206, N'I''m not running away.', N'Present Continuous', N'Я не убегаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (207, N'You''re not thinking clearly.', N'Present Continuous', N'Ты не мыслишь ясно.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (208, N'You''re not going alone.', N'Present Continuous', N'Ты не идёшь один.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (209, N'I am not going anywhere.', N'Present Continuous', N'Я не иду никуда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (210, N'You''re not missing much.', N'Present Continuous', N'Ты не теряешь многого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (211, N'I''m not walking away.', N'Present Continuous', N'Я не ухожу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (212, N'I''m not going there.', N'Present Continuous', N'Я не иду туда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (213, N'I''m not going away.', N'Present Continuous', N'Я не сбегаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (214, N'I''m not turning back.', N'Present Continuous', N'Я не отступаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (215, N'We were sitting in the library.', N'Past Continuous', N'Мы сидели в библиотеке.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (216, N'He was looking at the ground.', N'Past Continuous', N'Он смотрел на землю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (217, N'I was standing on a hill.', N'Past Continuous', N'Я стоял на холме.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (218, N'She was coming in the room.', N'Past Continuous', N'Она входила в комнату.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (219, N'She was talking about the party.', N'Past Continuous', N'Она говорила об этой вечеринке.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (220, N'He was talking about the weather.', N'Past Continuous', N'Он говорил о погоде.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (221, N'He was sitting by a wall.', N'Past Continuous', N'Он сидел у стены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (222, N'She was waiting for a bus.', N'Past Continuous', N'Она ждала автобус.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (223, N'He was yelling into the phone.', N'Past Continuous', N'Он кричал в телефон.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (224, N'He was not studying medicine.', N'Past Continuous', N'Он не изучал медицину.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (225, N'They weren''t working today.', N'Past Continuous', N'Они не работали сегодня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (226, N'I wasn''t bothering anybody.', N'Past Continuous', N'Я не беспокоил никого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (227, N'She wasn''t going home.', N'Past Continuous', N'Она не шла домой.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (228, N'I wasn''t doing anything!', N'Past Continuous', N'Я не делал ничего!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (229, N'He wasn''t doing anything.', N'Past Continuous', N'Он не делал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (230, N'He wasn''t attacking anyone.', N'Past Continuous', N'Он не нападал ни на кого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (231, N'I wasn''t meeting anyone.', N'Past Continuous', N'Я не встречался ни с кем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (232, N'We weren''t avoiding anything.', N'Past Continuous', N'Мы не избегали ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (233, N'She wasn''t dating anyone.', N'Past Continuous', N'Она не встречалась ни с кем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (234, N'They weren''t doing anything.', N'Past Continuous', N'Они не делали ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (235, N'I wasn''t planning anything.', N'Past Continuous', N'Я не планировал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (236, N'He wasn''t saying anything.', N'Past Continuous', N'Он не говорил ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (237, N'He wasn''t sharing information.', N'Past Continuous', N'Он не делился информацией.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (238, N'I wasn''t asking anything.', N'Past Continuous', N'Я не спрашивал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (239, N'It wasn''t raining yesterday.', N'Past Continuous', N'Дождь не шёл вчера.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (240, N'We weren''t having fun.', N'Past Continuous', N'Мы не веселились.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (241, N'You weren''t saying anything.', N'Past Continuous', N'Вы не говорили ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (242, N'I wasn''t going home.', N'Past Continuous', N'Я не шёл домой.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (243, N'We weren''t watching TV.', N'Past Continuous', N'Мы не смотрели телевизор.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (244, N'What was she doing there?', N'Past Continuous', N'Что она делала там?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (245, N'What was he saying then?', N'Past Continuous', N'Что он говорил тогда?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (246, N'What was he doing there?', N'Past Continuous', N'Что он делал там?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (247, N'What were you doing here?', N'Past Continuous', N'Что вы делали здесь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (248, N'What were you doing before?', N'Past Continuous', N'Что вы делали раньше?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (249, N'What were you doing then?', N'Past Continuous', N'Что ты делал тогда?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (250, N'What were we defending there?', N'Past Continuous', N'Что мы защищали там?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (251, N'Who were you calling then?', N'Past Continuous', N'Кому вы звонили тогда?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (252, N'What was he seeking?', N'Past Continuous', N'Что он искал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (253, N'What were you doing?', N'Past Continuous', N'Что вы делали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (254, N'What was she doing?', N'Past Continuous', N'Что она делала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (255, N'What were you thinking?', N'Past Continuous', N'Что вы думали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (256, N'What were you saying?', N'Past Continuous', N'Что ты говорил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (257, N'What were you expecting?', N'Past Continuous', N'Чего вы ожидали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (258, N'What was he doing ?', N'Past Continuous', N'Что он делал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (259, N'What was she thinking?', N'Past Continuous', N'Что она думала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (260, N'What was I doing ?', N'Past Continuous', N'Что я делал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (261, N'What was she hiding?', N'Past Continuous', N'Что она скрывала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (262, N'What were they eating?', N'Past Continuous', N'Что они ели?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (263, N'What was he planning?', N'Past Continuous', N'Что он планировал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (264, N'I''ll be arriving in an hour.', N'Future Continuous', N'Я приеду через час.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (265, N'I will be waiting in the car.', N'Future Continuous', N'Я буду ждать в машине.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (266, N'I''ll be waiting for a call.', N'Future Continuous', N'Я буду ждать звонка.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (267, N'We''ll be sitting at a table', N'Future Continuous', N'Мы будем сидеть за столом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (268, N'We''ll be swimming in the sea', N'Future Continuous', N'Мы будем плавать в море.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (269, N'You''ll be sleeping on the floor.', N'Future Continuous', N'Вы будете спать на полу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (270, N'He''ll be waiting for a response.', N'Future Continuous', N'Он будет ждать ответа.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (271, N'I''ll be starting in the fall.', N'Future Continuous', N'Я начну осенью.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (272, N'He''ll be walking around this town', N'Future Continuous', N'Он будет гулять по этому городу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (273, N'We''ll be dining on the terrace.', N'Future Continuous', N'Мы будем обедать на террасе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (274, N'Will he be leaving soon?', N'Future Continuous', N'Он уезжает скоро?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (275, N'Will you be going back?', N'Future Continuous', N'Вы будете возвращаться?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (276, N'Will you be staying there?', N'Future Continuous', N'Вы будете останавливаться там?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (277, N'Will you be staying long?', N'Future Continuous', N'Вы останетесь надолго?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (278, N'Will you be coming back?', N'Future Continuous', N'Ты приедешь обратно?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (279, N'Will you be going soon?', N'Future Continuous', N'Ты поедешь скоро?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (280, N'Will she be coming alone?', N'Future Continuous', N'Она придёт одна?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (281, N'Will you be leaving soon?', N'Future Continuous', N'Ты уезжаешь скоро?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (282, N'Will I be staying there?', N'Future Continuous', N'Я остановлюсь там?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (283, N'Will you be going there?', N'Future Continuous', N'Вы пойдёте туда?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (284, N'Will you be leaving too?', N'Future Continuous', N'Вы уезжаете тоже?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (285, N'What will we be playing?', N'Future Continuous', N'Что мы будем играть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (286, N'What will I be doing?', N'Future Continuous', N'Что я буду делать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (287, N'What will we be eating?', N'Future Continuous', N'Что мы будем есть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (288, N'What will you be doing?', N'Future Continuous', N'Что ты будешь делать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (289, N'Who will I be playing?', N'Future Continuous', N'Кого я буду играть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (290, N'Who will we be fighting?', N'Future Continuous', N'С кем мы будем драться?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (291, N'And what will I be doing?', N'Future Continuous', N'И что я буду делать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (292, N'And what will you be serving?', N'Future Continuous', N'И что вы будете подавать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (293, N'And what will you be doing?', N'Future Continuous', N'И что вы будете делать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (294, N'I won''t be seeing them again.', N'Future Continuous', N'Я не буду видеться с ними снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (295, N'I won''t be chasing you anymore.', N'Future Continuous', N'Я не буду преследовать вас больше.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (296, N'I won''t be seeing you again.', N'Future Continuous', N'Я не буду видеться с тобой снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (297, N'He won''t be bothering you again.', N'Future Continuous', N'Он не побеспокоит вас снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (298, N'He won''t be bothering me again!', N'Future Continuous', N'Он не побеспокоит меня снова!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (299, N'We won''t be troubling you again.', N'Future Continuous', N'Мы не будем беспокоить вас снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (300, N'We won''t be seeing him again.', N'Future Continuous', N'Мы не будем встречаться с ним снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (301, N'I won''t be seeing you later.', N'Future Continuous', N'Я не буду видеться с вами позже.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (302, N'You won''t be getting it back.', N'Future Continuous', N'Вы не получите это обратно.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (303, N'I won''t be seeing him again.', N'Future Continuous', N'Я не буду видеться с ним снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (304, N'He won''t be wearing it again.', N'Future Continuous', N'Он не будет носить это снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (305, N'You won''t be seeing him again.', N'Future Continuous', N'Вы не будете видеться с ним снова.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (306, N'I won''t be kissing you again.', N'Future Continuous', N'Я не буду целовать вас снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (307, N'I won''t be keeping you anymore.', N'Future Continuous', N'Я не буду задерживать вас больше.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (308, N'I won''t be asking him again.', N'Future Continuous', N'не буду спрашивать его снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (309, N'I won''t be using them again.', N'Future Continuous', N'Я не буду пользоваться ими снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (310, N'He won''t be hitting you again.', N'Future Continuous', N'Он не будет бить вас снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (311, N'I won''t be doing it again.', N'Future Continuous', N'Я не буду делать это снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (312, N'I won''t be eating.', N'Future Continuous', N'Я не буду есть.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (313, N'We won''t be starving.', N'Future Continuous', N'Мы не будем голодать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (314, N'I have asked him.', N'Present Perfect', N'Я спросил его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (315, N'I have solved it.', N'Present Perfect', N'Я решил это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (316, N'You have guessed it.', N'Present Perfect', N'Вы угадали это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (317, N'I''ve tried it.', N'Present Perfect', N'Я пробовал это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (318, N'I have helped you.', N'Present Perfect', N'Я помог тебе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (319, N'I''ve seen them.', N'Present Perfect', N'Я видел их.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (320, N'You''ve guessed it!', N'Present Perfect', N'Вы угадали это!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (321, N'You have forgotten me.', N'Present Perfect', N'Ты забыл меня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (322, N'You''ve convinced me.', N'Present Perfect', N'Вы убедили меня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (323, N'I have returned it.', N'Present Perfect', N'Я вернул это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (324, N'What have you seen?', N'Present Perfect', N'Что вы видели?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (325, N'What have we lost?', N'Present Perfect', N'Что мы потеряли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (326, N'What have you remembered?', N'Present Perfect', N'Что ты вспомнил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (327, N'What have you eaten?', N'Present Perfect', N'Что ты ел?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (328, N'What have I missed?', N'Present Perfect', N'Что я пропустил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (329, N'What have you written?', N'Present Perfect', N'Что ты написал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (330, N'What have we missed?', N'Present Perfect', N'Что мы пропустили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (331, N'What have you discovered?', N'Present Perfect', N'Что вы обнаружили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (332, N'What have you planned?', N'Present Perfect', N'Что ты cпланировал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (333, N'What have they achieved?', N'Present Perfect', N'Чего они достигли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (334, N'What have you experienced?', N'Present Perfect', N'Что вы испытали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (335, N'What have we won?', N'Present Perfect', N'Что мы выиграли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (336, N'What have I learned?', N'Present Perfect', N'Что я выучил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (337, N'What have you noticed?', N'Present Perfect', N'Что ты заметил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (338, N'What have I created?', N'Present Perfect', N'Что я создал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (339, N'What have you brought?', N'Present Perfect', N'Что ты принёс?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (340, N'What have you forgotten?', N'Present Perfect', N'Что ты забыл?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (341, N'What have you arranged?', N'Present Perfect', N'Что вы организовали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (342, N'What have you chosen?', N'Present Perfect', N'Что ты выбрал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (343, N'What have I written?', N'Present Perfect', N'Что я написал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (344, N'I haven''t prepared anything.', N'Present Perfect', N'Я не приготовил ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (345, N'You haven''t heard anything', N'Present Perfect', N'Ты не слышал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (346, N'We haven''t changed anything.', N'Present Perfect', N'Мы не меняли ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (347, N'You haven''t achieved anything.', N'Present Perfect', N'Вы не достигли ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (348, N'I haven''t told anyone.', N'Present Perfect', N'Я не говорил никому.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (349, N'You haven''t missed anything.', N'Present Perfect', N'Вы не пропустили ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (350, N'I haven''t asked anyone.', N'Present Perfect', N'Я не спрашивал никого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (351, N'I''ve never used magic.', N'Present Perfect', N'Я никогда не использовал магию.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (352, N'I haven''t shown anyone.', N'Present Perfect', N'Я не показывал никому.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (353, N'You haven''t seen anything.', N'Present Perfect', N'Вы не видели ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (354, N'We haven''t ordered anything.', N'Present Perfect', N'Мы не заказывали ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (355, N'You''ve never lost anything.', N'Present Perfect', N'Вы никогда не теряли ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (356, N'I have not hidden anything.', N'Present Perfect', N'Я не скрывал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (357, N'I haven''t noticed anything.', N'Present Perfect', N'Я не заметил ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (358, N'I''ve never understood music.', N'Present Perfect', N'Я никогда не понимал музыку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (359, N'We''ve never grown wheat.', N'Present Perfect', N'Мы никогда не выращивали пшеницу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (360, N'I haven''t met anybody.', N'Present Perfect', N'Я не встречал никого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (361, N'You haven''t done anything.', N'Present Perfect', N'Вы не делали ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (362, N'We haven''t told anyone.', N'Present Perfect', N'Мы не говорили никому.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (363, N'I''ve never stolen anything.', N'Present Perfect', N'Я никогда не крал ничего.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (364, N'We''ve been reading.', N'Present Perfect Continuous', N'Мы читаем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (365, N'I have been thinking.', N'Present Perfect Continuous', N'Я думаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (366, N'We''ve been dancing', N'Present Perfect Continuous', N'Мы танцуем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (367, N'You''ve been hiding.', N'Present Perfect Continuous', N'Вы скрываетесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (368, N'They''ve been waiting.', N'Present Perfect Continuous', N'Они ждут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (369, N'I''ve been listening.', N'Present Perfect Continuous', N'Я слушаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (370, N'You''ve been reading', N'Present Perfect Continuous', N'Вы читаете.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (371, N'I''ve been worrying', N'Present Perfect Continuous', N'Я беспокоюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (372, N'They''ve been hiding', N'Present Perfect Continuous', N'Они прячутся.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (373, N'We''ve been discussing', N'Present Perfect Continuous', N'Мы обсуждаем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (374, N'How long have I been sleeping?', N'Present Perfect Continuous', N'Как долго я сплю?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (375, N'How long have I been dreaming?', N'Present Perfect Continuous', N'Как долго я мечтаю?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (376, N'How long have you been writing?', N'Present Perfect Continuous', N'Как долго вы пишете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (377, N'How long have you been trying?', N'Present Perfect Continuous', N'Как долго вы пытаетесь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (378, N'How long have you been talking?', N'Present Perfect Continuous', N'Как долго вы разговариваете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (379, N'How long have you been waiting?', N'Present Perfect Continuous', N'Как долго ты ждёшь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (380, N'How long have we been waiting?', N'Present Perfect Continuous', N'Как долго мы ждём?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (381, N'How long have you been acting?', N'Present Perfect Continuous', N'Как долго вы играете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (382, N'How long have you been drawing?', N'Present Perfect Continuous', N'Как долго ты рисуешь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (383, N'How long have you been playing?', N'Present Perfect Continuous', N'Как долго вы играете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (384, N'How long have you been listening?', N'Present Perfect Continuous', N'Как долго ты слушаешь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (385, N'How long have you been working?', N'Present Perfect Continuous', N'Как долго ты работаешь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (386, N'How long have you been chasing?', N'Present Perfect Continuous', N'Как долго вы преследуете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (387, N'How long have you been hiding?', N'Present Perfect Continuous', N'Как долго вы прячетесь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (388, N'How long have we been driving?', N'Present Perfect Continuous', N'Как долго мы едем?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (389, N'How long have you been teaching?', N'Present Perfect Continuous', N'Как долго вы преподаёте?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (390, N'How long have you been doing?', N'Present Perfect Continuous', N'Как долго вы делаете?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (391, N'How long have we been walking?', N'Present Perfect Continuous', N'Как долго мы гуляем?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (392, N'How long have they been playing?', N'Present Perfect Continuous', N'Как долго они играют?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (393, N'How long have you been standing?', N'Present Perfect Continuous', N'Как долго вы стоите?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (394, N'I haven''t been exercising.', N'Present Perfect Continuous', N'Я не тренируюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (395, N'We haven''t been lying.', N'Present Perfect Continuous', N'не лжём.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (396, N'You haven''t been practicing', N'Present Perfect Continuous', N'не практикуетесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (397, N'I haven''t been dreaming.', N'Present Perfect Continuous', N'Я не мечтаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (398, N'You haven''t been listening.', N'Present Perfect Continuous', N'Вы не слушаете.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (399, N'I haven''t been fighting.', N'Present Perfect Continuous', N'Я не борюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (400, N'I haven''t been swimming', N'Present Perfect Continuous', N'Я не плаваю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (401, N'I haven''t been lying!', N'Present Perfect Continuous', N'Я не вру!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (402, N'You haven''t been listening', N'Present Perfect Continuous', N'Вы не слушаете.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (403, N'I haven''t been hiding.', N'Present Perfect Continuous', N'Я не прячусь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (404, N'I haven''t been running.', N'Present Perfect Continuous', N'Я не бегаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (405, N'I haven''t been sitting.', N'Present Perfect Continuous', N'Я не сижу.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (406, N'I have not been working', N'Present Perfect Continuous', N'Я не работаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (407, N'I haven''t been spying.', N'Present Perfect Continuous', N'Я не шпионю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (408, N'You haven''t been reading', N'Present Perfect Continuous', N'Ты не читаешь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (409, N'I haven''t been trying', N'Present Perfect Continuous', N'Я не пытаюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (410, N'You haven''t been sleeping.', N'Present Perfect Continuous', N'Вы не спите.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (411, N'I haven''t been listening.', N'Present Perfect Continuous', N'Я не слушаю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (412, N'I''ve not been drinking.', N'Present Perfect Continuous', N'Я не пью.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (413, N'We haven''t been trying', N'Present Perfect Continuous', N'Мы не пытаемся.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (414, N'She had deceived him again.', N'Past Perfect', N'Она обманула его снова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (415, N'That girl had given her money.', N'Past Perfect', N'Та девочка отдала ей её деньги.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (416, N'All energy had left his body.', N'Past Perfect', N'Вся энергия покинула его тело.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (417, N'She had suffered from depression.', N'Past Perfect', N'Она страдала от депрессии.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (418, N'They had sung of love.', N'Past Perfect', N'Они спели о любви.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (419, N'He had forgotten that voice.', N'Past Perfect', N'Он забыл тот голос.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (420, N'They had discussed that question.', N'Past Perfect', N'Они обсудили тот вопрос.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (421, N'She had earned that fate.', N'Past Perfect', N'Она заслужила ту судьбу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (422, N'It had been someone else.', N'Past Perfect', N'Это был кто-то другой.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (423, N'They had lost everything else.', N'Past Perfect', N'Они потеряли всё остальное.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (424, N'What had she done?', N'Past Perfect', N'Что она сделала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (425, N'What had he expected?', N'Past Perfect', N'Чего он ожидал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (426, N'What had they done?', N'Past Perfect', N'Что они сделали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (427, N'What had it been?', N'Past Perfect', N'Что это было?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (428, N'What had you done?', N'Past Perfect', N'Что ты сделал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (429, N'What had she worn?', N'Past Perfect', N'Что она носила?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (430, N'What had he observed?', N'Past Perfect', N'Что он наблюдал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (431, N'What had he forgotten?', N'Past Perfect', N'Что он забыл?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (432, N'What had we done?', N'Past Perfect', N'Что мы сделали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (433, N'What had they forgotten?', N'Past Perfect', N'Что они забыли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (434, N'Where had they gone?', N'Past Perfect', N'Куда они ушли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (435, N'Where had she been?', N'Past Perfect', N'Где она была?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (436, N'Where had it been?', N'Past Perfect', N'Где оно было?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (437, N'Why had it altered?', N'Past Perfect', N'Почему оно изменилось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (438, N'Why had I bothered?', N'Past Perfect', N'Почему я беспокоился?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (439, N'Why had he hidden?', N'Past Perfect', N'Почему он спрятался?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (440, N'Why had he bothered?', N'Past Perfect', N'Почему он беспокоился?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (441, N'When had he fallen?', N'Past Perfect', N'Когда он упал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (442, N'Where had it begun?', N'Past Perfect', N'Где это началось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (443, N'When had it begun?', N'Past Perfect', N'Когда это началось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (444, N'They had not forgotten him.', N'Past Perfect', N'Они не забыли его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (445, N'She had not noticed it.', N'Past Perfect', N'Она не заметила этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (446, N'They had not written me', N'Past Perfect', N'Они не написали мне.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (447, N'They had not changed it.', N'Past Perfect', N'Они не изменили это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (448, N'He hadn''t believed them.', N'Past Perfect', N'Он не поверил им.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (449, N'He hadn''t recognized her.', N'Past Perfect', N'Он не узнал её.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (450, N'They hadn''t seen her.', N'Past Perfect', N'Они не видели её.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (451, N'She had not believed him.', N'Past Perfect', N'Она не поверила ему.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (452, N'He had not realized it.', N'Past Perfect', N'Он не осознал это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (453, N'She hadn''t found him.', N'Past Perfect', N'Она не нашла его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (454, N'He hadn''t expected it.', N'Past Perfect', N'Он не ожидал этого.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (455, N'He hadn''t signed it.', N'Past Perfect', N'Он не подписал это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (456, N'He had not introduced himself.', N'Past Perfect', N'Он не представился.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (457, N'It hadn''t bothered him.', N'Past Perfect', N'Это не побеспокоило его.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (458, N'I hadn''t brought it.', N'Past Perfect', N'Я не приносил это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (459, N'He had never considered it.', N'Past Perfect', N'Он никогда не рассматривал это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (460, N'She hadn''t seen them', N'Past Perfect', N'Она не видела их.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (461, N'He hadn''t saved her.', N'Past Perfect', N'Он не спас её.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (462, N'She had not heard it.', N'Past Perfect', N'Она не услышала это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (463, N'She had not told him.', N'Past Perfect', N'Она не сказала ему.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (464, N'They had been dancing all night.', N'Past Perfect Continuous', N'Они танцевали всю ночь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (465, N'She had been working all night.', N'Past Perfect Continuous', N'Она работала всю ночь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (466, N'She had been telling the truth.', N'Past Perfect Continuous', N'Она говорила правду.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (467, N'He had been walking some time.', N'Past Perfect Continuous', N'Он гулял какое-то время.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (468, N'He had been chasing a fantasy.', N'Past Perfect Continuous', N'Он гонялся за фантазией.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (469, N'She had been preparing this speech.', N'Past Perfect Continuous', N'Она готовила эту речь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (470, N'She had been expecting the explosion.', N'Past Perfect Continuous', N'Она ожидала взрыва.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (471, N'He''d been expecting this day.', N'Past Perfect Continuous', N'Он ожидал этого дня.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (472, N'He had been telling the truth.', N'Past Perfect Continuous', N'Он говорил правду.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (473, N'We''d been walking all day.', N'Past Perfect Continuous', N'Мы гуляли весь день.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (474, N'Had I been dreaming?', N'Past Perfect Continuous', N'Я мечтал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (475, N'Had I been shouting?', N'Past Perfect Continuous', N'Я кричал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (476, N'Had you been arguing?', N'Past Perfect Continuous', N'Вы спорили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (477, N'Had she been lying?', N'Past Perfect Continuous', N'Она обманывала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (478, N'Had I been drinking?', N'Past Perfect Continuous', N'Я пил?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (479, N'Had she been listening?', N'Past Perfect Continuous', N'Она слушала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (480, N'Had he been sleeping?', N'Past Perfect Continuous', N'Он спал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (481, N'Had she been crying?', N'Past Perfect Continuous', N'Она плакала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (482, N'Had he been listening?', N'Past Perfect Continuous', N'Он слушал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (483, N'Had he been cheating?', N'Past Perfect Continuous', N'Он обманывал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (484, N'Had he been watching?', N'Past Perfect Continuous', N'Он смотрел?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (485, N'Had he been crying?', N'Past Perfect Continuous', N'Он плакал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (486, N'Had I been screaming?', N'Past Perfect Continuous', N'Я кричала?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (487, N'Had he been practicing?', N'Past Perfect Continuous', N'Он практиковался?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (488, N'What had they been counting on?', N'Past Perfect Continuous', N'На что они рассчитывали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (489, N'What had I been thinking of?', N'Past Perfect Continuous', N'О чём я думал?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (490, N'What had I been worrying about?', N'Past Perfect Continuous', N'О чём я беспокоился?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (491, N'What had they been talking about?', N'Past Perfect Continuous', N'О чём они говорили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (492, N'What had we been talking about?', N'Past Perfect Continuous', N'О чём мы говорили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (493, N'How long had he been watching?', N'Past Perfect Continuous', N'Как долго он смотрел?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (494, N'She had not been walking a minute', N'Past Perfect Continuous', N'Она не гуляла ни минуты.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (495, N'I hadn''t been following the conversation.', N'Past Perfect Continuous', N'Я не следил за разговором.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (496, N'He had not been expecting a kick.', N'Past Perfect Continuous', N'Он не ожидал пинка.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (497, N'He had not been chasing the kid.', N'Past Perfect Continuous', N'Он не преследовал ребёнка.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (498, N'Jennifer had not been listening.', N'Past Perfect Continuous', N'Дженнифер не слушала.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (499, N'Charles had not been attending.', N'Past Perfect Continuous', N'Чарльз не присутствовал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (500, N'Dad hadn''t been listening.', N'Past Perfect Continuous', N'Папа не слушал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (501, N'They had not been playing', N'Past Perfect Continuous', N'Они не играли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (502, N'They had not been crying.', N'Past Perfect Continuous', N'Они не плакали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (503, N'He had not been running.', N'Past Perfect Continuous', N'Он не бегал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (504, N'He hadn''t been sleeping.', N'Past Perfect Continuous', N'Он не спал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (505, N'She had not been working.', N'Past Perfect Continuous', N'Она не работала.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (506, N'She hadn''t been winning.', N'Past Perfect Continuous', N'Она не побеждала.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (507, N'I hadn''t been listening.', N'Past Perfect Continuous', N'Я не слушал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (508, N'He hadn''t been joking.', N'Past Perfect Continuous', N'Он не шутил.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (509, N'She had not been attending.', N'Past Perfect Continuous', N'Она не присутствовала.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (510, N'We hadn''t been waiting', N'Past Perfect Continuous', N'Мы не ждали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (511, N'He hadn''t been working.', N'Past Perfect Continuous', N'Он не работал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (512, N'He hadn''t been counting', N'Past Perfect Continuous', N'Он не считал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (513, N'He hadn''t been listening.', N'Past Perfect Continuous', N'Он не слушал.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (514, N'They said he would feel better.', N'Future in the past', N'Они сказали, что он будет чувствовать себя лучше.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (515, N'They knew we would be here.', N'Future in the past', N'Они знали, что мы будем здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (516, N'I forgot you''d come.', N'Future in the past', N'Я забыл, что вы придёте.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (517, N'I expected he would leave.', N'Future in the past', N'Я ожидал, что он уедет.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (518, N'I knew he''d call back.', N'Future in the past', N'Я знал, что он перезвонит.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (519, N'I knew he would hear.', N'Future in the past', N'Я знала, что он услышит.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (520, N'I thought it would work.', N'Future in the past', N'Я думал, что это сработает.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (521, N'I knew you would be here.', N'Future in the past', N'Я знала, что вы будете здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (522, N'I thought it would end.', N'Future in the past', N'Я думал, что это закончится.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (523, N'We knew we would win.', N'Future in the past', N'Мы знали, что мы победим.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (524, N'We are going to release you.', N'To be going to', N'Мы собираемся освободить вас.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (525, N'I am going to tell you.', N'To be going to', N'Я собираюсь сказать вам.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (526, N'I''m going to criticize them.', N'To be going to', N'Я собираюсь критиковать их.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (527, N'I''m going to catch them!', N'To be going to', N'Я собираюсь поймать их!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (528, N'I''m going to read it.', N'To be going to', N'Я собираюсь прочитать это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (529, N'I''m going to find them.', N'To be going to', N'Я собираюсь найти их.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (530, N'I''m going to return it.', N'To be going to', N'Я собираюсь вернуть это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (531, N'I''m going to correct it.', N'To be going to', N'Я собираюсь исправить это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (532, N'I''m going to punish you.', N'To be going to', N'Я собираюсь наказать тебя.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (533, N'I''m going to sell it.', N'To be going to', N'Я собираюсь продать это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (534, N'What are you going to say?', N'To be going to', N'Что вы собираетесь сказать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (535, N'What are you going to give?', N'To be going to', N'Что ты собираешься дать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (536, N'Who''re we going to tell?', N'To be going to', N'Кому мы собираемся рассказать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (537, N'What are you going to use?', N'To be going to', N'ты собираешься использовать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (538, N'What are we going to achieve?', N'To be going to', N'Чего мы собираемся достичь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (539, N'What are we going to lose?', N'To be going to', N'Что мы собираемся потерять?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (540, N'What are you going to find?', N'To be going to', N'Что ты собираешься найти?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (541, N'What are you going to study?', N'To be going to', N'Что вы собираетесь изучать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (542, N'Who am I going to call?', N'To be going to', N'Кому я собираюсь позвонить?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (543, N'Who are you going to believe?', N'To be going to', N'Кому вы собираетесь поверить?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (544, N'What are we going to say?', N'To be going to', N'Что мы собираемся сказать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (545, N'What are you going to eat?', N'To be going to', N'Что ты собираешься съесть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (546, N'What are you going to write?', N'To be going to', N'Что ты собираешься написать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (547, N'Who am I going to tell?', N'To be going to', N'Кому я собираюсь сказать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (548, N'What are you going to sing?', N'To be going to', N'Что ты собираешься спеть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (549, N'What are you going to try?', N'To be going to', N'Что ты собираешься попробовать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (550, N'What are you going to print?', N'To be going to', N'Что ты собираешься распечатать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (551, N'What are you going to make?', N'To be going to', N'Что ты собираешься создать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (552, N'Who am I going to be?', N'To be going to', N'Кем я собираюсь быть?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (553, N'What are they going to say?', N'To be going to', N'Что они собираются сказать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (554, N'I''m not going to sell this company.', N'To be going to', N'Я не собираюсь продавать эту компанию.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (555, N'I''m not going to wear a hat.', N'To be going to', N'Я не собираюсь носить шляпу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (556, N'I''m not going to drink this poison.', N'To be going to', N'Я не собираюсь пить этот яд.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (557, N'You''re not going to catch any disease.', N'To be going to', N'Вы не собираетесь подхватить какую-нибудь болезнь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (558, N'I''m not going to marry the prince!', N'To be going to', N'Я не собираюсь выходить замуж за принца!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (559, N'I''m not going to rob the shop.', N'To be going to', N'Я не собираюсь грабить магазин.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (560, N'I''m not going to take the scholarship.', N'To be going to', N'Я не собираюсь принимать стипендию.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (561, N'You''re not going to save the world.', N'To be going to', N'Вы не собираетесь спасать мир.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (562, N'I''m not going to lose any time.', N'To be going to', N'Я не собираюсь терять время.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (563, N'I''m not going to serve any dinner.', N'To be going to', N'Я не собираюсь накрывать какой-либо ужин.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (564, N'I''m not going to discuss the matter.', N'To be going to', N'Я не собираюсь обсуждать вопрос.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (565, N'I''m not going to be a doctor.', N'To be going to', N'Я не собираюсь быть врачом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (566, N'I''m not going to leave the car.', N'To be going to', N'Я не собираюсь оставлять машину.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (567, N'We''re not going to see the palace.', N'To be going to', N'Мы не собираемся осматривать дворец.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (568, N'You are not going to make the mistake', N'To be going to', N'Ты не собираешься совершать ошибку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (569, N'You are not going to believe this story.', N'To be going to', N'Ты не собираешься поверить этой истории.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (570, N'I''m not going to be a coward.', N'To be going to', N'Я не собираюсь быть трусом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (571, N'I''m not going to do any work.', N'To be going to', N'Я не собираюсь делать какую-либо работу.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (572, N'I am not going to be a spy.', N'To be going to', N'Я не собираюсь быть шпионом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (573, N'You aren''t going to lose any power.', N'To be going to', N'Вы не собираетесь терять какую-либо власть.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (574, N'The house is deserted.', N'Passive Voice - Present Simple', N'Дом заброшен.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (575, N'The battle is won.', N'Passive Voice - Present Simple', N'Битва выиграна.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (576, N'The matter is closed.', N'Passive Voice - Present Simple', N'Вопрос закрыт.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (577, N'The decision is made.', N'Passive Voice - Present Simple', N'Решение принимается.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (578, N'The way is shut.', N'Passive Voice - Present Simple', N'Путь закрыт.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (579, N'The meeting is adjourned.', N'Passive Voice - Present Simple', N'Заседание откладывается.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (580, N'The picture is finished.', N'Passive Voice - Present Simple', N'Картина закончена.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (581, N'The world is changed.', N'Passive Voice - Present Simple', N'Мир меняется.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (582, N'This meeting is adjourned.', N'Passive Voice - Present Simple', N'Эта встреча перенесена.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (583, N'The door is locked.', N'Passive Voice - Present Simple', N'Дверь закрыта.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (584, N'Am I understood?', N'Passive Voice - Present Simple', N'Меня понимают?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (585, N'Am I fired?', N'Passive Voice - Present Simple', N'Я уволен?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (586, N'Are you armed?', N'Passive Voice - Present Simple', N'Вы вооружены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (587, N'Are you shot?', N'Passive Voice - Present Simple', N'Ты ранен?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (588, N'Are you wounded?', N'Passive Voice - Present Simple', N'Ты ранен?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (589, N'Am I forgiven?', N'Passive Voice - Present Simple', N'Я прощён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (590, N'Am I invited?', N'Passive Voice - Present Simple', N'Я приглашён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (591, N'Is it broken?', N'Passive Voice - Present Simple', N'Это сломано?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (592, N'Is she injured?', N'Passive Voice - Present Simple', N'Она ранена?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (593, N'Is it fixed?', N'Passive Voice - Present Simple', N'Это закреплено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (594, N'Is it locked?', N'Passive Voice - Present Simple', N'Оно заперто?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (595, N'Is breakfast included?', N'Passive Voice - Present Simple', N'Завтрак включён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (596, N'Is anything broken?', N'Passive Voice - Present Simple', N'Что-то разбито?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (597, N'Is war declared?', N'Passive Voice - Present Simple', N'Война объявлена?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (598, N'Why is the door locked?', N'Passive Voice - Present Simple', N'Почему дверь закрыта?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (599, N'How is this word pronounced?', N'Passive Voice - Present Simple', N'Как это слово произносят?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (600, N'Where is the rifle pointed?', N'Passive Voice - Present Simple', N'Куда винтовка направлена?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (601, N'Where is the rope attached?', N'Passive Voice - Present Simple', N'Куда верёвку крепят?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (602, N'Why is this door locked?', N'Passive Voice - Present Simple', N'Почему эта дверь заперта?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (603, N'Why is this room hidden?', N'Passive Voice - Present Simple', N'Почему эта комната спрятана?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (604, N'That ship is not built for speed.', N'Passive Voice - Present Simple', N'Тот корабль не строится для скорости.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (605, N'This booth is not equipped for microsurgery.', N'Passive Voice - Present Simple', N'Эта кабина не оборудована для микрохирургии.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (606, N'This idea is not based on anything', N'Passive Voice - Present Simple', N'Эта мысль не основывается ни на чём.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (607, N'The stove isn''t lit till evening.', N'Passive Voice - Present Simple', N'Печь не зажигают до вечера.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (608, N'This jam is not made with feijoa.', N'Passive Voice - Present Simple', N'Этот джем не делают с фейхоа.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (609, N'It''s not derived from any theory.', N'Passive Voice - Present Simple', N'Это не происходит из какой-либо теории.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (610, N'It''s not needed in the sentence.', N'Passive Voice - Present Simple', N'Это не нужно в предложении.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (611, N'It''s not needed in the conversation.', N'Passive Voice - Present Simple', N'Это не нужно в этой беседе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (612, N'He''s not prepared for this situation.', N'Passive Voice - Present Simple', N'Он не подготовлен для этой ситуации.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (613, N'It''s not taught as a science.', N'Passive Voice - Present Simple', N'Это не преподают как науку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (614, N'It is not marked upon the map.', N'Passive Voice - Present Simple', N'Это не отмечено на карте.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (615, N'I hope they''re not ruined.', N'Passive Voice - Present Simple', N'Я надеюсь, они не разрушены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (616, N'I think I''m not fired.', N'Passive Voice - Present Simple', N'Я думаю, меня не увольняют.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (617, N'You know you''re not allowed', N'Passive Voice - Present Simple', N'Вы знаете, что вам не разрешается.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (618, N'I guess we''re not invited', N'Passive Voice - Present Simple', N'Я догадываюсь, что нас не пригласили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (619, N'But I''m not paid for it.', N'Passive Voice - Present Simple', N'Но мне не платят за это.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (620, N'I''m not often invited to parties.', N'Passive Voice - Present Simple', N'Меня нечасто приглашают на вечеринки.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (621, N'Details are not provided in this example.', N'Passive Voice - Present Simple', N'Подробности не представлены в этом примере.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (622, N'Stamps aren''t sold in a supermarket.', N'Passive Voice - Present Simple', N'Марки не продаются в супермаркете.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (623, N'This situation is not observed in the private sector.', N'Passive Voice - Present Simple', N'Эта ситуация не наблюдается в частном секторе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (624, N'I was chosen.', N'Passive Voice - Past Simple', N'Меня выбрали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (625, N'It was done.', N'Passive Voice - Past Simple', N'Оно было сделано.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (626, N'He was poisoned.', N'Passive Voice - Past Simple', N'Его отравили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (627, N'They were separated.', N'Passive Voice - Past Simple', N'Они были разделены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (628, N'I was told.', N'Passive Voice - Past Simple', N'Мне сказали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (629, N'It was confiscated.', N'Passive Voice - Past Simple', N'Оно было конфисковано.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (630, N'We were betrayed.', N'Passive Voice - Past Simple', N'Нас предали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (631, N'It was accomplished.', N'Passive Voice - Past Simple', N'Оно было достигнуто.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (632, N'It was shut.', N'Passive Voice - Past Simple', N'Оно было заперто.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (633, N'He was destroyed.', N'Passive Voice - Past Simple', N'Он был уничтожен.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (634, N'What poison was injected?', N'Passive Voice - Past Simple', N'Какой яд был введён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (635, N'What magic was used?', N'Passive Voice - Past Simple', N'Какая магия использовалась?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (636, N'What time was it found?', N'Passive Voice - Past Simple', N'В какое время оно было найдено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (637, N'What year were you born?', N'Passive Voice - Past Simple', N'В каком году ты родился?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (638, N'Was it agreed?', N'Passive Voice - Past Simple', N'Это было согласовано?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (639, N'Were you asked?', N'Passive Voice - Past Simple', N'Тебя спрашивали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (640, N'Was she forced?', N'Passive Voice - Past Simple', N'Её заставили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (641, N'Was he poisoned?', N'Passive Voice - Past Simple', N'Он был отравлен?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (642, N'Were you offended?', N'Passive Voice - Past Simple', N'Вы были оскорблены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (643, N'Was it advertised?', N'Passive Voice - Past Simple', N'Оно рекламировалось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (644, N'Was he disturbed?', N'Passive Voice - Past Simple', N'Его побеспокоили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (645, N'Were we loved?', N'Passive Voice - Past Simple', N'Мы были любимы?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (646, N'Was it fulfilled?', N'Passive Voice - Past Simple', N'Оно было выполнено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (647, N'Were they rescued?', N'Passive Voice - Past Simple', N'Они были спасены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (648, N'Were they launched?', N'Passive Voice - Past Simple', N'Они были запущены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (649, N'Were they paid?', N'Passive Voice - Past Simple', N'Им заплатили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (650, N'Were we followed?', N'Passive Voice - Past Simple', N'За нами следовали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (651, N'Was it poisoned?', N'Passive Voice - Past Simple', N'Оно было отравлено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (652, N'Were you fired?', N'Passive Voice - Past Simple', N'Тебя уволили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (653, N'Were we baptized?', N'Passive Voice - Past Simple', N'Нас крестили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (654, N'It wasn''t locked.', N'Passive Voice - Past Simple', N'Оно не было заперто.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (655, N'They were not forgotten.', N'Passive Voice - Past Simple', N'Они не были забыты.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (656, N'I wasn''t invited.', N'Passive Voice - Past Simple', N'Я не был приглашён.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (657, N'It wasn''t planned.', N'Passive Voice - Past Simple', N'Это не было запланировано.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (658, N'He wasn''t invited.', N'Passive Voice - Past Simple', N'Он не был приглашён.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (659, N'I was not told.', N'Passive Voice - Past Simple', N'Мне не сказали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (660, N'We weren''t told much.', N'Passive Voice - Past Simple', N'Нам не рассказали много.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (661, N'They weren''t treated badly.', N'Passive Voice - Past Simple', N'С ними не обращались плохо.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (662, N'I wasn''t born there.', N'Passive Voice - Past Simple', N'Я не родился там.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (663, N'He wasn''t blown away.', N'Passive Voice - Past Simple', N'Его не сдуло.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (664, N'She wasn''t born here.', N'Passive Voice - Past Simple', N'Она не родилась здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (665, N'I wasn''t invited anyway.', N'Passive Voice - Past Simple', N'Меня не приглашали всё равно.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (666, N'It wasn''t done yet.', N'Passive Voice - Past Simple', N'Оно не было сделано ещё.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (667, N'She wasn''t medicated anymore.', N'Passive Voice - Past Simple', N'Её не лечили больше.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (668, N'We weren''t brought here.', N'Passive Voice - Past Simple', N'Нас не привезли сюда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (669, N'We weren''t raised together.', N'Passive Voice - Past Simple', N'Мы не воспитывались вместе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (670, N'He was not invited there.', N'Passive Voice - Past Simple', N'Его не приглашали туда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (671, N'It wasn''t healed yet.', N'Passive Voice - Past Simple', N'Оно не было вылечено ещё.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (672, N'He wasn''t expected anywhere.', N'Passive Voice - Past Simple', N'Его не ожидали нигде.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (673, N'They were not made here.', N'Passive Voice - Past Simple', N'Они не были сделаны здесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (674, N'His promises will be fulfilled.', N'Passive Voice - Future Simple', N'Его обещания будут выполнены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (675, N'Your sins will be forgiven.', N'Passive Voice - Future Simple', N'Ваши грехи будут прощены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (676, N'Her interests will be protected.', N'Passive Voice - Future Simple', N'Её интересы будут защищать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (677, N'Your words will be heard', N'Passive Voice - Future Simple', N'Ваши слова будут услышаны.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (678, N'Our prisoners will be released.', N'Passive Voice - Future Simple', N'Наши заключённые будут освобождены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (679, N'Your goods will be confiscated.', N'Passive Voice - Future Simple', N'Ваши товары будут конфискованы.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (680, N'Their names will be lost.', N'Passive Voice - Future Simple', N'Их имена будут потеряны.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (681, N'Your thoughts will be forgotten.', N'Passive Voice - Future Simple', N'Твои мысли будут забыты.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (682, N'Your photos will be returned', N'Passive Voice - Future Simple', N'Ваши фотографии вернут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (683, N'Your questions will be answered.', N'Passive Voice - Future Simple', N'Ваши вопросы будут отвечены.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (684, N'Will I be known as the philosopher?', N'Passive Voice - Future Simple', N'Меня будут знать как философа?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (685, N'Will we be arrested on the spot?', N'Passive Voice - Future Simple', N'Нас арестуют на месте?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (686, N'Will you be recognized?', N'Passive Voice - Future Simple', N'Вас узнают?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (687, N'Will it be closed?', N'Passive Voice - Future Simple', N'Оно будет закрыто?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (688, N'Will they be solved?', N'Passive Voice - Future Simple', N'Они будут решены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (689, N'Will we be invited?', N'Passive Voice - Future Simple', N'Мы будем приглашены?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (690, N'Will he be recognized?', N'Passive Voice - Future Simple', N'Его узнают?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (691, N'Will he be arrested?', N'Passive Voice - Future Simple', N'Он будет арестован?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (692, N'Will he be released?', N'Passive Voice - Future Simple', N'Он будет освобождён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (693, N'Will I be forgiven?', N'Passive Voice - Future Simple', N'Я буду прощён?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (694, N'Will we be remembered?', N'Passive Voice - Future Simple', N'Нас будут помнить?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (695, N'Will it be found?', N'Passive Voice - Future Simple', N'Оно будет найдено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (696, N'Will he be punished?', N'Passive Voice - Future Simple', N'Он будет наказан?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (697, N'Will I be punished?', N'Passive Voice - Future Simple', N'Я буду наказан?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (698, N'How will the money be spent?', N'Passive Voice - Future Simple', N'Как будут потрачены деньги?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (699, N'How will the information be used?', N'Passive Voice - Future Simple', N'Как будет использоваться информация?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (700, N'How will this material be distributed?', N'Passive Voice - Future Simple', N'Как этот материал будет распространяться?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (701, N'When will the film be released?', N'Passive Voice - Future Simple', N'Когда будет выпущен фильм?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (702, N'When will this feature be used?', N'Passive Voice - Future Simple', N'Когда будет использоваться эта функция?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (703, N'Will we be permitted to go, Father?', N'Passive Voice - Future Simple', N'Нам разрешат идти, отец?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (704, N'I will not be interrupted.', N'Passive Voice - Future Simple', N'Меня не будут перебивать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (705, N'He won''t be used.', N'Passive Voice - Future Simple', N'Его не будут использовать.')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (706, N'I will not be cheated!', N'Passive Voice - Future Simple', N'Я не буду обманут!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (707, N'We will not be cheated.', N'Passive Voice - Future Simple', N'Нас не обманут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (708, N'They will not be found', N'Passive Voice - Future Simple', N'Их не найдут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (709, N'I will not be questioned.', N'Passive Voice - Future Simple', N'Я не буду допрошен.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (710, N'You will not be harmed.', N'Passive Voice - Future Simple', N'Вам не навредят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (711, N'They will not be remembered.', N'Passive Voice - Future Simple', N'Их не вспомнят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (712, N'I will not be refused.', N'Passive Voice - Future Simple', N'Мне не откажут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (713, N'I will not be offended.', N'Passive Voice - Future Simple', N'Я не буду обижен.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (714, N'He won''t be offended.', N'Passive Voice - Future Simple', N'Он не будет обижен.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (715, N'I will not be threatened.', N'Passive Voice - Future Simple', N'Меня не запугают.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (716, N'It will not be repeated.', N'Passive Voice - Future Simple', N'Это не будут повторять.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (717, N'You won''t be understood.', N'Passive Voice - Future Simple', N'Тебя не поймут.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (718, N'We won''t be bothered.', N'Passive Voice - Future Simple', N'Нас не побеспокоят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (719, N'You will never be replaced', N'Passive Voice - Future Simple', N'Вас никогда не заменят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (720, N'They won''t be noticed.', N'Passive Voice - Future Simple', N'Их не заметят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (721, N'It will not be delayed.', N'Passive Voice - Future Simple', N'Оно не будет отложено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (722, N'I will not be blackmailed.', N'Passive Voice - Future Simple', N'Меня не будут шантажировать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (723, N'We''ll never be divided', N'Passive Voice - Future Simple', N'Нас никогда не разделят.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (724, N'My purse has been stolen.', N'Passive Voice - Present Perfect', N'Мой кошелёк украли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (725, N'His student has been attacked.', N'Passive Voice - Present Perfect', N'Его студента атаковали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (726, N'Your car has been stolen.', N'Passive Voice - Present Perfect', N'Вашу машину украли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (727, N'Your absence has been noted.', N'Passive Voice - Present Perfect', N'Ваше отсутствие отметили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (728, N'My decision has been made.', N'Passive Voice - Present Perfect', N'Моё решение принято.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (729, N'Your book has been published!', N'Passive Voice - Present Perfect', N'Ваша книга опубликована!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (730, N'My son has been betrayed.', N'Passive Voice - Present Perfect', N'Моего сына предали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (731, N'Your schedule has been changed.', N'Passive Voice - Present Perfect', N'Ваше расписание изменено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (732, N'Their home has been destroyed', N'Passive Voice - Present Perfect', N'Их дом уничтожили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (733, N'My husband has been found.', N'Passive Voice - Present Perfect', N'Моего мужа нашли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (734, N'Has he been warned?', N'Passive Voice - Present Perfect', N'Его предупредили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (735, N'Has he been threatened?', N'Passive Voice - Present Perfect', N'Ему угрожали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (736, N'Has he been caught?', N'Passive Voice - Present Perfect', N'Его поймали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (737, N'Has she been bitten?', N'Passive Voice - Present Perfect', N'Её укусили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (738, N'Has he been found?', N'Passive Voice - Present Perfect', N'Его нашли?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (739, N'Has he been arrested?', N'Passive Voice - Present Perfect', N'Его арестовали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (740, N'Has she been transferred?', N'Passive Voice - Present Perfect', N'Её перевели?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (741, N'Has she been harmed?', N'Passive Voice - Present Perfect', N'Ей навредили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (742, N'Has it been replaced?', N'Passive Voice - Present Perfect', N'Оно заменено?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (743, N'Has he been chosen?', N'Passive Voice - Present Perfect', N'Его выбрали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (744, N'Has he been poisoned?', N'Passive Voice - Present Perfect', N'Его отравили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (745, N'Has he been released?', N'Passive Voice - Present Perfect', N'Его освободили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (746, N'Has it been proved?', N'Passive Voice - Present Perfect', N'Это было доказано?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (747, N'Has he been called?', N'Passive Voice - Present Perfect', N'Ему позвонили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (748, N'Has he been told?', N'Passive Voice - Present Perfect', N'Ему сказали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (749, N'Has he been fired?', N'Passive Voice - Present Perfect', N'Его уволили?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (750, N'Has he ever been arrested?', N'Passive Voice - Present Perfect', N'Его когда-нибудь арестовывали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (751, N'Has he ever been published?', N'Passive Voice - Present Perfect', N'Его когда-нибудь публиковали?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (752, N'Has it ever been used?', N'Passive Voice - Present Perfect', N'Оно когда-нибудь использовалось?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (753, N'Has it ever been accomplished?', N'Passive Voice - Present Perfect', N'Это когда-либо было достигнуто?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (754, N'It has not been used', N'Passive Voice - Present Perfect', N'Оно не использовалось.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (755, N'She hasn''t been told.', N'Passive Voice - Present Perfect', N'Ей не сказали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (756, N'He hasn''t been found.', N'Passive Voice - Present Perfect', N'Его не нашли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (757, N'She hasn''t been arrested', N'Passive Voice - Present Perfect', N'Её не арестовали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (758, N'She hasn''t been asked.', N'Passive Voice - Present Perfect', N'Её не спросили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (759, N'It hasn''t been confirmed.', N'Passive Voice - Present Perfect', N'Это не было подтверждено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (760, N'It hasn''t been washed.', N'Passive Voice - Present Perfect', N'Оно не было вымыто.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (761, N'It hasn''t been scheduled.', N'Passive Voice - Present Perfect', N'Оно не было назначено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (762, N'She hasn''t been trained.', N'Passive Voice - Present Perfect', N'Её не обучали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (763, N'She has not been forgotten.', N'Passive Voice - Present Perfect', N'Её не забыли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (764, N'It has not been announced.', N'Passive Voice - Present Perfect', N'Это не объявили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (765, N'It''s not been assigned.', N'Passive Voice - Present Perfect', N'Это не было установлено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (766, N'It hasn''t been stolen.', N'Passive Voice - Present Perfect', N'Оно не было украдено.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (767, N'The lawn hasn''t been mowed.', N'Passive Voice - Present Perfect', N'Газон не подстригли.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (768, N'The story has not been changed.', N'Passive Voice - Present Perfect', N'Историю не изменили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (769, N'This room hasn''t been used', N'Passive Voice - Present Perfect', N'Эта комната не использовалась.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (770, N'The future has not been written.', N'Passive Voice - Present Perfect', N'Будущее не написали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (771, N'The winner hasn''t been announced.', N'Passive Voice - Present Perfect', N'Победителя не объявили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (772, N'This information has not been confirmed.', N'Passive Voice - Present Perfect', N'Эта информация не подтвердилась.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (773, N'This place hasn''t been photographed.', N'Passive Voice - Present Perfect', N'Это место не фотографировали.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (774, N'I was expecting your husband.', N'Participles', N'Я ожидал вашего мужа.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (775, N'I was washing my hair.', N'Participles', N'Я мыл свои волосы.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (776, N'I was enjoying our talk.', N'Participles', N'Я наслаждался нашей беседой.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (777, N'I was visiting my sister.', N'Participles', N'Я навещал свою сестру.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (778, N'I was practising my answer.', N'Participles', N'Я практиковал свой ответ.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (779, N'She was helping her sister.', N'Participles', N'Она помогала своей сестре.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (780, N'He was sipping his coffee.', N'Participles', N'Он пил маленькими глотками свой кофе.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (781, N'I was depositing my paycheck.', N'Participles', N'Я клал в банк свою зарплату.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (782, N'I was cleaning your room.', N'Participles', N'Я убирал твою комнату.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (783, N'I was painting my apartment.', N'Participles', N'Я красила свою квартиру.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (784, N'You could feel the man coming.', N'Modal Verbs (Can/Could)', N'Вы могли почувствовать, что мужчина приближается.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (785, N'He could feel the dog shivering.', N'Modal Verbs (Can/Could)', N'Он мог почувствовать, что собака дрожит.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (786, N'He could feel the table trembling', N'Modal Verbs (Can/Could)', N'Он мог почувствовать, как стол вибрирует.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (787, N'She could feel the sub moving.', N'Modal Verbs (Can/Could)', N'Она могла почувствовать, как субмарина движется.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (788, N'She could feel the girl trembling.', N'Modal Verbs (Can/Could)', N'Она могла почувствовать, как девочка дрожит.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (789, N'He could feel the ship rocking.', N'Modal Verbs (Can/Could)', N'Он мог почувствовать, как корабль качает.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (790, N'You can go to a movie theater', N'Modal Verbs (Can/Could)', N'Вы можете сходить в кинотеатр.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (791, N'We can go to the game tonight!', N'Modal Verbs (Can/Could)', N'Мы можем пойти на игру сегодня вечером!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (792, N'We could go to an art gallery.', N'Modal Verbs (Can/Could)', N'Мы могли бы пойти в картинную галерею.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (793, N'You can go to the gas station', N'Modal Verbs (Can/Could)', N'Вы можете сходить на заправку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (794, N'Can I show you something?', N'Modal Verbs (Can/Could)', N'Могу я показать тебе кое-что?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (795, N'Can I offer you anything?', N'Modal Verbs (Can/Could)', N'Могу я предложить вам что-нибудь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (796, N'Can you do it tomorrow?', N'Modal Verbs (Can/Could)', N'Можете ли вы сделать это завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (797, N'Can I see you tomorrow?', N'Modal Verbs (Can/Could)', N'Могу я увидеть тебя завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (798, N'Can I call you tomorrow?', N'Modal Verbs (Can/Could)', N'Могу я позвонить тебе завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (799, N'Can I bring you anything?', N'Modal Verbs (Can/Could)', N'Могу я принести вам что-нибудь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (800, N'Can we get you anything?', N'Modal Verbs (Can/Could)', N'Можем ли мы принести вам что-нибудь?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (801, N'Can I borrow it tomorrow?', N'Modal Verbs (Can/Could)', N'Можно я позаимствую это завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (802, N'Can I offer you coffee?', N'Modal Verbs (Can/Could)', N'Могу я предложить тебе кофе?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (803, N'Could you call me tomorrow?', N'Modal Verbs (Can/Could)', N'Не мог бы ты позвонить мне завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (804, N'Could you help me upstairs?', N'Modal Verbs (Can/Could)', N'Не могли бы вы мне помочь наверху?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (805, N'Can you give me coffee?', N'Modal Verbs (Can/Could)', N'Можешь дать мне кофе?')
GO
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (806, N'Can I buy you breakfast?', N'Modal Verbs (Can/Could)', N'Можно я куплю тебе завтрак?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (807, N'Can you meet me tonight?', N'Modal Verbs (Can/Could)', N'Можешь ли ты встретить меня сегодня вечером?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (808, N'Can we discuss it tomorrow?', N'Modal Verbs (Can/Could)', N'Можем ли мы обсудить это завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (809, N'Could we do it tomorrow?', N'Modal Verbs (Can/Could)', N'Не могли бы мы сделать это завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (810, N'Can I get you water?', N'Modal Verbs (Can/Could)', N'Могу я принести тебе воды?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (811, N'Can I walk you home?', N'Modal Verbs (Can/Could)', N'Могу я проводить вас домой?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (812, N'Can I sell you glass?', N'Modal Verbs (Can/Could)', N'Можно я продам вам стекло?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (813, N'Can I pay you tomorrow?', N'Modal Verbs (Can/Could)', N'Могу я заплатить вам завтра?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (814, N'I couldn''t pick a career.', N'Modal Verbs (Can/Could)', N'Я не мог определиться с карьерой.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (815, N'I could not sleep that night.', N'Modal Verbs (Can/Could)', N'Я не мог уснуть той ночью.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (816, N'I could not bear the disgrace.', N'Modal Verbs (Can/Could)', N'Я не мог вынести позор.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (817, N'I can''t stay all night.', N'Modal Verbs (Can/Could)', N'Я не могу оставаться всю ночь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (818, N'You can''t keep a secret.', N'Modal Verbs (Can/Could)', N'Вы не можете хранить секрет.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (819, N'She could not speak another word.', N'Modal Verbs (Can/Could)', N'Она не могла сказать ни слова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (820, N'He couldn''t say a word', N'Modal Verbs (Can/Could)', N'Он не мог сказать ни слова.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (821, N'We can''t change that history.', N'Modal Verbs (Can/Could)', N'Мы не можем изменить ту историю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (822, N'She could not believe the news.', N'Modal Verbs (Can/Could)', N'Она не могла поверить новостям.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (823, N'She could not tolerate a rebuke.', N'Modal Verbs (Can/Could)', N'Она не могла вытерпеть упрёк.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (824, N'He couldn''t find an explanation.', N'Modal Verbs (Can/Could)', N'Он не мог найти объяснение.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (825, N'I couldn''t paint a fence.', N'Modal Verbs (Can/Could)', N'Я не мог покрасить забор.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (826, N'I can''t wait another minute.', N'Modal Verbs (Can/Could)', N'Я не могу ждать еще минуту.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (827, N'He couldn''t move a muscle.', N'Modal Verbs (Can/Could)', N'Он не мог пошевелить ни одним мускулом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (828, N'I can''t find a shirt', N'Modal Verbs (Can/Could)', N'Я не могу найти рубашку.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (829, N'He could not be the villain.', N'Modal Verbs (Can/Could)', N'Он не мог быть злодеем.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (830, N'I couldn''t keep that promise.', N'Modal Verbs (Can/Could)', N'Я не мог сдержать то обещание.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (831, N'I couldn''t sleep that night.', N'Modal Verbs (Can/Could)', N'Я не могла спать той ночью.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (832, N'It can''t do any harm.', N'Modal Verbs (Can/Could)', N'Это не может причинить никакого вреда.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (833, N'I could not touch the boy.', N'Modal Verbs (Can/Could)', N'Я не мог прикоснуться к мальчику.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (834, N'I am glad to see you.', N'Infinitive', N'Я рад видеть вас.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (835, N'He promised to follow my advice.', N'Infinitive', N'Он пообещал, что последует моему совету.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (836, N'He tried to raise his head.', N'Infinitive', N'Он попытался поднять свою голову.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (837, N'He offered to buy my company.', N'Infinitive', N'Он предложил купить мою компанию.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (838, N'She decided to test her theory.', N'Infinitive', N'Она решила проверить свою теорию.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (839, N'He wanted to check his mail.', N'Infinitive', N'Он хотел проверить свою почту.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (840, N'They wanted to hear his story.', N'Infinitive', N'Они хотели услышать его историю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (841, N'She wanted to share her story.', N'Infinitive', N'Она хотела поделиться своей историей.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (842, N'I wanted to be your friend.', N'Infinitive', N'Я хотел быть твоим другом.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (843, N'I decided to express my opinion.', N'Infinitive', N'Я решила выразить своё мнение.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (844, N'She refused to lose her brother.', N'Infinitive', N'Она отказалась терять своего брата.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (845, N'She goes to the supermarket to buy food.', N'Infinitive', N'Она ходит в супермаркет, чтобы покупать еду.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (846, N'Thank you for calling.', N'Gerund', N'Спасибо, что звоните.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (847, N'Thank you for participating!', N'Gerund', N'Спасибо за участие!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (848, N'Thank you for trying.', N'Gerund', N'Спасибо, что пытаетесь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (849, N'Thank you for cooperating.', N'Gerund', N'Спасибо за сотрудничество.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (850, N'Thank you for noticing.', N'Gerund', N'Спасибо, что заметили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (851, N'Thank you for listening!', N'Gerund', N'Спасибо за то, что слушали!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (852, N'Thank you for reading!', N'Gerund', N'Спасибо за чтение!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (853, N'Thank you for replying.', N'Gerund', N'Спасибо за ответ.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (854, N'Do you like racing?', N'Gerund', N'Вам нравятся гонки?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (855, N'Thank you for asking!', N'Gerund', N'Спасибо, что спрашиваете!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (856, N'Forgive me for interrupting.', N'Gerund', N'Простите меня за то, что перебиваю.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (857, N'Thank you for joining.', N'Gerund', N'Спасибо, что присоединились.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (858, N'Do you like sleeping?', N'Gerund', N'Тебе нравится спать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (859, N'Thank you for visiting.', N'Gerund', N'Спасибо, что навестили.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (860, N'Thank you for watching!', N'Gerund', N'Спасибо, что смотрите!')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (861, N'Forgive me for complaining.', N'Gerund', N'Простите меня за то, что жалуюсь.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (862, N'Do you like travelling?', N'Gerund', N'Вам нравится путешествовать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (863, N'Do you like writing?', N'Gerund', N'Тебе нравится писать?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (864, N'Do you like studying?', N'Gerund', N'Тебе нравится учиться?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (865, N'Do you like driving?', N'Gerund', N'Вам нравится водить машину?')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (866, N'I plan on meeting my relatives.', N'Gerund', N'Я собираюсь навестить своих родственников.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (867, N'I like swimming.', N'Gerund', N'Мне нравится плавать.')
INSERT [dbo].[SentenceTasks] ([SentenceTaskId], [Sentence], [Category], [Translate]) VALUES (868, N'I’m interested in drawing.', N'Gerund', N'Я интересуюсь рисованием.')
SET IDENTITY_INSERT [dbo].[SentenceTasks] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Role]) VALUES (6, N'Admin', N'Creator', N'admin@admin.com', N'DefaultAdmin9856', N'admin')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserWords]  WITH CHECK ADD FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordId])
GO

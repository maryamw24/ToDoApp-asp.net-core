USE [master]
GO
/****** Object:  Database [TodoApp]    Script Date: 8/8/2024 10:53:30 PM ******/
CREATE DATABASE [TodoApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TodoApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TodoApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TodoApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TodoApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TodoApp] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TodoApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TodoApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TodoApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TodoApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TodoApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TodoApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [TodoApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TodoApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TodoApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TodoApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TodoApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TodoApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TodoApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TodoApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TodoApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TodoApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TodoApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TodoApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TodoApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TodoApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TodoApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TodoApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TodoApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TodoApp] SET RECOVERY FULL 
GO
ALTER DATABASE [TodoApp] SET  MULTI_USER 
GO
ALTER DATABASE [TodoApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TodoApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TodoApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TodoApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TodoApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TodoApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TodoApp', N'ON'
GO
ALTER DATABASE [TodoApp] SET QUERY_STORE = ON
GO
ALTER DATABASE [TodoApp] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TodoApp]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[id] [int] NOT NULL,
	[value] [nvarchar](50) NOT NULL,
	[category] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [text] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[isActive] [int] NOT NULL,
	[statusId] [int] NOT NULL,
 CONSTRAINT [PK__Tasks__7C6949D1F16DF5FC] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[isActive] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Users__1788CCAC5E2544EA] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF__Tasks__CreatedAt__3E52440B]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__CreatedAt__38996AB5]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK__Tasks__UserID__3F466844] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK__Tasks__UserID__3F466844]
GO
/****** Object:  StoredProcedure [dbo].[stpAddTask]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stpAddTask]
@title text,
@dueDate DateTime,
@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Tasks (Title, DueDate, StatusID, UserID, CreatedAt,isActive) VALUES (@title, @dueDate,3,@userId,GetDate(),1)
END
GO
/****** Object:  StoredProcedure [dbo].[stpAddUser]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stpAddUser]
@username text,
@password text,
@fullName text

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Users(UserName, Password, CreatedAt, isActive,FullName) VALUES (@username, @password, getDate(), (SELECT id FROM Lookup Where value = 'Yes' AND category = 'isActive'),@fullName)
END
GO
/****** Object:  StoredProcedure [dbo].[stpCheckIfTaskIsIncomplete]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stpCheckIfTaskIsIncomplete]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE TASKS SET statusId = (SELECT id FROM Lookup WHERE Value = 'Incomplete') Where DueDate < getDate()
END
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteTask]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stpDeleteTask]
@taskId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Tasks SET isActive = (Select id FROM Lookup Where category = 'isActive' and value = 'No') Where TaskID = @taskId
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateTask]    Script Date: 8/8/2024 10:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stpUpdateTask]
	-- Add the parameters for the stored procedure here

@id int,
@title text,
@dueDate datetime,
@statusId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Tasks set Title = @title, DueDate = @dueDate, statusId = 3 , UpdatedAt = GetDate() Where TaskID = @id
END
GO
USE [master]
GO
ALTER DATABASE [TodoApp] SET  READ_WRITE 
GO

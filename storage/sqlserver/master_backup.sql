USE [master]
GO
/****** Object:  Database [MasterData]    Script Date: 12/15/2022 2:11:29 PM ******/
CREATE DATABASE [MasterData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MasterData', FILENAME = N'/var/opt/mssql/data/MasterData.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MasterData_log', FILENAME = N'/var/opt/mssql/data/MasterData_log.ldf' , SIZE = 270336KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MasterData] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MasterData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MasterData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MasterData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MasterData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MasterData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MasterData] SET ARITHABORT OFF 
GO
ALTER DATABASE [MasterData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MasterData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MasterData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MasterData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MasterData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MasterData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MasterData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MasterData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MasterData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MasterData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MasterData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MasterData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MasterData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MasterData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MasterData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MasterData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MasterData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MasterData] SET RECOVERY FULL 
GO
ALTER DATABASE [MasterData] SET  MULTI_USER 
GO
ALTER DATABASE [MasterData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MasterData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MasterData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MasterData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MasterData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MasterData] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MasterData', N'ON'
GO
ALTER DATABASE [MasterData] SET QUERY_STORE = OFF
GO
USE [MasterData]
GO
/****** Object:  Table [dbo].[FakeDataMaster]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FakeDataMaster](
	[Id] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryNotication]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryNotication](
	[Id] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Method] [nvarchar](50) NOT NULL,
	[Body] [nvarchar](250) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Read] [bit] NOT NULL,
	[Link] [nvarchar](250) NULL,
	[OnDelete] [bit] NOT NULL,
	[UserNameRead] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListApp]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListApp](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[Description] [nvarchar](50) NULL,
	[IsAPI] [bit] NULL,
	[InActive] [bit] NULL,
	[SoftShow] [int] NULL,
	[ParentId] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListAuthozire]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListAuthozire](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[Description] [nvarchar](50) NULL,
	[InActive] [bit] NULL,
	[SoftShow] [int] NULL,
	[ParentId] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListAuthozireByListRole]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListAuthozireByListRole](
	[Id] [nvarchar](50) NOT NULL,
	[AppId] [nvarchar](50) NOT NULL,
	[ListRoleId] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[AuthozireId] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListAuthozireRoleByUser]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListAuthozireRoleByUser](
	[Id] [nvarchar](50) NOT NULL,
	[ListAuthozireId] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[UserId] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListRole]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListRole](
	[Id] [nvarchar](50) NOT NULL,
	[AppId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[Description] [nvarchar](50) NULL,
	[InActive] [bit] NULL,
	[Key] [nvarchar](50) NULL,
	[ParentId] [nvarchar](50) NULL,
	[SoftShow] [int] NULL,
	[IsAPI] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListRoleByUser]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListRoleByUser](
	[Id] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[OnDelete] [bit] NULL,
	[ListRoleId] [nvarchar](50) NULL,
	[AppId] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 12/15/2022 2:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[Id] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[InActive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[RoleNumber] [int] NOT NULL,
	[Read] [bit] NOT NULL,
	[Create] [bit] NOT NULL,
	[Edit] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[WarehouseId] [nvarchar](max) NULL,
	[ListWarehouseId] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'07fdc52e-be77-462f-aae7-6b54591195d7', N'Department 8', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'1e906547-2dc6-4906-a143-eeee1ae63837', N'Employee 1', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'200b71f0-cf40-49c5-bc3e-a8e29d80923c', N'Department 6', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'20e15316-04b3-416a-a8c1-32bd73e7e65d', N'Station 7', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'267609a9-2d39-4abe-9257-ddc582a9af02', N'Department 5', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'29fba842-4700-4499-9860-44ecab23ed5b', N'Create By 7', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'2aa43676-d366-4377-8b4d-f39fbcf360c2', N'Employee 5', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'2b24714f-d647-4863-a852-75e9d065e433', N'Department 3', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'2d7330ee-88fd-4b28-b5b8-26a73295082f', N'Project 3', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'33f53b0d-49e4-4de9-a31d-3b9111ed5b1b', N'Department 2', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'427cb41f-d3a0-45c0-b93e-ed0f9a0024fc', N'Project 6', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'440b6b96-f376-4ade-9ef3-40eb89389473', N'Customer 5', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'45364a79-2298-4684-8207-72c55014691f', N'Station 8', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'481a777b-89a4-44b6-9c14-97d22b156bae', N'Create By 8', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'48df2592-9bdc-4594-8ed3-98e580a54af2', N'Department 0', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'4e71df03-1353-423b-91b8-b3ff8ec6d002', N'Customer 1', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'53a621d8-c1a4-4ecb-a27e-e29e103f14fa', N'Project 4', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'55153175-b5ab-4b70-b440-69d91becdd4e', N'Create By 1', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'55819249-d17c-451d-8d74-bb235295bff2', N'Employee 6', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'58dd9797-76ac-40a5-b419-b09e44dd15f0', N'Employee 3', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'5b064a5d-cd95-4d1b-9013-cf4b939014ca', N'Station 6', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'6ce183d3-e1f6-4a90-bb68-29a5ea3d4493', N'Employee 0', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'724d15a7-9e74-4fce-8e22-da33aa765b4b', N'Create By 3', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'73412082-4edd-4661-b0e8-eb2dfa2b7040', N'Create By 6', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'752875cc-5d8a-41a9-9f56-59834b9924b2', N'Project 8', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'79e6132f-5fbf-4343-a48e-eb7d7dbe080d', N'Employee 9', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'7a547d3e-96ad-405a-8b8b-1f499857e436', N'Customer 9', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'876860f6-6c5b-4d69-9113-c01d05ef2371', N'Station 3', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'8835dad4-dd82-4df1-9e5e-9c3d0886655a', N'Station 2', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'8aa96b30-2f27-44e4-ac48-20d504b04dfa', N'Station 5', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'8e9c7600-885e-4eaa-8a46-d0746e0c9c02', N'Create By 5', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'8fc63d6e-b9a9-43f1-807b-53f93c871ef9', N'Create By 2', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'944033fa-b4d6-4d3f-b6a7-638ba5f52417', N'Customer 3', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'9752979d-846a-4083-839c-c315eb89e730', N'Project 2', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'9b92f065-7f1e-4134-b82f-8a634b61962b', N'Project 0', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'9ef15d32-4c60-43ee-af30-ad7273ccdf03', N'Project 1', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'9f828230-f491-4aff-913f-d6cffdf67732', N'Employee 7', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'a253201e-9793-464b-a114-34655537683e', N'Customer 7', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'a8ffc4c7-e78e-467a-aa6c-a8b9e6b4ea95', N'Project 5', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'b2b6121d-f938-444b-8546-a51f733b4c15', N'Department 7', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'b3a16e9e-6df3-448c-a511-4f7f6cce0152', N'Station 1', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'b59a8512-e540-40a9-81cd-ef3062abfffe', N'Customer 0', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'b628bf9b-862d-4480-887a-338aa7032eec', N'Project 7', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'baab2372-9c14-4b6c-9d21-4040ad6daa48', N'Customer 8', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'bf60c14b-2031-4864-9dbb-1493565522c9', N'Department 9', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'c2391362-d838-4e70-b9ff-510710c2ea6f', N'Project 9', 3, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'c5fae085-a18a-4874-8f45-d0f44b5531e7', N'Station 0', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'c981115e-1a03-4af6-a82d-f4fe0bda94ab', N'Station 9', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'd111fd83-b830-497d-afd8-419619d59e51', N'Employee 4', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'd4ebdb7b-593b-4c69-9c03-d8c966a5fe31', N'Department 4', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'e3680f1c-1767-432b-b981-fd4fba3a1a0b', N'Station 4', 2, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'e8d873f8-b35f-4b43-a866-2db48b0ae291', N'Employee 2', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'e9594f20-8642-4eb1-89e2-f815dd5f0f72', N'Create By 9', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'ec7af2c9-9021-42bf-8f3a-4e0315d4ae56', N'Customer 6', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'f0766d47-bbad-4f89-b1a1-6326fb7359ce', N'Employee 8', 7, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'f10faf00-1bfd-4559-b81f-5b410635e9e9', N'Create By 0', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'f3c381c2-6a2c-49ad-aaa1-c46c9064439e', N'Create By 4', 1, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'f3f858d4-a845-41b6-ab57-05424319a8d9', N'Customer 4', 5, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'f77bfa3b-273d-4459-bb12-00f7241526f5', N'Department 1', 6, 0)
INSERT [dbo].[FakeDataMaster] ([Id], [Name], [Type], [OnDelete]) VALUES (N'feca961f-d669-44a3-a930-cf6e3aa1f5ee', N'Customer 2', 5, 0)
GO
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'03c0036b-f516-4746-8c31-b281769c831a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:48.5629429' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0a4b7c6b-91c0-478c-aeb1-3df800cb20a7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:47.5740224' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0f9971ad-4db0-4dde-a233-2e8ce00a6bb4', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T13:33:18.7491479' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2367b002-1a0d-43b5-9e4a-b89c9c065f87', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:49.1391761' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'25c1d840-411a-4b8e-9f17-e3a11eef8026', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:45.4094953' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'309cbe9a-6e0a-4385-bb56-559868dff935', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:54.1245260' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'392d3b1e-813c-4fca-bd91-1ce09f28217b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:40:31.8320479' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'54e73e66-e160-4ddc-9062-7082c927110c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:57.1412242' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5719f1c7-b688-4af2-96aa-09f2e81e4777', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:48.7492980' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'572dbecf-08fd-4c8b-9f5f-7654ee833e8b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:50.4423313' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6d48bce9-cbb4-492b-9c21-d322ec9be5f7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:48.3479533' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7a57862f-c235-43f0-ae11-8e93a09e7646', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:51.9960866' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'85e5a6c9-709a-4a9d-97e4-64eed59aab4b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:58.0538402' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8dac3522-bb4a-4c20-ac07-edcf6f30f0f3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:46.9530863' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a89f55aa-a39d-4768-993c-ab07720f033c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:40:37.0352822' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b9cc29e8-4441-4bec-9fb3-075add2efff0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:40:36.2028662' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cd0eb5d0-abb9-421d-97bb-47fab4fcbdd3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:11.5566475' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ceb8551e-5048-4207-9a4d-e40e107c11d5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:39:23.3294301' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd0bb166c-c13b-496c-92d2-293f420fbe34', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:53.0681316' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd0febcbb-9ddc-4dd3-9f26-338f1f9c29b4', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:38:51.0654823' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd8bdec3e-503a-417e-a42f-e5cf2b60d32f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:40:34.6042647' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ddcf9aed-1e10-4e6e-b5d2-9d1af1d44a9f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T12:40:35.4979691' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fbd18d82-3383-4ecd-a793-6e40002b147c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-12-14T13:33:33.2898891' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
GO
INSERT [dbo].[ListApp] ([Id], [Name], [OnDelete], [Description], [IsAPI], [InActive], [SoftShow], [ParentId]) VALUES (N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'App Kho', 0, N'App Kho', 1, 1, 1, NULL)
INSERT [dbo].[ListApp] ([Id], [Name], [OnDelete], [Description], [IsAPI], [InActive], [SoftShow], [ParentId]) VALUES (N'fd264709-39fe-4e4c-b414-d749f1614876', N'App Master', 0, N'App Master', 1, 1, 2, NULL)
GO
INSERT [dbo].[ListAuthozire] ([Id], [Name], [OnDelete], [Description], [InActive], [SoftShow], [ParentId]) VALUES (N'f9337db6-d550-44a1-9823-8f6248658bd7', N'Admin', 0, N'Admin', 1, 1, NULL)
GO
INSERT [dbo].[ListAuthozireByListRole] ([Id], [AppId], [ListRoleId], [OnDelete], [AuthozireId]) VALUES (N'1ac1f548-a182-41c9-9257-71ad3b8c8750', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'd8a99363-3f51-4f6d-bf1a-44aa095d86d0', 0, N'f9337db6-d550-44a1-9823-8f6248658bd7')
INSERT [dbo].[ListAuthozireByListRole] ([Id], [AppId], [ListRoleId], [OnDelete], [AuthozireId]) VALUES (N'2d8efb4d-e89a-4db5-adb8-e2438da58318', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'10adc2c8-2686-4692-bcb2-f874fec638bd', 0, N'f9337db6-d550-44a1-9823-8f6248658bd7')
INSERT [dbo].[ListAuthozireByListRole] ([Id], [AppId], [ListRoleId], [OnDelete], [AuthozireId]) VALUES (N'cbfd65ce-aba8-422d-b581-6feebbe413de', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'11a8090c-02db-4984-973a-09ccf701383b', 0, N'f9337db6-d550-44a1-9823-8f6248658bd7')
GO
INSERT [dbo].[ListAuthozireRoleByUser] ([Id], [ListAuthozireId], [OnDelete], [UserId]) VALUES (N'e447ed93-f18f-4b2a-82bb-561db6dd0ae3', N'f9337db6-d550-44a1-9823-8f6248658bd7', 0, N'9d81692d-35d1-43ae-92d1-df17844b774e')
GO
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'bf93e442-b029-4ecd-9253-86b25d1fa665', N'', N'Quản lý kho', 0, N'Quản lý kho', 1, N'WH.API', N'', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'4a5cf2c3-91c4-4f2a-ba56-d75a58bd85af', N'', N'Đọc dữ liệu kho', 0, N'Đọc dữ liệu kho', 1, N'WareHouse.WareHouse.Read.GetList', N'd224fd00-b6ee-4222-93a0-970dfaa5cc10', 2, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'd64ca3ed-5599-4023-b432-fa94629e62f5', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Quản lý kho', 0, N'Quản lý kho', 1, N'WH.API', N'', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'a0d5b7ee-935f-4f44-aa0d-a8fed2837028', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Đọc dữ liệu kho', 0, N'Đọc dữ liệu kho', 0, N'Warehouse.Warehouse.Read', N'd64ca3ed-5599-4023-b432-fa94629e62f5', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'11a8090c-02db-4984-973a-09ccf701383b', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Sửa kho', 0, N'test quyền', 1, N'Warehouse.Warehouse.Edit', N'd64ca3ed-5599-4023-b432-fa94629e62f5', 3, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'd354d90a-a05e-416a-a1b5-45380426bd86', N'fd264709-39fe-4e4c-b414-d749f1614876', N'Quản lý Master', 0, N'Quản lý Master', 1, N'Master.API', N'', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'd8a99363-3f51-4f6d-bf1a-44aa095d86d0', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Thêm kho', 0, N'', 1, N'Warehouse.Warehouse.Create', N'd64ca3ed-5599-4023-b432-fa94629e62f5', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'10adc2c8-2686-4692-bcb2-f874fec638bd', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Xoá kho', 0, N'', 1, N'Warehouse.Warehouse.Delete', N'd64ca3ed-5599-4023-b432-fa94629e62f5', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'8e8f4471-d043-4070-b5ea-07de04a1be35', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Quản lý sổ kho', 0, N'Thêm nhanh, không cần tạo role từng loại phiếu', 1, N'Warehouse.WareHouseBook', N'', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'e08ffaa3-9b3c-41da-ae56-5544a4d9b3f6', N'fd264709-39fe-4e4c-b414-d749f1614876', N'Thêm', 0, N'', 1, N'Master.Master.Create', N'd354d90a-a05e-416a-a1b5-45380426bd86', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'd13cbe13-a91a-4fa7-b194-b294de4e0bab', N'fd264709-39fe-4e4c-b414-d749f1614876', N'Sửa', 0, N'', 1, N'Master.Master.Edit', N'd354d90a-a05e-416a-a1b5-45380426bd86', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'1b3c6626-d34a-4abc-86ab-06e7a9ad76a4', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Sửa phiếu', 0, N'', 1, N'Warehouse.WareHouseBook.Edit', N'8e8f4471-d043-4070-b5ea-07de04a1be35', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'1ea0037b-76a0-4c14-80af-51b4334d3042', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Thêm phiếu', 0, N'', 1, N'Warehouse.WareHouseBook.Create', N'8e8f4471-d043-4070-b5ea-07de04a1be35', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'b4b2772e-7595-4fe9-95a3-a25553781942', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93', N'Xóa phiếu', 0, N'Xóa phiếu', 1, N'Warehouse.WareHouseBook.Delete', N'8e8f4471-d043-4070-b5ea-07de04a1be35', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'91626a45-3561-4306-b369-ac4441bf0294', N'fd264709-39fe-4e4c-b414-d749f1614876', N'Xóa', 0, N'', 1, N'Master.Master.Delete', N'd354d90a-a05e-416a-a1b5-45380426bd86', 1, 1)
INSERT [dbo].[ListRole] ([Id], [AppId], [Name], [OnDelete], [Description], [InActive], [Key], [ParentId], [SoftShow], [IsAPI]) VALUES (N'3a098743-7cc0-4e22-ac22-43d0c4b7b04a', N'fd264709-39fe-4e4c-b414-d749f1614876', N'Đọc', 0, N'', 1, N'Master.Master.Read', N'd354d90a-a05e-416a-a1b5-45380426bd86', 1, 1)
GO
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'2dcfc27e-5d59-48f8-9ad1-7ff84402e4f0', N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, N'd354d90a-a05e-416a-a1b5-45380426bd86', N'fd264709-39fe-4e4c-b414-d749f1614876')
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'3c546686-b66c-4667-b523-450d8f859d8b', N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, N'e08ffaa3-9b3c-41da-ae56-5544a4d9b3f6', N'fd264709-39fe-4e4c-b414-d749f1614876')
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'6744caff-2f1d-4691-a911-d9fd6a22d6d8', N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, N'd13cbe13-a91a-4fa7-b194-b294de4e0bab', N'fd264709-39fe-4e4c-b414-d749f1614876')
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'726b561d-992a-4278-8ce9-39fd32dddf88', N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, N'3a098743-7cc0-4e22-ac22-43d0c4b7b04a', N'fd264709-39fe-4e4c-b414-d749f1614876')
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'd5756efc-d91d-4ab3-b185-90d76cb7a895', N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, N'91626a45-3561-4306-b369-ac4441bf0294', N'fd264709-39fe-4e4c-b414-d749f1614876')
INSERT [dbo].[ListRoleByUser] ([Id], [UserId], [OnDelete], [ListRoleId], [AppId]) VALUES (N'32d4e8c3-c3c2-460d-8a52-948b194510a9', N'9d81692d-35d1-43ae-92d1-df17844b774e', 0, N'a0d5b7ee-935f-4f44-aa0d-a8fed2837028', N'5fc1d683-5506-41d6-909c-0c07bfaa7f93')
GO
INSERT [dbo].[UserMaster] ([Id], [UserName], [Password], [InActive], [OnDelete], [Role], [RoleNumber], [Read], [Create], [Edit], [Delete], [WarehouseId], [ListWarehouseId]) VALUES (N'9a5fc419-63e0-423e-98f6-058c164c7a9b', N'user@example.com', N'AQAAAAEAACcQAAAAEGBJvwZ4/gVq2WB6lYrSVhHbDDMIm774APyX/7Y02immfUEb6Br8wBIqa/UEUprThw==', 1, 0, N'User', 1, 1, 1, 1, 0, N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae,6f95a3ad-6506-4a43-b809-8bbb11b2dea5,872ea678-8b3e-41a7-876a-89279387f8aa,a3da7204-ec66-4f62-8c8a-72cda7740044,d0cb38bf-b405-46bb-b5a0-24c6c298404c,d1c22d2a-9863-4df6-ad47-32462349be09,fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae,6f95a3ad-6506-4a43-b809-8bbb11b2dea5,872ea678-8b3e-41a7-876a-89279387f8aa,a3da7204-ec66-4f62-8c8a-72cda7740044,d0cb38bf-b405-46bb-b5a0-24c6c298404c,d1c22d2a-9863-4df6-ad47-32462349be09,fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d')
INSERT [dbo].[UserMaster] ([Id], [UserName], [Password], [InActive], [OnDelete], [Role], [RoleNumber], [Read], [Create], [Edit], [Delete], [WarehouseId], [ListWarehouseId]) VALUES (N'9d81692d-35d1-43ae-92d1-df17844b774e', N'admin@gmail.com', N'AQAAAAEAACcQAAAAEEYZJp7o7Hs2MUAEZzV41DGKtr0SfPWQsRyT23pYVfT1xuyX83gIQjM5dJWMC5MXhw==', 1, 0, N'Admin', 3, 1, 1, 1, 1, N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd,52bb3cad-6855-44a8-b522-1d079cea7649,53bd3cad-9dff-4f26-8527-d5789d325d1a,61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae,6f95a3ad-6506-4a43-b809-8bbb11b2dea5,84fd821c-2984-470b-8c1b-5123f7ddbd10,872ea678-8b3e-41a7-876a-89279387f8aa,a3da7204-ec66-4f62-8c8a-72cda7740044,acf49099-ee31-4633-b905-e148a6071bef,d0cb38bf-b405-46bb-b5a0-24c6c298404c,d1c22d2a-9863-4df6-ad47-32462349be09,fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd,52bb3cad-6855-44a8-b522-1d079cea7649,53bd3cad-9dff-4f26-8527-d5789d325d1a,61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae,6f95a3ad-6506-4a43-b809-8bbb11b2dea5,84fd821c-2984-470b-8c1b-5123f7ddbd10,872ea678-8b3e-41a7-876a-89279387f8aa,a3da7204-ec66-4f62-8c8a-72cda7740044,acf49099-ee31-4633-b905-e148a6071bef,d0cb38bf-b405-46bb-b5a0-24c6c298404c,d1c22d2a-9863-4df6-ad47-32462349be09,fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d')
INSERT [dbo].[UserMaster] ([Id], [UserName], [Password], [InActive], [OnDelete], [Role], [RoleNumber], [Read], [Create], [Edit], [Delete], [WarehouseId], [ListWarehouseId]) VALUES (N'c2483cc3-ecab-471c-a472-df0c8dbd5dc4', N'manager@example.com', N'AQAAAAEAACcQAAAAEOyZZHmIaPJ5PEfT9o9iAsqrt+Np/hJdQ7sTZjk7dguAjp8N8K28lTFHTZV5bv14Tw==', 1, 0, N'Manager', 2, 1, 1, 1, 1, N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d')
GO
ALTER TABLE [dbo].[HistoryNotication] ADD  CONSTRAINT [DF_HistoryNotication_Read]  DEFAULT ((0)) FOR [Read]
GO
ALTER TABLE [dbo].[HistoryNotication] ADD  CONSTRAINT [DF_HistoryNotication_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_InActive]  DEFAULT ((1)) FOR [InActive]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_Read]  DEFAULT ((0)) FOR [Read]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_Create]  DEFAULT ((0)) FOR [Create]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_Edit]  DEFAULT ((0)) FOR [Edit]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_Delete]  DEFAULT ((0)) FOR [Delete]
GO
USE [master]
GO
ALTER DATABASE [MasterData] SET  READ_WRITE 
GO

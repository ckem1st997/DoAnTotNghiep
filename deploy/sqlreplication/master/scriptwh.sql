USE [master]
GO
/****** Object:  Database [WarehouseManagement]    Script Date: 7/18/2022 3:45:42 PM ******/
CREATE DATABASE [WarehouseManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WarehouseManagement', FILENAME = N'/var/opt/mssql/data/WarehouseManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WarehouseManagement_log', FILENAME = N'/var/opt/mssql/data/WarehouseManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WarehouseManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WarehouseManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WarehouseManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WarehouseManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WarehouseManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WarehouseManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WarehouseManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [WarehouseManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WarehouseManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WarehouseManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WarehouseManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WarehouseManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WarehouseManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WarehouseManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WarehouseManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WarehouseManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WarehouseManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WarehouseManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WarehouseManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WarehouseManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WarehouseManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WarehouseManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WarehouseManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WarehouseManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WarehouseManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [WarehouseManagement] SET  MULTI_USER 
GO
ALTER DATABASE [WarehouseManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WarehouseManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WarehouseManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WarehouseManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WarehouseManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WarehouseManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'WarehouseManagement', N'ON'
GO
ALTER DATABASE [WarehouseManagement] SET QUERY_STORE = OFF
GO
USE [WarehouseManagement]
GO
/****** Object:  FullTextCatalog [FullText]    Script Date: 7/18/2022 3:45:42 PM ******/
CREATE FULLTEXT CATALOG [FullText] 
GO
/****** Object:  Table [dbo].[BeginningWareHouse]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeginningWareHouse](
	[Id] [varchar](36) NOT NULL,
	[WareHouseId] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[UnitId] [varchar](36) NOT NULL,
	[UnitName] [nvarchar](255) NULL,
	[Quantity] [decimal](15, 2) NOT NULL,
	[CreatedDate] [datetime2](0) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime2](0) NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inward]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inward](
	[Id] [varchar](36) NOT NULL,
	[VoucherCode] [varchar](255) NULL,
	[VoucherDate] [datetime2](0) NOT NULL,
	[WareHouseID] [varchar](36) NOT NULL,
	[Deliver] [nvarchar](255) NULL,
	[Receiver] [nvarchar](255) NULL,
	[VendorId] [varchar](36) NULL,
	[Reason] [nvarchar](255) NULL,
	[ReasonDescription] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Reference] [varchar](255) NULL,
	[CreatedDate] [datetime2](0) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime2](0) NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
	[DeliverPhone] [varchar](50) NULL,
	[DeliverAddress] [nvarchar](50) NULL,
	[DeliverDepartment] [nvarchar](50) NULL,
	[ReceiverPhone] [varchar](50) NULL,
	[ReceiverAddress] [nvarchar](50) NULL,
	[ReceiverDepartment] [nvarchar](50) NULL,
	[Voucher] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InwardDetail]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InwardDetail](
	[Id] [varchar](36) NOT NULL,
	[InwardId] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[UnitId] [varchar](36) NOT NULL,
	[UIQuantity] [decimal](15, 2) NOT NULL,
	[UIPrice] [decimal](15, 2) NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[Quantity] [decimal](15, 2) NOT NULL,
	[Price] [decimal](15, 2) NOT NULL,
	[DepartmentId] [varchar](36) NULL,
	[DepartmentName] [nvarchar](255) NULL,
	[EmployeeId] [varchar](36) NULL,
	[EmployeeName] [nvarchar](255) NULL,
	[StationId] [varchar](36) NULL,
	[StationName] [nvarchar](255) NULL,
	[ProjectId] [varchar](36) NULL,
	[ProjectName] [nvarchar](255) NULL,
	[CustomerId] [varchar](36) NULL,
	[CustomerName] [nvarchar](255) NULL,
	[OnDelete] [bit] NOT NULL,
	[AccountMore] [varchar](255) NULL,
	[AccountYes] [varchar](255) NULL,
	[Status] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outward]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outward](
	[Id] [varchar](36) NOT NULL,
	[VoucherCode] [varchar](255) NULL,
	[VoucherDate] [datetime2](0) NOT NULL,
	[WareHouseID] [varchar](36) NOT NULL,
	[ToWareHouseId] [varchar](36) NULL,
	[Deliver] [nvarchar](255) NULL,
	[Receiver] [nvarchar](255) NULL,
	[Reason] [nvarchar](255) NULL,
	[ReasonDescription] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Reference] [varchar](255) NULL,
	[CreatedDate] [datetime2](0) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime2](0) NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
	[ReceiverDepartment] [nvarchar](50) NULL,
	[ReceiverAddress] [nvarchar](50) NULL,
	[ReceiverPhone] [varchar](50) NULL,
	[DeliverDepartment] [nvarchar](50) NULL,
	[DeliverAddress] [nvarchar](50) NULL,
	[DeliverPhone] [varchar](50) NULL,
	[Voucher] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutwardDetail]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutwardDetail](
	[Id] [varchar](36) NOT NULL,
	[OutwardId] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[UnitId] [varchar](36) NOT NULL,
	[UIQuantity] [decimal](15, 2) NOT NULL,
	[UIPrice] [decimal](15, 2) NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[Quantity] [decimal](15, 2) NOT NULL,
	[Price] [decimal](15, 2) NOT NULL,
	[DepartmentId] [varchar](36) NULL,
	[DepartmentName] [nvarchar](255) NULL,
	[EmployeeId] [varchar](36) NULL,
	[EmployeeName] [nvarchar](255) NULL,
	[StationId] [varchar](36) NULL,
	[StationName] [nvarchar](255) NULL,
	[ProjectId] [varchar](36) NULL,
	[ProjectName] [nvarchar](255) NULL,
	[CustomerId] [varchar](36) NULL,
	[CustomerName] [nvarchar](255) NULL,
	[OnDelete] [bit] NOT NULL,
	[AccountMore] [nvarchar](255) NULL,
	[AccountYes] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vWareHouseLedger]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE VIEW [dbo].[vWareHouseLedger]
AS
SELECT
  bwh.Id AS Id,
  bwh.WareHouseId AS WareHouseId,
  bwh.ItemId AS ItemId,
  bwh.UnitId AS UnitId,
  bwh.Quantity AS Quantity,
  'DDK' AS VoucherCode,
  CAST('1970-1-1' AS date) AS VoucherDate,
  NULL AS DepartmentId,
  NULL AS DepartmentName,
  NULL AS StationId,
  NULL AS StationName,
  NULL AS CustomerId,
  NULL AS CustomerName,
  NULL AS ProjectId,
  NULL AS ProjectName,
  NULL AS EmployeeId,
  NULL AS EmployeeName
FROM BeginningWareHouse bwh where bwh.OnDelete=0
UNION ALL
SELECT DISTINCT
  id.Id AS id,
  i.WareHouseID AS WareHouseId,
  id.ItemId AS ItemId,
  id.UnitId AS UnitId,
  id.Quantity AS Quantity,
  i.VoucherCode AS VoucherCode,
  i.VoucherDate AS VoucherDate,
  id.DepartmentId AS DepartmentId,
  id.DepartmentName AS DepartmentName,
  id.StationId AS StationId,
  id.StationName AS StationName,
  id.CustomerId AS CustomerId,
  id.CustomerName AS CustomerName,
  id.ProjectId AS ProjectId,
  id.ProjectName AS ProjectName,
  id.EmployeeId AS EmployeeId,
  id.EmployeeName AS EmployeeName
FROM (Inward i
  JOIN InwardDetail id
    ON ((i.Id = id.InwardId and i.OnDelete=0 and id.OnDelete=0)))
UNION ALL
SELECT DISTINCT
  od.Id AS Id,
  o.WareHouseID AS WareHouseId,
  od.ItemId AS ItemId,
  od.UnitId AS UnitId,
  -(od.Quantity) AS Quantity,
  o.VoucherCode AS VoucherCode,
  o.VoucherDate AS VoucherDate,
  od.DepartmentId AS DepartmentId,
  od.DepartmentName AS DepartmentName,
  od.StationId AS StationId,
  od.StationName AS StationName,
  od.CustomerId AS CustomerId,
  od.CustomerName AS CustomerName,
  od.ProjectId AS ProjectId,
  od.ProjectName AS ProjectName,
  od.EmployeeId AS EmployeeId,
  od.EmployeeName AS EmployeeName
FROM (Outward o
  JOIN OutwardDetail od
    ON ((o.Id = od.OutwardId and o.OnDelete=0 and od.OnDelete=0)));
GO
/****** Object:  Table [dbo].[Audit]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[Id] [varchar](36) NOT NULL,
	[VoucherCode] [varchar](20) NOT NULL,
	[VoucherDate] [datetime2](0) NOT NULL,
	[WareHouseId] [varchar](36) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedDate] [datetime2](0) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime2](0) NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditCouncil]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditCouncil](
	[Id] [varchar](36) NOT NULL,
	[AuditId] [varchar](36) NOT NULL,
	[EmployeeId] [varchar](36) NULL,
	[EmployeeName] [varchar](100) NULL,
	[Role] [varchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditDetail]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditDetail](
	[Id] [varchar](36) NOT NULL,
	[AuditId] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NULL,
	[Quantity] [decimal](15, 2) NOT NULL,
	[AuditQuantity] [decimal](15, 2) NOT NULL,
	[Conclude] [varchar](255) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditDetailSerial]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditDetailSerial](
	[Id] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[Serial] [varchar](50) NOT NULL,
	[AuditDetailId] [varchar](36) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SerialWareHouse]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SerialWareHouse](
	[Id] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[Serial] [varchar](50) NOT NULL,
	[InwardDetailId] [varchar](36) NULL,
	[OutwardDetailId] [varchar](36) NULL,
	[IsOver] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [varchar](36) NOT NULL,
	[UnitName] [nvarchar](255) NULL,
	[Inactive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[Id] [varchar](36) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[Inactive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WareHouse]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WareHouse](
	[Id] [varchar](36) NOT NULL,
	[Code] [varchar](255) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Address] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[ParentId] [varchar](36) NULL,
	[Path] [varchar](255) NULL,
	[Inactive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseBalance]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseBalance](
	[Id] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[WarehouseId] [varchar](36) NOT NULL,
	[Quantity] [decimal](15, 2) NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WareHouseItem]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WareHouseItem](
	[Id] [varchar](36) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CategoryID] [varchar](36) NULL,
	[Description] [nvarchar](50) NULL,
	[VendorID] [varchar](36) NULL,
	[VendorName] [varchar](255) NULL,
	[Country] [nvarchar](50) NULL,
	[UnitId] [varchar](36) NOT NULL,
	[Inactive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WareHouseItemCategory]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WareHouseItemCategory](
	[Id] [varchar](36) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ParentId] [varchar](36) NULL,
	[Path] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
	[Inactive] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WareHouseItemUnit]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WareHouseItemUnit](
	[Id] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[UnitId] [varchar](36) NOT NULL,
	[ConvertRate] [int] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WareHouseLimit]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WareHouseLimit](
	[Id] [varchar](36) NOT NULL,
	[WareHouseId] [varchar](36) NOT NULL,
	[ItemId] [varchar](36) NOT NULL,
	[UnitId] [varchar](36) NOT NULL,
	[UnitName] [varchar](255) NULL,
	[MinQuantity] [int] NOT NULL,
	[MaxQuantity] [int] NOT NULL,
	[CreatedDate] [datetime2](0) NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedDate] [datetime2](0) NOT NULL,
	[ModifiedBy] [varchar](100) NULL,
	[OnDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Audit] ([Id], [VoucherCode], [VoucherDate], [WareHouseId], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'3dad0c93-4546-45d0-b7ea-3027b72a60e1', N'ee', CAST(N'2021-10-12T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'qqq', CAST(N'2021-10-13T03:02:22.0000000' AS DateTime2), N'qqq', CAST(N'2021-10-13T03:02:22.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Audit] ([Id], [VoucherCode], [VoucherDate], [WareHouseId], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'626729d7-6772-4c23-ada4-59075c374b64', N'ttt', CAST(N'2021-09-09T17:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'ttt', CAST(N'2021-10-09T10:18:13.0000000' AS DateTime2), N'ttt', CAST(N'2021-10-09T10:18:13.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Audit] ([Id], [VoucherCode], [VoucherDate], [WareHouseId], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'c37524bd-eadd-4459-8b4b-62c913bbc0ea', N'qqq', CAST(N'2021-10-12T17:00:00.0000000' AS DateTime2), N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'qqq', CAST(N'2021-10-13T08:54:45.0000000' AS DateTime2), N'qqq', CAST(N'2021-10-13T08:54:45.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Audit] ([Id], [VoucherCode], [VoucherDate], [WareHouseId], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'f3158cda-32b2-4d8b-a302-0f16f45770c8', N'pham', CAST(N'2021-10-11T17:00:00.0000000' AS DateTime2), N'872ea678-8b3e-41a7-876a-89279387f8aa', N'pham', CAST(N'2021-10-12T09:03:14.0000000' AS DateTime2), N'trung', CAST(N'2021-10-12T09:03:14.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Audit] ([Id], [VoucherCode], [VoucherDate], [WareHouseId], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'f3f70d4a-cfa2-46d9-99d2-e3eb70bebc33', N'trung', CAST(N'2021-09-06T00:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'trung', CAST(N'2021-09-20T08:25:11.0000000' AS DateTime2), N'trung', CAST(N'2021-09-20T08:25:11.0000000' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[AuditDetail] ([Id], [AuditId], [ItemId], [Quantity], [AuditQuantity], [Conclude], [OnDelete]) VALUES (N'7cda8438-bc57-4209-b51b-db1e5c188d05', N'f3f70d4a-cfa2-46d9-99d2-e3eb70bebc33', N'd9ddfb77-5e17-4b42-a2c8-4f49ad8f9106', CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), N'trung', 0)
GO
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'20cadd82-0426-4917-80f8-5c96c135561f', N'52bb3cad-6855-44a8-b522-1d079cea7649', N'eeacaf7c-3106-4927-a312-746a8f087787', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, CAST(99.00 AS Decimal(15, 2)), CAST(N'2021-01-01T03:08:41.0000000' AS DateTime2), NULL, CAST(N'2022-02-07T19:03:32.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'6cbca79b-c01c-4760-b848-6fe2a25e1131', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'0fcdbed4-e185-42de-b394-e93d6e22f0dd', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', CAST(88.00 AS Decimal(15, 2)), CAST(N'2021-01-02T10:30:24.0000000' AS DateTime2), NULL, CAST(N'2021-09-21T08:04:53.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'6fbbce42-5587-4c44-9e92-ec38475899cd', N'3d4e0634-5f64-4e1a-8644-3dc2fd245415', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, CAST(1.00 AS Decimal(15, 2)), CAST(N'2022-07-09T07:40:21.0000000' AS DateTime2), NULL, CAST(N'2022-07-14T14:40:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'7e602240-dc25-4b1d-8d5b-8d3c2eb52488', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', CAST(10.00 AS Decimal(15, 2)), CAST(N'2021-10-14T02:26:54.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T02:26:54.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'7ebc0f7d-09de-48c4-9728-d1e65030d80b', N'872ea678-8b3e-41a7-876a-89279387f8aa', N'3e480fbd-2502-47d2-9e4e-d622e1500b45', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, CAST(6.00 AS Decimal(15, 2)), CAST(N'2021-10-02T01:26:13.0000000' AS DateTime2), NULL, CAST(N'2022-02-07T19:03:52.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'b5cfc868-9858-40b7-ba48-4c04120b59ec', N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, CAST(10.00 AS Decimal(15, 2)), CAST(N'2022-04-02T04:48:18.0000000' AS DateTime2), NULL, CAST(N'2022-04-02T04:48:18.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'bbf0ad34-9a8c-47db-9d59-bd44abd2d701', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, CAST(7.00 AS Decimal(15, 2)), CAST(N'2021-10-02T01:26:35.0000000' AS DateTime2), NULL, CAST(N'2022-02-07T19:04:21.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'c8a488cd-f454-4c45-9f5d-59ed41c8a6de', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'3b2ec5b2-0a5f-472e-b9d5-0e5d2721f639', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', NULL, CAST(112.00 AS Decimal(15, 2)), CAST(N'2021-10-13T08:53:09.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T08:53:09.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'ca837a5c-7046-4856-9e6d-80a80ac70c7d', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'ae9c3653-4b10-480a-8298-675d176cbbf8', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', CAST(77.00 AS Decimal(15, 2)), CAST(N'2021-01-03T09:37:32.0000000' AS DateTime2), NULL, CAST(N'2021-10-20T03:42:18.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[BeginningWareHouse] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [Quantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'cc94bf9f-419f-4eb0-b03f-27e08ed26d5f', N'53bd3cad-9dff-4f26-8527-d5789d325d1a', N'810d7d09-2dd6-4272-95e0-81776b4339d6', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', CAST(66.00 AS Decimal(15, 2)), CAST(N'2021-01-04T10:41:27.0000000' AS DateTime2), NULL, CAST(N'2021-09-14T04:40:12.0000000' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'05eae147-4f19-40a9-8210-ec03b19a8ebb', N'dfsfsd', CAST(N'2022-04-25T08:02:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'IntegrationEventContext2', N'IntegrationEventContext2', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-25T15:03:48.0000000' AS DateTime2), N'6', CAST(N'2022-04-25T15:03:48.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN20224251524736')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'06e7a099-b584-4f16-bbe2-2490178f930d', N'VCI001', CAST(N'2021-09-06T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T01:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'1102565d-4db0-4ed3-9d46-f5d0877f46c0', N'VCI002', CAST(N'2021-09-12T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T02:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'167e4eb6-5beb-44ac-9cbc-c76e131f852c', N'VCI003', CAST(N'2021-09-13T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T03:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'17efccef-9811-4ada-aea4-34c4654ad1f9', N'423423fsdfsd', CAST(N'2022-04-02T02:59:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fsdfsd', N'fsdfsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:59:50.0000000' AS DateTime2), N'7', CAST(N'2022-04-02T09:59:50.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'16505')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'1a251eb5-480e-4285-9cfa-5a7e34f47154', N'PN2022714143451255', CAST(N'2022-07-10T06:34:00.0000000' AS DateTime2), N'3d4e0634-5f64-4e1a-8644-3dc2fd245415', N'Nguyễn Khả Hợp', N'Phạm Văn Chung', NULL, N'Cấp phát', NULL, N'Cấp phát', NULL, CAST(N'2022-07-14T14:39:21.0000000' AS DateTime2), N'9d81692d-35d1-43ae-92d1-df17844b774e', CAST(N'2022-07-15T10:59:54.0000000' AS DateTime2), NULL, 0, N'0929228035', N'Đông Anh, Hà Nội', N'CNTT IT 12345', N'0123456789', N'Hải Phòng', N'CNTT', N'PN2022714143451255')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'1d2a4780-a416-4f8c-8bdc-dae4c697f15d', N'fdsf534vcx', CAST(N'2022-05-20T07:01:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fdsfsd', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:01:27.0000000' AS DateTime2), N'2', CAST(N'2022-05-20T14:01:45.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202252014118745')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'20ef0f13-0711-490a-9d6b-4c8fc8c08050', N'fsdfsdfs', CAST(N'2022-04-02T02:49:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fwfsd', N'fsdfsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:49:26.0000000' AS DateTime2), N'6', CAST(N'2022-04-02T09:49:26.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'274050573')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'229c764d-1359-4356-aa75-c348e0537ad6', N'VCI004', CAST(N'2021-09-04T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T04:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'27be213f-f198-4acd-ad33-ee36fd618645', N'fsdfsd', CAST(N'2022-04-02T02:51:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'fsdfsd', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:51:56.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:51:56.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'429532534')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'292602d3-da68-4e90-91f7-c2de7f6c6064', N'dsad324vxcvxc', CAST(N'2022-05-20T07:04:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsadas', N'dsadas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:04:21.0000000' AS DateTime2), N'5', CAST(N'2022-05-20T14:04:21.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202252014412785')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'2ef3aabe-0a73-44ac-af0b-77c166b32860', N'tetaetatse', CAST(N'2022-04-25T08:05:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'tetaetatse', N'tetaetatse', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-25T15:07:46.0000000' AS DateTime2), N'5', CAST(N'2022-04-25T15:07:46.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202242515559492')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'2fda64d8-74de-4056-a7a1-54853e5d4152', N'c3424fz', CAST(N'2022-05-20T07:15:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'czxc', N'czxczx', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:15:35.0000000' AS DateTime2), N'7', CAST(N'2022-06-10T11:50:10.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141527245')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'3229d21d-a0e8-4d1a-98da-1fa0199368f9', N'IntegrationEventContext', CAST(N'2022-04-25T08:02:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'IntegrationEventContext', N'IntegrationEventContext', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-25T15:02:35.0000000' AS DateTime2), N'5', CAST(N'2022-04-25T15:02:35.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202242515222154')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'33b2fea0-6920-4ee7-bcfa-605aeff79333', N'mnb mn', CAST(N'2021-10-17T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'gdfgdf', NULL, N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'20', NULL, NULL, N'null', CAST(N'2021-10-22T03:44:38.0000000' AS DateTime2), N'06b74d87-9994-48c4-8312-0ffb34bb3989', CAST(N'2021-10-22T04:31:43.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'3a28e8bb-fa6d-4fc2-9673-38581d77dafd', N'dâs342423', CAST(N'2022-05-20T07:17:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsadsa', N'dsadas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:18:05.0000000' AS DateTime2), N'6', CAST(N'2022-06-10T11:23:12.0000000' AS DateTime2), N'9d81692d-35d1-43ae-92d1-df17844b774e', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141753397')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'3ef4e0c8-d9b2-44f3-bec6-4bdd1ca81217', N'dasadasdas', CAST(N'2022-05-20T07:00:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'đâs', N'đâs', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:00:10.0000000' AS DateTime2), N'4', CAST(N'2022-05-20T14:01:04.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN20225201401691')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'4abeccf8-1260-45f8-947c-99f2680d3046', N'dasda321312', CAST(N'2022-04-02T03:07:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'2131221', N'dasdsadas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:08:03.0000000' AS DateTime2), N'6', CAST(N'2022-04-21T12:26:20.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'350126687')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'4addc10d-8169-47a8-a288-185c7a75f897', N'VCI005', CAST(N'2021-09-16T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'4b9052db-59a7-4604-9ebd-b87c2cd2b569', N'dasd432', CAST(N'2022-04-02T03:54:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'423423', N'sdadasdas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:54:38.0000000' AS DateTime2), N'6', CAST(N'2022-04-02T10:54:38.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'140904457')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'4feb4880-4ae5-49f5-b8f6-7353e07cb8d8', N'sfsdfsdfsd', CAST(N'2022-04-22T07:18:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'rẻwerwe', N'rưerwerwerwe', N'786545ee-ad79-4f40-8c81-316acdefd375', NULL, NULL, NULL, NULL, CAST(N'2022-04-22T14:19:07.0000000' AS DateTime2), N'7', CAST(N'2022-04-22T14:19:07.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'460695449')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'509f1f69-4d36-410a-a4ba-8956dadcc333', N'fsdfsdfsd', CAST(N'2022-05-20T07:15:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'sdfsdfsd', N'fsdf', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:15:53.0000000' AS DateTime2), N'7', CAST(N'2022-06-10T13:27:50.0000000' AS DateTime2), N'c2483cc3-ecab-471c-a472-df0c8dbd5dc4', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141545723')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'51dd8112-08da-4d01-a181-977dba2b1b94', N'fsdfsdfsd', CAST(N'2022-04-02T02:53:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'fsdfsdfsd', N'fsdfsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:54:08.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:54:08.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'336648881')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'5299eb23-a2f5-4b42-8738-db84464a256a', N'123456789', CAST(N'2022-03-07T06:05:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'Nguyễn Văn A', N'Nguyễn Văn B', N'12a52803-613f-4055-9c73-df7bb6c4179c', N'Nguyễn Văn B', NULL, N'Nguyễn Văn A', NULL, CAST(N'2022-03-07T13:10:34.0000000' AS DateTime2), N'0', CAST(N'2022-03-07T13:10:34.0000000' AS DateTime2), NULL, 0, N'123456789', N'Hà Nội', N'CNTT', N'12345679', N'Hà Nội', N'CNTT 2', N'513105090')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'5a9d8ba4-d237-423f-917d-4b7a927b2cda', N'423423423', CAST(N'2022-05-20T06:45:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsdas', N'dsadas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:46:09.0000000' AS DateTime2), N'5', CAST(N'2022-05-20T13:46:09.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520134558506')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'5c809316-6c73-4bd9-8ba2-5306c2018c0c', N'vc00000342432', CAST(N'2022-04-01T08:54:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'đâs', N'đâs', N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', N'sdas', NULL, N'đâs', NULL, CAST(N'2022-04-01T15:55:36.0000000' AS DateTime2), N'0', CAST(N'2022-04-01T15:55:36.0000000' AS DateTime2), NULL, 0, N'423423423', N'đấ', N'đâs', N'423423423', N'đâs', N'đấ', N'220785314')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'5e38dcab-e13f-4350-9693-ccd9bf2d644c', N'fsdfsdfsd', CAST(N'2022-04-02T03:04:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsads', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:06:11.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T10:07:34.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'978572996')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'65710cfd-e361-4ceb-8aee-5426d75d8292', N'ghjk', CAST(N'2021-10-16T17:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'vanss', N'hjgk', N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', N'20', NULL, N'hgjk', N'null', CAST(N'2021-10-17T16:17:59.0000000' AS DateTime2), NULL, CAST(N'2021-10-17T16:17:59.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6826208c-11ab-4a1f-83f3-42307e453063', N'fsdfsd5324vcx', CAST(N'2022-04-02T02:47:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'ffsdfsd', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:47:39.0000000' AS DateTime2), N'6', CAST(N'2022-04-02T09:47:39.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'54380481')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'693ff1bb-81b3-42d9-bb74-9e6e329009a6', N'fsdfsd', CAST(N'2022-04-02T03:54:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'rwds', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:55:07.0000000' AS DateTime2), N'4', CAST(N'2022-04-21T12:26:10.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'601465015')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6a0a3fbd-e1cb-4392-9ba8-96b820e9ef18', N'dâsd', CAST(N'2022-04-25T08:11:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'đâs', N'đâsdas', NULL, NULL, NULL, N'đâs', NULL, CAST(N'2022-04-25T15:12:08.0000000' AS DateTime2), N'5', CAST(N'2022-04-25T15:12:08.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022425151146531')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', N'rewcvxvc', CAST(N'2022-04-04T08:46:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'ưewer', N'rewrwe', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-04T15:46:39.0000000' AS DateTime2), N'5', CAST(N'2022-04-21T14:28:05.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'44496733')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6bd1f9f2-b3db-41ec-974d-e2bc5ec664a0', N'sadas423423', CAST(N'2022-04-02T03:33:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'ddas', N'dasdas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:33:21.0000000' AS DateTime2), N'4', CAST(N'2022-04-02T10:33:21.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'933895371')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6d7874b7-2d8f-4fbd-bd76-e3bf94c788f0', N'432423423', CAST(N'2022-05-20T06:43:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsds', N'fdsfsdfds', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:44:02.0000000' AS DateTime2), N'3', CAST(N'2022-05-20T13:44:02.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520134349802')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'6eadafc9-1b1d-445a-94c7-7035e6e1b00f', N'cvb34vbdf', CAST(N'2022-04-02T02:42:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dasdas', N'vxcvxcvxc', NULL, NULL, NULL, N'xczcx', NULL, CAST(N'2022-04-02T09:43:28.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:43:28.0000000' AS DateTime2), NULL, 1, N'423423', NULL, NULL, N'4423423423', NULL, NULL, N'804438651')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'7068a2a1-3eb5-43be-9031-b3912e64537f', N'VCI006', CAST(N'2021-09-17T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'74f85b4c-9b87-43a7-b688-191370066e2f', N'VCI007', CAST(N'2021-09-26T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'75c1a7e2-710b-45a3-9745-41e5728e32c2', N'rwerwe432423', CAST(N'2022-04-02T03:01:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'rwerwe', N'rwerwerwe', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:02:13.0000000' AS DateTime2), N'4', CAST(N'2022-04-02T10:02:13.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'744443358')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'76369535-3a22-4c0a-aa05-106d265578ac', N'dasdas4423423', CAST(N'2022-04-02T02:41:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dasdasdas', N'dasdas', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', NULL, NULL, N'dasdasdas', NULL, CAST(N'2022-04-02T09:42:03.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:42:03.0000000' AS DateTime2), NULL, 1, N'dasdas', NULL, NULL, NULL, NULL, NULL, N'600491959')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'778ef079-9339-4463-91c4-d18d2bb7bb46', N'www', CAST(N'2021-10-13T17:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'www', N'test', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'30', NULL, N'www', N'null', CAST(N'2021-10-14T15:47:25.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T15:47:25.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'7a4bdb0f-7b39-4b3d-ae5b-95d48578b244', N'das423423', CAST(N'2022-04-02T03:28:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'43sasdas', N'dasdasdas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:29:06.0000000' AS DateTime2), N'4', CAST(N'2022-04-02T10:29:06.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'440332818')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'7acb7afd-9cb2-46ef-a22d-158f6f1d6f25', N'423423dsadas', CAST(N'2022-04-02T02:56:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dasdas', N'dasdas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:56:23.0000000' AS DateTime2), N'6', CAST(N'2022-04-02T09:56:23.0000000' AS DateTime2), NULL, 1, N'432423423', NULL, NULL, N'423423', NULL, NULL, N'31129556')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'7b875866-1a23-48b9-98d6-707b35e2d692', N'vvvvvvv', CAST(N'2021-10-21T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'10', NULL, NULL, N'null', CAST(N'2021-10-22T05:18:00.0000000' AS DateTime2), N'dbd3a4e0-1738-4bd7-918e-a3b64436cd42', CAST(N'2021-10-22T05:18:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'7f82ada1-a55a-4352-b2f9-c05d216932de', N'23312312', CAST(N'2022-05-20T06:52:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'đasa', N'dsadas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:52:22.0000000' AS DateTime2), N'4', CAST(N'2022-05-20T13:52:22.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520135211669')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'81f37790-173b-48d6-9edf-86514fbdb1d2', N'fsd43534534gfdgdf', CAST(N'2022-04-22T07:16:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'sfsdf', N'fsdfsdfsd', N'a1269b0d-6bc5-4aa1-a79c-5f78635f64b1', NULL, NULL, NULL, NULL, CAST(N'2022-04-22T14:16:53.0000000' AS DateTime2), N'5', CAST(N'2022-04-22T14:16:53.0000000' AS DateTime2), NULL, 0, N'432423', NULL, NULL, N'423423423', NULL, NULL, N'870480128')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'867b6c92-5454-4354-aa1b-80078cfc1449', N'jhg7657mbn', CAST(N'2022-05-20T07:02:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'jghj', N'ghjghjgh', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:02:48.0000000' AS DateTime2), N'5', CAST(N'2022-05-20T14:02:48.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN20225201423894')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'870b9af1-3c9e-476e-90ab-ed174ffbf6b8', N'eqwe234423', CAST(N'2022-04-02T03:24:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'eqweqwe', N'eqweqweqw', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:25:07.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T10:25:07.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'88245363')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'881c8859-b34a-4896-959b-19dab8fd53a9', N'test kafka', CAST(N'2022-04-25T06:50:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'test kafka', N'test kafka', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'test kafka', NULL, N'test kafka', NULL, CAST(N'2022-04-25T14:30:06.0000000' AS DateTime2), N'5', CAST(N'2022-04-25T14:30:06.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022425135031354')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'8a148d80-2cb5-4000-9470-bf334841b4a0', N'fdsfsd5vxvcx', CAST(N'2022-05-20T07:18:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fdsfsd', N'fdsfsdfsd', NULL, NULL, NULL, N'1111111111111', NULL, CAST(N'2022-05-20T14:19:03.0000000' AS DateTime2), N'6', CAST(N'2022-07-01T09:44:44.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141849492')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'96b96ade-db85-481d-aabb-c47877a583d9', N'rwerwer', CAST(N'2022-04-02T03:55:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'rewrwe', N'fsdfsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:56:11.0000000' AS DateTime2), N'5', CAST(N'2022-04-21T14:25:38.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'562191778')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'9d57ebb8-6d89-41ab-a393-5b0554a654be', N'VCI008', CAST(N'2021-09-02T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'a13a885d-cc0e-45f9-8753-1af25c0ca285', N'test kafka', CAST(N'2022-04-25T07:30:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'test kafka', N'test kafka', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-25T14:33:39.0000000' AS DateTime2), N'5', CAST(N'2022-04-25T14:33:39.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202242514305610')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'a13c3d8c-c614-438e-82ac-b68643f6eab0', N'vcxvxc324vcxvxc', CAST(N'2022-05-20T07:04:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'xcvxcvvcx', N'vcxvcx', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:05:03.0000000' AS DateTime2), N'5', CAST(N'2022-05-20T14:05:03.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202252014454626')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'a72c692c-89e4-421e-976f-57373aab68f2', N'test kafka', CAST(N'2022-04-25T07:56:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'test kafka', N'test kafka', N'55708ae7-0c75-4620-8ad8-ea0fee8632d9', N'test kafka', NULL, N'test kafka', NULL, CAST(N'2022-04-25T14:56:57.0000000' AS DateTime2), N'6', CAST(N'2022-04-25T14:56:57.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022425145639609')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'a802af21-8ddd-474f-9ac9-d9377f1ada4d', N'IN20211012000001', CAST(N'2021-10-09T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'SAM', N'HTC-ITC', N'ee574f85-e571-4e73-8e17-12b2c4965326', N'10', NULL, N'Nh?p thi?t b? T10-2021', N'null', CAST(N'2021-10-13T03:27:46.0000000' AS DateTime2), N'4a20d949-31bb-4ab7-9870-4e2120eab859', CAST(N'2021-10-19T13:34:43.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'a85473b1-705c-4278-bddd-c1d20b24c503', N'LAP001', CAST(N'2022-03-09T01:50:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'Nguyễn Khả Hợp', N'Nguyễn Văn B', N'12a52803-613f-4055-9c73-df7bb6c4179c', N'gửi lap', NULL, N'Gửi lap', NULL, CAST(N'2022-03-09T08:53:03.0000000' AS DateTime2), N'0', CAST(N'2022-03-09T08:53:03.0000000' AS DateTime2), NULL, 0, N'038817774', N'Duy Tân, Hà Nội', N'IT', N'099999999', N'hòa Lạc, Hà Nội', N'IT System', N'265171106')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'ac984dc2-e0b0-4433-ba8c-3958b59e465d', N'op[op[op[', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'20', NULL, NULL, N'null', CAST(N'2021-10-20T02:45:45.0000000' AS DateTime2), N'dbd3a4e0-1738-4bd7-918e-a3b64436cd42', CAST(N'2021-10-22T04:29:53.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'ae2ffafc-4066-4adb-8178-10a70f6915e6', N'3213das', CAST(N'2022-04-02T03:16:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'321312', N'312312', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:16:18.0000000' AS DateTime2), N'6', CAST(N'2022-04-21T12:26:15.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'478443348')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'b0621a50-34ef-4d8b-8519-b6b94822c265', N'VCI009', CAST(N'2021-09-25T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'b373b7c6-72f5-4511-8f9d-49d10275dab6', N'test kafka', CAST(N'2022-05-20T07:12:00.0000000' AS DateTime2), N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'test kafka', N'test kafka', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:12:49.0000000' AS DateTime2), N'6', CAST(N'2022-07-12T14:48:16.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141230945')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'c33ff070-de56-44a0-8506-89d91429828d', N'PN202271312173050', CAST(N'2022-07-13T05:17:00.0000000' AS DateTime2), N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd', N'dsadas', N'đâsdsa', NULL, N'432423423', NULL, N'432432', NULL, CAST(N'2022-07-13T12:17:53.0000000' AS DateTime2), N'c2483cc3-ecab-471c-a472-df0c8dbd5dc4', CAST(N'2022-07-13T12:17:53.0000000' AS DateTime2), NULL, 1, N'432423', NULL, NULL, N'342432', NULL, NULL, N'PN202271312173050')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'c64c38ee-ee0f-40f7-b2bc-8c5529fab4b5', N'VCI0010', CAST(N'2021-09-05T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'c65e7088-5895-430e-ae00-6e2851350fe0', N'fgdgdf534bcvbvc', CAST(N'2022-04-02T02:43:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'gdfgdf', N'gdfgdf', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:44:14.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:44:14.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'790026554')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'cb9d683c-5fcc-4340-bd27-acd24e25ab35', N'dsasd44423423', CAST(N'2022-04-02T02:37:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'3312312', N'eqweqweqw', N'3c0ffdaf-23b6-4fcc-8a7d-31b5db96feb1', NULL, NULL, NULL, NULL, CAST(N'2022-04-02T09:37:34.0000000' AS DateTime2), N'5', CAST(N'2022-04-02T09:37:34.0000000' AS DateTime2), NULL, 1, N'eweqweqw', NULL, NULL, N'eqweqw', N'eqweqweqw', NULL, N'331977597')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'd1358f51-b30a-469c-8d86-cbaa30742bac', N'111', CAST(N'2022-03-07T05:14:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'1', N'6', N'12a52803-613f-4055-9c73-df7bb6c4179c', N'10', NULL, N'5', NULL, CAST(N'2022-03-07T05:14:00.0000000' AS DateTime2), N'0', CAST(N'2022-03-07T05:14:00.0000000' AS DateTime2), NULL, 0, N'2', N'3', N'4', N'7', N'8', N'9', NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'd3d6a2e7-10c2-4478-b8b8-f257c22d1364', N'NK0001', CAST(N'2021-10-21T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'Vuong Ng?c H?i', N'Vuong Ng?c H?i', N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', N'10', NULL, NULL, N'null', CAST(N'2021-10-21T03:48:03.0000000' AS DateTime2), N'684bb7fb-cd6f-4d34-9be9-ec54c644fd96', CAST(N'2021-10-21T03:48:03.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'd4c3659f-3987-49e4-a820-1814bf8a777c', N'VCI0015', CAST(N'2021-09-15T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'd57485e6-b0b3-4d50-96be-1e9a30a3e714', N'PN202271311362396', CAST(N'2022-07-13T04:36:00.0000000' AS DateTime2), N'53bd3cad-9dff-4f26-8527-d5789d325d1a', N'Nguyễn Khả Hợp', N'Phạm Văn Trung', N'3d57ecb4-45a0-4cc8-8780-75d9cec990d9', N'test', NULL, N'đâsdas', NULL, CAST(N'2022-07-13T11:37:01.0000000' AS DateTime2), N'9d81692d-35d1-43ae-92d1-df17844b774e', CAST(N'2022-07-13T11:37:01.0000000' AS DateTime2), NULL, 0, N'432423432423', NULL, NULL, N'12544156156', NULL, NULL, N'PN202271311362396')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'db210548-929d-4eaf-bfbf-d5393cff021c', N'test9999999', CAST(N'2022-07-04T08:42:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'test9999999', N'test9999999', NULL, N'test notication', NULL, NULL, NULL, CAST(N'2022-07-04T15:42:45.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', CAST(N'2022-07-15T10:46:12.0000000' AS DateTime2), NULL, 1, NULL, N'test9999999', NULL, N'', N'test9999999', NULL, N'PN202274154212309')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'db31fb7d-7c6d-495c-9482-c38e17b229d0', N'PN202271511321490', CAST(N'2022-07-15T04:03:00.0000000' AS DateTime2), N'3d4e0634-5f64-4e1a-8644-3dc2fd245415', N'fdsf', N'fdsfs', NULL, N'4324', NULL, N'23423423423', NULL, CAST(N'2022-07-15T11:03:43.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', CAST(N'2022-07-15T11:06:54.0000000' AS DateTime2), NULL, 0, N'432432423', NULL, NULL, N'2341432423', NULL, NULL, N'PN202271511321490')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', N'fsd5uerfd', CAST(N'2022-05-20T07:13:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dsad234sd', N'ds423sdf', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:13:56.0000000' AS DateTime2), N'5', CAST(N'2022-06-10T12:03:04.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141343486')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'e50eef6d-3a3b-4bb3-83d5-550bfcac4974', N'fdsfsdf54', CAST(N'2022-05-20T07:03:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fsdfs', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:03:24.0000000' AS DateTime2), N'4', CAST(N'2022-05-20T14:03:24.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202252014316652')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'e5dc6e33-1373-4e66-b634-9fe4a9836c07', N'VCI0016', CAST(N'2021-09-18T04:31:43.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'f89a6469-29e5-41f9-a3c2-2713244d5c07', N'VCI0018', CAST(N'2021-09-20T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'f99d38ff-2de8-48e6-99c0-d12ca8494515', N'5454cvxc', CAST(N'2022-05-20T06:54:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fdsf', N'dsfdsfs', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:54:13.0000000' AS DateTime2), N'3', CAST(N'2022-05-20T13:59:26.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN202252013543739')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', N'PN2022713121825168', CAST(N'2022-07-13T05:18:00.0000000' AS DateTime2), N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd', N'Nguyễn Khả Hợp', N'Phạm Văn Trung', NULL, N'Update', NULL, N'Nguyễn Khả Hợp', NULL, CAST(N'2022-07-13T12:19:03.0000000' AS DateTime2), N'c2483cc3-ecab-471c-a472-df0c8dbd5dc4', CAST(N'2022-07-14T10:19:51.0000000' AS DateTime2), NULL, 0, N'423432423', NULL, NULL, N'423432423', NULL, NULL, N'PN2022713121825168')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'fbac2f2f-408a-4f73-b99a-40878e5a9d70', N'dsadas423423', CAST(N'2022-04-02T03:01:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'dsadas', N'dasdas', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-02T10:01:31.0000000' AS DateTime2), N'6', CAST(N'2022-04-02T10:01:31.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'685965995')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'fc257e08-1ab5-45e8-b7ca-0fe844fa9a22', N'123213123', CAST(N'2021-10-07T17:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Bên A', N'Bên B', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'10', NULL, N'OK', N'null', CAST(N'2021-10-13T06:54:56.0000000' AS DateTime2), N'0ed002dd-2152-4969-839b-4f990f166c38', CAST(N'2021-10-19T13:34:29.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'fc3344f2-649e-49b0-a9ed-f37423eb722f', N'fdsfsdfsd', CAST(N'2022-04-25T08:04:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'fsdfsdf', N'fsdfsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-04-25T15:05:34.0000000' AS DateTime2), N'3', CAST(N'2022-04-25T15:05:34.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022425154389')
INSERT [dbo].[Inward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [Deliver], [Receiver], [VendorId], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [DeliverPhone], [DeliverAddress], [DeliverDepartment], [ReceiverPhone], [ReceiverAddress], [ReceiverDepartment], [Voucher]) VALUES (N'ffc32a20-a24b-4a7e-99fd-c76a15258190', N'fsdfsdfsd', CAST(N'2022-05-20T07:14:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'dfsd', N'fsdfsd', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-20T14:14:47.0000000' AS DateTime2), N'6', CAST(N'2022-06-10T11:51:08.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 0, NULL, NULL, NULL, NULL, NULL, NULL, N'PN2022520141439571')
GO
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'191d34f8-b16f-4a8a-a59c-8a6b58e37278', N'd57485e6-b0b3-4d50-96be-1e9a30a3e714', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'2fbd4a88-8b99-46f9-81d1-6f0e3decaf89', N'db31fb7d-7c6d-495c-9482-c38e17b229d0', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(1.00 AS Decimal(15, 2)), CAST(10000.00 AS Decimal(15, 2)), CAST(10000.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(10000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'47493aec-71d7-49a3-9685-1f0dc7b9cfc7', N'1a251eb5-480e-4285-9cfa-5a7e34f47154', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'f485376d-44de-48e7-91f3-78d2b2a63a93', CAST(150.00 AS Decimal(15, 2)), CAST(10.00 AS Decimal(15, 2)), CAST(1500.00 AS Decimal(15, 2)), CAST(1.50 AS Decimal(15, 2)), CAST(1500.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'48fc77e2-c809-41ae-8c2e-78c75162de8c', N'1a251eb5-480e-4285-9cfa-5a7e34f47154', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'c6e878a8-0665-4783-ac50-235ffd7a7857', CAST(5000.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(5000.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(5000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'74b59301-a728-4375-a8a9-fa35178d4f2f', N'db210548-929d-4eaf-bfbf-d5393cff021c', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'8314fff5-111d-4512-a0db-c0a454826a8c', CAST(100.00 AS Decimal(15, 2)), CAST(50000.00 AS Decimal(15, 2)), CAST(5000000.00 AS Decimal(15, 2)), CAST(100.00 AS Decimal(15, 2)), CAST(5000000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'7cfd0050-2c86-43fe-b250-e10a5b408581', N'4feb4880-4ae5-49f5-b8f6-7353e07cb8d8', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(3.00 AS Decimal(15, 2)), CAST(33333.00 AS Decimal(15, 2)), CAST(99999.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(99999.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'7d4cd5c4-ba5c-4613-8f97-d8ca1f8f99cd', N'3a28e8bb-fa6d-4fc2-9673-38581d77dafd', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'83959494-a175-4c15-8e69-a9dee13e2eb6', N'a802af21-8ddd-474f-9ac9-d9377f1ada4d', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(100000.00 AS Decimal(15, 2)), CAST(600000.00 AS Decimal(15, 2)), CAST(12.00 AS Decimal(15, 2)), CAST(600000.00 AS Decimal(15, 2)), N'14', NULL, N'41bf6c16-ca1f-4c6e-86e8-05093065ca8b', NULL, N'230', NULL, N'2', NULL, N'104', NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'856c51c1-3fc9-493e-8b83-6ba4ec4a5c70', N'509f1f69-4d36-410a-a4ba-8956dadcc333', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'857b05c3-66c8-4cae-8990-5cd323b0a708', N'1d2a4780-a416-4f8c-8bdc-dae4c697f15d', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'8c1b6fc4-611f-4709-adcd-80c189a5d626', N'7acb7afd-9cb2-46ef-a22d-158f6f1d6f25', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', CAST(430.00 AS Decimal(15, 2)), CAST(32432.00 AS Decimal(15, 2)), CAST(13945760.00 AS Decimal(15, 2)), CAST(860.00 AS Decimal(15, 2)), CAST(13945760.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'8cb83a34-fbab-41f6-8450-324d1535207f', N'5299eb23-a2f5-4b42-8738-db84464a256a', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', CAST(5.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(25.00 AS Decimal(15, 2)), CAST(10.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), N'0', N'Department 0', N'1', N'Employee 1', N'2', N'Station 2', N'3', N'Project 3', N'5', N'Customer 5', 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'8e1e8210-6b40-4aa4-841f-acfe2da2ab20', N'a802af21-8ddd-474f-9ac9-d9377f1ada4d', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(8.00 AS Decimal(15, 2)), CAST(100000.00 AS Decimal(15, 2)), CAST(800000.00 AS Decimal(15, 2)), CAST(16.00 AS Decimal(15, 2)), CAST(800000.00 AS Decimal(15, 2)), N'14', NULL, N'41bf6c16-ca1f-4c6e-86e8-05093065ca8b', NULL, N'230', NULL, N'2', NULL, N'104', NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'928a5aa4-0add-4591-8952-32b453e295f3', N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(3.00 AS Decimal(15, 2)), CAST(30000.00 AS Decimal(15, 2)), CAST(90000.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(90000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'9884478c-78f1-40c7-8df4-69133906c0d0', N'cb9d683c-5fcc-4340-bd27-acd24e25ab35', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(55.00 AS Decimal(15, 2)), CAST(55.00 AS Decimal(15, 2)), CAST(3025.00 AS Decimal(15, 2)), CAST(110.00 AS Decimal(15, 2)), CAST(3025.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'9c96c17f-091f-498f-8d01-37d8b8a30af3', N'3229d21d-a0e8-4d1a-98da-1fa0199368f9', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'9f9268d8-35bb-4352-8f06-548915ceb083', N'fc257e08-1ab5-45e8-b7ca-0fe844fa9a22', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(10.00 AS Decimal(15, 2)), CAST(50000.00 AS Decimal(15, 2)), CAST(50000.00 AS Decimal(15, 2)), CAST(20.00 AS Decimal(15, 2)), CAST(100000.00 AS Decimal(15, 2)), N'4', NULL, N'4a20d949-31bb-4ab7-9870-4e2120eab859', NULL, N'399', NULL, N'2', NULL, N'108', NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'a327467e-d653-46e8-b156-6b5ac4b12711', N'c64c38ee-ee0f-40f7-b2bc-8c5529fab4b5', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(35.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(70.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'a707b477-28a5-48e6-8f25-fabce497512e', N'a13a885d-cc0e-45f9-8753-1af25c0ca285', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'aa5ba05f-6bb2-47af-bbd4-0e6da4baca88', N'693ff1bb-81b3-42d9-bb74-9e6e329009a6', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'af60ac0f-c828-4ff1-b792-d9b6609d5f69', N'4b9052db-59a7-4604-9ebd-b87c2cd2b569', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'b9958049-ad4a-4378-9c35-6b5225303512', N'1a251eb5-480e-4285-9cfa-5a7e34f47154', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(100.00 AS Decimal(15, 2)), CAST(10000.00 AS Decimal(15, 2)), CAST(1000000.00 AS Decimal(15, 2)), CAST(100.00 AS Decimal(15, 2)), CAST(1000000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'ba479352-890b-4d24-b193-0072160c20d5', N'a72c692c-89e4-421e-976f-57373aab68f2', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(50.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(250.00 AS Decimal(15, 2)), CAST(100.00 AS Decimal(15, 2)), CAST(250.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'bde8810e-40a1-4b2c-9b97-85e8def39d53', N'2ef3aabe-0a73-44ac-af0b-77c166b32860', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'c1b18344-771d-4c83-bcbb-24957ad8975f', N'f99d38ff-2de8-48e6-99c0-d12ca8494515', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'8314fff5-111d-4512-a0db-c0a454826a8c', CAST(2.00 AS Decimal(15, 2)), CAST(30000.00 AS Decimal(15, 2)), CAST(60000.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'c6bc4909-388d-4f67-b7db-6eaf4423e7fd', N'27be213f-f198-4acd-ad33-ee36fd618645', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'8314fff5-111d-4512-a0db-c0a454826a8c', CAST(9999.00 AS Decimal(15, 2)), CAST(999.00 AS Decimal(15, 2)), CAST(9989001.00 AS Decimal(15, 2)), CAST(19998.00 AS Decimal(15, 2)), CAST(9989001.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'c749e754-7a6f-438a-96b6-caa0b0b4a17a', N'05eae147-4f19-40a9-8210-ec03b19a8ebb', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'8314fff5-111d-4512-a0db-c0a454826a8c', CAST(5.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(25.00 AS Decimal(15, 2)), CAST(10.00 AS Decimal(15, 2)), CAST(25.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'cc035309-4d05-411e-9713-711ad5aa2b29', N'fc3344f2-649e-49b0-a9ed-f37423eb722f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'cdd8661e-0524-4fee-98c5-63b3d52fdf1b', N'fbac2f2f-408a-4f73-b99a-40878e5a9d70', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'492fd220-aeed-42a7-956d-ad11c324f3d0', CAST(423.00 AS Decimal(15, 2)), CAST(423423.00 AS Decimal(15, 2)), CAST(179107929.00 AS Decimal(15, 2)), CAST(846.00 AS Decimal(15, 2)), CAST(179107929.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'd06c827d-543f-4217-9af3-15a58a905dc3', N'7a4bdb0f-7b39-4b3d-ae5b-95d48578b244', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'd13a3107-ecb8-4b0d-8551-83a30e546840', N'76369535-3a22-4c0a-aa05-106d265578ac', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(99.00 AS Decimal(15, 2)), CAST(999.00 AS Decimal(15, 2)), CAST(98901.00 AS Decimal(15, 2)), CAST(198.00 AS Decimal(15, 2)), CAST(98901.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'd2740eba-b7ab-4065-9a46-f9332a43c85b', N'2fda64d8-74de-4056-a7a1-54853e5d4152', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'd2e9ff52-0f44-4097-bdd3-afd571f1ddc5', N'c33ff070-de56-44a0-8506-89d91429828d', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'd5b25348-408b-468f-8806-e6fb1ac46307', N'6eadafc9-1b1d-445a-94c7-7035e6e1b00f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', CAST(9.00 AS Decimal(15, 2)), CAST(1000000.00 AS Decimal(15, 2)), CAST(9000000.00 AS Decimal(15, 2)), CAST(18.00 AS Decimal(15, 2)), CAST(9000000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'db760720-98f7-4d47-8475-256defa1689a', N'6826208c-11ab-4a1f-83f3-42307e453063', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(9.00 AS Decimal(15, 2)), CAST(999999.00 AS Decimal(15, 2)), CAST(8999991.00 AS Decimal(15, 2)), CAST(18.00 AS Decimal(15, 2)), CAST(8999991.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'ea3d4338-7480-49eb-92df-116bfa6ca4e0', N'a802af21-8ddd-474f-9ac9-d9377f1ada4d', N'05ce3050-d239-4cf5-8431-351484b88eac', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(100000.00 AS Decimal(15, 2)), CAST(600000.00 AS Decimal(15, 2)), CAST(12.00 AS Decimal(15, 2)), CAST(600000.00 AS Decimal(15, 2)), N'14', NULL, N'41bf6c16-ca1f-4c6e-86e8-05093065ca8b', NULL, N'230', NULL, N'2', NULL, N'104', NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'ea424f26-07a9-4593-bb14-4ba95e8d31d0', N'96b96ade-db85-481d-aabb-c47877a583d9', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'ed62136f-be47-4e04-8460-0a79598b0e13', N'd4c3659f-3987-49e4-a820-1814bf8a777c', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(23.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(46.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'ee5e0718-308e-49fe-ae4d-e7e2b84d72c7', N'06e7a099-b584-4f16-bbe2-2490178f930d', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(45.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(90.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'f105b19d-776f-4e9e-90d5-8bb4db70c9f5', N'ae2ffafc-4066-4adb-8178-10a70f6915e6', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(2.00 AS Decimal(15, 2)), CAST(2.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'f3158e52-7ab4-4c30-9bfe-5bf8d87d74f2', N'6a0a3fbd-e1cb-4392-9ba8-96b820e9ef18', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'fa16da30-08f2-42a9-a504-89e0c5c45ac1', N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(111111111.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(111111111.00 AS Decimal(15, 2)), CAST(222222222.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[InwardDetail] ([Id], [InwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'fa6fbb12-7a59-48e4-9027-bd2760a57c19', N'b373b7c6-72f5-4511-8f9d-49d10275dab6', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'06e7a099-b584-4f16-bbe2-2490178f930d', N'vco001', CAST(N'2021-09-10T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'0a3b7cea-f952-463e-8ca3-01701eda0fb8', N'vco002', CAST(N'2021-09-30T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'0d266986-3b3d-43f4-97df-c4b52bf26b96', N'ooooooo', CAST(N'2021-10-21T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'20', NULL, NULL, N'null', CAST(N'2021-10-22T05:18:45.0000000' AS DateTime2), N'dbd3a4e0-1738-4bd7-918e-a3b64436cd42', CAST(N'2021-10-22T05:18:45.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'10862a22-0743-40eb-8e8a-9e112b494f22', N'zxxzxzxzxzx', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'52bb3cad-6855-44a8-b522-1d079cea7649', N'Th? kho', NULL, N'20', NULL, N'dfgdfg', N'null', CAST(N'2021-10-20T03:02:31.0000000' AS DateTime2), N'cef5fb8e-e9ae-4eef-a12f-be169b82a9fe', CAST(N'2021-10-20T03:20:55.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'1102565d-4db0-4ed3-9d46-f5d0877f46c0', N'vco003', CAST(N'2021-09-11T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'1104af87-c6a2-4a17-b6ae-03ded1669971', N'rrr', CAST(N'2021-10-17T17:00:00.0000000' AS DateTime2), N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', N'Th? kho', N'rrr', N'20', NULL, N'rrr', N'null', CAST(N'2021-10-14T15:51:27.0000000' AS DateTime2), N'4a20d949-31bb-4ab7-9870-4e2120eab859', CAST(N'2021-10-20T03:26:41.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'167e4eb6-5beb-44ac-9cbc-c76e131f852c', N'vco004', CAST(N'2021-09-13T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'229c764d-1359-4356-aa75-c348e0537ad6', N'vco005', CAST(N'2021-09-08T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'2636e50c-f3ad-485c-8a5e-43d5c7302348', N'eeeee', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', N'eeeee', N'20', NULL, N'eeeee', N'null', CAST(N'2021-10-20T03:24:11.0000000' AS DateTime2), N'e79dc303-d866-4e23-9ed6-dc87cb9e6771', CAST(N'2021-10-20T03:24:38.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'2c8ecfa9-80f0-4b3a-b660-491b717ff4a3', N'vco006', CAST(N'2021-09-27T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'4a53b597-188b-4055-aced-4bebe3eb028d', N'vco007', CAST(N'2021-09-29T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'4addc10d-8169-47a8-a288-185c7a75f897', N'vco008', CAST(N'2021-09-15T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'5b2e0f27-09cd-4eb8-81b2-f15de87c05cc', N'wwww111', CAST(N'2021-10-17T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'20', NULL, N'oipio', N'null', CAST(N'2021-10-20T02:42:04.0000000' AS DateTime2), N'e5f2705d-f42f-4280-8694-76ca59775840', CAST(N'2021-10-20T03:22:21.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'6146dd48-2c03-45dd-afb3-d93171ff4918', N'fghdfghfg', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'20', NULL, NULL, N'null', CAST(N'2021-10-20T02:04:19.0000000' AS DateTime2), N'b89eb1e3-34eb-44f3-a14c-4be82a3034fe', CAST(N'2021-10-20T03:21:35.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', N'trung', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', N'ghjfghj', N'20', NULL, N'fghjfghj', NULL, CAST(N'2021-10-20T01:41:07.0000000' AS DateTime2), N'4a20d949-31bb-4ab7-9870-4e2120eab859', CAST(N'2022-07-13T12:39:07.0000000' AS DateTime2), N'9a5fc419-63e0-423e-98f6-058c164c7a9b', 1, N'FSDFSD', N'FSDFSD', N'432423', N'FSDFSD', N'SDFASDF', N'423423', N'VCPX00909')
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'6eeb9999-a9bd-43d7-940d-40e20a27b565', N'dfgdfgdfgd', CAST(N'2021-10-18T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'20', NULL, N'dfgdfg', N'null', CAST(N'2021-10-20T03:01:23.0000000' AS DateTime2), N'71385493-48cc-4d90-83db-c1044d063446', CAST(N'2021-10-20T03:21:54.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'7068a2a1-3eb5-43be-9031-b3912e64537f', N'vco009', CAST(N'2021-09-16T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'74f85b4c-9b87-43a7-b688-191370066e2f', N'vco0010', CAST(N'2021-09-24T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'87679c26-9d60-4a08-abf0-45406709ca14', N'vc005', CAST(N'2022-04-08T08:33:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'Nguyễn Văn A555', N'Nguyễn Văn B', N'ĐÂS', NULL, N'ÐÂSDASDAS', NULL, CAST(N'2022-04-08T15:34:39.0000000' AS DateTime2), N'5', CAST(N'2022-05-20T14:36:59.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'751629002')
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'88ea580c-4f35-4698-9e38-5195c72f9f21', N'test00001', CAST(N'2021-10-16T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', N'Nguy?n Van A', N'20', NULL, N'dâsdas', N'null', CAST(N'2021-10-20T01:51:10.0000000' AS DateTime2), N'e5f2705d-f42f-4280-8694-76ca59775840', CAST(N'2021-10-21T09:50:35.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'9d57ebb8-6d89-41ab-a393-5b0554a654be', N'vco0011', CAST(N'2021-09-07T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'aaeacfe2-1ed6-407a-98a7-328143e04c72', N'PX001', CAST(N'2022-03-09T01:53:00.0000000' AS DateTime2), N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'Nguyễn Khả Hợp', N'Nguyễn Văn B', N'IT', NULL, N'IT', NULL, CAST(N'2022-03-09T08:55:44.0000000' AS DateTime2), N'1', CAST(N'2022-03-09T08:55:44.0000000' AS DateTime2), NULL, 1, N'IT', N'Hòa Lạc, Hà Nội', N'098888888', N'IT', N'Duy Tân, Hà Nội', N'0929228035', NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'b0621a50-34ef-4d8b-8519-b6b94822c265', N'vco0012', CAST(N'2021-09-23T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'b8423139-0064-469a-a407-f9cb60c053f9', N'vco0015', CAST(N'2021-09-28T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'c64c38ee-ee0f-40f7-b2bc-8c5529fab4b5', N'vco0017', CAST(N'2021-09-09T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'debcbb39-5246-4543-bbbc-29d7e298f68d', N'vco0019', CAST(N'2021-09-20T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, N'Th? Kho', N'Ð? Th? Huong', N'10', N'sdgs', N'ffd', NULL, CAST(N'2021-09-20T03:52:26.0000000' AS DateTime2), NULL, CAST(N'2021-09-20T03:52:26.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'e40735a1-e3ba-45dc-967d-fae66d6e59a2', N'vco00121', CAST(N'2021-09-20T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, N'Th? Kho', N'Ð? Th? Huong', N'10', N'sdgs', N'ffd', NULL, CAST(N'2021-09-20T03:49:10.0000000' AS DateTime2), NULL, CAST(N'2021-09-20T03:49:10.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'e5dc6e33-1373-4e66-b634-9fe4a9836c07', N'vco001997', CAST(N'2021-09-17T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'e6751f20-41b0-4125-b9ec-9eca11a2e51d', N'wwww', CAST(N'2021-10-17T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'20', NULL, N'oipio', N'null', CAST(N'2021-10-20T02:40:01.0000000' AS DateTime2), N'e5f2705d-f42f-4280-8694-76ca59775840', CAST(N'2021-10-20T03:22:51.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'f3455296-f5bc-4680-b29e-20073a3eec7f', N'ghjg', CAST(N'2021-10-19T17:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'Th? kho', NULL, N'70', NULL, N'dsadasdas', N'null', CAST(N'2021-10-21T02:50:24.0000000' AS DateTime2), N'06b74d87-9994-48c4-8312-0ffb34bb3989', CAST(N'2021-10-21T04:00:37.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'f89a6469-29e5-41f9-a3c2-2713244d5c07', NULL, CAST(N'2021-09-22T00:00:00.0000000' AS DateTime2), N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, NULL, NULL, N'20', NULL, NULL, NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-10T00:00:00.0000000' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Outward] ([Id], [VoucherCode], [VoucherDate], [WareHouseID], [ToWareHouseId], [Deliver], [Receiver], [Reason], [ReasonDescription], [Description], [Reference], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete], [ReceiverDepartment], [ReceiverAddress], [ReceiverPhone], [DeliverDepartment], [DeliverAddress], [DeliverPhone], [Voucher]) VALUES (N'fc4d73e0-675e-4e8f-bde9-636b97fd32aa', N'PX2022715101751962', CAST(N'2022-07-15T03:17:00.0000000' AS DateTime2), N'3d4e0634-5f64-4e1a-8644-3dc2fd245415', NULL, N'ẻwqrqwrw', N'rưerwrwe', N'fdsfsdf', NULL, N'sdfdsfds', NULL, CAST(N'2022-07-15T10:18:19.0000000' AS DateTime2), N'9d81692d-35d1-43ae-92d1-df17844b774e', CAST(N'2022-07-15T10:18:19.0000000' AS DateTime2), NULL, 1, NULL, NULL, N'423423423', NULL, NULL, N'32423423', N'PX2022715101751962')
GO
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'271cdece-fa10-439f-a183-7dfce017b5a6', N'debcbb39-5246-4543-bbbc-29d7e298f68d', N'eeacaf7c-3106-4927-a312-746a8f087787', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', CAST(2.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), N'1', N'1', N'1', N'1', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'3093d604-2aad-4854-a206-b1d87b1b3845', N'2c8ecfa9-80f0-4b3a-b660-491b717ff4a3', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'3599d153-b5a3-4f01-bd29-d3ed98c179b9', N'fc4d73e0-675e-4e8f-bde9-636b97fd32aa', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(5.00 AS Decimal(15, 2)), CAST(10000.00 AS Decimal(15, 2)), CAST(50000.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(50000.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'430f6aa7-330f-4c95-af63-d96b019c2012', N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'f485376d-44de-48e7-91f3-78d2b2a63a93', CAST(0.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), CAST(99.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'449f2ac9-7cd7-41b3-aa44-f52403f56f2d', N'b0621a50-34ef-4d8b-8519-b6b94822c265', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'44a4b096-bb45-4558-af15-1f607314e27b', N'e5dc6e33-1373-4e66-b634-9fe4a9836c07', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'4d10afd4-0723-4147-970c-d1b0068a4800', N'4addc10d-8169-47a8-a288-185c7a75f897', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'5b8907da-8775-4823-8c39-8983172bd119', N'1102565d-4db0-4ed3-9d46-f5d0877f46c0', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'624414d4-04c4-4120-8004-2d4b2a377306', N'88ea580c-4f35-4698-9e38-5195c72f9f21', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'f485376d-44de-48e7-91f3-78d2b2a63a93', CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'751067b8-6b50-4ba8-8043-6e3309d54144', N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'f485376d-44de-48e7-91f3-78d2b2a63a93', CAST(2.00 AS Decimal(15, 2)), CAST(2.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), CAST(99.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'7b7d30b4-dad3-41e6-b8e7-a1650e7b4bd6', N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'f485376d-44de-48e7-91f3-78d2b2a63a93', CAST(2.00 AS Decimal(15, 2)), CAST(2.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), CAST(99.00 AS Decimal(15, 2)), CAST(4.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'86b2bfb7-60c9-4f07-a2b9-2b82fecf3a05', N'0d266986-3b3d-43f4-97df-c4b52bf26b96', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(99.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'8a7be9f4-b151-438c-86c4-8c99daa49105', N'b8423139-0064-469a-a407-f9cb60c053f9', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(5.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'926e513a-a466-42ee-9f1c-be4757374c5e', N'4a53b597-188b-4055-aced-4bebe3eb028d', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'9c7d430c-9411-4ad5-8a18-93343642e747', N'87679c26-9d60-4a08-abf0-45406709ca14', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'492fd220-aeed-42a7-956d-ad11c324f3d0', CAST(99999.00 AS Decimal(15, 2)), CAST(999.00 AS Decimal(15, 2)), CAST(99899001.00 AS Decimal(15, 2)), CAST(99999.00 AS Decimal(15, 2)), CAST(99899001.00 AS Decimal(15, 2)), N'1', N'Department 1', N'8', N'Employee 8', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'9d017c8e-8d01-4144-953f-1ad7d573cac2', N'74f85b4c-9b87-43a7-b688-191370066e2f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'a0ae520e-e6d2-4bc8-8e67-1ec162ec12bb', N'167e4eb6-5beb-44ac-9cbc-c76e131f852c', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(5.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'a8400c4c-a1a0-495c-8616-95455f24ee8b', N'c64c38ee-ee0f-40f7-b2bc-8c5529fab4b5', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'a8e1a50d-9954-4660-a469-036771bd4393', N'229c764d-1359-4356-aa75-c348e0537ad6', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(8.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(8.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'b5021ab3-8dc9-4942-b46a-62fd79c884f3', N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', N'ae973639-135a-48d7-8f04-0e5bfa660d1a', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', CAST(5.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), CAST(25.00 AS Decimal(15, 2)), CAST(789.00 AS Decimal(15, 2)), CAST(25.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'bdce5cf1-a5d4-40e4-8fea-7ab9bed83649', N'aaeacfe2-1ed6-407a-98a7-328143e04c72', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', CAST(3.00 AS Decimal(15, 2)), CAST(3.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), CAST(99.00 AS Decimal(15, 2)), CAST(9.00 AS Decimal(15, 2)), N'0', N'Department 0', N'0', N'Employee 0', N'0', N'Station 0', N'1', N'Project 1', N'3', N'Customer 3', 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'dff43a9c-df1c-42fc-a9b8-c1d43f9b1c4d', N'7068a2a1-3eb5-43be-9031-b3912e64537f', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(6.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'e6ea7e22-b7fe-473c-9453-b10f10dc4bde', N'0a3b7cea-f952-463e-8ca3-01701eda0fb8', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(7.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[OutwardDetail] ([Id], [OutwardId], [ItemId], [UnitId], [UIQuantity], [UIPrice], [Amount], [Quantity], [Price], [DepartmentId], [DepartmentName], [EmployeeId], [EmployeeName], [StationId], [StationName], [ProjectId], [ProjectName], [CustomerId], [CustomerName], [OnDelete], [AccountMore], [AccountYes], [Status]) VALUES (N'f3a37ec4-bffa-4253-8244-5e302524d7a2', N'88ea580c-4f35-4698-9e38-5195c72f9f21', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), CAST(1.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'066a2ec6-d2de-40ea-899c-ae39af12349b', N'15f148b2-f324-4376-8e40-a07315544dd5', N'SR003', N'8cb83a34-fbab-41f6-8450-324d1535207f', NULL, 1, 0)
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'2846cfca-9741-439c-bb45-a26fab64dc99', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'1', NULL, N'9c7d430c-9411-4ad5-8a18-93343642e747', 1, 0)
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'32a8e69e-90a4-4084-aea1-1db6a2a1a01e', N'15f148b2-f324-4376-8e40-a07315544dd5', N'SR001', N'8cb83a34-fbab-41f6-8450-324d1535207f', NULL, 1, 0)
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'85ef0331-4028-43ef-8513-4395235f783d', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'2', NULL, N'9c7d430c-9411-4ad5-8a18-93343642e747', 1, 0)
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'f52095ae-a31b-4045-a6f0-90f7c02c291f', N'15f148b2-f324-4376-8e40-a07315544dd5', N'SR002', N'8cb83a34-fbab-41f6-8450-324d1535207f', NULL, 1, 0)
INSERT [dbo].[SerialWareHouse] ([Id], [ItemId], [Serial], [InwardDetailId], [OutwardDetailId], [IsOver], [OnDelete]) VALUES (N'f8fd88a6-a6a0-440f-8cc5-fc673e49fef3', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'3', NULL, N'9c7d430c-9411-4ad5-8a18-93343642e747', 1, 0)
GO
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'0528bf58-3cf5-44a5-a09d-89f405699387', N'DSADSA', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'0b0782b6-5a5e-4700-986c-2e1bc2de21e1', N'test 01', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'2d9c66d0-10cc-41c9-90a0-564593a57840', N'test delete new', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', N'Chiếc', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'492fd220-aeed-42a7-956d-ad11c324f3d0', N'M', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'6c646726-a6cc-4036-badd-9652ce8b24c3', N'dsadsa', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'8314fff5-111d-4512-a0db-c0a454826a8c', N'KG', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'9e77fd58-f048-4578-8353-06b66707844c', N'DSADSA', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'9ee1844e-7834-484b-94ef-a4d306c98eda', N'Chiếc', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'ac3f867a-4d41-4b5d-9365-875ba36f9709', N'test dedsadsaletdsadsae new', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'b741bd24-f97f-4a36-aed1-84ec23f22675', N'str', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'c6e878a8-0665-4783-ac50-235ffd7a7857', N'mm', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chiếc', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'f485376d-44de-48e7-91f3-78d2b2a63a93', N'cm', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'f637edb6-c2cd-4f38-977f-0f497bea9d97', N'htm', 1, 0)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'fa7d71d4-5529-4292-ac36-57aa9804d429', N'test dedsadsalete new', 1, 1)
INSERT [dbo].[Unit] ([Id], [UnitName], [Inactive], [OnDelete]) VALUES (N'fd7200ff-3609-412f-8a1b-2924ace18ef1', N'DSADSA', 1, 1)
GO
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'0425d132-63fe-4d13-8fa7-955e88c12737', N'string', N'string1', N'string', N'0388177774', N'string@gmail.com', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'05eac5f2-67f4-47dc-bd3c-5c630038ce84', N'string', N'Add to number: 9539', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'0bad6d2b-3f54-4ca5-8ddd-d38422f2a6bd', N'string', N'Add to number: 8188', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'0c66989a-b6e5-4e5b-9f50-256332b9928b', N'string', N'Add to number: 5652', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'0c78456f-b99e-4aad-aa70-36e7eaea7401', N'string', N'Add to number: 3266', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'0fc444cf-6270-4f5b-a2fc-d52e315ee9ae', N'string', N'Add to number: 3988', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'12a52803-613f-4055-9c73-df7bb6c4179c', N'NCC02', N'NCC02', N'string', N'0388177774', N'string@gmail.com', N'string', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'149868d1-9d66-426d-8a2c-98099f8935a9', N'string', N'Add to number: 8872', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'2845bca3-d4a9-4c60-bc5b-cc408ea374b1', N'string', N'Add to number: 2586', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'2b30a9ef-62e5-47e7-a1ac-daf4d25784f7', N'string', N'Add to number: 5832', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'2c737151-df42-4f25-99be-5efb47106ae3', N'dsadsadsa', N'dsadsadsa', N'', N'', NULL, N'', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'2ff892a9-c59f-4a28-9d65-6e84d71cecb5', N'string', N'Add to number: 7223', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'321', N'test base id1', N'test base id1', N'test base id1', N'00000000', N'test base id', N'test base id', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'3516f44c-987b-4a75-abe1-cd09ef813bcf', N'string', N'Add to number: 3568', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'39286ee6-95af-4fdd-9302-b7db16051a72', N'NCC05', N'NCC05', N'string', N'0388177774', N'string@gmail.com', N'string', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'3c0ffdaf-23b6-4fcc-8a7d-31b5db96feb1', N'CODE0019', N'CODE001    ', N'CODE001    ', N'', NULL, N'0388177774', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'3d57ecb4-45a0-4cc8-8780-75d9cec990d9', N'NCC03', N'NCC03', N'string', N'0388177774', N'string@gmail.com', N'string', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', N'ETCJX9', N'Nguyễn Văn S', N'Hà Nội', N'0388177774', N'dsaa@gmail.com', N'Nguyễn Văn S', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'523e4fb1-e5a9-400c-a074-763b909f3a9e', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'523fec4e-d0af-4f13-a48c-4c88951f783d', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'537a0c7d-cf7f-4d94-9a93-ca887106c96c', N'string', N'Add to number: 1776', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'55708ae7-0c75-4620-8ad8-ea0fee8632d9', N'Code002', N'NCC01', N'Hà Nội', N'0388177774', N'hbs@gmail.com', N'string', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'57daa3c3-ed76-4d2e-94f9-ec343710adcd', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'5f20b0ca-9191-4d38-94b6-23395f5afd82', N'string', N'Add to number: 3163', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'5fbbb772-ea5c-47d4-b37e-f786a5a2222b', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'6002d50b-8e58-4bc0-86da-c81fcc9c77f0', N'string', N'Add to number: 3729', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'67328184-45a6-47e3-b053-d50f942a917e', N'string11111111111', N'string11111111', N'string1111111111111111111', N'string11111111111', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', N'ETCJX99', N'ETCJX99', N'Hà Nội', N'0388177774', N'abc@gmail.com', N'Hà Nội', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'6b779b69-73e3-4955-9778-7086154f85bf', N'string', N'Add to number: 6790', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'6f9aa942-a021-4a0b-b0f7-35599a4a596a', N'string', N'Add to number: 8627', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'71d5c325-32fd-4d59-9117-356132f48228', N'CODE001', N'Adidas', N'Hà Nội', N'0388177774', N'string@gmail.com', N'0999999999', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'786545ee-ad79-4f40-8c81-316acdefd375', N'HONDA01', N'Honda', N'Hà Nội', N'0388177774', N'abc@gmail.com', N'Hà Nội', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'79045d68-f531-4ef8-9ccf-f25c109adaa4', N'string', N'Add to number: 7629', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'7b19b2a9-ed90-4436-b6e7-e8143da972e1', N'string', N'Add to number: 7778', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'8028b61b-bed1-408f-afe6-9b1062268e7b', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'81bdcca5-0989-4bd9-8724-77910b11be0c', N'string', N'Add to number: 1741', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'85099692-a1f7-457d-b112-5020768b6d27', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'858ec213-5cae-45c8-acf5-852be2a977aa', N'string', N'Add to number: 6566', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'86a6d368-9243-4d24-bfba-440bcfb492c6', N'string', N'Add to number: 1832', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'897397e2-7239-48ed-8c9c-ffc035fe628a', N'string', N'Add to number: 5512', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'8e7b8a9d-1699-4301-820d-539c2ba20d73', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'8f61c50f-6cd4-4610-ac9f-0fd4d937d4c5', N'string', N'Add to number: 4473', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'93de1f43-7dc3-4345-9dee-a92e105ee64f', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'9bddf375-29a6-42b3-8fd6-c76277dfc1e6', N'string', N'Add to number: 9487', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'9f46b45e-7cc3-49a7-b8fa-a734e69141ef', N'string', N'Add to number: 682', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'a1269b0d-6bc5-4aa1-a79c-5f78635f64b1', N'NCC06', N'NCC06', N'Hà Nội', N'0388177774', N'g@gmail.com', N'Hà Nội', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'a28276d8-ceea-4514-9e87-c79659fca7a4', N'string', N'Add to number: 2461', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'aa535055-b8c6-4a3b-8b0e-d72b68b2886b', N'string', N'Add to number: 1790', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'b43a6453-e908-4917-9a76-80d4a90bbdc9', N'string', N'Add to number: 4690', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'b653d0ad-0f5f-4182-a5fe-d81964b2f47a', N'string', N'Add to number: 2751', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'b658baca-3caf-4609-9fb9-c52fe24d7aff', N'tt', N'ttt', N'ttt', N'342699952', N'dfg@gmail.com', N'rter', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'bb232b2e-080f-46e0-9d75-beb2dcbade8d', N'string', N'Add to number: 7231', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'bd9edd83-5bd5-4d72-a5f6-d5a97b392e55', N'string', N'Add to number: 572', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'c755c0c7-5ad2-4c52-b34c-4cfbda19ea19', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'c8a8c225-2ae5-4a38-a7a4-043cafda1b7e', N'NCC04', N'NCC04', N'string', N'0388177774', N'string@gmail.com', N'string', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'd37fdaed-b339-4a55-a3bd-fe6882f099a0', N'1111111', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'e0368bd4-ad78-4e45-925d-6cfc4ba4a1c2', N'string', N'Add to number: 7218', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'e394a475-73d9-42f3-b70f-6f8045445f03', N'string', N'Add to number: 8934', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'e770db7b-7478-4f33-a39a-840dd8b476ea', N'string', N'Add to number: 6836', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'eb4d832b-fbcf-4634-88f4-4f4438f80299', N'1111111', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'ee574f85-e571-4e73-8e17-12b2c4965326', N'SAM19', N'SAM Holdings', N'Hà Nội', N'0123456789', N'contact@samholdings.com.vn', N'Nguyễn Văn S', 1, 0)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'ef89eb00-a5f9-4079-ba2a-58cc572ef300', N'string', N'Add to number: 2735', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'efa342f4-bcdf-4b56-9e0b-f0355d2f9c31', N'string', N'Add to number: 6251', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'f110ee11-87d0-4b69-b75f-31fea237c989', N'string', N'Add to number: 7795', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'f6c6f6d5-d53d-432b-9ee9-0f0f79ec5e6c', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'f77ce8f6-71ee-43ec-9ad7-964c4071116d', N'TEST02', N'TEST02', N'Hà N?i', N'0123456789', N'abc@gmail.com', N'Nguy?n Van C', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'f9645f10-0b16-4672-a341-f93a5e4e1262', N'string', N'Add to number: 2332', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'faad41c5-3e7f-46c6-b712-f0199fb09ae6', N'string', N'Add to number: 3536', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'fe238b00-2d89-4588-aadc-ed6dd1ef1f4a', N'string', N'Add to number: 9022', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'string', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'string12345', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[Vendor] ([Id], [Code], [Name], [Address], [Phone], [Email], [ContactPerson], [Inactive], [OnDelete]) VALUES (N'test001', N'TEST01', N'TEST01', N'Hà N?i', N'0999999999', N'abc@gmail.com', N'Nguy?n Van A', 1, 1)
GO
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'04c99b8b-47bd-44d7-9612-1a9428876178', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'3213372b-591e-4d36-aa33-331d7cbd993e', N'HTCITC7', N'dsadsadsa', N'	HTCITC7', N'	HTCITC7', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'32578553-9b8c-438a-86e7-d73ece11817c', N'HTCITC5', N'HTCITC5', N'dsadsa', N'dsadsa', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'3571373c-31ae-46c3-8ee7-5be80f78e9b4', N'HTCITC71', N'string', N'string', N'string', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'382253ba-776a-45c2-807f-ea07e7d77766', N'HTCITC7dsadsa', N'HTCITC7  ewrewrew', N'HTCITC7   ', N'	HTCITC7543543  ', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'3d4e0634-5f64-4e1a-8644-3dc2fd245415', N'WH2022714143428669', N'Kho Đà Nẵng', N'Đà Nẵng', NULL, NULL, NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd', N'HTCITC3', N'Kho Support', N'52 Trung Liệt - Ðống Ða - HN', N'Việt Nam', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'52bb3cad-6855-44a8-b522-1d079cea7649', N'HTCITC2', N'Kho Ðông Tác', N'Ðông Tác - HN ( di thuê kho)', NULL, N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'53bd3cad-9dff-4f26-8527-d5789d325d1a', N'HTCITC5', N'Kho Văn phòng', N'Khu công nghệ cao Hoà Lạc', NULL, N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'HTCITC9', N'Kho thu hồi', N'Khu công nghệ cao Hoà Lạc', NULL, N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'686689ff-edad-469a-8111-cf9c4cecee73', N'HTCITC5', N'HTCITC5', NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'68c666ae-dd12-42e9-b985-ac854d35a0d9', N'KHO001', N'Kho Test', N'Hà Nội', N'12345', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'6f95a3ad-6506-4a43-b809-8bbb11b2dea5', N'HTCITC6', N'Kho Data centrer', N'Khu công nghệ cao Hoà Lạc', N'11111', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'6f95a3ad-6506-4a43-b809-8bbb11b2dea5', 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'7bed9b1b-fc56-41c6-a222-7aa96e01174b', N'HTCITC88', N'HTCITC88', N'HTCITC88', N'HTCITC88', N'c13f83c1-71c3-4121-b5dc-5fae58175d99', NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'7c6cf847-d9ca-4c95-9331-d6da78dfae65', N'string111111111', N'string', N'string', N'string', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'HTCITC1', N'Kho Hoà Lạc', N'Khu công nghệ cao Hoà Lạc', NULL, NULL, N'84fd821c-2984-470b-8c1b-5123f7ddbd10', 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'872ea678-8b3e-41a7-876a-89279387f8aa', N'HTCITC7', N'Kho Ðà Nẵng', N'121 Ðặng Huy Trứ - Liên Chiểu - ÐN', N'Việt Nam', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'92f08c06-62a3-429a-a5bb-5023428d6911', N'ueqweqw', N'ưeqweqw', NULL, NULL, N'382253ba-776a-45c2-807f-ea07e7d77766', NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'9544687c-8901-40a1-bb5e-33f75e958474', N'string', N'string', N'string', N'string', N'string', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'95c1cbae-c1a7-485d-b810-7875d2d02ead', N'HTCITC5', N'dsadsadsa', N'dsadsa', N'dsadsadsa', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'HTCITC10', N'Kho thiết bị hỏng', N'Khu công nghệ cao Hoà Lạc', NULL, N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'a3da7204-ec66-4f62-8c8a-72cda7740044', 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'a5251a2a-c8e1-4b45-90d3-c91d4102fe27', N'string', N'string', N'string', N'string', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'acf49099-ee31-4633-b905-e148a6071bef', N'KHO01', N'Kho Hà Nội', N'Kho Hà Nội', N'Kho Hà Nội', N'52bb3cad-6855-44a8-b522-1d079cea7649', NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'c13f83c1-71c3-4121-b5dc-5fae58175d99', N'HTCITC8', N'HTCITC8', N'HTCITC8', N'HTCITC8', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', N'HTCITC4', N'Kho Vùng 1', N'Sơn Tây - HN', NULL, N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'd1c22d2a-9863-4df6-ad47-32462349be09', N'TL.ST', N'Kho Trung Liệt', N'238 Trung Liệt, Đống Đa, Hà Nội', NULL, N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'd1c22d2a-9863-4df6-ad47-32462349be09', 0, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'd35f9d57-ba00-4aeb-9825-9a2f2d441b11', N'1111string111111111', N'string', N'string', N'string', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'e1ea6691-3658-4cd3-9b97-6641bfe88e32', N'string111111111', N'string', N'string', N'string', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'eac17e9f-7d93-427e-9f15-9cd0626ef440', N'HTCITC71111111111111111111111111', N'string', N'string', N'string', NULL, N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'HTCITC8', N'Kho Hồ Chí Minh', N'385 Nguyễn Trãi -Quận 1- HCM', N'Việt Nam', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'string11', N'HTCITC71', N'string', N'string', N'string', NULL, N'string', 1, 1)
INSERT [dbo].[WareHouse] ([Id], [Code], [Name], [Address], [Description], [ParentId], [Path], [Inactive], [OnDelete]) VALUES (N'string1111', N'HTCITC719', N'Hà Nội', N'Hà Nội', N'Hà Nội', NULL, NULL, 1, 1)
GO
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'12323sdsd-vcdffd', N'0fcdbed4-e185-42de-b394-e93d6e22f0dd', N'53bd3cad-9dff-4f26-8527-d5789d325d1a', CAST(30.00 AS Decimal(15, 2)), CAST(2320.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'2332cwe23-dssdwe-322323ds', N'bf61bc30-bbe2-453a-92af-c78cf56b042e', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', CAST(5550.00 AS Decimal(15, 2)), CAST(55555.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'3123dsadsdas-321dsadasdsa', N'05ce3050-d239-4cf5-8431-351484b88eac', N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd', CAST(1.00 AS Decimal(15, 2)), CAST(1230.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'323-ddsa-423dsd-dasda', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', CAST(1997.00 AS Decimal(15, 2)), CAST(1997.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'323dssd-4343-4343', N'fe99dfe6-72e3-4f5c-a7df-fab72db26bda', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', CAST(5.00 AS Decimal(15, 2)), CAST(5.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'43c34-cve-cvdf', N'9dfd4fd8-319b-40ea-bb57-a197e96afeb8', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', CAST(66.00 AS Decimal(15, 2)), CAST(44699.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'cxcsdsd-dssdsd21', N'd9ddfb77-5e17-4b42-a2c8-4f49ad8f9106', N'd1c22d2a-9863-4df6-ad47-32462349be09', CAST(77.00 AS Decimal(15, 2)), CAST(7777.00 AS Decimal(15, 2)), 0)
INSERT [dbo].[WarehouseBalance] ([Id], [ItemId], [WarehouseId], [Quantity], [Amount], [OnDelete]) VALUES (N'sd23-vcds-sdsd', N'ae9c3653-4b10-480a-8298-675d176cbbf8', N'6f95a3ad-6506-4a43-b809-8bbb11b2dea5', CAST(66.00 AS Decimal(15, 2)), CAST(6666.00 AS Decimal(15, 2)), 0)
GO
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'05ce3050-d239-4cf5-8431-351484b88eac', N'CQ011', N'Cáp treo ADSS 96FO', N'140d633a-7b1f-4716-8a45-75fc3cca7380', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'0fcdbed4-e185-42de-b394-e93d6e22f0dd', N'CQ013', N'Cáp treo F8 8FO', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'15f148b2-f324-4376-8e40-a07315544dd5', N'VT001', N'Cáp quang ngầm F01', N'49bcb6b8-d5d0-4aa8-ad27-4adb8c3d47f8', N'Việt Nam', N'39286ee6-95af-4fdd-9302-b7db16051a72', NULL, N'Việt Nam', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'2e2cfbc2-286d-4c68-bbfb-8ec1a3a62f70', N'CQ015', N'Cáp treo F8 24FO', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'2f15e623-dbd9-4beb-bf69-81798e3b9372', N'PKNÐ021', N'Máy phát điện', NULL, NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'33d5abae-5b6a-4d8b-a282-c07b09a5507f', N'CQ014 TH', N'Cáp treo F8 12FO TH', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'CAP002', N'Cáp Quang Ngầm 2F0', N'474a4194-9813-4cc8-b596-497644f8f897', NULL, N'12a52803-613f-4055-9c73-df7bb6c4179c', NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'3b2ec5b2-0a5f-472e-b9d5-0e5d2721f639', N'DC001', N'Kìm c?t', N'ad2a2b09-c3da-459a-8251-51c3fd27b801', NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'3e480fbd-2502-47d2-9e4e-d622e1500b45', N'CQ007', N'Cáp quang ngầm 96FO', N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', NULL, NULL, NULL, N'Việt Nam', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'4d7bc4d7-f0ab-43cc-9005-b0a42ec8287e', N'CQ015 TH', N'Cáp treo F8 24FO TH', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'50c306bc-3365-43cd-aca6-dc52c19f27b2', N'CQ014', N'Cáp treo F8 12FO', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'5a28c168-98fd-4ddb-a3b2-ac16a41ea647', N'CQ006', N'Cáp quang ngầm 72FO', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'AITEM202271414375823', N'Cáp quang test data', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'67ae201c-f62d-42e5-92b6-3973b8f708ed', N'TBVP170', N'Máy chi?u Infocus ', N'92a34a2b-2bb3-460d-8d59-4efbc0f758a3', NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'7439fc7f-acf8-48d8-96d6-6e7a1227bd89', N'CQ012', N'Cáp treo F8 2FO', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'77f59f7a-4bbc-4335-aef4-82ecbba946c1', N'CQ013TH', N'Cáp treo F8 8FO TH', N'2bef50f1-f2a4-4dc4-acee-472705974bf3', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'7a0eb9db-26c8-4a1c-83da-7e8c6a06d37c', N'1111111111', N'1111111111', N'474a4194-9813-4cc8-b596-497644f8f897', NULL, N'12a52803-613f-4055-9c73-df7bb6c4179c', NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'810d7d09-2dd6-4272-95e0-81776b4339d6', N'CQ004 TH', N'Cáp quang ngầm 24FO TH', N'eebf3dcb-02f4-44fd-b3c5-062c108f6f24', NULL, N'3c0ffdaf-23b6-4fcc-8a7d-31b5db96feb1', NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'CQ005', N'Cáp quang ngầm 48FO', N'26129862-8bb0-4b21-b855-8bc9d09f3601', N'Việt Nam', N'12a52803-613f-4055-9c73-df7bb6c4179c', NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'963eceab-8325-4223-bd20-c03a4475e3a8', N'CQ010', N'Cáp treo ADSS 48FO', N'140d633a-7b1f-4716-8a45-75fc3cca7380', NULL, NULL, NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'CQ003', N'Cáp quang ngầm 12FO', N'474a4194-9813-4cc8-b596-497644f8f897', NULL, N'12a52803-613f-4055-9c73-df7bb6c4179c', NULL, N'Việt Nam', N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'9dfd4fd8-319b-40ea-bb57-a197e96afeb8', N'PKCQ001', N'Dây dai cắt điệnn', N'49bcb6b8-d5d0-4aa8-ad27-4adb8c3d47f8', NULL, N'39286ee6-95af-4fdd-9302-b7db16051a72', NULL, NULL, N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'9e06faf8-293f-462a-a4f3-ba40298c28e9', N'tes01', N'tes01', N'474a4194-9813-4cc8-b596-497644f8f897', N'tes01', N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', NULL, N'tes01', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'a54b9ad5-3b15-4383-a79b-36353ddbfbb6', N'GPS001', N'Máy d?nh v? GPS Etrex', NULL, NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'ae973639-135a-48d7-8f04-0e5bfa660d1a', N'000056', N'trung', N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', N'trung', N'68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250', NULL, N'trung', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'ae9c3653-4b10-480a-8298-675d176cbbf8', N'DC009', N'Kìm bấm mạng', N'ad2a2b09-c3da-459a-8251-51c3fd27b801', NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'bca52437-30f6-4d83-ad7d-2d4eb549a5c1', N'CQ010.1', N'Cáp treo ADSS 48FO KV 300', N'140d633a-7b1f-4716-8a45-75fc3cca7380', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'bf61bc30-bbe2-453a-92af-c78cf56b042e', N'PKNÐ020', N'Dây điện 2x6', N'd699067f-99af-452a-93b1-fb468ef7d13e', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'c9808b87-ec4e-401d-88cb-1ee070dfcccd', N'ITEM2022714143324350', N'Cáp quang test data', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, N'12a52803-613f-4055-9c73-df7bb6c4179c', NULL, N'Việt Nam', N'c6e878a8-0665-4783-ac50-235ffd7a7857', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'd9ddfb77-5e17-4b42-a2c8-4f49ad8f9106', N'CQ005 TH', N'Cáp quang ngầm 48FO TH', N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', NULL, NULL, NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'e589d904-54da-4fed-af3a-5b8c2293ec1d', N'CQ010TH', N'Cáp treo ADSS 48FO TH', N'140d633a-7b1f-4716-8a45-75fc3cca7380', NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'e8a2422e-b87c-454c-9e13-5c23c96d082f', N'DC003', N'Kìm di?n', N'ad2a2b09-c3da-459a-8251-51c3fd27b801', NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'string', N'string', NULL, N'string', NULL, NULL, N'string', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'ed209ffa-a220-4ded-a29b-8badf2aba7a6', N'dsadsadsa', N'dsadsadsa', NULL, NULL, NULL, NULL, NULL, N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'eda15932-8f25-47d1-b568-0641d078e06d', N'CODE001', N'Cáp Quang trên biển 001', N'49bcb6b8-d5d0-4aa8-ad27-4adb8c3d47f8', N'Việt Nam', N'55708ae7-0c75-4620-8ad8-ea0fee8632d9', NULL, N'Việt Nam', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'eeacaf7c-3106-4927-a312-746a8f087787', N'CQ004', N'Cáp quang ngầm 24FO', N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', NULL, N'439dc2d9-3b9c-4894-abc4-d3ff0db7e264', NULL, N'Việt Nam', N'f485376d-44de-48e7-91f3-78d2b2a63a93', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'CQ002', N'Cáp quang ngầm 8FO', N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', NULL, NULL, NULL, N'Việt Nam', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 0)
INSERT [dbo].[WareHouseItem] ([Id], [Code], [Name], [CategoryID], [Description], [VendorID], [VendorName], [Country], [UnitId], [Inactive], [OnDelete]) VALUES (N'fe99dfe6-72e3-4f5c-a7df-fab72db26bda', N'PKCQ001.1', N'Khóa dai', N'72c521c4-d8be-4a6e-92da-d837e9d76f70', NULL, NULL, NULL, NULL, N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', 1, 0)
GO
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'05ed73d2-008c-42d8-a0e2-ed2d86ccc59d', N'00013', N'Gía', N'fd386a17-b37a-48d4-860a-ac710a0ce5b6', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'0f0ff0cf-d6d5-4aa8-8cba-022d097dd604', N'00019', N'Ph? ki?n thi?t b? khách hàng', N'5be24ad6-d0b8-4b74-8284-e6f86a318919', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'140d633a-7b1f-4716-8a45-75fc3cca7380', N'00002', N'Cáp treo ADSS', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'1890c1df-3823-49d7-b636-540c1e5b2fe5', N'00018', N'?ng co nhi?t', N'2faecdb2-be81-4872-aecc-4a54e773d9e9', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'26129862-8bb0-4b21-b855-8bc9d09f3601', N'00024', N'Cáp quang', NULL, N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'29af4fab-8b6d-47cc-aa25-30eedbb36209', N'00022', N'Dây Multicore 4 sợi', N'5be24ad6-d0b8-4b74-8284-e6f86a318919', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'2bef50f1-f2a4-4dc4-acee-472705974bf3', N'00003', N'Cáp treo ', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'2d956d52-5086-4245-8012-cbda4840f8a1', N'aaa1212', N'aaaaaaa', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'2faecdb2-be81-4872-aecc-4a54e773d9e9', N'00027', N'Ph? ki?n cáp quang', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'34370c46-00a0-4a29-af10-6794c7074bb4', N'aaaaa', N'aaaaaaaaaaaa', NULL, NULL, N'dsadsa', 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'350ec3ac-1674-4d24-8bf7-b4708d494815', N'00008', N'Kìm c?t', N'72c521c4-d8be-4a6e-92da-d837e9d76f70', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'399d196c-a44c-44c6-8c77-618c14b71c1e', N'00010', N'B?ng d?ng ', N'fd386a17-b37a-48d4-860a-ac710a0ce5b6', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'3de0cd7f-c05d-4848-911f-2c6520349027', N'000159', N'00015 ', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, N'00015', 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'474a4194-9813-4cc8-b596-497644f8f897', N'00005', N'Bình chữa cháy', N'26129862-8bb0-4b21-b855-8bc9d09f3601', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'49bcb6b8-d5d0-4aa8-ad27-4adb8c3d47f8', N'00007', N'Bộ tua vit', NULL, N'49bcb6b8-d5d0-4aa8-ad27-4adb8c3d47f8', NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'54829797-9bb9-463c-8d32-3d1cc8aca49d', N'00006', N'Máy khoan', N'72c521c4-d8be-4a6e-92da-d837e9d76f70', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'5be24ad6-d0b8-4b74-8284-e6f86a318919', N'00028', N'Ph? ki?n thi?t b? m?ng', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'61fb5b17-848a-486e-8868-55d7899c4d81', N'00016', N'k?p', N'2faecdb2-be81-4872-aecc-4a54e773d9e9', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'67930c3f-3b23-40f7-a956-ea210cae2a86', N'dsadsasa', N'aaaaaaaaaa3312321', N'7c1f21b5-4760-4ad7-aaa3-32dcbedeeeb9', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'6c26e07b-694c-4875-a95e-59465e9edf46', N'00012', N'T? S?t', N'fd386a17-b37a-48d4-860a-ac710a0ce5b6', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'70271a72-6399-40f3-9b54-fb609c28f05f', N'00018', N'Kìm nhiệt', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'70953984-1a34-4d3b-a420-ef7ed6fd4b24', N'00020', N'Dây nhảy quang dúp', N'5be24ad6-d0b8-4b74-8284-e6f86a318919', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'72c521c4-d8be-4a6e-92da-d837e9d76f70', N'00025', N'Công c? d?ng c?', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'7c1f21b5-4760-4ad7-aaa3-32dcbedeeeb9', N'00017', N'Dây hàn', N'2faecdb2-be81-4872-aecc-4a54e773d9e9', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'92a34a2b-2bb3-460d-8d59-4efbc0f758a3', N'00015', N'Adapter', N'474a4194-9813-4cc8-b596-497644f8f897', NULL, N'Vi?t Nam', 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'ad2a2b09-c3da-459a-8251-51c3fd27b801', N'00004', N'Dụng cụ', NULL, NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'c34617a3-febe-4c5d-8b11-f27368ea7a11', N'00023', N'Dây nhảy 8FO', N'5be24ad6-d0b8-4b74-8284-e6f86a318919', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', N'00001', N'Cáp quang ngầm', NULL, N'd50d553b-d22d-4e2b-916d-5a7e6655a1fc', NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'd699067f-99af-452a-93b1-fb468ef7d13e', N'00014', N'Dây điện', NULL, NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'd8cec2d4-ad51-43cc-aa5a-d7fb92f98020', N'00009', N'S? cách di?n', N'fd386a17-b37a-48d4-860a-ac710a0ce5b6', NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'eebf3dcb-02f4-44fd-b3c5-062c108f6f24', N'00011', N'Cầu dao', NULL, NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'f137299a-4f11-4c41-ada0-0ebf650be5a8', N'ttt', N'ttt', N'26129862-8bb0-4b21-b855-8bc9d09f3601', N'26129862-8bb0-4b21-b855-8bc9d09f3601/f137299a-4f11-4c41-ada0-0ebf650be5a8', NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'f3709813-89b7-411e-a3e3-5c6247f6247c', N'00021', N'Dây nhảy quang', N'5be24ad6-d0b8-4b74-8284-e6f86a318919', NULL, NULL, 1, 0)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'fd386a17-b37a-48d4-860a-ac710a0ce5b6', N'00026', N'Nhà tr?m', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[WareHouseItemCategory] ([Id], [Code], [Name], [ParentId], [Path], [Description], [Inactive], [OnDelete]) VALUES (N'string', N'string', N'string', N'string', N'string/string', N'string', 0, 1)
GO
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'08a1c0e4-83f1-4566-b83b-61b80bcb267a', N'5a28c168-98fd-4ddb-a3b2-ac16a41ea647', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'09d0c131-60fb-4ccb-a17b-a786f0c7dbd2', N'eda15932-8f25-47d1-b568-0641d078e06d', N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'0B2F566D-0963-4A62-B1CC-8EE115C8F579', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'0fd02d63-6abc-497d-88f0-83e2e8487a74', N'15f148b2-f324-4376-8e40-a07315544dd5', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'150a5788-3324-4165-b4cb-50df662193f3', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'1cfb38f1-75eb-4439-8586-3bb5d5386ed4', N'3e480fbd-2502-47d2-9e4e-d622e1500b45', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'3b3db5c2-dbac-4a81-86e5-f11c3e4c3f26', N'15f148b2-f324-4376-8e40-a07315544dd5', N'f637edb6-c2cd-4f38-977f-0f497bea9d97', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'41b2ef12-2866-4c4c-8e34-4cb0605d347b', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'45883ced-0882-494e-acb3-a31f0bda1a5d', N'15f148b2-f324-4376-8e40-a07315544dd5', N'8314fff5-111d-4512-a0db-c0a454826a8c', 99, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'492fd220-aeed-42a7-956d-ad11c324f3d0', N'eda15932-8f25-47d1-b568-0641d078e06d', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'530fa58e-1e3e-4ed9-8151-487b156e12d2', N'eda15932-8f25-47d1-b568-0641d078e06d', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'539f217e-e690-40af-87a8-ba99016dbe01', N'15f148b2-f324-4376-8e40-a07315544dd5', N'c6e878a8-0665-4783-ac50-235ffd7a7857', 55, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'57df3323-f70c-4496-b353-24f1b89bd081', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 33, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'6c66cb53-f7be-45b4-95de-2921340ea028', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'770356ad-bc00-487b-83d2-a35e337afd13', N'15f148b2-f324-4376-8e40-a07315544dd5', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'79363571-BC3E-4F41-83E3-5915AD8EE731', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'7B80B897-F634-403C-BABF-4B9631362E72', N'15f148b2-f324-4376-8e40-a07315544dd5', N'f637edb6-c2cd-4f38-977f-0f497bea9d97', 2, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'7d65e5d1-b2d4-40e8-a09d-ffe11fc4f769', N'5a28c168-98fd-4ddb-a3b2-ac16a41ea647', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'8314fff5-111d-4512-a0db-c0a454826a8c', N'eda15932-8f25-47d1-b568-0641d078e06d', N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'84c758b6-bba4-45e0-934b-3349c9610378', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'f485376d-44de-48e7-91f3-78d2b2a63a93', 100, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'8bf75ee3-9ad3-4f52-8b18-7e14b110b959', N'5a28c168-98fd-4ddb-a3b2-ac16a41ea647', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'9ef414f1-1579-4f80-9132-3e7c5ed765a9', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'a8a92227-c0f2-432f-a7d5-b6ed35709856', N'810d7d09-2dd6-4272-95e0-81776b4339d6', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'b091f37f-4888-4d08-8b3e-62fb59dbf17c', N'5b30d14c-3066-462f-b286-e30a3a42fc29', N'c6e878a8-0665-4783-ac50-235ffd7a7857', 1000, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'B1813E55-0F39-4FC5-9EA7-1911A04668B2', N'e96f1e98-8860-4c8c-a0ae-40714817a6cc', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'B4A7AD8F-0B81-45C3-BB45-1622BC628D3A', N'15f148b2-f324-4376-8e40-a07315544dd5', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 3, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'baea9f0d-c7ea-457e-8fb3-ef0ed60f294e', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'bd51b68a-9ebf-4afe-89a3-af734e00b857', N'3798d47f-7f6b-4201-ba85-a285b44c9b66', N'422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'be804346-0336-4d8b-b9e9-e48c24b70697', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'BF056179-BA79-4060-9C6E-414EF994314D', N'c9808b87-ec4e-401d-88cb-1ee070dfcccd', N'c6e878a8-0665-4783-ac50-235ffd7a7857', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'c7850a7b-63d8-4388-add5-e3a101335f4a', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'c885030e-4b07-465a-a4b9-865431ff4396', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'cfa9eee0-04c2-4c71-81ad-242c2412f355', N'15f148b2-f324-4376-8e40-a07315544dd5', N'8314fff5-111d-4512-a0db-c0a454826a8c', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'E6101583-C252-4846-834C-59EE7A44D4DB', N'7a0eb9db-26c8-4a1c-83da-7e8c6a06d37c', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'e6213f2e-752d-4c8b-b134-82963737eacf', N'15f148b2-f324-4376-8e40-a07315544dd5', N'f485376d-44de-48e7-91f3-78d2b2a63a93', 1, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'f637edb6-c2cd-4f38-977f-0f497bea9d97', N'15f148b2-f324-4376-8e40-a07315544dd5', N'f637edb6-c2cd-4f38-977f-0f497bea9d97', 2, 1, 1)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'f745a807-0816-4682-af7e-49f2c7f2495b', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 0)
INSERT [dbo].[WareHouseItemUnit] ([Id], [ItemId], [UnitId], [ConvertRate], [IsPrimary], [OnDelete]) VALUES (N'fd823057-d27b-432f-8d56-498b30c58829', N'15f148b2-f324-4376-8e40-a07315544dd5', N'492fd220-aeed-42a7-956d-ad11c324f3d0', 1, 1, 1)
GO
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'0cdb0a3c-b9f2-422c-afaf-489c6848acbe', N'fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', 99, 99, CAST(N'2021-10-13T01:50:20.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:50:20.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'125f35e3-1610-41b7-a526-2113215f6141', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 23, 24, CAST(N'2021-09-21T03:40:18.0000000' AS DateTime2), NULL, CAST(N'2021-09-30T06:48:30.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'223dc468-ea56-48ec-8acc-a139959874d4', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', NULL, 22, 30, CAST(N'2021-10-13T02:59:03.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T02:59:03.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'33f346f2-a57a-4cc1-878f-e2f5ce2bcbc1', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'eda15932-8f25-47d1-b568-0641d078e06d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, 0, 1000000000, CAST(N'2022-07-04T02:44:12.0000000' AS DateTime2), NULL, CAST(N'2022-07-04T02:44:12.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'3f0a5019-2492-43db-89b6-83b0ad8ed8e8', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'810d7d09-2dd6-4272-95e0-81776b4339d6', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 33, 55, CAST(N'2021-10-13T01:57:46.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T02:18:24.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'45f70580-af05-4734-a6a1-31d879c9aff0', N'52bb3cad-6855-44a8-b522-1d079cea7649', N'f940cbb9-6052-4a2b-9d33-630b95d9d60b', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', NULL, 100, 1000, CAST(N'2021-10-18T03:31:09.0000000' AS DateTime2), NULL, CAST(N'2021-10-18T03:31:09.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'462c8a5a-2a23-481b-a650-db67f7c6d94b', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', 2, 2, CAST(N'2021-10-13T01:49:37.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:49:37.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'4ec7b76b-c2cb-4c5b-872a-3f23d283cee7', N'53bd3cad-9dff-4f26-8527-d5789d325d1a', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 32, 33, CAST(N'2021-09-20T10:06:57.0000000' AS DateTime2), NULL, CAST(N'2021-09-30T06:48:30.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'668a536e-2e8e-4bc7-8e71-fcfe8953b9f7', N'61517a8c-2f08-43c3-b8d2-4f69d9f9e9ae', N'eeacaf7c-3106-4927-a312-746a8f087787', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', NULL, 20, 30, CAST(N'2021-10-13T10:12:58.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T10:12:58.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'68b631c3-5c48-4193-a34a-ef979e7ec5e3', N'41bb3ca9-bdec-4190-a3bf-fe40d006e6fd', N'eeacaf7c-3106-4927-a312-746a8f087787', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', 1, 1, CAST(N'2021-10-13T01:49:43.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:49:43.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'8607b7f6-1baa-4f34-8c27-d567806c3b1d', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'a54b9ad5-3b15-4383-a79b-36353ddbfbb6', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 22, 22, CAST(N'2021-09-20T10:06:57.0000000' AS DateTime2), NULL, CAST(N'2021-10-18T09:19:46.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'9aa7d8bc-b203-4337-bef7-fa562b5fed5e', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'2f15e623-dbd9-4beb-bf69-81798e3b9372', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 44, 55, CAST(N'2021-09-16T02:40:10.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T03:30:21.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'a57973b4-74ff-4448-bb2b-ac6a69de927d', N'52bb3cad-6855-44a8-b522-1d079cea7649', N'eeacaf7c-3106-4927-a312-746a8f087787', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 22, 33, CAST(N'2021-10-13T01:57:59.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:57:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'b4fa443d-cd8b-4874-b3f1-37fb900f8ec0', N'a3da7204-ec66-4f62-8c8a-72cda7740044', N'9c894acd-7975-414b-bc44-3006f6bcf3b7', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 33, 55, CAST(N'2021-10-13T01:57:59.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:57:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'bc256efe-fa5c-4d67-a636-c06c65a7caed', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'2e2cfbc2-286d-4c68-bbfb-8ec1a3a62f70', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', 1, 2, CAST(N'2021-09-21T03:54:26.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T02:18:24.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'e41bbf0a-fed0-4602-adf7-b9db477fd9d1', N'd0cb38bf-b405-46bb-b5a0-24c6c298404c', N'810d7d09-2dd6-4272-95e0-81776b4339d6', N'e71dce44-bdfd-4f17-ade3-aa5054c87be8', N'Chi?c', 9, 9, CAST(N'2021-10-13T01:50:20.0000000' AS DateTime2), NULL, CAST(N'2021-10-13T01:50:20.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[WareHouseLimit] ([Id], [WareHouseId], [ItemId], [UnitId], [UnitName], [MinQuantity], [MaxQuantity], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [OnDelete]) VALUES (N'fce5b0a9-be50-4bb3-b727-4bb9825c4318', N'84fd821c-2984-470b-8c1b-5123f7ddbd10', N'95dcbe8a-133f-4242-af7d-b87e15af396d', N'0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d', N'Mét', 32, 45, CAST(N'2021-09-21T03:51:31.0000000' AS DateTime2), NULL, CAST(N'2021-09-21T04:46:40.0000000' AS DateTime2), NULL, 0)
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT ('') FOR [VoucherCode]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (getdate()) FOR [VoucherDate]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (NULL) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Audit] ADD  DEFAULT (NULL) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Audit] ADD  CONSTRAINT [DF_Audit_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  DEFAULT ('') FOR [AuditId]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  DEFAULT (NULL) FOR [EmployeeId]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  DEFAULT (NULL) FOR [EmployeeName]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  DEFAULT (NULL) FOR [Role]
GO
ALTER TABLE [dbo].[AuditCouncil] ADD  CONSTRAINT [DF_AuditCouncil_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT ('') FOR [AuditId]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT (NULL) FOR [ItemId]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT ((0)) FOR [AuditQuantity]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  DEFAULT (NULL) FOR [Conclude]
GO
ALTER TABLE [dbo].[AuditDetail] ADD  CONSTRAINT [DF_AuditDetail_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[AuditDetailSerial] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[AuditDetailSerial] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[AuditDetailSerial] ADD  DEFAULT ('') FOR [Serial]
GO
ALTER TABLE [dbo].[AuditDetailSerial] ADD  DEFAULT (NULL) FOR [AuditDetailId]
GO
ALTER TABLE [dbo].[AuditDetailSerial] ADD  CONSTRAINT [DF_AuditDetailSerial_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT ('') FOR [WareHouseId]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT (NULL) FOR [UnitName]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT (NULL) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  DEFAULT (NULL) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[BeginningWareHouse] ADD  CONSTRAINT [DF_BeginningWareHouse_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [VoucherCode]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (getdate()) FOR [VoucherDate]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT ('') FOR [WareHouseID]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [Deliver]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [Receiver]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [VendorId]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [ReasonDescription]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [Reference]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Inward] ADD  DEFAULT (NULL) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Inward] ADD  CONSTRAINT [DF_Inward_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ('') FOR [InwardId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ((0)) FOR [UIQuantity]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ((0.00)) FOR [UIPrice]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ((0.00)) FOR [Amount]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT ((0.00)) FOR [Price]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [DepartmentId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [DepartmentName]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [EmployeeId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [EmployeeName]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [StationId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [StationName]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [ProjectId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [ProjectName]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [CustomerId]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  DEFAULT (NULL) FOR [CustomerName]
GO
ALTER TABLE [dbo].[InwardDetail] ADD  CONSTRAINT [DF_InwardDetail_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [VoucherCode]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (getdate()) FOR [VoucherDate]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT ('') FOR [WareHouseID]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [ToWareHouseId]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [Deliver]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [Receiver]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [ReasonDescription]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [Reference]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Outward] ADD  DEFAULT (NULL) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Outward] ADD  CONSTRAINT [DF_Outward_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ('') FOR [OutwardId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ((0)) FOR [UIQuantity]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ((0.00)) FOR [UIPrice]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ((0.00)) FOR [Amount]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT ((0.00)) FOR [Price]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [DepartmentId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [DepartmentName]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [EmployeeId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [EmployeeName]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [StationId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [StationName]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [ProjectId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [ProjectName]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [CustomerId]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  DEFAULT (NULL) FOR [CustomerName]
GO
ALTER TABLE [dbo].[OutwardDetail] ADD  CONSTRAINT [DF_OutwardDetail_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT ('') FOR [Serial]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT (NULL) FOR [InwardDetailId]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT (NULL) FOR [OutwardDetailId]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  DEFAULT ((0)) FOR [IsOver]
GO
ALTER TABLE [dbo].[SerialWareHouse] ADD  CONSTRAINT [DF_SerialWareHouse_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[Unit] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Unit] ADD  DEFAULT (NULL) FOR [UnitName]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ('') FOR [Code]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT (NULL) FOR [Phone]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT (NULL) FOR [Email]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_Vendor_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT ('') FOR [Code]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT (NULL) FOR [Address]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT (NULL) FOR [ParentId]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT (NULL) FOR [Path]
GO
ALTER TABLE [dbo].[WareHouse] ADD  DEFAULT ((0)) FOR [Inactive]
GO
ALTER TABLE [dbo].[WareHouse] ADD  CONSTRAINT [DF_WareHouse_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  DEFAULT ('') FOR [WarehouseId]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  DEFAULT ((0.00)) FOR [Amount]
GO
ALTER TABLE [dbo].[WarehouseBalance] ADD  CONSTRAINT [DF_WarehouseBalance_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT ('') FOR [Code]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT (NULL) FOR [CategoryID]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT (NULL) FOR [VendorID]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT (NULL) FOR [VendorName]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  DEFAULT ((1)) FOR [Inactive]
GO
ALTER TABLE [dbo].[WareHouseItem] ADD  CONSTRAINT [DF_WareHouseItem_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT ('') FOR [Code]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT (NULL) FOR [ParentId]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT (NULL) FOR [Path]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  DEFAULT ((1)) FOR [Inactive]
GO
ALTER TABLE [dbo].[WareHouseItemCategory] ADD  CONSTRAINT [DF_WareHouseItemCategory_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WareHouseItemUnit] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WareHouseItemUnit] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[WareHouseItemUnit] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[WareHouseItemUnit] ADD  DEFAULT ((1)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[WareHouseItemUnit] ADD  CONSTRAINT [DF_WareHouseItemUnit_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ('') FOR [WareHouseId]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ('') FOR [ItemId]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ('') FOR [UnitId]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT (NULL) FOR [UnitName]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ((0)) FOR [MinQuantity]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT ((0)) FOR [MaxQuantity]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT (NULL) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  DEFAULT (NULL) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[WareHouseLimit] ADD  CONSTRAINT [DF_WareHouseLimit_OnDelete]  DEFAULT ((0)) FOR [OnDelete]
GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_Audits_PK_WareHouse] FOREIGN KEY([WareHouseId])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_Audits_PK_WareHouse]
GO
ALTER TABLE [dbo].[AuditCouncil]  WITH CHECK ADD  CONSTRAINT [FK_AuditCouncils_PK_Audit] FOREIGN KEY([AuditId])
REFERENCES [dbo].[Audit] ([Id])
GO
ALTER TABLE [dbo].[AuditCouncil] CHECK CONSTRAINT [FK_AuditCouncils_PK_Audit]
GO
ALTER TABLE [dbo].[AuditDetail]  WITH CHECK ADD  CONSTRAINT [FK_AuditDetails_PK_Audit] FOREIGN KEY([AuditId])
REFERENCES [dbo].[Audit] ([Id])
GO
ALTER TABLE [dbo].[AuditDetail] CHECK CONSTRAINT [FK_AuditDetails_PK_Audit]
GO
ALTER TABLE [dbo].[AuditDetail]  WITH CHECK ADD  CONSTRAINT [FK_AuditDetails_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[AuditDetail] CHECK CONSTRAINT [FK_AuditDetails_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[AuditDetailSerial]  WITH CHECK ADD  CONSTRAINT [FK_AuditDetailSerials_PK_AuditDetail] FOREIGN KEY([AuditDetailId])
REFERENCES [dbo].[AuditDetail] ([Id])
GO
ALTER TABLE [dbo].[AuditDetailSerial] CHECK CONSTRAINT [FK_AuditDetailSerials_PK_AuditDetail]
GO
ALTER TABLE [dbo].[AuditDetailSerial]  WITH CHECK ADD  CONSTRAINT [FK_AuditDetailSerials_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[AuditDetailSerial] CHECK CONSTRAINT [FK_AuditDetailSerials_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[BeginningWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_BeginningWareHouses_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[BeginningWareHouse] CHECK CONSTRAINT [FK_BeginningWareHouses_PK_Unit]
GO
ALTER TABLE [dbo].[BeginningWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_BeginningWareHouses_PK_WareHouse] FOREIGN KEY([WareHouseId])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[BeginningWareHouse] CHECK CONSTRAINT [FK_BeginningWareHouses_PK_WareHouse]
GO
ALTER TABLE [dbo].[BeginningWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_BeginningWareHouses_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[BeginningWareHouse] CHECK CONSTRAINT [FK_BeginningWareHouses_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[Inward]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseInwards_PK_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([Id])
GO
ALTER TABLE [dbo].[Inward] CHECK CONSTRAINT [FK_WareHouseInwards_PK_Vendor]
GO
ALTER TABLE [dbo].[Inward]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseInwards_PK_WareHouse] FOREIGN KEY([WareHouseID])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[Inward] CHECK CONSTRAINT [FK_WareHouseInwards_PK_WareHouse]
GO
ALTER TABLE [dbo].[InwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_InwardDetails_PK_Inward] FOREIGN KEY([InwardId])
REFERENCES [dbo].[Inward] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InwardDetail] CHECK CONSTRAINT [FK_InwardDetails_PK_Inward]
GO
ALTER TABLE [dbo].[InwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_InwardDetails_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[InwardDetail] CHECK CONSTRAINT [FK_InwardDetails_PK_Unit]
GO
ALTER TABLE [dbo].[InwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_InwardDetails_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[InwardDetail] CHECK CONSTRAINT [FK_InwardDetails_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[Outward]  WITH CHECK ADD  CONSTRAINT [FK_ToWareHouse_Outwards_PK_ToWareHouse] FOREIGN KEY([ToWareHouseId])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[Outward] CHECK CONSTRAINT [FK_ToWareHouse_Outwards_PK_ToWareHouse]
GO
ALTER TABLE [dbo].[Outward]  WITH CHECK ADD  CONSTRAINT [FK_Warehouse_Outwards_PK_WareHouse] FOREIGN KEY([WareHouseID])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[Outward] CHECK CONSTRAINT [FK_Warehouse_Outwards_PK_WareHouse]
GO
ALTER TABLE [dbo].[OutwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_OutwardDetails_PK_Outward] FOREIGN KEY([OutwardId])
REFERENCES [dbo].[Outward] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OutwardDetail] CHECK CONSTRAINT [FK_OutwardDetails_PK_Outward]
GO
ALTER TABLE [dbo].[OutwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_OutwardDetails_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[OutwardDetail] CHECK CONSTRAINT [FK_OutwardDetails_PK_Unit]
GO
ALTER TABLE [dbo].[OutwardDetail]  WITH CHECK ADD  CONSTRAINT [FK_OutwardDetails_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[OutwardDetail] CHECK CONSTRAINT [FK_OutwardDetails_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[SerialWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_SerialWareHouses_PK_InwardDetail] FOREIGN KEY([InwardDetailId])
REFERENCES [dbo].[InwardDetail] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SerialWareHouse] CHECK CONSTRAINT [FK_SerialWareHouses_PK_InwardDetail]
GO
ALTER TABLE [dbo].[SerialWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_SerialWareHouses_PK_OutwardDetail] FOREIGN KEY([OutwardDetailId])
REFERENCES [dbo].[OutwardDetail] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SerialWareHouse] CHECK CONSTRAINT [FK_SerialWareHouses_PK_OutwardDetail]
GO
ALTER TABLE [dbo].[SerialWareHouse]  WITH CHECK ADD  CONSTRAINT [FK_SerialWareHouses_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[SerialWareHouse] CHECK CONSTRAINT [FK_SerialWareHouses_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[WareHouseItem]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItems_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[WareHouseItem] CHECK CONSTRAINT [FK_WareHouseItems_PK_Unit]
GO
ALTER TABLE [dbo].[WareHouseItem]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItems_PK_Vendor] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendor] ([Id])
GO
ALTER TABLE [dbo].[WareHouseItem] CHECK CONSTRAINT [FK_WareHouseItems_PK_Vendor]
GO
ALTER TABLE [dbo].[WareHouseItem]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItems_PK_WareHouseItemCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[WareHouseItemCategory] ([Id])
GO
ALTER TABLE [dbo].[WareHouseItem] CHECK CONSTRAINT [FK_WareHouseItems_PK_WareHouseItemCategory]
GO
ALTER TABLE [dbo].[WareHouseItemCategory]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItemCategories_PK_Parent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[WareHouseItemCategory] ([Id])
GO
ALTER TABLE [dbo].[WareHouseItemCategory] CHECK CONSTRAINT [FK_WareHouseItemCategories_PK_Parent]
GO
ALTER TABLE [dbo].[WareHouseItemUnit]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItemUnits_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[WareHouseItemUnit] CHECK CONSTRAINT [FK_WareHouseItemUnits_PK_Unit]
GO
ALTER TABLE [dbo].[WareHouseItemUnit]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseItemUnits_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WareHouseItemUnit] CHECK CONSTRAINT [FK_WareHouseItemUnits_PK_WareHouseItem]
GO
ALTER TABLE [dbo].[WareHouseLimit]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseLimits_PK_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[WareHouseLimit] CHECK CONSTRAINT [FK_WareHouseLimits_PK_Unit]
GO
ALTER TABLE [dbo].[WareHouseLimit]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseLimits_PK_WareHouse] FOREIGN KEY([WareHouseId])
REFERENCES [dbo].[WareHouse] ([Id])
GO
ALTER TABLE [dbo].[WareHouseLimit] CHECK CONSTRAINT [FK_WareHouseLimits_PK_WareHouse]
GO
ALTER TABLE [dbo].[WareHouseLimit]  WITH CHECK ADD  CONSTRAINT [FK_WareHouseLimits_PK_WareHouseItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WareHouseItem] ([Id])
GO
ALTER TABLE [dbo].[WareHouseLimit] CHECK CONSTRAINT [FK_WareHouseLimits_PK_WareHouseItem]
GO
/****** Object:  Trigger [dbo].[Triger_Wh_Ut]    Script Date: 7/18/2022 3:45:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[Triger_Wh_Ut] ON [dbo].[WareHouseItem] 
  AFTER INSERT
AS 
BEGIN
  SET NOCOUNT ON;

  -- get the last id value of the record inserted or updated
  DECLARE @id nvarchar(100)
  DECLARE @idunit nvarchar(100)
  DECLARE @iditem nvarchar(100)
  SELECT @id = Id
  FROM INSERTED
  SELECT @idunit = UnitId
  FROM INSERTED
  -- Insert statements for trigger here
  insert into WareHouseItemUnit values(NEWID(),@id,@idunit,1,1,0)

END
GO
ALTER TABLE [dbo].[WareHouseItem] ENABLE TRIGGER [Triger_Wh_Ut]
GO
USE [master]
GO
ALTER DATABASE [WarehouseManagement] SET  READ_WRITE 
GO



-- MasterData

USE [master]
GO
/****** Object:  Database [MasterData]    Script Date: 7/18/2022 3:42:42 PM ******/
CREATE DATABASE [MasterData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MasterData', FILENAME = N'/var/opt/mssql/data/MasterData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MasterData_log', FILENAME = N'/var/opt/mssql/data/MasterData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[FakeDataMaster]    Script Date: 7/18/2022 3:42:42 PM ******/
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
/****** Object:  Table [dbo].[HistoryNotication]    Script Date: 7/18/2022 3:42:42 PM ******/
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
/****** Object:  Table [dbo].[UserMaster]    Script Date: 7/18/2022 3:42:42 PM ******/
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
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'01104f7c-55b7-4e00-82a1-92108d346043', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-14T10:16:38.0212545' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'017c998a-8426-40c1-b7fc-57e197c8473b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:05.6809907' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0205c6f1-c4e3-473f-b755-ababb96eea49', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:12.4392086' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'02a71a01-849c-4d18-89ad-b641dcbcb77e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:55.8206291' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'02dcdfb9-cb62-4cf9-a282-952d64f4f26d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:22:28.9102769' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'02f96435-2233-455c-8ba3-69b4f887bcde', N'admin@gmail.com', N'Tạo', N'vừa tạo phiếu nhập kho có mã rwerwer!', CAST(N'2022-04-21T11:35:10.9497512' AS DateTime2), 0, N'96b96ade-db85-481d-aabb-c47877a583d9', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0329830f-06f9-411d-a346-ba30e4368ee7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:33:04.9087169' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'044cca98-5e6d-4c88-98ec-658c8bb16adb', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T14:06:14.8026858' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'04aa392a-d890-4775-b598-5d46b3196cd6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022714143451255!', CAST(N'2022-07-15T10:59:54.6067558' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'04cde982-debb-4ad8-96a4-6fb38b04afc2', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu xuất kho có mã PX2022715101751962!', CAST(N'2022-07-15T10:18:20.3338972' AS DateTime2), 0, N'fc4d73e0-675e-4e8f-bde9-636b97fd32aa', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'04d49089-fc9d-4fce-b55b-97c79a4d9ae8', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã jhg7657mbn!', CAST(N'2022-05-20T14:02:47.7883751' AS DateTime2), 0, N'867b6c92-5454-4354-aa1b-80078cfc1449', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'05386e63-dffc-4181-86fb-b02ace2f1ce6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-07-15T11:06:11.5710337' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'06ed41f1-45a5-4fca-b622-f0fbf3aa0b57', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:50:01.8891641' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'07f4462b-2072-44de-942e-0e297008b3c6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:12:04.7960087' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'08875323-c655-4eb9-ac7a-8a3d0ec25294', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:26:05.0628207' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'08e357e0-e175-4594-8408-f6ca1ea483c1', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T13:37:43.3664903' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0920b1e0-cf48-416b-ac55-485f81e44418', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã 5454cvxc!', CAST(N'2022-05-20T13:59:25.8109069' AS DateTime2), 0, N'f99d38ff-2de8-48e6-99c0-d12ca8494515', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'092e0ee5-47bd-4ac8-a8fc-126b8bfb0a4b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:47:00.6358641' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'093d0e37-c7d8-4f8b-b21a-0b4c9ddf3e4b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T08:56:29.7530876' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0979d4aa-582e-407a-920a-078ccf42902e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:33:10.6925708' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0ab92d73-4873-4b69-9a71-9bf3567e879c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:34:44.2671688' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0b3b6f09-5f2e-4a5d-8643-b14c682fdc2d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:10.2248450' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0b499c99-214b-4f5b-b59b-e85f17d6496e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:25.8229541' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0bed5ddf-16e5-4f93-94ce-b01a1dea076f', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T10:37:16.3923726' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0d8231f8-57f5-4c75-9a77-0677d406f79d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã test9999999!', CAST(N'2022-07-13T08:48:29.3074723' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0e026204-811f-4bf8-911d-e100025cbbc0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:16:22.2162283' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0efe635f-8068-4646-9eab-01fa828391d4', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:15.4634273' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0f04bec6-f244-44e1-b549-fd3326307dfe', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fsd43534534gfdgdf!', CAST(N'2022-04-22T14:16:52.7041823' AS DateTime2), 0, N'81f37790-173b-48d6-9edf-86514fbdb1d2', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'0f0b4e08-24f2-45e3-944c-42d1f67f3635', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:38:06.4238874' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'109e1288-c31f-4464-8a75-e92f48cdc93c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:35:05.1763285' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'10c8f21c-bc2a-4973-8f8b-931028815957', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:48.4344712' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'12b72d48-28c9-4e33-af7d-0d9b307d443a', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-15T02:30:18.7531832' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'13b2492b-9dd6-4b03-8ce0-25365229295d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T11:51:15.2239694' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'14abaa6b-824a-4833-aea3-ffc88dcd9194', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T09:03:24.9294308' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'15d7f54e-8e19-4be7-ac43-2e42db2d939b', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:39:33.5899674' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'18371c4b-0f01-4286-8b43-d4458cb5aa43', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T10:51:13.2407552' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'197cbed2-56cf-4d58-9793-cdc0d2da95a2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:17:47.6253598' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'19e5456a-c4ff-4f8c-a264-f9f1e6764b09', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:51.1155176' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1c0e44b9-fcb5-4d15-bac0-4453cc4db0b9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:54:17.9481438' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1c1e2af9-e628-42ed-a356-fb4c8295285c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:38:29.3356836' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1d02ba0e-52f9-4067-b5a7-d078d121c3e7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:08:51.4563267' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1e38e61a-f5c5-48ee-a9bd-4f0c53198ccd', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:22:25.5330815' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1e89095d-fc85-494f-903b-b2d98d6c815d', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:46:15.8382678' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'1fcbb22e-43c8-445b-9e18-68ef69acbec2', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:39:33.2102281' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'20320586-0b86-4df7-a055-2677db1a38c2', N'admin@gmail.com', N'Tạo', N'vừa tạo mới vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T15:00:36.7493894' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'20fa2f3c-e2e1-4397-8025-56eef318f2ae', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:29:20.9705512' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'21118595-26bf-4e38-9584-3ae3f910f6e9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:33:39.7741863' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'21f59537-21a0-45e2-b999-ecf977a35e77', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:23.7080237' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'22802bb4-ea09-45b6-9262-da7195186874', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:33:42.4286160' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'22b31789-d7f1-4828-8287-f15c8010eba1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:29:31.2420426' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'22d1202c-88ce-4af1-a3bf-f51604e77a3b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T14:47:52.8537980' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2450bcd4-5834-4e33-97f8-06bf6d1d5607', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-07-01T01:43:04.6294305' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'25f4fbf5-f026-4939-8b07-3d532f165718', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:35:06.1542512' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'26f4c95e-0877-4351-9cfb-715c68324010', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:43:15.3597582' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'270043aa-5d3f-4c4d-9e2b-10642a6aa3fe', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:35.1961748' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2752ee13-0098-4336-8775-162e017476d0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:36.4949173' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'275426c5-398b-4c4e-aa66-9bd833ae48b4', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T03:11:10.8675257' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2773fdae-4652-4c50-896a-2dade2fe4558', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:16:39.3976007' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'28beed8b-1fc0-4ae2-9f3c-b43842d76992', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:47.6709929' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'29e6ddd9-502b-4bbd-99f4-1b17999635e8', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fdsf534vcx!', CAST(N'2022-05-20T14:01:27.5024387' AS DateTime2), 0, N'1d2a4780-a416-4f8c-8bdc-dae4c697f15d', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2a5c14d3-5796-4f35-aaa0-27f2f79d2368', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:10.4274827' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2b3514aa-44de-4cd8-a80b-fa0d4d5cc2b5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:20.5007576' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2bba74fb-3e09-4bf5-9fd3-26aed3102e2f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:35.8730385' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2bee6ed3-203a-4780-a33a-273ffb510c27', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T13:27:49.6185081' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2c3b1839-0f0e-4fad-990b-cb8c28514403', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:10:25.2256797' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2c4fb36f-09f1-4b58-9fd5-78610fc41834', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T16:43:20.3742230' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2cb2d7ca-1506-44db-8dbe-459a3c53236f', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:34:25.2011592' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2df7c542-55db-4c35-84bd-e6adc93a71c1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T12:02:56.2529747' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2eefcba1-f1e0-41c0-81ac-d2f363f80776', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T12:03:04.5029615' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'2f01b2b4-9936-441b-9c7e-85ef72a03636', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T14:06:56.2385339' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'30489217-c543-48f0-8437-57354b8a63dc', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T04:13:56.8273735' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'305e5ade-1669-40c8-bb08-44d11251a2cf', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:54:45.6882813' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3107332b-86f3-4c21-a72f-9cc0ae884d45', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:49.3880908' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'31a65394-caa5-439f-8ee7-cf838a00d1c1', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:18.4055503' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'33b2c09c-3fe0-4070-9381-3f9a52b5a4bb', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:40:14.5265602' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'33fb4475-d53e-4aba-bf85-5fcc7875e799', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T09:03:24.8156259' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'340eaad8-d98f-4d08-a7ad-f0e8bc7407b5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:16:51.2311971' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'343d50f4-f003-4353-b594-73c2bc0d70d5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã c3424fz!', CAST(N'2022-05-20T14:35:20.9784649' AS DateTime2), 0, N'2fda64d8-74de-4056-a7a1-54853e5d4152', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3505507b-628a-4947-8107-185264407faa', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã PN202271311362396!', CAST(N'2022-07-13T11:37:02.1791035' AS DateTime2), 0, N'd57485e6-b0b3-4d50-96be-1e9a30a3e714', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'35096c29-209d-4f23-b980-83940a33d110', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-07-01T02:00:50.8790766' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3564f4dd-2b29-4305-87e2-558359db6782', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-14T10:19:51.4768876' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'358c2b75-ebd8-44d9-935f-29b650c12789', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T08:53:31.5435568' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'35a449ab-7d0d-46d9-8c29-27228817aff5', N'manager@example.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:31:13.1505559' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'35aed8c8-6c92-428f-96f6-88013df9fe9e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T15:04:37.1900056' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'36a84bae-9b5a-4fa5-a57b-ffb8f4c0179c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:16:15.6078507' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'378a05c2-6981-40eb-b532-1a6e8db0ed9e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsd!', CAST(N'2022-04-21T12:26:10.3413240' AS DateTime2), 0, N'693ff1bb-81b3-42d9-bb74-9e6e329009a6', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'37b7fae3-3351-416d-ab0f-103e4848a6f3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:17:27.5785985' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'381b55e7-0925-416b-8fa2-a216c8fe2931', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:21:24.7203858' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'38364672-4908-413f-b50f-465f17c89e78', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:35:04.2791452' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3945f648-dec2-42c7-a34c-581bcaa2254b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:43.9042927' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'394cf9ee-0e56-4b68-8e90-ed735d44f28e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:30:16.4171202' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'39d380f7-c3a9-48e6-9db9-2a7e52d6d040', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T14:14:30.9987720' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'39eb9697-126d-4471-b083-eddadcd3ecce', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:39:45.2620007' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3a1f0476-6a1d-4ced-af07-cdba4730a1e3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:15:46.7456956' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3a807981-b80f-4a38-a168-d1aabfd1f5bc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:34:20.2368010' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3a832ac9-e976-412f-bbaa-663f1af7c265', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T15:17:47.8252124' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3a8b6bdd-4513-4b03-a8d5-773f865b0639', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T14:41:37.7391363' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3a9e80c9-48b6-4019-9ac1-2ea02224e90e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã trung!', CAST(N'2022-07-13T12:39:07.3792930' AS DateTime2), 0, N'6c7c0b1a-59fc-40d0-9651-cae26c29ca2f', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3ad1a4b0-5978-4548-92a9-ad7740a2c528', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T09:58:50.4388000' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3b13c237-3cfa-48d3-934b-aa69a5e0f622', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:17:25.7778325' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
GO
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3c5c6047-0395-4093-abb0-c0230807dc4f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:38:06.0120859' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3cafd333-0001-4e52-964b-58a2da44a079', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:21:42.4634912' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3e309d58-dc6b-4c6c-9bfe-5510d6b4e3cd', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T13:36:38.7959691' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3e6dc973-48cc-4c80-a285-ea5df3142fba', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:45:36.9313027' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'3e86e322-bec9-4e43-a6cb-586b79d0f52c', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:23:51.2247496' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'408939de-42b3-4b34-99f5-857d6834e415', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:54:50.0544518' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'448caee7-2567-4ba1-8072-1e6d14894be3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.4290589' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4518cd5e-d16f-49c3-b8d1-cc78efd3e075', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:46.4496493' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4533f503-9828-4d7b-92ac-a8ffb989cefa', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T11:20:13.1359935' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4568485d-5371-4b71-9a92-267908bb6af5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:50:02.8723461' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4673d0ac-74b3-4607-9fd2-2b0a2370bedd', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:47:13.2135582' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'47e0f193-46ee-46a8-9463-d867e33d7613', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:25.0140881' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4811baf3-fffd-42ff-8a1f-e1c66352820d', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:19:36.8939717' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'482b5c76-1386-4682-861b-cd67f2574ba6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:35:07.3018306' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'49562719-2876-4e97-998a-735251d74349', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-14T10:16:30.7402844' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4986f8d5-bd03-45bd-8528-4c9f9d94bb95', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:41.8281120' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'49b08383-aa39-4284-9c11-52956bf5d261', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:40:52.2739002' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'49db265d-221b-47cd-b050-3483dc032670', N'manager@example.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:31:14.5877377' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'49e5b5b7-2f67-4a65-b90d-c0d8dd0ea173', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:11.0933633' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4a1fea3b-01f6-4634-8637-989cf5d86aa2', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:19:41.1520805' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4a5d198f-1d42-4e48-afef-f4c2f16231d8', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T09:58:54.2857951' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4a5e9d1a-8b20-406a-9861-38e81fa3b3ab', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:31:59.8927234' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4ae32907-96ad-4f7a-95bc-9197cbfcb37e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:56.9007969' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4b18cf68-fbd1-40bf-bbec-984df1334a72', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:13.1700766' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4ba6c257-5fab-467d-a7ae-15edec78d553', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:38:10.9535242' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4bae6f63-ab63-4e78-9be7-65a32a5c5f79', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T12:02:29.1046859' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4bfd2844-8049-42ae-97c7-32090f3fea3f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:49.1076619' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4c08381e-bbaf-458c-8144-30bf2982c3b1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:04.1070336' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4c78d8ee-ac97-41d6-a1b5-9a3ad4d5593b', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-07-15T11:03:43.5286706' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4e3b86e3-555b-4340-a127-524d4f5d0476', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:09.9664885' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'4ea7c5c1-035a-42a1-ad96-bd793835b2ff', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:31:56.5112264' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5035cb6e-17dc-49d3-a8cd-d36ebc0af865', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã test kafka!', CAST(N'2022-07-12T14:48:16.7786042' AS DateTime2), 0, N'b373b7c6-72f5-4511-8f9d-49d10275dab6', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'508b142d-5ab6-4d9e-9c79-b1872b3f21f2', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:56:14.1769765' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5124374e-e53a-4964-94c2-6a934286fc92', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:28:05.0601829' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5190a3a1-b80b-4c12-93e3-7bf4e11a9227', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:27:59.1484329' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'530fcfe2-987f-4430-9031-1de063fd860a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:37.3505558' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'532fee8e-7863-4418-bb0d-e386b5f88239', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khotest9999999!', CAST(N'2022-07-15T10:46:10.9135443' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'534289c3-7336-4d8d-81f1-30ff90da39d5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:00:39.2668553' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'53cd4560-be5d-4858-9969-d623babdd7f9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:18:52.3091459' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'540529c2-ff5b-4ea2-b3f0-8fed8bbc642a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T11:51:15.3739157' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5469e472-0a47-4e94-bd6a-9bc23eb37df0', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:19:02.9352714' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'54a3ac78-3aa8-4508-9345-8ab842f7bd9c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:47:01.5576423' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'55eefcbd-f220-47ea-99cf-90196720fa8c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T14:41:40.5574951' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'55f0c32f-3138-4947-b0dc-3480ec8dd81d', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã test kafka!', CAST(N'2022-05-20T14:12:50.9512043' AS DateTime2), 0, N'b373b7c6-72f5-4511-8f9d-49d10275dab6', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'56f30784-69a8-4ca5-a440-5d1bcb926382', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:29:18.4287593' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5761c2db-5a1e-4300-9987-57f26d49a570', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fsd5ưerfd!', CAST(N'2022-05-20T14:13:55.7348765' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'57aee36a-0252-4a29-adcb-51d3fb0a1b77', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:59:09.8394585' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'586cf651-5d2d-4027-b4cf-7947d1242264', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T09:58:44.1815497' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'597f8a95-c227-46fc-b842-3891b6b83a0e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:02.8783287' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'59a5714e-3db1-4990-9465-97ba297e55f1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:22:29.9801352' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5bec6ced-3d54-4e12-b5f4-f93999056792', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-07T10:34:36.2617036' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5cd0dbcb-516e-4c54-9bae-c91bb17b0bbb', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:35:08.0580896' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5e3f0a2e-bea2-45d5-8859-cd24105dfa57', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã 23312312!', CAST(N'2022-05-20T13:52:58.1142568' AS DateTime2), 0, N'7f82ada1-a55a-4352-b2f9-c05d216932de', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5fd320a2-f822-4d2b-9710-994ea27bd08f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:32:39.9353446' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'5ff9e210-bbbd-467d-96a6-d5ecf5527bdc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:25:05.0314686' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'601eabda-094f-4fc4-8383-33fab8b27f71', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:45:31.8902858' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'605c863f-ff63-4e7c-ad27-94e6c5494b37', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsf534vcx!', CAST(N'2022-05-20T14:01:45.5141295' AS DateTime2), 0, N'1d2a4780-a416-4f8c-8bdc-dae4c697f15d', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6222ef42-cd7e-479d-b402-f181760dc3c0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:57.2335065' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6337c61d-cd05-41e2-af5c-d9398066bfbe', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:50:01.2324480' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6352dc3c-7db6-4c30-9d91-588894e9d452', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:33:43.1473851' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6366242d-0a64-4914-b7ac-28e25df5a928', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T16:43:30.7863370' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'63caf18f-929c-4aaa-8b63-dd8a1ff17bfd', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:06.7615193' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'641d6e37-630a-45dc-b4e3-add217945216', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:25:19.0271127' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'64975031-0fb8-4f42-8d9e-ccdc2251a628', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:34:44.2871144' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'66111cb9-2c25-497d-92b1-008eac841294', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu xuất khovc005!', CAST(N'2022-04-21T15:18:11.0111218' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'664422da-dc2e-4c9d-aef8-c8482cb7678f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T11:35:37.4135175' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'670721ca-61c1-4f06-aa33-2eaf7033cabf', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T14:41:39.7062110' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'675190e1-2c15-43e3-81ec-a56c8c9c30f8', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:30:12.6462885' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'690c41fc-fe84-43bc-b1b7-c5703ee9a459', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:47:12.4507552' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'69d6c7b2-36f0-4c05-9f0a-1e43fda32d3a', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:21:31.3085240' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6a81c7db-3ffe-42fa-812b-c40ea52ecb35', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:22.4856208' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6aacdf41-6cf2-42e6-95ab-ba810975dc64', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã test9999999!', CAST(N'2022-07-15T10:46:12.4634056' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6b4073e2-1e0b-4960-9ea1-8334cedafe07', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khofsd5uerfd!', CAST(N'2022-06-10T12:03:18.8953867' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6b73144e-41f1-47d5-beb0-3ef231a92eba', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:31:35.0578051' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6e0eaa31-dee1-4a86-b2e6-d56311ce5a8d', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T02:53:30.4705707' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6e5340cd-ccad-4071-94b3-9ed34524d16d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:19.2939451' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6e79d054-214b-404a-9a59-395c5ed813c6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:54.4035166' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6e9ccb33-8c26-4b1b-ab08-71df3edd7b9f', N'user@example.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-20T14:15:53.4900442' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6f65b9ae-a59c-47f5-948c-7d9d0adcbdc6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:32:54.8322601' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'6fd6dd38-3453-4977-99b0-b0856122e481', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-14T10:18:37.5913689' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'703a2b3e-d156-40fe-985b-8412dd83764b', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:32:43.2370416' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'70b91cd4-cd4d-4d44-9c94-4839bde36433', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:37:16.7958508' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'70be5244-e215-4c50-95f8-a472089abfc7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:27:54.0529300' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'71f71d3f-9e66-4f7f-9bce-95334ada7c8d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:31:59.2359403' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'72382389-87e2-405a-b45a-ea91f982c7ef', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-07T10:33:43.1529644' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'72901f1d-e5f0-457c-b798-f5b459e3f3c0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:58.7051370' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'74045b8a-0a70-4951-a8f4-3dd788c98b3d', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T02:53:32.2777352' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'747323b9-ab54-4840-989e-a3cbc250fed7', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:48:50.4290744' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7511cd67-7f24-406d-9645-664db041a14d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:35:06.3958199' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'761f67d4-f61b-4445-beea-f653fcab3c7a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:13.8348489' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7656a917-dc08-44aa-8178-c3fa2d71b9c6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:29.2717816' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'767a4597-54fe-4906-98e0-19500273afad', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:40:18.6860153' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'769d712b-8112-4b55-a311-04d22a532aeb', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã sfsdfsdfsd!', CAST(N'2022-04-22T14:19:06.5478106' AS DateTime2), 0, N'4feb4880-4ae5-49f5-b8f6-7353e07cb8d8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'76ab2ef5-c44f-4d4c-88c1-95939ec093a5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:13.2312261' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'77ea0a91-5827-4765-a1ff-8236e4a0692b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-07-01T09:46:35.9220873' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'78562f3f-19ea-41e7-b8e3-bed909820a3d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:28:31.6064102' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7869000a-0a45-409c-bfdd-e54a04e06ea4', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập kho5454cvxc!', CAST(N'2022-07-14T14:29:11.3477356' AS DateTime2), 0, N'f99d38ff-2de8-48e6-99c0-d12ca8494515', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7ad1c5ee-f986-4193-8569-d1d9abc99985', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:56:16.4092280' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7bf2cfb4-9a09-4411-b78b-4d627b5eb771', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T10:35:57.5432403' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7bfd3899-edd7-4d0c-af92-7ec740fbac71', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:33:07.8830162' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
GO
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7db3f451-af56-4604-abe9-7580a36a71c7', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-12T08:51:39.1788023' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7dbbb4d2-6c2c-4d47-8412-1687c41fcf5b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:32:35.3505341' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7dbc4fd0-cef9-4a97-9212-afecbdcf3b3f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã dasadasdas!', CAST(N'2022-05-20T14:01:04.0116064' AS DateTime2), 0, N'3ef4e0c8-d9b2-44f3-bec6-4bdd1ca81217', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7e041d30-0eed-4968-aa14-3165a713945a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:12.8059417' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7e241a18-2967-46e1-bb0f-a8af9c635a89', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:14:04.2068596' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7e2dbe34-5e28-4536-8f10-238f0c5bc51d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã 3213das!', CAST(N'2022-04-21T12:26:15.2488254' AS DateTime2), 0, N'ae2ffafc-4066-4adb-8178-10a70f6915e6', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7e723cf0-d5d1-419d-93a4-c473e0019667', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:48:06.6850309' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7eca5881-c63a-462f-a938-18e677f203e0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-09T11:08:34.8913809' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7f0f4eb0-8cf2-4866-be4f-b2cd3de005d0', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:37:16.8476699' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'7f74ebe2-2392-4d06-b80c-85c4056c790a', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fdsfsdf54!', CAST(N'2022-05-20T14:03:24.4171939' AS DateTime2), 0, N'e50eef6d-3a3b-4bb3-83d5-550bfcac4974', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'802ce305-4f60-42b7-9188-e94920260d5c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:32:32.3497017' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8152c3b6-7b96-41b6-9bf7-2256cbe19bf2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:03:34.4159825' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8182bc62-7b86-4421-a92a-7aca69b82bf9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khotest9999999!', CAST(N'2022-07-15T10:46:02.8465035' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'836ef2f5-4604-4767-ab4e-35447485f29f', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-06-10T12:39:55.0202153' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'84bd8e13-f59c-409c-9334-b40aa61b64c9', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:40:04.2809097' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'85313a1e-acf3-416e-b00c-e48118e4e82d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:58.4670471' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'870f4c6b-6188-4cf1-b1ea-0632c78ac364', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:28:04.3895378' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'897eac16-e39d-4892-ba6e-f16e99788518', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:53.7698550' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'89d615f4-c44b-4f86-ac6b-3616cff6ff14', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T15:04:07.8971836' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8a690673-c904-458c-a428-102412a3f9b9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:58:53.9911381' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8b622959-e819-41f5-8fef-8e917d698c13', N'manager@example.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:31:09.4679346' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8b92774a-b70e-42e9-97b8-7b206946ba94', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoc3424fz!', CAST(N'2022-05-20T14:36:00.8884585' AS DateTime2), 0, N'2fda64d8-74de-4056-a7a1-54853e5d4152', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8be3f70a-77c9-46fc-a0e7-c59cfa42537b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:24.6328965' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8d9ce1e5-e0d7-4055-b34d-27960a2fb177', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:07.1064608' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8fbef076-5d9e-46e1-accd-3ea9b519d3b9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:47:11.7773926' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8fc7dd0f-998c-411f-a264-467bf4faedbd', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:18.5817587' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'8fd5288b-1e59-4974-b5fa-4260980451f8', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:58:57.3006999' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'90355a20-d5a6-4566-9d00-574a30a4d355', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:46:42.6934586' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'91527479-1cc4-44fa-bfb7-34716090dffe', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:22:23.2005603' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'93ed09e3-ed69-4b9a-a026-d2e2ca245729', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:48:55.7130404' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'942f751c-f5ed-43eb-bcf3-4310686b3126', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-08T15:44:47.9267443' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94d52971-dc48-4ebd-9821-1d2da2f9d386', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:57.8383065' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94daea4a-d5d6-41f4-af6b-6f4e064e5a55', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-13T03:01:58.4175964' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94de5973-40e3-46e7-9454-8a717b8e990a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-13T03:02:00.1027387' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94f76a93-b4d9-43b5-9907-d28980e9e7cf', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:09.6396895' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94f8bfd1-de5b-4278-9cdb-eb185c64efeb', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:36:54.5728599' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'94f93789-1dc2-45e1-a350-74bf33b45c49', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:55:33.7239971' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'955ac6de-adc4-4104-8f23-e785e57332f7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T10:47:22.9963387' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'96305bf2-d31a-497c-92e7-c3724fef3b82', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T10:55:01.8584453' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'97a5cbe8-a984-45d2-9436-aa9838b1e811', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khotest9999999!', CAST(N'2022-07-14T15:05:26.8414397' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'97a71779-0cea-46b2-9d89-ea47dd4c593a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rwerwer!', CAST(N'2022-04-21T14:25:37.6646128' AS DateTime2), 0, N'96b96ade-db85-481d-aabb-c47877a583d9', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'97a88891-0da7-47e0-863c-abd9356282a7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:34:44.3139372' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'98657e73-f4b9-4b03-9cca-e76d0e978813', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã test kafka!', CAST(N'2022-06-10T11:55:24.2763849' AS DateTime2), 0, N'b373b7c6-72f5-4511-8f9d-49d10275dab6', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'99386a0a-0b93-4e10-a7c2-f5a9e6e793f5', N'user@example.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã c3424fz!', CAST(N'2022-05-20T14:15:34.7941344' AS DateTime2), 0, N'2fda64d8-74de-4056-a7a1-54853e5d4152', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'9a7f998e-9a63-4e48-9a13-1591d1ecc583', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:11.6037011' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'9bc50a5b-0c1b-49d6-8716-167165c0e9dd', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:34:25.0319666' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'9d627777-d549-468e-84d2-3ac626bd8224', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3299411' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'9e5b8135-e8b0-4f94-830c-dfef5bb59ac9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T14:41:38.7664379' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'9fb0ca87-6516-4738-91ea-7738071b992d', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:49:35.0515805' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a0946f25-80b1-4ef2-8bc2-a49a716ad291', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:57:35.7343916' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a11cfea4-ef0f-49e5-9897-aa3c66ca0dee', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:34:20.1100672' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a15aea06-2a40-4ae0-b3bd-b5ec93f4b0e8', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022714143451255!', CAST(N'2022-07-15T02:40:07.5625888' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a44c796f-09db-4e04-9505-32b31d6d1f68', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:43:52.0435323' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a45dba60-2dcb-4eea-9ef9-95a763ab5de1', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-14T10:17:58.2498524' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a4f3440d-ea13-44da-90bc-8150926572bc', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:52:13.7420156' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a56b36da-4466-4793-a289-f30b7dc57a5a', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:00:39.1070670' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a5830c53-eb00-489a-a2c3-86da2af41daf', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:34:20.2490735' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a6bae6e3-1056-4167-be33-dd079498f85c', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T11:35:29.4947369' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a6eecdd4-4e31-41c4-8dfd-aac8265d58d5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3506963' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a79acc41-632a-4961-ab84-91c1270bb7e5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:51:05.4555516' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a7ac74bb-69f4-45d1-b0be-355b14d4efe5', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T13:36:35.8361090' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a832038a-d968-43b0-acfb-6e6f744c10f1', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:56:17.2883685' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a8db7bef-f8d6-4fb6-9b34-c6148cd6986e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:11.5914292' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'a9caf3c5-e77f-4c95-8cad-d42c8a439c49', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:59.0415361' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'aa5bb8ba-9abb-4671-930b-08514cabf430', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:22:26.9031708' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'aa7a84a3-126a-453d-92ae-781ca53ada87', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T15:00:51.8196816' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ac23f05d-6ecc-4b2d-871f-b085f2249b06', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:21:44.3118597' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ac89b6e1-1949-4619-b6f2-1316bb67e757', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T13:35:13.0980724' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ad106d5d-3d8b-4ed8-80b5-48c76eef096a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:56.9427076' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ae43f4d4-e39d-4984-b631-a402d6a70cee', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:55.0700683' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ae67ecc8-0842-48d0-bb4a-3c29add0480b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:45.6972906' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b0d88415-f61f-47f1-8e37-98858ee849db', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T08:49:11.5684682' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b1cb68c7-d5f2-4f29-b45e-64c6d3991b4b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3186979' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b1e00571-9318-493e-85fd-5842ff650204', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:07.5565284' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b332b539-fc0d-4ae3-94cd-e458e8ea3d18', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T04:13:01.3581019' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b488d9c0-943a-4096-8aa4-931e5446088c', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã 5454cvxc!', CAST(N'2022-05-20T13:54:16.2447645' AS DateTime2), 0, N'f99d38ff-2de8-48e6-99c0-d12ca8494515', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b4ccc446-bf2c-4765-b798-570460046703', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:41:08.9332908' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b5b52565-4856-4708-a098-346d54a065cc', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T10:35:01.7152854' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b66954cd-8bc9-414f-a1ba-889db6320ce9', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:19:21.3620100' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b6a78494-9112-4a9a-914d-b48f31b21b57', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3069534' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b9943a97-4f3a-4c8b-ada9-81c90d8fa265', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T11:51:15.4366623' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'b9e6fc81-8809-474e-8048-7f6eef185ee2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T14:14:40.9040467' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'bbe1c5c2-683f-4cdb-a0fa-52210f4ab353', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T16:43:28.8089240' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'be17b116-49eb-4382-ade2-3c2366ace410', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:31:58.4717243' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'be66a70d-07d0-4687-a526-79ff8098b1a8', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:29:25.9916414' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'be717821-1b4d-4ee8-8461-bc6a26b6e3c2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3800813' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'bef2637b-b68a-4d08-b9af-5364f6762efd', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:43:41.4527669' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'bf49a452-f690-43a5-8459-358cd5680b93', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:23:58.0710336' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c07c88a1-f08e-4b21-858b-fb69f24c58a7', N'user@example.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-20T14:14:47.4746688' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c0aabfae-9d61-4381-9465-c562f077a8e6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:20.3670876' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c14a93da-a2b9-4513-b326-0022179f27ef', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:31:09.3230119' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c449c46d-fb08-461b-8524-62e6132bfffa', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:52:33.0268338' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c45442a4-ee6a-45f2-88f4-4472a0b587b8', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã PN202271312173050!', CAST(N'2022-07-13T12:17:53.6891115' AS DateTime2), 0, N'c33ff070-de56-44a0-8506-89d91429828d', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c4d0bb10-1294-479f-931b-39f2d34cfc80', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:21:31.6975776' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c6ac9688-50fa-4341-b961-dd61f2ec2703', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-15T10:59:52.8513677' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c6be7765-a448-4acb-8806-6d4882f46166', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:48:56.5996569' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c7d2c08b-a98d-4b5c-8eb7-4031f81132e7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã c3424fz!', CAST(N'2022-06-10T11:50:09.6362249' AS DateTime2), 0, N'2fda64d8-74de-4056-a7a1-54853e5d4152', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c88af664-196c-4fbe-81eb-2ad11a4e9af7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:36:34.6576628' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c8a4ae53-4104-4735-bed7-e90c3f364c84', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-21T15:16:14.1728731' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c8bd1bea-17fe-446a-b8be-f5c1de2eb295', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:21.7004390' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
GO
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'c9dcdf46-c557-4c90-b468-4004ca24e431', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:26:07.5767907' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ca3fcc0a-3828-4354-800c-ee5c66275d93', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:54:43.3914265' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ca4bb0a5-c132-4ac7-93e5-601377c7c84d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:30:14.3239903' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ca545e7f-77f3-4d61-b1af-500ee13c205b', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T13:25:08.5160047' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cb15f650-f30e-4382-b8ee-aa337af57901', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:49:15.5143389' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cb6bd0ba-6a84-4ecc-b3b9-25cb778207ce', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:49.7889098' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ccbd1949-ad90-4587-8fcb-10d7ab747cc5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-14T10:31:09.4942650' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ccd5f6a6-4926-4866-bd30-e4aab41cdc86', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:40:25.6715676' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cd59b736-30de-4ba9-89cd-26e8f3a9c8d4', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:19:48.5439896' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com,user@example.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cd5c35d0-561e-4d6a-a9c3-f869ba567ecf', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:11.2052266' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cda0a7f9-e22a-45b3-90d5-a0f12b3bf7d6', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã test9999999!', CAST(N'2022-07-04T15:42:45.7872588' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cdb2e8ad-620e-44c2-a179-250e147024f9', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T13:36:31.0762200' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ce86d033-0250-46dd-a054-4d3454ee9c7e', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:20:07.2232719' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cf5799cd-66b2-43bb-a341-163a87ae3b74', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T02:53:32.2065399' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'cff51cc7-02f4-49c0-aec4-f2b4bc75d5a1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T16:43:25.0438701' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd022e34a-3170-4a9a-b421-9a3758d2bcda', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:50:02.3628694' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd26f3810-7c7f-49e1-b32e-4fc9112df8e7', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T11:36:03.1547311' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd369c1d3-7566-4251-b5eb-a9e795be70a0', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:18:12.3516007' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd45a173e-20ba-4e8f-bb01-f76946bc61f5', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T09:03:25.6579322' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd4f33a43-2b25-4998-9f27-c8adb100bb00', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khotest9999999!', CAST(N'2022-07-14T14:29:51.0187752' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd633a4e8-ddb1-4349-b199-829e561dc85a', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:45:31.6096000' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd823daf7-7752-4f9a-b294-d202c03fd43a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:10.8236225' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd825e931-065d-4513-a344-8792073eaac2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:17:52.4626705' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd880c502-cc8d-4181-a890-c9da8ee39605', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:52:32.7716802' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd962f7f5-0e93-48d8-bf53-3e38043de8c4', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:02:07.5422820' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'd9c41f97-eefd-424f-9348-7e869abeafe3', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:22:15.4876691' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'da2a4258-899e-4c82-9652-608e9b18d45e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:33:09.7443620' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'da644593-837e-4737-bd36-b872a37f7472', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:25:00.6266673' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'db9c932c-b7c1-462e-b4a4-68479767d8ee', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:23.1844059' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'dc3f946a-4ef5-4550-beb9-bdeb8a77fb8d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T17:26:24.6639923' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'dcdc3e88-4b2c-431f-b45b-bc8475747d1b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:25.3826612' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'de294d15-32b3-4a34-b2f2-9c49d73f7c65', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:33:08.8512911' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'de2ba033-c04d-4e64-b0d6-cfd5424cbf39', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã dasda321312!', CAST(N'2022-04-21T12:26:19.8657134' AS DateTime2), 0, N'4abeccf8-1260-45f8-947c-99f2680d3046', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'de4cd336-2b9e-4d86-8047-36726342ca1e', N'user@example.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất khovc005!', CAST(N'2022-04-22T09:54:35.1063086' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',user@example.com,admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'de79c8ae-e47a-4747-b8f5-9689fbf4c2dc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:14.8009633' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'decc4498-28d3-443d-9d37-3c9f0178e922', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:50:00.5980273' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'dee37a68-4a5b-462d-8228-8aebda78efd8', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rwerwer!', CAST(N'2022-04-21T11:34:47.9973877' AS DateTime2), 0, N'96b96ade-db85-481d-aabb-c47877a583d9', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'df6df97d-0a57-4779-8b95-712b71ce5403', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:13.0037359' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e0346833-d6e6-4bde-80b4-02081f404285', N'user@example.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã đâs342423!', CAST(N'2022-05-20T14:18:34.8407809' AS DateTime2), 0, N'3a28e8bb-fa6d-4fc2-9673-38581d77dafd', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e04e6fd5-562a-42d6-a4e8-a5c7d4e4ed52', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:55:38.9938011' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e076086e-32eb-429f-8a35-01339db3d8cc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T11:50:23.7973665' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e128a017-465b-4c89-b7ff-b7d8f7073cc6', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:08.8788488' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e2315707-db78-4607-aacd-4002e310516f', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã đasadasdas!', CAST(N'2022-05-20T14:00:10.4222783' AS DateTime2), 0, N'3ef4e0c8-d9b2-44f3-bec6-4bdd1ca81217', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e23fff05-4128-4504-9ccf-60a43999ca6b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:10:16.9916781' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e27063ea-d7c9-45bf-81aa-cc4d7e5c7ffa', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:59.9329913' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e3a13b2c-4d6d-456b-8c9f-39f1e1d20ddb', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:09.4419594' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e3d19636-b80c-4c1c-8dd9-f67fe2ead98e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:29:24.3954448' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e3de517b-3827-455d-94f6-0bb5bdff0303', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-05-20T14:36:56.5041754' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e3eb4e7a-d830-47aa-8cd8-6975d191d0e1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:28:30.3880057' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e4473c42-e5f9-4890-86fa-fcd05de73393', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:22:57.3047182' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e46543f1-8145-4c70-b736-d57681b76110', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã vcxvxc324vcxvxc!', CAST(N'2022-05-20T14:05:09.4143259' AS DateTime2), 0, N'a13c3d8c-c614-438e-82ac-b68643f6eab0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e4b63e58-6d2d-4196-aa83-b5677478939d', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:11.4230481' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e4f64c38-fbd6-4164-9140-e82ac3cc4626', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:11.0142718' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e548f1a9-88b1-435a-bdfd-21e2e41ccb46', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-06-10T11:51:15.4093360' AS DateTime2), 0, N'509f1f69-4d36-410a-a4ba-8956dadcc333', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e64cadc9-7d09-4fdf-ac28-43c290ba7035', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:32:58.4472527' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e736b516-ca1e-4904-b46f-7c12de87fbcc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:49:30.8255693' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e7486e51-b514-4ca2-a9a9-c1ae66b3938b', N'admin@gmail.com', N'Tạo', N'vừa tạo mới phiếu nhập kho có mã PN2022714143451255!', CAST(N'2022-07-14T14:39:21.3377745' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e873e3f7-0b49-4f0e-a7e1-8690c6c6e998', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu xuất kho có mã vc005!', CAST(N'2022-04-21T14:10:58.0126352' AS DateTime2), 0, N'87679c26-9d60-4a08-abf0-45406709ca14', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e8ed0aa5-49e3-45bd-a5b3-de0b303ef86e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-23T16:43:29.8073140' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'e9936f55-c7bd-42b3-a6ab-c136f0bd1932', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-07T10:34:49.8569100' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ec0192a4-4743-4cb7-82e0-28111e10cef8', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T03:46:20.0638826' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ec8c8ce8-673a-4b1b-9ff9-a3eb69450e06', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022714143451255!', CAST(N'2022-07-15T02:21:40.0638509' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'edb5123b-3b5f-4164-8108-cee57819f936', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN202271511321490!', CAST(N'2022-07-15T11:06:54.4761347' AS DateTime2), 0, N'db31fb7d-7c6d-495c-9482-c38e17b229d0', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ede56862-6c4a-4dd8-bcc3-e5f7b402b030', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:27:08.4353740' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'edea195a-cae0-495a-afe7-8b3d86e19cf2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:28:26.8878584' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ef081ca9-cebd-423d-bff1-45aef77e0887', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:25:34.2097682' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ef14a9c7-48ce-4e34-8e70-8521427c0296', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:22:17.7067678' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ef3c52b0-165e-411b-96fa-ee41e1d04481', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:23:00.9844866' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'efd43855-b7c6-429f-8d7a-6a463decddec', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T10:15:01.5406840' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f0df0cc5-0429-45d3-b7d0-e67ee4a5084b', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T04:12:55.8557183' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f17a7bee-f25b-4a24-9a7d-e4dfcc02bc37', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:08.3414172' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f1adfd2d-46fd-4ae5-a54b-06999c555e17', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khotest9999999!', CAST(N'2022-07-14T15:05:13.9275731' AS DateTime2), 0, N'db210548-929d-4eaf-bfbf-d5393cff021c', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f1dc8480-b477-4b0b-88d0-acb305d5d6b9', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:43:48.2404234' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f23359f3-303f-4812-a383-1e253d82c376', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-04-22T14:21:20.8650105' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f276a783-3b55-4fbe-9823-f5c2c11c70d5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:22.0519300' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f376b48b-0965-4f5f-ae67-9fe0b6afe6c2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:36:26.1906537' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f377e5e0-ba1b-4fcd-a9d5-7fc4785b389b', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa vật tư trong phiếu nhập khoPN2022714143451255!', CAST(N'2022-07-14T14:43:19.6157957' AS DateTime2), 0, N'1a251eb5-480e-4285-9cfa-5a7e34f47154', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f5536ea6-20ad-44ca-962a-9220ce02fdc2', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:57.9277659' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f618f005-01b6-4a19-9d12-b6f05647a5e7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-06-10T11:48:54.0022453' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f669e601-08b9-4285-aa63-92e6adfa9fd8', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T10:39:33.5437236' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f70c568b-95c1-42ff-a3cc-02fffa93fabc', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:27:06.1636872' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f79620eb-4bf3-4307-b99b-832d681a6a4f', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fdsfsd5vxvcx!', CAST(N'2022-05-20T14:22:10.6288501' AS DateTime2), 0, N'8a148d80-2cb5-4000-9470-bf334841b4a0', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f7d29b57-bf6f-4da4-8ea6-5ccde18f363c', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-04-22T14:23:56.8559595' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f7dfe65a-ddcf-4c85-b5ab-5bd1a2ede64d', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-15T10:48:29.9854994' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f7f61d6e-d7ec-4499-9dd7-ee9482a738d3', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T12:22:06.6187037' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f80be8d3-bc74-451a-af5a-b787a3691ecc', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-11T11:08:13.2035052' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'f864b2d2-9989-4851-b9d5-5b40f65fd315', N'admin@gmail.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-15T11:35:32.2157385' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fa650ed1-b039-4be8-98df-e2a51008a924', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:33:41.5329526' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fa6e16e6-649c-442f-a98b-c8dfcd26598e', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã PN2022713121825168!', CAST(N'2022-07-13T12:27:00.4168238' AS DateTime2), 0, N'fb3bc004-57a8-41a2-9ae7-10c93c84f1e8', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'facf5246-de64-4d66-be8e-ec5d37b2d0e9', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu danh sách phiếu trong sổ kho !', CAST(N'2022-07-11T02:53:32.2521233' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'faf37e70-3e81-45a9-9b13-f928a22058d5', N'admin@gmail.com', N'Xóa', N'vừa xóa phiếu nhập kho !', CAST(N'2022-07-07T13:25:28.6924526' AS DateTime2), 0, N'', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fb50ae3a-e2a9-4d7b-9522-5cdef7d22f7a', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:04:24.1180857' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fc48ddfa-49df-433f-93c7-65d450871749', N'manager@example.com', N'Chỉnh sửa', N'vừa đồng bộ hóa giữa Sql và Elastic !', CAST(N'2022-07-14T10:31:11.1193634' AS DateTime2), 0, N'', 0, NULL)
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fcabcc7f-6b1e-4d07-ba4b-1a125878e7b1', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsdfsdfsd!', CAST(N'2022-05-23T09:34:44.3288693' AS DateTime2), 0, N'ffc32a20-a24b-4a7e-99fd-c76a15258190', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fcc44adc-0fed-4a0a-a158-51d72345d6c9', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã fsd5uerfd!', CAST(N'2022-06-10T11:51:56.9217375' AS DateTime2), 0, N'dbf3e351-36a9-4c3b-b587-d1f9f8424976', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'fcf1e953-0723-42ac-ad6f-a6a951832385', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:21:25.3844692' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
INSERT [dbo].[HistoryNotication] ([Id], [UserName], [Method], [Body], [CreateDate], [Read], [Link], [OnDelete], [UserNameRead]) VALUES (N'ffd966a1-5467-4925-bdf5-ee96df7ae6c7', N'admin@gmail.com', N'Chỉnh sửa', N'vừa chỉnh sửa phiếu nhập kho có mã rewcvxvc!', CAST(N'2022-04-21T14:24:42.1848360' AS DateTime2), 0, N'6a73aee7-78c1-4ee9-bb69-7b564660e1e7', 0, N',admin@gmail.com')
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

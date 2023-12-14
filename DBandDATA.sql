USE [master]
GO
/****** Object:  Database [MyShopDB]    Script Date: 12/14/2023 3:15:10 PM ******/
CREATE DATABASE [MyShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShopDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShopDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShopDB]
GO
/****** Object:  User [huy]    Script Date: 12/14/2023 3:15:10 PM ******/
CREATE USER [huy] FOR LOGIN [huy] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [chanhuynek]    Script Date: 12/14/2023 3:15:10 PM ******/
CREATE USER [chanhuynek] FOR LOGIN [chanhuynek] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [huy]
GO
ALTER ROLE [db_owner] ADD MEMBER [chanhuynek]
GO
/****** Object:  Table [dbo].[CATEGORY]    Script Date: 12/14/2023 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUSTOMER]    Script Date: 12/14/2023 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSTOMER_NAME] [nvarchar](30) NULL,
	[AGE] [int] NULL,
	[PHONE_NUMBER] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDER_DETAIL]    Script Date: 12/14/2023 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDER_DETAIL](
	[ORDER_ID] [int] NOT NULL,
	[PRODUCT_ID] [int] NOT NULL,
	[AMOUNT] [int] NULL,
	[PRICE] [float] NULL,
	[TOTAL_PRICE] [int] NULL,
 CONSTRAINT [PK__ORDER_DE__6321D5126DB4B103] PRIMARY KEY CLUSTERED 
(
	[ORDER_ID] ASC,
	[PRODUCT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDER1]    Script Date: 12/14/2023 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDER1](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ORDER_DATE] [date] NULL,
	[CUSTOMER_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 12/14/2023 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PRODUCT_NAME] [nvarchar](30) NULL,
	[PRICE] [float] NULL,
	[DESCRIPTION] [nvarchar](50) NULL,
	[CATEGORY_ID] [int] NULL,
	[IMG_PATH] [nchar](50) NULL,
	[AMOUNT] [int] NULL,
 CONSTRAINT [PK__PRODUCT__3214EC27976716B6] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CATEGORY] ON 

INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (1, N'Laptop')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (2, N'Smartphone')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (3, N'Tablet')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (4, N'TV')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (5, N'Smartwatch')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (6, N'Máy in')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (7, N'Laptop')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (8, N'Camera')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (9, N'Sạc dự phòng')
INSERT [dbo].[CATEGORY] ([ID], [NAME]) VALUES (10, N'Bàn phím')
SET IDENTITY_INSERT [dbo].[CATEGORY] OFF
GO
SET IDENTITY_INSERT [dbo].[CUSTOMER] ON 

INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (1, N'test', 18, N'0901476807')
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (2, N'chan huy', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (3, N'lili', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (4, N'huy234', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (5, N'w', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (6, N'kitkat', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (7, N'111', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (8, N'nhi', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (9, N'kitkut', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (10, N'kiko', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (11, N'landautien', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (12, N'jakinatsumi', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (13, N'Lưu Chấn Huy', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (14, N'Nguyễn Thanh Tú', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (15, N'Đặng Ái Nhi', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (16, N'Nguyễn Thành Nam', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (17, N'Trương Thanh Mỹ', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (18, N'Nguyễn Tú Nhi', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (19, N'Nguyễn Thành An', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (20, N'Nguyễn Trúc Nhi', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (21, N'kgjgjgj', NULL, NULL)
INSERT [dbo].[CUSTOMER] ([ID], [CUSTOMER_NAME], [AGE], [PHONE_NUMBER]) VALUES (22, N'aaaa', NULL, NULL)
SET IDENTITY_INSERT [dbo].[CUSTOMER] OFF
GO
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (1, 3, 4, 16, 64)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (1, 7, 2, 5, 10)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (1, 8, 1, 11, 11)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (2, 2, 3, 25, 75)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (2, 5, 2, 4, 8)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (3, 3, 3, 16, 48)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (3, 4, 3, 2, 6)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (3, 5, 3, 4, 12)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (3, 8, 2, 11, 22)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (4, 2, 3, 25, 75)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (4, 6, 2, 6, 12)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (5, 6, 1, 6, 6)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (6, 4, 2, 2, 4)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (6, 5, 2, 4, 8)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (6, 7, 2, 5, 10)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (7, 2, 3, 25, 75)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (7, 5, 2, 4, 8)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (8, 4, 1, 2, 2)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (9, 4, 2, 2, 4)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (12, 7, 3, 5, 15)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (13, 2, 3, 25, 75)
INSERT [dbo].[ORDER_DETAIL] ([ORDER_ID], [PRODUCT_ID], [AMOUNT], [PRICE], [TOTAL_PRICE]) VALUES (13, 9, 2, 4, 8)
GO
SET IDENTITY_INSERT [dbo].[ORDER1] ON 

INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (1, CAST(N'2016-04-06' AS Date), 13)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (2, CAST(N'2016-04-06' AS Date), 14)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (3, CAST(N'2023-12-05' AS Date), 15)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (4, CAST(N'2023-12-05' AS Date), 16)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (5, CAST(N'2023-12-10' AS Date), 13)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (6, CAST(N'2023-12-10' AS Date), 17)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (7, CAST(N'2023-11-26' AS Date), 18)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (8, CAST(N'2023-12-10' AS Date), 19)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (9, CAST(N'2023-12-10' AS Date), 18)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (12, CAST(N'2023-12-14' AS Date), 21)
INSERT [dbo].[ORDER1] ([ID], [ORDER_DATE], [CUSTOMER_ID]) VALUES (13, CAST(N'2023-12-14' AS Date), 22)
SET IDENTITY_INSERT [dbo].[ORDER1] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (2, N'Máy tính bảng Honor Pad X9', 25, N'Chip:Snapdragon 685 8 nhân', 2, NULL, 87)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (3, N'Acer Nitro 5 Gaming AN515', 16, N'i5 11400H/8GB/512GB/144Hz/4GB GTX1650/Win11', 1, NULL, 93)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (4, N'BeFit Sporty 2 Pro', 2, N'44.8mm Ðen', 5, NULL, 91)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (5, N'HP LaserJet MFP 135a', 4, N'Máy in laser trắng đen đa năng', 6, NULL, 91)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (6, N'Baseus ioTa BPE45A', 6, N'Trạm sạc dự phòng di động 90000mAh', 9, NULL, 97)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (7, N'Samsung Galaxy Watch 6', 5, N'40mm', 5, NULL, 93)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (8, N'Laptop Dell Inspiron 15', 11, N'i3 1215U/8GB/256GB/OfficeHS/Win11', 1, NULL, 97)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (9, N'Samsung Galaxy Tab A9', 4, N'44.8mm Ðen', 3, NULL, 97)
INSERT [dbo].[PRODUCT] ([ID], [PRODUCT_NAME], [PRICE], [DESCRIPTION], [CATEGORY_ID], [IMG_PATH], [AMOUNT]) VALUES (10, N' DareU EK807G', 14, N'Không Dây, 3 chế độ kết nối', 10, N'Images\Employee1.png                              ', 100)
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
GO
ALTER TABLE [dbo].[ORDER_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_DETAIL_ORDER1] FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[ORDER1] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ORDER_DETAIL] CHECK CONSTRAINT [FK_ORDER_DETAIL_ORDER1]
GO
ALTER TABLE [dbo].[ORDER_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_DETAIL_PRODUCT] FOREIGN KEY([PRODUCT_ID])
REFERENCES [dbo].[PRODUCT] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ORDER_DETAIL] CHECK CONSTRAINT [FK_ORDER_DETAIL_PRODUCT]
GO
ALTER TABLE [dbo].[ORDER1]  WITH CHECK ADD  CONSTRAINT [FK_ORDER1_CUSTOMER] FOREIGN KEY([CUSTOMER_ID])
REFERENCES [dbo].[CUSTOMER] ([ID])
GO
ALTER TABLE [dbo].[ORDER1] CHECK CONSTRAINT [FK_ORDER1_CUSTOMER]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_CATEGORY] FOREIGN KEY([CATEGORY_ID])
REFERENCES [dbo].[CATEGORY] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_CATEGORY]
GO
USE [master]
GO
ALTER DATABASE [MyShopDB] SET  READ_WRITE 
GO

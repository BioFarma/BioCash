USE [master]
GO
/****** Object:  Database [BioCash]    Script Date: 15/11/2018 16.23.56 ******/
CREATE DATABASE [BioCash]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BioCash', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BioCash.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BioCash_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BioCash_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BioCash] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BioCash].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BioCash] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BioCash] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BioCash] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BioCash] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BioCash] SET ARITHABORT OFF 
GO
ALTER DATABASE [BioCash] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BioCash] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BioCash] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BioCash] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BioCash] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BioCash] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BioCash] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BioCash] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BioCash] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BioCash] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BioCash] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BioCash] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BioCash] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BioCash] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BioCash] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BioCash] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BioCash] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BioCash] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BioCash] SET RECOVERY FULL 
GO
ALTER DATABASE [BioCash] SET  MULTI_USER 
GO
ALTER DATABASE [BioCash] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BioCash] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BioCash] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BioCash] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [BioCash]
GO
/****** Object:  Schema [biocash]    Script Date: 15/11/2018 16.23.56 ******/
CREATE SCHEMA [biocash]
GO
/****** Object:  StoredProcedure [dbo].[getLaporan]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getLaporan]

as
begin

declare @tmptransaksi table (id int,jumlah decimal)
insert into @tmptransaksi select 1,1000
insert into @tmptransaksi select 2,2000
insert into @tmptransaksi select 3,3000
insert into @tmptransaksi select 4,4000

declare @tmpjasa table (id int,jumlah decimal)

insert into @tmpjasa select 1,100


select * from @tmptransaksi a left join @tmpjasa b on a.id=b.id
select * from @tmptransaksi  union all select * from @tmpjasa 

end
GO
/****** Object:  Table [biocash].[Bagian]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[Bagian](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nama_bagian] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [biocash].[Masterkas]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[Masterkas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[kd_kas] [varchar](50) NULL,
	[Kas] [varchar](50) NULL,
	[Plafond] [int] NULL,
	[nosk] [varchar](50) NULL,
	[BEGDA] [datetime] NULL,
	[ENDDA] [datetime] NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [biocash].[Pemasukkan]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[Pemasukkan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kas] [varchar](50) NULL,
	[tgl_masuk] [varchar](50) NULL,
	[thn_periode] [varchar](50) NULL,
	[jmlh_masuk] [int] NULL,
	[BEGDA] [datetime] NULL,
	[ENDDA] [datetime] NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [biocash].[Pengeluaran]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[Pengeluaran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kas] [varchar](50) NULL,
	[jmlh_keluar] [int] NULL,
	[unit] [int] NULL,
	[harga] [int] NULL,
	[pph] [int] NULL,
	[nama_bagian] [varchar](50) NULL,
	[vendor] [varchar](50) NULL,
	[nomor] [varchar](50) NULL,
	[satuan] [varchar](50) NULL,
	[jasa] [varchar](50) NULL,
	[keterangan] [varchar](50) NULL,
	[tgl_keluar] [varchar](50) NULL,
	[thn_periode] [varchar](50) NULL,
	[BEGDA] [datetime] NULL,
	[ENDDA] [datetime] NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [biocash].[PengeluaranJasa]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[PengeluaranJasa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kas] [varchar](50) NULL,
	[jmlh_keluar] [int] NULL,
	[unit] [int] NULL,
	[harga] [int] NULL,
	[pph] [int] NULL,
	[nama_bagian] [varchar](50) NULL,
	[vendor] [varchar](50) NULL,
	[nomor] [varchar](50) NULL,
	[satuan] [varchar](50) NULL,
	[jasa] [varchar](50) NULL,
	[keterangan] [varchar](50) NULL,
	[tgl_keluar] [varchar](50) NULL,
	[thn_periode] [varchar](50) NULL,
	[BEGDA] [datetime] NULL,
	[ENDDA] [datetime] NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [biocash].[Saldo]    Script Date: 15/11/2018 16.23.56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [biocash].[Saldo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kas] [varchar](50) NULL,
	[saldo] [int] NULL,
	[thn_periode] [varchar](50) NULL,
	[BEGDA] [datetime] NULL,
	[ENDDA] [datetime] NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [biocash].[Bagian] ON 

INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (1, N'TI')
INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (2, N'Infrastruktur')
INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (3, N'Keuangan')
INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (4, N'Dapur')
INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (5, N'Office Boy')
INSERT [biocash].[Bagian] ([Id], [nama_bagian]) VALUES (6, N'Logistik')
SET IDENTITY_INSERT [biocash].[Bagian] OFF
SET IDENTITY_INSERT [biocash].[Masterkas] ON 

INSERT [biocash].[Masterkas] ([id], [kd_kas], [Kas], [Plafond], [nosk], [BEGDA], [ENDDA], [change_date]) VALUES (6, N'IT', N'IT', 25000000, N'3207220811960003', CAST(0x0000A98900AC8473 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A98900AC8473 AS DateTime))
SET IDENTITY_INSERT [biocash].[Masterkas] OFF
SET IDENTITY_INSERT [biocash].[Pemasukkan] ON 

INSERT [biocash].[Pemasukkan] ([Id], [Kas], [tgl_masuk], [thn_periode], [jmlh_masuk], [BEGDA], [ENDDA], [change_date]) VALUES (49, N'IT', N'11/8/2018', N'2018', 25000000, CAST(0x0000A99200AC7901 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A99200AC7901 AS DateTime))
SET IDENTITY_INSERT [biocash].[Pemasukkan] OFF
SET IDENTITY_INSERT [biocash].[Pengeluaran] ON 

INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (139, N'IT', 4000000, 4, 1000000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500A9B077 AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A99500A9B077 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (145, N'IT', 4000000, 4, 1000000, 50000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500ADE1EE AS DateTime), CAST(0x0000A99500AE213C AS DateTime), CAST(0x0000A99500AE213C AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (146, N'IT', 4000000, 4, 1000000, 10000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500AE213C AS DateTime), CAST(0x0000A99500C9AB67 AS DateTime), CAST(0x0000A99500C9AB67 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (147, N'IT', 2000000, 4, 500000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A996015083B7 AS DateTime), CAST(0x0000A99901044A1A AS DateTime), CAST(0x0000A99901044A1A AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (148, N'IT', 400000, 4, 100000, NULL, N'Infrastruktur', N'avian', NULL, N'Unit', N'Tidak', N'coba tidak', N'11/14/2018', N'2018', CAST(0x0000A9960150B751 AS DateTime), CAST(0x0000A999010489E4 AS DateTime), CAST(0x0000A9960150B751 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (149, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'3810', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A996015134B4 AS DateTime), CAST(0x0000A996015313B5 AS DateTime), CAST(0x0000A996015313B5 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (150, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A9990086F69E AS DateTime), CAST(0x0000A99900E73842 AS DateTime), CAST(0x0000A99900E73842 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (151, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'212312', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E75660 AS DateTime), CAST(0x0000A99900E7908C AS DateTime), CAST(0x0000A99900E7908C AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (152, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'123', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E7B3FB AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (153, N'IT', 2000000, 4, 500000, 60000, N'TI', N'MegaComp', N'123', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E7C8A2 AS DateTime), CAST(0x0000A99901016E0E AS DateTime), CAST(0x0000A99901016E0E AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (154, N'IT', 100000, 2, 50000, NULL, N'Dapur', N'Toko beras Rosebrand', NULL, N'kiloan', N'Ya', N'beras', N'11/19/2018', N'2018', CAST(0x0000A99900FBCE94 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A99900FBCE94 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (155, N'IT', 100000, 2, 50000, 5000, N'Dapur', N'Toko beras Rosebrand', N'098098', N'kiloan', N'Ya', N'beras', N'11/19/2018', N'2018', CAST(0x0000A99900FC8E58 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (156, N'IT', 2000000, 4, 500000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99901044A1A AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A99901044A1A AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (157, N'IT', 200000, 2, 100000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'printer', N'11/20/2018', N'2018', CAST(0x0000A999010DB645 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A999010DB645 AS DateTime))
INSERT [biocash].[Pengeluaran] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (158, N'IT', 200000, 2, 100000, 30000, N'TI', N'MegaComp', N'312312', N'Unit', N'Ya', N'printer', N'11/20/2018', N'2018', CAST(0x0000A999010DD37D AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A999010DD37D AS DateTime))
SET IDENTITY_INSERT [biocash].[Pengeluaran] OFF
SET IDENTITY_INSERT [biocash].[PengeluaranJasa] ON 

INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (26, N'IT', 4000000, 4, 1000000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500A9B077 AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A99500C9AB67 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (32, N'IT', 4000000, 4, 1000000, 50000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500ADE1EE AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A99500AE213C AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (33, N'IT', 4000000, 4, 1000000, 10000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'hey tayo', N'11/12/2018', N'2018', CAST(0x0000A99500AE213C AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A99500AE213C AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (34, N'IT', 2000000, 4, 500000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A996015083B7 AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A99901016E0E AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (35, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'3810', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A996015134B4 AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A996015134B4 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (36, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'1029380', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A9990086F69E AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A9990086F69E AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (37, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'212312', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E75660 AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A99900E75660 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (38, N'IT', 2000000, 4, 500000, 50000, N'TI', N'MegaComp', N'123', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E7B3FB AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (39, N'IT', 2000000, 4, 500000, 60000, N'TI', N'MegaComp', N'123', N'Unit', N'Ya', N'COBA AJA', N'11/13/2018', N'2018', CAST(0x0000A99900E7C8A2 AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (40, N'IT', 100000, 2, 50000, NULL, N'Dapur', N'Toko beras Rosebrand', NULL, N'kiloan', N'Ya', N'beras', N'11/19/2018', N'2018', CAST(0x0000A99900FBCE94 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (41, N'IT', 100000, 2, 50000, 5000, N'Dapur', N'Toko beras Rosebrand', N'098098', N'kiloan', N'Ya', N'beras', N'11/19/2018', N'2018', CAST(0x0000A99900FC8E58 AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (42, N'IT', 200000, 2, 100000, NULL, N'TI', N'MegaComp', NULL, N'Unit', N'Ya', N'printer', N'11/20/2018', N'2018', CAST(0x0000A999010DB645 AS DateTime), CAST(0x0000A999010DD37D AS DateTime), CAST(0x0000A999010DD37D AS DateTime))
INSERT [biocash].[PengeluaranJasa] ([Id], [Kas], [jmlh_keluar], [unit], [harga], [pph], [nama_bagian], [vendor], [nomor], [satuan], [jasa], [keterangan], [tgl_keluar], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (43, N'IT', 200000, 2, 100000, 30000, N'TI', N'MegaComp', N'312312', N'Unit', N'Ya', N'printer', N'11/20/2018', N'2018', CAST(0x0000A999010DD37D AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A999010DD37D AS DateTime))
SET IDENTITY_INSERT [biocash].[PengeluaranJasa] OFF
SET IDENTITY_INSERT [biocash].[Saldo] ON 

INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (198, N'IT', 25000000, N'2018', CAST(0x0000A99400F08092 AS DateTime), CAST(0x0000A99500A9B077 AS DateTime), CAST(0x0000A99500A9B077 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (224, N'IT', 21000000, N'2018', CAST(0x0000A99500A9B077 AS DateTime), CAST(0x0000A99500ADE1EE AS DateTime), CAST(0x0000A99500ADE1EE AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (230, N'IT', 21050000, N'2018', CAST(0x0000A99500ADE1EE AS DateTime), CAST(0x0000A99500AE213C AS DateTime), CAST(0x0000A99500AE213C AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (231, N'IT', 21010000, N'2018', CAST(0x0000A99500AE213C AS DateTime), CAST(0x0000A99500C9AB67 AS DateTime), CAST(0x0000A99500C9AB67 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (232, N'IT', 21000000, N'2018', CAST(0x0000A99500C9AB67 AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A99500C9C5D1 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (233, N'IT', 25000000, N'2018', CAST(0x0000A99500C9C5D1 AS DateTime), CAST(0x0000A996015083B7 AS DateTime), CAST(0x0000A996015083B7 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (234, N'IT', 23000000, N'2018', CAST(0x0000A996015083B7 AS DateTime), CAST(0x0000A9960150B751 AS DateTime), CAST(0x0000A9960150B751 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (235, N'IT', 22600000, N'2018', CAST(0x0000A9960150B751 AS DateTime), CAST(0x0000A996015134B4 AS DateTime), CAST(0x0000A996015134B4 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (236, N'IT', 22650000, N'2018', CAST(0x0000A996015134B4 AS DateTime), CAST(0x0000A996015313B5 AS DateTime), CAST(0x0000A996015313B5 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (237, N'IT', 22600000, N'2018', CAST(0x0000A996015313B5 AS DateTime), CAST(0x0000A9990086F69E AS DateTime), CAST(0x0000A9990086F69E AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (238, N'IT', 22650000, N'2018', CAST(0x0000A9990086F69E AS DateTime), CAST(0x0000A99900E73842 AS DateTime), CAST(0x0000A99900E73842 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (239, N'IT', 22600000, N'2018', CAST(0x0000A99900E73842 AS DateTime), CAST(0x0000A99900E75660 AS DateTime), CAST(0x0000A99900E75660 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (240, N'IT', 22650000, N'2018', CAST(0x0000A99900E75660 AS DateTime), CAST(0x0000A99900E7908C AS DateTime), CAST(0x0000A99900E7908C AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (241, N'IT', 22600000, N'2018', CAST(0x0000A99900E7908C AS DateTime), CAST(0x0000A99900E7B3FB AS DateTime), CAST(0x0000A99900E7B3FB AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (242, N'IT', 22650000, N'2018', CAST(0x0000A99900E7B3FB AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime), CAST(0x0000A99900E7C8A2 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (243, N'IT', 22660000, N'2018', CAST(0x0000A99900E7C8A2 AS DateTime), CAST(0x0000A99900FBCE94 AS DateTime), CAST(0x0000A99900FBCE94 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (244, N'IT', 22560000, N'2018', CAST(0x0000A99900FBCE94 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime), CAST(0x0000A99900FC8E58 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (245, N'IT', 22565000, N'2018', CAST(0x0000A99900FC8E58 AS DateTime), CAST(0x0000A99901016E0E AS DateTime), CAST(0x0000A99901016E0E AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (246, N'IT', 22505000, N'2018', CAST(0x0000A99901016E0E AS DateTime), CAST(0x0000A99901044A1A AS DateTime), CAST(0x0000A99901044A1A AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (247, N'IT', 22505000, N'2018', CAST(0x0000A99901044A1A AS DateTime), CAST(0x0000A999010489E4 AS DateTime), CAST(0x0000A999010489E4 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (248, N'IT', 22905000, N'2018', CAST(0x0000A999010489E4 AS DateTime), CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A9990104E184 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (249, N'IT', 24905000, N'2018', CAST(0x0000A9990104E184 AS DateTime), CAST(0x0000A999010DB645 AS DateTime), CAST(0x0000A999010DB645 AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (250, N'IT', 24705000, N'2018', CAST(0x0000A999010DB645 AS DateTime), CAST(0x0000A999010DD37D AS DateTime), CAST(0x0000A999010DD37D AS DateTime))
INSERT [biocash].[Saldo] ([Id], [Kas], [saldo], [thn_periode], [BEGDA], [ENDDA], [change_date]) VALUES (251, N'IT', 24735000, N'2018', CAST(0x0000A999010DD37D AS DateTime), CAST(0x002D247F00000000 AS DateTime), CAST(0x0000A999010DD37D AS DateTime))
SET IDENTITY_INSERT [biocash].[Saldo] OFF
USE [master]
GO
ALTER DATABASE [BioCash] SET  READ_WRITE 
GO

/****** Object:  Table [dbo].[DealDetail]    Script Date: 06/21/2014 15:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DealDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Position] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsOneSize] [bit] NOT NULL,
	[CollectionUnitPrice] [smallmoney] NOT NULL,
	[DeliveryUnitPrice] [smallmoney] NOT NULL,
	[PromotionText] [nvarchar](500) NOT NULL,
	[Remarks] [nvarchar](500) NOT NULL,
	[OptionEnabled_1] [bit] NULL,
	[OptionEnabled_2] [bit] NULL,
	[OptionEnabled_3] [bit] NULL,
	[OptionEnabled_4] [bit] NULL,
	[OptionEnabled_5] [bit] NULL,
	[OptionEnabled_6] [bit] NULL,
	[OptionEnabled_7] [bit] NULL,
	[OptionEnabled_8] [bit] NULL,
	[OptionEnabled_9] [bit] NULL,
	[LinkedMenu_1] [int] NULL,
	[LinkedMenu_2] [int] NULL,
	[LinkedMenu_3] [int] NULL,
	[LinkedMenu_4] [int] NULL,
	[LinkedMenu_5] [int] NULL,
	[LinkedMenu_6] [int] NULL,
	[LinkedMenu_7] [int] NULL,
	[LinkedMenu_8] [int] NULL,
	[LinkedMenu_9] [int] NULL,
 CONSTRAINT [PK_DealDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE dbo.Menu_Category ADD
	IsDeal bit NULL
GO
ALTER TABLE dbo.Menu_Category ADD
	IsAvailableForDeal bit NULL
    
ALTER TABLE dbo.Menu_Category ADD MaxDressing INT NULL;
ALTER TABLE dbo.PostCodesPrices ADD Min_Order SMALLMONEY NULL;

/****** 2015 Jan 06 - For Delivery Timing ******/
ALTER TABLE dbo.OrderInfo ADD ExpectedTime varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD ExpectedTime varchar(50) NULL;

/****** 2015 May 22 - For Storing extra information when paying order by card ******/
ALTER TABLE dbo.BasketTemp ADD VpsTxId varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD TxAuthNo varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD BankAuthCode varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD Last4Digits varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD CreatedOn datetime NULL;
ALTER TABLE dbo.BasketTemp ADD CreatedBy varchar(50) NULL;
ALTER TABLE dbo.BasketTemp ADD ChangedOn datetime NULL;
ALTER TABLE dbo.BasketTemp ADD ChangedBy varchar(50) NULL;

ALTER TABLE dbo.OrderInfo ADD VpsTxId varchar(50) NULL;
ALTER TABLE dbo.OrderInfo ADD TxAuthNo varchar(50) NULL;
ALTER TABLE dbo.OrderInfo ADD BankAuthCode varchar(50) NULL;
ALTER TABLE dbo.OrderInfo ADD Last4Digits varchar(50) NULL;
/****** Object:  StoredProcedure [dbo].[GetBaseSelection]    Script Date: 03/28/2014 11:51:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBaseSelection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBaseSelection]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerAddress]    Script Date: 03/28/2014 11:51:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCustomerAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCustomerAddress]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerInfo]    Script Date: 03/28/2014 11:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCustomerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCustomerInfo]
GO
/****** Object:  StoredProcedure [dbo].[GetDealbyMenuId]    Script Date: 03/28/2014 11:51:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealbyMenuId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDealbyMenuId]
GO
/****** Object:  StoredProcedure [dbo].[GetDealInfo]    Script Date: 03/28/2014 11:51:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDealInfo]
GO
/****** Object:  StoredProcedure [dbo].[GetDealMenubyCatId]    Script Date: 03/28/2014 11:51:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealMenubyCatId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDealMenubyCatId]
GO
/****** Object:  StoredProcedure [dbo].[GetDealSubMenubyMenuId]    Script Date: 03/28/2014 11:51:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealSubMenubyMenuId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDealSubMenubyMenuId]
GO
/****** Object:  StoredProcedure [dbo].[GetDeliveryTiming]    Script Date: 03/28/2014 11:51:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDeliveryTiming]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDeliveryTiming]
GO
/****** Object:  StoredProcedure [dbo].[GetDressings]    Script Date: 03/28/2014 11:51:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDressings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDressings]
GO
/****** Object:  StoredProcedure [dbo].[GetEmailSettings]    Script Date: 03/28/2014 11:51:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEmailSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEmailSettings]
GO
/****** Object:  StoredProcedure [dbo].[GetMenubyCategory]    Script Date: 03/28/2014 11:51:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMenubyCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMenubyCategory]
GO
/****** Object:  StoredProcedure [dbo].[GetOpeningTiming]    Script Date: 03/28/2014 11:51:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOpeningTiming]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOpeningTiming]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetail]    Script Date: 03/28/2014 11:51:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOrderDetail]
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentOptions]    Script Date: 03/28/2014 11:51:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPaymentOptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPaymentOptions]
GO
/****** Object:  StoredProcedure [dbo].[GetPricebySubMenuId]    Script Date: 03/28/2014 11:51:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPricebySubMenuId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPricebySubMenuId]
GO
/****** Object:  StoredProcedure [dbo].[GetSideOrders]    Script Date: 03/28/2014 11:51:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSideOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSideOrders]
GO
/****** Object:  StoredProcedure [dbo].[GetSubmenubyMenuId]    Script Date: 03/28/2014 11:51:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubmenubyMenuId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSubmenubyMenuId]
GO
/****** Object:  StoredProcedure [dbo].[GetToppings]    Script Date: 03/28/2014 11:51:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetToppings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetToppings]
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateEmail]    Script Date: 03/28/2014 11:51:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckDuplicateEmail]
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateMobile]    Script Date: 03/28/2014 11:51:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateMobile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckDuplicateMobile]
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateTelephone]    Script Date: 03/28/2014 11:51:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateTelephone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckDuplicateTelephone]
GO
/****** Object:  StoredProcedure [dbo].[CheckPostCode]    Script Date: 03/28/2014 11:51:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckPostCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckPostCode]
GO
/****** Object:  StoredProcedure [dbo].[RecoverPassword]    Script Date: 03/28/2014 11:51:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoverPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecoverPassword]
GO
/****** Object:  StoredProcedure [dbo].[SaveCustomerAddress]    Script Date: 03/28/2014 11:51:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveCustomerAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveCustomerAddress]
GO
/****** Object:  StoredProcedure [dbo].[SaveOrderInfo]    Script Date: 03/28/2014 11:51:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveOrderInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveOrderInfo]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderInfo]    Script Date: 03/28/2014 11:51:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateOrderInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateOrderInfo]
GO
/****** Object:  StoredProcedure [dbo].[VarifyLogin]    Script Date: 03/28/2014 11:51:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarifyLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[VarifyLogin]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/28/2014 11:41:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[ServicesCharge]    Script Date: 03/28/2014 11:39:59 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServicesC__IsAct__30F848ED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ServicesCharge] DROP CONSTRAINT [DF__ServicesC__IsAct__30F848ED]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServicesCharge]') AND type in (N'U'))
DROP TABLE [dbo].[ServicesCharge]
GO
/****** Object:  Table [dbo].[StaticPage]    Script Date: 03/28/2014 11:40:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StaticPage]') AND type in (N'U'))
DROP TABLE [dbo].[StaticPage]
GO
/****** Object:  Table [dbo].[SubMenu_Item]    Script Date: 03/28/2014 11:40:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SubMenu_Item]') AND type in (N'U'))
DROP TABLE [dbo].[SubMenu_Item]
GO
/****** Object:  Table [dbo].[ToppingCategory]    Script Date: 03/28/2014 11:40:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToppingCategory]') AND type in (N'U'))
DROP TABLE [dbo].[ToppingCategory]
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 03/28/2014 11:39:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Restaurant]') AND type in (N'U'))
DROP TABLE [dbo].[Restaurant]
GO
/****** Object:  Table [dbo].[Restaurant_Timing]    Script Date: 03/28/2014 11:39:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Restaurant_Timing]') AND type in (N'U'))
DROP TABLE [dbo].[Restaurant_Timing]
GO
/****** Object:  Table [dbo].[Base_Selection]    Script Date: 03/28/2014 11:32:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Selection]') AND type in (N'U'))
DROP TABLE [dbo].[Base_Selection]
GO
/****** Object:  Table [dbo].[BasketItemTemp]    Script Date: 03/28/2014 11:32:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasketItemTemp]') AND type in (N'U'))
DROP TABLE [dbo].[BasketItemTemp]
GO
/****** Object:  Table [dbo].[BasketTemp]    Script Date: 03/28/2014 11:32:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasketTemp]') AND type in (N'U'))
DROP TABLE [dbo].[BasketTemp]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 03/28/2014 11:33:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[Customer_Address]    Script Date: 03/28/2014 11:33:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer_Address]') AND type in (N'U'))
DROP TABLE [dbo].[Customer_Address]
GO
/****** Object:  Table [dbo].[Deal_Detail]    Script Date: 03/28/2014 11:33:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Deal_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Deal_Detail]
GO
/****** Object:  Table [dbo].[Delivery_Timing]    Script Date: 03/28/2014 11:34:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Timing]') AND type in (N'U'))
DROP TABLE [dbo].[Delivery_Timing]
GO
/****** Object:  Table [dbo].[DeliveryCharges]    Script Date: 03/28/2014 11:34:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryCharges]') AND type in (N'U'))
DROP TABLE [dbo].[DeliveryCharges]
GO
/****** Object:  Table [dbo].[EmailSender]    Script Date: 03/28/2014 11:34:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSender]') AND type in (N'U'))
DROP TABLE [dbo].[EmailSender]
GO
/****** Object:  Table [dbo].[EmailSettings]    Script Date: 03/28/2014 11:34:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSettings]') AND type in (N'U'))
DROP TABLE [dbo].[EmailSettings]
GO
/****** Object:  Table [dbo].[FlatFieldsName]    Script Date: 03/28/2014 11:35:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatFieldsName]') AND type in (N'U'))
DROP TABLE [dbo].[FlatFieldsName]
GO
/****** Object:  Table [dbo].[FlatFieldsValue]    Script Date: 03/28/2014 11:35:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatFieldsValue]') AND type in (N'U'))
DROP TABLE [dbo].[FlatFieldsValue]
GO
/****** Object:  Table [dbo].[Menu_Category]    Script Date: 03/28/2014 11:35:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Category]') AND type in (N'U'))
DROP TABLE [dbo].[Menu_Category]
GO
/****** Object:  Table [dbo].[Menu_Dressing]    Script Date: 03/28/2014 11:36:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Dressing]') AND type in (N'U'))
DROP TABLE [dbo].[Menu_Dressing]
GO
/****** Object:  Table [dbo].[Menu_Item]    Script Date: 03/28/2014 11:36:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Item]') AND type in (N'U'))
DROP TABLE [dbo].[Menu_Item]
GO
/****** Object:  Table [dbo].[Menu_Item_SideOrder]    Script Date: 03/28/2014 11:36:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Item_SideOrder]') AND type in (N'U'))
DROP TABLE [dbo].[Menu_Item_SideOrder]
GO
/****** Object:  Table [dbo].[Menu_Topping]    Script Date: 03/28/2014 11:37:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Topping]') AND type in (N'U'))
DROP TABLE [dbo].[Menu_Topping]
GO
/****** Object:  Table [dbo].[MenuOption]    Script Date: 03/28/2014 11:37:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuOption]') AND type in (N'U'))
DROP TABLE [dbo].[MenuOption]
GO
/****** Object:  Table [dbo].[OptionDetail]    Script Date: 03/28/2014 11:38:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OptionDetail]') AND type in (N'U'))
DROP TABLE [dbo].[OptionDetail]
GO
/****** Object:  Table [dbo].[Order_Detail]    Script Date: 03/28/2014 11:38:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Order_Detail]
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 03/28/2014 11:38:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderInfo]') AND type in (N'U'))
DROP TABLE [dbo].[OrderInfo]
GO
/****** Object:  Table [dbo].[PostCodesPrices]    Script Date: 03/28/2014 11:38:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostCodesPrices]') AND type in (N'U'))
DROP TABLE [dbo].[PostCodesPrices]
GO
/****** Object:  Table [dbo].[PostCodesPrices]    Script Date: 03/28/2014 11:38:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostCodesPrices]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PostCodesPrices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[POST_CODE] [varchar](50) NOT NULL,
	[PRICE] [smallmoney] NULL,
	[ALLOW_DELIVERY] [bit] NULL,
 CONSTRAINT [PK_PostCodesPrices_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 03/28/2014 11:38:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderInfo](
	[OrderId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CustomerID] [numeric](18, 0) NULL,
	[OrderDate] [datetime] NULL,
	[OrderType] [varchar](50) NULL,
	[OrderStatus] [varchar](50) NULL,
	[ProcessingTime] [int] NULL,
	[TotalAmount] [float] NULL,
	[AmountReceived] [float] NULL,
	[AmountDue] [float] NULL,
	[DiscountType] [varchar](200) NULL,
	[Discount] [float] NULL,
	[VoucherCode] [varchar](50) NULL,
	[DeliveryCharges] [float] NULL,
	[PaymentCharges] [float] NULL,
	[PaymentType] [varchar](50) NULL,
	[PayStatus] [varchar](50) NULL,
	[Special_Instructions] [varchar](1000) NULL,
	[IsEdited] [bit] NULL,
	[AnyReason] [varchar](1024) NULL,
	[AddressId] [numeric](18, 0) NULL,
	[Shop_PostCode] [varchar](10) NULL,
 CONSTRAINT [PK_OrderInfo] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Detail]    Script Date: 03/28/2014 11:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Order_Detail](
	[OrderDetail_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[OrderID] [numeric](18, 0) NULL,
	[MenuItemName] [varchar](1024) NULL,
	[Quantity] [int] NULL,
	[Price] [numeric](12, 2) NULL,
	[SpecialRequest] [varchar](1024) NULL,
	[Dressing] [varchar](1000) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[OrderDetail_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionDetail]    Script Date: 03/28/2014 11:38:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OptionDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OptionDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Position] [int] NOT NULL,
	[UnitPrice] [numeric](10, 2) NOT NULL,
	[OptionID] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_OptionDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MenuOption]    Script Date: 03/28/2014 11:37:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuOption]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuOption](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Position] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[ItemsAllowed] [int] NULL,
 CONSTRAINT [PK_MenuOptions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Topping]    Script Date: 03/28/2014 11:37:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Topping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu_Topping](
	[Topping_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[Position] [int] NULL,
	[CategoryID] [int] NULL,
 CONSTRAINT [PK_Menu_Topping] PRIMARY KEY CLUSTERED 
(
	[Topping_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Item_SideOrder]    Script Date: 03/28/2014 11:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Item_SideOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu_Item_SideOrder](
	[Menu_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[PromotText] [varchar](300) NULL,
	[IsActive] [bit] NULL,
	[Collection_Price] [numeric](12, 2) NULL,
	[Delivery_Price] [numeric](12, 2) NULL,
	[MultisaveQuantity] [int] NULL,
	[MultiSaveDiscount] [numeric](12, 2) NULL,
	[PreparationTime] [int] NULL,
	[Remarks] [nvarchar](1000) NULL,
	[MenuImage] [varchar](500) NULL,
	[ItemPosition] [int] NULL,
	[LargeImage] [varchar](500) NULL,
 CONSTRAINT [PK_Menu_Item_SideOrder] PRIMARY KEY CLUSTERED 
(
	[Menu_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Item]    Script Date: 03/28/2014 11:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu_Item](
	[Menu_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[PromotText] [varchar](300) NULL,
	[IsActive] [bit] NULL,
	[Collection_Price] [numeric](12, 2) NULL,
	[Delivery_Price] [numeric](12, 2) NULL,
	[MultisaveQuantity] [int] NULL,
	[MultiSaveDiscount] [numeric](12, 2) NULL,
	[PreparationTime] [int] NULL,
	[Category_Id] [int] NULL,
	[HasSubMenu] [bit] NULL,
	[Remarks] [varchar](1000) NULL,
	[MenuImage] [varchar](500) NULL,
	[bHasDressing] [bit] NULL,
	[bHasTopping] [bit] NULL,
	[bHasBase] [bit] NULL,
	[ItemPosition] [int] NULL,
	[Topping_Price] [float] NULL,
	[LargeImage] [varchar](500) NULL,
	[Option_ID_1] [int] NULL,
	[Option_ID_2] [int] NULL,
	[ToppingPrice1] [numeric](10, 2) NULL,
	[ToppingPrice2] [numeric](10, 2) NULL,
	[ToppingPrice3] [numeric](10, 2) NULL,
 CONSTRAINT [PK_Menu_Item] PRIMARY KEY CLUSTERED 
(
	[Menu_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Dressing]    Script Date: 03/28/2014 11:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Dressing]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu_Dressing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[ItemPosition] [int] NULL,
 CONSTRAINT [PK_Menu_Dressing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Category]    Script Date: 03/28/2014 11:35:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Category]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu_Category](
	[Category_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[Active] [bit] NULL,
	[ItemPosition] [int] NULL,
	[NormalImage] [varchar](500) NULL,
	[MouseOverImage] [varchar](500) NULL,
	[ExclOnlineDiscount] [bit] NULL,
 CONSTRAINT [PK_Menu_Category] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FlatFieldsValue]    Script Date: 03/28/2014 11:35:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatFieldsValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FlatFieldsValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[FieldValue] [nvarchar](500) NULL,
 CONSTRAINT [PK_FlatFieldsValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FlatFieldsName]    Script Date: 03/28/2014 11:35:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatFieldsName]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FlatFieldsName](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FlatFieldsName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmailSettings]    Script Date: 03/28/2014 11:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [varchar](500) NOT NULL,
	[Host] [varchar](500) NOT NULL,
	[AuthenticationUser] [varchar](500) NOT NULL,
	[AuthenticationPassword] [varchar](500) NOT NULL,
	[ApplicationServerURL] [varchar](500) NULL,
	[FeedbackEmail] [varchar](500) NULL,
	[Port] [int] NOT NULL,
	[Timeout] [int] NOT NULL,
	[EnableSsl] [bit] NOT NULL,
 CONSTRAINT [PK_EmailSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmailSender]    Script Date: 03/28/2014 11:34:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSender]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailSender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[FullName] [nvarchar](250) NULL,
 CONSTRAINT [PK_EmailSender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DeliveryCharges]    Script Date: 03/28/2014 11:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryCharges]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DeliveryCharges](
	[ID] [int] NOT NULL,
	[Distance] [float] NULL,
	[Charges] [float] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Delivery_Timing]    Script Date: 03/28/2014 11:34:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Timing]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Delivery_Timing](
	[DeliveryTime_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Delivery_Day] [smallint] NULL,
	[Start_Time] [varchar](50) NULL,
	[End_Time] [varchar](50) NULL,
	[Remarks] [varchar](1024) NULL,
	[Discountpercent] [int] NULL,
	[DiscountOver] [decimal](10, 2) NULL,
	[DiscountValue] [decimal](10, 2) NULL,
	[OfferText] [nvarchar](500) NULL,
 CONSTRAINT [PK_Delivery_Timing] PRIMARY KEY CLUSTERED 
(
	[DeliveryTime_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Deal_Detail]    Script Date: 03/28/2014 11:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Deal_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Deal_Detail](
	[Deal_Step_Id] [numeric](18, 0) NOT NULL,
	[Step_No] [int] NULL,
	[Step_Label] [varchar](300) NULL,
	[Step_Description] [varchar](500) NULL,
	[Step_Category_Label] [varchar](300) NULL,
	[Step_Category_Id] [numeric](18, 0) NULL,
	[Step_Menu_Id] [numeric](18, 0) NULL,
	[IsFixed] [bit] NULL,
	[IsTopping] [bit] NULL,
	[IsDressing] [bit] NULL,
	[IsSubMenu] [bit] NULL,
	[SubMenuLabel] [varchar](300) NULL,
	[SubMenuFixed] [bit] NULL,
	[SubMenuIndex] [smallint] NULL,
	[Menu_Id] [numeric](18, 0) NULL,
	[Step_Image] [varchar](1024) NULL,
	[Remarks] [varchar](1024) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Address]    Script Date: 03/28/2014 11:33:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer_Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer_Address](
	[Address_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Customer_Id] [numeric](18, 0) NOT NULL,
	[Address] [varchar](1000) NULL,
	[City] [varchar](200) NULL,
	[PostCode] [varchar](11) NULL,
	[County] [varchar](200) NULL,
	[AddressNotes] [varchar](1024) NULL,
	[GridEast] [varchar](50) NULL,
	[GridNorth] [varchar](50) NULL,
	[Distance] [float] NULL,
 CONSTRAINT [PK_Customer_Address] PRIMARY KEY CLUSTERED 
(
	[Address_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 03/28/2014 11:33:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[Customer_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Title] [varchar](10) NULL,
	[First_Name] [varchar](200) NULL,
	[Last_Name] [varchar](200) NULL,
	[Telephone] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[Email] [varchar](300) NULL,
	[Password] [varchar](50) NULL,
	[PasswordHint] [varchar](50) NULL,
	[Image_Location] [varchar](1024) NULL,
	[Points] [int] NULL,
	[Rating] [int] NULL,
	[Credit_Customer] [bit] NULL,
	[CreditLimit] [smallmoney] NULL,
	[Current_Credit] [smallmoney] NULL,
	[Invoice_Period] [int] NULL,
	[Active] [bit] NULL,
	[Member_Since] [datetime] NULL,
	[Remarks] [varchar](1024) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BasketTemp]    Script Date: 03/28/2014 11:32:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasketTemp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BasketTemp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserLogIn] [nvarchar](128) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderType] [varchar](50) NULL,
	[OrderStatus] [varchar](50) NULL,
	[TotalAmount] [decimal](10, 2) NULL,
	[AmountReceived] [decimal](10, 2) NULL,
	[AmountDue] [decimal](10, 2) NULL,
	[PayStatus] [varchar](50) NULL,
	[VoucherCode] [varchar](50) NULL,
	[DiscountType] [varchar](50) NULL,
	[Discount] [decimal](10, 2) NULL,
	[DeliveryCharge] [decimal](10, 2) NULL,
	[PaymentCharge] [decimal](10, 2) NULL,
	[PaymentType] [varchar](50) NULL,
	[SpecialInstructions] [nvarchar](500) NULL,
	[AddressID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[SagePayStatus] [varchar](100) NULL,
 CONSTRAINT [PK_BasketTemp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BasketItemTemp]    Script Date: 03/28/2014 11:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasketItemTemp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BasketItemTemp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BasketID] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[Quantity] [int] NULL,
	[SpecialRequest] [nvarchar](500) NULL,
	[ItemName] [nvarchar](200) NULL,
	[Status] [bit] NULL,
	[Dressing] [nvarchar](200) NULL,
 CONSTRAINT [PK_BasketItemTemp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Base_Selection]    Script Date: 03/28/2014 11:32:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Selection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Base_Selection](
	[Base_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[Price] [float] NULL,
	[SelectedIndex] [smallint] NULL,
	[Topping_Price] [float] NULL,
	[Remarks] [varchar](1024) NULL,
 CONSTRAINT [PK_Base_Selection] PRIMARY KEY CLUSTERED 
(
	[Base_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Restaurant_Timing]    Script Date: 03/28/2014 11:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Restaurant_Timing]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Restaurant_Timing](
	[Time_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[DAY] [smallint] NULL,
	[Opening_Time] [varchar](50) NULL,
	[Closing_Time] [varchar](50) NULL,
	[Remarks] [varchar](1024) NULL,
	[Discountpercent] [int] NULL,
	[DiscountOver] [decimal](10, 2) NULL,
	[DiscountValue] [decimal](10, 2) NULL,
	[OfferText] [nvarchar](500) NULL,
 CONSTRAINT [PK_Restaurant_Timing] PRIMARY KEY CLUSTERED 
(
	[Time_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 03/28/2014 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Restaurant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Restaurant](
	[RestaurantID] [int] IDENTITY(1,1) NOT NULL,
	[ShopName] [varchar](200) NOT NULL,
	[ShopNo] [varchar](10) NULL,
	[Building_Name] [varchar](200) NULL,
	[Street] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[PostCode] [varchar](12) NOT NULL,
	[County] [varchar](200) NULL,
	[Telephone1] [varchar](20) NULL,
	[Telephone2] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](200) NULL,
	[Logo] [varbinary](1) NULL,
	[Slogan] [varchar](300) NULL,
	[WebURL] [varchar](1024) NULL,
	[NumberOfTelLines] [tinyint] NULL,
	[OrderStartNo] [char](5) NULL,
	[DeliveryValue] [money] NULL,
	[DeliveryCharge] [money] NULL,
	[ServiceCharge] [money] NULL,
	[BackUpDay] [int] NULL,
	[WebSiteStatus] [bit] NULL,
	[Messageoftheday] [varchar](5000) NULL,
	[EnableCashPayments] [bit] NULL,
	[EnableNochex] [bit] NULL,
	[OnlineDiscount] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ToppingCategory]    Script Date: 03/28/2014 11:40:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToppingCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ToppingCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Position] [int] NULL,
	[Remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_ToppingCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SubMenu_Item]    Script Date: 03/28/2014 11:40:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SubMenu_Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SubMenu_Item](
	[SubMenu_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[Menu_Id] [numeric](18, 0) NOT NULL,
	[Delivery_Price] [float] NULL,
	[Collection_Price] [float] NULL,
	[MultisaveQuantity] [int] NULL,
	[MultiSaveDiscount] [float] NULL,
	[PreparationTime] [int] NULL,
	[SubMenuItemImage] [varchar](1000) NULL,
	[ItemPosition] [int] NULL,
	[IsActive] [bit] NULL,
	[Topping_Price] [float] NULL,
	[Remarks] [varchar](1024) NULL,
	[ToppingPrice1] [numeric](10, 2) NULL,
	[ToppingPrice2] [numeric](10, 2) NULL,
	[ToppingPrice3] [numeric](10, 2) NULL,
 CONSTRAINT [PK_SubMenu_Item] PRIMARY KEY CLUSTERED 
(
	[SubMenu_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StaticPage]    Script Date: 03/28/2014 11:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StaticPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StaticPage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StaticPage] [varchar](50) NOT NULL,
	[Image] [varchar](256) NULL,
	[Abstract] [nvarchar](1000) NULL,
	[Body] [ntext] NULL,
 CONSTRAINT [PK_StaticPage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ServicesCharge]    Script Date: 03/28/2014 11:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServicesCharge]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ServicesCharge](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Charge] [float] NULL,
	[Description] [nvarchar](500) NULL,
	[IsActived] [bit] NULL DEFAULT ((0)),
	[ChargeOnOrder] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/28/2014 11:41:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastLoggedIn] [datetime] NULL,
	[IsActived] [bit] NULL,
	[Email] [varchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_System] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[VarifyLogin]    Script Date: 03/28/2014 11:51:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarifyLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[VarifyLogin]   
@username as varchar(50), -- we first define the parameters we will get from the application when using this procedure   
@password as varchar(50),
@CustomerId as int OUTPUT,
@FirstName as varchar(200) OUTPUT,
@LastName as varchar(200) OUTPUT  
AS  
BEGIN  
SELECT Customer_Id,First_Name,Last_Name FROM Customer WHERE Email=@username AND password=@password --we compare the values from the application we got with the values in the database   

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderInfo]    Script Date: 03/28/2014 11:51:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateOrderInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateOrderInfo] 
	-- Add the parameters for the stored procedure here
	@OrderId int,
	@Orderamount float,
	@OrderStatus varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update orderinfo set OrderStatus=@OrderStatus,AmountReceived=@orderamount,AmountDue=0,PayStatus=''PAID'' where OrderId=@OrderId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveOrderInfo]    Script Date: 03/28/2014 11:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveOrderInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveOrderInfo]
  
      
  @CustomerId int,
  @OrderType varchar(50),
  @OrderStatus varchar(50),
  @TotalAmount float,
  @AmountReceived float,
  @AmountDue float,
  @DiscountType varchar(100),
  @Discount float,
  @DeliveryCharges float,
  @PaymentCharges float,
  @PaymentType varchar(50),
  @PayStatus varchar(50),
  @SpecInstr varchar(1023),
  @AddressId int,
  
  @OrderItems varchar(7999),
  @OrderItemsQty varchar(1023),
  @OrderItemsPrice varchar(5000),
  @OrderItemsDressing varchar(7999),
  @OrderItemsStatus varchar(1023),
  @LastOrderId as int OUTPUT
     
AS
      DECLARE @count int
      DECLARE @OrderId int
      DECLARE @ItemName VARCHAR(500)
      DECLARE @spot SMALLINT 
      DECLARE @Quantity  int
      DECLARE @Price float
      DECLARE @Dressing varchar(1023)
      DECLARE @Status bit
 
	BEGIN
      INSERT INTO OrderInfo(CustomerID,OrderDate,OrderType,OrderStatus,TotalAmount,AmountReceived,AmountDue,DiscountType,Discount,DeliveryCharges,PaymentCharges,PaymentType,PayStatus,Special_Instructions,AddressId) VALUES (@CustomerID,GetDate(),@OrderType,@OrderStatus,@TotalAmount,@AmountReceived,@AmountDue,@DiscountType,@Discount,@DeliveryCharges,@PaymentCharges,@PaymentType,@PayStatus,@SpecInstr,@AddressId)
       SELECT @OrderId = SCOPE_IDENTITY()
       SET @LastOrderId = SCOPE_IDENTITY()  
     END
     
    --Here we will insert order detail values in orderDetail table  
    WHILE @OrderItems  <> '''' 
    BEGIN 
        
        SET @spot = CHARINDEX('','', @OrderItems) 
        IF @spot>0 
			BEGIN 
               SET @ItemName = LEFT(@OrderItems , @spot-1) 
               SET @OrderItems  = RIGHT(@OrderItems , LEN(@OrderItems )-@spot) 
             END
          ELSE 
               SET @OrderItems  = '''' 
        
        SET @spot = CHARINDEX('','', @OrderItemsQty) 
        IF @spot>0 
			BEGIN 
				SET @Quantity = LEFT(@OrderItemsQty , @spot-1) 
				SET @OrderItemsQty  = RIGHT(@OrderItemsQty , LEN(@OrderItemsQty )-@spot) 
			END
		ELSE
			SET @OrderItemsQty  = '''' 
			
				
        SET @spot = CHARINDEX('','', @OrderItemsPrice) 
        IF @spot>0 
			BEGIN 
               SET @Price = LEFT(@OrderItemsPrice , @spot-1)  
               SET @OrderItemsPrice  = RIGHT(@OrderItemsPrice , LEN(@OrderItemsPrice )-@spot) 
            END
        ELSE
               SET @OrderItemsPrice  = ''''
                
        SET @spot = CHARINDEX('','', @OrderItemsDressing) 
        IF @spot>0 
			BEGIN 
				SET @Dressing = LEFT(@OrderItemsDressing , @spot-1)  
				SET @OrderItemsDressing  = RIGHT(@OrderItemsDressing , LEN(@OrderItemsDressing )-@spot)
			END
		ELSE
			SET @OrderItemsDressing  = ''''
			
        SET @spot = CHARINDEX('','', @OrderItemsStatus) 
        IF @spot>0 
			BEGIN
				SET @Status = LEFT(@OrderItemsStatus , @spot-1)  
				SET @OrderItemsStatus  = RIGHT(@OrderItemsStatus , LEN(@OrderItemsStatus )-@spot)
            END
        ELSE
			SET @OrderItemsStatus  = ''''
                
		INSERT INTO Order_Detail(OrderId,MenuItemName,Quantity,Price,Dressing,[Status]) VALUES (@OrderId,@ItemName,@Quantity,@Price,@Dressing,@Status)
       
    END
     
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveCustomerAddress]    Script Date: 03/28/2014 11:51:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveCustomerAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveCustomerAddress]
	-- Add the parameters for the stored procedure here
	@CustomerId int=0,
	@Address varchar(500),
	@City varchar(200),
	@PostCode varchar(200),
	@AddressNotes varchar(1023),
	@AddressId as int OUTPUT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    insert into Customer_Address(Customer_Id,[Address],City,PostCode,AddressNotes) values(@CustomerId,@Address,@City,@PostCode,@AddressNotes)
	
	SET @AddressId = SCOPE_IDENTITY() 
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[RecoverPassword]    Script Date: 03/28/2014 11:51:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoverPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecoverPassword]
	-- Add the parameters for the stored procedure here
	@CustEmail as varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Password] from Customer where Email=@CustEmail
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[CheckPostCode]    Script Date: 03/28/2014 11:51:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckPostCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[CheckPostCode]
@PostCode nvarchar(50)
as 
	begin
		select * from PostCodesPrices where POST_CODE=@PostCode
	end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateTelephone]    Script Date: 03/28/2014 11:51:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateTelephone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckDuplicateTelephone]
	-- Add the parameters for the stored procedure here
	@CustTelephone as varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(*) from Customer where Telephone=@CustTelephone
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateMobile]    Script Date: 03/28/2014 11:51:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateMobile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckDuplicateMobile]
	-- Add the parameters for the stored procedure here
	@CustMobile as varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(*) from Customer where Mobile=@CustMobile
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateEmail]    Script Date: 03/28/2014 11:51:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDuplicateEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckDuplicateEmail]
	-- Add the parameters for the stored procedure here
	@CustEmail as varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(*) from Customer where Email=@CustEmail
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetToppings]    Script Date: 03/28/2014 11:51:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetToppings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetToppings] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Menu_Topping order by Name
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSubmenubyMenuId]    Script Date: 03/28/2014 11:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubmenubyMenuId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSubmenubyMenuId]
	-- Add the parameters for the stored procedure here
	@MenuId as int 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SubMenu_Id,Name,Delivery_Price,Collection_Price,Topping_Price from SubMenu_Item where IsActive=''True'' and Menu_Id=@MenuId order by ItemPosition
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSideOrders]    Script Date: 03/28/2014 11:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSideOrders]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSideOrders] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Menu_Id,Name,Delivery_Price,MenuImage FROM [Menu_Item_SideOrder]
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPricebySubMenuId]    Script Date: 03/28/2014 11:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPricebySubMenuId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPricebySubMenuId] 
	-- Add the parameters for the stored procedure here
	@SubMenuId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Delivery_Price,Topping_Price from SubMenu_Item where SubMenu_Id=@SubMenuId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentOptions]    Script Date: 03/28/2014 11:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPaymentOptions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPaymentOptions]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 EnableCashPayments,EnableNochex from Restaurant
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetail]    Script Date: 03/28/2014 11:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderDetail] 
	-- Add the parameters for the stored procedure here
	@CustomerId INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ord.OrderId,ord.orderdate,ord.orderstatus,ord.TotalAmount,od.MenuItemName,od.Quantity,od.Price from Orderinfo ord,Order_Detail od where ord.OrderId=od.OrderId and ord.orderstatus<>''DELIVERED'' and ord.orderstatus<>''WAITING_PAYMENT'' and datediff(day,orderdate,getdate())<5 and ord.CustomerId=@CustomerId order by orderdate desc
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetOpeningTiming]    Script Date: 03/28/2014 11:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOpeningTiming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOpeningTiming] 
	-- Add the parameters for the stored procedure here
	@DayNumber INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Opening_Time,Closing_Time from Restaurant_Timing where [Day]=@DayNumber
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetMenubyCategory]    Script Date: 03/28/2014 11:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMenubyCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMenubyCategory] 
	-- Add the parameters for the stored procedure here
	@CategoryId int=1
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Menu_Id,Name,PromotText,Delivery_Price,MultiSaveQuantity,MultiSaveDiscount,HasSubMenu,bHasTopping,bHasDressing,bHasBase,Remarks,MenuImage,LargeImage FROM [Menu_Item] where Category_Id=@CategoryId order by itemposition
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmailSettings]    Script Date: 03/28/2014 11:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEmailSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmailSettings] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from EmailSettings
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDressings]    Script Date: 03/28/2014 11:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDressings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDressings]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Menu_Dressing order by Name
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDeliveryTiming]    Script Date: 03/28/2014 11:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDeliveryTiming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDeliveryTiming] 
	-- Add the parameters for the stored procedure here
	@DayNumber INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Start_Time,End_Time from Delivery_Timing where Delivery_Day=@DayNumber
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDealSubMenubyMenuId]    Script Date: 03/28/2014 11:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealSubMenubyMenuId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDealSubMenubyMenuId]
	-- Add the parameters for the stored procedure here
	@DealStepId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SubMenu_Id,Name from SubMenu_Item where Menu_Id=(select Step_Menu_Id from Deal_Detail where Deal_Step_Id=@DealStepId)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDealMenubyCatId]    Script Date: 03/28/2014 11:51:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealMenubyCatId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDealMenubyCatId]
	-- Add the parameters for the stored procedure here
	@DealStepId INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Menu_Id,Name from Menu_Item where Category_Id=(select Step_Category_Id from Deal_Detail where Deal_Step_Id=@DealStepId)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDealInfo]    Script Date: 03/28/2014 11:51:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDealInfo]
	-- Add the parameters for the stored procedure here
	@DealId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Menu_Id,Name,Delivery_Price,Remarks from Menu_Item where Menu_Id=@DealId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDealbyMenuId]    Script Date: 03/28/2014 11:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDealbyMenuId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDealbyMenuId]
	-- Add the parameters for the stored procedure here
	@MenuId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Deal_Step_Id,Step_No,Step_Label,Step_Description,Step_Category_Label,Step_Category_Id,IsFixed,IsTopping,IsDressing,IsSubMenu,SubMenuLabel,SubMenuFixed,SubMenuIndex,Step_Image from Deal_Detail where Menu_Id=@MenuId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerInfo]    Script Date: 03/28/2014 11:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCustomerInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerInfo] 
	-- Add the parameters for the stored procedure here
	@CustomerId int=0,
	@AddressId int=0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Cust.First_Name,Cust.Last_Name,Cust.Telephone,Cust.Mobile,Cust.Email,CustAdd.[Address],CustAdd.City,CustAdd.PostCode from Customer Cust,Customer_Address CustAdd where Cust.Customer_Id=CustAdd.Customer_Id and Cust.Customer_Id=@CustomerId and CustAdd.Address_Id=@AddressId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerAddress]    Script Date: 03/28/2014 11:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCustomerAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,M Imran Rafiq>
-- Create date: <19-12-2010,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerAddress] 
	-- Add the parameters for the stored procedure here
	@CustomerId int=0

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Customer_Id,Address_Id,Address,City,PostCode from Customer_Address where Customer_Id=@CustomerId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetBaseSelection]    Script Date: 03/28/2014 11:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBaseSelection]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetBaseSelection]
	-- Add the parameters for the stored procedure here
	@SelectedIndex INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Base_Id,Name,Price,Topping_Price from Base_Selection where SelectedIndex=@SelectedIndex
END
' 
END
GO

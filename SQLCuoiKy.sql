CREATE TABLE [dbo].[ItemTb1]
(
	IdId Int  Not Null Primary Key Identity,
	ItName Nvarchar(50) Not null,
	ItCat Int Not null,
	ItQty Int Not null,
	ItBprice Int Not null,
	ItSprice Int Not null,
	ItProfit Int Not null,
	ItDetails Nvarchar(100) Not null,
	ItAddDate date Not null
)

go

CREATE TABLE [dbo].[UserTb1]
(
	UId Int  Not Null Primary Key Identity(1000,1),
	UName Nvarchar(50) Not null,
	UEmail Nvarchar(50) Not null,
	UDOB date Not null,
	UGen Nvarchar(10) Not null,
	UPhone Nvarchar(20) Not null,
	UPassword Nvarchar(50) Not null,
)

go

CREATE TABLE [dbo].[CategoryTb1]
(
	Catid Int  Not Null Primary Key Identity,
	CatName Nvarchar(50) Not null
)

go

CREATE TABLE [dbo].[SaleTb1]
(
	SNum Int  Not Null Primary Key Identity(500,1),
	SDate date Not null,
	SCustomer Nvarchar(50) Not null,
	SUser Int Not null,
	SAmount Int Not null,
)

go
/*
ALTER TABLE ItemTb1
ADD Primary Key Clustered ([ItId] ASC);
*/
go
ALTER TABLE ItemTb1
ADD CONSTRAINT [FK1] FOREIGN KEY (ItCat) REFERENCES [CategoryTb1](Catid);

go
ALTER TABLE SaleTb1
ADD CONSTRAINT [FK2] FOREIGN KEY (SUser) REFERENCES [UserTb1](UId);


use MyShopDB
go

--Category data
delete from CATEGORY
go
DBCC CHECKIDENT (CATEGORY, RESEED, 0)
go
insert into CATEGORY values ('Laptop')
insert into CATEGORY values ('Smartphone')
insert into CATEGORY values ('Tablet')
insert into CATEGORY values ('TV')
insert into CATEGORY values ('Smartwatch')
insert into CATEGORY values (N'Máy in')
insert into CATEGORY values ('Laptop')
insert into CATEGORY values ('Camera')
insert into CATEGORY values (N'Sạc dự phòng')
insert into CATEGORY values (N'Bàn phím')

--Product data
delete from PRODUCT
go
DBCC CHECKIDENT (PRODUCT, RESEED, 0)
go
insert into PRODUCT values('Laptop Asus Vivobook X415EA',20,'i3 1115G4/8GB/256GB/Win11',1,NULL)
insert into PRODUCT values(N'Máy tính bảng Honor Pad X9',25,N'Chip:Snapdragon 685 8 nhân',2,NULL)
insert into PRODUCT values('Acer Nitro 5 Gaming AN515',16,'i5 11400H/8GB/512GB/144Hz/4GB GTX1650/Win11',1,NULL)
insert into PRODUCT values('BeFit Sporty 2 Pro',2,'44.8mm Đen',5,NULL)
insert into PRODUCT values('HP LaserJet MFP 135a',4,N'Máy in laser trắng đen đa năng',6,NULL)

insert into PRODUCT values('Baseus ioTa BPE45A',6,N'Trạm sạc dự phòng di động 90000mAh',9,NULL)
insert into PRODUCT values(N'Samsung Galaxy Watch 6',5,N'40mm',5,NULL)
insert into PRODUCT values('Laptop Dell Inspiron 15',11,'i3 1215U/8GB/256GB/OfficeHS/Win11',1,NULL)
insert into PRODUCT values('Samsung Galaxy Tab A9',4,'44.8mm Đen',3,NULL)
insert into PRODUCT values(N' DareU EK807G',1,N'Không Dây, 3 chế độ kết nối',10,NULL)

select *from PRODUCT

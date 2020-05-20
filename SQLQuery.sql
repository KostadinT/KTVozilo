
if not exists (select name  from master.dbo.sysdatabases  where ('[' + name + ']' = 'VoziloDB' or name = 'VoziloDB'))
create database VoziloDB

use VoziloDB

if not exists (select * from sysobjects where name = 'MarkaNaVozilo')
create table MarkaNaVozilo(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	VehicleModel varchar(50) NOT NULL UNIQUE
	)


if not exists (select * from sysobjects where name = 'DataForVehicles')
create table DataForVehicles(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	LicensePlateNumber varchar(50) NOT NULL UNIQUE,
	VIN varchar(50) NOT NULL UNIQUE,
	VehicleModel varchar(50) FOREIGN KEY REFERENCES MarkaNaVozilo(VehicleModel) NOT NULL
)

if not exists (select * from MarkaNaVozilo where VehicleModel = 'Ford Fiesta')
insert into MarkaNaVozilo
values('Ford Fiesta')

if not exists (select * from DataForVehicles where LicensePlateNumber='SK 0000 AB' or VIN='1f1i2b3o5n8a13c21c34i')
insert into DataForVehicles
values('SK 0000 AB','1f1i2b3o5n8a13c21c34i','Ford Fiesta')

if not exists (select * from MarkaNaVozilo where VehicleModel = 'Ford Focus')
insert into MarkaNaVozilo
values('Ford Focus')

if not exists (select * from DataForVehicles where LicensePlateNumber='SK 0000 ZZ' or VIN='i43c12c31a8n5o3b2i1f1')
insert into DataForVehicles
values('SK 0000 ZZ','i43c12c31a8n5o3b2i1f1','Ford Focus')

if not exists (select * from DataForVehicles where LicensePlateNumber='SR 0000 AB' or VIN='1a2b3c4d4c3b2a1')
insert into DataForVehicles
values('SR 0000 AB','1a2b3c4d4c3b2a1','Ford Focus')

if not exists (select * from MarkaNaVozilo where VehicleModel = 'Renault Megane')
insert into MarkaNaVozilo
values('Renault Megane')

if not exists (select * from DataForVehicles where LicensePlateNumber='SR 0000 ZZ' or VIN='k0afjjjw3ok0afjjjw3o')
insert into DataForVehicles
values('SR 0000 ZZ','k0afjjjw3ok0afjjjw3o','Renault Megane')



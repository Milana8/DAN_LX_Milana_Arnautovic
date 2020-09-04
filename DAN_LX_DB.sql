CREATE DATABASE DAN_LX
 IF DB_ID('DAN_LX') IS NULL
CREATE DATABASE DAN_LX;
GO
USE DAN_LX;
--we provided drop tables
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployees')
drop table tblEmployees;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSectors')
drop table tblSectors;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblLocations')
drop table tblLocations;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwLocations')
drop view vwLocations;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwEmployees')
drop view vwEmployees;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwSectors')
drop view vwSectors;







CREATE TABLE tblEmployees(
 EmployeeID int IDENTITY(1,1) Primary key not null, --Primary key
 NameandSurname nvarchar (100) not null,
 JMBG nvarchar(13) not null unique check (JMBG not like '%[^0-9]%') , --jmbg validation
 DateOfBirth date not null check (DateOfBirth<=dateadd(year,(-16),getdate()) ) , --Date of birth validation
 RegistrationNumber varchar(50) check (LEN(RegistrationNumber)=9) unique,
 PhoneNumber nvarchar (50) not null,
 Gender varchar(10) not null,
 Manager int FOREIGN KEY REFERENCES tblEmployees(EmployeeID),
 SectorID int,
 LocationID int,
 

 )

CREATE TABLE tblLocations(
LocationID int identity(1,1) Primary key, --Primary key
Adress nvarchar(50) not null,
Place nvarchar (50) not null,
States nvarchar(50) not null,
)



create table tblSectors(
SectorID int identity (1,1) primary key, -- Primary key
SectorName nvarchar(50) not null,

)



ALTER TABLE tblEmployees
ADD FOREIGN KEY (LocationID) REFERENCES tblLocations(LocationID);

ALTER TABLE tblEmployees
ADD FOREIGN KEY (SectorID) REFERENCES tblSectors(SectorID);

GO
create view vwLocations as
select *, Adress + ', '+ Place + ', ' + States 'Location'
from tblLocations;

GO
create view vwSectors as
select *
from tblSectors;

GO
create view vwEmployees as
select e.*,  s.SectorName, l.Adress +', '+ l.Place +', '+ l.States 'Location', m.NameandSurname 'ManagerName'
from tblEmployees e 
INNER JOIN tblLocations l 
ON e.LocationID = l.LocationID
INNER JOIN tblSectors s
ON e.SectorID = s.SectorID
LEFT JOIN tblEmployees m
ON e.Manager = m.EmployeeID;

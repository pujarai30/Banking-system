Create Database IndiaBank;
Go
use IndiaBank;
Go

--To Create tables
create table Customer
(
CustomerId int primary key,
Firstname varchar(50),
Lastname varchar(50),
Street varchar(50),
City varchar(50),
StateName varchar(50),
Phone decimal(10,0),
Email varchar(100),
RegDate date default getdate()
);

create table Account
(
AccountId int primary key,
CustomerId int foreign key references Customer(CustomerId),
AccountType varchar(10),
AcDescription varchar(50),
balance decimal(10,2)
);

create table Transfers
(
TransferId int primary key identity(1,1),
FromAccountId int foreign key references Account (AccountId),
ToAccountId int foreign key references Account (AccountId),
Amount decimal(10,2),
TranDate date default getdate()
);
create table Transactions
(
TransactionId int primary key identity(1,1),
AccountId int foreign key references Account (AccountId),
balance decimal(10,2),
deposit decimal(10,2),
withdraw decimal(10,2),
TranDate date default getdate()
);

--To view Data

select * from Customer;
select * from Account;
select * from Transactions;
select * from Transfers;


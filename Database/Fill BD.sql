
CREATE PROC Random_Client  /*Клиент*/
@count int 
AS
BEGIN
Declare @FIO varchar(100) = ''
Declare @passport  varchar(10) = ''
Declare @income int
Declare @credit_load float
Declare @age int
Declare @i int = 0
declare @login varchar(50) = ''
declare @password varchar(50) = ''

while (@i < @count)
BEGIN
SET @FIO = CAST((select top (1) a
from(
values 
('Старостин '), ('Виноградов '), ('Рудаков '),
('Семенов '), ('Киселев '), ('Воробьев '),
('Горшков '), ('Орехов '), ('Одинцов '),
('Фокин '), ('Попов '), ('Ильин '),
('Масленников '), ('Богданов '), ('Беляев '),
('Кудрявцев '), ('Постников '), ('Гордеев '),
('Калашников '), ('Емельянов '), ('Симонов '),
('Капустин '), ('Богданов '), ('Ершов '),
('Дроздов '), ('Дементьев '), ('Гущин '),
('Журавлёв '), ('Жуков '), ('Константинов '),
('Ларионов '), ('Лобанов '), ('Миронов '),
('Орлов '), ('Поляков '), ('Савин ')
) as fam(a)
order by abs(checksum(newid()))) as varchar (100))
+ CAST((select top (1) b 
from (
values 
('Владимир '),('Максим '),('Егор '),
('Матвей '),('Михаил '),('Олег '),
('Игорь '),('Егор '),('Александр '),
('Константин '),('Иван '),('Данил '),
('Кирилл '),('Денис '),('Артём '),
('Вадим '),('Илья '),('Давид '),
('Глеб '),('Валерий '),('Валерий '),
('Альберт '),('Захар '),('Мирослав '),
('Павел '),('Роман '),('Тихон '),
('Тимофей '),('Вадим '),('Евгений '),
('Егор '),('Леонид '),('Максим ')
)as im(b)
ORDER BY ABS(CHECKSUM(NewId()))) AS VARCHAR(100))
+CAST((select top (1) c
from ( 
values 
('Ярославович'),('Михайлович'),('Фёдорович'),
('Святославович'),('Дмитриевич'),('Максимович'),
('Егорович'),('Олегович'),('Данилович'),
('Романович'),('Алексеевич'),('Александрович'),
('Иванович'),('Федорович'),('Владиславович'),
('Вадимович'),('Афанасьевич'),('Игоревич'),
('Альбертович'),('Анатолиевич'),('Артурович'),
('Артурович'),('Богданович'),('Вадимович'),
('Васильевич'),('Викторович'),('Гаврилович'),
('Григорьевич'),('Денисович'),('Евгениевич'),
('Егорович'),('Корнеевич'),('Леонидович')
) as otchestvo(c)
ORDER BY ABS(CHECKSUM(NewId()))) AS VARCHAR(100))

SET @passport = SUBSTRING(CAST(((ABS(CHECKSUM(Round(RAND()*(19-0)+0, 0))))) AS VARCHAR(30)),1,19)
SET @income = ABS(CHECKSUM(NewId())) % 80 + 20;
SET @credit_load = ABS(CHECKSUM(NewId())) % 30.01 /*рандом или КН = (Долг/Доход)*100%*/
SET @age = ABS(CHECKSUM(NewId())) % 60 + 18;
set @login = (char(ascii('A')+(Abs(Checksum(NewId()))%25)) +
       char(ascii('a')+(Abs(Checksum(NewId()))%25)) +
	   char(ascii('a')+(Abs(Checksum(NewId()))%25)) +
       char(ascii('A')+(Abs(Checksum(NewId()))%25)) +
	   char(ascii('a')+(Abs(Checksum(NewId()))%25)) +
       char(ascii('A')+(Abs(Checksum(NewId()))%25)) +
       left(newid(),5)+'@gmail.com')
set @password = substring(replace(newID(),'-',''),cast(RAND()*(31-10) as int),10)
--SELECT SUBSTRING(CONVERT(varchar(40), NEWID()),0,9) 
--print left(replace(newid(),'-',''),10)
--select substring(replace(newID(),'-',''),cast(RAND()*(31-10) as int),10)
INSERT INTO Client(FIO,passport,income,credit_load,age,login, password) VALUES (@FIO,@passport,@income,@credit_load,@age, @login, @password) 
SET @i = @i + 1
END
END


CREATE PROC Random_Deposit /*Депозит*/
@count int
AS
BEGIN
DECLARE 
@id_client int,
@id_typeDeposit int,
--@max_client int,
--@max_typeDeposit int,
@Summ float,
@minS int,
@maxS int = 3500000,
@i int = 0
--Select @max_client = MAX(id_client) from Client
--Select @max_typeDeposit = MAX(id_typeDeposit) from TypeDeposit
Select @minS = MAX(min_sum) from TypeDeposit
WHILE (@i < @count)
BEGIN
--SET @id_client = 1 + ABS(CHECKSUM(NewId())) % @max_client
SET @id_client = (select top 1 id_client from Client order by NewId())
--SET @id_typeDeposit = 1 + ABS(CHECKSUM(NewId())) % @max_typeDeposit
SET @id_typeDeposit = (select top 1 id_typeDeposit from TypeDeposit order by NewId())
SET @Summ = 1 + ABS(CHECKSUM(NewId())) % @maxS + @minS

INSERT INTO Deposit(Summ, id_client, id_typeDeposit) VALUES (@Summ, @id_client, @id_typeDeposit)
SET @i = @i + 1
END
END


CREATE PROC Random_PaymentProc /*Порядок выплат*/
AS
BEGIN
	DECLARE 
		@id_typeDep int = 0,
		--@max_typeDep int,
		@Payment_proc int,
		@term int,
		@i int = 0,
		@count int,
		@min_id_type int,
		@sch int = 0, 
		@rand int
	set @count = (select count(*) from TypeDeposit)
	--Select @max_typeDep = MAX(id_typeDeposit) from TypeDeposit
	set @min_id_type = (select MIN(id_typeDeposit) from TypeDeposit)
	WHILE (@i < @count)
	BEGIN
		set @term = (select term_in_months from TypeDeposit where id_typeDeposit=@i)
		--SET @id_typeDep = 1 + ABS(CHECKSUM(NewId())) % @max_typeDep
		SET @id_typeDep +=@min_id_type
		SET @Payment_proc = (select top (1) a from(values (@term*30), (90), (180), (30) ) as sr(a) order by (newid()))
		INSERT INTO PaymentProcedure(id_typeDeposit, Payment_procedure) VALUES (@id_typeDep, @Payment_proc)
		SET @i = @i + 1
	END
END

--drop proc Random_PaymentProc
--delete from PaymentProcedure where id_PaymentProcedure > 0
--exec Random_PaymentProc


--select * from DepositDateChangeStatus
/*Операции по вкладу*/
create proc Open_deposit
@ind int,
@begin datetime,
@razn int,
@perc float,
@date datetime output,
@Sum int output
AS
BEGIN
	declare
		@id_kindOfAct int,
		@maxPurpose int = 5,
		@Summ float,
		@percent float,
		@nameOp varchar(20),
		@randNum int
	Set @nameOp = 'Открыть вклад'
	Set @id_kindOfAct = (select id_kindOfAction from KindOfAction where name=@nameOp)
	Set @Summ = (Select Summ from Deposit where id_deposit=@ind)
	Set @Sum = @Summ
	SET @randNum = 1 + ABS(CHECKSUM(NewId())) % @razn
	SET @date = DATEADD(day, @randNum, @begin)
	INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
END


create proc Other_operations_depos
@ind int,
@begin datetime,
@perc float,
@Sum int output
AS
BEGIN
	declare
		@id_kindOfAct int,
		@maxPurpose int = 5,
		@Summ float,
		@percent float,
		@nameOp varchar(20),
		@randNum int = 0,
		@withdrawal bit,
		@depositing bit,
		@maxS int = 100000,
		@date datetime,
		@term int,
		@paiment int
	set @term = (select term_in_months-2 from TypeDeposit inner join deposit on deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where deposit.id_deposit=@ind)
	set @depositing = (select depositing_funds from TypeDeposit inner join deposit on deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where depositing_funds=1 and deposit.id_deposit=@ind)
	set @withdrawal = (select withdrawal_funds from TypeDeposit inner join deposit on deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where withdrawal_funds=1 and deposit.id_deposit=@ind)
	set @paiment = (select Payment_procedure from PaymentProcedure inner join TypeDeposit on TypeDeposit.id_typeDeposit=PaymentProcedure.id_typeDeposit inner join Deposit on Deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where Deposit.id_deposit=@ind)
	While (@randNum <= (@term+2))
	begin
		--Set @nameOp = (select top (1) a from (values ('Перевести полученные проценты на основной счет'),('Перевести полученные проценты на другой счет')) as nameOp(a) order by newid())
		Set @id_kindOfAct = (select top (1) a from (values (5),(6)) as nameOp(a) order by newid())
		Set @Summ = round((@Sum*@perc)/100, 1)
		Set @randNum += @paiment
		Set @date = DATEADD(day, @randNum, @begin)
		INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
		print @id_kindOfAct
		print ' 3 выполнилось'
	end
	while ((select ABS(CHECKSUM(NewId())) % 3)<>0)
	begin
		if (@depositing=1 and (select ABS(CHECKSUM(NewId())) % 2)<>0)
		begin
			Set @nameOp = 'Положить деньги'
			Set @id_kindOfAct = (select id_kindOfAction from KindOfAction where name=@nameOp)
			Set @Summ = 1 + ABS(CHECKSUM(NewId())) % @maxS
			Set @Sum += @Summ
			Set @randNum = 1 + ABS(CHECKSUM(NewId())) % (@term*30)
			Set @date = DATEADD(day, @randNum, @begin)
			INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
			print @id_kindOfAct
			print ' 1 выполнилось'
		end
		else if (@withdrawal=1 and (select ABS(CHECKSUM(NewId())) % 2)<>0)
		begin
			Set @nameOp = 'Снять деньги'
			--Set @id_kindOfAct = (select id_kindOfAction from KindOfAction where name=@nameOp)
			Set @id_kindOfAct = 3
			Set @Summ = -1 +(-ABS(CHECKSUM(NewId())) % @maxS)
			Set @Sum += -@Summ
			Set @randNum = 1 + ABS(CHECKSUM(NewId())) % (@term*30)
			Set @date = DATEADD(day, @randNum, @begin)
			INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
			print @id_kindOfAct
			print ' 2 выполнилось'
		end
		/*else --if ((select ABS(CHECKSUM(NewId())) % 2)<>0)
		begin
			--Set @nameOp = (select top (1) a from (values ('Перевести полученные проценты на основной счет'),('Перевести полученные проценты на другой счет')) as nameOp(a) order by newid())
			Set @id_kindOfAct = (select top (1) a from (values (5),(6)) as nameOp(a) order by newid())
			Set @Summ = round((@Sum*@perc)/100, 1)
			if (@randNum < (@term+2))
				Set @randNum = 1 + ABS(CHECKSUM(NewId())) % (@term)
			Set @date = DATEADD(month, @randNum, @begin)
			INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
			print @id_kindOfAct
			print ' 3 выполнилось'
		end*/
	end
END

create proc Close_deposit
@ind int,
@begin datetime,
@perc float,
@Sum int
AS
BEGIN
	declare
		@id_kindOfAct int,
		@maxPurpose int = 5,
		@Summ float,
		@percent float,
		@nameOp varchar(20),
		@randNum int,
		@date datetime,
		@currentDate datetime
	Set @nameOp = 'Закрыть вклад'
	Set @currentDate = GETDATE()
	Set @id_kindOfAct = (select id_kindOfAction from KindOfAction where name=@nameOp)
	print @Sum
	Set @Summ = @Sum
	SET @randNum = (select term_in_months from TypeDeposit inner join deposit on deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where (Deposit.id_deposit=@ind and deposit.id_typeDeposit=TypeDeposit.id_typeDeposit))
	SET @date = DATEADD(month, @randNum, @begin)
	if (@date > @currentDate)
		return
	else
	begin
		INSERT INTO DepositTransaction(Summ, date, id_deposit, id_kindOfAction) VALUES (@Summ, @date, @ind, @id_kindOfAct)
		print ' Выполнилось закрытие++++++'
	end
END


CREATE PROC Random_DepositTransaction_main /*Операции по вкладу (вызов процедур)*/
AS
BEGIN
	declare
		@count int = (select MAX(id_deposit) from Deposit),
		@id_deposit int = 0,
		@percent float,
		@result_date datetime,
		@i int = 0,
		@min_id_dep int,
		@begin1 datetime = '2015-01-01', @end1 datetime = '2021-05-01',
		@razn1 int,
		@final_summ int
	set @min_id_dep = (select MIN(id_deposit) from Deposit)
	SET @razn1 = DATEDIFF(day,@begin1,@end1)
	WHILE (@i < @count)
	BEGIN
		set @id_deposit += @min_id_dep
		set @percent = (select percent_rate from TypeDeposit inner join deposit on deposit.id_typeDeposit=TypeDeposit.id_typeDeposit where (Deposit.id_deposit=@id_deposit and deposit.id_typeDeposit=TypeDeposit.id_typeDeposit))
		exec Open_deposit @id_deposit, @begin1, @razn1, @percent, @result_date output, @final_summ output	
		exec Other_operations_depos  @id_deposit, @result_date, @percent, @final_summ output	
		exec Close_deposit @id_deposit, @result_date, @percent, @final_summ	
		SET @i = @i + 1
	END
END



CREATE PROC Random_DepChangeStat /*Дата изменения статуса*/
AS
BEGIN
DECLARE 
@id_deposit int = 0,
@id_depositStatus int,
@max_deposit int,
@max_status int,
@date datetime,
@min_id_dep int,
@i int = 0,
@count int
set @count = (select count(*) from Deposit)
set @min_id_dep = (select MIN(id_deposit) from Deposit)
WHILE (@i < @count)
BEGIN
set @id_deposit += @min_id_dep
SET @id_depositStatus = 1
SET @date = (select date from DepositTransaction where id_deposit=@id_deposit and id_kindOfAction=1)
INSERT INTO DepositDateChangeStatus(date,id_deposit, id_depositStatus) VALUES (@date, @id_deposit, @id_depositStatus)
if (select count(id_kindOfAction) from DepositTransaction where id_deposit=@id_deposit and id_kindOfAction=2)=1
begin
	SET @id_depositStatus = 2
	SET @date = (select date from DepositTransaction where id_deposit=@id_deposit and id_kindOfAction=2)
	INSERT INTO DepositDateChangeStatus(date,id_deposit, id_depositStatus) VALUES (@date, @id_deposit, @id_depositStatus)
end
SET @i = @i + 1
END
END

/*
drop procedure Open_deposit
drop procedure Close_deposit
drop procedure Other_operations_depos
drop procedure Random_DepositTransaction_main
*/
/*Конец кода по заполнению операций*/
--drop proc Random_DepChangeStat

--delete from DepositTransaction where id_depositTransaction>0
--delete from DepositDateChangeStatus where id_depositDateChangeDate>0

select * from DepositTransaction

insert into KindOfAction
select name
from KindOfAct$;

insert into DepositStatus
select status
from DepositStatus$

insert into TypeDeposit
select name, percent_rate, minSum, maxSum, termMonth, depositing_funds, withdrawal_funds     
from dbo.TypeDep$

--select * from Deposit

--delete from TypeDeposit where 

exec Random_Client @count = 10000

exec Random_Deposit @count = 10000

exec Random_PaymentProc

exec Random_DepositTransaction_main

exec Random_DepChangeStat

--select * from DepositDateChangeStatus

select * from DepositTransaction


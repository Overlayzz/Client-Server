create database Bank

CREATE TABLE Client
( 
	id_client            integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	FIO                  varchar(100)  NOT NULL ,
	passport             varchar(50)  NOT NULL ,
	income               integer  NOT NULL ,
	credit_load          float  NULL ,
	age                  integer  NOT NULL,
	login 			 varchar(50)  NOT NULL ,
	password             varchar(50)  NOT NULL
)
go



CREATE TABLE Credit
( 
	id_credit            integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	Summ                  integer  NULL ,
	main_debt            float  NULL ,
	id_client            integer  NOT NULL,
	term                 integer  NULL ,
	id_typeCredit        integer  NOT NULL 
)
go



CREATE TABLE CreditDateChangeStatus
( 
	id_creditDateChangeStatus integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	date                 datetime  NULL ,
	id_credit            integer  NOT NULL ,
	id_creditStatus      integer  NOT NULL 
)
go



CREATE TABLE CreditStatus
( 
	id_creditStatus      integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	status               varchar(50)  NULL 
)
go



CREATE TABLE CreditTransaction
( 
	id_creditTransaction integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	Summ                 float NULL ,
	date                 datetime  NULL ,
	id_credit            integer  NOT NULL ,
	id_typeCreditPayment integer  NOT NULL
)
go



CREATE TABLE Deposit
( 
	id_deposit           integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	Summ                  float  NOT NULL ,
	id_client            integer  NOT NULL,
	id_typeDeposit       integer  NOT NULL 
)
go




CREATE TABLE DepositDateChangeStatus
( 
	id_depositDateChangeDate integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	date                 datetime  NOT NULL ,
	id_deposit           integer  NOT NULL ,
	id_depositStatus     integer  NOT NULL 
)
go



CREATE TABLE DepositStatus
( 
	id_depositStatus     integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	status               varchar(50)  NULL 
)
go



CREATE TABLE DepositTransaction
( 
	id_depositTransaction integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	Summ                  float  NOT NULL ,
	date                 datetime  NOT NULL ,
	id_deposit           integer  NOT NULL,
	id_kindOfAction  	integer  NOT NULL
)
go



CREATE TABLE House
( 
	id_house             integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	cost                 integer  NULL ,
	category             bit  NULL 
)
go



CREATE TABLE InsurenceCredit
( 
	id_insurenceCredit   integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	id_typeInsurenceCredit integer  NOT NULL ,
	id_credit            integer  NOT NULL 
)
go



CREATE TABLE InsurenceMortgage
( 
	id_insurenceMortgage  integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	id_mortgage          integer  NOT NULL ,
	id_tyoeInsurenceMortgage integer  NOT NULL 
)
go



CREATE TABLE KindOfAction
( 
	id_kindOfAction      integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	name                 varchar(100)  NULL 
)
go



CREATE TABLE MortgageDateChangeDate
( 
	id_MortgageDateChangeDate integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	id_mortgage          integer  NOT NULL ,
	id_MortgageStatus     integer  NOT NULL ,
	date                 datetime  NULL 
)
go



CREATE TABLE MortgageStatus
( 
	id_MortgageStatus     integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	status               varchar(50)  NULL 
)
go



CREATE TABLE MortgageTransaction
( 
	id_MortgageTransaction integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	id_mortgage          integer  NOT NULL ,
	id_typeMortgagePayment integer  NOT NULL ,
	summ                  integer  NULL ,
	date                 datetime  NULL 
)
go



CREATE TABLE Mortgage
( 
	id_mortgage          integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	current_percent_rate float  NULL ,
	summ                  integer  NULL ,
	main_debt            float  NULL ,
	id_client            integer  NOT NULL,
	id_privileges        integer  NULL ,
	id_house             integer  NOT NULL ,
	id_typeMortgage       integer  NOT NULL ,
	initial_Payment      integer  NULL ,
	term                 integer  NULL 
)
go



CREATE TABLE PaymentProcedure
( 
	id_PaymentProcedure  integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	id_typeDeposit       integer  NOT NULL ,
	Payment_procedure    integer  NULL 
)
go



CREATE TABLE Privileges
( 
	id_privileges        integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	privilege            varchar(100)  NULL ,
	decrease_percent_rate float  NULL ,
)
go



CREATE TABLE TypeCreditPayment
( 
	id_typeCreditPayment integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	purpose_Payment      bit  NULL 
)
go



CREATE TABLE TypeDeposit
( 
	id_typeDeposit       integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	name                 varchar(100)  NOT NULL ,
	percent_rate         float  NOT NULL ,
	min_sum              integer  NULL ,
	max_sum              integer  NULL ,
	term_in_months       integer  NULL ,
	depositing_funds     bit  NOT NULL ,
	withdrawal_funds     bit  NOT NULL
)
go



CREATE TABLE TypeInsurenceCredit
( 
	id_typeInsurenceCredit integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	insurence            varchar(100)  NULL ,
	decrease_percent_rate integer  NULL 
)
go



CREATE TABLE TypeInsurenceMortgage
( 
	id_tyoeInsurenceMortgage integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	insurence            varchar(100)  NULL ,
	decrease_percent_rate integer  NULL 
)
go



CREATE TABLE TypeMortgage
( 
	id_typeMortgage       integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	name                 varchar(100)  NULL ,
	base_percent_rate    float  NULL ,
	term_in_months       integer  NULL 
)
go



CREATE TABLE TypeMortgagePayment
( 
	id_typeMortgagePayment integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	type_Payment         varchar(50)  NULL 
)
go



CREATE TABLE Type—redit
( 
	id_typeCredit        integer  PRIMARY KEY  NOT NULL IDENTITY(1,1),
	name                 varchar(100)  NULL ,
	percent_rate         float  NULL ,
	refund_sum           integer  NULL ,
	credit_category      varchar(100)  NULL 
)
go



ALTER TABLE Credit
	ADD FOREIGN KEY (id_client) REFERENCES Client(id_client)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Credit
	ADD FOREIGN KEY (id_typeCredit) REFERENCES Type—redit(id_typeCredit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE CreditDateChangeStatus
	ADD FOREIGN KEY (id_credit) REFERENCES Credit(id_credit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE CreditDateChangeStatus
	ADD FOREIGN KEY (id_creditStatus) REFERENCES CreditStatus(id_creditStatus)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE CreditTransaction
	ADD FOREIGN KEY (id_credit) REFERENCES Credit(id_credit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE CreditTransaction
	ADD FOREIGN KEY (id_typeCreditPayment) REFERENCES TypeCreditPayment(id_typeCreditPayment)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Deposit
	ADD FOREIGN KEY (id_client) REFERENCES Client(id_client)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Deposit
	ADD FOREIGN KEY (id_typeDeposit) REFERENCES TypeDeposit(id_typeDeposit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE DepositDateChangeStatus
	ADD FOREIGN KEY (id_deposit) REFERENCES Deposit(id_deposit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE DepositDateChangeStatus
	ADD FOREIGN KEY (id_depositStatus) REFERENCES DepositStatus(id_depositStatus)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE DepositTransaction
	ADD FOREIGN KEY (id_deposit) REFERENCES Deposit(id_deposit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
Go


ALTER TABLE DepositTransaction
	ADD FOREIGN KEY (id_kindOfAction) REFERENCES KindOfAction(id_kindOfAction)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go





ALTER TABLE InsurenceCredit
	ADD FOREIGN KEY (id_typeInsurenceCredit) REFERENCES TypeInsurenceCredit(id_typeInsurenceCredit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE InsurenceCredit
	ADD FOREIGN KEY (id_credit) REFERENCES Credit(id_credit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE InsurenceMortgage
	ADD FOREIGN KEY (id_mortgage) REFERENCES Mortgage(id_mortgage)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE InsurenceMortgage
	ADD FOREIGN KEY (id_tyoeInsurenceMortgage) REFERENCES TypeInsurenceMortgage(id_tyoeInsurenceMortgage)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE MortgageDateChangeDate
	ADD FOREIGN KEY (id_mortgage) REFERENCES Mortgage(id_mortgage)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE MortgageDateChangeDate
	ADD FOREIGN KEY (id_MortgageStatus) REFERENCES MortgageStatus(id_MortgageStatus)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE MortgageTransaction
	ADD FOREIGN KEY (id_mortgage) REFERENCES Mortgage(id_mortgage)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE MortgageTransaction
	ADD FOREIGN KEY (id_typeMortgagePayment) REFERENCES TypeMortgagePayment(id_typeMortgagePayment)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Mortgage
	ADD FOREIGN KEY (id_client) REFERENCES Client(id_client)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Mortgage
	ADD FOREIGN KEY (id_privileges) REFERENCES Privileges(id_privileges)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Mortgage
	ADD FOREIGN KEY (id_house) REFERENCES House(id_house)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Mortgage
	ADD FOREIGN KEY (id_typeMortgage) REFERENCES TypeMortgage(id_typeMortgage)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE PaymentProcedure
	ADD FOREIGN KEY (id_typeDeposit) REFERENCES TypeDeposit(id_typeDeposit)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go


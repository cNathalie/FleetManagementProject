
USE FleetManagementDB;

CREATE TABLE [dbo].[TypeRijbewijs](

	[typeRijbewijsId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,
	
	CONSTRAINT PK_TypeRijbewijs PRIMARY KEY (typeRijbewijsId)
);


CREATE TABLE [dbo].[Bestuurder](

	[bestuurderId] [int] IDENTITY(1,1) NOT NULL,
	[naam] [varchar](30) NOT NULL,
	[voornaam] [varchar](30) NOT NULL,
	[adres] [varchar](100) NOT NULL,
	[rijksregisternummer] [varchar](30) NOT NULL,
	[typerijbewijsId] [int] NOT NULL,
	
	CONSTRAINT PK_Bestuurder PRIMARY KEY (bestuurderId),
	CONSTRAINT FK_Bestuurder_TypeRijbewijs FOREIGN KEY (typerijbewijsId) REFERENCES TypeRijbewijs(typeRijbewijsId)
);


CREATE TABLE [dbo].[BrandstofType](

	[brandstofTypeId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,
	
	CONSTRAINT PK_BrandstofType PRIMARY KEY (brandstofTypeId)
);



CREATE TABLE [dbo].[Tankkaart](

	[tankkaartId] [int] IDENTITY(1,1) NOT NULL,
	[kaartnummer] [int] NOT NULL,
	[geldigheidsdatum] [datetime] NOT NULL,
	[pincode] [int] NOT NULL,
	[brandstofTypeId] [int] NOT NULL,
	[actief] [bit] NULL DEFAULT 1,
	
	CONSTRAINT PK_Tankkaart PRIMARY KEY (tankkaartId),
	CONSTRAINT FK_Tankkaart_BrandstofType FOREIGN KEY (brandstofTypeId) REFERENCES BrandstofType(brandstofTypeId)
	
);



CREATE TABLE [dbo].[TypeWagen](

	[typeWagenId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,
	
	CONSTRAINT PK_TypeWagen PRIMARY KEY (typeWagenId)
);



CREATE TABLE [dbo].[Voertuig](

	[voertuigId] [int] IDENTITY(1,1) NOT NULL,
	[merkEnModel] [varchar](30) NOT NULL,
	[chassisnummer] [varchar](30) NOT NULL,
	[nummerplaat] [varchar](30) NOT NULL,
	[brandstofTypeId] [int] NOT NULL,
	[typeWagenId] [int] NOT NULL,
	[kleur] [varchar](30) NOT NULL,
	[aantal_deuren] [int] NOT NULL,

	CONSTRAINT PK_Voertuig PRIMARY KEY (voertuigId),
	CONSTRAINT FK_Voertuig_BrandstofType FOREIGN KEY (brandstofTypeId) REFERENCES BrandstofType(brandstofTypeId),
	CONSTRAINT FK_Voertuig_TypeWagen FOREIGN KEY (typeWagenId) REFERENCES TypeWagen(typeWagenId)
);
	




CREATE TABLE [dbo].[Fleet](

	[fleetId] [int] IDENTITY(1,1) NOT NULL,
	[bestuurderId] [int] NOT NULL,
	[tankkaartId] [int] NOT NULL,
	[voertuigId] [int] NOT NULL,
	
	CONSTRAINT PK_Fleet PRIMARY KEY (fleetId),
	CONSTRAINT FK_Fleet_Bestuurder FOREIGN KEY (bestuurderId) REFERENCES Bestuurder(bestuurderId),
	CONSTRAINT FK_Fleet_Tankkaart FOREIGN KEY (tankkaartId) REFERENCES Tankkaart(tankkaartId),
	CONSTRAINT FK_Fleet_Voertuig FOREIGN KEY (voertuigId) REFERENCES Voertuig(voertuigId),

	CONSTRAINT UK_Fleet_BTV UNIQUE (bestuurderId,tankkaartId, voertuigId),
	CONSTRAINT UK_Fleet_B UNIQUE (bestuurderId),
	CONSTRAINT UK_Fleet_T UNIQUE (tankkaartId),
	CONSTRAINT UK_Fleet_V UNIQUE (voertuigId)

);


CREATE TABLE [dbo].[Login] (

	loginId int IDENTITY(1,1) NOT NULL,
	email varchar(50) NOT NULL,
	wachtwoord varchar(20) NOT NULL,
	rol varchar(10) NOT NULL DEFAULT 'User',
	
	CONSTRAINT PK_Login PRIMARY KEY (loginId)
);
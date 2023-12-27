
USE FleetManagement007;

CREATE TABLE [dbo].[TypeRijbewijs](

	[typeRijbewijsId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,
	
	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0

	CONSTRAINT PK_TypeRijbewijs PRIMARY KEY (typeRijbewijsId)
);


CREATE TABLE [dbo].[Bestuurder](

	[bestuurderId] [int] IDENTITY(1,1) NOT NULL,
	[naam] [varchar](30) NOT NULL,
	[voornaam] [varchar](30) NOT NULL,
	[adres] [varchar](100) NOT NULL,
	[rijksregisternummer] [varchar](30) NOT NULL,
	[typerijbewijsId] [int] NOT NULL,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0
	
	CONSTRAINT PK_Bestuurder PRIMARY KEY (bestuurderId),
	CONSTRAINT FK_Bestuurder_TypeRijbewijs FOREIGN KEY (typerijbewijsId) REFERENCES TypeRijbewijs(typeRijbewijsId)
);


CREATE TABLE [dbo].[BrandstofType](

	[brandstofTypeId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0
	
	CONSTRAINT PK_BrandstofType PRIMARY KEY (brandstofTypeId)
);



CREATE TABLE [dbo].[Tankkaart](

	[tankkaartId] [int] IDENTITY(1,1) NOT NULL,
	[kaartnummer] [int] NOT NULL,
	[geldigheidsdatum] [datetime] NOT NULL,
	[pincode] [int] NOT NULL,
	[brandstofTypeId] [int] NOT NULL,
	[actief] [bit] NULL DEFAULT 1,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0
	
	CONSTRAINT PK_Tankkaart PRIMARY KEY (tankkaartId),
	CONSTRAINT FK_Tankkaart_BrandstofType FOREIGN KEY (brandstofTypeId) REFERENCES BrandstofType(brandstofTypeId),
	CONSTRAINT UQ_Tankaart_Kaartnummer UNIQUE (kaartnummer)
	
);



CREATE TABLE [dbo].[TypeWagen](

	[typeWagenId] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0
	
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

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0

	CONSTRAINT PK_Voertuig PRIMARY KEY (voertuigId),
	CONSTRAINT FK_Voertuig_BrandstofType FOREIGN KEY (brandstofTypeId) REFERENCES BrandstofType(brandstofTypeId),
	CONSTRAINT FK_Voertuig_TypeWagen FOREIGN KEY (typeWagenId) REFERENCES TypeWagen(typeWagenId),
	CONSTRAINT FK_Voertuig_Chassisnummer UNIQUE (chassisnummer),
	CONSTRAINT FK_Voertuig_Nummerplaat UNIQUE (nummerplaat)
);
	

CREATE TABLE [dbo].[FleetMember](

	[fleetId] [int] IDENTITY(1,1) NOT NULL,
	[bestuurderId] [int] NOT NULL,
	[tankkaartId] [int] NOT NULL,
	[voertuigId] [int] NOT NULL,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0
	
	CONSTRAINT PK_Fleet PRIMARY KEY (fleetId),
	CONSTRAINT FK_Fleet_Bestuurder FOREIGN KEY (bestuurderId) REFERENCES Bestuurder(bestuurderId),
	CONSTRAINT FK_Fleet_Tankkaart FOREIGN KEY (tankkaartId) REFERENCES Tankkaart(tankkaartId),
	CONSTRAINT FK_Fleet_Voertuig FOREIGN KEY (voertuigId) REFERENCES Voertuig(voertuigId)
);

-- Unique filtered index for unique combinations (bestuurderId, tankkaartId, voertuigId) where IsDeleted is false
CREATE UNIQUE NONCLUSTERED INDEX IX_Fleet_UniqueCombinations 
    ON [dbo].[FleetMember] (bestuurderId, tankkaartId, voertuigId) 
    WHERE IsDeleted = 0;


CREATE TABLE [dbo].[User] (

	UserId int IDENTITY(1,1) NOT NULL,
	Email varchar(50) NOT NULL,
	PasswordSalt varchar(max) NOT NULL,
	PasswordHash varchar(max) NOT NULL,
	[Role] varchar(10) NOT NULL DEFAULT 'User',
	
	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0

	CONSTRAINT PK_Login PRIMARY KEY (userId)
);

CREATE TABLE [dbo].[UserRefreshToken] (

	RefreshTokenId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	RefreshToken varchar(max) NOT NULL,
	IsActive bit NOT NULL DEFAULT 1,

	AUTO_TIME_CREATION DateTime2(7) NOT NULL DEFAULT GETDATE(),
	AUTO_TIME_UPDATE DateTime2(7) NOT NULL DEFAULT GETDATE(),	
	AUTO_UPDATE_COUNT INT NOT NULL DEFAULT 0,

	IsDeleted BIT NOT NULL DEFAULT 0

	CONSTRAINT PK_RefreshToken PRIMARY KEY (RefreshTokenId),
	CONSTRAINT FK_RefreshToken_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
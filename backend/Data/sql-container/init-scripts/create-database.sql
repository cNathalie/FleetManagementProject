USE master;
GO

CREATE DATABASE FleetManagement007;
GO

USE FleetManagement007;
GO

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

USE FleetManagement007
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
-- Unique filtered index for unique combinations (bestuurderId, tankkaartId, voertuigId) where IsDeleted is false
CREATE UNIQUE NONCLUSTERED INDEX IX_Fleet_UniqueCombinations 
    ON [dbo].[FleetMember] (bestuurderId, tankkaartId, voertuigId) 
    WHERE IsDeleted = 0;

USE FleetManagement007
GO

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


USE FleetManagement007
GO

-- TypeRijbewijs
CREATE TRIGGER TRG_TypeRijbewijs_Update
ON [dbo].[TypeRijbewijs]
AFTER INSERT, UPDATE
AS
BEGIN
if update(typeRijbewijsId) or update([type]) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE tr
    SET tr.AUTO_TIME_UPDATE = GETDATE(),
        tr.AUTO_UPDATE_COUNT = tr.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN TypeRijbewijs tr
    ON tr.typeRijbewijsId = d.typeRijbewijsId
END
END
GO

-- Bestuurder
CREATE TRIGGER TRG_Bestuurder_Update
ON [dbo].[Bestuurder]
AFTER INSERT, UPDATE
AS
BEGIN
if update(bestuurderId) or update(naam) or update(voornaam) or update(adres) or update(rijksregisternummer) or update(typerijbewijsId) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE b
    SET b.AUTO_TIME_UPDATE = GETDATE(),
        b.AUTO_UPDATE_COUNT = b.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN Bestuurder b
    ON b.bestuurderId = d.bestuurderId
END
END
GO

-- BrandstofType
CREATE TRIGGER TRG_BrandstofType_Update
ON [dbo].[BrandstofType]
AFTER INSERT, UPDATE
AS
BEGIN
if update(brandstofTypeId) or update([type]) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE b
    SET b.AUTO_TIME_UPDATE = GETDATE(),
        b.AUTO_UPDATE_COUNT = b.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN BrandstofType b
    ON b.brandstofTypeId = d.brandstofTypeId
END
END
GO

-- Tankkaart
CREATE TRIGGER TRG_Tankkaart_Update
ON [dbo].[Tankkaart]
AFTER INSERT, UPDATE
AS
BEGIN
if update(tankkaartId) or update(kaartnummer) or update(geldigheidsdatum) or update(pincode) or update(brandstofTypeId) or update(actief) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE t
    SET t.AUTO_TIME_UPDATE = GETDATE(),
        t.AUTO_UPDATE_COUNT = t.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN Tankkaart t
    ON t.tankkaartId = d.tankkaartId
END
END
GO

-- TypeWagen
CREATE TRIGGER TRG_TypeWagen_Update
ON [dbo].[TypeWagen]
AFTER INSERT, UPDATE
AS
BEGIN
if update(typeWagenId) or update([type]) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE tw
    SET tw.AUTO_TIME_UPDATE = GETDATE(),
        tw.AUTO_UPDATE_COUNT = tw.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN TypeWagen tw
    ON tw.typeWagenId = d.typeWagenId
END
END
GO


-- Voertuig
CREATE TRIGGER TRG_Voertuig_Update
ON [dbo].[Voertuig]
AFTER INSERT, UPDATE
AS
BEGIN
if update(voertuigId) or update(merkEnModel) or update(chassisnummer) or update(nummerplaat) or update(brandstofTypeId) or update(typeWagenId) or update(kleur)  or update(aantal_deuren) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE v
    SET v.AUTO_TIME_UPDATE = GETDATE(),
        v.AUTO_UPDATE_COUNT = v.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN Voertuig v
    ON v.voertuigId = d.voertuigId
END
END
GO

-- FleetMember
CREATE TRIGGER TRG_FleetMember_Update
ON [dbo].[FleetMember]
AFTER INSERT, UPDATE
AS
BEGIN
if update(fleetId) or update(bestuurderId) or update(tankkaartId) or update(voertuigId) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE fm
    SET fm.AUTO_TIME_UPDATE = GETDATE(),
        fm.AUTO_UPDATE_COUNT = fm.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN FleetMember fm
    ON fm.fleetId = d.fleetId
END
END
GO

-- User
CREATE TRIGGER TRG_User_Update
ON [dbo].[User]
AFTER INSERT, UPDATE
AS
BEGIN
if update(UserId) or update(Email) or update(PasswordSalt) or update(PasswordHash) or update(Role) or update(IsDeleted) or exists (select * from deleted)
BEGIN
    UPDATE u
    SET u.AUTO_TIME_UPDATE = GETDATE(),
        u.AUTO_UPDATE_COUNT = u.AUTO_UPDATE_COUNT + 1
    FROM deleted d
    INNER JOIN [dbo].[User] u
    ON u.UserId = d.UserId
END
END
GO


-- UserRefreshToken
CREATE TRIGGER TRG_UserRefreshToken_Update
ON UserRefreshToken
AFTER INSERT, UPDATE
AS
BEGIN
if update(RefreshTokenId) or update(UserId) or update(RefreshToken) or update(IsActive) or update(IsDeleted) or exists (select * from deleted)
BEGIN
	UPDATE urt
	SET urt.AUTO_TIME_UPDATE = GETDATE(),
		urt.AUTO_UPDATE_COUNT = urt.AUTO_UPDATE_COUNT + 1
	FROM deleted d
	INNER JOIN [dbo].[UserRefreshToken] urt
	ON urt.RefreshTokenId = d.RefreshTokenId
END
END 
GO

USE FleetManagement007;
GO

INSERT INTO [dbo].[TypeRijbewijs] ([type])
VALUES
  ('Type A'),
  ('Type B'),
  ('Type C'),
  ('Type D'),
  ('Type BE'),
  ('Type C1'),
  ('Type CE'),
  ('Type D1'),
  ('Type DE'),
  ('Type AM'),
  ('Type G'),
  ('Type T'),
  ('Type C1E'),
  ('Type CE1'),
  ('Type L'),
  ('Type L1'),
  ('Type L2');
  
  
INSERT INTO [dbo].[Bestuurder] ([naam], [voornaam], [adres], [rijksregisternummer], [typerijbewijsId])
VALUES
  ('Janssens', 'Peter', 'Kerkstraat 123, 2000 Antwerpen', '67110716938', 1),
  ('Vermeulen', 'Sarah', 'Dorpstraat 45, 9000 Gent', '42080772404', 2),
  ('De Vos', 'Lucas', 'Havenlaan 78, 8000 Brugge', '58022897004', 3),
  ('Peeters', 'Lotte', 'Schoolstraat 12, 3000 Leuven', '20061363618', 4),
  ('Jacobs', 'Eva', 'Parkweg 67, 3500 Hasselt', '26111691207', 5),
  ('Van Damme', 'Michael', 'Boslaan 34, 9000 Gent', '84022152739', 6),
  ('Lenaerts', 'Lise', 'Kanaalstraat 56, 2800 Mechelen', '91073141731', 7),
  ('Wouters', 'David', 'Dennenweg 89, 2500 Lier', '92052470359', 8),
  ('Smet', 'Julie', 'Zuidlaan 23, 9200 Dendermonde', '54092909538', 9),
  ('Maes', 'Max', 'Kastanjelaan 12, 1500 Halle', '61022035054', 10),
  ('Bosmans', 'Emma', 'Eikenlaan 45, 2600 Berchem', '41020536371', 11),
  ('Verhoeven', 'Noah', 'Lindenstraat 78, 2300 Turnhout', '59122483658', 12),
  ('Peters', 'Olivia', 'Wilgenweg 34, 2000 Antwerpen', '91100664391', 13),
  ('Cools', 'Luc', 'Birkenweg 67, 3500 Hasselt', '33090597618', 14),
  ('Jansen', 'Mia', 'Kastanjeplein 89, 9100 Sint-Niklaas', '16100497488', 15),
  ('Van den Broeck', 'Liam', 'Kanaaldijk 23, 9200 Dendermonde', '18041330007', 16),
  ('Peeters', 'Sophie', 'Beukenlaan 56, 2600 Berchem', '98111086401', 1),
  ('De Sutter', 'Finn', 'Eikenstraat 12, 1500 Halle', '85070902485', 2),
  ('Lemmens', 'Lara', 'Dennenlaan 45, 2500 Lier', '61041561154', 3),
  ('Willems', 'Julien', 'Kastanjeweg 78, 3500 Hasselt', '68030905161', 4),
  ('Bernaerts', 'Zoe', 'Wilgenlaan 34, 2000 Antwerpen', '28123140120', 5),
  ('Verhaegen', 'Sam', 'Kerklaan 67, 3500 Hasselt', '87071214018', 6),
  ('Smeets', 'Ella', 'Dorpstraat 89, 3500 Hasselt', '45051194001', 7),
  ('Maertens', 'Lucas', 'Parkweg 23, 3500 Hasselt', '17061999991', 8),
  ('Jacobs', 'Julia', 'Boslaan 56, 9000 Gent', '60051373177', 9),
  ('De Vries', 'Sem', 'Kanaalstraat 12, 9200 Dendermonde', '51051574409', 10),
  ('Van Hout', 'Fleur', 'Zuidlaan 45, 8500 Kortrijk', '16102447980', 11),
  ('Bosch', 'Nina', 'Schoolweg 78, 2000 Antwerpen', '86031115581', 12),
  ('Vermeiren', 'Nick', 'Havenplein 34, 2000 Antwerpen', '52020316971', 13);

  
INSERT INTO [dbo].[BrandstofType] ([type])
VALUES
  ('Benzine'),
  ('Diesel'),
  ('Elektrisch'),
  ('LPG'),
  ('CNG');
  
INSERT INTO [dbo].[Tankkaart] ([kaartnummer], [geldigheidsdatum], [pincode], [brandstofTypeId])
VALUES
  (123456789, CAST(N'2025-01-02T00:00:00.000' AS DateTime), 1234, 1),
  (234567890, CAST(N'2025-02-03T00:00:00.000' AS DateTime), 2345, 2),
  (345678901, CAST(N'2025-03-04T00:00:00.000' AS DateTime), 3456, 3),
  (456789012, CAST(N'2025-04-05T00:00:00.000' AS DateTime), 4567, 4),
  (567890123, CAST(N'2025-05-06T00:00:00.000' AS DateTime), 5678, 5),
  (678901234, CAST(N'2025-06-07T00:00:00.000' AS DateTime), 6789, 1),
  (789012345, CAST(N'2025-07-08T00:00:00.000' AS DateTime), 7890, 2),
  (890123456, CAST(N'2025-08-09T00:00:00.000' AS DateTime), 8901, 3),
  (901234567, CAST(N'2025-09-10T00:00:00.000' AS DateTime), 9012, 4),
  (102345678, CAST(N'2025-10-11T00:00:00.000' AS DateTime), 1234, 5),
  (112345678, CAST(N'2025-11-12T00:00:00.000' AS DateTime), 2345, 1),
  (122345678, CAST(N'2025-12-13T00:00:00.000' AS DateTime), 3456, 2),
  (132345678, CAST(N'2026-01-14T00:00:00.000' AS DateTime), 4567, 3),
  (142345678, CAST(N'2026-02-15T00:00:00.000' AS DateTime), 5678, 4),
  (152345678, CAST(N'2026-03-16T00:00:00.000' AS DateTime), 6789, 5),
  (162345678, CAST(N'2026-04-17T00:00:00.000' AS DateTime), 1234, 1),
  (172345678, CAST(N'2026-05-18T00:00:00.000' AS DateTime), 2345, 2),
  (182345678, CAST(N'2026-06-19T00:00:00.000' AS DateTime), 3456, 3),
  (192345678, CAST(N'2026-07-20T00:00:00.000' AS DateTime), 4567, 4),
  (202345678, CAST(N'2026-08-21T00:00:00.000' AS DateTime), 5678, 5),
  (212345678, CAST(N'2026-09-22T00:00:00.000' AS DateTime), 6789, 1),
  (222345678, CAST(N'2026-10-23T00:00:00.000' AS DateTime), 7890, 2),
  (232345678, CAST(N'2026-11-24T00:00:00.000' AS DateTime), 8901, 3),
  (242345678, CAST(N'2026-12-25T00:00:00.000' AS DateTime), 9012, 4),
  (252345678, CAST(N'2027-01-26T00:00:00.000' AS DateTime), 1234, 5),
  (262345678, CAST(N'2027-02-27T00:00:00.000' AS DateTime), 2345, 1),
  (272345678, CAST(N'2027-03-28T00:00:00.000' AS DateTime), 3456, 2);

  
INSERT INTO [dbo].[TypeWagen] ([type])
VALUES
  ('Sedan'),
  ('Hatchback'),
  ('SUV'),
  ('Stationwagen'),
  ('Coupé');
  

INSERT INTO [dbo].[Voertuig] ([merkEnModel], [chassisnummer], [nummerplaat], [brandstofTypeId], [typeWagenId], [kleur], [aantal_deuren])
VALUES
  ('Volkswagen Golf', 'WVWZZZ1JZ3W358075', '1-ABC-123', 1, 2, 'Blauw', 5),
  ('Toyota Corolla', 'JTDSJ20X203123456', '2-DEF-456', 2, 3, 'Rood', 4),
  ('Ford Explorer', '1FM5K8GT8MGB16175', '3-GHI-789', 3, 1, 'Zilver', 5),
  ('BMW 3 Serie', 'WBA8E9C07LAP49999', '4-JKL-101', 1, 2, 'Grijs', 4),
  ('Nissan Rogue', '5N1AT3CA3MC753799', '5-MNO-202', 2, 3, 'Zwart', 5),
  ('Honda Civic', '19XFC2F51NE300548', '6-PQR-303', 1, 4, 'Wit', 4),
  ('Mercedes-Benz E-Klasse', 'WDDZF8EB1LA731192', '7-STU-404', 3, 1, 'Rood', 5),
  ('Audi A4', 'WAUAAAF43KN007379', '8-VWX-505', 2, 2, 'Blauw', 4),
  ('Hyundai Tucson', 'KMHJ3814KLU439234', '9-YZAB-606', 1, 4, 'Zilver', 5),
  ('Chevrolet Malibu', '1G1ZD5ST5LF056877', '1-CDE-707', 3, 1, 'Grijs', 4),
  ('Kia Forte', '3KPFL4A77JE189072', '2-FGH-808', 2, 3, 'Wit', 5),
  ('Volvo XC90', 'YV4A22PK8J1295463', '3-IJK-909', 1, 2, 'Zwart', 4),
  ('Jeep Grand Cherokee', '1C4RJFDJ5LC132337', '4-LMN-010', 3, 1, 'Zilver', 5),
  ('Subaru Outback', '4S4BRBLC2D3232850', '5-OPQ-111', 2, 3, 'Blauw', 4),
  ('Lexus RX', '2T2HZMDA6MC171059', '6-RST-212', 1, 2, 'Grijs', 5),
  ('Mazda CX-5', 'JM3KFBDL3M0654783', '7-UVW-313', 3, 1, 'Wit', 4),
  ('Tesla Model 3', '5YJ3E1EB7KF513299', '8-XYZ-414', 2, 3, 'Zwart', 5),
  ('Acura MDX', '5J8TC2H35NL012193', '9-ABC-515', 1, 4, 'Rood', 4),
  ('Chrysler 300', '2C3CCARG2JH224609', '1-DEF-616', 3, 1, 'Blauw', 5),
  ('GMC Sierra', '3GTU2UEC2JG122666', '2-GHI-717', 2, 2, 'Zwart', 4),
  ('Jaguar F-PACE', 'SADCL2FX3LA612127', '3-JKL-818', 1, 4, 'Grijs', 5),
  ('Land Rover Discovery', 'SALRR2RV4LA131646', '4-MNO-919', 3, 1, 'Wit', 4),
  ('Porsche Cayenne', 'WP1AA2AY7MDA01673', '5-PQR-020', 2, 3, 'Rood', 5),
  ('Volvo S60', '7JR102FK9LG040154', '6-STU-121', 1, 2, 'Zwart', 4),
  ('Infiniti QX50', '3PCAJ5M37KF140865', '7-TUV-222', 3, 1, 'Blauw', 5),
  ('Mitsubishi Outlander', 'JA4AZ3A3XGZ100784', '8-VWX-323', 2, 4, 'Grijs', 4),
  ('Lexus ES', '58ABZ1B14KU012588', '9-XYZ-424', 1, 1, 'Wit', 5),
  ('Cadillac XT5', '1GYKNARS2LZ138873', '1-ABC-525', 3, 2, 'Rood', 4),
  ('Buick Encore', 'KL4CJGSB5KB100344', '2-DEF-626', 2, 3, 'Zwart', 5),
  ('Audi Q7', 'WA1VAAF77JD031778', '3-GHI-727', 1, 4, 'Blauw', 4),
  ('Lincoln Nautilus', '2LMPJ6J93LBL07319', '4-JKL-828', 3, 1, 'Grijs', 5),
  ('Kia Seltos', 'KNDEU2A28L7021255', '5-MNO-929', 2, 2, 'Wit', 4),
  ('Nissan Altima', '1N4BL4BV9LC247717', '6-PQR-030', 1, 3, 'Zwart', 5),
  ('Subaru Legacy', '4S3BWAC60L3001043', '7-STU-131', 3, 4, 'Rood', 4),
  ('Jeep Wrangler', '1C4HJXEN4LW308461', '8-VWX-232', 2, 1, 'Blauw', 5),
  ('Honda CR-V', '2HKRW2H52MH659411', '9-YZAB-333', 1, 2, 'Grijs', 4),
  ('Toyota RAV4', '2T3DFRFV3LW137998', '1-CDE-434', 3, 3, 'Wit', 5),
  ('Hyundai Kona', 'KM8K1CAA2LU575381', '2-FGH-535', 2, 1, 'Zwart', 4),
  ('Ford F-150', '1FTFW1E52LFB23275', '3-IJK-636', 1, 2, 'Rood', 5),
  ('Chevrolet Silverado', '1GCUYDED5MZ279112', '4-LMN-737', 3, 4, 'Blauw', 4),
  ('GMC Acadia', '1GKKRRKD0LZ241987', '5-OPQ-838', 2, 1, 'Grijs', 5),
  ('Ram 1500', '1C6RREFT8MN712313', '6-RST-939', 1, 2, 'Wit', 4),
  ('Volkswagen Passat', '1VWBA7A31MC210971', '7-UVW-040', 3, 3, 'Zwart', 5),
  ('Acura RDX', '5J8TC1H54ML006184', '8-XYZ-141', 2, 1, 'Rood', 4),
  ('Lincoln Aviator', '5LM5J7XC8LGL26048', '9-ABC-242', 1, 4, 'Blauw', 5),
  ('Tesla Model S', '5YJSA1E29LF291382', '1-DEF-343', 3, 2, 'Grijs', 4),
  ('Audi Q5', 'WA1ANAFY6M2029689', '2-GHI-444', 2, 1, 'Wit', 5),
  ('Porsche 911', 'WP0AB2A98KS112395', '3-JKL-545', 1, 3, 'Zwart', 4),
  ('Mercedes-Benz GLE', '4JGFB4KE3LA016862', '4-MNO-646', 3, 2, 'Rood', 5),
  ('Land Rover Range Rover', 'SALGS2SE9LA570036', '5-PQR-747', 2, 1, 'Blauw', 4);




INSERT INTO [dbo].[FleetMember] (bestuurderId, tankkaartId, voertuigId)
VALUES (1,1,4), (2,2,8), (3,3,12), (4,4,16);

INSERT INTO [dbo].[User] (Email, PasswordSalt, PasswordHash, [Role])
VALUES ('admin@fm.com', 'oJcneo5hnsyaq96MlKTH8g==', 'DxGooDQNCqYhqhxq7SOAqxEd5WW+4gCKf35jWOcx6a0=', 'Admin');

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

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
  ('Janssens', 'Peter', 'Kerkstraat 123, 2000 Antwerpen', '75063012345', 1),
  ('Vermeulen', 'Sarah', 'Dorpstraat 45, 9000 Gent', '98020023456', 2),
  ('De Vos', 'Lucas', 'Havenlaan 78, 8000 Brugge', '01080034567', 3),
  ('Peeters', 'Lotte', 'Schoolstraat 12, 3000 Leuven', '88050045678', 4),
  ('Jacobs', 'Eva', 'Parkweg 67, 3500 Hasselt', '91020056789', 5),
  ('Van Damme', 'Michael', 'Boslaan 34, 9000 Gent', '92070067890', 6),
  ('Lenaerts', 'Lise', 'Kanaalstraat 56, 2800 Mechelen', '73100078901', 7),
  ('Wouters', 'David', 'Dennenweg 89, 2500 Lier', '86110089012', 8),
  ('Smet', 'Julie', 'Zuidlaan 23, 9200 Dendermonde', '70063000123', 9),
  ('Maes', 'Max', 'Kastanjelaan 12, 1500 Halle', '80123111234', 10),
  ('Bosmans', 'Emma', 'Eikenlaan 45, 2600 Berchem', '02102034556', 11),
  ('Verhoeven', 'Noah', 'Lindenstraat 78, 2300 Turnhout', '03062045667', 12),
  ('Peters', 'Olivia', 'Wilgenweg 34, 2000 Antwerpen', '82121456778', 13),
  ('Cools', 'Luc', 'Birkenweg 67, 3500 Hasselt', '84110567889', 14),
  ('Jansen', 'Mia', 'Kastanjeplein 89, 9100 Sint-Niklaas', '10073178990', 15),
  ('Van den Broeck', 'Liam', 'Kanaaldijk 23, 9200 Dendermonde', '11113089001', 16),
  ('Peeters', 'Sophie', 'Beukenlaan 56, 2600 Berchem', '70000000112', 1),
  ('De Sutter', 'Finn', 'Eikenstraat 12, 1500 Halle', '81010111223', 2),
  ('Lemmens', 'Lara', 'Dennenlaan 45, 2500 Lier', '92063012334', 3),
  ('Willems', 'Julien', 'Kastanjeweg 78, 3500 Hasselt', '75012323445', 4),
  ('Bernaerts', 'Zoe', 'Wilgenlaan 34, 2000 Antwerpen', '88023434556', 5),
  ('Verhaegen', 'Sam', 'Kerklaan 67, 3500 Hasselt', '99011145667', 6),
  ('Smeets', 'Ella', 'Dorpstraat 89, 3500 Hasselt', '04040456778', 7),
  ('Maertens', 'Lucas', 'Parkweg 23, 3500 Hasselt', '98121267889', 8),
  ('Jacobs', 'Julia', 'Boslaan 56, 9000 Gent', '75111578990', 9),
  ('De Vries', 'Sem', 'Kanaalstraat 12, 9200 Dendermonde', '91013189001', 10),
  ('Van Hout', 'Fleur', 'Zuidlaan 45, 8500 Kortrijk', '85042500112', 11),
  ('Bosch', 'Nina', 'Schoolweg 78, 2000 Antwerpen', '02121211223', 12),
  ('Vermeiren', 'Nick', 'Havenplein 34, 2000 Antwerpen', '10012312334', 13);

  
INSERT INTO [dbo].[BrandstofType] ([type])
VALUES
  ('Benzine'),
  ('Diesel'),
  ('Elektrisch'),
  ('LPG (vloeibaar petroleumgas)'),
  ('CNG (aardgas)');
  
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


INSERT INTO [dbo].[Login] ([email], [wachtwoord], [rol])
VALUES
  ('peter.janssens@example.com', 'mijnwachtwoord1', 'User'),
  ('sarah.vermeulen@example.com', 'mijnwachtwoord2', 'User'),
  ('lucas.devos@example.com', 'mijnwachtwoord3', 'User'),
  ('lotte.peeters@example.com', 'mijnwachtwoord4', 'User');







-- Voeg gegevens toe aan de Fleet-tabel
INSERT INTO [dbo].[Fleet] ([bestuurderId], [tankkaartId], [voertuigId])
SELECT
  b.bestuurderId,
  t.tankkaartId,
  v.voertuigId
FROM [dbo].[Bestuurder] AS b
JOIN [dbo].[Tankkaart] AS t ON b.bestuurderId = t.tankkaartId
JOIN [dbo].[Voertuig] AS v ON v.brandstofTypeId = t.brandstofTypeId
WHERE (b.bestuurderId, t.tankkaartId, v.voertuigId) NOT IN (
  SELECT [bestuurderId], [tankkaartId], [voertuigId] FROM [dbo].[Fleet]
);
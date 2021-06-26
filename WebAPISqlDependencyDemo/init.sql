CREATE DATABASE [Fake_SAP]
USE [Fake_SAP]
CREATE TABLE [AKL_CONTENT_TOTE] (
    [id] INT IDENTITY PRIMARY KEY NOT NULL,
    [vlenr] CHAR(20) NOT NULL,
    [aufnr] CHAR(12) NOT NULL,
    [matnr] CHAR(18) NOT NULL,
    [charg] CHAR(10) NOT NULL,
    [menge_aufnr] DECIMAL(16, 3) NULL,
    [menge_source] DECIMAL(16, 3) NULL,
    [menge_target] DECIMAL(16, 3) NULL,
    [meins] CHAR(3) NULL DEFAULT 'PC',
    [type_counting] CHAR(1) NULL, -- M/W/S
    [workstation] CHAR(4) NULL
    CHECK(
    [workstation] = '01'
    OR [workstation] = '02'
    OR [workstation] = '03'
    OR [workstation] = '04'
    OR [workstation] = '05'
    OR [workstation] = '06'
    OR [workstation] = '07'
    OR [workstation] = '08'
    OR [workstation] = '09'
    OR [workstation] = '10'
    OR [workstation] = '11'
    OR [workstation] = '12'
    OR [workstation] = '13'
    OR [workstation] = '14'
    OR [workstation] = '15'
    OR [workstation] = '16'
    OR [workstation] = '17'
    OR [workstation] = '18'
       ), -- 01 to 18
    [pms_user] CHAR(12) NULL,
    [pms_date] DATE NULL,
    [pms_time] TIME NULL
    );
SELECT * FROM [AKL_CONTENT_TOTE];
INSERT INTO [AKL_CONTENT_TOTE] ([vlenr], [aufnr], [matnr], [charg], [menge_aufnr], [menge_source],
    [menge_target], [meins], [type_counting], [workstation],
    [pms_user], [pms_date], [pms_time])
VALUES ('vlenr1', 'aufnr1', 'matnr1', 'charg1', 11.999, 12.999,
    13.999, NULL, 'S', '01',
    'user1', '01-07-2021', '12:01:01');
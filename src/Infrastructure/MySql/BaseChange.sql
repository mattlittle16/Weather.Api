CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `LogType` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Type` int NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_LogType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Log` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `LogTypeId` char(36) COLLATE ascii_general_ci NOT NULL,
    `EventId` longtext CHARACTER SET utf8mb4 NOT NULL,
    `EventName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Message` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_Log` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Log_LogType_LogTypeId` FOREIGN KEY (`LogTypeId`) REFERENCES `LogType` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Log_LogTypeId` ON `Log` (`LogTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240519125621_BaseChange', '8.0.2');

COMMIT;


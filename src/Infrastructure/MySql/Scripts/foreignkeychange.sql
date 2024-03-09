START TRANSACTION;

ALTER TABLE `Log` DROP FOREIGN KEY `FK_Log_LogType_Id`;

CREATE INDEX `IX_Log_LogTypeId` ON `Log` (`LogTypeId`);

ALTER TABLE `Log` ADD CONSTRAINT `FK_Log_LogType_LogTypeId` FOREIGN KEY (`LogTypeId`) REFERENCES `LogType` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240309134126_KeyChange', '8.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240309134344_ForeignKeyChange', '8.0.2');

COMMIT;


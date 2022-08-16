IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Client] (
    [Id] int NOT NULL IDENTITY,
    [Cpf] NVARCHAR(14) NOT NULL,
    [State] NVARCHAR(45) NOT NULL,
    [City] NVARCHAR(45) NOT NULL,
    [Complement] NVARCHAR(300) NULL,
    [Cep] NVARCHAR(9) NOT NULL,
    [Street] NVARCHAR(300) NOT NULL,
    [StreetNumber] INT NOT NULL,
    [Name] NVARCHAR(80) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [Phone] NVARCHAR(14) NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Doctor] (
    [Id] int NOT NULL IDENTITY,
    [Cpf] NVARCHAR(14) NOT NULL,
    [Specialty] NVARCHAR(30) NOT NULL,
    [RegionalCouncil] NVARCHAR(30) NOT NULL,
    [State] NVARCHAR(45) NOT NULL,
    [City] NVARCHAR(45) NOT NULL,
    [Complement] NVARCHAR(300) NULL,
    [Cep] NVARCHAR(9) NOT NULL,
    [Street] NVARCHAR(300) NOT NULL,
    [StreetNumber] INT NOT NULL,
    [Name] NVARCHAR(80) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [Phone] NVARCHAR(14) NOT NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Drugstore] (
    [Id] int NOT NULL IDENTITY,
    [Cnpj] NVARCHAR(14) NOT NULL,
    [State] NVARCHAR(45) NOT NULL,
    [City] NVARCHAR(45) NOT NULL,
    [Complement] NVARCHAR(300) NULL,
    [Cep] NVARCHAR(9) NOT NULL,
    [Street] NVARCHAR(300) NOT NULL,
    [StreetNumber] INT NOT NULL,
    [Name] NVARCHAR(80) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [Phone] NVARCHAR(14) NOT NULL,
    CONSTRAINT [PK_Drugstore] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Appointment] (
    [Id] int NOT NULL IDENTITY,
    [DateTimeAppointment] DATETIME NOT NULL,
    [Remote] BIT NOT NULL,
    [VideoCallUrl] TEXT NOT NULL,
    [ClientId] int NOT NULL,
    [DoctorId] int NOT NULL,
    CONSTRAINT [PK_Appointment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Appointments_Clients] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Doctor] FOREIGN KEY ([DoctorId]) REFERENCES [Doctor] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Order] (
    [Id] int NOT NULL IDENTITY,
    [Image] TEXT NOT NULL,
    [ClientId] int NOT NULL,
    [DrugstoresId] int NOT NULL,
    [State] NVARCHAR(45) NOT NULL,
    [City] NVARCHAR(45) NOT NULL,
    [Complement] NVARCHAR(300) NULL,
    [Cep] NVARCHAR(9) NOT NULL,
    [Street] NVARCHAR(300) NOT NULL,
    [StreetNumber] INT NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Clients] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Drugstores] FOREIGN KEY ([DrugstoresId]) REFERENCES [Drugstore] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Appointment_ClientId] ON [Appointment] ([ClientId]);
GO

CREATE INDEX [IX_Appointment_DoctorId] ON [Appointment] ([DoctorId]);
GO

CREATE INDEX [IX_Order_ClientId] ON [Order] ([ClientId]);
GO

CREATE INDEX [IX_Order_DrugstoresId] ON [Order] ([DrugstoresId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220730041313_CreateDatabase', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220731174753_modifyDatabase', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Appointment]') AND [c].[name] = N'VideoCallUrl');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Appointment] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Appointment] ALTER COLUMN [VideoCallUrl] TEXT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220731193530_AppointmentVideoCallUrlIsNull', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220731202651_CreateDatabaseAgain', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Drugstore]') AND [c].[name] = N'Cnpj');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Drugstore] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Drugstore] ALTER COLUMN [Cnpj] NVARCHAR(18) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220801021136_ModifySizeCnpj', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Drugstore] ADD [PasswordHash] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Drugstore] ADD [RefreshToken] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Drugstore] ADD [Roles] NVARCHAR(20) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Doctor] ADD [PasswordHash] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Doctor] ADD [RefreshToken] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Doctor] ADD [Roles] NVARCHAR(20) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Client] ADD [PasswordHash] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Client] ADD [RefreshToken] NVARCHAR(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Client] ADD [Roles] NVARCHAR(20) NOT NULL DEFAULT N'';
GO

CREATE UNIQUE INDEX [IX_Drugstore_Email] ON [Drugstore] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Doctor_Email] ON [Doctor] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Client_Email] ON [Client] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220810010834_CreateResfreshTokenAttribute', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Order] ADD [DateTimeOrder] DATETIME NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220814014243_CreateDataOrders', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Appointment] ADD [Confirmed] BIT NOT NULL DEFAULT CAST(0 AS BIT);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220814180210_CreateAttributeConfirmedInOrderTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Order] ADD [District] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Drugstore] ADD [District] NVARCHAR(300) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Doctor] ADD [District] NVARCHAR(300) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Client] ADD [District] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220816013952_CreateDistrictAttributeInTablesClientDoctorDrugstore', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'District');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Order] ALTER COLUMN [District] NVARCHAR(300) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Client]') AND [c].[name] = N'District');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Client] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Client] ALTER COLUMN [District] NVARCHAR(300) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220816015026_CreateDistrictAttributeInTableOrder', N'6.0.8');
GO

COMMIT;
GO


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
VALUES (N'20220730041313_CreateDatabase', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220731174753_modifyDatabase', N'6.0.7');
GO

COMMIT;
GO


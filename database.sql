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
CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(200) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);

CREATE TABLE [ServiceTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [BasePrice] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_ServiceTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [Vehicles] (
    [Id] int NOT NULL IDENTITY,
    [RegistrationNumber] nvarchar(20) NOT NULL,
    [Brand] nvarchar(100) NOT NULL,
    [Model] nvarchar(100) NOT NULL,
    [Year] int NOT NULL,
    [CustomerId] int NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehicles_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Bookings] (
    [Id] int NOT NULL IDENTITY,
    [VehicleId] int NOT NULL,
    [StartTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [BookingServiceTypes] (
    [BookingId] int NOT NULL,
    [ServiceTypeId] int NOT NULL,
    CONSTRAINT [PK_BookingServiceTypes] PRIMARY KEY ([BookingId], [ServiceTypeId]),
    CONSTRAINT [FK_BookingServiceTypes_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookingServiceTypes_ServiceTypes_ServiceTypeId] FOREIGN KEY ([ServiceTypeId]) REFERENCES [ServiceTypes] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Bookings_VehicleId] ON [Bookings] ([VehicleId]);

CREATE INDEX [IX_BookingServiceTypes_ServiceTypeId] ON [BookingServiceTypes] ([ServiceTypeId]);

CREATE INDEX [IX_Vehicles_CustomerId] ON [Vehicles] ([CustomerId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251202194717_InitialCreate', N'9.0.0');

COMMIT;
GO


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

CREATE TABLE [Airports] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Airports] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Login] nvarchar(50) NOT NULL,
    [PasswordHash] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Flights] (
    [Id] int NOT NULL IDENTITY,
    [FlightNumber] nvarchar(7) NOT NULL,
    [DepartureDateTime] datetime2 NOT NULL,
    [DepartureAirportId] int NOT NULL,
    [ArrivalAirportId] int NOT NULL,
    [PlaneType] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Flights] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flights_Airports_ArrivalAirportId] FOREIGN KEY ([ArrivalAirportId]) REFERENCES [Airports] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flights_Airports_DepartureAirportId] FOREIGN KEY ([DepartureAirportId]) REFERENCES [Airports] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Airports_Name] ON [Airports] ([Name]);
GO

CREATE INDEX [IX_Flights_ArrivalAirportId] ON [Flights] ([ArrivalAirportId]);
GO

CREATE INDEX [IX_Flights_DepartureAirportId] ON [Flights] ([DepartureAirportId]);
GO

CREATE UNIQUE INDEX [IX_Flights_FlightNumber] ON [Flights] ([FlightNumber]);
GO

CREATE UNIQUE INDEX [IX_Users_Login] ON [Users] ([Login]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240419173907_InitialFlightDB', N'8.0.4');
GO

COMMIT;
GO


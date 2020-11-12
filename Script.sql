CREATE TABLE [dbo].[ElectricMeters] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [SerialNumber]    NVARCHAR (50)    NOT NULL,
    [FirmwareVersion] NVARCHAR (20)    NULL,
    [State]           INT              NULL
    CONSTRAINT PK_ELECTRICMETER PRIMARY KEY ([Id] ASC),
    CONSTRAINT U_ELECTRICMETER UNIQUE ([SerialNumber] ASC)
);

CREATE TABLE [dbo].[WaterMeters] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [SerialNumber]    NVARCHAR (50)    NOT NULL,
    [FirmwareVersion] NVARCHAR (20)    NULL,
    [State]           INT              NULL
    CONSTRAINT PK_WATERMETER PRIMARY KEY ([Id] ASC),
    CONSTRAINT U_WATERMETER UNIQUE ([SerialNumber] ASC)
);

CREATE TABLE [dbo].[Gateways] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [SerialNumber]    NVARCHAR (50)    NOT NULL,
    [FirmwareVersion] NVARCHAR (20)    NULL,
    [State]           INT              NULL,
    [IP]              NVARCHAR(20)     NULL,
    [Port]            INT              NULL,
    CONSTRAINT PK_GATEWAY PRIMARY KEY ([Id] ASC),
    CONSTRAINT U_GATEWAY UNIQUE ([SerialNumber] ASC)
);

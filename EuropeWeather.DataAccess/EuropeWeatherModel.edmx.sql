
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/05/2016 17:05:48
-- Generated from EDMX file: C:\Projects\api-test\EuropeWeather.DataAccess\EuropeWeatherModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EuropeWeather.Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Cities_Countries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_Cities_Countries];
GO
IF OBJECT_ID(N'[dbo].[FK_WeatherData_Cities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeatherData] DROP CONSTRAINT [FK_WeatherData_Cities];
GO
IF OBJECT_ID(N'[dbo].[FK_WeatherDataConditions_WeatherConditions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeatherDataConditions] DROP CONSTRAINT [FK_WeatherDataConditions_WeatherConditions];
GO
IF OBJECT_ID(N'[dbo].[FK_WeatherDataConditions_WeatherData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeatherDataConditions] DROP CONSTRAINT [FK_WeatherDataConditions_WeatherData];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[WeatherConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeatherConditions];
GO
IF OBJECT_ID(N'[dbo].[WeatherData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeatherData];
GO
IF OBJECT_ID(N'[dbo].[WeatherDataConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeatherDataConditions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [CityId] int IDENTITY(1,1) NOT NULL,
    [CountryId] int  NOT NULL,
    [CityName] nvarchar(250)  NOT NULL,
    [ExternalId] int  NULL,
    [Longitude] decimal(9,6)  NULL,
    [Latitude] decimal(9,6)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Code2] nvarchar(2)  NOT NULL,
    [Code3] nvarchar(3)  NULL
);
GO

-- Creating table 'WeatherConditions'
CREATE TABLE [dbo].[WeatherConditions] (
    [WeatherConditionId] int IDENTITY(1,1) NOT NULL,
    [ExternalId] int  NOT NULL,
    [Description] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'WeatherData'
CREATE TABLE [dbo].[WeatherData] (
    [WeatherDataId] bigint IDENTITY(1,1) NOT NULL,
    [CityId] int  NOT NULL,
    [Temperature] decimal(5,2)  NULL,
    [Pressure] decimal(8,2)  NULL,
    [Humidity] decimal(5,2)  NULL,
    [MinTemperature] decimal(5,2)  NULL,
    [MaxTemperature] decimal(5,2)  NULL,
    [Visibility] int  NULL,
    [WindSpeed] decimal(4,2)  NULL,
    [WindDirection] decimal(7,4)  NULL,
    [Clouds] decimal(5,2)  NULL,
    [Rain] decimal(10,2)  NULL,
    [Snow] decimal(10,2)  NULL,
    [Sunrise]  [datetime2](2)  NULL,
    [Sunset]  [datetime2](2)  NULL,
    [TimeOfCalculation]  [datetime2](2)  NULL,
    [Created]  [datetime2](7)  NULL
);
GO

-- Creating table 'WeatherDataConditions'
CREATE TABLE [dbo].[WeatherDataConditions] (
    [WeatherDataConditionId] bigint IDENTITY(1,1) NOT NULL,
    [WeatherDataId] bigint  NOT NULL,
    [WeatherConditionId] int  NOT NULL,
    [WeatherMain] nvarchar(250)  NULL,
    [WeatherIcon] varchar(10)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CityId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([CityId] ASC);
GO

-- Creating primary key on [CountryId] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryId] ASC);
GO

-- Creating primary key on [WeatherConditionId] in table 'WeatherConditions'
ALTER TABLE [dbo].[WeatherConditions]
ADD CONSTRAINT [PK_WeatherConditions]
    PRIMARY KEY CLUSTERED ([WeatherConditionId] ASC);
GO

-- Creating primary key on [WeatherDataId] in table 'WeatherData'
ALTER TABLE [dbo].[WeatherData]
ADD CONSTRAINT [PK_WeatherData]
    PRIMARY KEY CLUSTERED ([WeatherDataId] ASC);
GO

-- Creating primary key on [WeatherDataConditionId] in table 'WeatherDataConditions'
ALTER TABLE [dbo].[WeatherDataConditions]
ADD CONSTRAINT [PK_WeatherDataConditions]
    PRIMARY KEY CLUSTERED ([WeatherDataConditionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_Cities_Countries]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([CountryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cities_Countries'
CREATE INDEX [IX_FK_Cities_Countries]
ON [dbo].[Cities]
    ([CountryId]);
GO

-- Creating foreign key on [CityId] in table 'WeatherData'
ALTER TABLE [dbo].[WeatherData]
ADD CONSTRAINT [FK_WeatherData_Cities]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeatherData_Cities'
CREATE INDEX [IX_FK_WeatherData_Cities]
ON [dbo].[WeatherData]
    ([CityId]);
GO

-- Creating foreign key on [WeatherConditionId] in table 'WeatherDataConditions'
ALTER TABLE [dbo].[WeatherDataConditions]
ADD CONSTRAINT [FK_WeatherDataConditions_WeatherConditions]
    FOREIGN KEY ([WeatherConditionId])
    REFERENCES [dbo].[WeatherConditions]
        ([WeatherConditionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeatherDataConditions_WeatherConditions'
CREATE INDEX [IX_FK_WeatherDataConditions_WeatherConditions]
ON [dbo].[WeatherDataConditions]
    ([WeatherConditionId]);
GO

-- Creating foreign key on [WeatherDataId] in table 'WeatherDataConditions'
ALTER TABLE [dbo].[WeatherDataConditions]
ADD CONSTRAINT [FK_WeatherDataConditions_WeatherData]
    FOREIGN KEY ([WeatherDataId])
    REFERENCES [dbo].[WeatherData]
        ([WeatherDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeatherDataConditions_WeatherData'
CREATE INDEX [IX_FK_WeatherDataConditions_WeatherData]
ON [dbo].[WeatherDataConditions]
    ([WeatherDataId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
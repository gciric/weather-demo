CREATE TABLE [dbo].[WeatherData] (
    [WeatherDataId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [CityId]            INT             NOT NULL,
    [Temperature]       DECIMAL (5, 2)  NULL,
    [Pressure]          DECIMAL (8, 2)  NULL,
    [Humidity]          DECIMAL (5, 2)  NULL,
    [MinTemperature]    DECIMAL (5, 2)  NULL,
    [MaxTemperature]    DECIMAL (5, 2)  NULL,
    [Visibility]        INT             NULL,
    [WindSpeed]         DECIMAL (4, 2)  NULL,
    [WindDirection]     DECIMAL (7, 4)  NULL,
    [Clouds]            DECIMAL (5, 2)  NULL,
    [Rain]              DECIMAL (10, 2) NULL,
    [Snow]              DECIMAL (10, 2) NULL,
    [Sunrise]           DATETIME2 (2)   NULL,
    [Sunset]            DATETIME2 (2)   NULL,
    [TimeOfCalculation] DATETIME2 (2)   NULL,
    [Created]           DATETIME2 (7)   NULL,
    CONSTRAINT [PK_WeatherData] PRIMARY KEY CLUSTERED ([WeatherDataId] ASC),
    CONSTRAINT [FK_WeatherData_Cities] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([CityId])
);


GO

GO
CREATE NONCLUSTERED INDEX [IX_FK_WeatherData_Cities]
    ON [dbo].[WeatherData]([CityId] ASC);


CREATE TABLE [dbo].[WeatherDataConditions] (
    [WeatherDataConditionId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [WeatherDataId]          BIGINT         NOT NULL,
    [WeatherConditionId]     INT            NOT NULL,
    [WeatherMain]            NVARCHAR (250) NULL,
    [WeatherIcon]            VARCHAR (10)   NULL,
    CONSTRAINT [PK_WeatherDataConditions] PRIMARY KEY CLUSTERED ([WeatherDataConditionId] ASC),
    CONSTRAINT [FK_WeatherDataConditions_WeatherConditions] FOREIGN KEY ([WeatherConditionId]) REFERENCES [dbo].[WeatherConditions] ([WeatherConditionId]),
    CONSTRAINT [FK_WeatherDataConditions_WeatherData] FOREIGN KEY ([WeatherDataId]) REFERENCES [dbo].[WeatherData] ([WeatherDataId])
);

GO

CREATE NONCLUSTERED INDEX [IX_FK_WeatherDataConditions_WeatherData]
    ON [dbo].[WeatherDataConditions]([WeatherDataId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_WeatherDataConditions_WeatherConditions]
    ON [dbo].[WeatherDataConditions]([WeatherConditionId] ASC);
GO


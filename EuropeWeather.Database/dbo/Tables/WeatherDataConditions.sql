CREATE TABLE [dbo].[WeatherDataConditions](
	[WeatherDataConditionId] [bigint] IDENTITY(1,1) NOT NULL,
	[WeatherDataId] [bigint] NOT NULL,
	[WeatherConditionId] [int] NOT NULL,
	[WeatherMain] [nvarchar](250) NULL,
	[WeatherIcon] [varchar](10) NULL,
 CONSTRAINT [PK__WeatherD__443B60A0BCE9E1CD] PRIMARY KEY CLUSTERED 
(
	[WeatherDataConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[WeatherDataConditions] ADD  CONSTRAINT [FK_WeatherDataConditions_WeatherConditions] FOREIGN KEY([WeatherConditionId])
REFERENCES [dbo].[WeatherConditions] ([WeatherConditionId])
GO

ALTER TABLE [dbo].[WeatherDataConditions] CHECK CONSTRAINT [FK_WeatherDataConditions_WeatherConditions]
GO

ALTER TABLE [dbo].[WeatherDataConditions] ADD  CONSTRAINT [FK_WeatherDataConditions_WeatherData] FOREIGN KEY([WeatherDataId])
REFERENCES [dbo].[WeatherData] ([WeatherDataId])
GO

ALTER TABLE [dbo].[WeatherDataConditions] CHECK CONSTRAINT [FK_WeatherDataConditions_WeatherData]
GO

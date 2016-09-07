CREATE TABLE [dbo].[WeatherConditions](
	[WeatherConditionId] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [int] NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_WeatherConditions] PRIMARY KEY CLUSTERED 
(
	[WeatherConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
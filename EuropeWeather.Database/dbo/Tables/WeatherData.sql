CREATE TABLE [dbo].[WeatherData](
	[WeatherDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[Temperature] [decimal](5, 2) NULL,
	[Pressure] [decimal](8, 2) NULL,
	[Humidity] [decimal](5, 2) NULL,
	[MinTemperature] [decimal](5, 2) NULL,
	[MaxTemperature] [decimal](5, 2) NULL,
	[Visibility] [int] NULL,
	[WindSpeed] [decimal](4, 2) NULL,
	[WindDirection] [decimal](7, 4) NULL,
	[Clouds] [decimal](5, 2) NULL,
	[Rain] [decimal](10, 2) NULL,
	[Snow] [decimal](10, 2) NULL,
	[Sunrise] [datetime2](2) NULL,
	[Sunset] [datetime2](2) NULL,
	[TimeOfCalculation] [datetime2](2) NULL,
	[Created] [datetime2](7) NULL,
 CONSTRAINT [PK_WeatherData] PRIMARY KEY CLUSTERED 
(
	[WeatherDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
ALTER TABLE [dbo].[WeatherData] ADD  CONSTRAINT [FK_WeatherData_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([CityId])
GO

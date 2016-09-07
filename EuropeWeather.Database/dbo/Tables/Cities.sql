CREATE TABLE [dbo].[Cities] (
    [CityId]     INT            IDENTITY (1, 1) NOT NULL,
    [CountryId]  INT            NOT NULL,
    [CityName]   NVARCHAR (250) NOT NULL,
    [ExternalId] INT            NULL,
    [Longitude]  DECIMAL (9, 6) NULL,
    [Latitude]   DECIMAL (9, 6) NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED ([CityId] ASC),
    CONSTRAINT [FK_Cities_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId])
);


GO



CREATE NONCLUSTERED INDEX [IX_FK_Cities_Countries]
    ON [dbo].[Cities]([CountryId] ASC);
GO

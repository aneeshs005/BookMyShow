USE [BookMyShow]
GO

/****** Object: Table [dbo].[Movies] Script Date: 11/30/2020 10:28:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movies] (
    [MovieId]   INT           IDENTITY (1, 1) NOT NULL,
    [MovieName] VARCHAR (100) NOT NULL,
    [Year]      INT           NOT NULL,
    [Language]  VARCHAR (50)  NOT NULL,
    [Genre]     VARCHAR (20)  NOT NULL,
    [ActorName] VARCHAR (50)  NOT NULL
);
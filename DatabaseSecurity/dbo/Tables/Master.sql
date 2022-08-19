CREATE TABLE [dbo].[Master] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [IdCompany] INT           NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [State]     TINYINT       NOT NULL,
    [UserCrea]  INT           NOT NULL,
    [DateCrea]  DATETIME      NOT NULL,
    [UserUpd]   INT           NULL,
    [DateUpd]   DATETIME      NULL,
    CONSTRAINT [PK_Master] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Company] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (120) NOT NULL,
    [Ruc]      VARCHAR (11)  NOT NULL,
    [State]    TINYINT       NOT NULL,
    [UserCrea] INT           NOT NULL,
    [DateCrea] DATETIME      NOT NULL,
    [UserUpd]  INT           NULL,
    [DateUpd]  DATETIME      NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC)
);


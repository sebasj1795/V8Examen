CREATE TABLE [dbo].[Role] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50)  NOT NULL,
    [Comment]  VARCHAR (150) NULL,
    [State]    TINYINT       NOT NULL,
    [UserCrea] INT           NOT NULL,
    [DateCrea] DATETIME      NOT NULL,
    [UserUpd]  INT           NULL,
    [DateUpd]  DATETIME      NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);


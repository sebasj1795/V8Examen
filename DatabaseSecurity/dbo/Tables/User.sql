CREATE TABLE [dbo].[User] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Name]               VARCHAR (50)  NOT NULL,
    [LastName]           VARCHAR (50)  NOT NULL,
    [UserName]           VARCHAR (50)  NOT NULL,
    [Password]           VARCHAR (150) NOT NULL,
    [Email]              VARCHAR (50)  NULL,
    [State]              TINYINT       NOT NULL,
    [Expire]             BIT           NOT NULL,
    [DateExpire]         DATETIME      NULL,
    [SuperUser]          BIT           NOT NULL,
    [EmailConfirm]       BIT           NOT NULL,
    [ChangePassword]     BIT           NULL,
    [NumberAttempt]      INT           NULL,
    [DateAttempt]        DATETIME      NULL,
    [ModeAuthentication] TINYINT       NOT NULL,
    [UserCrea]           INT           NOT NULL,
    [DateCrea]           DATETIME      NOT NULL,
    [UserUpd]            INT           NULL,
    [DateUpd]            DATETIME      NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);


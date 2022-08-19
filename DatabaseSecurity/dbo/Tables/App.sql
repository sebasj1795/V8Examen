CREATE TABLE [dbo].[App] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (70)  NOT NULL,
    [Comment]        VARCHAR (150) NULL,
    [Server]         VARCHAR (50)  NOT NULL,
    [UserServer]     VARCHAR (100) NOT NULL,
    [PasswordServer] VARCHAR (150) NOT NULL,
    [Port]           VARCHAR (4)   NULL,
    [NameBD]         VARCHAR (25)  NOT NULL,
    [Platform]       INT           NOT NULL,
    [State]          TINYINT       NOT NULL,
    [UserCrea]       INT           NOT NULL,
    [DateCrea]       DATETIME      NOT NULL,
    [UserUpd]        INT           NULL,
    [DateUpd]        DATETIME      NULL,
    [IdCompany]      INT           NOT NULL,
    CONSTRAINT [PK_App] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_App_Company] FOREIGN KEY ([IdCompany]) REFERENCES [dbo].[Company] ([Id])
);


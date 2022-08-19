CREATE TABLE [dbo].[Module] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [IdApp]       INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (150) NULL,
    [Url]         VARCHAR (100) NULL,
    [Order]       INT           NOT NULL,
    [IconCss]     VARCHAR (50)  NULL,
    [IconImg]     VARCHAR (50)  NULL,
    [State]       TINYINT       NOT NULL,
    [UserCrea]    INT           NOT NULL,
    [DateCrea]    DATETIME      NOT NULL,
    [UserUpd]     INT           NULL,
    [DateUpd]     DATETIME      NULL,
    CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Module_App] FOREIGN KEY ([IdApp]) REFERENCES [dbo].[App] ([Id])
);


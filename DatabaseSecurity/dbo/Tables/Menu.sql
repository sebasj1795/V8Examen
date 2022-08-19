CREATE TABLE [dbo].[Menu] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [IdParent] INT           NULL,
    [IdModule] INT           NOT NULL,
    [Name]     VARCHAR (50)  NOT NULL,
    [Url]      VARCHAR (100) NULL,
    [Order]    INT           NULL,
    [IsForm]   BIT           NULL,
    [Level]    INT           NOT NULL,
    [IconCss]  VARCHAR (50)  NULL,
    [IconImg]  VARCHAR (50)  NULL,
    [State]    TINYINT       NOT NULL,
    [UserCrea] INT           NOT NULL,
    [DateCrea] DATETIME      NOT NULL,
    [UserUpd]  INT           NULL,
    [DateUpd]  DATETIME      NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Menu_Module] FOREIGN KEY ([IdModule]) REFERENCES [dbo].[Module] ([Id])
);


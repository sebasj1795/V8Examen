CREATE TABLE [dbo].[MenuAction] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [IdMenu]   INT      NOT NULL,
    [IdAction] INT      NOT NULL,
    [State]    TINYINT  NOT NULL,
    [UserCrea] INT      NOT NULL,
    [DateCrea] DATETIME NOT NULL,
    [UserUpd]  INT      NULL,
    [DateUpd]  DATETIME NULL,
    CONSTRAINT [PK_MenuAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuAction_Action] FOREIGN KEY ([IdAction]) REFERENCES [dbo].[Action] ([Id]),
    CONSTRAINT [FK_MenuAction_Menu1] FOREIGN KEY ([IdMenu]) REFERENCES [dbo].[Menu] ([Id])
);


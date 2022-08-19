CREATE TABLE [dbo].[MenuRole] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IdRole]       INT      NOT NULL,
    [IdMenuAction] INT      NOT NULL,
    [State]        TINYINT  NOT NULL,
    [UserCrea]     INT      NOT NULL,
    [DateCrea]     DATETIME NOT NULL,
    [UserUpd]      INT      NULL,
    [DateUpd]      DATETIME NULL,
    CONSTRAINT [PK_MenuRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuRole_MenuAction] FOREIGN KEY ([IdMenuAction]) REFERENCES [dbo].[MenuAction] ([Id]),
    CONSTRAINT [FK_MenuRole_Role] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Role] ([Id])
);


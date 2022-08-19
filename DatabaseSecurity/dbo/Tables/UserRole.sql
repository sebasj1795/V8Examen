CREATE TABLE [dbo].[UserRole] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [IdUser]   INT      NOT NULL,
    [IdRole]   INT      NOT NULL,
    [Default]  BIT      NOT NULL,
    [State]    TINYINT  NOT NULL,
    [UserCrea] INT      NOT NULL,
    [DateCrea] DATETIME NOT NULL,
    [UserUpd]  INT      NULL,
    [DateUpd]  DATETIME NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Id])
);


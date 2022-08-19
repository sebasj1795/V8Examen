CREATE TABLE [dbo].[Action] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    [State]      TINYINT      NOT NULL,
    [UserCrea]   INT          NOT NULL,
    [DateCrea]   DATETIME     NOT NULL,
    [UserUpd]    INT          NULL,
    [DateUpd]    DATETIME     NULL,
    [ColumnDemo] VARCHAR (50) NULL,
    CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED ([Id] ASC)
);


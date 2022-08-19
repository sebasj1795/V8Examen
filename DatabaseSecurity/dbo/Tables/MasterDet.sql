CREATE TABLE [dbo].[MasterDet] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [IdMaster]    INT           NOT NULL,
    [Order]       INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (150) NULL,
    [Value]       VARCHAR (100) NOT NULL,
    [Value2]      VARCHAR (100) NULL,
    [Value3]      VARCHAR (100) NULL,
    [State]       TINYINT       NOT NULL,
    [UserCrea]    INT           NOT NULL,
    [DateCrea]    DATETIME      NOT NULL,
    [UserUpd]     INT           NULL,
    [DateUpd]     DATETIME      NULL,
    CONSTRAINT [PK_MasterDet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MasterDet_Master] FOREIGN KEY ([IdMaster]) REFERENCES [dbo].[Master] ([Id])
);


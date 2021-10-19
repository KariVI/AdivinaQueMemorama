
 CREATE TABLE [dbo].[Card] (
        [Id] [int] NOT NULL IDENTITY,
        [description] [nvarchar](max),
        [type] [nvarchar](max),
        [topic] [nvarchar](max),
        CONSTRAINT [PK_dbo.Card] PRIMARY KEY ([Id])
    )

 CREATE TABLE [dbo].[Game] (
        [Id] [int] NOT NULL IDENTITY,
        [date] [nvarchar](max),
        [topic] [nvarchar](max),
        [winner] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Game] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_dbo.AdivinaQue_dbo.Game_winner] FOREIGN KEY ([winner]) REFERENCES [dbo].[Players] ([Id]) 

    )

     CREATE TABLE [dbo].[Participate] (
        [IdPlayer] [int] NOT NULL,
        [score] [int] NOT NULL,
        [IdGame] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Participate] PRIMARY KEY ([IdPlayer], [IdGame]),
        CONSTRAINT [FK_dbo.AdivinaQue_dbo.Participate_IdPlayer] FOREIGN KEY ([IdPlayer]) REFERENCES [dbo].[Players] ([Id]) ,
        CONSTRAINT [FK_dbo.AdivinaQue_dbo.Participate_IdGame] FOREIGN KEY ([IdGame]) REFERENCES [dbo].[Game] ([Id]) 


    )

    
     CREATE TABLE [dbo].[Pair] (
        [IdQuestion] [int] NOT NULL,
        [IdAnswer] [int] NOT NULL,
        CONSTRAINT [FK_dbo.AdivinaQue_dbo.Pair_IdQuestion] FOREIGN KEY ([IdQuestion]) REFERENCES [dbo].[Card] ([Id]) ,
        CONSTRAINT [FK_dbo.AdivinaQue_dbo.Pair_IdAnswer] FOREIGN KEY ([IdAnswer]) REFERENCES [dbo].[Card] ([Id]) 
    )
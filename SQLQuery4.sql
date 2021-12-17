USE [AdivinaQue]
GO

DELETE FROM [dbo].[Score]
      
GO

DELETE FROM [dbo].[Players]
     
GO
DBCC CHECKIDENT('DBO.Players',RESEED,0)
DELETE FROM [dbo].[Participate]

GO

DELETE FROM [dbo].[Participate]
    
GO

DELETE FROM [dbo].[Game]

GO

INSERT INTO [dbo].[Players]
        
     VALUES
           ('Mariana Yazmin Vargas Segura',
           'Marii',
           'MarianaVSYazmin@gmail.com',
            '123',
           null)
GO

INSERT INTO [dbo].[Score]
     VALUES
           ('1',0)
GO
INSERT INTO [dbo].[Players]
        
     VALUES
           ('Mariana Yazmin Vargas Segura',
           'MariV',
           'MarianaVSYazmin@hotmail.com',
            '63640264849a87c90356129d99ea165e37aa5fabc1fea46906df1a7ca50db492',
           null)
GO

INSERT INTO [dbo].[Players]
        
     VALUES
           ('Karina V',
           'Kari',
           'zs19014013@estudiantes.uv.mx',
            '63640264849a87c90356129d99ea165e37aa5fabc1fea46906df1a7ca50db492',
           null)
GO
INSERT INTO [dbo].[Score]
     VALUES
           ('3',0)
GO
INSERT INTO [dbo].[Players]
        
     VALUES
           ('Egy',
           'egy',
           'egy@hotmail.com',
            '63640264849a87c90356129d99ea165e37aa5fabc1fea46906df1a7ca50db492',
           null)
GO
INSERT INTO [dbo].[Score]
     VALUES
           ('4',0)
GO

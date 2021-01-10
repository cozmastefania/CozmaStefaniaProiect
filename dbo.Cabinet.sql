CREATE TABLE [dbo].[Cabinet] (
    [IdCab]    INT IDENTITY (1, 1) NOT NULL,
    [IdAnimal] INT NULL,
    [IdClient] INT NULL,
    PRIMARY KEY CLUSTERED ([IdCab] ASC),
    CONSTRAINT [fk_cabinet_client_id] FOREIGN KEY ([IdClient]) REFERENCES [dbo].[Client] ([IdClient]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [fk_cabinet_animal_id] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animal] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE
);


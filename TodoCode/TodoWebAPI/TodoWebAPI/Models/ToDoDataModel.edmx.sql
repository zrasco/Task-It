
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/25/2017 13:43:28
-- Generated from EDMX file: C:\Users\zebr\Documents\Visual Studio 2015\Projects\TodoWebAPI\TodoWebAPI\Models\ToDoDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModelFirst.ToDoList];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_userstodo_items]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[todo_items] DROP CONSTRAINT [FK_userstodo_items];
GO
IF OBJECT_ID(N'[dbo].[FK_usersachivements]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[achivements] DROP CONSTRAINT [FK_usersachivements];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[todo_items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[todo_items];
GO
IF OBJECT_ID(N'[dbo].[achivements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[achivements];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password_hash] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [access_level] int  NOT NULL
);
GO

-- Creating table 'todo_item'
CREATE TABLE [dbo].[todo_item] (
    [id] int IDENTITY(1,1) NOT NULL,
    [created_date] datetime  NOT NULL,
    [due_date] datetime  NOT NULL,
    [userstodo_items_todo_items_id] int  NOT NULL
);
GO

-- Creating table 'achivements'
CREATE TABLE [dbo].[achivements] (
    [id] int IDENTITY(1,1) NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [usersachivements_achivements_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'todo_item'
ALTER TABLE [dbo].[todo_item]
ADD CONSTRAINT [PK_todo_item]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'achivements'
ALTER TABLE [dbo].[achivements]
ADD CONSTRAINT [PK_achivements]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userstodo_items_todo_items_id] in table 'todo_item'
ALTER TABLE [dbo].[todo_item]
ADD CONSTRAINT [FK_userstodo_items]
    FOREIGN KEY ([userstodo_items_todo_items_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_userstodo_items'
CREATE INDEX [IX_FK_userstodo_items]
ON [dbo].[todo_item]
    ([userstodo_items_todo_items_id]);
GO

-- Creating foreign key on [usersachivements_achivements_id] in table 'achivements'
ALTER TABLE [dbo].[achivements]
ADD CONSTRAINT [FK_usersachivements]
    FOREIGN KEY ([usersachivements_achivements_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersachivements'
CREATE INDEX [IX_FK_usersachivements]
ON [dbo].[achivements]
    ([usersachivements_achivements_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
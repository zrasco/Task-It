
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/25/2017 18:38:16
-- Generated from EDMX file: C:\Users\Zeb\Documents\Visual Studio 2015\Projects\TodoProject\fall2017-todolist\TodoCode\TodoWebAPI\TodoWebAPI\Models\ToDoDataModel.edmx
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
    ALTER TABLE [dbo].[todo_item] DROP CONSTRAINT [FK_userstodo_items];
GO
IF OBJECT_ID(N'[dbo].[FK_usersachivements]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[achivements] DROP CONSTRAINT [FK_usersachivements];
GO
IF OBJECT_ID(N'[dbo].[FK_useruser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_useruser];
GO
IF OBJECT_ID(N'[dbo].[FK_userreminder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[reminders] DROP CONSTRAINT [FK_userreminder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[todo_item]', 'U') IS NOT NULL
    DROP TABLE [dbo].[todo_item];
GO
IF OBJECT_ID(N'[dbo].[achivements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[achivements];
GO
IF OBJECT_ID(N'[dbo].[reminders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[reminders];
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
    [access_level] int  NOT NULL,
    [phone_number] nvarchar(max)  NOT NULL,
    [useruser_user1_id] int  NOT NULL
);
GO

-- Creating table 'tasks'
CREATE TABLE [dbo].[tasks] (
    [id] int IDENTITY(1,1) NOT NULL,
    [created_date] datetime  NOT NULL,
    [due_date] datetime  NOT NULL,
    [summary] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [is_pin] nvarchar(max)  NOT NULL,
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

-- Creating table 'reminders'
CREATE TABLE [dbo].[reminders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [start_datetime] nvarchar(max)  NOT NULL,
    [end_datetime] nvarchar(max)  NOT NULL,
    [threshold_minutes] nvarchar(max)  NOT NULL,
    [is_event] nvarchar(max)  NOT NULL,
    [summary] nvarchar(max)  NOT NULL,
    [userreminder_reminder_id] int  NOT NULL
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

-- Creating primary key on [id] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [PK_tasks]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'achivements'
ALTER TABLE [dbo].[achivements]
ADD CONSTRAINT [PK_achivements]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'reminders'
ALTER TABLE [dbo].[reminders]
ADD CONSTRAINT [PK_reminders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userstodo_items_todo_items_id] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [FK_userstodo_items]
    FOREIGN KEY ([userstodo_items_todo_items_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_userstodo_items'
CREATE INDEX [IX_FK_userstodo_items]
ON [dbo].[tasks]
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

-- Creating foreign key on [useruser_user1_id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [FK_useruser]
    FOREIGN KEY ([useruser_user1_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_useruser'
CREATE INDEX [IX_FK_useruser]
ON [dbo].[users]
    ([useruser_user1_id]);
GO

-- Creating foreign key on [userreminder_reminder_id] in table 'reminders'
ALTER TABLE [dbo].[reminders]
ADD CONSTRAINT [FK_userreminder]
    FOREIGN KEY ([userreminder_reminder_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_userreminder'
CREATE INDEX [IX_FK_userreminder]
ON [dbo].[reminders]
    ([userreminder_reminder_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
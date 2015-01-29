
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2015 17:41:38
-- Generated from EDMX file: C:\Users\jquesad\Documents\GitHub\SmartRoom\SmartRoomSolution\SmartRoom.Model\VTModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SmartRoom];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonClassroomRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassroomRoles] DROP CONSTRAINT [FK_PersonClassroomRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassroomClassroomRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassroomRoles] DROP CONSTRAINT [FK_ClassroomClassroomRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassroomClassroomOptions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Classrooms] DROP CONSTRAINT [FK_ClassroomClassroomOptions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[ClassroomRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassroomRoles];
GO
IF OBJECT_ID(N'[dbo].[Classrooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Classrooms];
GO
IF OBJECT_ID(N'[dbo].[ClassroomOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassroomOptions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [ModiefiedBy] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ClassroomRoles'
CREATE TABLE [dbo].[ClassroomRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [ClassroomId] int  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Classrooms'
CREATE TABLE [dbo].[Classrooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Term] nvarchar(max)  NOT NULL,
    [ClassCode] nvarchar(max)  NOT NULL,
    [ClassName] nvarchar(max)  NOT NULL,
    [Year] int  NOT NULL,
    [Campus] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ClassroomOption_Id] int  NOT NULL
);
GO

-- Creating table 'ClassroomOptions'
CREATE TABLE [dbo].[ClassroomOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClassroomRoles'
ALTER TABLE [dbo].[ClassroomRoles]
ADD CONSTRAINT [PK_ClassroomRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Classrooms'
ALTER TABLE [dbo].[Classrooms]
ADD CONSTRAINT [PK_Classrooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClassroomOptions'
ALTER TABLE [dbo].[ClassroomOptions]
ADD CONSTRAINT [PK_ClassroomOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonId] in table 'ClassroomRoles'
ALTER TABLE [dbo].[ClassroomRoles]
ADD CONSTRAINT [FK_PersonClassroomRoles]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonClassroomRoles'
CREATE INDEX [IX_FK_PersonClassroomRoles]
ON [dbo].[ClassroomRoles]
    ([PersonId]);
GO

-- Creating foreign key on [ClassroomId] in table 'ClassroomRoles'
ALTER TABLE [dbo].[ClassroomRoles]
ADD CONSTRAINT [FK_ClassroomClassroomRoles]
    FOREIGN KEY ([ClassroomId])
    REFERENCES [dbo].[Classrooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassroomClassroomRoles'
CREATE INDEX [IX_FK_ClassroomClassroomRoles]
ON [dbo].[ClassroomRoles]
    ([ClassroomId]);
GO

-- Creating foreign key on [ClassroomOption_Id] in table 'Classrooms'
ALTER TABLE [dbo].[Classrooms]
ADD CONSTRAINT [FK_ClassroomClassroomOptions]
    FOREIGN KEY ([ClassroomOption_Id])
    REFERENCES [dbo].[ClassroomOptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassroomClassroomOptions'
CREATE INDEX [IX_FK_ClassroomClassroomOptions]
ON [dbo].[Classrooms]
    ([ClassroomOption_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
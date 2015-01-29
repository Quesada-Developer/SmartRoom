CREATE TABLE [dbo].[ClassroomRoles] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]     INT            NOT NULL,
    [ClassroomId]  INT            NOT NULL,
    [Role]         NVARCHAR (MAX) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (MAX) NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ClassroomRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassroomClassroomRoles] FOREIGN KEY ([ClassroomId]) REFERENCES [dbo].[Classrooms] ([Id]),
    CONSTRAINT [FK_PersonClassroomRoles] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_PersonClassroomRoles]
    ON [dbo].[ClassroomRoles]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ClassroomClassroomRoles]
    ON [dbo].[ClassroomRoles]([ClassroomId] ASC);


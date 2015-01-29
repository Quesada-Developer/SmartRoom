CREATE TABLE [dbo].[Classrooms] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Term]               NVARCHAR (MAX) NOT NULL,
    [Year]               DATETIME       NOT NULL,
    [Campus]             NVARCHAR (MAX) NOT NULL,
    [CreateDate]         DATETIME       NOT NULL,
    [CreatedBy]          NVARCHAR (MAX) NOT NULL,
    [ModifiedDate]       DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (MAX) NOT NULL,
    [IsActive]           BIT            NOT NULL,
    [ClassroomOption_Id] INT            NOT NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassroomClassroomOptions] FOREIGN KEY ([ClassroomOption_Id]) REFERENCES [dbo].[ClassroomOptions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ClassroomClassroomOptions]
    ON [dbo].[Classrooms]([ClassroomOption_Id] ASC);


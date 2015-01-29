CREATE TABLE [dbo].[ClassroomOptions] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (MAX) NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ClassroomOptions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


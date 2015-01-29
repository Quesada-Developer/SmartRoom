CREATE TABLE [dbo].[People] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (MAX) NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [ModiefiedBy]  NVARCHAR (MAX) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([Id] ASC)
);


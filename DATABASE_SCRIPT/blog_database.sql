SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BLOG_User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginId] [varchar](10) NOT NULL,
	[userName] [varchar](20) NOT NULL,
	[isActive] [bit] NOT NULL,
	[userKey] [varchar](10) NOT NULL,
	[roleName] [varchar](10) NOT NULL,
 CONSTRAINT [UC_BLOG_User] UNIQUE NONCLUSTERED 
(
	[loginId] ASC,
	[userKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[BLOG_Post]    Script Date: 15-03-2021 23:24:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BLOG_Post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[blog_title] [varchar](200) NOT NULL,
	[blog_Description] [varchar](1000) NOT NULL,
	[isActive] [bit] NOT NULL,
	[createdBy] [int] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[updatedBy] [int] NULL,
	[updatedOn] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BLOG_Post] ADD  DEFAULT (getdate()) FOR [createdOn]
GO




/****** Object:  StoredProcedure [dbo].[proc_AddEditBlog]    Script Date: 15-03-2021 23:25:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[proc_AddEditBlog]
(

@Id int,
@BlogTitle varchar(200),
@BlogDescription varchar(200),
@CreatedBy int
)
AS
BEGIN	
	IF(@Id>0)
	BEGIN
			UPDATE BLOG_Post SET 
			blog_title = @BlogTitle,
			blog_Description = @BlogDescription,
			isActive= 1,
			updatedBy = @CreatedBy,
			updatedOn = GETDATE() WHERE id = @Id 
	
	END
	ELSE
	BEGIN
			INSERT INTO BLOG_Post (blog_title,blog_Description,isActive,createdBy,createdOn)
			VALUES(@BlogTitle,@BlogDescription,1,@CreatedBy,GETDATE()) 
	END	
END
GO



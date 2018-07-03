USE [TRAMITE]
GO

/****** Object:  Table [dbo].[tProcessoMovimento]    Script Date: 26/06/2018 11:55:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tProcessoMovimento](
	[IdProcessoMovimento] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProcesso] [bigint] NULL,
	[DtMovimentacao] [datetime] NULL,
	[NuMatriculaAutor] [bigint] NULL,
	[CdMovimento] [bigint] NULL,
	[DsComplemento] [text] NULL,
	[IdOrgaoUnidadeOrigem] [bigint] NULL,
	[NuMatriculaOrigem] [bigint] NULL,
	[IdOrgaoUnidadeDestino] [bigint] NULL,
	[NuMatriculaDestino] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProcessoMovimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tProcessoMovimento]  WITH CHECK ADD  CONSTRAINT [FK8633B7D28B4F9CB9] FOREIGN KEY([IdProcesso])
REFERENCES [dbo].[tProcesso] ([IdProcesso])
GO

ALTER TABLE [dbo].[tProcessoMovimento] CHECK CONSTRAINT [FK8633B7D28B4F9CB9]
GO


ALTER TABLE [dbo].[tProcesso]  WITH CHECK ADD  CONSTRAINT [FK_tProcessoMovimento_CdUltimoMovimento] FOREIGN KEY([CdUltimoMovimento])
REFERENCES [dbo].[tProcessoMovimento] ([IdProcessoMovimento])
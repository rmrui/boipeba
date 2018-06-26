USE [TRAMITE]
GO

/****** Object:  Table [dbo].[tProcesso]    Script Date: 26/06/2018 11:56:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tProcesso](
	[IdProcesso] [bigint] IDENTITY(1,1) NOT NULL,
	[DtCadastro] [datetime] NULL,
	[NuSimp] [text] NULL,
	[StUrgente] [bit] NULL,
	[StReservado] [bit] NULL,
	[NmInteressado] [text] NULL,
	[IdOuInteressado] [bigint] NULL,
	[StSociedadeInteressada] [bit] NULL,
	[CdAssunto] [bigint] NULL,
	[DsComplementoAssunto] [text] NULL,
	[NuMatriculaAutor] [bigint] NULL,
	[NuMatriculaDestino] [bigint] NULL,
	[IdOuDestino] [bigint] NULL,
	[DtUltimaModificacao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProcesso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



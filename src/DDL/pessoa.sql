USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_Pessoa]    Script Date: 26/06/2018 10:34:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_Pessoa]
AS
SELECT        f.NuMatricula, f.NmFuncionario AS NmPessoa, l.IdCentroCusto AS IdOuLotacao, CASE WHEN f.DsSituacao LIKE 'INAT%' OR
                         f.DsSituacao LIKE 'RESC%' OR
                         f.DsSituacao LIKE 'EXON%' THEN 0 ELSE 1 END AS StAtivo
FROM            GLOBAL.rh.vw_DS71_Funcionario AS f INNER JOIN
                         GLOBAL.rh.tLotacao AS l ON l.IdLotacao = f.IdLotacao

GO
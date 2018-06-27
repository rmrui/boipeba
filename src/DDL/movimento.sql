USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_Movimento]    Script Date: 27/06/2018 10:02:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[vw_Movimento]
AS

SELECT        m.CdMovimento, i.NmItemTree AS DsMovimento, i.NmItem as DsMovimentoSimples, m.DsGlossario, i.StSituacao AS StAtivo, i.CdPath
FROM            IDEA_R3_C1_05062018.dbo.tMovimento AS m 
INNER JOIN IDEA_R3_C1_05062018.dbo.tItem AS i ON i.CdItem = m.CdMovimento
LEFT JOIN IDEA_R3_C1_05062018.dbo.tItem AS filho ON i.CdPath = filho.CdPathPai
WHERE        
filho.CdItem IS NULL
AND(
i.CdPath.IsDescendantOf((select i.CdPath
from IDEA_R3_C1_05062018.dbo.tMovimento m
join IDEA_R3_C1_05062018.dbo.tItem i
	on m.CdMovimento = i.CdItem
where CdMovimento = 920280)) = 1
OR
i.CdPath.IsDescendantOf((select i.CdPath
from IDEA_R3_C1_05062018.dbo.tMovimento m
join IDEA_R3_C1_05062018.dbo.tItem i
	on m.CdMovimento = i.CdItem
where CdMovimento = 920282)) = 1
)




GO
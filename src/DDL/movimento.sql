USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_Movimento]    Script Date: 26/06/2018 11:56:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vw_Movimento]
AS

SELECT        m.CdMovimento, i.NmItemTree AS DsMovimento, i.StSituacao AS StAtivo, i.CdPath
FROM            IDEA_R3_C1_05062018.dbo.tMovimento AS m INNER JOIN
                         IDEA_R3_C1_05062018.dbo.tItem AS i ON i.CdItem = m.CdMovimento
WHERE        
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



GO
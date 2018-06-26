USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_Classe]    Script Date: 26/06/2018 10:33:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vw_Classe]
AS

select c.CdClasse, i.NmItemTree as DsClasse, i.StSituacao as StAtivo, i.CdPath
from IDEA_R3_C1_05062018.dbo.tClasse c
join IDEA_R3_C1_05062018.dbo.tItem i on i.CdItem = c.CdClasse
where i.CdPath.IsDescendantOf((
select CdPath from IDEA_R3_C1_05062018.dbo.tItem
where CdItem = 910019
)) = 1




GO



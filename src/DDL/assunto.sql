USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_Assunto]    Script Date: 26/06/2018 10:33:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_Assunto]
AS
SELECT        a.CdAssunto, i.NmItemTree AS DsAssunto, i.StSituacao AS StAtivo, i.CdPath
FROM            IDEA_R3_C1_05062018.dbo.tAssunto AS a INNER JOIN
                         IDEA_R3_C1_05062018.dbo.tItem AS i ON a.CdAssunto = i.CdItem
WHERE        (a.CdAssunto > 930000)

GO
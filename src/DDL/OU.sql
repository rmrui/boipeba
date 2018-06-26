USE [TRAMITE]
GO

/****** Object:  View [dbo].[vw_OrgaoUnidade]    Script Date: 26/06/2018 10:34:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_OrgaoUnidade]
AS
SELECT        IdOrgaoUnidade, DsOrgaoUnidadeCompletaMunicipio AS DsOrgaoUnidade, DsTpOU, StProcProm, StTpOE, CdMunicipio, NmMunicipio, IdComarca, 
                         CdMunicipioSedeCom, NmMunicipioSedeCom, IdRegional, IdTpRegional, DsTpRegional, CdMunicipioSedeReg, NmMunicipioSedeReg, IdRegionalEsp, 
                         IdTpRegionalEsp, DsTpRegionalEsp, CdMunicipioSedeRegEsp, NmMunicipioSedeRegEsp, Atributos, StAtivo, IdTpOU, IdPathPai, DsSigla, IdPath
FROM            GLOBAL.OU.vw_DS01_OUCompleto_MDM
WHERE        (StTpOE = 0)

GO

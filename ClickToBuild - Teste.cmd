"C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" build.config /target:Integrate  /property:^
Configuration=Release

del "\\bespin.intranet.mpba.mp.br\Aplicacoes\SIGAEV_Desenvolvimento\*" /s /q

xcopy "_build\_PublishedWebsites\Boipeba.Web"\*" "\\bespin.intranet.mpba.mp.br\Aplicacoes\SIGAEV_Desenvolvimento\" /y

if errorlevel 1 pause
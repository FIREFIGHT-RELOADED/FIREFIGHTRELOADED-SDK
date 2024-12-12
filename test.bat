SET copypath=G:\Projects\GitHub\FIREFIGHTRELOADED-SDK\build
if not exist "%copypath%" mkdir "%copypath%"
xcopy "G:\Projects\GitHub\FIREFIGHTRELOADED-SDK\MapCompiler\bin\Release\net6.0-windows\*.*" "%copypath%" /e
pause
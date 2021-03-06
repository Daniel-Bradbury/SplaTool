@echo off
echo Initialising...
rd /S /Q "%cd%\BUILD"
echo Compiling binaries...
start /B csc -out:SplaTools.exe SplaToolsMenu\*.cs >nul
processing-java --sketch="%cd%\FestTool" --no-java --export --platform windows >nul
"c:\Program Files\AutoHotkey\Compiler\Ahk2Exe.exe" /in "SplaToolsMenu\Zoom.ahk" /out "SplaToolsMenu\Zoom32.exe" /bin "c:\Program Files\AutoHotkey\Compiler\Unicode 32-bit.bin"
"c:\Program Files\AutoHotkey\Compiler\Ahk2Exe.exe" /in "SplaToolsMenu\Zoom.ahk" /out "SplaToolsMenu\Zoom64.exe" /bin "c:\Program Files\AutoHotkey\Compiler\Unicode 64-bit.bin"
echo Moving binaries into build folder...
cd FestTool
ren application.windows32 FestTool32
ren application.windows64 FestTool64
cd ..
mkdir BUILD
cd BUILD
mkdir SplaTools64
mkdir SplaTools32
cd SplaTools32
mkdir data
cd ..
cd SplaTools64
mkdir data
cd ..
cd ..
move FestTool\FestTool32 BUILD\SplaTools32\FestTool
move FestTool\FestTool64 BUILD\SplaTools64\FestTool
copy SplaTools.exe BUILD\SplaTools32
move SplaTools.exe BUILD\SplaTools64
move SplaToolsMenu\Zoom32.exe BUILD\SplaTools32\data
move SplaToolsMenu\Zoom64.exe BUILD\SplaTools64\data
ren BUILD\SplaTools64\data\Zoom64.exe Zoom.exe
ren BUILD\SplaTools32\data\Zoom32.exe Zoom.exe
echo Cleaning up...
rd /S /Q "%cd%\BUILD\SplaTools32\FestTool\source"
rd /S /Q "%cd%\BUILD\SplaTools64\FestTool\source"
echo Done! exiting in 3 seconds...
ping 127.0.0.1 -n 4 > nul
call compiler\getPID.bat >nul
taskkill /pid %errorlevel%
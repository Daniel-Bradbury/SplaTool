@echo off
echo Initialising...
rd /S /Q "%cd%\BUILD"
echo Compiling binaries...
start /B csc -out:SplaTools.exe SplaToolsMenu\*.cs >nul
processing-java --sketch="%cd%\FestTool" --no-java --export --platform windows >nul
"c:\Program Files\AutoHotkey\Compiler\Ahk2Exe.exe" /in "SplaToolsMenu\Zoom.ahk"
echo Moving binaries into build folder...
cd FestTool
ren application.windows32 FestTool32
ren application.windows64 FestTool64
cd ..
mkdir BUILD
cd BUILD
mkdir 64
mkdir 32
cd 32
mkdir data
cd ..
cd 64
mkdir data
cd ..
cd ..
move FestTool\FestTool32 BUILD\32\FestTool
move FestTool\FestTool64 BUILD\64\FestTool
copy SplaTools.exe BUILD\32
move SplaTools.exe BUILD\64
copy SplaToolsMenu\Zoom.exe BUILD\32\data
move SplaToolsMenu\Zoom.exe BUILD\64\data
echo Cleaning up...
rd /S /Q "%cd%\BUILD\32\FestTool\source"
rd /S /Q "%cd%\BUILD\64\FestTool\source"
echo Done!
rem ROBOCOPY node_modules node_modules /S /MOVE
rmdir /s /q node_modules tmp typings
del /q /s node_modules tmp typings
rem rmdir /s /q dist 
rem del /q /s dist
rem rmdir /s /q tmp 
rem del /q /s tmp

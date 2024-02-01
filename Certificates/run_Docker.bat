@ECHO OFF
taskkill /f /t /im "ZSATray.exe" > nul
set mSpinner=-

:LOOP
  Rem Draw spinner
  cls
  echo.
  echo  ZSA killing in progress... %mSpinner%  
  echo.
  
  Rem Kill ZSA
  taskkill /IM ZSA* /f > nul
  
  Rem Update spinner
  if %mSpinner%==- (set mSpinner=/& GOTO NEXT)
  if %mSpinner%==/ (set mSpinner=^^^|& GOTO NEXT)
  if %mSpinner%==^| (set mSpinner=^\& GOTO NEXT)
  if %mSpinner%==^\ (set mSpinner=-& GOTO NEXT)
  
:NEXT
  Rem Delay
  ping -n 2 127.0.0.1 > nul
  GOTO LOOP


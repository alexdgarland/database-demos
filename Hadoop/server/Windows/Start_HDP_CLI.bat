cmd /k IF EXIST "C:\hdp\GettingStarted\init.cmd" ( "C:\hdp\GettingStarted\init.cmd" ) ELSE ( "C:\hdp\hadoop-2.4.0.2.1.3.0-1981\bin\hadoop.cmd" ) && cd %DBDEMO%\Hadoop\server\Windows
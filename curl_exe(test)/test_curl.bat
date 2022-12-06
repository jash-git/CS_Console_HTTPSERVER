REM UTF-8
chcp 65001

@echo off

for /l %%x in (1, 1, 1000) do (
	echo %%x
    curl.exe http://127.0.0.1:8001
    timeout 1
)

pause
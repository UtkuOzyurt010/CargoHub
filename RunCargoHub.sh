#! /usr/bin/bash

#terminate if already active
kill $(pgrep -f "dotnet run --project /home/ubuntu-1079726/ubuntu-1079726/CargoHub/CargoHub/CargoHub.csproj")
#Re-run API
nohup dotnet run --project /home/ubuntu-1079726/ubuntu-1079726/CargoHub/CargoHub/CargoHub.csproj > /dev/null 2>&1 &

#echo "${{secrets.SERVERPASSWORD}}" | sudo 

#dotnet run --project /home/ubuntu-1079726/ubuntu-1079726/CargoHub/CargoHub/CargoHub.csproj # this works but isn't on background
#CargoHub/bin/Debug/net8.0/CargoHub.exe
#C:\VSCodeProjects\CargoHub\CargoHub\bin\Debug\net8.0\CargoHub.exe
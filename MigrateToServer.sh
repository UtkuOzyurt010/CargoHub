

cd ..
scp scp -r CargoHub ubuntu-1079726@145.24.223.236:~/ubuntu-1079726
ssh ubuntu-1079726@145.24.223.236
#these 2 steps could maybe be replaced with just "dotnet run"
cd ubuntu-1079726/CargoHub/CargoHub 
nohup bash RunCargoHub.sh &

    - name: Set up SSH keys
      uses: webfactory/ssh-agent@v0.5.3
      with:
        ssh-private-key: ${{ secrets.SSH_PRIVATE_KEY }}

    # - name: Ping the server
    #   run: ping -c 4 145.24.223.236
    - name: Test SSH connection
      run: |
        nc -zv 145.24.223.236 8000
    # Step 3: Copy the repository to the remote server using SCP
    - name: SCP repository to server
      run: |
        scp -r ./api ubuntu-1079726@145.24.223.236:~/ubuntu-1079726/Cargohub
        scp -r ./data ubuntu-1079726@145.24.223.236:~/ubuntu-1079726/Cargohub
        scp ./main.py ubuntu-1079726@145.24.223.236:~/ubuntu-1079726/Cargohub
      env:
        PRIVATE_KEY: ${{ secrets.SSH_PRIVATE_KEY }}


cd ..
scp scp -r CargoHub ubuntu-1079726@145.24.223.236:~/ubuntu-1079726
ssh ubuntu-1079726@145.24.223.236
#these 2 steps could maybe be replaced with just "dotnet run"
cd ubuntu-1079726/CargoHub/CargoHub 
nohup bash RunCargoHub.sh &
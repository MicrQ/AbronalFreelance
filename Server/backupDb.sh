#!/usr/bin/env bash
# script to backup my AbronalFreelance db

name=$(date +%Y-%m-%d-%H-%M-%S)
echo "BACKUP DATABASE AbronalFreelance TO DISK='$name.bak'" | sqlcmd -S localhost -U sa -P MicrQ123 -C

sudo cp /var/opt/mssql/data/$name.bak ~/AbronalFreelance/DatabaseBackups/.
sudo chmod 666 ~/AbronalFreelance/DatabaseBackups/$name.bak

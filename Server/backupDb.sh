#!/usr/bin/env bash
# script to backup my AbronalFreelance db

name=$(date +%Y-%m-%d-%H-%M-%S)
echo "BACKUP DATABASE AbronalFreelance TO DISK='$name.bak'" | sqlcmd -S MicrQ -U sa -P MicrQ123 -C

sudo cp /var/opt/mssql/data/$name.bak ~/AbronalFreelance/.
sudo chmod 666 ~/AbronalFreelance/$name.bak

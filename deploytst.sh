#!/bin/bash
########################################################

## Shell Script to Build Docker Image and run.   

########################################################


result=$( docker images -q apigetway )
if [[ -n "$result" ]]; then
echo "image exists"
 docker rmi -f apigetway
else
echo "No such image"
fi

echo "build the docker image"
echo "built docker images and proceeding to delete existing container"

result=$( docker ps -q -f name=apigetway )
if [[ $? -eq 0 ]]; then
echo "Container exists"
 docker container rm -f apigetway
echo "Deleted the existing docker container"
else
echo "No such container"
fi

cp -a /home/francisco/estudos/azuredevops/myagent/_work/10/s/.  /home/francisco/estudos/azuredevops/myagent/_work/r9/a/

docker build -t apigetway .

echo "built docker images and proceeding to delete existing container"
echo "Deploying the updated container"

docker run --name apigetway -d -p 5000:5000  --link orderapi  apigetway

echo "Deploying the container"
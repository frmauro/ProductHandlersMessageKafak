#!/bin/bash
########################################################

## Shell Script to Build Docker Image and run.   

########################################################


result=$( docker images -q producthandlersmessagekafak )
if [[ -n "$result" ]]; then
echo "image exists"
 docker rmi -f producthandlersmessagekafak
else
echo "No such image"
fi

echo "build the docker image"
echo "built docker images and proceeding to delete existing container"

result=$( docker ps -q -f name=producthandlersmessagekafak )
if [[ $? -eq 0 ]]; then
echo "Container exists"
 docker container rm -f producthandlersmessagekafak
echo "Deleted the existing docker container"
else
echo "No such container"
fi

cp -a /home/francisco/estudos/azuredevops/myagent/_work/15/s/.  /home/francisco/estudos/azuredevops/myagent/_work/r14/a/

docker build -t producthandlersmessagekafak .

echo "built docker images and proceeding to delete existing container"
echo "Deploying the updated container"

docker run --name producthandlersmessagekafak -d --link kafka --link sql1 producthandlersmessagekafak

echo "Deploying the container"
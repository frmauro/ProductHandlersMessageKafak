# ProductHandlersMessageKafak

## Create image docker
docker build --tag producthandlersmessagekafak .

# create container
docker run --name producthandlersmessagekafak -d --link kafka --link sql1 producthandlersmessagekafak

## types connections strings
# local
"myConnection": "Server=127.0.0.1,1433;Database=productApi;User Id=sa;Password=Mau123&&&"

# Docker
"myConnection": "Server=sql1,1433;Database=productApi;User Id=sa;Password=Mau123&&&"


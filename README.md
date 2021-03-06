## create image docker command

docker build --tag apigetway .

## create docker container command

docker run --name apigetway -d -p 5000:5000 --link salesusernode --link salesproductapi --link orderapi --link kafka apigetway

## many another commands

curl http://localhost:5000/GetAllOrders

# apigetwaysalesorder

curl -X POST -H "Content-Type: application/json" -d '{"items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/UpdateAmount

curl -X POST -H "Content-Type: application/json" -d '{"items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://salesorder.com/UpdateAmount

# verificar qual a porta exata

curl -X POST -H "Content-Type: application/json" -d '{"id": 0, "description": "Order 013", "status": "1", "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://salesorder.com/createOrder

curl -X PUT -H "Content-Type: application/json" -d '{"id": 1, "description": "Order 000001", "status": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://salesorder.com/UpdateOrder/1

## create product request POST via CURL

curl -X POST -H "Content-Type: application/json" -d '{"Description": "Product 005", "Amount": 250, "Status": "Active", "Price": "600"}' http://localhost:5158/createProduct

## update product request PUT via CURL

curl -X PUT -H "Content-Type: application/json" -d '{"Id": 1, "Description": "Product 001", "Amount": 250, "Status": "Active", "Price": "600"}' http://localhost:5158/UpdateProduct

curl -X PUT -H "Content-Type: application/json" -d '{"Id": 1, "Description": "Product 001", "Amount": 200, "Status": "Active", "Price": "600"}' http://salesorder.com/UpdateProduct

## user autenticate request POST via CURL

curl -X POST -H "Content-Type: application/json" -d '{"Email": "frmauro8@gmail.com", "Password": "123"}' http://localhost:5158/FindUserByEmailAndPassword

curl -X POST -H "Content-Type: application/json" -d '{"Email": "frmauro8@gmail.com", "Password": "123"}' http://salesorder.com/findUserByEmailAndPassword

## create user request POST via CURL

curl -X POST -H "Content-Type: application/json" -d '{"Name": "Joca Silva", "Email": "jocas@gmail.com", "Password": "123", "UserType": "Client", "Status": "Active"}' http://localhost:5158/createUser

## update user request PUT via CURL

curl -X PUT -H "Content-Type: application/json" -d '{"id": "61d7058ea8537e2b468f8d75", "Name": "Joca Silva", "Email": "jocas@gmail.com", "Password": "123", "UserType": "Client", "Status": "Active"}' http://localhost:5158/updateUser

curl -X PUT -H "Content-Type: application/json" -d '{"id": "615df4928dc8279ab1357f89", "Name": "Alexandre Silva", "Email": "as001@gmail.com", "Password": "123", "UserType": "Client", "Status": "Active"}' http://salesorder.com/updateUser

## command CURL via POST (order)

curl -X POST -H "Content-Type: application/json" -d '{"id": 0, "description": "Order 004", "orderStatus": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/CreateOrder

## command CURL via PUT (order)

curl -X PUT -H "Content-Type: application/json" -d '{"id": 4, "description": "Order 004", "orderStatus": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 4, "description": "Product 001", "quantity": 1, "price": 450, "productId": 1}]}' http://localhost:5158/UpdateOrder

## grant permision to my user

sudo chown -R francisco:francisco /tmp/NuGetScratch/

## get messages of especific topic in Kafka container docker

docker exec samples_kafka_1 /opt/kafka/bin/kafka-console-consumer.sh --bootstrap-server localhost:9092 --from-beginning --topic fila_produto

## types of serviceurl and port orderapi

    // use from local to docker container without compose
    //private const string SERVICEURL = "127.0.0.1";
    //private const string SERVICEURL = "172.17.0.6";
    // use from container to docker container without compose
    //private const string SERVICEURL = "orderapi";


    // use from container to docker container with compose
    //private const string SERVICEURL = "order-api";
    // use for service kubernetes (Minikube)
        //private const string SERVICEURL = "orderapigrpc";



## types of serviceurl and port salesusernode
        //Local PORT 
        private int PORT = 50051;

        //container PORT 
        //private int PORT = 9090;

        // use from local to docker container without compose
        //private String SERVICEURL = "127.0.0.1";
        //private String SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        private String SERVICEURL = "salesusernode";
        
        //private String SERVICEURL = "salesusernode";
        // use from container to docker container with compose
        //private String SERVICEURL = "user-api";
        // use for service kubernetes
        //private String SERVICEURL = "apiusergrpc";




## types of serviceurl and port salesproductapi
        //Local PORT 
        //private int PORT = 5000;

        //container PORT 
        private const int PORT = 9091;

        // use from local to docker container without compose
        //private const string SERVICEURL = "http://127.0.0.1:";
        //private const string SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        private const string SERVICEURL = "salesproductapi";
        // use from container to docker container with compose
        //private const string SERVICEURL = "product-api";
        // use for service kubernetes
        //private const string SERVICEURL = "productapigrpc";



## command to grant full access in folder ubunto linux
sudo chown -R $USER <folder>        



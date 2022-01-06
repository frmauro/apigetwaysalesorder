# apigetwaysalesorder

curl -X POST -H "Content-Type: application/json" -d '{"items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/UpdateAmount

# verificar qual a porta exata
curl -X POST -H "Content-Type: application/json" -d '{"id": 0, "description": "Order 001", "orderStatus": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/CreateOrder


curl -X PUT -H "Content-Type: application/json" -d '{"id": 1, "description": "Order 001", "orderStatus": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/UpdateOrder/1


## create product request POST via CURL
 curl -X POST -H "Content-Type: application/json" -d '{"Description": "Product 005", "Amount": 250, "Status": "Active", "Price": "600"}' http://localhost:5158/createProduct

 ## update product request PUT via CURL
 curl -X PUT -H "Content-Type: application/json" -d '{"Id": 1, "Description": "Product 001", "Amount": 250, "Status": "Active", "Price": "600"}' http://localhost:5158/UpdateProduct



## user autenticate request POST via CURL
 curl -X POST -H "Content-Type: application/json" -d '{"Email": "frmauro8@gmail.com", "Password": "123"}' http://localhost:5158/FindByEmailAndPassword


## create user request POST via CURL
 curl -X POST -H "Content-Type: application/json" -d '{"Name": "Joca Silva", "Email": "jocas@gmail.com", "Password": "123", "UserType": "Client", "Status": "Active"}' http://localhost:5158/create




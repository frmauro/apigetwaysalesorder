# apigetwaysalesorder

curl -X POST -H "Content-Type: application/json" -d '[{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]' http://localhost:5157/UpdateAmount

# verificar qual a porta exata
curl -X POST -H "Content-Type: application/json" -d '{"id": 0, "description": "Order 001", "orderStatus": 1, "userId": "611aa80245c2ed2212c3ec3d", "items": [{"id": 1, "description": "Product 001", "quantity": 1, "price": 200, "productId": 1}]}' http://localhost:5158/CreateOrder
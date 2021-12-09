using ApiGetwaySalesOrder.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//builder.Services.addGrp

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();



//user autentication
app.MapGet("/getByEmailAndPassword", () =>
{
    return new UserAuthDto("611aa80245c2ed2212c3ec3d", "frmauro8@gmail.com", "123", "99999999999");

})
.WithName("GetByEmailAndPassword");


//get all product
app.MapGet("/getAllProduct", () =>
{
    return new List<ProductDto>() { new ProductDto(1, "Product 001", 195, "Active", 200.0), new ProductDto(1, "Product 002", 200, "Active", 300.0) }.ToArray();

})
.WithName("GetAllProduct");

//get product By Id
app.MapGet("/getProductById/{id}", (int id) =>
{
    return new ProductDto(1, "Product 001", 195, "Active", 200.0);

})
.WithName("GetProductById");


//update amount product 
app.MapPost("/updateAmount", (List<ProductDto> items) =>
{
    return items.ToArray();

})
.WithName("UpdateAmount");



//get all orders
app.MapGet("/getAllOrders", () =>
{
    return new List<OrderDto>() { new OrderDto(1, "Order 001", "2020-07-20T19:53:07Z", 1, "1", new List<OrderItemDto>() {
        new OrderItemDto(1, "Product 001", 1, 200.0), new OrderItemDto(2, "Product 002", 1, 300.0)
    }), new OrderDto(1, "Order 002", "2020-07-20T19:53:07Z", 1, "1", new List<OrderItemDto>() {
        new OrderItemDto(1, "Product 001", 1, 200.0), new OrderItemDto(2, "Product 002", 1, 300.0)
    }) }.ToArray();

})
.WithName("GetAllOrders");



//get order by id
app.MapGet("/getOrderById/{id}", (int id) =>
{
    return new OrderDto(1, "Order 001", "2020-07-20T19:53:07Z", 1, "1", new List<OrderItemDto>() {
        new OrderItemDto(1, "Product 001", 1, 200.0), new OrderItemDto(2, "Product 002", 1, 300.0)
    });

})
.WithName("GetOrderById");


//create order 
app.MapPost("/createOrder", (OrderDto order) =>
{
    order.Id = 1;
    return order.Id;

})
.WithName("CreateOrder");


//update order 
app.MapPut("/updateOrder/{id}", (int id, OrderDto order) =>
{
    return id;

})
.WithName("UpdateOrder");



app.Run();


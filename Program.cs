using ApiGetwaySalesOrder.Dtos;
using ApiGetwaySalesOrder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddSingleton<ProductServiceGRPC>();
builder.Services.AddSingleton<UserServiceGRPC>();
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



// ******************************* START COMUNICATION WITH API USER **********************************************************
//user autentication
app.MapPost("/findUserByEmailAndPassword", (UserEmailPasswordDto dto, UserServiceGRPC serviceGRPC) =>
{
    var result = serviceGRPC.GetByEmailAndPassword(dto);
    //new UserAuthDto("611aa80245c2ed2212c3ec3d", "frmauro8@gmail.com", "123", "99999999999");
    return result;
})
.WithName("FindUserByEmailAndPassword");

//gel all users
app.MapGet("/users", (UserServiceGRPC serviceGRPC) => 
{
    var users = serviceGRPC.GetUsers(new SalesUserApi.Empty());
    return users;
});

//get user by id
app.MapGet("/GetUserById/{id}", (string id, UserServiceGRPC serviceGRPC) => 
{
    SalesUserApi.UserRequestId request = new SalesUserApi.UserRequestId();
    request.Id = id;
    var user = serviceGRPC.Get(request);
    return user;
});

//create user
app.MapPost("/CreateUser", (UserCreateDto dto, UserServiceGRPC serviceGRPC) => 
{
    var user = serviceGRPC.Create(dto);
    return user;
});

//update user
app.MapPut("/UpdateUser", (UserUpdateDto dto, UserServiceGRPC serviceGRPC) => 
{
    var user = serviceGRPC.Update(dto);
    return user;
});

// ******************************* END COMUNICATION WITH API PRODUCT **********************************************************




// ******************************* START COMUNICATION WITH API PRODUCT **********************************************************
//get all product
app.MapGet("/getAllProduct", (ProductServiceGRPC serviceGRPC) =>
{
    var products = serviceGRPC.GetProducts(new SalesProductApi.Empty());
    //return new List<ProductDto>() { new ProductDto(1, "Product 001", 195, "Active", 200.0), new ProductDto(1, "Product 002", 200, "Active", 300.0) }.ToArray();
    new List<ProductDto>() { new ProductDto(1, "Product 001", 195, "Active", 200.0), new ProductDto(1, "Product 002", 200, "Active", 300.0) }.ToArray();
    return products;

})
.WithName("GetAllProduct");

//get product By Id
app.MapGet("/getProductById/{id}", (int id, ProductServiceGRPC serviceGRPC) =>
{
    var product = serviceGRPC.GetProductById(id);
    new ProductDto(1, "Product 001", 195, "Active", 200.0);
    return product;

})
.WithName("GetProductById");



//create product 
app.MapPost("/createProduct", (ProductDto dto, ProductServiceGRPC serviceGRPC) =>
{
    var id = serviceGRPC.InsertProduct(dto);
    new ProductDto(1, "Product 001", 195, "Active", 200.0);
    return id;

})
.WithName("CreateProduct");


//update product 
app.MapPut("/updateProduct", (ProductDto dto, ProductServiceGRPC serviceGRPC) =>
{
    var id = serviceGRPC.UpdateProduct(dto);
    //new ProductDto(1, "Product 001", 195, "Active", 200.0);
    return id;

})
.WithName("UpdateProduct");



//update amount product 
app.MapPost("/updateAmount", (UpdateAmountDto dto, ProductServiceGRPC serviceGRPC) =>
{
    var result = serviceGRPC.UpdateAmount(dto);
    return result;
})
.WithName("UpdateAmount");
// ******************************* END COMUNICATION WITH API PRODUCT **********************************************************



// ******************************* START COMUNICATION WITH API ORDER **********************************************************
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
// ******************************* END COMUNICATION WITH API ORDER **********************************************************



app.Run();


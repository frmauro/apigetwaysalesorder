using ApiGetwaySalesOrder.Dtos;
using ApiGetwaySalesOrder.Services;
using System.Text.Json;


string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var strserviceurlorderapi = builder.Configuration.GetSection("serviceurlorderapi").Value;
var strorderapiport = builder.Configuration.GetSection("orderapiport").Value;
builder.Services.AddSingleton<OrderServiceGRPC>(_ => new OrderServiceGRPC(strserviceurlorderapi, strorderapiport));

var strsalesproductapi = builder.Configuration.GetSection("salesproductapi").Value;
var strsalesproductapiport = builder.Configuration.GetSection("salesproductapiport").Value;
builder.Services.AddSingleton<ProductServiceGRPC>(_ => new ProductServiceGRPC(strsalesproductapi, strsalesproductapiport));

builder.Services.AddSingleton<ProductServiceKAFKA>();

var strserviceurlsalesusernode = builder.Configuration.GetSection("serviceurlsalesusernode").Value;
var strsalesusernodeport = builder.Configuration.GetSection("salesusernodeport").Value;
builder.Services.AddSingleton<UserServiceGRPC>(_ => new UserServiceGRPC(strserviceurlsalesusernode, strsalesusernodeport));


//builder.Services.addGrp

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins("*");
      });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();


// ******************************* START COMUNICATION WITH API TESTE **********************************************************
app.MapGet("/tst", () =>
{
    return "TESTE OK";
});

app.MapGet("/teste/{id}", (string id) =>
{
    return $"TESTE OK - {id} !";
});


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
app.MapGet("/users", async (UserServiceGRPC serviceGRPC) =>
{
    var users = await serviceGRPC.GetUsers(new SalesUserApi.Empty());
    return users;
});

//get user by id
app.MapGet("/GetUserById/{id}", (string id, UserServiceGRPC serviceGRPC) =>
{
    SalesUserApi.UserRequestId request = new SalesUserApi.UserRequestId();
    request.Id = id;
    var user = serviceGRPC.Get(request);
    return user;
    // var user = serviceGRPC.Get(request);
    // return request.Id;
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
    //var user = new UserUpdateDto();
    var user = serviceGRPC.Update(dto);
    return user;
});

// ******************************* END COMUNICATION WITH API PRODUCT **********************************************************




// ******************************* START COMUNICATION WITH API PRODUCT **********************************************************
//get all product
app.MapGet("/getAllProduct", (ProductServiceGRPC serviceGRPC) =>
{
    var productsDtos = serviceGRPC.GetProducts(new SalesProductApi.Empty());
    //return new List<ProductDto>() { new ProductDto(1, "Product 001", 195, "Active", 200.0), new ProductDto(1, "Product 002", 200, "Active", 300.0) }.ToArray();
    //var productsDtos = new List<ProductDto>();
    return productsDtos.ToArray();
})
.WithName("GetAllProduct");

//get product By Id
app.MapGet("/getProductById/{id}", (int id, ProductServiceGRPC serviceGRPC) =>
{
    var product = serviceGRPC.GetProductById(id).Result;
    new ProductDto(product.Id, product.Description, Convert.ToInt32(product.Amount), product.Status, Convert.ToDouble(product.Price));
    return product;
})
.WithName("GetProductById");



//create product 
app.MapPost("/createProduct", (ProductDto dto, ProductServiceGRPC serviceGRPC) =>
{
    var id = serviceGRPC.InsertProduct(dto);
    //new ProductDto(1, "Product 001", 195, "Active", 200.0);
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

// app.MapPost("/updateAmount", (UpdateAmountDto dto, ProductServiceGRPC serviceGRPC) =>
// {
//     var result = serviceGRPC.UpdateAmount(dto);
//     return result;
// })
// .WithName("UpdateAmount");

// app.MapPost("/updateAmount", (ProductServiceKAFKA serviceKAFKA, HttpContext context) =>
// {
//     var result = "";
//     using (var reader = new StreamReader(context.Request.Body))
//     {
//         var body = reader.ReadToEndAsync().Result;
//         result = serviceKAFKA.SendMsgUpdateAmount(body);
//     }

//     return result;
// })
// .WithName("UpdateAmount");

app.MapPost("/updateAmount", (UpdateAmountDto dto, ProductServiceKAFKA serviceKAFKA) =>
{
    var result = "";
    var options = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(dto, options);
    result = serviceKAFKA.SendMsgUpdateAmount(jsonString);

    return result;
})
.WithName("UpdateAmount");


// ******************************* END COMUNICATION WITH API PRODUCT **********************************************************



// ******************************* START COMUNICATION WITH API ORDER **********************************************************
//get all orders
app.MapGet("/getAllOrders", (OrderServiceGRPC serviceGRPC) =>
{
    // return new List<OrderDto>() { new OrderDto(1, "Order 001", "2020-07-20T19:53:07Z", 1, "1", new List<OrderItemDto>() {
    //     new OrderItemDto(1, "Product 001", 1, 200.0), new OrderItemDto(2, "Product 002", 1, 300.0)
    // }), new OrderDto(1, "Order 002", "2020-07-20T19:53:07Z", 1, "1", new List<OrderItemDto>() {
    //     new OrderItemDto(1, "Product 001", 1, 200.0), new OrderItemDto(2, "Product 002", 1, 300.0)
    // }) }.ToArray();

    var result = serviceGRPC.GetOrders(new SalesOrderApi.Empty());
    return result.Items;
})
.WithName("GetAllOrders");



//get order by id
app.MapGet("/getOrder/{id}", (int id, OrderServiceGRPC serviceGRPC) =>
{
    var request = new SalesOrderApi.OrderId();
    request.Id = id;
    var result = serviceGRPC.GetOrder(request);

    var dto = new OrderDto(result.Id, result.Description, result.Moment, Convert.ToInt32(result.Status), result.Userid);
    dto.Items = new List<OrderItemDto>();

    result.Items.Items.ToList().ForEach(it =>
    {
        var currentDto = new OrderItemDto(it.Id, it.ProductId, it.Description, it.Quantity, Convert.ToDouble(it.Price));
        dto.Items.Add(currentDto);
    });

    return dto;
})
.WithName("GetOrder");


//get order by user id
app.MapGet("/getOrderByUserId/{id}", (string id, OrderServiceGRPC serviceGRPC) =>
{
    var request = new SalesOrderApi.UserId();
    request.Id = id;
    var result = serviceGRPC.GetOrderByUserId(request);

    var ordersDto = new List<OrderDto>();


    result.Items.ToList().ForEach(o =>
    {
        var dto = new OrderDto(o.Id, o.Description, o.Moment, Convert.ToInt32(o.Status), o.Userid);
        dto.Items = new List<OrderItemDto>();
        o.Items.Items.ToList().ForEach(it =>
        {
            var currentDto = new OrderItemDto(it.Id, it.ProductId, it.Description, it.Quantity, Convert.ToDouble(it.Price));
            dto.Items.Add(currentDto);
        });
        ordersDto.Add(dto);
    });

    return ordersDto;
    //return "OK";
})
.WithName("GetOrderByUserId");


//create order 
app.MapPost("/createOrder", (OrderDto order, OrderServiceGRPC serviceGRPC) =>
{
    var request = new SalesOrderApi.OrderRequest();
    request.Description = order.Description;
    //request.Moment = order.Moment;
    request.Status = order.Status.ToString();
    request.Userid = order.UserId;

    var itemsOrderRequest = new List<SalesOrderApi.ItemOrderRequest>();
    if (order.Items != null)
        order.Items.ForEach(i =>
        {
            var itemOrderRequest = new SalesOrderApi.ItemOrderRequest();
            itemOrderRequest.Description = i.Description;
            itemOrderRequest.Price = i.Price.ToString();
            itemOrderRequest.ProductId = i.ProductId.HasValue ? i.ProductId.Value : 0;
            itemOrderRequest.Quantity = i.Quantity.HasValue ? i.Quantity.Value : 0;
            itemsOrderRequest.Add(itemOrderRequest);
        });

    request.Items = new SalesOrderApi.ItemsOrderRequest();
    request.Items.Items.AddRange(itemsOrderRequest);
    var reply = serviceGRPC.SendOrder(request);
    return reply;
    //    return "OKKKKKK";
})
.WithName("CreateOrder");


//update order 
app.MapPut("/updateOrder/{id}", (int id, OrderDto order, OrderServiceGRPC serviceGRPC) =>
{
    var request = new SalesOrderApi.OrderRequest();
    request.Id = id;
    request.Description = order.Description;
    request.Status = order.Status.ToString();
    request.Userid = order.UserId;

    var itemsOrderRequest = new List<SalesOrderApi.ItemOrderRequest>();
    if (order.Items != null)
        order.Items.ForEach(i =>
        {
            var itemOrderRequest = new SalesOrderApi.ItemOrderRequest();
            itemOrderRequest.Id = i.Id.HasValue ? i.Id.Value : 0;
            itemOrderRequest.Description = i.Description;
            itemOrderRequest.Price = i.Price.ToString();
            itemOrderRequest.ProductId = i.ProductId.HasValue ? i.ProductId.Value : 0;
            itemOrderRequest.Quantity = i.Quantity.HasValue ? i.Quantity.Value : 0;
            itemsOrderRequest.Add(itemOrderRequest);
        });

    request.Items = new SalesOrderApi.ItemsOrderRequest();
    request.Items.Items.AddRange(itemsOrderRequest);
    var reply = serviceGRPC.UpdateOrder(request);
    return reply;
})
.WithName("UpdateOrder");
// ******************************* END COMUNICATION WITH API ORDER **********************************************************



// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

//app.Urls.Add("https://0.0.0.0:5001");
app.Urls.Add("http://0.0.0.0:5000");
app.Run();
//await app.RunAsync();


using Grpc.Core;
using Grpc.Net.Client;
using ApiGetwaySalesOrder.Dtos;

namespace ApiGetwaySalesOrder.Services
{
    public class ProductServiceGRPC
    {
        //Local PORT 
        //private int PORT = 5000;

        //container PORT 
        private const int PORT = 9091;

        // use from local to docker container without compose
        //private const string SERVICEURL = "http://127.0.0.1:";
        //private const string SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        //private const string SERVICEURL = "salesproductapi";
        // use from container to docker container with compose
        //private const string SERVICEURL = "product-api";
        // use for service kubernetes
        private const string SERVICEURL = "productapigrpc";

        public List<ProductDto> GetProducts(SalesProductApi.Empty request)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}",SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            List<ProductDto> productsDto = new List<ProductDto>();

            var reply = client.GetProducts(request);
            var items = reply.Items;


            foreach (var item in items)
            {
                ProductDto dto = new ProductDto();
                dto.Id = item.Id;
                dto.Description = item.Description;
                dto.Amount = Convert.ToInt32(item.Amount);
                dto.Price = Convert.ToDouble(item.Price);
                dto.Status = item.Status;
                productsDto.Add(dto);
            }
            

            return productsDto;

        }


        public async Task<SalesProductApi.ProductResponse> GetProductById(int id)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}",SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            SalesProductApi.ProductResponse product = new SalesProductApi.ProductResponse();
            SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();
            SalesProductApi.ProductId productId = new SalesProductApi.ProductId();
            productId.Id = id;

            var reply = await client.GetProductAsync(productId);

            //response.Items.Add(reply);
            return Task.FromResult(reply).Result;
        }

        public async Task<int> InsertProduct(ProductDto dto)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}",SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            SalesProductApi.ProductRequest request = new SalesProductApi.ProductRequest();
            request.Amount = dto.Amount.ToString();
            request.Description = dto.Description;
            request.Price = dto.Price.ToString();
            request.Status = dto.Status;
            //SalesProductApi.ProductReply response = new SalesProductApi.ProductReply();

            var response = await client.SendProductAsync(request);
            //SalesProductApi.ProductReply productReply = response.Message;
            return Convert.ToInt32(response.Message);
        }


        public async Task<int> UpdateProduct(ProductDto dto)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}",SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            SalesProductApi.ProductRequest request = new SalesProductApi.ProductRequest();
            request.Id = Convert.ToInt32(dto.Id);
            request.Amount = dto.Amount.ToString();
            request.Description = dto.Description;
            request.Price = dto.Price.ToString();
            request.Status = dto.Status;

            var response = await client.SendProductAsync(request);
            //SalesProductApi.ProductReply productReply = response.Message;
            return Convert.ToInt32(response.Message);
        }


        public async Task<string> UpdateAmount(UpdateAmountDto dto)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            var request = new SalesProductApi.ItemUpdateAmount();
            if (dto.Items != null)
                dto.Items.ForEach(itemdto =>
                {
                    var item = new SalesProductApi.UpdateAmountRequest();
                    item.Amount = itemdto.Quantity.ToString();
                    item.Id = Convert.ToInt32(itemdto.Id);
                    item.IsSum = itemdto.IsSum;
                    request.Items.Add(item);
                });

            var response = await client.UpdateAmountAsync(request);
            return response.Message;
        }


    }
}
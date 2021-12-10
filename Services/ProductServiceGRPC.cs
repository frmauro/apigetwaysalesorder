using Grpc.Core;
using Grpc.Net.Client;

namespace ApiGetwaySalesOrder.Services
{
    public class ProductServiceGRPC
    {
        //Local PORT 
        //private int PORT = 5000;

        //container PORT 
        private const int PORT = 9090;

        // use from local to docker container without compose
        private const string SERVICEURL = "http://127.0.0.1:";
        //private const string SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        //private const string SERVICEURL = "salesproductapi";
        // use from container to docker container with compose
        //private const string SERVICEURL = "product-api";
        // use for service kubernetes
        //private const string SERVICEURL = "productapigrpc";

        public async Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

            List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
            SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();

            var reply = await client.GetProductsAsync(request);

            //var productsDb = _context.Products.ToList();

            // productsDb.ToList().ForEach(productDb => {
            //     var product = new SalesProductApi.ProductResponse();
            //     product.Id = productDb.ProductId;
            //     product.Description = productDb.Description;
            //     product.Amount = productDb.Amount.ToString();
            //     product.Price = productDb.Price.ToString();
            //     product.Status = productDb.Status.ToString();
            //     products.Add(product);
            // });

            response.Items.AddRange(products);
            return Task.FromResult(reply).Result;

        }
    }
}
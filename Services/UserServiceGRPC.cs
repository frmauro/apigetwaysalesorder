using Grpc.Core;
using Grpc.Net.Client;
using ApiGetwaySalesOrder.Dtos;

namespace ApiGetwaySalesOrder.Services
{
    public class UserServiceGRPC
    {
        //Local PORT 
        private int PORT = 50051;

        //container PORT 
        //private int PORT = 9090;

        // use from local to docker container without compose
        //private String SERVICEURL = "localhost";
        private String SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        //private String SERVICEURL = "salesusernode";
        // use from container to docker container with compose
        //private String SERVICEURL = "user-api";
        // use for service kubernetes
        //private String SERVICEURL = "apiusergrpc";


        //    public async Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request)
        // {
        //     var url = SERVICEURL + PORT;
        //     AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        //     using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
        //     var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

        //     List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
        //     SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();

        //     var reply = await client.GetProductsAsync(request);

        //     response.Items.AddRange(products);
        //     return Task.FromResult(reply).Result;

        // }
    }
}
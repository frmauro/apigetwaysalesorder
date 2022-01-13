using Grpc.Core;
using Grpc.Net.Client;

namespace ApiGetwaySalesOrder.Services
{
    public class OrderServiceGRPC
    {
        //Local PORT 
        //private int PORT = 5000;

        //container PORT 
        private const int PORT = 9090;

        // use from local to docker container without compose
        private const string SERVICEURL = "http://127.0.0.1:";
        //private const string SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        //private const string SERVICEURL = "salesorderapi";
        // use from container to docker container with compose
        //private const string SERVICEURL = "order-api";
        // use for service kubernetes
        //private const string SERVICEURL = "orderapigrpc";


        public SalesOrderApi.ItemResponse GetOrders(SalesOrderApi.Empty request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);

            List<SalesOrderApi.OrderResponse> orders = new List<SalesOrderApi.OrderResponse>();
            SalesOrderApi.ItemResponse response = new SalesOrderApi.ItemResponse();

            var reply = client.GetOrders(request);

            response.Items.AddRange(orders);
            return Task.FromResult(reply).Result;
        }


        public SalesOrderApi.OrderResponse GetOrder(SalesOrderApi.OrderId request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.GetOrder(request);
            return Task.FromResult(reply).Result;
        }

        public SalesOrderApi.OrderReply SendOrder(SalesOrderApi.OrderRequest request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.SendOrder(request);
            return Task.FromResult(reply).Result;
        }

        public SalesOrderApi.OrderReply UpdateOrder(SalesOrderApi.OrderRequest request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.UpdateOrder(request);
            return Task.FromResult(reply).Result;
        }
    }
}
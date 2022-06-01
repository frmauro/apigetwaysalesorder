using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace ApiGetwaySalesOrder.Services
{
    public class OrderServiceGRPC
    {
        private int PORT;
        private string serviceurlorderapi = string.Empty;
        private string orderapiport = string.Empty;
        private string SERVICEURL = string.Empty;

        public OrderServiceGRPC(string _serviceurlorderapi, string _orderapiport)
        {
            serviceurlorderapi = _serviceurlorderapi;
            orderapiport = _orderapiport;
            PORT = Convert.ToInt32(orderapiport);
            SERVICEURL = serviceurlorderapi;
        }


        public SalesOrderApi.ItemResponse GetOrders(SalesOrderApi.Empty request)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}", SERVICEURL, PORT);
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
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}", SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.GetOrder(request);
            return Task.FromResult(reply).Result;
        }

        public SalesOrderApi.ItemResponse GetOrderByUserId(SalesOrderApi.UserId request)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}", SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.GetOrderByUserId(request);
            return Task.FromResult(reply).Result;
        }


        public SalesOrderApi.OrderReply SendOrder(SalesOrderApi.OrderRequest request)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}", SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.SendOrder(request);
            return Task.FromResult(reply).Result;
        }

        public SalesOrderApi.OrderReply UpdateOrder(SalesOrderApi.OrderRequest request)
        {
            //var url = SERVICEURL + PORT;
            var url = string.Format("http://{0}:{1}", SERVICEURL, PORT);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesOrderApi.OrderServiceProto.OrderServiceProtoClient(channel);
            var reply = client.UpdateOrder(request);
            return Task.FromResult(reply).Result;
        }
    }
}
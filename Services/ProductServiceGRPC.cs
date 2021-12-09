using Grpc.Core;
using Grpc.Net.Client;

namespace ApiGetwaySalesOrder.Services
{
    public class ProductServiceGRPC 
    {
        private const string SERVICEURL = "http://productapi/Product/";

        // public override Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request, ServerCallContext _context)
        // {

        //     using var channel = GrpcChannel.ForAddress(SERVICEURL, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
        //     //var client = new SalesProductApi.ProductServiceProto.ProductServiceProtoClient(channel);

        //     List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
        //     SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();

        //     //var productsDb = _context.Products.ToList();

        //     // productsDb.ToList().ForEach(productDb => {
        //     //     var product = new SalesProductApi.ProductResponse();
        //     //     product.Id = productDb.ProductId;
        //     //     product.Description = productDb.Description;
        //     //     product.Amount = productDb.Amount.ToString();
        //     //     product.Price = productDb.Price.ToString();
        //     //     product.Status = productDb.Status.ToString();
        //     //     products.Add(product);
        //     // });

        //     response.Items.AddRange(products);
        //     return Task.FromResult(response);

        // }
    }
}
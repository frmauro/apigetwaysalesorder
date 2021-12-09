using Grpc.Core;

namespace ApiGetwaySalesOrder.Services
{
    public class ProductServiceGRPC : SalesProductApi.ProductServiceProto.ProductServiceProtoBase
    {
                public override Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request, ServerCallContext _context){

            List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
                    SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();

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
                    return Task.FromResult(response);

        }
    }
}
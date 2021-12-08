namespace ApiGetwaySalesOrder.Dtos
{
    public class ProductDto
    {
        public ProductDto() { }

        public ProductDto(int? id = null, string? description = null, int? amount = null, string? status = null, double? price = null)
        {
            this.Id = id;
            this.Description = description;
            this.Amount = amount;
            this.Status = status;
            this.Price = price;
        }

        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? Amount { get; set; }
        public string? Status { get; set; }
        public double? Price { get; set; }
    }
}
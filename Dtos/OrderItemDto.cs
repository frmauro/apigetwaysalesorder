namespace ApiGetwaySalesOrder.Dtos
{
    public class OrderItemDto
    {
        public OrderItemDto() { }

        public OrderItemDto(int? productId = null, string? description = null, int? quantity = null, double? price = null) {
            this.ProductId = productId;
            this.Description = description;
            this.Quantity = quantity;
            this.Price = price;
         }

        public int? ProductId { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}
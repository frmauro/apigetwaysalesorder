namespace ApiGetwaySalesOrder.Dtos
{
    public class OrderDto
    {
        public OrderDto() { }

        public OrderDto(int? id = null, string? description = null, string? moment = null, int? orderStatus = null, string? userId = null, List<OrderItemDto>? items = null) {
            this.Id = id;
            this.Description = description;
            this.Moment = moment;
            this.Status = orderStatus;
            this.UserId = userId;
            this.Items = items;
         }


        public int? Id { get; set; }
        public string? Description { get; set; }
        public string? Moment { get; set; }
        public int? Status { get; set; }
        public string? UserId { get; set; }
        public List<OrderItemDto>? Items { get; set; }

    }
}
namespace Shared.DTOs.OrderDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
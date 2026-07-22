namespace SalesController.DTOs
{
    public class CreateSaleDto
    {
        public List<SaleItemDto> Items { get; set; } = new();
    }
    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class SaleResponseDto
    {
        public int SaleId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CretedAt { get; set; }
        public List<string> ItemsSummary { get; set; } = new();
    }
}
namespace SalesController.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }

}

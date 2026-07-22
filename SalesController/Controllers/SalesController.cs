using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesController.DTOs;
using SalesController.Entities;


namespace SalesController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private static readonly List<Product> ProductsMemory = new()
        {
            new Product { Id = 1, Name = "Mekanik Klavye", Price = 1500.00m, StockQuantity = 20 },
            new Product { Id = 2, Name = "Oyuncu Mouse", Price = 800.00m, StockQuantity = 15 },
            new Product { Id = 3, Name = "Monitör", Price = 3000.00m, StockQuantity = 10 },
            new Product { Id = 4, Name = "Kulaklık", Price = 500.00m, StockQuantity = 25 },
            new Product { Id = 5, Name = "Webcam", Price = 700.00m, StockQuantity = 30 },
            new Product { Id = 6, Name = "USB Hub", Price = 200.00m, StockQuantity = 50 },
            new Product { Id = 7, Name = "Harici Hard Disk", Price = 1200.00m, StockQuantity = 12 },
            new Product { Id = 8, Name = "SSD", Price = 1500.00m, StockQuantity = 18 },
            new Product { Id = 9, Name = "RAM", Price = 800.00m, StockQuantity = 22 },
            new Product { Id = 10, Name = "Anakart", Price = 2500.00m, StockQuantity = 8 },
            new Product { Id = 11, Name = "Ekran Kartı", Price = 4000.00m, StockQuantity = 5 },
            new Product { Id = 12, Name = "İşlemci", Price = 3500.00m, StockQuantity = 7 },
            new Product { Id = 13, Name = "Power Supply", Price = 600.00m, StockQuantity = 15 }
        };
        private static readonly List<Sale> SalesMemory = new();
        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleDto request)
        {
            if (request == null || !request.Items.Any())
            {
                return BadRequest(new { Message = "Ürün Sepetinizde bulunmuyor!" });
            }
            decimal totalAmount = 0;
            var summary = new List<string>();
            foreach (var item in request.Items)
            {
                var product = ProductsMemory.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                {
                    return NotFound(new { Message = $"Ürün bulunamadı: {item.ProductId}" });
                }
                if(product.StockQuantity < item.Quantity)
                {
                    return BadRequest(new { Message = $"{product.Name} ürünü için yetersiz stok! Kalan: {product.StockQuantity}" });
                }
                product.StockQuantity -= item.Quantity;
                var lineTotal = product.Price * item.Quantity;
                totalAmount += lineTotal;
                summary.Add($"{product.Name} - {item.Quantity} adet - {lineTotal:C}");
            }
            var sale = new Sale
            {
                Id = SalesMemory.Count + 1,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow
            };
            SalesMemory.Add(sale);
            var response = new SaleResponseDto
            {
                SaleId = sale.Id,
                TotalAmount = sale.TotalAmount,
                CretedAt = sale.CreatedAt,
                ItemsSummary = summary
            };
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, response);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetSaleById(int id)
        {
            var sale = SalesMemory.FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return NotFound(new { Message = $"Satış bulunamadı: {id}" });
            }
            var response = new SaleResponseDto
            {
                SaleId = sale.Id,
                TotalAmount = sale.TotalAmount,
                CretedAt = sale.CreatedAt,
                ItemsSummary = new List<string>() 
            };
            return Ok(response);
        }
    }
}

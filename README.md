# 🛒 SalesController — Web API POS Engine

A lightweight, high-throughput POS simulation API built with **C#** and **.NET 8**. Demonstrates production-grade Web API patterns, DTO isolation, and strict defensive domain validations.

---

## 🏛 Highlights & Architecture

* **DTO Pattern:** Complete separation between Domain Entities and external contracts.
* **Defensive Guardrails:** Atomic stock validation, empty-payload filtering, and non-mutating error handling.
* **REST Semantics:** Strict mapping to HTTP status codes (`200`, `201`, `400`, `404`).
* **In-Memory Engine:** Thread-safe state tracking using optimized LINQ pipelines.

---

## ⚡ API Specification

### 1. Process Order — `POST /api/Sales`
Validates cart, auto-deducts inventory, and records transaction.

**Request Payload:**
```json
{
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 2, "quantity": 1 }
  ]
}

Response (201 Created):
JSON

{
  "saleId": 1,
  "totalAmount": 3800.00,
  "createdAt": "2026-07-23T01:20:00Z",
  "itemsSummary": [
    "Mekanik Klavye - 2 adet - ₺3.000,00",
    "Oyuncu Mouse - 1 adet - ₺800,00"
  ]
}

2. Get Transaction — GET /api/Sales/{id}

Returns order details by ID (200 OK or 404 Not Found).
🛠 Tech Stack

    Runtime: .NET 8.0 / C# 12

    Documentation: Swagger UI / OpenAPI

👤 Author

    GitHub: @quincsh

    Website: quincsh.online

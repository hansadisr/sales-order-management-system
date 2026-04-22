using System;
using System.Collections.Generic;

namespace SalesOrderAPI.API.Models
{
    public class SalesOrderDto
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Note { get; set; }
        public decimal TotalExcl { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalIncl { get; set; }
        public List<SalesOrderDetailDto>? OrderDetails { get; set; }
    }

    public class SalesOrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ExclAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal InclAmount { get; set; }
        public string? Note { get; set; }
    }
}

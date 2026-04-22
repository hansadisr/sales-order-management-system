using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderAPI.Domain.Entities
{
    public class SalesOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Note { get; set; }
        public decimal TotalExcl { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalIncl { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}

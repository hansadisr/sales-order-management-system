using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderAPI.Domain.Entities
{
    public class SalesOrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ExclAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal InclAmount { get; set; }
        public string? Note { get; set; }

        [ForeignKey("OrderId")]
        public virtual SalesOrder SalesOrder { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}

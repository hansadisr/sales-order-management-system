using System.ComponentModel.DataAnnotations;

namespace SalesOrderAPI.Domain.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesOrderAPI.Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Suburb { get; set; }
        public string? State { get; set; }
        public string? PostCode { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}

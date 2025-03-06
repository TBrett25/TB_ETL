using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAnalyticsAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SalesAnalyticsAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}

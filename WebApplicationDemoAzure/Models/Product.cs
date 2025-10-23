using System.ComponentModel.DataAnnotations;

namespace WebApplicationDemoAzure.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? ImagePath { get; set; } // relative path to saved image
    }
}

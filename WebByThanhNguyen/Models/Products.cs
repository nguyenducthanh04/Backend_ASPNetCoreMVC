using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebByThanhNguyen.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [Column("ProductId", TypeName = "INT")]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? ProductName { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Price { get; set; }

        [Required]
        public string? TypeName { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public string? Size { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Quantity { get; set; }

        [Required]
        public string? Saled { get; set; }

        [Column("CategoryId", TypeName = "INT")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }

        // Quan hệ 1 nhiều với Images
        public ICollection<Images> Images { get; set; }

        // Khởi tạo ICollection trong constructor
        public Products()
        {
            Images = new List<Images>();
        }
    }
}

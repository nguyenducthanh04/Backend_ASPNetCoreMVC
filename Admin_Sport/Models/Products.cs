using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Admin_Sport.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [Column("ProductId", TypeName = "INT")]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Price { get; set; }
        [Required]
        [MaxLength(200)]
        public string Type { get; set; }
        [Required]
        [MaxLength(100)]
        public string Color { get; set; }
        [Required]
        [MaxLength(100)]
        public string Size { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Saled { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string DateSubmitted { get; set; }
        [Column("CategoryId", TypeName = "INT")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }

    }
}

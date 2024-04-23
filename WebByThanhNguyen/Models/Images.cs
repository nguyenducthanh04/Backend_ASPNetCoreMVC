using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebByThanhNguyen.Models
{
    [Table("Images")]
    public class Images
    {
        [Key]
        [Column("ImageId", TypeName ="INT")]
        public int ImageId { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Column("ProductId", TypeName ="INT")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

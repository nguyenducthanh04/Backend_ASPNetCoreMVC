using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Sport.Models
{
    [Table("Images")]
    public class Images
    {
        [Key]
        [Column("ImageId", TypeName = "INT")]
        public int ImageId { get; set; }
        [Required]
        public string Image { get; set; }
        //public string Path { get; set; }
        [Required]
        [MaxLength(50)]
        public string Main { get; set; }
        [Column("ProductId", TypeName = "INT")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

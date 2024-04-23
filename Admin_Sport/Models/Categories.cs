using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Sport.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        [Column("CategoryId", TypeName ="INT")]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }//Quan hệ 1 nhiều

    }
}

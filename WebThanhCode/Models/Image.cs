using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebThanhCode.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string Image1 { get; set; } = null!;
        public string? Main { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        [NotMapped]
        public IFormFile ImageFile { get; set; } = null!;
    }
}

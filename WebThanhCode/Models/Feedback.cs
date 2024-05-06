using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebThanhCode.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        [NotMapped]
        public IFormFile ImageFile { get; set; } = null!;
    }
}

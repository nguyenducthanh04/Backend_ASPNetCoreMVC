using System;
using System.Collections.Generic;

namespace Website_Sport.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

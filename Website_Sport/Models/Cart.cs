﻿using System;
using System.Collections.Generic;

namespace Website_Sport.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string Price { get; set; } = null!;
        public byte[] CreatedDate { get; set; } = null!;

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}

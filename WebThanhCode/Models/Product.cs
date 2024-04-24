using System;
using System.Collections.Generic;

namespace WebThanhCode.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<Image>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
        public string Description { get; set; } = null!;
        public int Saled { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}

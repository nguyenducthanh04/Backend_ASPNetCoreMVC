using System;
using System.Collections.Generic;

namespace WebThanhCode.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string OrderName { get; set; } = null!;
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

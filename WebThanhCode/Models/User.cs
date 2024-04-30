using System;
using System.Collections.Generic;

namespace WebThanhCode.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string Address { get; set; } = null!;
        public int Phone { get; set; }
        public int PositionId { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}

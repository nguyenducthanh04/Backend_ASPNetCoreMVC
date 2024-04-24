using System;
using System.Collections.Generic;

namespace Website_Sport.Models
{
    public partial class Position
    {
        public Position()
        {
            Users = new HashSet<User>();
        }

        public int PositionId { get; set; }
        public string? PositionName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

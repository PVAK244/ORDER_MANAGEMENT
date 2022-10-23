using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccess
{
    public partial class OrderObject
    {
        public OrderObject()
        {
            OrderLines = new HashSet<OrderLineObject>();
        }

        [Key]
        public decimal OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal CustomerId { get; set; }

        public virtual CustomerObject Customer { get; set; }
        public virtual ICollection<OrderLineObject> OrderLines { get; set; }
    }
}

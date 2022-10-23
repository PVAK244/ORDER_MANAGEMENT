using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccess
{
    public partial class OrderLineObject
    {
        [Key]
        public decimal OrderId { get; set; }
        public decimal ProductId { get; set; }
        public decimal OrderedQuantity { get; set; }

        public virtual OrderObject Order { get; set; }
        public virtual ProductObject Product { get; set; }
    }
}

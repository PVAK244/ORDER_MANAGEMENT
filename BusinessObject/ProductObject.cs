using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccess
{
    public partial class ProductObject
    {
        public ProductObject()
        {
            OrderLines = new HashSet<OrderLineObject>();
        }

        [Key]
        public decimal ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductFinish { get; set; }
        public decimal? StandardPrice { get; set; }

        public virtual ICollection<OrderLineObject> OrderLines { get; set; }
    }
}

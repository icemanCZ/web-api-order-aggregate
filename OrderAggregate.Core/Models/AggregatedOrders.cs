using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Models
{
    public class AggregatedOrders
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }

        public AggregatedOrders() { }

        public AggregatedOrders(string productID, int quantity)
        {
            ProductID = productID ?? throw new ArgumentNullException(nameof(productID));
            Quantity = quantity;
        }
    }
}

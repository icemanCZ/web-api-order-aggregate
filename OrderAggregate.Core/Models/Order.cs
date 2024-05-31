using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Models
{
    public class Order
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}

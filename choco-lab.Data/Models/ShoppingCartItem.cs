using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Chocolate Chocolate { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public string FullName { get; set; }
        public string Address { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}

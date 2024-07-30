using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Cart
{
    public class Carts : BaseModel
    {
        public string title { get; set; }
        public string totalQty { get; set; }
        public double totalPrice { get; set;  }

        public string status { get; set; }

        public ICollection<CartItem> Items { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Cart
{
    public class CartItem
    {
        public Guid Id { get; set;  }
        public string Name { get; set; }

        public int qty { get; set;  }

        public int price { get; set; }


        public Carts Cart { get; set; }

    }

}

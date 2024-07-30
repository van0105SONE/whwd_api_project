using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.PurchaseDto
{
    public class PurchaseDto
    {
        public string? cartId { get; set; }
        public string? title { get; set; }
        public int totalQty { get; set; }
        public double totalPrice { get; set; }
        public string? status { get; set; }
        public string? userId { get; set; }

        public List<ItemDto>? items { get; set; }
    }
}

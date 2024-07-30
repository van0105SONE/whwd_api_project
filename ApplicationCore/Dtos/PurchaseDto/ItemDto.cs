using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.PurchaseDto
{
    public class ItemDto
    {
        public string? itemId { get; set; }
        public string? Name { get; set; }

        public int qty { get; set; }

        public int price { get; set; }

    }
}

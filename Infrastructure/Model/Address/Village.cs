﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Address
{
    public class Village
    {
        [Key]
        public  String villageCode { get; set; }
        public  String villageName { get; set; }
        public District district { get; set; }

    }
}

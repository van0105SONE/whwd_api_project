using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model.Users;

namespace Infrastructure.Model
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public ApplicationUser CreateBy { get; set; }
        public ApplicationUser? UpdateBy { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set;  }
        
    }
}

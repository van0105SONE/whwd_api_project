using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model.Account{
    public class SourceType {
       [Key]
       public Guid Id {get; set;}
       public required string Name {get; set;}
    }
}
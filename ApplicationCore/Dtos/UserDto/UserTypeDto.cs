
namespace ApplicationCore.Dtos.UserDto {
   public class UserTypeDto {
      public required string Id {get; set;}
    }


   public class UserTypeUpdateDto : UserTypeDto {
        public string? Name {get; set;}
    }
}
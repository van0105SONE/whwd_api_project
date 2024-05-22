namespace whwd_web_api.Dtos.Accounts{
    public class AccountDto {
        public required string AccountNo { get; set; }
        public required string BookingNO { get; set; }
        public required string BankName { get; set; }
        public required  string userId { get; set; }
        public required Guid AccountTypeId { get; set; }
        public required  string OwnerId { get; set; }

        public  double   Balance { get; set; }
    }

    public class AccountUpdateDto : AccountDto {
        public Guid Id {get; set;}
    }
}
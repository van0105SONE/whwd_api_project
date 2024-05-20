namespace ApplicationCore.Dtos.Work
{
    public class DonateThingDto
    {
        public String Name { get; set; }
        public Double Price { get; set; }

        public string UnitType { get; set; }
        public int Unit { get; set; }
        public int personAmount { get; set; }
        public string userId { get; set; }
    }

    public class UpdateDonateThingDto : DonateThingDto
    {
      public Guid Id { get; set; }
    }
}

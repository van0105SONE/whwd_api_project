namespace whwd_web_api.Dtos.Address
{
    public class VillageDto
    {
        public string villageCode { get; set; }
        public string villageName { get; set; }
        public DistrictDto district { get; set; }
    }
}

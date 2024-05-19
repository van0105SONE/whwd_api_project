namespace whwd_web_api.Dtos.Address
{
    public class VillageDto
    {
        public string? villageCode { get; set; }
        public required string villageName { get; set; }
        public required DistrictDto district { get; set; }
    }
}

namespace NZWalks.API.Models.DTO
{
    public class UpateRegionRequest
    {//Id not included because we dont want the option to update the id field.  Whatever you want updated, only include that.
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}

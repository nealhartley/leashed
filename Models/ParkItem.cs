namespace TodoApi.Models
{
    public class ParkItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsLeashed { get; set; }
        public bool RoadFront { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
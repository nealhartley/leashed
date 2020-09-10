using CsvHelper.Configuration;
using leashApi.Models;

namespace leashed.helpers
{
    public class ParkItemMap : ClassMap<ParkItemCSV>
    {
        public ParkItemMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Suburb).Name("suburb");
            Map(m => m.IsLeashed).Name("On_Off");
            Map(m => m.Details).Name("details");
            Map(m => m.RoadFront).Name("road_front");
        }


    }

    public class ParkItemCSV
    {
        public string Name { get; set; }
        public string IsLeashed { get; set; }
        public string RoadFront { get; set; }
        public string Suburb { get; set; }
        public string Details { get; set; }
    }

}


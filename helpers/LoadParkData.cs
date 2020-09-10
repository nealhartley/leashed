using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using leashApi.Models;

namespace leashed.helpers
{
    public class LoadParkData
    {

        public LoadParkData()
        {
        }

        public static bool checkData(){
            // query that data exists .Any on a not
            
            return false;
        }

        public static List<ParkItem> loadData(){
            var dir = Path.GetFullPath(Directory.GetCurrentDirectory());
            Console.WriteLine("directory: " + dir);
            var reader = new StreamReader("../Assets/Dog_Exercise___Restriction_Layer.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
            csv.Configuration.RegisterClassMap<ParkItemMap>();
            var parksCsv = csv.GetRecords<ParkItemCSV>();
            var parksCsvList = parksCsv.ToList();
            Console.WriteLine("CSV: " + parksCsvList[1].Name +" " + parksCsvList[2].Name );
            //bulk convert to parks
            var parks = parksCsvList
                .Select(x => new ParkItem() {
                    Name = x.Name,
                    IsLeashed = x.IsLeashed,
                    RoadFront = String.Equals(x.RoadFront, "yes", comparisonType: StringComparison.OrdinalIgnoreCase),
                    Suburb = x.Suburb ,
                    City  = "Wellington",
                    Country = "New Zealand",
                    })
                .ToList();
            Console.WriteLine( "Parks: " +parks[1].Name +" " + parks[2].Name );
            return parks;
        }
    }
}
using PlattSampleApp.StarWarsModels;
using System.Collections.Generic;

namespace PlattSampleApp.StarWarsModels.DTOs
{
    public class SwapiVehiclesResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Vehicle> Results { get; set; }
    }
}
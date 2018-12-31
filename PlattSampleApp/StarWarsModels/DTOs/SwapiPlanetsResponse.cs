using PlattSampleApp.StarWarsModels;
using System.Collections.Generic;

namespace PlattSampleApp.StarWarsModels.DTOs
{
    public class SwapiPlanetsResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Planet> Results { get; set; }
    }
}
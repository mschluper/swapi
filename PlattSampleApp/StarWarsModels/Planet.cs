using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattSampleApp.StarWarsModels
{
    public class Planet
    {
        public string Name { get; set; }

        public string Population { get; set; }

        public string Diameter { get; set; }
        //public int DiameterAsInt { get; set; }

        public string Terrain { get; set; }

        [JsonProperty(PropertyName = "orbital_period")]
        public string LengthOfYear { get; set; }

        [JsonProperty(PropertyName = "rotation_period")]
        public string LengthOfDay { get; set; }

        [JsonProperty(PropertyName = "residents")]
        public List<string> ResidentUrls { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        [JsonProperty(PropertyName = "surface_water")]
        public string SurfaceWaterPercentage { get; set; }
    }
}
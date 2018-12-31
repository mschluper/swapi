using Newtonsoft.Json;

namespace PlattSampleApp.StarWarsModels
{
    public class Resident
    {
        public string Name { get; set; }

        public string Height { get; set; }

        [JsonProperty(PropertyName = "mass")]
        public string Weight { get; set; }

        public string Gender { get; set; }

        [JsonProperty(PropertyName = "hair_color")]
        public string HairColor { get; set; }

        [JsonProperty(PropertyName = "eye_color")]
        public string EyeColor { get; set; }

        [JsonProperty(PropertyName = "skin_color")]
        public string SkinColor { get; set; }
    }
}
namespace PlattSampleApp.Models
{
    public class PlanetDetailsViewModel
    {
        public string Name { get; set; }

        public string Population { get; set; }

        public string Diameter { get; set; }
        public int DiameterAsInt => Diameter == "unknown" ? 0 : int.Parse(Diameter);

        public string Terrain { get; set; }

        public string LengthOfYear { get; set; }
    }
}
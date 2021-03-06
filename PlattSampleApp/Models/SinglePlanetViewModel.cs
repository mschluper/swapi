﻿namespace PlattSampleApp.Models
{
    public class SinglePlanetViewModel
    {
        public string Name { get; set; }

        public string LengthOfDay { get; set; }

        public string LengthOfYear { get; set; }

        public string Diameter { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        public string SurfaceWaterPercentage { get; set; }

        public string Population { get; set; }
        public string FormattedPopulation => Population == "unknown" ? "unknown" : long.Parse(Population).ToString("N0");
    }
}
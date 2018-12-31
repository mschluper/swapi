using PlattSampleApp.StarWarsModels;
using System.Collections.Generic;
using System.Linq;

namespace PlattSampleApp.Models
{
    public class AllPlanetsViewModel
    {
        public AllPlanetsViewModel()
        {
        }

        public List<PlanetDetailsViewModel> PlanetViewModels { get; set; }

        public double AverageDiameter {
            get
            {
                // Only include planets with a known diameter larger than 0
                return ((double)PlanetViewModels.Sum(p => p.DiameterAsInt)) / PlanetViewModels.Count(p => p.DiameterAsInt > 0);
            }
        }
    }
}
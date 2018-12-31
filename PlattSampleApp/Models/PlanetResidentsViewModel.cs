using System.Collections.Generic;

namespace PlattSampleApp.Models
{
    public class PlanetResidentsViewModel
    {
        public PlanetResidentsViewModel()
        {
            Residents = new List<ResidentSummaryViewModel>();
        }

        public List<ResidentSummaryViewModel> Residents { get; set; }
    }
}
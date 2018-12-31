using System.Collections.Generic;

namespace PlattSampleApp.Models
{
    public class VehicleSummaryViewModel
    {
        public VehicleSummaryViewModel()
        {
            Details = new List<Models.VehicleStatsViewModel>();
        }

        public int VehicleCount { get; set; }

        public int ManufacturerCount { get; set; }

        public List<VehicleStatsViewModel> Details { get; set; }
    }
}
using System.Collections.Generic;

namespace PlattSampleApp.Models
{
    public class VehicleViewModel
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string Cost { get; set; }
        public double CostAsDouble => Cost == "unknown" ? 0 : double.Parse(Cost);

        public string MaxSpeed { get; set; }

        public string Crew { get; set; }

        public string Passengers { get; set; }

        public string CargoCapacity { get; set; }

        public string Consumables { get; set; }

        public string VehicleClass { get; set; }

        public List<string> Pilots { get; set; }

        public List<string> Films { get; set; }
    }
}
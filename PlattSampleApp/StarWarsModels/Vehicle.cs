using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.StarWarsModels
{
    public class Vehicle
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "cost_in_credits")]
        public string Cost { get; set; }

        [JsonProperty(PropertyName = "max_atmosphering_speed")]
        public string MaxSpeed { get; set; }

        public string Crew { get; set; }

        public string Passengers { get; set; }

        [JsonProperty(PropertyName = "cargo_capacity")]
        public string CargoCapacity { get; set; }

        public string Consumables { get; set; }

        [JsonProperty(PropertyName = "vehicle_class")]
        public string VehicleClass { get; set; }

        public List<string> Pilots { get; set; }

        public List<string> Films { get; set; }
    }
}
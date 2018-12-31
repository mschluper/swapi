using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlattSampleApp.Models;
using PlattSampleApp.Services;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private IStarWarsService _StarWarsService;

        public HomeController(IStarWarsService starWarsService)
        {
            _StarWarsService = starWarsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieve a list of all planets, and order them by their Diameter, from largest to smallest, 
        /// as well as the average diameter of those planets whose diameter is known.
        /// </summary>
        /// <returns>ActionResult</returns>
        public async Task<IActionResult> GetAllPlanets()
        {
            var model = new AllPlanetsViewModel();

            model.PlanetViewModels = (await _StarWarsService.GetPlanetsAsync())
                    .Select(p => new PlanetDetailsViewModel
                    {
                        Name = p.Name,
                        Population = p.Population == "unknown" ? "unknown" : long.Parse(p.Population).ToString("N0"),
                        Diameter = p.Diameter,
                        Terrain = p.Terrain,
                        LengthOfYear = p.LengthOfYear
                    })
                    .OrderByDescending(p => p.DiameterAsInt) // Largest Diameter to Smallest Diameter
                    .ToList();

            return View(model);
        }

        /// <summary>
        /// Retrieve detailed information about a single planet identified by planetid.
        /// </summary>
        /// <param name="planetid">The id of the planet to be retrieved.</param>
        /// <returns>ActionResult</returns>
        public async Task<IActionResult> GetPlanetTwentyTwo(int planetid)
        {
            var planet = await _StarWarsService.GetPlanetAsync(planetid);

            var model = new SinglePlanetViewModel
            {
                Climate = planet.Climate,
                Diameter = planet.Diameter,
                Gravity = planet.Gravity,
                LengthOfDay = planet.LengthOfDay,
                LengthOfYear = planet.LengthOfYear,
                Name = planet.Name,
                Population = planet.Population,
                SurfaceWaterPercentage = planet.SurfaceWaterPercentage
            };

            return View(model);
        }

        /// <summary>
        /// Retrieve detailed information about a single planet identified by planetname.
        /// </summary>
        /// <param name="planetname">The name of the planet to be retrieved.</param>
        /// <returns>ActionResult</returns>
        public async Task<IActionResult> GetResidentsOfPlanetNaboo(string planetname)
        {
            var model = new PlanetResidentsViewModel();

            model.Residents = (await _StarWarsService.GetPlanetResidentsAsync(planetname))
                .Select(r => new ResidentSummaryViewModel
                {
                    EyeColor = r.EyeColor,
                    Gender = r.Gender,
                    HairColor = r.HairColor,
                    Height = r.Height,
                    Name = r.Name,
                    SkinColor = r.SkinColor,
                    Weight = r.Weight
                })
                .OrderBy(r => r.Name)
                .ToList();

            return View(model);
        }

        /// <summary>
        /// Get a list of all vehicles from the Star Wars API and present some summary information. 
        /// The top portion of the view will display the total number of vehicles where the cost is not "unknown", 
        /// as well as the total number of unique manufacturers.
        /// For the table, take the vehicles where the cost is not "unknown", and group them by manufacturer.
        /// For each group(table row), get the manufacturer name, the number of vehicles by that manufacturer, 
        /// and their average cost.
        /// Sort them by vehicle count(highest to lowest), then by average cost(highest to lowest).
        /// </summary>
        /// <returns>ActionResult</returns>
        public async Task<IActionResult> VehicleSummary()
        {
            var model = new VehicleSummaryViewModel();

            var vehicles = await _StarWarsService.GetVehiclesAsync();
            var vehiclesWithKnownCost = vehicles.Where(v => v.Cost != "unknown").ToList();
            model.VehicleCount = vehiclesWithKnownCost.Count;
            model.ManufacturerCount = vehicles.Select(v => v.Manufacturer).Distinct().Count();

            model.Details = vehiclesWithKnownCost
                .Select(v => new VehicleViewModel
                {
                    Cost = v.Cost,
                    Manufacturer = v.Manufacturer
                })
                .GroupBy(v => v.Manufacturer)
                .Select(g => new VehicleStatsViewModel
                {
                    ManufacturerName = g.Key,
                    VehicleCount = g.Count(),
                    AverageCost = g.Average(v => v.CostAsDouble)
                })
                .OrderByDescending(s => s.VehicleCount)
                .ThenByDescending(s => s.AverageCost)
                .ToList();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Platt coding exercise.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Marc Schluper";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

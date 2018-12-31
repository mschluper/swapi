using Newtonsoft.Json;
using PlattSampleApp.Caches;
using PlattSampleApp.StarWarsModels;
using PlattSampleApp.StarWarsModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattSampleApp.Services
{
    public class StarWarsService : IStarWarsService
    {
        const string SwapiUrl = "https://swapi.co/api";
        const bool GetOneSwapiPageAtATime = false;
        private IHttpService HttpService;

        public StarWarsService(IHttpService httpService)
        {
            HttpService = httpService;
        }

        async public Task<List<Planet>> GetPlanetsAsync()
        {
            var planets = new List<Planet>();
            var swapiUrl = $"{SwapiUrl}/planets/";

            if (GetOneSwapiPageAtATime)
            {
                while (swapiUrl != null)
                {
                    string jsonResult = await HttpService.GetResponseAsJson(swapiUrl);
                    var swapiPlanetsResponse = JsonConvert.DeserializeObject<SwapiPlanetsResponse>(jsonResult);
                    planets.AddRange(swapiPlanetsResponse.Results);
                    swapiUrl = swapiPlanetsResponse.Next;
                }
            }
            else
            {
                // Helper conversion function - takes an HttpResponse string and, assuming it is an SwapiPlanetsResponse, deserializes it
                Func<string, SwapiPlanetsResponse> extractPlanetsFromResponse = response =>
                {
                    return JsonConvert.DeserializeObject<SwapiPlanetsResponse>(response);
                };

                // Get pages concurrently (as much as possible)
                List<string> allResponses = await HttpService.GetAllResponsePagesAsJson(swapiUrl, response =>
                {
                    return extractPlanetsFromResponse(response).Count;
                });

                // Extract the planets from the page responses
                allResponses.ForEach(r =>
                {
                    planets.AddRange(extractPlanetsFromResponse(r).Results);
                });

                //// Here we assume the Star Wars API is fixed (with page size 10) and therefore we can use concurrent data retrieval.
                //// The first page tells us how many pages there are.
                //const int pageSize = 10;
                //string firstPageResponse = await HttpService.GetResponseAsJson(swapiUrl);
                //var swapiPlanetsResponse = JsonConvert.DeserializeObject<SwapiPlanetsResponse>(firstPageResponse);

                //var pageCount = swapiPlanetsResponse.Count / pageSize;
                //if (swapiPlanetsResponse.Count % pageSize > 0)
                //{
                //    pageCount += 1;
                //}
                //var urls = new List<string>();
                //for (var pageNumber = 2; pageNumber <= pageCount; pageNumber++)
                //{
                //    urls.Add($"{swapiUrl}?page={pageNumber}");
                //}
                //var tasks = urls.Select(url => HttpService.GetResponseAsJson(url));

                //var responses = await Task.WhenAll(tasks);

                //// Add very first response (page 1)
                //var completeList = responses.ToList();
                //completeList.Add(firstPageResponse);
                //completeList
                //    .ForEach(r => planets.AddRange(JsonConvert.DeserializeObject<SwapiPlanetsResponse>(r).Results));
            }

            return planets;
        }

        async public Task<Planet> GetPlanetAsync(int planetNumber)
        {
            string jsonResult = await HttpService.GetResponseAsJson($"{SwapiUrl}/planets/{planetNumber}/");
            return JsonConvert.DeserializeObject<Planet>(jsonResult);
        }

        async public Task<List<Resident>> GetPlanetResidentsAsync(string planetName)
        {
            var planet = await GetPlanetByName(planetName);
            if (planet == null)
            {
                return new List<Resident>(); // null?
            }
            var tasks = planet.ResidentUrls.Select(url => HttpService.GetResponseAsJson(url)).ToArray();
            var results = await Task.WhenAll(tasks);

            var list = new List<Resident>();
            results.ToList().ForEach(r => list.Add(JsonConvert.DeserializeObject<Resident>(r)));

            return list;
        }

        async public Task<List<Vehicle>> GetVehiclesAsync()
        {
            var vehicles = new List<Vehicle>();
            var swapiUrl = $"{SwapiUrl}/vehicles/";

            // Helper conversion function - takes an HttpResponse string and, assuming it is an SwapiVehiclesResponse, deserializes it
            Func<string, SwapiVehiclesResponse> extractVehiclesFromResponse = response =>
            {
                return JsonConvert.DeserializeObject<SwapiVehiclesResponse>(response);
            };

            // Get pages concurrently (as much as possible)
            List<string> allResponses = await HttpService.GetAllResponsePagesAsJson(swapiUrl, response =>
            {
                return extractVehiclesFromResponse(response).Count;
            });

            // Extract the vehicles from the page responses
            allResponses.ForEach(r =>
            {
                vehicles.AddRange(extractVehiclesFromResponse(r).Results);
            });

            return vehicles;
        }

        async private Task<Planet> GetPlanetByName(string planetName)
        {
            var planets = new List<Planet>();
            var swapiUrl = $"{SwapiUrl}/planets/";

            while (swapiUrl != null)
            {
                string jsonResult = await HttpService.GetResponseAsJson(swapiUrl);
                var swapiPlanetsResponse = JsonConvert.DeserializeObject<SwapiPlanetsResponse>(jsonResult);
                var planet = swapiPlanetsResponse.Results.Find(p => p.Name == planetName);
                if (planet != null)
                {
                    return planet;
                }
                swapiUrl = swapiPlanetsResponse.Next;
            }
            return null;
        }
    }
}

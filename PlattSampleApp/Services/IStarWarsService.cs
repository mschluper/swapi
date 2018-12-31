using PlattSampleApp.StarWarsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlattSampleApp.Services
{
    public interface IStarWarsService
    {
        /// <summary>
        /// Get the list of all planets (using swapi).
        /// </summary>
        /// <returns>A list of all planets.</returns>
        Task<List<Planet>> GetPlanetsAsync();

        /// <summary>
        /// Get the planet that has the given number (using swapi).
        /// </summary>
        /// <param name="planetNumber">The number of the planet to retrieve.</param>
        /// <returns>The requested planet</returns>
        Task<Planet> GetPlanetAsync(int planetNumber);

        /// <summary>
        /// Get a list of residents of a planet that has a given name (using swapi).
        /// </summary>
        /// <param name="planetName">The name of the planet whose residents are to be retrieved.</param>
        /// <returns>The list of all residents of the planet.</returns>
        Task<List<Resident>> GetPlanetResidentsAsync(string planetName);

        /// <summary>
        /// Get a list of all vehicles (using swapi).
        /// </summary>
        /// <returns>The list of all vehicles.</returns>
        Task<List<Vehicle>> GetVehiclesAsync();
    }
}

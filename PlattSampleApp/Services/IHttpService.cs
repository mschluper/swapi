using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlattSampleApp.Services
{
    public interface IHttpService
    {
        /// <summary>
        /// Get the Http Get Response (string) 
        /// </summary>
        /// <param name="url">The url of the Http Get</param>
        /// <returns></returns>
        Task<string> GetResponseAsJson(string url);

        /// <summary>
        /// Assuming that the n-th page (n > 2) of a response is retrieved by appending "?page=n" to the url of the first page, 
        /// retrieve all page responses concurrently.
        /// </summary>
        /// <param name="url">The url of the Http Get (of the first page)</param>
        /// <param name="extractListCount">A function that given an Http Response yields the total number results (on all pages).</param>
        /// <param name="extractPageSizeCount">A function that given an Http Response yields the number of results (on the page).</param>
        /// <returns></returns>
        Task<List<string>> GetAllResponsePagesAsJson(string url, Func<string, int> extractListCount, Func<string, int> extractPageSize = null);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattSampleApp.Services
{
    public class HttpService : IHttpService
    {
        const int PageSize = 10;
        const bool UseCaching = true;
        private Dictionary<string, string> ResponseCache;

        public HttpService()
        {
            ResponseCache = Startup.GetStarWarsResponseCache();
        }

        public async Task<string> GetResponseAsJson(string url)
        {
            if (UseCaching && ResponseCache.ContainsKey(url))
            {
                return ResponseCache[url];
            }
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(url))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (UseCaching)
                {
                    ResponseCache[url] = data; // we may want to zip this before we store it in the cache
                }
                return data;
            }
        }

        public async Task<List<string>> GetAllResponsePagesAsJson(string url, Func<string, int> extractListCount, Func<string, int> extractPageSize = null)
        {
            string firstPageResponse = await GetResponseAsJson(url);
            var listCount = extractListCount(firstPageResponse);

            var pageSize = extractPageSize == null ? PageSize : extractPageSize(firstPageResponse);
            var pageCount = listCount / pageSize;
            if (listCount % pageSize > 0)
            {
                pageCount += 1;
            }
            var urls = new List<string>();
            for (var pageNumber = 2; pageNumber <= pageCount; pageNumber++)
            {
                urls.Add($"{url}?page={pageNumber}");
            }
            var tasks = urls.Select(u => GetResponseAsJson(u));

            var responses = await Task.WhenAll(tasks);

            // Add very first response (page 1)
            var completeList = responses.ToList();
            completeList.Add(firstPageResponse);
            return completeList;
        }
    }
}

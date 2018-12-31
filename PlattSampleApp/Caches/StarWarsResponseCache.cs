using System.Collections.Generic;

namespace PlattSampleApp.Caches
{
    public class StarWarsResponseCache
    {
        public Dictionary<string, string> ResponseCache { get; private set; }

        public StarWarsResponseCache()
        {
            ResponseCache = new Dictionary<string, string>();
        }
    }
}
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;


namespace HTMLParser.Core
{
    class HTMLoader
    {
        readonly HttpClient client;
        public string url;



        public HTMLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseURL}";

        

        }

        public async Task<string> GetSourceByPageId(int id)
        {

            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            
            var response = await client.GetAsync(currentUrl);
            
            string source = null;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }


            return source;
        }

        

    }


}

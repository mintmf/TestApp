using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestApp
{
    public class HttpHelper
    {
        public static async Task<string> GetResponseString(string text)
        {
            HttpResponseMessage response;
            string responseBody;

            using (var client = new HttpClient())
            {
                response = await client.GetAsync(text);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }
    }
}

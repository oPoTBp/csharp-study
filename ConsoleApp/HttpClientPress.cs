using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class HttpClientPress
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetRequest(string url)
        {
            HttpResponseMessage v = await client.GetAsync(url);
            string response = await v.Content.ReadAsStringAsync();
            return response;
        }

        //public static async Task<string> PostRequest(string url, string body)
        //{
        //    HttpContent httpContent = new HttpContent();
        //    HttpResponseMessage v = await client.PostAsync(url, body);
        //    string response = await v.Content.ReadAsStringAsync();
        //    return response;
        //}
    }
}

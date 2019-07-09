using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Newtonsoft.Json.Linq;

namespace TCPProject.Repository
{
    public class CrawlDataImp : ICrawlDataRepository
    {
        public async Task CrawlDataByUrlProfile(List<string> listUrlProfile)
        {
            foreach(string urlProfile in listUrlProfile){
                await GetDataByUrlProfile(urlProfile);
            }
        }
        public async Task GetDataByUrlProfile(string urlProfile)
        {
            string UrlJson = urlProfile + "?__a=1";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(UrlJson);
            JObject rss = JObject.Parse(response);
            
        }
    }
}

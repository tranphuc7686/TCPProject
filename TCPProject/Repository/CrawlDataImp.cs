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

        private const int TYPE_IMAGE = 0;
        private const int TYPE_VIDEO = 1;
        private const int TYPE_SLIDECAR = 2;
        private readonly IDataRepository dataRepository;
        public CrawlDataImp()
        {
            dataRepository = new DataRepository();
        }


        public async Task CrawlDataByUrlProfile(List<string> listUrlProfile, int ctgId)
        {
            foreach(string urlProfile in listUrlProfile){
                await GetDataByUrlProfile(urlProfile, ctgId);
            }
        }
        public async Task GetDataByUrlProfile(string urlProfile,int ctgId)
        {
            string UrlJson = urlProfile + "?__a=1";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(UrlJson);
            JObject rss = JObject.Parse(response);
            JArray arrJsonData =(JArray)rss["graphql"]["user"]["edge_owner_to_timeline_media"]["edges"];
            foreach(JObject element2 in arrJsonData)
            {
                var element = element2["node"];
             


                int typeData = element["__typename"].Equals("GraphImage") ? 0 : (element["__typename"].Equals("GraphVideo") ? 1 : 2);
                switch (typeData)
                {
                    case TYPE_IMAGE:
                        {
                            Data dataInsert = new Data
                            {
                                Caption = (string)element["edge_media_to_caption"]["edges"],
                                Url = (string)element["display_url"],
                                TypeData = TYPE_IMAGE,
                                SrcThumbail = (string)element["thumbnail_src"],
                                IsUser = 0,
                                CategoryId = ctgId

                            };
                            await InsertDataByImageType(dataInsert);
                            break;
                        }
                    case TYPE_VIDEO:
                        {
                            string shortCode = (string)element["shortcode"];
                            await InsertDataByVideoType(shortCode, ctgId);
                            break;
                        }
                    case TYPE_SLIDECAR:
                        {
                            string shortCode = (string)element["shortcode"];
                            await InsertDataBySlideCarType(shortCode, ctgId);
                            break;
                        }
                }


            }

        }
       
        private async Task InsertDataByImageType(Data data)
        {

        }
        private async Task InsertDataByVideoType(string shortCode, int ctgId)
        {
            string UrlJson = "https://www.instagram.com/p/" + shortCode + "/?__a=1";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(UrlJson);
            JObject element = JObject.Parse(response);
            element = (JObject)element["graphql"];
            /**/
            Data dataInsert = new Data
            {
                Caption = (string)element["edge_media_to_caption"]["edges"],
                Url = (string)element["video_url"],
                TypeData = TYPE_VIDEO,
                SrcThumbail = (string)element["display_url"],
                IsUser = 0,
                CategoryId = ctgId

            };
        }
        private async Task InsertDataBySlideCarType(string shortCode, int ctgId)
        {
            string UrlJson = "https://www.instagram.com/p/" + shortCode + "/?__a=1";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(UrlJson);
            JObject element = JObject.Parse(response);
            element = (JObject)element["graphql"];

            JArray listElement = (JArray)element["edge_sidecar_to_children"]["edges"];

            foreach (JObject e in listElement)
            {
                /**/
                Data dataInsert = new Data
                {
                    Caption = (string)e["edge_media_to_caption"]["edges"],
                    Url = (string)e["video_url"],
                    TypeData = element["__typename"].Equals("GraphImage") ? 0 : 1,
                    SrcThumbail = (string)e["display_resources"][0]["src"],
                    IsUser = 0,
                    CategoryId = ctgId

                };
            }

           
        }
    }
}

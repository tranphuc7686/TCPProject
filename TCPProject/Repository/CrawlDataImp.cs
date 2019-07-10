using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Newtonsoft.Json.Linq;
using TCPProject.Model;

namespace TCPProject.Repository
{
    public class CrawlDataImp : ICrawlDataRepository
    {

        private const int TYPE_IMAGE = 0;
        private const int TYPE_VIDEO = 1;
        private const int TYPE_SLIDECAR = 2;

        private readonly  DataDbContext dataRepository;
        public CrawlDataImp(DataDbContext _db)
        {
            dataRepository = _db;
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
             


                int typeData = element["__typename"].ToString().Equals("GraphImage") ? 0 : (element["__typename"].ToString().Equals("GraphVideo") ? 1 : 2);
                string shortCode = (string)element["shortcode"];
                switch (typeData)
                {
                    case TYPE_IMAGE:
                        {
                            Data dataInsert = new Data
                            {
                                Caption = element["edge_media_to_caption"]["edges"].Any() ? (string)element["edge_media_to_caption"]["edges"][0]["node"]["text"] : "",
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
                            await InsertDataByVideoType(shortCode, ctgId);
                            break;
                        }
                    case TYPE_SLIDECAR:
                        {                           
                            await InsertDataBySlideCarType(shortCode, ctgId);
                            break;
                        }
                }


            }

        }
       
        private async Task InsertDataByImageType(Data data)
        {
            await AddData(data);
        }
        private async Task InsertDataByVideoType(string shortCode, int ctgId)
        {
            try
            {
                string UrlJson = "https://www.instagram.com/p/" + shortCode + "/?__a=1";

                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(UrlJson);
                JObject element = JObject.Parse(response);
                element = (JObject)element["graphql"]["shortcode_media"];
                /**/
                Data dataInsert = new Data
                {
                    Caption = element["edge_media_to_caption"]["edges"].Any() ? (string)element["edge_media_to_caption"]["edges"][0]["node"]["text"] : "",
                    Url = (string)element["video_url"],
                    TypeData = TYPE_VIDEO,
                    SrcThumbail = (string)element["display_url"],
                    IsUser = 0,
                    CategoryId = ctgId

                };
                await AddData(dataInsert);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            
        }
        private async Task InsertDataBySlideCarType(string shortCode, int ctgId)
        {
            string UrlJson = "https://www.instagram.com/p/" + shortCode + "/?__a=1";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(UrlJson);
            JObject element = JObject.Parse(response);
            element = (JObject)element["graphql"];

            JArray listElement = (JArray)element["shortcode_media"]["edge_sidecar_to_children"]["edges"];
            try
            {
                foreach (JObject e2 in listElement)
                {
                    JObject e = (JObject)e2["node"];
                    Data dataInsert = null;
                    /**/
                    if (e["__typename"].ToString().Equals("GraphImage"))
                    {
                        dataInsert = new Data
                        {
                            Caption = element["shortcode_media"]["edge_media_to_caption"]["edges"].Any() ? (string)element["shortcode_media"]["edge_media_to_caption"]["edges"][0]["node"]["text"] : "",
                            Url = (string)e["display_url"],
                            TypeData = TYPE_IMAGE,
                            SrcThumbail = (string)e["display_resources"][0]["src"],
                            IsUser = 0,
                            CategoryId = ctgId

                        };
                    }
                    else
                    {
                        dataInsert = new Data
                        {
                            Caption = element["shortcode_media"]["edge_media_to_caption"]["edges"].Any() ? (string)element["shortcode_media"]["edge_media_to_caption"]["edges"][0]["node"]["text"] : "",
                            Url = (string)e["video_url"],
                            TypeData = TYPE_VIDEO,
                            SrcThumbail = (string)e["display_resources"][0]["src"],
                            IsUser = 0,
                            CategoryId = ctgId

                        };
                    }
                    await AddData(dataInsert);

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            

           
        }

        public async Task<int> AddData(Data data)
        {
            if (dataRepository != null)
            {
                await dataRepository.Datas.AddAsync(data);
                await dataRepository.SaveChangesAsync();


                return 1;
            }

            return 0;
        }
    }
}

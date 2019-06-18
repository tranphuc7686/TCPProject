using CrawlDataTool.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TCPProject.Model;

namespace CrawlDataTool.Service
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlCategory { get; set; }

        //get data list data
        public ICollection<Data> Datas { get; set; }

        //get data applciatons
        public int ApplicationsId { get; set; }
        public Applications Applications { get; set; }

        public Category()
        {
            Datas = new HashSet<Data>();
        }

    }
}
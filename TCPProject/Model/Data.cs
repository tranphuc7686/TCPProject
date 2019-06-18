using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlDataTool.Service;
using Newtonsoft.Json.Linq;

namespace CrawlDataTool.Model
{
    public class Data
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public int TypeData { get; set; }
        public string SrcThumbail { get; set; }      
        public int IsUser { get; set; }

        //get data category
        public int CategoryId { get; set; }
        public Category Category { get; set; }



        public override string ToString()
        {
            return Id;
        }


    }
}

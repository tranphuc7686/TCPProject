using CrawlDataTool.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TCPProject.Model
{
    public class Applications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //get data list data
        public ICollection<Category> Categories { get; set; }


        public Applications()
        {
            Categories = new HashSet<Category>();
        }
    }
}


using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace TCPProject.Model
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
          : base(options)
        {
        }
       

        public DbSet<Data> Datas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Applications> Applications { get; set; }


    }
}

using CrawlDataTool.Model;
using CrawlDataTool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCPProject.Repository
{
    public interface ICrawlDataRepository
    {       
        Task CrawlDataByUrlProfile(List<string> listUrlProfile,int ctgId);
    }
}

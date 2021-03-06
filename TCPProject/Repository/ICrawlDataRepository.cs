﻿using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCPProject.Repository
{
    public interface ICrawlDataRepository
    {       
        Task CrawlDataByUrlProfile(List<string> listUrlProfile,int ctgId);

        Task<int> AddData(Data data);
        Task<string> AddImageAsync(IFormFile data);
    }
}

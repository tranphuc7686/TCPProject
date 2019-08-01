using CrawlDataTool.Model;
using CrawlDataTool.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCPProject.Model;
using TCPProject.ViewModel;

namespace TCPProject
{
    public interface IDataRepository
    {
        Task<List<Category>> GetCategories();

        Task<List<DataViewModel>> GetDatas(int ctgId, int index);

        Task<List<CategoryViewModel>> GetCategoriesById(int ctgId);

        Task<DataViewModel> GetData(string postId);

        Task<int> AddData(Data data);

        Task<int> DeleteData(int? dataId);

        Task UpdateData(Data data);
        Task PublicElement(string id);

        Task<List<DataViewModel>> GetDatasPending(int ctgId, int index);
        Task<List<ApplicationAdminViewModel>> GetApplications();
    }
}
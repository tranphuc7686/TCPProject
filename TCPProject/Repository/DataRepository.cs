using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TCPProject.Model;
using TCPProject.ViewModel;

namespace TCPProject
{
    internal class DataRepository
    : IDataRepository
    {
        DataDbContext db;
        public const int PENDING_STATUS = 2;
        public DataRepository(DataDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Category>> GetCategories()
        {
            if (db != null)
            {
                return await db.Categories.ToListAsync();
            }

            return null;
        }

        public async Task<List<DataViewModel>> GetDatas(int ctgId, int index)
        {
            if (db != null)
            {
                return await db.Datas.Where(e => e.CategoryId == ctgId && e.IsUser != PENDING_STATUS).Skip(index).Take(30).Select(e => new DataViewModel
                {
                    Id = e.Id,
                    Caption = e.Caption,
                    IsUser = e.IsUser,
                    SrcThumbail = e.SrcThumbail,
                    TypeData = e.TypeData,
                    Url = e.Url
                }).ToListAsync();
            }

            return null;
        }

        public async Task<DataViewModel> GetData(string dataId)
        {
            if (db != null)
            {
                return await db.Datas
                    .Where(e => e.Id.Equals(dataId))
                    .Select(e => new DataViewModel
                    {
                        Id = e.Id,
                        Caption = e.Caption,
                        IsUser = e.IsUser,
                        SrcThumbail = e.SrcThumbail,
                        TypeData = e.TypeData,
                        Url = e.Url
                    }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddData(Data data)
        {
            if (db != null)
            {
                try
                {
                    await db.Datas.AddAsync(data);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
               

                return 1;
            }

            return 0;
        }

        public async Task<int> DeletePost(int? dataId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                //var post = await db.Datas.FirstOrDefaultAsync(x => x.id == dataId);

                //if (post != null)
                //{
                //    //Delete that post
                //    db.Post.Remove(post);

                //    //Commit the transaction
                //    result = await db.SaveChangesAsync();
                //}
                //return result;
            }

            return result;
        }


        public async Task UpdatePost(Data data)
        {
            if (db != null)
            {
                //Delete that post
                db.Datas.Update(data);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

       

        public Task<int> DeleteData(int? dataId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateData(Data data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CategoryViewModel>> GetCategoriesById(int applicationId)
        {
            List<CategoryViewModel> resulf = new List<CategoryViewModel>();

            if (db != null)
            {
                
                return db.Categories
                    .Where(e => e.ApplicationsId == applicationId)
                    .Select(e => new CategoryViewModel { Id = e.Id, Name = e.Name, UrlCategory = e.UrlCategory })
                    .ToList();
            }

            return null;
        }

        public async Task<int> PublicElement(string id)
        {
            Data resulf = new Data();

            if (db != null)
            {

                resulf = db.Datas
                    .Where(e => e.Id.Equals(id))
                    .First();
                resulf.IsUser = 1;
                await UpdatePost(resulf);
                
            }

            return 0;
        }

        Task IDataRepository.PublicElement(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataViewModel>> GetDatasPending(int appId, int index)
        {
            if (db != null)
            {
                return await db.Datas.Where(e => e.Category.ApplicationsId == appId && e.IsUser == PENDING_STATUS).Skip(index).Take(30).Select(e => new DataViewModel
                {
                    Id = e.Id,
                    Caption = e.Caption,
                    IsUser = e.IsUser,
                    SrcThumbail = e.SrcThumbail,
                    TypeData = e.TypeData,
                    Url = e.Url
                }).ToListAsync();
            }

            return null;
        }

        public Task<List<ApplicationAdminViewModel>> GetApplications()
        {


            if (db != null)
            {

                string script = "SELECT APP.\"Id\",APP.\"Name\",COUNT(DATAS.\"Id\") FROM \"Applications\" APP INNER JOIN \"Categories\" CTG ON APP.\"Id\" = CTG.\"ApplicationsId\" LEFT JOIN \"Datas\" DATAS ON CTG.\"Id\" = DATAS.\"CategoryId\" AND DATAS.\"IsUser\" = 2 GROUP BY APP.\"Id\" ORDER BY APP.\"Id\"";
                using (var context = db)
                {
                    using (var command = context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = script;
                        command.CommandType = CommandType.Text;

                        context.Database.OpenConnection();

                        using (var result = command.ExecuteReader())
                        {


                            while (result.Read())
                            {
                                var id = result[0];
                                var name = result[1];
                                var count = result[2];
                            }


                        }

                    }
                }
            }
            return null;
        }
       

        private List<ApplicationAdminViewModel> ToApplicationsViewModel(DbDataReader dataReader)
        {
            List<ApplicationAdminViewModel> resulf = new List<ApplicationAdminViewModel>();
            using (var result = dataReader)
            {

                while (result.Read())
                {
                    
                    var Count =result[2];
                    resulf.Add(new ApplicationAdminViewModel
                    {
                        applications = new Applications
                        {
                            Id = (int)result[0],
                            Name = result[1].ToString()
                        },
                        CountElementPending = 0
                    });
                }
                return resulf;

            }
        }
    }
}
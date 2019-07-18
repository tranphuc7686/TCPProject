using CrawlDataTool.Model;
using CrawlDataTool.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                return await db.Datas.Skip(30).Take(index).Where(e => e.CategoryId == ctgId).Select(e => new DataViewModel
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

        
    }
}
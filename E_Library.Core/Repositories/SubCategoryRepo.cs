using E_Library.Core.Repositories.IRepositories;
using E_Library.Data.Context;
using E_Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Repositories
{
    public class SubCategoryRepo : RepositoryBase<SubCategory>, ISubCategoryRepo
    {
        private AppDbContext _DbContext;

        public SubCategoryRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }
        public void Update(SubCategory subCategory)
        {
            _DbContext.Entry(subCategory).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}

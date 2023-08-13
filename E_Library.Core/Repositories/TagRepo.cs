using E_Library.Core.Repositories.IRepositories;
using E_Library.Data.Context;
using E_Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Repositories
{
    public class TagRepo : RepositoryBase<Tag>, ITagRepo
    {
        private AppDbContext _DbContext;

        public TagRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }
        public void Update(Tag tag)
        {
            _DbContext.Entry(tag).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}

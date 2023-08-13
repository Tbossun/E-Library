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
    public class PublisherRepo : RepositoryBase<Publisher>, IPublisherRepo
    {
        private AppDbContext _DbContext;

        public PublisherRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }
        public void Update(Publisher publisher)
        {
            _DbContext.Entry(publisher).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}

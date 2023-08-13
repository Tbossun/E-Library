using E_Library.Data.Context;
using E_Library.Data.Models;
using E_Library.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Repositories
{
    public class AuthorRepo : RepositoryBase<Author>, IAuthorRepo
    {
        private AppDbContext _DbContext;

        public AuthorRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Author author)
        {
            _DbContext.Entry(author).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}

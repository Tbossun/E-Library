using E_Library.Data.Context;
using E_Library.Data.Models;
using E_Library.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Repositories
{
    public class BookRepo : RepositoryBase<Book>, IBookRepo
    {
        private AppDbContext _DbContext;

        public BookRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Book book)
        {
            _DbContext.Entry(book).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }

        public IEnumerable<Book> GetBooksByCategory(string id)
        {
            return _DbContext.Set<Book>()
                .Where(book => book.CategoryId == id)
                    .ToList();
        }

        /*public IEnumerable<Category> GetSubcategoryByCategory(Category category)
        {
            return _DbContext.Set<Category>()
                .Where(u => u.Subcategories)
                .ToList();
        }*/

    }
}

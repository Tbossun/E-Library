﻿using E_Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Repositories.IRepositories
{
    public interface IBookRepo : IRepositoryBase<Book>
    {
        void Update(Book book);
        IEnumerable<Book> GetBooksByCategory(string id);
    }
}

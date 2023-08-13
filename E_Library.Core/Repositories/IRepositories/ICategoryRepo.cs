﻿using E_Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Repositories.IRepositories
{
    public interface ICategoryRepo : IRepositoryBase<Category>
    {
        void Update(Category category);
    }
}
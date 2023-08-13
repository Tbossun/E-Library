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
    public class AddressRepo : RepositoryBase<Address>, IAddressRepo
    {
        private AppDbContext _DbContext;

        public AddressRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Address address)
        {
            _DbContext.Entry(address).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}

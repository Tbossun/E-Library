using E_Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Repositories.IRepositories
{
    public interface IAddressRepo : IRepositoryBase<Address>
    {
        void Update(Address address);
    }
}

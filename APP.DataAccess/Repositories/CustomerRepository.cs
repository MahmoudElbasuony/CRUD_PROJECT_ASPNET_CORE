using APP.Entities;
using APP.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {

        }
        public override Task<List<Customer>> GetAll(int pageNumber = 0, int pageSize = 100)
                         => Entities.AsQueryable().Include(p => p.Company).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

    }
}

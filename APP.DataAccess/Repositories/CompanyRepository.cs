using APP.Entities;
using APP.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.DataAccess.Repositories
{
    public class CompanyRepository : BaseRepository<Company> , ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {

        }
    }
}

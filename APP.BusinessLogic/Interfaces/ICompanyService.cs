using APP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP.BusinessLogic.Interfaces
{
    public interface ICompanyService : IBussinessService<Company>
    {
        Task<Company> CreateCompany(string code, byte[] companyPhoto, string name, int? phoneNumber);
        Task DeleteCompany(int companyId);
        Task<Company> GetCompanyById(int companyId);
        Task<List<Company>> GetAllCompanies(int pageNumber = 0, int pageSize = 100);
        Task<Company> UpdateCompany(int companyId, string code, byte[] companyPhoto, string name, int? phoneNumber);
    }
}

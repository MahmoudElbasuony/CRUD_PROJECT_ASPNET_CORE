using APP.BusinessLogic.Interfaces;
using APP.Entities;
using APP.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BusinessLogic.Services
{
    public class CompanyService : BaseBusinessService<Company>, ICompanyService
    {
        private readonly ICompanyRepository _CompanyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _CompanyRepository = companyRepository;
        }

        public async Task<Company> CreateCompany(string code, byte[] companyPhoto, string name, int? phoneNumber)
        {

            var newCompany = new Company
            {
                Code = code,
                CompanyPhoto = companyPhoto,
                Name = name,
                PhoneNumber = phoneNumber,
            };

            _CompanyRepository.Create(newCompany);
            await _CompanyRepository.Save();

            return newCompany;
        }

        public async Task DeleteCompany(int companyId)
        {
            var company = await GetCompanyById(companyId);
            _ = company ?? throw new Exception("Company not exists!");
        }

        public Task<List<Company>> GetAllCompanies(int pageNumber = 0, int pageSize = 100) => _CompanyRepository.GetAll(pageNumber, pageSize);

        public Task<Company> GetCompanyById(int companyId) =>  _CompanyRepository.GetById(companyId);

        public async Task<Company> UpdateCompany(int companyId, string code, byte[] companyPhoto, string name, int? phoneNumber)
        {
            var company = await _CompanyRepository.GetById(companyId);
            _ = company ?? throw new Exception("Company not exists!");

            company.Name = name;
            company.PhoneNumber = phoneNumber;
            company.Code = code;
            company.CompanyPhoto = companyPhoto ?? company.CompanyPhoto;

            _CompanyRepository.Update(company);
            await _CompanyRepository.Save();

            return company;
        }

    }
}

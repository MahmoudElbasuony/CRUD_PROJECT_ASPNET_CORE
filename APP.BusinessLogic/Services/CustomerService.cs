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
    public class CustomerService : BaseBusinessService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly ICompanyRepository _CompanyRepository;

        public CustomerService(ICustomerRepository customerRepository, ICompanyRepository companyRepository) : base()
        {
            _CustomerRepository = customerRepository;
            _CompanyRepository = companyRepository;
        }

        public async Task<Customer> CreateCustomer(string name, int? mobileNo, string job, DateTime? birthDate, int companyId, string email)
        {

            var company = await _CompanyRepository.GetById(companyId);
            if (company == null)
                throw new Exception("Company not exists!");

            var newCustomer = new Customer
            {
                BirthDate = birthDate,
                CompanyId = companyId,
                Job = job,
                MobileNo = mobileNo,
                Name = name,
                Email = email,
            };

            _CustomerRepository.Create(newCustomer);
            await _CustomerRepository.Save();

            return newCustomer;
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await GetCustomerById(customerId);
            _ = customer ?? throw new Exception("Customer not exists!");
            await _CustomerRepository.Delete(customerId);
            await _CustomerRepository.Save();
        }

        public async Task<List<Customer>> GetAllCustomers(int pageNumber = 0, int pageSize = 100)
        {
           return await _CustomerRepository.GetAll(pageNumber, pageSize);
        }

        public async Task<Customer> GetCustomerById(int customerId) => await _CustomerRepository.GetById(customerId);

        public async Task<Customer> UpdateCustomer(int id, string name, int? mobileNo, string job, DateTime? birthDate, int companyId, string email)
        {
            var customer = await _CustomerRepository.GetById(id);
            _ = customer ?? throw new Exception("Customer not exists!");
            var company = await _CompanyRepository.GetById(companyId);
            _ = company ?? throw new Exception("Company not exists!");

            customer.Job = job;
            customer.MobileNo = mobileNo;
            customer.Name = name;
            customer.Email = email;
            customer.CompanyId = company.ID;
            customer.Company = company;
            customer.BirthDate = birthDate;

            _CustomerRepository.Update(customer);
            await _CustomerRepository.Save();

            return customer;
        }
    }
}

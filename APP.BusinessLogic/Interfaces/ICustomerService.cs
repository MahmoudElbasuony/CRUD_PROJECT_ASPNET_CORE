using APP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP.BusinessLogic.Interfaces
{
    public interface ICustomerService : IBussinessService<Customer>
    {
        Task<Customer> CreateCustomer(string name, int? mobileNo, string job, DateTime? birthDate, int companyId, string email);
        Task DeleteCustomer(int customerId);
        Task<Customer> GetCustomerById(int customerId);
        Task<List<Customer>> GetAllCustomers(int pageNumber = 0, int pageSize = 100);
        Task<Customer> UpdateCustomer(int id, string name, int? mobileNo, string job, DateTime? birthDate, int companyId, string email);

    }
}

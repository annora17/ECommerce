using EC.Application.DTOs.Customers;
using EC.Application.Interfaces;
using EC.Domain.Entities.Customers;
using EC.Domain.Guards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IAsyncRepository<Customer, int> _customerRepositoryAsync;
        public CustomerService(
            IRepository<Customer, int> customerRepository, 
            IAsyncRepository<Customer, int> customerRepositoryAsync)
        {
            _customerRepository = customerRepository;
            _customerRepositoryAsync = customerRepositoryAsync;
        }
        public Task AddCustomerAddressAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddCustomerBillingAddressAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateCustomerAsync(AddCustomerDTO input)
        {
            Guard.Instance.Null(input, nameof(AddCustomerDTO));

            var customer = new Customer(input.IdentityID, input.Name, input.LastName);

            await _customerRepositoryAsync.AddEntityAsync(customer);
        }
    }
}

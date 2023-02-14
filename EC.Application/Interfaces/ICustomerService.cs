using EC.Application.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Application.Interfaces
{
    public interface ICustomerService
    {
        Task CreateCustomerAsync(AddCustomerDTO input);
        Task AddCustomerAddressAsync();
        Task AddCustomerBillingAddressAsync();
    }
}

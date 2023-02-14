using EC.Domain.Entities.Base;
using EC.Domain.Entities.ValueObjects;
using EC.Domain.Guards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Entities.Customers
{
    public class Customer : AuditEntities
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }

        private List<Address> _customerAddresses = new();
        public IReadOnlyList<Address> CustomerAddresses { get => _customerAddresses.AsReadOnly(); }

        private List<Address> _customerBillingAddresses = new();
        public IReadOnlyList<Address> CustomerBillingAddresses { get => _customerBillingAddresses.AsReadOnly(); }
        public Customer(int identityID, string name, string lastName, DateTime? birthDate = null, bool IsBirthDateControl = false)
        {
            Guard.Instance.IsEntityIdValidForInt(identityID, "CustomerID");
            Guard.Instance.NullOrWhiteSpace(name, "CustomerName");
            Guard.Instance.StringLength(name, "CustomerName", minlength: 2, maxlength: 36);
            Guard.Instance.NullOrWhiteSpace(lastName, "CustomerLastName");
            Guard.Instance.StringLength(lastName, "CustomerLastName", minlength: 2, maxlength: 36);

            if (IsBirthDateControl)
                Guard.Instance.IsValidDatetime(birthDate.Value, "CustomerBirthDate");

            ID = identityID;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
        }

        private Customer() { }

        public void AddCustomerAddress(Address address)
        {
            Guard.Instance.Null(address, "CustomerAddress");
            Guard.Instance.AddressSameName(address, CustomerAddresses);

            if (address.IsDefault)
                foreach (var item in _customerAddresses.Where(adrs => adrs.IsDefault))
                {
                    item.DisableDefaultState();
                    IsUpdatedChanged();
                }

            _customerAddresses.Add(address);
        }
        public void AddCustomerBillingAddress(Address address)
        {
            Guard.Instance.Null(address, "CustomerBillingAddress");
            Guard.Instance.AddressSameName(address, CustomerBillingAddresses);

            if (address.IsDefault)
                foreach (var item in _customerBillingAddresses.Where(adrs => adrs.IsDefault))
                {
                    item.DisableDefaultState();
                    IsUpdatedChanged();
                }

            _customerBillingAddresses.Add(address);
        }
    }
}

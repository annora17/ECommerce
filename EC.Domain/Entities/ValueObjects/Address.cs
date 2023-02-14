using EC.Domain.Guards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Entities.ValueObjects
{
    public class Address
    {
        public int AddressID { get; private set; }
        public string Country { get; private set; }
        public int CountryCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Quarter { get; private set; }
        public string CSBM { get; private set; }
        public string OutDoorNumber { get; private set; }
        public string InDoorNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string FullAddress { get; private set; }
        public string AddressName { get; private set; }
        public bool IsDefault { get; private set; }

        public Address(string city, string state, string quarter, string cSBM, string outDoorNumber, string inDoorNumber, bool isDefault = false, string addressName = null, string postalCode = null, string fullAddress = null, string country = "Türkiye", int countryCode = 90, bool isPostalCodeControl = false, bool isFullAddressEntry = false)
        {
            Guard.Instance.NullOrWhiteSpace(city, nameof(City));
            Guard.Instance.StringLength(city, nameof(City), minlength: 2, maxlength:32);
            Guard.Instance.AlphabeticUnicode(city, nameof(City));
            Guard.Instance.NullOrWhiteSpace(state, nameof(State));
            Guard.Instance.StringLength(state, nameof(State), minlength: 2, maxlength:32);
            Guard.Instance.AlphabeticUnicode(state, nameof(state));
            Guard.Instance.NullOrWhiteSpace(quarter, nameof(Quarter));
            Guard.Instance.StringLength(quarter, nameof(Quarter), minlength: 2, maxlength: 32);
            Guard.Instance.AlphabeticUnicodeWithWhiteSpace(quarter, nameof(Quarter));
            Guard.Instance.NullOrWhiteSpace(cSBM, nameof(CSBM));
            Guard.Instance.StringLength(cSBM, nameof(CSBM), minlength: 2, maxlength: 32);
            Guard.Instance.AlphabeticUnicodeWithWhiteSpace(cSBM, nameof(CSBM));
            Guard.Instance.NullOrWhiteSpace(outDoorNumber, nameof(OutDoorNumber));
            Guard.Instance.StringLength(outDoorNumber, nameof(OutDoorNumber), minlength: 1, maxlength: 8);
            Guard.Instance.NullOrWhiteSpace(inDoorNumber, nameof(InDoorNumber));
            Guard.Instance.StringLength(inDoorNumber, nameof(InDoorNumber), minlength: 1, maxlength: 8);

            if (isPostalCodeControl)
            {
                Guard.Instance.NullOrWhiteSpace(postalCode, nameof(PostalCode));
                Guard.Instance.StringLength(postalCode, nameof(PostalCode), minlength: 4, maxlength: 20);
                Guard.Instance.AlfaNumericUnicode(postalCode, nameof(PostalCode));
                PostalCode = postalCode;
            }

            if (isFullAddressEntry)
            {
                Guard.Instance.NullOrWhiteSpace(fullAddress, nameof(FullAddress));
                Guard.Instance.StringLength(fullAddress, nameof(FullAddress), minlength: 16, maxlength: 256);
                FullAddress = fullAddress;
            }

            Country = country;
            CountryCode = countryCode;
            City = city;
            State = state;
            Quarter = quarter;
            CSBM = cSBM;
            OutDoorNumber = outDoorNumber;
            InDoorNumber = inDoorNumber;
            IsDefault = isDefault;
            AddressName = String.IsNullOrWhiteSpace(addressName) ? String.Empty : addressName;
        }
        private Address() { }
        public void DisableDefaultState()
        {
            IsDefault = false;
        }
    }
}

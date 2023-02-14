using EC.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Guards
{
    public static class GuardAddressExtensions
    {
        private static string ErrorMessageSameAddressName => $"Aynı etikete sahip adres bilgisi olamaz!";

        public static void AddressSameName(this IGuardService guard, Address input, IReadOnlyCollection<Address> addresses)
        {
            Guard.Instance.Null(input, "Address");
            Guard.Instance.Null(addresses, nameof(addresses));

            if (String.IsNullOrWhiteSpace(input.AddressName))
                return;

            var address = addresses.FirstOrDefault(q => q.AddressName.Equals(input.AddressName, StringComparison.OrdinalIgnoreCase));

            if (input != null)
                throw new ArgumentException(ErrorMessageSameAddressName, nameof(Address.AddressName));
        }

    }
}

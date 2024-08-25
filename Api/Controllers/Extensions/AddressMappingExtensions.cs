using Api.DTOs;
using Core.Entities;

namespace Api.Controllers.Extensions;

public static class AddressMappingExtensions
{
    public static AddressDto ToDto(this Address address)
    {
        ArgumentNullException.ThrowIfNull(address); // if (address == null) throw new ArgumentNullException(nameof(address));

        return new AddressDto {
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
        };
    }
    public static Address ToEntity(this AddressDto addressDto)
    {
        ArgumentNullException.ThrowIfNull(addressDto); // if (address == null) throw new ArgumentNullException(nameof(address));

        return new Address {
            Line1 = addressDto.Line1,
            Line2 = addressDto.Line2,
            City = addressDto.City,
            State = addressDto.State,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country
        };
    }

    public static void UpdateFromDto(this Address address, AddressDto addressDto)
    {
        ArgumentNullException.ThrowIfNull(address); // if (address == null) throw new ArgumentNullException(nameof(address));
        ArgumentNullException.ThrowIfNull(addressDto); // if (address == null) throw new ArgumentNullException(nameof(address));

        address.Line1 = addressDto.Line1;
        address.Line2 = addressDto.Line2;
        address.City = addressDto.City;
        address.State = addressDto.State;
        address.PostalCode = addressDto.PostalCode;
        address.Country = addressDto.Country;
    }
}

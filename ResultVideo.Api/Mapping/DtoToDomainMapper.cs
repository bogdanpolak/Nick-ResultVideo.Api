using ResultVideo.Api.Contracts.Data;
using ResultVideo.Api.Models;

namespace ResultVideo.Api.Mapping;

public static class DtoToDomainMapper
{
    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer
        {
            Id = customerDto.Id,
            Email = customerDto.Email,
            GitHubUsername = customerDto.GitHubUsername,
            FullName = customerDto.FullName,
            DateOfBirth = DateOnly.FromDateTime(customerDto.DateOfBirth)
        };
    }
}

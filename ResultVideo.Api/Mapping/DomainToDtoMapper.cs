﻿using ResultVideo.Api.Contracts.Data;
using ResultVideo.Api.Models;

namespace ResultVideo.Api.Mapping;

public static class DomainToDtoMapper
{
    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Email = customer.Email,
            GitHubUsername = customer.GitHubUsername,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth.ToDateTime(TimeOnly.MinValue)
        };
    }
}

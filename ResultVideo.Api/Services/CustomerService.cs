using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using ResultVideo.Api.Mapping;
using ResultVideo.Api.Models;
using ResultVideo.Api.Repositories;

namespace ResultVideo.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly IValidator<Customer> _validator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IGitHubService _gitHubService;

    public CustomerService(IValidator<Customer> validator, 
        ICustomerRepository customerRepository, IGitHubService gitHubService)
    {
        _validator = validator;
        _customerRepository = customerRepository;
        _gitHubService = gitHubService;
    }

    public async Task<Result<Customer>> CreateAsync(Customer customer)
    {
        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<Customer>(validationException);
        }

        var isValidGitHubUser = await _gitHubService.IsValidGitHubUser(customer.GitHubUsername);
        if (!isValidGitHubUser)
        {
            var message = $"There is no GitHub user with username {customer.GitHubUsername}";
            return new Result<Customer>(new ValidationException(message, GenerateValidationError(nameof(customer.GitHubUsername), message)));
        }
        
        var customerDto = customer.ToCustomerDto();
        await _customerRepository.CreateAsync(customerDto);
        return customer;
    }

    public async Task<Customer?> GetAsync(Guid id)
    {
        var customerDto = await _customerRepository.GetAsync(id);
        return customerDto?.ToCustomer();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var customerDtos = await _customerRepository.GetAllAsync();
        return customerDtos.Select(x => x.ToCustomer());
    }

    public async Task<Result<Customer>> UpdateAsync(Customer customer)
    {
        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<Customer>(validationException);
        }
        
        var isValidGitHubUser = await _gitHubService.IsValidGitHubUser(customer.GitHubUsername);
        if (!isValidGitHubUser)
        {
            var message = $"There is no GitHub user with username {customer.GitHubUsername}";
            return new Result<Customer>(new ValidationException(message, GenerateValidationError(nameof(customer.GitHubUsername), message)));
        }
        
        var customerDto = customer.ToCustomerDto();
        await _customerRepository.UpdateAsync(customerDto);
        return customer;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _customerRepository.DeleteAsync(id);
    }

    private static ValidationFailure[] GenerateValidationError(string paramName, string message)
    {
        return new []
        {
            new ValidationFailure(paramName, message)
        };
    }
}

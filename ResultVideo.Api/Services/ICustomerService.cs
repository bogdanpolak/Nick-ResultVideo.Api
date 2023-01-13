using LanguageExt.Common;
using ResultVideo.Api.Models;

namespace ResultVideo.Api.Services;

public interface ICustomerService
{
    Task<Result<Customer>> CreateAsync(Customer customer);

    Task<Customer?> GetAsync(Guid id);

    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Result<Customer>> UpdateAsync(Customer customer);

    Task<bool> DeleteAsync(Guid id);
}

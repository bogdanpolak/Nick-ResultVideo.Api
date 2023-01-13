using System.Text.RegularExpressions;
using FluentValidation;
using ResultVideo.Api.Models;

namespace ResultVideo.Api.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FullName)
            .NotEmpty()
            .Matches("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
            .WithMessage("Invalid fullname format");
            
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.GitHubUsername)
            .NotEmpty()
            .Matches("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
    }
}

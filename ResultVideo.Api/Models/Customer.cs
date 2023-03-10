namespace ResultVideo.Api.Models;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string GitHubUsername { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateOnly DateOfBirth { get; init; } = default!;
}

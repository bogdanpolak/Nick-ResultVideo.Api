using FluentValidation;

namespace ResultVideo.Api.Validation;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = 400;
            var error = exception.ToProblemDetails();
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}

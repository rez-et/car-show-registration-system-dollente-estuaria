namespace Api.Common.Filters;

using Api.Common.Extensions;
using Domain.Common;
using FluentValidation;

public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null)
        {
            return await next(context);
        }

        var request = context.Arguments.OfType<T>().FirstOrDefault();
        if (request is null)
        {
            return Result.Failure(
                Error.Validation("Validation.MissingRequest", "Request payload is required."))
                .ToProblemDetails();
        }

        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

            var detail = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage).Distinct());

            return Result.Failure(Error.Validation("Validation.Failed", detail))
                .ToProblemDetails(errors);
        }

        return await next(context);
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClientManagementAPI.Filters
{
    public class ValidationFilter<T> : IAsyncActionFilter
    where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.Values
                .OfType<T>()
                .FirstOrDefault();

            if (model == null)
            {
                await next();
                return;
            }

            var result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(
                    new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation failed."
                    });

                return;
            }

            await next();
        }
    }

}

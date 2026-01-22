
using FluentValidation.Results;

namespace RestaurantApi.Core.Application.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationErrorException(IEnumerable<ValidationFailure> failures)
            : base("Ocurrió uno o más errores de validación")
        {
            Errors = failures
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        public ValidationErrorException(string errorMessage)
            : base(errorMessage)
        {
            Errors = new Dictionary<string, string[]>
            {
                {string.Empty, new[] { errorMessage} }
            };
        }
    }
}

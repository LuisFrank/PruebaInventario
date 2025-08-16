using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventario.BusinessLogicLayer.Exceptions
{
    /// <summary>
    /// Excepción específica para errores de validación de datos
    /// </summary>
    public class ValidationException : BusinessException
    {
        public List<string> ValidationErrors { get; }
        public string? PropertyName { get; }

        public ValidationException(string message) : base(message, "VALIDATION_ERROR")
        {
            ValidationErrors = new List<string> { message };
        }

        public ValidationException(string message, string propertyName) : base(message, "VALIDATION_ERROR")
        {
            ValidationErrors = new List<string> { message };
            PropertyName = propertyName;
        }

        public ValidationException(List<string> validationErrors) : base(GetCombinedMessage(validationErrors), "VALIDATION_ERROR")
        {
            ValidationErrors = validationErrors ?? new List<string>();
        }

        public ValidationException(List<string> validationErrors, string propertyName) : base(GetCombinedMessage(validationErrors), "VALIDATION_ERROR")
        {
            ValidationErrors = validationErrors ?? new List<string>();
            PropertyName = propertyName;
        }

        public ValidationException(string message, Exception innerException) : base(message, "VALIDATION_ERROR", innerException)
        {
            ValidationErrors = new List<string> { message };
        }

        private static string GetCombinedMessage(List<string> errors)
        {
            if (errors == null || !errors.Any())
                return "Error de validación";

            if (errors.Count == 1)
                return errors.First();

            return $"Se encontraron {errors.Count} errores de validación: {string.Join("; ", errors)}";
        }

        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error) && !ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            if (errors != null)
            {
                foreach (var error in errors.Where(e => !string.IsNullOrWhiteSpace(e)))
                {
                    AddError(error);
                }
            }
        }

        public bool HasErrors => ValidationErrors.Any();

        public override string ToString()
        {
            var result = $"[{ErrorCode}] {Message}";
            
            if (!string.IsNullOrEmpty(PropertyName))
            {
                result += $" | Property: {PropertyName}";
            }
            
            if (ValidationErrors.Any())
            {
                result += $" | Errors: [{string.Join(", ", ValidationErrors)}]";
            }
            
            if (InnerException != null)
            {
                result += $" | Inner: {InnerException.Message}";
            }
            
            return result;
        }
    }
}
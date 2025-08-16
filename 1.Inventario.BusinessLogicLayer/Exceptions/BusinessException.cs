using System;

namespace Inventario.BusinessLogicLayer.Exceptions
{
    /// <summary>
    /// Excepción base para errores de lógica de negocio
    /// </summary>
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }
        public object? ErrorData { get; }

        public BusinessException(string message) : base(message)
        {
            ErrorCode = "BUSINESS_ERROR";
        }

        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = "BUSINESS_ERROR";
        }

        public BusinessException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message, string errorCode, object errorData) : base(message)
        {
            ErrorCode = errorCode;
            ErrorData = errorData;
        }

        public BusinessException(string message, string errorCode, object errorData, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
            ErrorData = errorData;
        }

        public override string ToString()
        {
            var result = $"[{ErrorCode}] {Message}";
            
            if (ErrorData != null)
            {
                result += $" | Data: {ErrorData}";
            }
            
            if (InnerException != null)
            {
                result += $" | Inner: {InnerException.Message}";
            }
            
            return result;
        }
    }
}
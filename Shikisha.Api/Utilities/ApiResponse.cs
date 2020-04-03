using System.Collections.Generic;

namespace Shikisha.Utilities
{
    /// <summary>
    /// Object used for returning information back to the client calling the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public List<ApiError> Errors {get; set;}
        public T Data {get; set;}
        public ApiResponse(){}
        public ApiResponse(List<ApiError> errors)
            => Errors = errors;

        public ApiResponse(T data)
            => Data = data;
    }

    /// <summary>
    /// Represents an error to be displayed back to the client calling the API.
    /// </summary>
    public class ApiError
    {
        public string ErrorCode {get;}
        public string Message {get;}
        public ApiError(string errorCode, string errorMessage)
            => (ErrorCode, Message) = (errorCode, errorMessage);
    }
}
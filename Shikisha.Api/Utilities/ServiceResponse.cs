using System.Collections.Generic;

namespace Shikisha.Utilities
{
    /// <summary>
    /// Object used for grouping data collected within a service call.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public List<ServiceError> Errors {get; set;}
        public T Data {get; set;}
        public ServiceResponse(){}
        public ServiceResponse(List<ServiceError> errors)
            => Errors = errors;

        public ServiceResponse(T data)
            => Data = data;
    }

    /// <summary>
    /// Represents an error to be displayed back to the client when calling a service method.
    /// </summary>
    public class ServiceError
    {
        public string ErrorCode {get;}
        public string Message {get;}
        public ServiceError(string errorCode, string errorMessage)
            => (ErrorCode, Message) = (errorCode, errorMessage);
    }
}
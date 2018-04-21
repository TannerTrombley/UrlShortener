using System;

namespace Common.Exceptions.ApiExceptions
{
    public class ApiNotFoundException : Exception
    {
        public ApiNotFoundException(string message) : base(message) { }
        public ApiNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}

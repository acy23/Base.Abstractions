using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response
{
    public class ServiceResponse<T> where T : class
    {
        public bool HasValue { get; }
        public T Value { get; }
        public int StatusCode { get; }
        public string Message { get; set; }

        public ServiceResponse(T value)
        {
            Value = value;
            HasValue = true;
            StatusCode = 200;
            Message = "Success";
        }

        public ServiceResponse(int statusCode, string message) 
        {
            Value = null;
            HasValue = false;
            StatusCode = statusCode;
            Message = message;
        }

        public static ServiceResponse<T> Forbidden(string message)
        {
            return new ServiceResponse<T>(403, message);
        }

        public static ServiceResponse<T> NotFound(string message)
        {
            return new ServiceResponse<T>(404, message);
        }

        public static ServiceResponse<T> Ok(T data)
        {
            return new ServiceResponse<T>(data);
        }

        public new ServiceResponse<T> NoContent()
        {
            return new ServiceResponse<T>(204, "No content");
        }

    }

}


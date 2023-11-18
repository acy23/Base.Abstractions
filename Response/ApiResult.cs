using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Response
{
    public static class ApiResult
    {
        public static ContentResult ToApiResult<T>(this ServiceResponse<T> response, ControllerContext? controllerContext = null) where T : class
        {
            var apiResponse = new ApiResponse
            {
                data = response.Value,
                message = response.Message
            };

            var contentResult = new ContentResult
            {
                Content = JsonSerializer.Serialize(apiResponse),
                StatusCode = response.StatusCode,
                ContentType = "application/json",
            };

            if(controllerContext is not null)
            {
                controllerContext.HttpContext.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                controllerContext.HttpContext.Response.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
                //controllerContext.HttpContext.Response.Headers.Add("Server", "Kestrel");
            }
            
            return contentResult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Common.Models
{
    public class Result
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public object Optional { get; set; }
        public string[] Errors { get; set; }
        public Result(bool succeed, IEnumerable<string> errors, string success, object optional, string statusCode)
        {
            Succeed = succeed;
            Errors = errors.ToArray();
            Message = success;
            Optional = optional;
            StatusCode = statusCode;
        }

        public static Result Success(string success = "Success", object optional = null, string statusCode = "")
        {
            return new Result(true, new string[] { }, success, optional, statusCode);
        }

        public static Result Failure(IEnumerable<string> errors, string message = "Failed", object optional = null, string statusCode = "")
        {
            return new Result(false, errors, message, optional, statusCode);
        }

        //public static Result Optionals(string optional = "")
        //{
        //    return new Result(true, new string[] {}, "", optional);
        //}
    }
}

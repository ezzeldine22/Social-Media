using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public ErrorType ErrorType { get; set; }

        public Result()
        {
           Errors = new List<string>(); 
        }
        public static Result success(string message = "Operation completed successfully")
        {
            return  new Result{ IsSuccess = true , Message = message};
        }

        public static Result Failure(List<string> Errors)
        {
            return new Result { IsSuccess = false , Errors = Errors};
        }
       
    }
}

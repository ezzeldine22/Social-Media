using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public class ResultT<T> : Result
    {
        public T Data {  get; set; }

        private ResultT() :base()
        {
                
        }
        public static ResultT<T> success(T data , string message = "Operation completed successfully")
        {
            return new ResultT<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
            };

        }
        public static ResultT<T> Failure(List<string> Error , ErrorType errorType = ErrorType.Validation)
        {
            return new ResultT<T>
            {
                IsSuccess = false,
                Errors = Error,
                ErrorType = errorType
            };
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KongZhiKa
{
    internal class ApiResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    
        public object Data { get; set; }

        public static ApiResult CreateSuccess()
        {
            return new ApiResult(){IsSuccess = true, Message = "成功"};
        }

        public static ApiResult CreateFail()
        { 
            return new ApiResult(){IsSuccess = false, Message = "失败"};
        }

        public static ApiResult CreateFailure(string message)
        {
            return new ApiResult(){IsSuccess = false, Message = message};
        }

    }
}

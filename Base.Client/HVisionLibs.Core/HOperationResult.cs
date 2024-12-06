using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core
{
    public class HOperateResult<T> : HOperateResult
    {
        public T Content { get; set; }

        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public HOperateResult(string message)
            : base(message)
        {
        }

        public HOperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message, T content)
            : base(isSuccess, errorCode, message)
        {
            Content = content;
        }
    }
    public class HOperateResult<T1, T2> : HOperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public HOperateResult(string message)
            : base(message)
        {
        }

        public HOperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
        }
    }
    public class HOperateResult<T1, T2, T3> : HOperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public HOperateResult(string message)
            : base(message)
        {
        }

        public HOperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
        }
    }
    public class HOperateResult<T1, T2, T3, T4> : HOperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public T4 Content4 { get; set; }

        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public HOperateResult(string message)
            : base(message)
        {
        }

        public HOperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
            Content4 = content4;
        }
    }
    public class HOperateResult<T1, T2, T3, T4, T5> : HOperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public T4 Content4 { get; set; }

        public T5 Content5 { get; set; }

        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public HOperateResult(string message)
            : base(message)
        {
        }

        public HOperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public HOperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4, T5 content5)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
            Content4 = content4;
            Content5 = content5;
        }
    }
    [Description("操作结果类")]
    public class HOperateResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = "UnKnownError";


        public int ErrorCode { get; set; } = 99999;


        public HOperateResult()
        {
        }

        public HOperateResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public HOperateResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public HOperateResult(bool isSuccess, int errorCode, string message)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            Message = message;
        }

        public HOperateResult(string message)
        {
            Message = message;
        }

        public HOperateResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public static HOperateResult CreateSuccessResult()
        {
            return new HOperateResult
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success"
            };
        }

        public static HOperateResult CreateFailResult(string message)
        {
            return new HOperateResult
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static HOperateResult CreateFailResult()
        {
            return new HOperateResult
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = "UnKnownError"
            };
        }

        public static HOperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new HOperateResult<T>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content = value
            };
        }

        public static HOperateResult<T> CreateFailResult<T>(HOperateResult result)
        {
            return new HOperateResult<T>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static HOperateResult<T> CreateFailResult<T>(string message)
        {
            return new HOperateResult<T>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static HOperateResult<T1, T2> CreateSuccessResult<T1, T2>(T1 value1, T2 value2)
        {
            return new HOperateResult<T1, T2>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2
            };
        }

        public static HOperateResult<T1, T2> CreateFailResult<T1, T2>(HOperateResult result)
        {
            return new HOperateResult<T1, T2>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static HOperateResult<T1, T2> CreateFailResult<T1, T2>(string message)
        {
            return new HOperateResult<T1, T2>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static HOperateResult<T1, T2, T3> CreateSuccessResult<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new HOperateResult<T1, T2, T3>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3
            };
        }

        public static HOperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(HOperateResult result)
        {
            return new HOperateResult<T1, T2, T3>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static HOperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(string message)
        {
            return new HOperateResult<T1, T2, T3>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static HOperateResult<T1, T2, T3, T4> CreateSuccessResult<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return new HOperateResult<T1, T2, T3, T4>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4
            };
        }

        public static HOperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(HOperateResult result)
        {
            return new HOperateResult<T1, T2, T3, T4>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static HOperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(string message)
        {
            return new HOperateResult<T1, T2, T3, T4>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static HOperateResult<T1, T2, T3, T4, T5> CreateSuccessResult<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            return new HOperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5
            };
        }

        public static HOperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(HOperateResult result)
        {
            return new HOperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static HOperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(string message)
        {
            return new HOperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }
    }
}

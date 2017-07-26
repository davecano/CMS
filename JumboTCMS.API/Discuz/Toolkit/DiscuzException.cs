using System;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.API.Discuz.Toolkit
{
    ///E:/swf/ <summary>
    ///E:/swf/ Discuz异常类
    ///E:/swf/ </summary>
    public class DiscuzException : Exception
    {
        private int error_code;
        private string error_message;

        ///E:/swf/ <summary>
        ///E:/swf/ 获取异常代码
        ///E:/swf/ </summary>
        public int ErrorCode
        {
            get { return error_code; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取异常描述
        ///E:/swf/ </summary>
        public string ErrorMessage
        {
            get { return error_message; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ Discuz错误异常
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="error_code">异常错误代码</param>
        ///E:/swf/ <param name="error_message">异常描述</param>
        public DiscuzException(int error_code, string error_message)
            : base(CreateMessage(error_code, error_message))
        {
            this.error_code = error_code;
            this.error_message = error_message;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 生成异常信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="error_code">异常错误代码</param>
        ///E:/swf/ <param name="error_message">异常描述</param>
        ///E:/swf/ <returns>异常信息</returns>
        private static string CreateMessage(int error_code, string error_message)
        {
            return string.Format("Code: {0}, Message: {1}", error_code, error_message);
        }
    }
}

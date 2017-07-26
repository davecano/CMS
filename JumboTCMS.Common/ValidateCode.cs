/*
 * 程序名称: JumboTCMS(将博内容管理系统通用版)
 * 
 * 程序版本: 7.x
 * 
 * 程序作者: 子木将博 (QQ：791104444@qq.com，仅限商业合作)
 * 
 * 版权申明: http://www.jumbotcms.net/about/copyright.html
 * 
 * 技术答疑: http://forum.jumbotcms.net/
 * 
 */

using System;
using System.Web;
namespace JumboTCMS.Common
{
    ///E:/swf/ <summary>
    ///E:/swf/ 验证码操作
    ///E:/swf/ </summary>
    public static class ValidateCode
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 判断验证码,如果判断正确则生成新的验证码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_code">不能是空值，否则为false</param>
        ///E:/swf/ <returns></returns>
        public static bool CheckValidateCode(string _code, ref string _realcode)
        {
            _realcode = GetValidateCode(4, false);//获取当前的验证码
            if (_code == null || _code.Length == 0)
                return false;
            if (_realcode.ToLower() == _code.ToLower())
            {
                //CreateValidateCode(4, true);
                return true;
            }
            return false;
        }
        ///E:/swf/ <summary>
        ///E:/swf/  获得验证码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_code">需要判断的值</param>
        ///E:/swf/ <param name="_init">是否初始化新的值</param>
        ///E:/swf/ <returns></returns>
        public static string GetValidateCode(int _length, bool _init)
        {
            if (_init)
                CreateValidateCode(_length, true);
            return JumboTCMS.Utils.Session.Get("ValidateCode");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建验证码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_length"></param>
        ///E:/swf/ <param name="_cover">是否覆盖老的值</param>
        public static void CreateValidateCode(int _length, bool _cover)
        {
            if (_cover)
                SaveCookie(_length);
            else
            {
                if (JumboTCMS.Utils.Session.Get("ValidateCode") == null)
                    SaveCookie(_length);
            }
        }
        public static void SaveCookie(int _length)
        {
            char[] chars = "3459ABCDEFGHJKLMNPRSTUVWXYZ".ToCharArray();
            Random random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < _length; i++)
                validateCode += chars[random.Next(0, chars.Length)].ToString();
            JumboTCMS.Utils.Session.Add("ValidateCode", validateCode);
        }
    }
}

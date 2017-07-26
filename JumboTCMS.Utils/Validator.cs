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
using System.Text.RegularExpressions;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 提供经常需要使用的一些验证逻辑。
    ///E:/swf/ </summary>
    public static class Validator
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 普通的域名
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsCommonDomain(string _value)
        {
            return Validator.QuickValidate("^(www.)?(\\w+\\.){1,3}(org|org.cn|gov.cn|com|cn|net|cc)$", _value.ToLower());
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">需验证的字符串。</param>
        ///E:/swf/ <returns>是否可以转化为日期的bool值。</returns>
        public static bool IsStringDate(string _value)
        {
            DateTime dTime;
            try
            {
                dTime = DateTime.Parse(_value);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">需验证的字符串。</param>
        ///E:/swf/ <returns>是否合法的bool值。</returns>
        public static bool IsNumeric(string _value)
        {
            return Validator.QuickValidate("^[-]?[1-9]*[0-9]*$", _value);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 检查一个字符串是否是纯字母和数字构成的，一般用于查询字符串参数的有效性验证。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">需验证的字符串。</param>
        ///E:/swf/ <returns>是否合法的bool值。</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return Validator.QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 判断是否是数字，包括小数和整数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">需验证的字符串。</param>
        ///E:/swf/ <returns>是否合法的bool值。</returns>
        public static bool IsNumber(string _value)
        {
            return Validator.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 快速验证一个字符串是否符合指定的正则表达式。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_express">正则表达式的内容。</param>
        ///E:/swf/ <param name="_value">需验证的字符串。</param>
        ///E:/swf/ <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            System.Text.RegularExpressions.Regex myRegex = new System.Text.RegularExpressions.Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为邮件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为邮编
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsZIPCode(string _value)
        {
            return Validator.QuickValidate("^([0-9]{6})$", _value);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为ID格式
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsIDCard(string _value)
        {
            Regex regex;
            string[] strArray;
            DateTime time;
            if ((_value.Length != 15) && (_value.Length != 0x12))
            {
                return false;
            }
            if (_value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$");
                if (!regex.Match(_value).Success)
                {
                    return false;
                }
                strArray = regex.Split(_value);
                try
                {
                    time = new DateTime(int.Parse("19" + strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(_value).Success)
            {
                return false;
            }
            strArray = regex.Split(_value);
            try
            {
                time = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为Int
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsInt(string _value)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(_value).Success)
            {
                if ((long.Parse(_value) > 0x7fffffffL) || (long.Parse(_value) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsLengthStr(string _value, int _begin, int _end)
        {
            int length = _value.Length;
            if ((length < _begin) && (length > _end))
            {
                return false;
            }
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断是不是纯中文
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsChinese(string _value)
        {
            Regex regex = new Regex(@"^[\u4E00-\u9FA5\uF900-\uFA2D]+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为手机号码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex(@"^(13|15)\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为电话号码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为网址
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsUrl(string _value)
        {
            Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为IP地址
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsIP(string _value)
        {
            Regex regex = new Regex(@"^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断一个字符串是否为字母加数字
        ///E:/swf/ Regex("[a-zA-Z0-9]?"
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 把字符串转成日期
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">字符串</param>
        ///E:/swf/ <param name="_defaultValue">默认值</param>
        ///E:/swf/ <returns></returns>
        public static DateTime StrToDate(string _value, DateTime _defaultValue)
        {
            if (IsStringDate(_value))
                return Convert.ToDateTime(_value);
            else
                return _defaultValue;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 日期比较
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="today">距离某个日期</param>
        ///E:/swf/ <param name="writeDate">输入日期</param>
        ///E:/swf/ <param name="n">比较天数</param>
        ///E:/swf/ <returns>大于天数返回true，小于返回false</returns>
        public static bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);
            WriteDate = WriteDate.AddDays(n);
            if (Today >= WriteDate)
                return false;
            else
                return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断日期是否过期
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="myDate">所要判断的日期</param>
        public static bool ValidDate(string myDate)
        {
            if (!IsStringDate(myDate))
                return true;
            return CompareDate(myDate, DateTime.Now.ToShortDateString(), 0);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 把字符串转成整型
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">字符串</param>
        ///E:/swf/ <param name="_defaultValue">默认值</param>
        ///E:/swf/ <returns></returns>
        public static int StrToInt(string _value, int _defaultValue)
        {
            if (IsNumeric(_value))
                return int.Parse(_value);
            else
                return _defaultValue;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否免费授权网站
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_defaultpage">首页地址</param>
        ///E:/swf/ <param name="_webname">网站名称</param>
        ///E:/swf/ <returns></returns>
        public static bool IsFreeSite(string _defaultpage, string _webname)
        {
            string _PageStr = Utils.HttpHelper.Get_Http(_defaultpage, 10000, System.Text.Encoding.UTF8);
            string _PageStr2 = _PageStr.ToLower().Replace("\"", "").Replace("'", "");
            return (_PageStr.Contains(_webname) && (_PageStr2.Contains("href=http://www.jumbotcms.net") || _PageStr2.Contains("href=http://jumbotcms.net")));
        }
    }
}

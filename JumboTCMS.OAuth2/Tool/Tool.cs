using System;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.OAuth2
{
    class Tool
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 截取参数,取不到值时返回""
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">不带?号的url参数</param>
        ///E:/swf/ <param name="para">要取的参数</param>
        public static string QueryString(string s, string para)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            s = s.Trim('?').Replace("%26", "&").Replace('?', '&');
            int num = s.Length;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = s[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string str = null;
                string str2 = null;
                if (num4 >= 0)
                {
                    str = s.Substring(startIndex, num4 - startIndex);
                    str2 = s.Substring(num4 + 1, (i - num4) - 1);
                    if (str == para)
                    {
                        return System.Web.HttpUtility.UrlDecode(str2);
                    }
                }
            }
            return "";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取配置文件的值。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="key"></param>
        ///E:/swf/ <returns></returns>
        public static string GetConfig(string key)
        {
            key = System.Web.Configuration.WebConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(key) ? string.Empty : key;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取Json string某节点的值。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="json"></param>
        ///E:/swf/ <param name="key"></param>
        ///E:/swf/ <returns></returns>
        public static string GetJosnValue(string json, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(json))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = json.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值

                    int end = json.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = json.IndexOf('}', index);
                    }
                    //index = json.IndexOf('"', index + key.Length + 1) + 1;
                    result = json.Substring(index, end - index);
                    //过滤引号或空格
                    result = result.Trim(new char[] { '"', ' ', '\'' });
                }
            }
            return result;
        }
    }
}

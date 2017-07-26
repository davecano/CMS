using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JumboTCMS.API.Discuz.Toolkit
{
    ///E:/swf/ <summary>
    ///E:/swf/ Discuz参数类
    ///E:/swf/ </summary>
    public class DiscuzParam : IComparable
    {
        private string name;
        private object value;

        ///E:/swf/ <summary>
        ///E:/swf/ 获取参数名称
        ///E:/swf/ </summary>
        public string Name
        {
            get { return name; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取参数值
        ///E:/swf/ </summary>
        public string Value
        {
            get
            {
                if (value is Array)
                    return ConvertArrayToString(value as Array);
                else
                    return value.ToString();
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取参数值
        ///E:/swf/ </summary>
        public string EncodedValue
        {
            get
            {
                if (value is Array)
                    return HttpUtility.UrlEncode(ConvertArrayToString(value as Array));
                else
                    return HttpUtility.UrlEncode(value.ToString());
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 构造参数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name">参数名称</param>
        ///E:/swf/ <param name="value">参数值</param>
        protected DiscuzParam(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 生成字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>返回字符串的名值对</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", Name, Value);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 生成encode字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string ToEncodedString()
        {
            return string.Format("{0}={1}", Name, EncodedValue);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 创建Discuz参数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name">参数名称</param>
        ///E:/swf/ <param name="value">参数值</param>
        ///E:/swf/ <returns>返回Discuz参数</returns>
        public static DiscuzParam Create(string name, object value)
        {
            return new DiscuzParam(name, value);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 比较参数是否相同
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="obj">要同当前参数比较的参数</param>
        ///E:/swf/ <returns>0相同,非0则不同</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is DiscuzParam))
                return -1;

            return this.name.CompareTo((obj as DiscuzParam).name);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 将Discuz参数数组转换为名值串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="a">Discuz参数数组</param>
        ///E:/swf/ <returns>转换的名值串,名值串之间用逗号分隔</returns>
        private static string ConvertArrayToString(Array a)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    builder.Append(",");

                builder.Append(a.GetValue(i).ToString());
            }

            return builder.ToString();
        }
    }
}

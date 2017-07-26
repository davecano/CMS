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
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 一些常用的字符串函数
    ///E:/swf/ </summary>
    public static class Strings
    {
        #region 普通加解密
        ///E:/swf/ <summary>
        ///E:/swf/ 倒序加1加密
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="rs"></param>
        ///E:/swf/ <returns></returns>
        public static string EncryptStr(string rs) //倒序加1加密 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] + 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 顺序减1解码 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="rs"></param>
        ///E:/swf/ <returns></returns>
        public static string DecryptStr(string rs) //顺序减1解码 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] - 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ Escape加密
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <returns></returns>
        public static string Escape(string str)
        {
            if (str == null)
                return String.Empty;
            StringBuilder sb = new StringBuilder();
            int len = str.Length;

            for (int i = 0; i < len; i++)
            {
                char c = str[i];

                //everything other than the optionally escaped chars _must_ be escaped 
                if (Char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '/' || c == '\\' || c == '.')
                    sb.Append(c);
                else
                    sb.Append(Uri.HexEscape(c));
            }

            return sb.ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ UnEscape解密
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <returns></returns>
        public static string UnEscape(string str)
        {
            if (str == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            int len = str.Length;
            int i = 0;
            while (i != len)
            {
                if (Uri.IsHexEncoding(str, i))
                    sb.Append(Uri.HexUnescape(str, ref i));
                else
                    sb.Append(str[i++]);
            }
            return sb.ToString();
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 左截取
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputString"></param>
        ///E:/swf/ <param name="len"></param>
        ///E:/swf/ <returns></returns>
        public static string Left(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(0, len);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 右截取
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputString"></param>
        ///E:/swf/ <param name="len"></param>
        ///E:/swf/ <returns></returns>
        public static string Right(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(inputString.Length - len, len);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 截取指定长度字符串,汉字为2个字符
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputString"></param>
        ///E:/swf/ <param name="len"></param>
        ///E:/swf/ <returns></returns>
        public static string CutString(string inputString, int len)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (tempLen >= len)
                    break;
            }
            return tempString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 去掉多余空格
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="original"></param>
        ///E:/swf/ <returns></returns>
        public static string RemoveSpaceStr(string original)
        {
            return System.Text.RegularExpressions.Regex.Replace(original, "\\s{2,}", " ");
        }
        public static string ToSummary(string Htmlstring)
        {
            string _content = NoHTML(Htmlstring);
            return RemoveSpaceStr(_content).Replace("[Jumbot_PageBreak]", " ");
        }
        #region 去除HTML标记
        ///E:/swf/<summary>   
        ///E:/swf/去除HTML标记   
        ///E:/swf/</summary>   
        ///E:/swf/<param name="NoHTML">包括HTML的源码</param>   
        ///E:/swf/<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //return Htmlstring;

            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "“", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "”", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring = Htmlstring.Replace("<", "&lt;");
            Htmlstring = Htmlstring.Replace(">", "&gt;");
            return Htmlstring;
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 不区分大小写的替换
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="original">原字符串</param>
        ///E:/swf/ <param name="pattern">需替换字符</param>
        ///E:/swf/ <param name="replacement">被替换内容</param>
        ///E:/swf/ <returns></returns>
        public static string ReplaceEx(string original, string pattern, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = original.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (original.Length / pattern.Length) * (replacement.Length - pattern.Length);
            char[] chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern, position0)) != -1)
            {
                for (int i = position0; i < position1; ++i) chars[count++] = original[i];
                for (int i = 0; i < replacement.Length; ++i) chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (int i = position0; i < original.Length; ++i) chars[count++] = original[i];
            return new string(chars, 0, count);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换html中的特殊字符
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString">需要进行替换的文本。</param>
        ///E:/swf/ <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("'", "&#39;");
            theString = theString.Replace("\r\n", "<br/> ");
            return theString;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 恢复html中的特殊字符
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString">需要恢复的文本。</param>
        ///E:/swf/ <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            theString = theString.Replace("&mdash;", "—");//2012-05-07新加的
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 转为货币格式
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value"></param>
        ///E:/swf/ <returns></returns>
        public static string ToMoney(double _value)
        {
            //return string.Format("{0:C2}", _value).Replace("¥", "").Replace("￥", "").Replace("$", "").Replace(",", "");
            return string.Format("{0:F2}", _value);
        }
        public static string ToMoney(string _value)
        {
            //return string.Format("{0:C2}", Convert.ToDouble(_value)).Replace("¥", "").Replace("￥", "").Replace("$", "").Replace(",", "");
            return string.Format("{0:F2}", Convert.ToDouble(_value));
        }
        public static string ToMoney(int _value)
        {
            //return string.Format("{0:C2}", Convert.ToDouble(_value)).Replace("¥", "").Replace("￥", "").Replace("$", "").Replace(",", "");
            return string.Format("{0:F2}", Convert.ToDouble(_value));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 转全角的函数(SBC case)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="input"></param>
        ///E:/swf/ <returns></returns>
        public static string ToSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 转半角的函数(DBC case)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="input"></param>
        ///E:/swf/ <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 输出单行简介
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string SimpleLineSummary(string theString)
        {
            theString = theString.Replace("&gt;", "");
            theString = theString.Replace("&lt;", "");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            theString = theString.Replace("\"", "");
            theString = theString.Replace("\t", " ");
            theString = theString.Replace("\r", " ");
            theString = theString.Replace("\n", " ");
            theString = Regex.Replace(theString, "\\s{2,}", " ");
            return theString;
        }
        ///E:/swf/ <summary> 
        ///E:/swf/ UBB代码处理函数 
        ///E:/swf/ </summary> 
        ///E:/swf/ <param name="content">输入字符串</param> 
        ///E:/swf/ <returns>输出字符串</returns> 
        public static string UBB2HTML(string content)  //ubb转html
        {
            content = Regex.Replace(content, @"\[b\](.+?)\[/b\]", "<b>$1</b>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[i\](.+?)\[/i\]", "<i>$1</i>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[u\](.+?)\[/u\]", "<u>$1</u>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[p\](.+?)\[/p\]", "<p>$1</p>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=left\](.+?)\[/align\]", "<align='left'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=center\](.+?)\[/align\]", "<align='center'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=right\](.+?)\[/align\]", "<align='right'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[url=(?<url>.+?)]\[/url]", "<a href='${url}' target=_blank>${url}</a>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[url=(?<url>.+?)](?<name>.+?)\[/url]", "<a href='${url}' target=_blank>${name}</a>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[quote](?<text>.+?)\[/quote]", "<div class=\"quote\">${text}</div>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[img](?<img>.+?)\[/img]", "<a href='${img}' target=_blank><img src='${img}' alt=''/></a>", RegexOptions.IgnoreCase);
            return content;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 将html转成js代码,不完全和原始数据一致
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="source"></param>
        ///E:/swf/ <returns></returns>
        public static string Html2Js(string source)
        {
            return String.Format("document.write(\"{0}\");",
                String.Join("\");\r\ndocument.write(\"", source.Replace("\\", "\\\\")
                                        .Replace("/", "\\/")
                                        .Replace("'", "\\'")
                                        .Replace("\"", "\\\"")
                                        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            ));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 将html转成可输出的js字符串,不完全和原始数据一致
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="source"></param>
        ///E:/swf/ <returns></returns>
        public static string Html2JsStr(string source)
        {
            return String.Format("{0}",
                String.Join(" ", source.Replace("\\", "\\\\")
                                        .Replace("/", "\\/")
                                        .Replace("'", "\\'")
                                        .Replace("\"", "\\\"")
                                        .Replace("\t", "")
                                        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            ));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 过滤所有特殊特号
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string FilterSymbol(string theString)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "(", ")", "{", "}", "%", "?", ",", ".", "=", "+", "-", "_", ";", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 过滤所有特殊特号，只允许逗号、分号和小数点
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string DelSymbol(string theString)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", "=", "-", "_", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 过滤一般特殊特号,主要用于过滤标题
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string SafetyTitle(string theString)
        {
            string[] aryReg = { "'", ";", "\"", "\r", "\n" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 过滤地址栏传参，允许=_-|%&?/\[]等符号，不允许'"<>;
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string SafetyQueryS(string theString)
        {
            string[] aryReg = { "'", ";", "\"", "\r", "\n", "<", ">" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到安全的sql关键词
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="theString"></param>
        ///E:/swf/ <returns></returns>
        public static string SafetyLikeValue(string theString)
        {
            string[] aryReg = { "'", ";", "\"", "\r", "\n", "%", "-", "[", "]", "(", ")" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 正则表达式取值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="HtmlCode">HTML代码</param>
        ///E:/swf/ <param name="RegexString">正则表达式</param>
        ///E:/swf/ <param name="GroupKey">正则表达式分组关键字</param>
        ///E:/swf/ <param name="RightToLeft">是否从右到左</param>
        ///E:/swf/ <returns></returns>
        public static string[] GetRegValue(string HtmlCode, string RegexString, string GroupKey, bool RightToLeft)
        {
            MatchCollection m;
            Regex r;
            if (RightToLeft == true)
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft);
            }
            else
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            m = r.Matches(HtmlCode);
            string[] MatchValue = new string[m.Count];
            for (int i = 0; i < m.Count; i++)
            {
                MatchValue[i] = m[i].Groups[GroupKey].Value;
            }
            return MatchValue;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得标签的属性值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="HtmlTag"></param>
        ///E:/swf/ <param name="AttributeName"></param>
        ///E:/swf/ <returns></returns>
        public static string AttributeValue(string HtmlTag, string AttributeName)
        {
            //前缀符号，要么为空，要么是空格/双引号/单引号/竖线/冒号……你还可以自己加入其他的符号
            string prefixCHAR = (HtmlTag.StartsWith(AttributeName + "=")) ? "(.{0})" : "([\"'\\s\\|:]{1})";
            string RegexString = prefixCHAR + AttributeName + "=(\"|')(?<" + AttributeName + ">.*?[^\\\\]{1})(\\2)";
            string[] _att = GetRegValue(HtmlTag, RegexString, AttributeName, false);
            if (_att.Length > 0)
                return _att[0].ToString();
            else
                return "";
        }
        ///E:/swf/ <summary>        
        ///E:/swf/ 格式化显示时间为几个月,几天前,几小时前,几分钟前,或几秒前        
        ///E:/swf/ </summary>        
        ///E:/swf/ <param name="dt">要格式化显示的时间</param>        
        ///E:/swf/ <returns>几个月,几天前,几小时前,几分钟前,或几秒前</returns>        
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60) { return dt.ToShortDateString(); }
            else if (span.TotalDays > 30) { return "1个月前"; }
            else if (span.TotalDays > 14) { return "2周前"; }
            else if (span.TotalDays > 7) { return "1周前"; }
            else if (span.TotalDays > 1) { return string.Format("{0}天前", (int)Math.Floor(span.TotalDays)); }
            else if (span.TotalHours > 1) { return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours)); }
            else if (span.TotalMinutes > 1) { return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes)); }
            else if (span.TotalSeconds >= 1) { return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds)); }
            else { return "1秒前"; }
        }
        #region 根据头、尾来截断字符串内容
        ///E:/swf/ <summary>
        ///E:/swf/ <para>获取截取内容数组:不包含头尾</para> 
        ///E:/swf/ <para>    sHtml(原文内容)</para> 
        ///E:/swf/ <para>    strStart(开头内容)</para> 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <returns></returns>
        public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd)
        {
            return getArray(sHtml, strStart, strEnd);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ <para>获取截取内容数组:自定义头尾</para> 
        ///E:/swf/ <para>    sHtml(原文内容)</para> 
        ///E:/swf/ <para>    strStart(开头内容)</para> 
        ///E:/swf/ <para>    strEnd(结束内容)</para> 
        ///E:/swf/ <para>    getStart(是否包含头内容)</para> 
        ///E:/swf/ <para>    getEnd(是否包含尾内容)</para> 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <param name="getStart">是否包含头内容</param>
        ///E:/swf/ <param name="getEnd">是否包含尾内容</param>
        ///E:/swf/ <returns></returns>
        public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
        {
            return getArray(sHtml, strStart, strEnd, getStart, getEnd);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ <para>获取截取内容字符串:不包含头尾</para> 
        ///E:/swf/ <para>    sHtml(原文内容)</para> 
        ///E:/swf/ <para>    strStart(开头内容)</para> 
        ///E:/swf/ <para>    strEnd(结束内容)</para> 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <returns></returns>
        public static string GetHtml(string sHtml, string strStart, string strEnd)
        {
            return getResult(sHtml, strStart, strEnd);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ <para>获取截取内容字符串:自定义头尾</para> 
        ///E:/swf/ <para>    sHtml(原文内容)</para> 
        ///E:/swf/ <para>    strStart(开头内容)</para> 
        ///E:/swf/ <para>    strEnd(结束内容)</para> 
        ///E:/swf/ <para>    getStart(是否包含头内容)</para> 
        ///E:/swf/ <para>    getEnd(是否包含尾内容)</para> 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <param name="getStart">是否包含头内容</param>
        ///E:/swf/ <param name="getEnd">是否包含尾内容</param>
        ///E:/swf/ <returns></returns>
        public static string GetHtml(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
        {
            return getResult(sHtml, strStart, strEnd, getStart, getEnd);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 先将一些特殊东西替换
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <returns></returns>
        private static string enReplaceStr(string str)
        {
            if ((str == null) || (str == ""))
            {
                return "superstring_空值";
            }
            return str.Replace("\r", "superstring_回车").Replace("\n", "superstring_换行").Replace("\"", "superstring_双引").Replace("\\", "superstring_反斜");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最后还原那些特殊的东西
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <returns></returns>
        private static string deReplaceStr(string str)
        {
            return str.Replace("superstring_回车", "\r").Replace("superstring_换行", "\n").Replace("superstring_双引", "\"").Replace("superstring_反斜", "\\").Replace("superstring_空值", "").Replace("superstring_空头", "").Replace("superstring_空尾", "");
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取截取内容数组:不包含头尾
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <returns></returns>
        private static ArrayList getArray(string sHtml, string strStart, string strEnd)
        {
            return getArray(sHtml, strStart, strEnd, false, false);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取截取内容数组:自定义头尾
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <param name="getStart">是否包含头内容</param>
        ///E:/swf/ <param name="getEnd">是否包含尾内容</param>
        ///E:/swf/ <returns></returns>
        private static ArrayList getArray(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
        {
            if ((strEnd == null) || (strEnd == ""))
            {
                sHtml = sHtml + "superstring_空尾";
                strEnd = "superstring_空尾";
            }
            if ((strStart == null) || (strStart == ""))
            {
                sHtml = "superstring_空头" + sHtml;
                strStart = "superstring_空头";
            }
            ArrayList list = new ArrayList();
            Regex re = new Regex(RegexStr(enReplaceStr(strStart), enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline);

            MatchCollection matchs = re.Matches(enReplaceStr(sHtml));
            for (int i = 0; i < matchs.Count; i++)
            {
                string matchStr = deReplaceStr(matchs[i].Value);
                if (getStart) matchStr = strStart + matchStr;
                if (getEnd) matchStr = matchStr + strEnd;
                list.Add(matchStr);
            }
            return list;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取截取内容:不包含头尾
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <returns></returns>
        private static string getResult(string sHtml, string strStart, string strEnd)
        {
            return getResult(sHtml, strStart, strEnd, false, false);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取截取内容:自定义头尾
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sHtml">原文内容</param>
        ///E:/swf/ <param name="strStart">开头内容</param>
        ///E:/swf/ <param name="strEnd">结束内容</param>
        ///E:/swf/ <param name="getStart">是否包含头内容</param>
        ///E:/swf/ <param name="getEnd">是否包含尾内容</param>
        ///E:/swf/ <returns></returns>
        private static string getResult(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
        {
            if ((strEnd == null) || (strEnd == ""))
            {
                sHtml = sHtml + "superstring_空尾";
                strEnd = "superstring_空尾";
            }
            if ((strStart == null) || (strStart == ""))
            {
                sHtml = "superstring_空头" + sHtml;
                strStart = "superstring_空头";
            }
            Regex re = new Regex(RegexStr(enReplaceStr(strStart), enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline);
            string matchStr = deReplaceStr(re.Match(enReplaceStr(sHtml)).Value);
            if (getStart) matchStr = strStart + matchStr;
            if (getEnd) matchStr = matchStr + strEnd;
            return matchStr;
        }
        public static string[] aryChar = { "\\", "^", "$", "{", "}", "[", "]", ".", "(", ")", "*", "+", "?", "!", "#", "|" };
        ///E:/swf/ <summary>
        ///E:/swf/ 根据头尾字符串获得正则规则
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strStart"></param>
        ///E:/swf/ <param name="strEnd"></param>
        ///E:/swf/ <returns></returns>
        private static string RegexStr(string strStart, string strEnd)
        {
            var str1 = strStart;
            var str2 = strEnd;
            for (int i = 0; i < aryChar.Length; i++)
            {
                str1 = str1.Replace(aryChar[i], "\\" + aryChar[i]);
                str2 = str2.Replace(aryChar[i], "\\" + aryChar[i]);
            }

            return "(?<=(" + str1 + "))[.\\s\\S]*?(?=(" + str2 + "))";
        }
        #endregion
    }
}

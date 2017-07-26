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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace JumboTCMS.TEngine.Parser
{
    public class ParseException : Exception
    {
        int line;
        int col;

        public ParseException(string msg, int line, int col)
            : base(msg)
        {
            this.line = line;
            this.col = col;

        }

        public int Col
        {
            get { return this.col; }
        }

        public int Line
        {
            get { return this.line; }
        }
    }
}

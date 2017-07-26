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
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 语言包实体（主要用以解析程序自动生成内容中的相关信息）
    ///E:/swf/ </summary>

    public class Language
    {
        public Language()
        { }

        private string _home;
        private string _more;
        ///E:/swf/ <summary>
        ///E:/swf/ 首页
        ///E:/swf/ </summary>
        public string Home
        {
            set { _home = value; }
            get { return _home; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更多
        ///E:/swf/ </summary>
        public string More
        {
            set { _more = value; }
            get { return _more; }
        }
    }
}


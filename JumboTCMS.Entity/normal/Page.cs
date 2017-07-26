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
    ///E:/swf/ 单页内容-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Page
    {
        public Normal_Page()
        { }

        private string _id;
        private string _title;
        private string _source;
        private string _outurl;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 标题
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 模板文件名
        ///E:/swf/ </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 静态文件路径
        ///E:/swf/ </summary>
        public string OutUrl
        {
            set { _outurl = value; }
            get { return _outurl; }
        }

    }
}


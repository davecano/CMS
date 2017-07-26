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
using System.Collections.Generic;
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 栏目树实体
    ///E:/swf/ </summary>
    public class Normal_ClassTree
    {
        public Normal_ClassTree()
        { }
        private string _id;
        private string _name = string.Empty;
        private string _link = string.Empty;
        private string _rssurl = string.Empty;
        private bool _haschild = false;
        private List<Normal_ClassTree> _subchild;
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目名称
        ///E:/swf/ </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目链接
        ///E:/swf/ </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ RSS地址
        ///E:/swf/ </summary>
        public string RssUrl
        {
            set { _rssurl = value; }
            get { return _rssurl; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public bool HasChild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        public List<Normal_ClassTree> SubChild
        {
            set { _subchild = value; }
            get { return _subchild; }
        }
    }
}


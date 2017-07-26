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
using System.Data;
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 专题-------表映射实体
    ///E:/swf/ </summary>
    public class Normal_Specials
    {
        public Normal_Specials()
        { }
        public List<Normal_Special> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Normal_Special>(_dt);
        }
    }
    public class Normal_Special
    {
        public Normal_Special()
        { }
        private string _id;
        private string _title;
        private string _info;
        private string _source;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 专题标题
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 专题简介
        ///E:/swf/ </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 专题文件名
        ///E:/swf/ </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }


    }
}


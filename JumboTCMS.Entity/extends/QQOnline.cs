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
    ///E:/swf/ QQ在线客服-------表映射实体
    ///E:/swf/ </summary>

    public class Extends_QQOnline
    {
        public Extends_QQOnline()
        { }
        public Extends_QQOnline(
            string id,
            string qqid,
            string title,
            string tcolor,
            string face
            )
        {
            this._id = id;
            this._qqid = qqid;
            this._title = title;
            this._tcolor = tcolor;
            this._face = face;
        }
        private string _id;
        private string _qqid;
        private string _title;
        private string _tcolor;
        private string _face;
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string QQID
        {
            set { _qqid = value; }
            get { return _qqid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string TColor
        {
            set { _tcolor = value; }
            get { return _tcolor; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Face
        {
            set { _face = value; }
            get { return _face; }
        }
    }
}


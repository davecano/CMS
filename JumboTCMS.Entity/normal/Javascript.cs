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
    ///E:/swf/ 外站调用-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Javascript
    {
        public Normal_Javascript()
        { }

        private string _id;
        private string _title;
        private string _code;
        private int _channelid;
        private int _classid;
        private int _selectnumber;
        private int _titlelen;
        private string _jumbotlicode;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int SelectNumber
        {
            set { _selectnumber = value; }
            get { return _selectnumber; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int TitleLen
        {
            set { _titlelen = value; }
            get { return _titlelen; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string JumbotLiCode
        {
            set { _jumbotlicode = value; }
            get { return _jumbotlicode; }
        }


    }
}


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
    ///E:/swf/ 会员短信-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserMessage
    {
        public Normal_UserMessage()
        { }

        private string _id;
        private string _title;
        private string _content;
        private string _sendip;
        private int _senduserid;
        private int _receiveuserid;
        private string _receiveusername;
        private DateTime _adddate;
        private int _state;
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
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SendIP
        {
            set { _sendip = value; }
            get { return _sendip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int SendUserId
        {
            set { _senduserid = value; }
            get { return _senduserid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ReceiveUserId
        {
            set { _receiveuserid = value; }
            get { return _receiveuserid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ReceiveUserName
        {
            set { _receiveusername = value; }
            get { return _receiveusername; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


    }
}


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
    ///E:/swf/ 发信日志-------表映射实体
    ///E:/swf/ </summary>

    public class Email_Sendlogs
    {
        public Email_Sendlogs()
        { }

        private string _id;
        private int _adminid;
        private string _sendtitle;
        private string _sendusers;
        private DateTime _sendtime;
        private string _sendip;
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
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发信标题
        ///E:/swf/ </summary>
        public string SendTitle
        {
            set { _sendtitle = value; }
            get { return _sendtitle; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发信收件人
        ///E:/swf/ </summary>
        public string SendUsers
        {
            set { _sendusers = value; }
            get { return _sendusers; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发信时间
        ///E:/swf/ </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发信IP
        ///E:/swf/ </summary>
        public string SendIP
        {
            set { _sendip = value; }
            get { return _sendip; }
        }


    }
}


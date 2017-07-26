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
    ///E:/swf/ 评论-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Review
    {
        public Normal_Review()
        { }

        private string _id = "0";
        private int _channelid;
        private int _parentid;
        private int _contentid;
        private DateTime _adddate;
        private string _content;
        private string _ip;
        private string _username;
        private int _ispass;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道ID
        ///E:/swf/ </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 父级ID
        ///E:/swf/ </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 内容ID
        ///E:/swf/ </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 日期
        ///E:/swf/ </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
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
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 状态:0表示未审;1表示审核
        ///E:/swf/ </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }


    }
}


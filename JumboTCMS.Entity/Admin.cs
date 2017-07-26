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
    ///E:/swf/ 管理员-------信息映射实体
    ///E:/swf/ </summary>

    public class Admin
    {
        public Admin()
        { }

        private string _id;
        private string _username;
        private int _adminid;
        private string _adminname;
        private string _adminpass;
        private string _adminsign;
        private string _adminsetting;
        private DateTime _lasttime2;
        private string _lastip2;
        private string _cookiess;
        private int _adminstate;

        ///E:/swf/ <summary>
        ///E:/swf/ 管理员对应的会员ID
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/  管理员对应的会员名称
        ///E:/swf/ </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员编号
        ///E:/swf/ </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员名称
        ///E:/swf/ </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员密码(32位密文)
        ///E:/swf/ </summary>
        public string AdminPass
        {
            set { _adminpass = value; }
            get { return _adminpass; }
        }
        public string AdminSign
        {
            set { _adminsign = value; }
            get { return _adminsign; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员权限值，比如:1-1,1-2
        ///E:/swf/ </summary>
        public string AdminSetting
        {
            set { _adminsetting = value; }
            get { return _adminsetting; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最后登录时间
        ///E:/swf/ </summary>
        public DateTime LastTime2
        {
            set { _lasttime2 = value; }
            get { return _lasttime2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最后登录IP
        ///E:/swf/ </summary>
        public string LastIP2
        {
            set { _lastip2 = value; }
            get { return _lastip2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ cookie匹配值，用于防止多次登录使用
        ///E:/swf/ </summary>
        public string Cookiess
        {
            set { _cookiess = value; }
            get { return _cookiess; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员状态
        ///E:/swf/ </summary>
        public int AdminState
        {
            set { _adminstate = value; }
            get { return _adminstate; }
        }
    }
}


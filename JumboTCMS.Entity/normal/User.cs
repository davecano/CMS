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
    ///E:/swf/ 会员-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_User
    {
        public Normal_User()
        { }

        private string _id = null;
        private string _username = "";
        private string _usersetting;
        private string _userpass;
        private string _nickname;
        private string _signature;
        private string _truename;
        private string _question;
        private string _answer;
        private int _sex;
        private string _email;
        private int _group;
        private int _state;
        private string _cookies;
        private DateTime _regtime;
        private string _regip;
        private DateTime _lasttime;
        private string _lastip;
        private string _homepage;
        private string _qq;
        private string _icq;
        private string _msn;
        private string _birthday;
        private string _provincecity;
        private int _login;
        private int _points;
        private int _idtype;
        private string _idcard;
        private string _workunit;
        private string _address;
        private string _zipcode;
        private string _telephone;
        private string _mobiletel;
        private int _isvip;
        private string _vipdate;
        private int _integral;
        private string _usersign;
        private int _adminid;
        private string _adminname;
        private string _adminpass;
        private string _adminsetting;
        private DateTime _lasttime2;
        private string _lastip2;
        private string _cookiess;
        private string _adminsign;
        private int _adminstate;
        private string _forumname;
        private string _forumpass;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 用户所在的组的权限
        ///E:/swf/ </summary>
        public string UserSetting
        {
            set { _usersetting = value; }
            get { return _usersetting; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 64位加密字符串
        ///E:/swf/ </summary>
        public string UserPass
        {
            set { _userpass = value; }
            get { return _userpass; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 昵称
        ///E:/swf/ </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 个性签名
        ///E:/swf/ </summary>
        public string Signature
        {
            set { _signature = value; }
            get { return _signature; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 真实姓名
        ///E:/swf/ </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Group
        {
            set { _group = value; }
            get { return _group; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Cookies
        {
            set { _cookies = value; }
            get { return _cookies; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string RegIp
        {
            set { _regip = value; }
            get { return _regip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime LastTime
        {
            set { _lasttime = value; }
            get { return _lasttime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string LastIP
        {
            set { _lastip = value; }
            get { return _lastip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string HomePage
        {
            set { _homepage = value; }
            get { return _homepage; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ICQ
        {
            set { _icq = value; }
            get { return _icq; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string BirthDay
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ProvinceCity
        {
            set { _provincecity = value; }
            get { return _provincecity; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Login
        {
            set { _login = value; }
            get { return _login; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int IDType
        {
            set { _idtype = value; }
            get { return _idtype; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string IDCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string WorkUnit
        {
            set { _workunit = value; }
            get { return _workunit; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ZipCode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string MobileTel
        {
            set { _mobiletel = value; }
            get { return _mobiletel; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int IsVIP
        {
            set { _isvip = value; }
            get { return _isvip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string VIPDate
        {
            set { _vipdate = value; }
            get { return _vipdate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Integral
        {
            set { _integral = value; }
            get { return _integral; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ User验证码，32位
        ///E:/swf/ </summary>
        public string UserSign
        {
            set { _usersign = value; }
            get { return _usersign; }
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
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string AdminPass
        {
            set { _adminpass = value; }
            get { return _adminpass; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string AdminSetting
        {
            set { _adminsetting = value; }
            get { return _adminsetting; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime LastTime2
        {
            set { _lasttime2 = value; }
            get { return _lasttime2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string LastIP2
        {
            set { _lastip2 = value; }
            get { return _lastip2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Cookiess
        {
            set { _cookiess = value; }
            get { return _cookiess; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员验证码，32位
        ///E:/swf/ </summary>
        public string AdminSign
        {
            set { _adminsign = value; }
            get { return _adminsign; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int AdminState
        {
            set { _adminstate = value; }
            get { return _adminstate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ForumName
        {
            set { _forumname = value; }
            get { return _forumname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ForumPass
        {
            set { _forumpass = value; }
            get { return _forumpass; }
        }


    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JumboTCMS.DAL.OAuthServer
{
    public class User
    {
        public User() { }

        public User(string _userid, string _email, string _nickname, string _headimgurl)
        {
            userid = _userid;
            email = _email;
            nickname = _nickname;
            headimgurl = _headimgurl;
            valid = true;
        }

        public bool valid = false;
        ///E:/swf/ <summary>
        ///E:/swf/ 用户id
        ///E:/swf/ </summary>
        public string userid { get; set; }
        ///E:/swf/ <summary>
        ///E:/swf/ 昵称
        ///E:/swf/ </summary>
        public string nickname { get; set; }
        ///E:/swf/ <summary>
        ///E:/swf/ 头像
        ///E:/swf/ </summary>
        public string headimgurl { get; set; }
        ///E:/swf/ <summary>
        ///E:/swf/ 邮箱
        ///E:/swf/ </summary>
        public string email { get; set; }
        public DateTime timestamp = DateTime.Now;
    }
}
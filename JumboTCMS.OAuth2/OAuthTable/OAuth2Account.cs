using System;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.OAuth2
{
    public class OAuth2Account:CYQ.Data.Orm.OrmBase
    {
        public OAuth2Account()
        {
            base.SetInit(this, "OAuth2Account", "Txt Path={0}_data");
        }
        private int _ID;

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        private string _OAuthServer;
        ///E:/swf/ <summary>
        ///E:/swf/ 授权的服务类型
        ///E:/swf/ </summary>
        public string OAuthServer
        {
            get
            {
                return _OAuthServer;
            }
            set
            {
                _OAuthServer = value;
            }
        }
        private string _Token;
        ///E:/swf/ <summary>
        ///E:/swf/ 保存的Token
        ///E:/swf/ </summary>
        public string Token
        {
            get
            {
                return _Token;
            }
            set
            {
                _Token = value;
            }
        }
        private string _OpenID;
        ///E:/swf/ <summary>
        ///E:/swf/ 保存对应的ID
        ///E:/swf/ </summary>
        public string OpenID
        {
            get
            {
                return _OpenID;
            }
            set
            {
                _OpenID = value;
            }
        }
        private string _BindAccount;
        
        private DateTime _ExpireTime;
        ///E:/swf/ <summary>
        ///E:/swf/ 过期时间
        ///E:/swf/ </summary>
        public DateTime ExpireTime
        {
            get
            {
                return _ExpireTime;
            }
            set
            {
                _ExpireTime = value;
            }
        }

        private string _NickName;
        ///E:/swf/ <summary>
        ///E:/swf/ 返回的第三方昵称
        ///E:/swf/ </summary>
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                _NickName = value;
            }
        }
        private string _HeadUrl;
        ///E:/swf/ <summary>
        ///E:/swf/ 返回的第三方账号对应的头像地址。
        ///E:/swf/ </summary>
        public string HeadUrl
        {
            get
            {
                return _HeadUrl;
            }
            set
            {
                _HeadUrl = value;
            }
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 绑定的账号
        ///E:/swf/ </summary>
        public string BindAccount
        {
            get
            {
                return _BindAccount;
            }
            set
            {
                _BindAccount = value;
            }
        }
    }
}

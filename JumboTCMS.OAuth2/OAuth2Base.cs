using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CYQ.Data.Table;
namespace JumboTCMS.OAuth2
{
    ///E:/swf/ <summary>
    ///E:/swf/ 授权基类
    ///E:/swf/ </summary>
    public abstract class OAuth2Base
    {
        protected WebClient wc = new WebClient();
        public OAuth2Base()
        {
            wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add("Pragma", "no-cache");
        }
        #region 基础属性
        ///E:/swf/ <summary>
        ///E:/swf/ 返回的开放ID。
        ///E:/swf/ </summary>
        public string openID = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 访问的Token
        ///E:/swf/ </summary>
        public string token = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 过期时间
        ///E:/swf/ </summary>
        public DateTime expiresTime;

        ///E:/swf/ <summary>
        ///E:/swf/ 第三方账号昵称
        ///E:/swf/ </summary>
        public string nickName = string.Empty;

        ///E:/swf/ <summary>
        ///E:/swf/ 第三方账号头像地址
        ///E:/swf/ </summary>
        public string headUrl = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 首次请求时返回的Code
        ///E:/swf/ </summary>
        internal string code = string.Empty;
        internal abstract OAuthServer server
        {
            get;
        }
        #endregion

        #region 非公开的请求路径和Logo图片地址。

        internal abstract string OAuthUrl
        {
            get;
        }
        internal abstract string TokenUrl
        {
            get;
        }
        #endregion

        #region WebConfig对应的配置【AppKey、AppSecret、CallbackUrl】
        internal string AppKey
        {
            get
            {
                return  JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/oauth2_" + server.ToString(), "AppKey");
            }
        }
        internal string AppSecret
        {
            get
            {
                return JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/oauth2_" + server.ToString(), "AppSecret");
            }
        }
        internal string CallbackUrl
        {
            get
            {
                return JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/oauth2_" + server.ToString(), "CallbackUrl");
            }
        }
        #endregion

        #region 基础方法

        ///E:/swf/ <summary>
        ///E:/swf/ 获得Token
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        protected string GetToken(string method,string oauthcode)
        {
            string result = string.Empty;
            try
            {
                        
                string para = "grant_type=authorization_code&client_id=" + AppKey + "&client_secret=" + AppSecret + "&code=" + code + "&state=" + server;
                if (oauthcode.ToLower() == "weixin")
                    para = "grant_type=authorization_code&appid=" + AppKey + "&secret=" + AppSecret + "&code=" + code + "&state=" + server;

                para += "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(CallbackUrl) + "&rnd=" + DateTime.Now.Second;
                if (method.ToUpper() == "POST")
                {
                    if (string.IsNullOrEmpty(wc.Headers["Content-Type"]))
                    {
                        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    }
                    result = wc.UploadString(TokenUrl, method, para);
                }
                else
                {
                    result = wc.DownloadString(TokenUrl + "?" + para);
                }
            }
            catch(Exception err)
            {
                CYQ.Data.Log.WriteLogToTxt(err);
            }
            return result;
        }
        public abstract string GetAuthorizeURL();
        ///E:/swf/ <summary>
        ///E:/swf/ 获取是否通过授权。
        ///E:/swf/ </summary>
        public abstract bool Authorize();
        ///E:/swf/ <param name="bindAccount">返回绑定的账号（若未绑定返回空）</param>
        public bool Authorize(out string bindAccount)
        {
            bindAccount = string.Empty;
            if (Authorize())
            {
                bindAccount = GetBindAccount();
                return true;
            }
            return false;
        }

        #endregion

        #region 关联绑定账号

        ///E:/swf/ <summary>
        ///E:/swf/ 读取已经绑定的账号
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string GetBindAccount()
        {
            string account = string.Empty;
            using (OAuth2Account oa = new OAuth2Account())
            {
                if (oa.Fill(string.Format("OAuthServer='{0}' and OpenID='{1}'", server, openID)))
                {
                    oa.Token = token;
                    oa.ExpireTime = expiresTime;
                    oa.NickName = nickName;
                    oa.HeadUrl = headUrl;
                    oa.Update();//更新token和过期时间
                    account = oa.BindAccount;
                }
            }
            return account;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加绑定账号
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="bindAccount"></param>
        ///E:/swf/ <returns></returns>
        public bool SetBindAccount(string bindAccount)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(openID) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(bindAccount))
            {
                using (OAuth2Account oa = new OAuth2Account())
                {
                    if (!oa.Exists(string.Format("OAuthServer='{0}' and OpenID='{1}'", server, openID)))
                    {
                        oa.OAuthServer = server.ToString();
                        oa.Token = token;
                        oa.OpenID = openID;
                        oa.ExpireTime = expiresTime;
                        oa.BindAccount = bindAccount;
                        oa.NickName = nickName;
                        oa.HeadUrl = headUrl;
                        result = oa.Insert(CYQ.Data.InsertOp.None);
                    }
                }
            }
            return result;
        }
        #endregion
    }
    ///E:/swf/ <summary>
    ///E:/swf/ 提供授权的服务商
    ///E:/swf/ </summary>
    public enum OAuthServer
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 新浪微博
        ///E:/swf/ </summary>
        Sina,
        ///E:/swf/ <summary>
        ///E:/swf/ 腾讯QQ
        ///E:/swf/ </summary>
        QQ,
        ///E:/swf/ <summary>
        ///E:/swf/ 淘宝网
        ///E:/swf/ </summary>
        TaoBao,
        ///E:/swf/ <summary>
        ///E:/swf/ 人人网
        ///E:/swf/ </summary>
        Renren,
        ///E:/swf/ <summary>
        ///E:/swf/ 微信
        ///E:/swf/ </summary>
        Weixin,
        ///E:/swf/ <summary>
        ///E:/swf/ 百度账号
        ///E:/swf/ </summary>
        Baidu,
    }
}

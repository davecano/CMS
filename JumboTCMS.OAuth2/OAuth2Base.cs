using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CYQ.Data.Table;
namespace JumboTCMS.OAuth2
{
    ///E:/swf/ <summary>
    ///E:/swf/ ��Ȩ����
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
        #region ��������
        ///E:/swf/ <summary>
        ///E:/swf/ ���صĿ���ID��
        ///E:/swf/ </summary>
        public string openID = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ ���ʵ�Token
        ///E:/swf/ </summary>
        public string token = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ ����ʱ��
        ///E:/swf/ </summary>
        public DateTime expiresTime;

        ///E:/swf/ <summary>
        ///E:/swf/ �������˺��ǳ�
        ///E:/swf/ </summary>
        public string nickName = string.Empty;

        ///E:/swf/ <summary>
        ///E:/swf/ �������˺�ͷ���ַ
        ///E:/swf/ </summary>
        public string headUrl = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ �״�����ʱ���ص�Code
        ///E:/swf/ </summary>
        internal string code = string.Empty;
        internal abstract OAuthServer server
        {
            get;
        }
        #endregion

        #region �ǹ���������·����LogoͼƬ��ַ��

        internal abstract string OAuthUrl
        {
            get;
        }
        internal abstract string TokenUrl
        {
            get;
        }
        #endregion

        #region WebConfig��Ӧ�����á�AppKey��AppSecret��CallbackUrl��
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

        #region ��������

        ///E:/swf/ <summary>
        ///E:/swf/ ���Token
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
        ///E:/swf/ ��ȡ�Ƿ�ͨ����Ȩ��
        ///E:/swf/ </summary>
        public abstract bool Authorize();
        ///E:/swf/ <param name="bindAccount">���ذ󶨵��˺ţ���δ�󶨷��ؿգ�</param>
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

        #region �������˺�

        ///E:/swf/ <summary>
        ///E:/swf/ ��ȡ�Ѿ��󶨵��˺�
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
                    oa.Update();//����token�͹���ʱ��
                    account = oa.BindAccount;
                }
            }
            return account;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ ��Ӱ��˺�
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
    ///E:/swf/ �ṩ��Ȩ�ķ�����
    ///E:/swf/ </summary>
    public enum OAuthServer
    {
        ///E:/swf/ <summary>
        ///E:/swf/ ����΢��
        ///E:/swf/ </summary>
        Sina,
        ///E:/swf/ <summary>
        ///E:/swf/ ��ѶQQ
        ///E:/swf/ </summary>
        QQ,
        ///E:/swf/ <summary>
        ///E:/swf/ �Ա���
        ///E:/swf/ </summary>
        TaoBao,
        ///E:/swf/ <summary>
        ///E:/swf/ ������
        ///E:/swf/ </summary>
        Renren,
        ///E:/swf/ <summary>
        ///E:/swf/ ΢��
        ///E:/swf/ </summary>
        Weixin,
        ///E:/swf/ <summary>
        ///E:/swf/ �ٶ��˺�
        ///E:/swf/ </summary>
        Baidu,
    }
}

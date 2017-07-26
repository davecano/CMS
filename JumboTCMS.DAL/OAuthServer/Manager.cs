using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;

using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL.OAuthServer
{
    public class Manager : Common
    {
        public Manager()
        {
            base.SetupSystemDate();
        }
        private static int CODE_EXPIRATION = 60; // seconds
        private static int TOKEN_EXPIRATION = 86400; // seconds
        private static string CODE_PREPEND = "code_";
        private static string TOKEN_PREPEND = "token_";
        private static string REFRESH_PREPEND = "refresh_";

        //PUBLIC

        public static void Logout(HttpRequest request, HttpResponse response)
        {
            HttpContext.Current.Session.RemoveAll();
            string urlRedirect = "authorize.aspx?"+request.ServerVariables["Query_String"].ToString();
            response.Redirect(urlRedirect);
        }
        public static void Authorize(HttpRequest request, HttpResponse response)
        {
            string client_id = request["client_id"];
            string response_type = request["response_type"];
            string redirect_uri = request["redirect_uri"];
            string state = request["state"];
            string scope = request["scope"];
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            if (HttpContext.Current.Session["client_id"] == null)
            {
                Cleaning();

                if (response_type != "code")
                {
                    EchoError(response, redirect_uri, "unsupported_response_type", "Only code response_type is supported", state);
                    return;
                }
                if (!VerifyClientId(response, client_id, redirect_uri, state))
                {
                    return;
                }
                HttpContext.Current.Session.Add("client_id", client_id);
                HttpContext.Current.Session.Add("response_type", response_type);
                HttpContext.Current.Session.Add("redirect_uri", redirect_uri);
                HttpContext.Current.Session.Add("state", state);
                HttpContext.Current.Session.Add("scope", scope);
                return;
            }
            /*
            string urlRedirect = session["redirect_uri"].ToString().Contains("?") ? "{0}&code={1}" : "{0}?code={1}";
            urlRedirect = string.Format(urlRedirect, session["redirect_uri"], session["client_code"]);
            if (state != null && state != "")
            {
                urlRedirect += "&state=" + state;
            }
            response.Redirect(urlRedirect);

            */


        }

        public static void Authenticate(HttpRequest request, HttpResponse response)
        {
            if (HttpContext.Current.Session["client_id"] == null)
            {
                response.Write("Session Expired.");
                return;
            }
            string client_id = HttpContext.Current.Session["client_id"].ToString();
            string code = "";
            if (HttpContext.Current.Session["client_email"] == null)
            {
                string email = request["email"];
                string userpass = request["userpass"];
                if (userpass.Length != 32)
                {
                    response.Write("密码应该是加密的32位MD5");
                    return;
                }


                User user1 = VerifyAuthentication(response, email, userpass);
                if (!user1.valid)
                {
                    response.Write("账号或密码错误");
                    return;
                }
                HttpContext.Current.Session.Add("client_email", user1.email);
                HttpContext.Current.Session.Add("client_nickname", user1.nickname);

                code = GetRandomCode(new Random());
                HttpContext.Current.Session.Add("client_code", code);
                HttpContext.Current.Application.Add(CODE_PREPEND + code, user1);
            }
            else
            {
                code = HttpContext.Current.Session["client_code"].ToString();
            }
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            string urlRedirect = session["redirect_uri"].ToString().Contains("?") ? "{0}&code={1}" : "{0}?code={1}";
            urlRedirect = string.Format(urlRedirect, session["redirect_uri"], code);
            string state = session["state"] as string;
            if (state != null && state != "")
            {
                urlRedirect += "&state=" + state;
            }

            response.Redirect(urlRedirect);
        }
        #region 获得Token
        public static void GetToken(HttpRequest request, HttpResponse response)
        {
            Cleaning();
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            string grant_type = request["grant_type"];
            string client_id = request["client_id"];
            string client_secret = request["client_secret"];
            string code = request["code"];
            string redirect_uri = request["redirect_uri"];
            if (grant_type == "authorization_code")
            {
                if (!VerifyClientIdAndSecret(response, client_id, client_secret, redirect_uri))
                {
                    EchoError(response, redirect_uri, "invalid_grant", "Not valid code.", null);
                    return;
                }
                if (HttpContext.Current.Application[CODE_PREPEND + code] == null)
                {
                    EchoError(response, redirect_uri, "invalid_grant", "Not valid code.", null);
                    return;
                }
                User userData = (User)HttpContext.Current.Application[CODE_PREPEND + code];
                if (DateTime.Now.Subtract(userData.timestamp).TotalSeconds > CODE_EXPIRATION)
                {
                    EchoError(response, redirect_uri, "invalid_grant", "Code is expired. Max duration is " + CODE_EXPIRATION + " sec.", null);
                }
                Random rdm = new Random();
                Token t = new Token(GetRandomCode(rdm), GetRandomCode(rdm), TOKEN_EXPIRATION, userData.userid);
                HttpContext.Current.Application.Add(TOKEN_PREPEND + t.access_token, userData);
                HttpContext.Current.Application.Add(REFRESH_PREPEND + t.refresh_token, t);
                response.ContentType = "application/json";
                response.AddHeader("Cache-Control", "no-store");
                response.Write(Newtonsoft.Json.Linq.JObject.FromObject(t).ToString());
            }
            else if (grant_type == "refresh_token")
            {
                if (!VerifyClientIdAndSecret(response, client_id, client_secret, redirect_uri))
                {
                    return;
                }
                string refresh = request["refresh_token"];
                Token t = (Token)HttpContext.Current.Application[REFRESH_PREPEND + refresh];
                User userData = (User)HttpContext.Current.Application[TOKEN_PREPEND + t.access_token];
                // delete old
                HttpContext.Current.Application.Remove(TOKEN_PREPEND + t.access_token);
                HttpContext.Current.Application.Remove(REFRESH_PREPEND + t.refresh_token);
                //save new
                Random rdm = new Random();
                t = new Token(GetRandomCode(rdm), GetRandomCode(rdm), TOKEN_EXPIRATION, t.userid);
                HttpContext.Current.Application.Add(TOKEN_PREPEND + t.access_token, userData);
                HttpContext.Current.Application.Add(REFRESH_PREPEND + t.refresh_token, t);

                response.ContentType = "application/json";
                response.CacheControl = "no-store";
                response.Write(Newtonsoft.Json.Linq.JObject.FromObject(t).ToString());
            }
            else
            {
                EchoError(response, session["redirect_uri"] as string, "invalid_grant", "The provided access grant is invalid", session["state"] as string);
            }
        }
        #endregion
        #region 获得用户信息
        public static void UserInfo(HttpRequest request, HttpResponse response)
        {
            Cleaning();
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            string oauth_token = request["access_token"];
            string userid = request["userid"];
            if (HttpContext.Current.Application[TOKEN_PREPEND + oauth_token] == null) //************* or expired
            {
                EchoError(response, session["redirect_uri"] as string, "invalid_token", "Invalid", session["state"] as string);
                return;
            }
            User userData = (User)HttpContext.Current.Application[TOKEN_PREPEND + oauth_token];
            if (userData.userid !=userid)
            {
                EchoError(response, session["redirect_uri"] as string, "invalid_userid", "Invalid userid", session["state"] as string);
                return;
            }
            if ((DateTime.Now - userData.timestamp).TotalSeconds > TOKEN_EXPIRATION)
            {
                EchoError(response, session["redirect_uri"] as string, "expired_token", "Token is expired.", session["state"] as string);
                return;
            }
            response.Write(Newtonsoft.Json.Linq.JObject.FromObject(userData).ToString());
        }
        #endregion
        #region 输出错误

        private static void EchoError(HttpResponse response, string redirect_uri, string error, string error_description, string state)
        {
            string urlRedirect = "{0}?error={1}&error_description={2}";
            if (state != null && state != "")
            {
                urlRedirect += "&state=" + state;
            }
            urlRedirect = string.Format(urlRedirect, redirect_uri, error, error_description, state);
            response.Redirect(urlRedirect);
        }
        #endregion
        #region 验证app的信息是否有效
        /// <summary>
        /// 验证app的信息是否有效
        /// </summary>
        /// <param name="response"></param>
        /// <param name="client_id"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private static bool VerifyClientId(HttpResponse response, string client_id, string redirect_uri, string state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "AppKey=@appkey and state=1";
                _doh.AddConditionParameter("@appkey", client_id);
                object[] _value = _doh.GetFields("jcms_oauth2_app", "CallbackURI,SiteName,SiteUrl");
                if (_value == null)
                {
                    EchoError(response, redirect_uri, "invalid_client", "Invalid client_id: " + client_id + ".", state);
                    return false;
                }
                string RedirectURI = _value[0].ToString();
                string SiteName = _value[1].ToString();
                string SiteUrl = _value[2].ToString();
                if (!redirect_uri.StartsWith(RedirectURI))
                {
                    EchoError(response, redirect_uri, "redirect_uri_mismatch", "Invalid redirect_uri.", state);
                    return false;
                }
                HttpContext.Current.Session.Add("client_sitename", SiteName);
                HttpContext.Current.Session.Add("client_siteurl", SiteUrl);
                return true;
            }
        }

        private static bool VerifyClientIdAndSecret(HttpResponse response, string client_id, string client_secret, string redirect_uri)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "AppKey=@appkey and state=1";
                _doh.AddConditionParameter("@appkey", client_id);
                object[] _value= _doh.GetFields("jcms_oauth2_app", "AppSecret,CallbackURI");
                if (_value==null)
                {
                    EchoError(response, redirect_uri, "invalid_client", "Invalid client_id: " + client_id + ".", null);
                    return false;
                }
                string AppSecret = _value[0].ToString();
                string RedirectURI = _value[1].ToString();
                if (!redirect_uri.StartsWith(RedirectURI))
                {
                    EchoError(response, redirect_uri, "redirect_uri_mismatch", "Invalid redirect_uri.", null);
                    return false;
                }
                if (AppSecret != client_secret)
                {
                    EchoError(response, redirect_uri, "unauthorized_client", "Bad client_secret.", null);
                    return false;
                }
                return true;
            }
        }
        #endregion
        #region 验证用户输入的信息是否合法
        /// <summary>
        /// 验证用户输入的信息是否合法
        /// </summary>
        /// <param name="response"></param>
        /// <param name="email"></param>
        /// <param name="userpass"></param>
        /// <returns></returns>
        private static User VerifyAuthentication(HttpResponse response, string email, string userpass)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                userpass = JumboTCMS.Utils.MD5.Last64(userpass);
                _doh.Reset();
                _doh.ConditionExpress = "email=@email and userpass=@userpass and state=1";
                _doh.AddConditionParameter("@email", email);
                _doh.AddConditionParameter("@userpass", userpass);
                if (!_doh.Exist("jcms_normal_user"))
                    return new User();

                _doh.Reset();
                _doh.ConditionExpress = "email=@email and userpass=@userpass and state=1";
                _doh.AddConditionParameter("@email", email);
                _doh.AddConditionParameter("@userpass", userpass);
                object[] _value = _doh.GetFields("jcms_normal_user", "id,nickname");
                string userid = _value[0].ToString();
                string nickname = _value[1].ToString();
                return new User(userid, email, nickname,"");
            }

        }
        #endregion
        private static string ComputeHash(string val)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(val);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return System.Convert.ToBase64String(tmpHash);
        }
        #region 获得随机数
        private static string GetRandomCode(Random rdm)
        {
            string rndStr = "";
            for (int i = 0; i < 20; i++)
            {
                string rndTmp = rdm.Next(9).ToString();
                rndStr = rndStr + rndTmp;
            }
            return rndStr;
        }
        #endregion
        #region  清除app信息
        private static void Cleaning()
        {
            HttpApplicationState app = HttpContext.Current.Application;
            object dt = app["oauth_cleaning"];
            if (dt == null || (DateTime.Now - (DateTime)dt).TotalSeconds > 60)
            {
                app["oauth_cleaning"] = DateTime.Now;
                //cleaning

                foreach (string key in app.AllKeys)
                {
                    if (key.StartsWith(CODE_PREPEND))
                    {
                        if ((DateTime.Now - ((User)app[key]).timestamp).TotalSeconds > CODE_EXPIRATION)
                        {
                            app.Remove(key);
                        }
                    }
                    else if (key.StartsWith(TOKEN_PREPEND))
                    {
                        if ((DateTime.Now - ((User)app[key]).timestamp).TotalSeconds > TOKEN_EXPIRATION)
                        {
                            app.Remove(key);
                        }
                    }
                    else if (key.StartsWith(REFRESH_PREPEND))
                    {
                        if (app[TOKEN_PREPEND + ((Token)app[key]).access_token] == null)
                        {
                            app.Remove(key);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
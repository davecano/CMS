using System;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.OAuth2
{
    public class WeixinOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.Weixin;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "https://open.weixin.qq.com/connect/qrconnect?appid={0}&scope=snsapi_login&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "https://api.weixin.qq.com/sns/oauth2/access_token";
            }
        }

        internal string UserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}";

        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "Weixin");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("GET", "Weixin");//一次性返回数据，QQ仅返回Token，还需要一次请求获取OpenID。//access_token=A5E175586196173434374BD3DBBAA5E8A3&expires_in=7776000
                //分解result;
                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        token = Tool.GetJosnValue(result, "access_token");
                        if (!string.IsNullOrEmpty(token))
                        {
                            double d = 0;
                            if (double.TryParse(Tool.GetJosnValue(result, "expires_in"), out d))
                            {
                                expiresTime = DateTime.Now.AddSeconds(d);
                            }
                            openID = Tool.GetJosnValue(result, "openid");
                            if (!string.IsNullOrEmpty(openID))
                            {
                                //读取QQ账号和头像
                                result = wc.DownloadString(string.Format(UserInfoUrl, token, openID));
                                if (!string.IsNullOrEmpty(result)) //返回：callback( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} ); 
                                {
                                    nickName = Tool.GetJosnValue(result, "nickname");
                                    headUrl = Tool.GetJosnValue(result, "headimgurl");
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            CYQ.Data.Log.WriteLogToTxt("WeixinOAuth.Authorize():" + result);
                        }
                    }
                    catch (Exception err)
                    {
                        CYQ.Data.Log.WriteLogToTxt(err);
                    }
                }
            }
            return false;
        }
    }
}

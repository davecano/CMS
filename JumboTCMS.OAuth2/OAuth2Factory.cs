using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace JumboTCMS.OAuth2
{
    ///E:/swf/ <summary>
    ///E:/swf/ 授权工厂类
    ///E:/swf/ </summary>
    public class OAuth2Factory
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 获取当前的授权类型。
        ///E:/swf/ </summary>
        public static OAuth2Base Current
        {
            get
            {
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                {
                    string url = System.Web.HttpContext.Current.Request.Url.Query;
                    if (url.IndexOf("state=") > -1)
                    {
                        string code = Tool.QueryString(url, "code");
                        string state = Tool.QueryString(url, "state");
                        if (ServerList.ContainsKey(state))
                        {
                            OAuth2Base ob = ServerList[state];
                            ob.code = code;
                            System.Web.HttpContext.Current.Session["OAuth2"] = ob;//对象存进Session，后期授权后会增加引用。
                            return ob;
                        }
                    }
                }
                return null;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取或设置当前Session存档的授权类型。 (注销用户时可以将此值置为Null)
        ///E:/swf/ </summary>
        public static OAuth2Base SessionOAuth
        {
            get
            {
                if (System.Web.HttpContext.Current.Session != null)
                {
                    object o = System.Web.HttpContext.Current.Session["OAuth2"];
                    if (o != null)
                    {
                        return o as OAuth2Base;
                    }
                }
                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["OAuth2"] = value;
            }
        }
        static Dictionary<string, OAuth2Base> _ServerList;
        ///E:/swf/ <summary>
        ///E:/swf/ 获取所有的类型（新开发的OAuth2需要到这里注册添加一下）
        ///E:/swf/ </summary>
        internal static Dictionary<string, OAuth2Base> ServerList
        {
            get
            {
                if (_ServerList == null)
                {
                    _ServerList = new Dictionary<string, OAuth2Base>(StringComparer.OrdinalIgnoreCase);
                    _ServerList.Add(OAuthServer.Sina.ToString(), new SinaOAuth());//新浪微博
                    _ServerList.Add(OAuthServer.QQ.ToString(), new QQOAuth());//QQ微博
                    _ServerList.Add(OAuthServer.TaoBao.ToString(), new TaobaoOAuth());//淘宝
                    _ServerList.Add(OAuthServer.Renren.ToString(), new RenrenOAuth());//人人网
                    _ServerList.Add(OAuthServer.Weixin.ToString(), new WeixinOAuth());//微信
                    _ServerList.Add(OAuthServer.Baidu.ToString(), new BaiduOAuth());//百度账号
                }
                return _ServerList;
            }
        }
    }
}

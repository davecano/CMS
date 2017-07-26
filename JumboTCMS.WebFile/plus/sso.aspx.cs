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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _sso : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _username = q("username");
            string _userpass = q("userpass");
            int _type = Str2Int(q("type"), 7);
            string _enc = q("enc");
            string _backurl = q("backurl") == "" ? site.Dir : q("backurl");
            string _statickey = site.StaticKey;
            string _time = System.DateTime.Now.ToString("yyyy-MM-ddHH");
            int iExpires = 0;
            if (_type > 0)
                iExpires = _type;//保存天数
            if (_enc == JumboTCMS.Utils.MD5.Upper32(_username + _userpass + _statickey + _time))
            {//表示请求合法
                string _info = new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_username, _userpass, iExpires);
                if (_info == "ok")
                    Response.Redirect(_backurl);
                else
                    Response.Write(_info);
            }
            else
                Response.Write("非法请求");
        }
    }
}

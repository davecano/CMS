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
using System.Collections.Generic;
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Passport
{
    public partial class _register_third : JumboTCMS.UI.FrontPassport
    {
        public string OAuth_Code = "";
        public string _Email = "";
        public string _UserName = "";
        public string _Sex = "1";
        public string _Birthday = "1980-01-01";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!site.AllowReg || (site.AllowReg && site.CheckReg))
            if (!site.AllowReg)
            {
                FinalMessage("对不起，本站不允许使用第三方登录!", site.Home, 0);
                Response.End();
            }
            OAuth_Code = q("code");
            CheckOAuthState(OAuth_Code);
            string oauth_info = JumboTCMS.Utils.Cookie.GetValue("oauthinfo_" + OAuth_Code);
            if (oauth_info == null || oauth_info == "")
            {
                FinalMessage("接口会话已失效，请重新登录第三方网站", site.Dir + "passport/login.aspx", 0);
                Response.End();
            }
            Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(oauth_info);
            string OAuth_Token = (string)newobj["token"];
            _Email = (string)newobj["email"];
            _UserName = (string)newobj["username"];
            _Birthday = (string)newobj["birthday"];
            if (!JumboTCMS.Utils.Validator.IsEmail(_Email))
                _Email = GetRandomNumberString(12) + "@domain.com";
            if (!JumboTCMS.Utils.Validator.IsStringDate(_Birthday))
                _Birthday = "1980-1-1";
            if (OAuth_Code != "" && OAuth_Token != "")
            {
                doh.Reset();
                doh.ConditionExpress = "[Token_" + OAuth_Code + "]=@oauthtoken";
                doh.AddConditionParameter("@oauthtoken", OAuth_Token);
                string _userid = Str2Str(doh.GetField("jcms_normal_user", "id").ToString());
                if (_userid == "0")//需要注册一个
                {
                    string _username = OAuth_Code + "_" + GetRandomNumberString(5, true);
                    string _password = GetRandomNumberString(12);
                    _userid = (new JumboTCMS.DAL.Normal_UserDAL().Register(_username, _username, JumboTCMS.Utils.MD5.Lower32(_password), 0, _Email, _Birthday, "", "", "", OAuth_Code, OAuth_Token, false)).ToString();
                }
                if (_userid != "0")
                {
                    //注册成功
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", _userid);
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_user");
                    JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false, "");
                    new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_User.UserName, _User.UserPass, 1);
                    Response.Redirect(site.Home);
                }
            }
        }
    }
}

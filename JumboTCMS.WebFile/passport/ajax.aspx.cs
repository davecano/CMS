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
using System.Web;
using System.Data;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Passport
{
    public partial class _ajax : JumboTCMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }

            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxLogin":
                    ajaxLogin();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "checkemail":
                    ajaxCheckEmail();
                    break;
                case "ajaxRegister":
                    ajaxRegister();
                    break;
                case "ajaxResetPassword":
                    ajaxResetPassword();
                    break;
                case "ajaxSendMailAgain":
                    ajaxSendMailAgain();
                    break;
                case "ajaxOAuthNewUser":
                    ajaxOAuthNewUser();
                    break;
                case "ajaxOAuthBindUser":
                    ajaxOAuthBindUser();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            User_Load("", "json");
        }
        private void ajaxLogin()
        {
            string _name = f("name");
            string _pass = f("pass");
            string _code = f("code");
            string _realcode = "";
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                this._response = "验证码应该是" + _realcode;
                return;
            }
            int _type = Str2Int(f("type"), 0);
            int iExpires = 0;
            if (_type > 0)
                iExpires = _type;//保存天数
            this._response = new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_name, _pass, iExpires);
        }
        private void ajaxCheckName()
        {
            if (q("txtUserName") != "")
            {
                doh.Reset();
                doh.ConditionExpress = "username=@username and id<>" + Str2Str(q("id"));
                doh.AddConditionParameter("@username", q("txtUserName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "已经存在");
                else
                    this._response = JsonResult(1, "可以注册");
            }
            else
                this._response = JsonResult(0, "为空");
        }
        private void ajaxCheckEmail()
        {
            if (q("txtEmail") != "")
            {
                doh.Reset();
                doh.ConditionExpress = "email=@email and id<>" + Str2Str(q("id"));
                doh.AddConditionParameter("@email", q("txtEmail"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "邮箱已被占用");
                else
                    this._response = JsonResult(1, "可以注册");
            }
            else
                this._response = JsonResult(0, "为空");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 提交注册信息
        ///E:/swf/ </summary>
        private void ajaxRegister()
        {
            string _code = f("txtCode");
            string _realcode = "";
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                Response.Write("JumboTCMS.Alert('验证码错误', '0');");
                Response.End();
            }
            if (f("txtUserName").Length > 0 && f("txtPass1").Length > 0 && f("txtEmail").Length > 0)
            {
                string usersign = GetRandomNumberString(64, false);
                if (new JumboTCMS.DAL.Normal_UserDAL().Register(f("txtUserName"), "", f("txtPass1"), Str2Int(f("rblSex")), f("txtEmail"), f("txtBirthday"), usersign, "", "", "", "", false) > 0)
                {
                    if (site.CheckReg)//说明需要邮件激活
                    {
                        string _body = f("txtUserName") + ", 您好！<br>" +
                            "感谢您注册" + site.Name + "，点击下面的链接即可完成注册：<br>" +
                            "<a href=\"" + site.Url + site.Dir + "passport/active.aspx?username=" + Server.UrlEncode(f("txtUserName")) + "&amp;email=" + f("txtEmail") + "&amp;usersign=" + usersign + "\" target=\"_blank\">" +
                            site.Url + site.Dir + "passport/active.aspx?username=" + Server.UrlEncode(f("txtUserName")) + "&amp;email=" + f("txtEmail") + "&amp;usersign=" + usersign + "</a><br>" +
                            "（如果链接无法点击，请将它拷贝到浏览器的地址栏中）";
                        if (new JumboTCMS.DAL.Normal_UserMailDAL().SendMail(f("txtEmail"), site.Name + "注册激活邮件", _body))
                        {
                            Session["jcms_user_register"] = "1";
                            this._response = "location='register_step2.aspx?username=" + Server.UrlEncode(f("txtUserName")) + "&amp;email=" + f("txtEmail") + "&amp;usersign=" + usersign + "';";
                        }
                        else
                            this._response = "JumboTCMS.Alert('注册成功，但由于某种原因，邮件发送失败', '1', \"window.location='login.aspx';\");";
                    }
                    else
                    {
                        this._response = "JumboTCMS.Alert('注册成功，请登录', '1', \"window.location='login.aspx';\");";
                    }
                }
                else
                {
                    this._response = "JumboTCMS.Alert('注册失败，原因未知', '0');";
                }
            }
            else
                this._response = "JumboTCMS.Alert('提交有误', '0');";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 再次发送验证邮件
        ///E:/swf/ </summary>
        private void ajaxSendMailAgain()
        {
            string uName = f("txtUserName");
            string uMail = f("txtEmail");
            string usersign = f("txtUserSign");
            string _body = uName + ", 您好！<br>感谢您注册" + site.Name + "，点击下面的链接即可完成注册：<br>" +
                "<a href=\"" + site.Url + site.Dir + "passport/active.aspx?username=" + Server.UrlEncode(uName) + "&amp;email=" + uMail + "&amp;usersign=" + usersign + "\" target=\"_blank\">" +
                site.Url + site.Dir + "passport/active.aspx?username=" + Server.UrlEncode(uName) + "&amp;email=" + uMail + "&amp;usersign=" + usersign + "</a><br>" +
                "（如果链接无法点击，请将它拷贝到浏览器的地址栏中）";
            if (new JumboTCMS.DAL.Normal_UserMailDAL().SendMail(uMail, site.Name + "注册激活邮件", _body))
            {
                Session["jcms_user_register"] = "";
                this._response = "JumboTCMS.Alert('激活邮件已经发送', '1');";
            }
            else
                this._response = "JumboTCMS.Alert('发送失败，请稍候再试', '0');";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 通过用户名和邮箱重置密码
        ///E:/swf/ </summary>
        private void ajaxResetPassword()
        {
            string RandomCode = GetRandomNumberString(8, false);
            doh.Reset();
            doh.ConditionExpress = "username=@username and email=@email";
            doh.AddConditionParameter("@username", f("txtUserName"));
            doh.AddConditionParameter("@email", f("txtEmail"));
            doh.AddFieldItem("UserPass", JumboTCMS.Utils.MD5.Last64(JumboTCMS.Utils.MD5.Lower32(RandomCode)));
            if (doh.Update("jcms_normal_user") > 0)
            {
                string _body = "用户您好！<br>您在" + site.Name + "重置了密码，新密码是：" + RandomCode;
                if (new JumboTCMS.DAL.Normal_UserMailDAL().SendMail(f("txtEmail"), site.Name + "密码找回", _body))
                    this._response = "JumboTCMS.Alert('邮件已发送，请立即去查收', '1', \"window.location='http://mail." + f("txtEmail").Split('@')[1].ToLower() + "';\");";
                else
                    this._response = "JumboTCMS.Alert('邮件发送失败，请稍后再试', '0');";
            }
            else
                this._response = "JumboTCMS.Alert('信息不匹配', '0');";
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 第三方登录（完善信息并注册）
        ///E:/swf/ </summary>
        private void ajaxOAuthNewUser()
        {
            string OAuth_Code = q("code");
            string oauth_info = JumboTCMS.Utils.Cookie.GetValue("oauthinfo_" + OAuth_Code);
            if (oauth_info == null || oauth_info == "")
            {
                this._response = "JumboTCMS.Alert('接口会话已失效，请重新登录第三方网站', '0');";
                return;
            }
            Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(oauth_info);
            string OAuth_Token = (string)newobj["token"];
            string _username = f("txtUserName");
            string _userpass = f("txtPass1");
            string _email = f("txtEmail");
            int _sex = Str2Int(f("rblSex"));
            string _birthday = f("txtBirthday");
            if (_username.Length > 0 && _userpass.Length > 0 && _email.Length > 0 && OAuth_Token != "")
            {
                doh.Reset();
                doh.ConditionExpress = "[Token_" + OAuth_Code + "]=@oauthtoken";
                doh.AddConditionParameter("@oauthtoken", OAuth_Token);
                string _userid = doh.GetField("jcms_normal_user", "id").ToString();
                if (_userid == "")
                {
                    _userid = (new JumboTCMS.DAL.Normal_UserDAL().Register(_username, _username, _userpass, _sex, _email, _birthday, "", "", "", OAuth_Code, OAuth_Token, false)).ToString();
                }
                if (_userid != "0")
                {
                    //注册成功
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", _userid);
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_user");
                    JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false,"");
                    new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_User.UserName, _User.UserPass, 1);
                    this._response = "window.location='" + site.Home + "';";
                }
                else
                    this._response = "JumboTCMS.Alert('注册失败，原因未知', '0');";
            }
            else
                this._response = "JumboTCMS.Alert('提交有误', '0');";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 第三方登录（绑定用户信息）
        ///E:/swf/ </summary>
        private void ajaxOAuthBindUser()
        {
            string OAuth_Code = q("code");
            string oauth_info = JumboTCMS.Utils.Cookie.GetValue("oauthinfo_" + OAuth_Code);
            if (oauth_info == null || oauth_info == "")
            {
                this._response = "JumboTCMS.Alert('接口会话已失效，请重新登录第三方网站', '0');";
                return;
            }
            Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(oauth_info);
            string OAuth_Token = (string)newobj["token"];
            if (f("txtBindUserName").Length > 0 && f("txtBindUserPass").Length > 0 && OAuth_Token != "")
            {
                string _username = f("txtBindUserName").Replace("\'", "");
                string _userpass = JumboTCMS.Utils.MD5.Last64(f("txtBindUserPass"));
                doh.Reset();
                doh.ConditionExpress = "username=@username and userpass=@userpass and state=1";
                doh.AddConditionParameter("@username", _username);
                doh.AddConditionParameter("@userpass", _userpass);
                string _userid = doh.GetField("jcms_normal_user", "id").ToString();
                if (_userid != "")
                {
                    doh.Reset();
                    doh.ConditionExpress = "username=@username and userpass=@userpass";
                    doh.AddConditionParameter("@username", _username);
                    doh.AddConditionParameter("@userpass", _userpass);
                    doh.AddFieldItem("Token_" + OAuth_Code, OAuth_Token);
                    doh.Update("jcms_normal_user");
                    JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false,"");
                    new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_User.UserName, _User.UserPass, 1);
                    this._response = "window.location='" + site.Home + "';";
                }
                else
                    this._response = "JumboTCMS.Alert('绑定失败，信息有误', '0');";
            }
            else
                this._response = "JumboTCMS.Alert('提交有误', '0');";
        }
    }
}

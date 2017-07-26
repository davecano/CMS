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
using System.Data;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Passport
{
    public partial class _discuz_api : JumboTCMS.UI.BasicPage
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (site.ForumAPIKey == "")//说明未整合
                return;
            string _ForumIP = "," + site.ForumIP + ",";
            if (!_ForumIP.Contains("," + Const.GetUserIp + ","))//来源可疑
                return;
            this._operType = q("action");
            switch (this._operType)
            {
                case "register"://注册(Discuz!NT中需增加email的返回)
                    {
                        string _username = q("user_name");
                        string _userpass = q("password");
                        string _email = q("email");
                        int _userid = new JumboTCMS.DAL.Normal_UserDAL().Register(_username, _username, _userpass, 0, _email, "1980-1-1", GetRandomNumberString(32), "", "", "", "", true);
                        if (_userid > 0)
                        {
                            //注册成功
                            doh.Reset();
                            doh.ConditionExpress = "id=@id";
                            doh.AddConditionParameter("@id", _userid);
                            doh.AddFieldItem("ForumName", _username);
                            doh.AddFieldItem("ForumPass", _userpass);
                            doh.AddFieldItem("State", 1);
                            doh.Update("jcms_normal_user");
                        }
                    }
                    break;
                case "login"://登录(Discuz!NT中需增加password的返回)
                    {
                        string _username = q("real_username");
                        string _userpass = q("real_userpass");

                        doh.Reset();
                        doh.ConditionExpress = "forumname=@forumname and forumpass=@forumpass and state=1";
                        doh.AddConditionParameter("@forumname", _username);
                        doh.AddConditionParameter("@forumpass", _userpass);
                        string _userid = doh.GetField("jcms_normal_user", "id").ToString();
                        if (_userid != "")
                        {
                            JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false,"");
                            new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_User.UserName, _User.UserPass, 1);
                        }
                    }
                    break;
                case "updatepwd"://修改密码
                    {
                        string _username = q("user_name");
                        string _userpass = q("password");
                        doh.Reset();
                        doh.ConditionExpress = "forumname=@forumname";
                        doh.AddConditionParameter("@forumname", _username);
                        doh.AddFieldItem("ForumPass", _userpass);
                        doh.Update("jcms_normal_user");
                    }
                    break;
                case "updateprofile"://修改资料
                    {
                        string _savefile = "~/_data/log/discuz_" + this._operType + ".log";
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                        sw.WriteLine(System.DateTime.Now.ToString());
                        sw.WriteLine("\tIP地址：" + Const.GetUserIp);
                        sw.WriteLine("\t用户ID：" + q("uid"));
                        sw.WriteLine("\t用户名：" + q("user_name"));
                        sw.WriteLine("\t来    源：" + Const.GetRefererUrl);
                        sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                        sw.WriteLine("---------------------------------------------------------------------------------------------------");
                        sw.Close();
                        sw.Dispose();
                    }
                    break;
                case "renameuser"://修改用户名
                    {
                        string _old_username = q("old_user_name");
                        string _new_username = q("new_user_name");
                        doh.Reset();
                        doh.ConditionExpress = "forumname=@forumname";
                        doh.AddConditionParameter("@forumname", _old_username);
                        doh.AddFieldItem("forumname", _new_username);
                        doh.Update("jcms_normal_user");
                    }
                    break;
                case "logout"://注销
                    {
                        new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogout("", true);
                    }
                    break;
                default:
                    {
                        string _savefile = "~/_data/log/discuz_" + this._operType + ".log";
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                        sw.WriteLine(System.DateTime.Now.ToString());
                        sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                        sw.WriteLine("\t来    源：" + Const.GetRefererUrl);
                        sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                        sw.WriteLine("---------------------------------------------------------------------------------------------------");
                        sw.Close();
                        sw.Dispose();
                    }
                    break;
            }


        }
    }
}
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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Ajax
{
    public partial class _content : JumboTCMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxDownCount":
                    ajaxDownCount();
                    break;
                case "ajaxAddFavorite":
                    ajaxAddFavorite();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxDownCount()
        {
            string _type = q("cType");
            string _contentid = Str2Str(q("id"));
            if (!ModuleIsOK(_type))
            {
                Response.Write("请勿恶意攻击");
                Response.End();
            }
            if (Str2Int(q("addit")) == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", _contentid);
                doh.Add("jcms_module_" + _type, "DownNum");
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", _contentid);
            this._response = "{\"count\" :\"" + Str2Int(doh.GetField("jcms_module_" + _type, "DownNum").ToString()) + "\"}";
        }
        private void ajaxAddFavorite()
        {
            if (!ModuleIsOK(q("cType")))
            {
                this._response = JsonResult(0, "error");
                return;
            }
            string _channelid = Str2Str(q("ccid"));
            string _contentid = Str2Str(q("id"));
            string _channeltype = q("cType");
            string _userid;
            string _usercookies;
            int favCount = 0;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                _userid = Str2Str(Cookie.GetValue(site.CookiePrev + "user", "id"));
                _usercookies = Cookie.GetValue(site.CookiePrev + "user", "cookies");
                JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, true, _usercookies);
                string[] usersetting = (string[])_User.UserSetting.Split(',');
                bool _CanFavorite = (usersetting[12] == "1");
                favCount = Str2Int(usersetting[14]);
                if (!_CanFavorite)
                {
                    this._response = JsonResult(0, "您所在的组不允许收藏");
                    return;
                }

                doh.Reset();
                doh.ConditionExpress = "userid=@userid and channelid=@channelid and contentid=@contentid";
                doh.AddConditionParameter("@userid", _userid);
                doh.AddConditionParameter("@channelid", _channelid);
                doh.AddConditionParameter("@contentid", _contentid);
                if (doh.Exist("jcms_normal_user_favorite"))
                {
                    this._response = JsonResult(0, "此内容已被你收藏");
                    return;
                }
                if (favCount > 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "[UserId]=" + _userid;
                    int aleadyFav = doh.Count("jcms_normal_user_favorite");
                    if (aleadyFav >= favCount)
                    {
                        this._response = JsonResult(0, "收藏的内容已满");
                        return;
                    }
                }
                doh.Reset();
                doh.ConditionExpress = "channelid=@channelid and id=@id";
                doh.AddConditionParameter("@channelid", _channelid);
                doh.AddConditionParameter("@id", _contentid);
                string _title = doh.GetField("jcms_module_" + _channeltype, "Title").ToString();
                doh.Reset();
                doh.AddFieldItem("Title", _title);
                doh.AddFieldItem("ChannelId", _channelid);
                doh.AddFieldItem("ContentId", _contentid);
                doh.AddFieldItem("ModuleType", _channeltype);
                doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                doh.AddFieldItem("UserId", _userid);
                doh.Insert("jcms_normal_user_favorite");
                this._response = JsonResult(1, "成功收藏");
            }
            else
            {
                this._response = JsonResult(0, "请登录后再收藏");
            }
        }
    }
}
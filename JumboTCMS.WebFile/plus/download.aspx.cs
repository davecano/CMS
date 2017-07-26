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
namespace JumboTCMS.WebFile.Plus
{
    public partial class _download : JumboTCMS.UI.UserCenter
    {
        public string Referer = "";
        public int NO = 0;
        public string UserChecked = "0";
        public string FileUrl = "";
        public string FileTitle = "";
        public string TipInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Referer = site.Home;
            if (Request.ServerVariables["HTTP_REFERER"] != null)
            {
                if (!Request.ServerVariables["HTTP_REFERER"].ToString().StartsWith("http://" + Request.ServerVariables["Server_Name"].ToString()))
                    Referer = Request.ServerVariables["HTTP_REFERER"].ToString();
            }
            if (Cookie.GetValue(site.CookiePrev + "user") == null)
            {
                Response.Write("<script type=\"text/javascript\" src=\"../_data/global.js\"></script>\r\n");
                Response.Write("<script type=\"text/javascript\">top.location.href='../passport/login.aspx?refer='+encodeURIComponent(top.location);</script>\r\n");
                Response.End();
            }
            User_Load("", "html");
            id = Str2Str(q("id"));
            ChannelId = Str2Str(q("ChannelId"));
            ChannelType = q("ChannelType");
            string _modulelist2 = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList2");
            if (!_modulelist2.Contains("." + ChannelType + "."))
            {
                Response.Write("请不要恶意修改提交参数!");
                Response.End();
            }
            if (id == "0")
            {
                Response.Write("请不要恶意修改提交参数!");
                Response.End();
            }
            NO = Str2Int(q("NO"));
            bool _showfileinfo = false;
            doh.Reset();
            doh.ConditionExpress = "ChannelId=" + ChannelId + " AND id=" + id;
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "admin") == null)
                doh.ConditionExpress += " AND [IsPass]=1";
            object[] _obj = doh.GetFields("jcms_module_" + ChannelType, "Title,Points," + ChannelType + "Url");
            if (_obj == null)
            {
                Response.Write("参数有误!");
                Response.End();
            }
            string _SourceTitle = _obj[0].ToString();
            int _Points = Str2Int(_obj[1].ToString(), 0);
            string downUrl = _obj[2].ToString().Replace("\r\n", "\r");
            bool _isvip = new JumboTCMS.DAL.Normal_UserDAL().IsVIPUser(UserId);
            if (!_isvip)//给用户扣除博币,VIP不扣除
            {
                TipInfo = "下载本资源需要" + _Points + "博币，您当前账户剩余" + UserPoints + "博币。";
                if (_Points > 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "ChannelId=" + ChannelId + " and [" + ChannelType + "Id]=" + id + " and UserId=" + UserId;
                    if (doh.Exist("jcms_module_" + ChannelType + "_downlogs"))//说明已经扣过
                    {
                        TipInfo = "您已经支付过本资源，本次下载为免费。";
                        _showfileinfo = true;
                    }
                }
                else
                {
                    _showfileinfo = true;
                }
            }
            else
            {
                TipInfo = "您是本站VIP会员，本次下载部会额外扣除博币。";
                _showfileinfo = true;
            }
            if (_showfileinfo)
            {
                if (downUrl == "")
                {
                    Response.Write("当前下载地址为空!");
                    Response.End();
                }
                string[] _DownUrl = downUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
                if ((NO > _DownUrl.Length - 1) || NO < 0)
                {
                    Response.Write("请不要恶意修改提交参数!");
                    Response.End();
                }
                string _url = _DownUrl[NO];
                if (_url.Contains("|||"))
                    _url = _url.Substring(_url.IndexOf("|||") + 3, (_url.Length - _url.IndexOf("|||") - 3));
                if (!JumboTCMS.Utils.DirFile.FileExists(_url))
                {
                    Response.Write("下载文件不存在!");
                    Response.End();
                }
                UserChecked = "1";
                FileTitle = JumboTCMS.Utils.Strings.DelSymbol(_SourceTitle);
                FileUrl = site.Url + _url;
            }
        }
    }
}

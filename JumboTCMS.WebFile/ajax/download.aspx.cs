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
namespace JumboTCMS.WebFile.Modules.Soft.Ajax
{
    public partial class _down : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 5;//脚本过期时间
            string _userid = Str2Str(q("userid"));
            //if (!(new JumboTCMS.DAL.Normal_UserDAL()).ChkUserSign(_userid, q("usersign")))
            //{
            //    Response.Write(JsonResult(0, "验证信息有误"));
            //    Response.End();
            //}
            string id = Str2Str(q("id"));
            string ChannelId = Str2Str(q("ChannelId"));
            string ChannelType = q("ChannelType");
            string _modulelist2 = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList2");
            if (!_modulelist2.Contains("." + ChannelType + "."))
            {
                Response.Write(JsonResult(0, "请不要恶意修改提交参数!"));
                Response.End();
            }
            int NO = Str2Int(q("NO"));
            doh.Reset();
            doh.ConditionExpress = "ChannelId=" + ChannelId + " and id=" + id;
            object[] _obj = doh.GetFields("jcms_module_" + ChannelType, "Title,Points," + ChannelType + "Url");
            if (_obj == null)
            {
                Response.Write(JsonResult(0, "请不要恶意修改提交参数!"));
                Response.End();
            }
            string _SourceTitle = _obj[0].ToString();
            int _Points = Str2Int(_obj[1].ToString(), 0);
            string downUrl = _obj[2].ToString().Replace("\r\n", "\r");
            if (downUrl == "")
            {
                Response.Write(JsonResult(0, "当前下载地址为空!"));
                Response.End();
            }
            if (_Points > 0)//说明是需要扣除博币的，那么肯定要判断当前用户博币够不够
            {
                if (!CanDownFile(ChannelType, ChannelId, id, _Points, _SourceTitle))
                {
                    Response.Write(JsonResult(0, "您账户的余额不足" + _Points + "博币!"));
                    Response.End();
                }
            }
            doh.Reset();
            doh.ConditionExpress = "ChannelId=" + ChannelId + " and id=" + id;
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "admin") == null)
                doh.ConditionExpress += " AND [IsPass]=1";
            doh.Add("jcms_module_" + ChannelType, "DownNum");
            string[] _DownUrl = downUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
            if ((NO > _DownUrl.Length - 1) || NO < 0)
            {
                Response.Write(JsonResult(0, "请不要恶意修改提交参数!"));
                Response.End();
            }
            string _url = _DownUrl[NO];
            if (_url.Contains("|||"))
                _url = _url.Substring(_url.IndexOf("|||") + 3, (_url.Length - _url.IndexOf("|||") - 3));
            if (JumboTCMS.Utils.DirFile.FileExists(_url))
                Response.Write("{\"result\" :\"1\", \"filetitle\" :\"" + JumboTCMS.Utils.Strings.DelSymbol(_SourceTitle) + "\", \"fileurl\" :\"" + site.Url + _url + "\"}");
            else
                Response.Write(JsonResult(0, "下载文件不存在!"));

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断附件下载权限
        ///E:/swf/ 并扣除相应的博币
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelType"></param>
        ///E:/swf/ <param name="_ChannelId">频道ID</param>
        ///E:/swf/ <param name="_SourceId">内容ID</param>
        ///E:/swf/ <param name="_Points">扣除博币</param>
        ///E:/swf/ <param name="_SourceTitle">资源名称</param>
        ///E:/swf/ <returns></returns>
        private bool CanDownFile(string _ChannelType, string _ChannelId, string _SourceId, int _Points, string _SourceTitle)
        {
            string _userid = "0";
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                _userid = Str2Str(Cookie.GetValue(site.CookiePrev + "user", "id"));
                bool _isvip = new JumboTCMS.DAL.Normal_UserDAL().IsVIPUser(_userid);
                if (!_isvip)//给用户扣除博币,VIP不扣除
                {
                    doh.Reset();
                    doh.ConditionExpress = "ChannelId=" + _ChannelId + " and [" + _ChannelType + "Id]=" + _SourceId + " and UserId=" + _userid;
                    if (doh.Exist("jcms_module_" + _ChannelType + "_downlogs"))//说明已经扣过
                    {
                        return true;
                    }
                    doh.Reset();
                    doh.ConditionExpress = "id=" + _userid;
                    int _myPoints = Str2Int(doh.GetField("jcms_normal_user", "Points").ToString());
                    if (_myPoints < _Points)//说明博币不够
                        return false;
                    //扣除博币
                    new JumboTCMS.DAL.Normal_UserDAL().DeductPoints(_userid, _Points);
                    string _OperInfo1 = "下载资源:<a href=\"" + Go2View(1, false, _ChannelId, _SourceId, false) + "\" target=\"_blank\">" + _SourceTitle + "</a>，扣除了" + _Points + "博币";
                    new JumboTCMS.DAL.Normal_UserLogsDAL().SaveLog(_userid, _OperInfo1, 2);
                    //增加一个下载日志记录
                    doh.Reset();
                    doh.AddFieldItem("UserId", _userid);
                    doh.AddFieldItem("ChannelId", _ChannelId);
                    doh.AddFieldItem(_ChannelType + "Id", _SourceId);
                    doh.AddFieldItem("Points", _Points);
                    doh.AddFieldItem("DownTime", DateTime.Now.ToString());
                    doh.AddFieldItem("DownIP", Const.GetUserIp);
                    doh.AddFieldItem("DownDegree", 1);
                    doh.Insert("jcms_module_" + _ChannelType + "_downlogs");
                }
                return true;
            }
            else
                return false;
        }
    }
}

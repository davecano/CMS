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
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _createhtmlajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 99999;
            ChannelId = Str2Str(q("ccid"));
            string classids = q("classid");
            Admin_Load(ChannelId + "-08", "json", true);
            if (q("oper") == "createchannel")
            {
                JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
                CreateChannelFile(MainChannel);
                teDAL.CreateDefaultFile();
                this._response = JsonResult(1, "频道及站点首页更新完成");
            }
            if (q("oper") == "createbyclass")
            {
                if (q("act") == "class")
                {

                    doh.Reset();
                    doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId;
                    doh.SqlCmd += " and [Id] in (" + classids + ")";
                    doh.SqlCmd += " ORDER BY code";
                    DataTable dtClass = doh.GetDataTable();
                    MakeClass(dtClass);
                    dtClass.Clear();
                    dtClass.Dispose();
                    this._response = JsonResult(1, "success");
                }
                if (q("act") == "content")
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT id FROM [jcms_module_" + ChannelType + "] WHERE [ChannelId]=" + ChannelId + " and [IsPass]=1";
                    doh.SqlCmd += " and [ClassId] in (" + classids + ")";
                    DataTable dtContent = doh.GetDataTable();
                    MakeView(dtContent);
                    dtContent.Clear();
                    dtContent.Dispose();
                    this._response = JsonResult(1, "success");
                }
            }
            if (q("oper") == "createbyid")
            {
                string Sid = Str2Str(q("id1"));
                string Eid = Str2Str(q("id2"));
                string wSql = string.Empty;
                doh.Reset();
                doh.SqlCmd = "SELECT id FROM [jcms_module_" + ChannelType + "] WHERE [ChannelId]=" + ChannelId + " and [IsPass]=1";
                if (Sid != "0")
                {
                    if (Eid == "0")
                        wSql = " And id>=" + Sid;
                    else
                        wSql = " And id between " + Sid + " and " + Eid;
                }
                else
                    return;
                doh.SqlCmd += wSql;
                DataTable dtContent = doh.GetDataTable();
                MakeView(dtContent);
                dtContent.Clear();
                dtContent.Dispose();
                this._response = JsonResult(1, "success");
            }
            Response.Write(this._response);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成栏目页
        ///E:/swf/ </summary>
        private void MakeClass(DataTable dt)
        {
            int total = dt.Rows.Count;
            if (total > 0)
            {
                for (int i = 0; i < total; i++)
                {
                    CreateClassFile(MainChannel, dt.Rows[i]["Id"].ToString(), false);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成内容页
        ///E:/swf/ </summary>
        private void MakeView(DataTable dt)
        {
            string ContentId = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ContentId = dt.Rows[i]["Id"].ToString();
                CreateContentFile(MainChannel, ContentId, -1);
            }
        }
    }
}

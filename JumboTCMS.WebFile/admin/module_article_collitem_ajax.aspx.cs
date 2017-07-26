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
namespace JumboTCMS.WebFile.Admin
{
    public partial class _module_article_collectitem_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            //if (ChannelId == "0")
            //{
            //    doh.Reset();
            //    doh.SqlCmd = "SELECT TOP 1 ID FROM [jcms_normal_channel] WHERE [Type]='article' ORDER BY SubsiteID asc,PID asc";
            //    DataTable dt = doh.GetDataTable();
            //    if (dt.Rows.Count == 1)
            //        ChannelId = dt.Rows[0][0].ToString();
            //    dt.Clear();
            //    dt.Dispose();
            //}
            id = Str2Str(q("id"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxCopy":
                    ajaxCopy();
                    break;
                case "ajaxColl":
                    ajaxColl();
                    break;
                case "ajaxInput":
                    ajaxInput();
                    break;
                case "checkname":
                    ajaxCheckName();
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
        private void ajaxCheckName()
        {
            if (id == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_article_collitem"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id") + " and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_article_collitem"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            //具备内容录入的管理员才可以进来
            Admin_Load(ChannelId + "-01", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int Flag = Str2Int(q("flag"));
            string Auto = q("auto");
            int totalCount = 0;
            string sqlStr = "SELECT a.id as id,a.Title as ItemName,webname,a.ErrorListRule,a.ErrorPageRule,a.ChannelId as Channelid,ClassId,b.Title as ClassName,flag,isrunning,lasttime,(getdate()) as thistime,CollecNewsNum,ListStr FROM [jcms_module_article_collitem] a LEFT JOIN [jcms_normal_class] b on a.classid=b.id WHERE a.deleted=0 and a.channelid=" + ChannelId;
            switch (Auto)
            {
                case "1":
                    sqlStr += " AND [AutoCollect]=1";
                    break;
                case "-1":
                    sqlStr += " AND [AutoCollect]=0";
                    break;
                default:
                    break;
            }
            if (Flag == 1)
                sqlStr += " and a.flag=1";
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            totalCount = dt.Rows.Count;
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxCopy()
        {
            Admin_Load("9999", "json");
            string lId = f("id");
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_module_article_collitem] WHERE [Id]=" + lId + " and channelid=" + ChannelId;
            DataTable dtCollItem = doh.GetDataTable();
            if (dtCollItem.Rows.Count > 0)
            {
                doh.Reset();
                for (int i = 0; i < dtCollItem.Columns.Count; i++)
                {
                    if (dtCollItem.Columns[i].ColumnName.ToLower() != "id")
                    {
                        doh.AddFieldItem(dtCollItem.Columns[i].ColumnName, dtCollItem.Rows[0][i].ToString());
                    }
                }
                doh.Insert("jcms_module_article_collitem");
            }
            dtCollItem.Clear();
            dtCollItem.Dispose();
            this._response = JsonResult(1, "成功克隆");
        }
        private void ajaxInput()
        {
            Admin_Load("9999", "json");
            string XmlPath = Server.MapPath(f("txtXmlPath"));
            if (!System.IO.File.Exists(XmlPath))
            {
                this._response = JsonResult(0, "项目Xml不存在");
                return;
            }
            DataSet ds = JumboTCMS.Utils.XmlCOM.ReadXml(Server.MapPath(f("txtXmlPath")));
            int succeedNum = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                doh.Reset();
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (ds.Tables[0].Columns[i].ColumnName.ToLower() != "id")
                        doh.AddFieldItem(ds.Tables[0].Columns[i].ColumnName.ToString(), dr[i].ToString());
                }
                doh.Insert("jcms_module_article_collitem");
                succeedNum++;
            }
            this._response = JsonResult(1, succeedNum + "条采集项目导入成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 执行批量操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oper"></param>
        ///E:/swf/ <param name="ids"></param>
        private void ajaxBatchOper()
        {
            Admin_Load(ChannelId + "-01", "json");
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            if (act == "del")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "[ItemId]=" + idValue[i];
                    if (doh.Exist("jcms_module_article_collfilters"))
                        continue;
                    else
                    {
                        doh.Reset();
                        doh.ConditionExpress = "[ItemId]=" + idValue[i];
                        doh.Delete("jcms_module_article_collhistory");
                        doh.Reset();
                        doh.ConditionExpress = "id=" + idValue[i];
                        doh.Delete("jcms_module_article_collitem");
                    }
                }
                this._response = JsonResult(1, "删除成功（带采集过滤的项目无法删除）");
            }
            else if (act == "out")
            {
                doh.Reset();
                doh.SqlCmd = "SELECT Id,Title,ChannelId,ClassId,WebName,WebUrl,ItemDemo,ListStr,ListStart,ListEnd,LinkStart,LinkEnd,TitleStart,ToString,ContentStart,ContentEnd,NPageStart,NPageEnd,AuthorStr,SourceFrom,Flag,Script_Iframe,Script_Object,Script_Script,Script_Div,Script_Table,Script_Span,Script_Img,Script_Font,Script_A,Script_Html,CollecNewsNum,SaveFiles,CollecOrder,ListWebEncode,ContentWebEncode" +
                    " FROM [jcms_module_article_collitem] WHERE [Id] in (" + ids + ")";
                DataTable dt = doh.GetDataTable();
                dt.WriteXml(Server.MapPath("~/_data/databackup/collitem" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xml"), XmlWriteMode.IgnoreSchema);
                this._response = JsonResult(1, "导出成功");
            }
            else
                this._response = JsonResult(0, "执行错误");
        }
        private void ajaxColl()
        {
            Admin_Load(ChannelId + "-01", "json");
            string id = Str2Str(f("id"));
            int num = Str2Int(f("num"));
            string CollitemName = "";
            string _Result = CollectionNews(id, num, AdminName, ref CollitemName);
            this._response = _Result.Replace("\"returnval\" :\"", "\"returnval\" :\"《" + CollitemName + "》：");
        }
    }
}
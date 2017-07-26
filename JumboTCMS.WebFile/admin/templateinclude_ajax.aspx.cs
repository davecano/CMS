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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _templateinclude_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string pId, tpPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            pId = Str2Str(q("pid"));
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", pId);
            tpPath = doh.GetField("jcms_normal_themeproject", "Dir").ToString();
            if (tpPath.Length == 0)
            {
                Response.Write("HTML模板方案选择有误!");
                Response.End();
                return;
            }
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "updatefore":
                    ajaxUpdateFore();
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
            Admin_Load("", "json");
            doh.Reset();
            doh.ConditionExpress = "title=@title and id<>" + id;
            doh.AddConditionParameter("@title", q("txtTitle"));
            if (doh.Exist("jcms_normal_themeinclude"))
                this._response = JsonResult(0, "不可录入");
            else
                this._response = JsonResult(1, "可以录入");
        }
        private void ajaxGetList()
        {
            Admin_Load("", "json");
            int _NeedBuild = Str2Int(q("needbuild"));
            doh.Reset();
            if (_NeedBuild == 1)
                doh.SqlCmd = "SELECT a.id as id,a.title as title,b.title as pTitle,a.info as info,Sort,a.source as source,a.needbuild FROM [jcms_normal_themeinclude] as a,[jcms_normal_themeproject] as b where a.needbuild=1 and a.pId = b.id and a.pId=" + pId;
            else
                doh.SqlCmd = "SELECT a.id as id,a.title as title,b.title as pTitle,a.info as info,Sort,a.source as source,a.needbuild FROM [jcms_normal_themeinclude] as a,[jcms_normal_themeproject] as b where a.pId = b.id and a.pId=" + pId;

            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            Admin_Load("templateinclude-mng", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_normal_themeinclude");
            this._response = JsonResult(1, "成功删除");
        }
        private void ajaxUpdateFore()
        {
            Admin_Load("templateinclude-mng", "json");
            CreateIncludeFiles();
            this._response = JsonResult(1, "更新完成,前台页面需要刷新");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成包含文件
        ///E:/swf/ </summary>
        private void CreateIncludeFiles()
        {
            string _source = q("source");
            doh.Reset();
            if (_source == "")
                doh.SqlCmd = "SELECT * FROM [jcms_normal_themeinclude] ORDER BY [Sort]";
            else
                doh.SqlCmd = "SELECT * FROM [jcms_normal_themeinclude] where [Source]='" + _source + "'";
            DataTable dt = doh.GetDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string TempStr = JumboTCMS.Utils.DirFile.ReadFile(site.Dir + "themes/" + tpPath + "/include/" + dt.Rows[i]["Source"].ToString());
                    JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
                    if (dt.Rows[i]["NeedBuild"].ToString() == "1")
                    {

                        teDAL.IsHtml = site.IsHtml;
                        teDAL.ReplacePublicTag(ref TempStr);
                        teDAL.ReplaceChannelClassLoopTag(ref TempStr);
                        teDAL.ReplaceContentLoopTag(ref TempStr);
                    }
                    teDAL.SaveHTML(TempStr, "~/_data/shtm/" + dt.Rows[i]["Source"].ToString(), true);//shtm引用
                    teDAL.SaveHTML(TempStr, "~/_data/html/" + dt.Rows[i]["Source"].ToString(), false);//aspx引用
                }
            }
            dt.Clear();
            dt.Dispose();
        }
    }
}
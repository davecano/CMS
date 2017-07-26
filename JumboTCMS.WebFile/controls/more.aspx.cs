﻿/*
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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Controls
{
    public partial class _more : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();

            string ChannelId = (this.lblChannelId.Text == "{$ChannelId}") ? Str2Str(q("ChannelId")) : Str2Str(this.lblChannelId.Text);
            string ClassId = (this.lblClassId.Text == "{$ClassId}") ? Str2Str(q("id")) : Str2Str(this.lblClassId.Text);
            string WhereStr = this.lblWhereStr.Text;
            int Pagesize = Str2Int(this.lblPagesize.Text);
            string ScriptFile = this.lblScriptFile.Text;
            string TemplateFile = this.lblTemplateFile.Text;


            doh.Reset();
            doh.ConditionExpress = "id=@id and Enabled=1";
            doh.AddConditionParameter("@id", ChannelId);
            if (!doh.Exist("jcms_normal_channel"))
            {
                FinalMessage("频道不存在或被禁用!", site.Home, 0, 8);
                Response.End();
            }
            if (ClassId != "0")
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " and [Id]=" + ClassId;
                DataTable dtSearch = doh.GetDataTable();
                if (dtSearch.Rows.Count == 0)
                {
                    FinalMessage("栏目不存在或已被删除!", site.Home, 0, 8);
                    Response.End();
                }
                dtSearch.Clear();
                dtSearch.Dispose();
            }

            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
            if (Pagesize == 0)
                Pagesize = 20;
            int pageCount = new JumboTCMS.DAL.Normal_ClassDAL().GetContetPageCount(ChannelId, ClassId, true, WhereStr, Pagesize);
            CurrentPage = JumboTCMS.Utils.Int.Min(CurrentPage, pageCount);
            teDAL.IsHtml = false;
            string TxtStr = teDAL.GetSiteListPage(ChannelId, ClassId, CurrentPage, Pagesize, WhereStr, TemplateFile, ScriptFile);
            teDAL.ReplaceSHTMLTag(ref TxtStr);
            teDAL.ReplaceUserTag(ref TxtStr);
            Response.Write(TxtStr);//直接输出
        }
    }
}

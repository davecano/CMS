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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _templateinclude_edittemplate : JumboTCMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        private string _Source = string.Empty;
        private string _tempFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("templateinclude-mng", "stop");
            id = Str2Str(q("id"));
            string pid = Str2Str(q("pid"));
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", pid);
            tpPath = doh.GetField("jcms_normal_themeproject", "Dir").ToString();
            if (tpPath.Length == 0)
            {
                Response.Write("HTML模板方案选择有误!");
                Response.End();
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            _Source = doh.GetField("jcms_normal_themeinclude", "Source").ToString();


            _tempFile = site.Dir + "themes/" + tpPath + "/include/" + _Source;
            this.lblTemplateFile.Text = _tempFile;
            if (!IsPostBack)
            {
                if (JumboTCMS.Utils.DirFile.FileExists(_tempFile))
                    this.txtTemplateContent.Text = JumboTCMS.Utils.DirFile.ReadFile(_tempFile);
                else
                    this.txtTemplateContent.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PageStr = this.txtTemplateContent.Text;
            JumboTCMS.Utils.DirFile.SaveFile(PageStr, _tempFile);
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}

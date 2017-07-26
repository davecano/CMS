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
    public partial class _template_edittemplate : JumboTCMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        private string _Source = string.Empty;
        private string _tempFile1 = string.Empty;
        private string _tempFile2 = string.Empty;
        public bool UsingAsterisk = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("template-mng", "stop");
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
            }
            JumboTCMS.DAL.Normal_TemplateDAL sTempate = new JumboTCMS.DAL.Normal_TemplateDAL();
            _Source = sTempate.GetSource(id);//可能带星号
            if (_Source.Contains("*"))
            {
                UsingAsterisk = true;
                _tempFile1 = site.Dir + "themes/" + tpPath + "/" + _Source.Replace("*", "0");
                _tempFile2 = site.Dir + "themes/" + tpPath + "/" + _Source.Replace("*", "1");

            }
            else
            {
                _tempFile1 = site.Dir + "themes/" + tpPath + "/" + _Source;
                _tempFile2 = site.Dir + "themes/" + tpPath + "/" + _Source;
            }
            this.lblTemplateFile1.Text = _tempFile1;
            this.lblTemplateFile2.Text = _tempFile2;
            if (!IsPostBack)
            {
                this.txtTemplateContent1.Text = JumboTCMS.Utils.DirFile.ReadFile(_tempFile1);
                this.txtTemplateContent2.Text = JumboTCMS.Utils.DirFile.ReadFile(_tempFile2);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            JumboTCMS.Utils.DirFile.SaveFile(this.txtTemplateContent1.Text, _tempFile1);
            if (UsingAsterisk)
                JumboTCMS.Utils.DirFile.SaveFile(this.txtTemplateContent2.Text, _tempFile2);
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}

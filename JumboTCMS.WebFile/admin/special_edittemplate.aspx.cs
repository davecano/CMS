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
    public partial class _special_edittemplate : JumboTCMS.UI.AdminCenter
    {
        private string _Source = string.Empty;
        private string _tempFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("special-mng", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.Entity.Normal_Special eSpecial = new JumboTCMS.DAL.Normal_SpecialDAL().GetEntity(id);
            _Source = eSpecial.Source;
            _tempFile = site.Dir + "_data/special/_" + _Source;
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
            if (this.chkSavaDefault.Checked)
                JumboTCMS.Utils.DirFile.SaveFile(PageStr, site.Dir + "themes/special_index.htm");
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}

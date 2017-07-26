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
    public partial class _special_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("special-mng", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_special", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtOrderNum, "OrderNum", false);
            wh.AddBind(txtSource, "Source", true);
            wh.AddBind(txtInfo, "Info", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                this.txtSource.Enabled = false;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            //判断重复性
            JumboTCMS.DAL.Normal_SpecialDAL dal = new JumboTCMS.DAL.Normal_SpecialDAL();
            if (dal.ExistTitle(this.txtTitle.Text, id, ""))
            {
                FinalMessage("专题名重复!", "", 1);
                return false;
            }
            if (dal.ExistSource(this.txtSource.Text, id, ""))
            {
                FinalMessage("文件名重复!", "", 1);
                return false;
            }
            return true;

        }
        protected void save_ok(object sender, EventArgs e)
        {
            string _tempFile = site.Dir + "_data/special/_" + this.txtSource.Text;
            if (!JumboTCMS.Utils.DirFile.FileExists(_tempFile))
            {
                string _defaultTemplate = JumboTCMS.Utils.DirFile.ReadFile("~/themes/special_index.htm");
                JumboTCMS.Utils.DirFile.SaveFile(_defaultTemplate, _tempFile);
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}

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
    public partial class _forbidip_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            this.txtExpireDate.Attributes.Add("onFocus", "WdatePicker({isShowClear:false,readOnly:true,skin:'blue',startDate:'" + System.DateTime.Now.AddYears(1).ToShortDateString() + "'})");
            if (id == "0")
                this.txtExpireDate.Attributes.Add("value", System.DateTime.Now.AddYears(1).ToShortDateString());
            this.txtExpireDate.Attributes.Add("readonly", "readonly");

            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_forbidip", btnSave);
            wh.AddBind(txtStartIP, "StartIP2", true);
            wh.AddBind(txtEndIP, "EndIP2", true);
            wh.AddBind(txtExpireDate, "ExpireDate", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
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
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 格式化为IP字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ipDoubleStr"></param>
        ///E:/swf/ <returns></returns>
        protected string GetIPStr(string ipDoubleStr)
        {
            long ipLong = long.Parse(ipDoubleStr);
            System.Net.IPAddress ip = JumboTCMS.Utils.IPHelp.Long2IP(ipLong);
            return ip.ToString();
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            if (new JumboTCMS.DAL.Normal_ForbidipDAL().UpdateIPData(id, this.txtStartIP.Text, this.txtEndIP.Text))
                FinalMessage("保存成功", "close.htm", 0);
            else
                FinalMessage("保存错误", "close.htm", 0);
        }
    }
}

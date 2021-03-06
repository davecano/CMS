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
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _admin_editpower : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            string[] menu = powerMenu();
            hfAdminId.Value = id;
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            string admin_power = doh.GetField("jcms_normal_user", "AdminSetting").ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" class=\"formtable\">");
            doh.Reset();
            doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_channel] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dtChannel = doh.GetDataTable();
            string mChannelId = string.Empty;
            string mChannelName = string.Empty;
            for (int i = 0; i < dtChannel.Rows.Count; i++)
            {
                mChannelId = dtChannel.Rows[i]["Id"].ToString();
                mChannelName = dtChannel.Rows[i]["Title"].ToString();
                sb.Append("<tr><th>" + mChannelName + "</th>");
                sb.Append("<td>");
                for (int j = 0; j < menu.GetLength(0); j++)
                {
                    if (menu[j] == null)
                        break;
                    sb.Append("<span style='margin-left:10px;padding-top:10px;'><input type=checkbox class='checkbox' name=\"admin_power\" value=\"");
                    string tPower = string.Empty;
                    if (j < 10)
                        tPower = mChannelId + "-0" + j.ToString();
                    else
                        tPower = mChannelId + "-" + j.ToString();
                    sb.Append(tPower + "\"");
                    //if (admin_power.IndexOf("," + tPower + ",") > -1)
                    if (admin_power.Contains("," + tPower + ","))
                        sb.Append(" checked");
                    sb.Append(">." + menu[j] + "</span>\r\n");
                    if ((j % 5 == 0) && (j > 0))
                        sb.Append("<br /><br />");
                }
                sb.Append("</td></tr>");
            }
            dtChannel.Clear();
            dtChannel.Dispose();
            sb.Append("<tr><th>其他管理</th>");
            sb.Append("<td>");

            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_normal_adminpower] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dtPower = doh.GetDataTable();
            for (int i = 0; i < dtPower.Rows.Count; i++)
            {
                string PowerName = dtPower.Rows[i]["Title"].ToString();
                string PowerCode = dtPower.Rows[i]["Code"].ToString();
                sb.Append("<span style='margin-left:10px;padding-top:10px;'><input type=checkbox class='checkbox' name=\"admin_power\" value=\"" + PowerCode + "\"");
                if (admin_power.Contains("," + PowerCode + ","))
                    sb.Append(" checked");
                sb.Append("> " + PowerName + "</span>");
            }
            dtPower.Clear();
            dtPower.Dispose();

            sb.Append("</td></tr>");
            sb.Append("</table>");
            this.ltAdminSetting.Text = sb.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string admin_power = ",";
            if (Request.Form["admin_power"] != null)
                admin_power = "," + Request.Form["admin_power"].ToString() + ",";
            id = hfAdminId.Value.ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("AdminSetting", admin_power);
            doh.Update("jcms_normal_user");
            FinalMessage("正确保存!", "close.htm", 0);
        }
    }
}

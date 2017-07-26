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
using JumboTCMS.DBUtility;
using System.Collections;
namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 发邮件信息
    ///E:/swf/ </summary>
    public class Normal_UserMailDAL : Common
    {
        public Normal_UserMailDAL()
        {
            base.SetupSystemDate();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 系统发邮件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_To">收件人,单封邮件</param>
        ///E:/swf/ <param name="_Title">标题</param>
        ///E:/swf/ <param name="_Body">内容</param>
        ///E:/swf/ <param name="_IsHtml">是否支持html</param>
        ///E:/swf/ <param name="_MailServer">邮箱服务器列表</param>
        ///E:/swf/ <returns></returns>
        public bool SendMails(string _To, string _Title, string _Body, string _Attach, bool _IsHtml, JumboTCMS.Entity.MailServer _MailServer)
        {
            _Body += "<br /><br />" + site.Name + "  <a href='" + site.Url + "'>" + site.Url + "</a>";
            return JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, _Attach, _IsHtml, _MailServer);

        }
        public bool SendMails(string _To, string _Title, string _Body, bool _IsHtml, JumboTCMS.Entity.MailServer _MailServer)
        {
            return SendMails(_To, _Title, _Body, "", _IsHtml, _MailServer);

        }
        public bool SendMails(string _To, string _Title, string _Body, JumboTCMS.Entity.MailServer _MailServer)
        {
            return SendMails(_To, _Title, _Body, "", true, _MailServer);
        }
        public bool SendMail(string _To, string _Title, string _Body)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/mail.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _MailFrom = XmlTool.GetText("Root/Address");
            string _MailFromName = XmlTool.GetText("Root/NickName");
            string _MailPwd = XmlTool.GetText("Root/Password");
            string _MailSmtpHost = XmlTool.GetText("Root/SmtpHost");
            int _MailSmtpPort = Str2Int(XmlTool.GetText("Root/SmtpPort"));
            XmlTool.Dispose();
            _Body += "<br /><br />" + site.Name + "  <a href='" + site.Url + "'>" + site.Url + "</a>";
            return JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, true, _MailFrom, _MailFromName, _MailPwd, _MailSmtpHost, _MailSmtpPort);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 系统发邮件给客服
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Title"></param>
        ///E:/swf/ <param name="_Body"></param>
        ///E:/swf/ <returns></returns>
        public bool SendServiceMail(string _Title, string _Body)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/message.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _ServiceMail = XmlTool.GetText("Messages/Service/UserMail");
            XmlTool.Dispose();
            return SendMail(_ServiceMail, _Title, _Body);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 导出数据至配置文件
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public bool ExportEmailServer()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                XmlTool.RemoveAll("Mails");
                XmlTool.Save();
                using (DbOperHandler _doh = new Common().Doh())
                {
                    _doh.Reset();
                    _doh.SqlCmd = "Select * FROM [jcms_email_smtpserver] WHERE [Enabled]=1 ORDER BY id asc";
                    DataTable dt = _doh.GetDataTable();
                    string _id = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _id = dt.Rows[i]["Id"].ToString();
                        XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                        XmlTool.InsertNode("Mails", "Mail", "ID", _id);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromAddress", dt.Rows[i]["FromAddress"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromName", dt.Rows[i]["FromName"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromPwd", dt.Rows[i]["FromPwd"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "SmtpHost", dt.Rows[i]["SmtpHost"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "SmtpPort", dt.Rows[i]["SmtpPort"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "Used", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"), false);
                        XmlTool.Save();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 导入配置文件至数据库
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public bool ImportEmailServer()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                using (DbOperHandler _doh = new Common().Doh())
                {
                    _doh.Reset();
                    _doh.Delete("jcms_email_smtpserver");
                    DataTable dt = XmlTool.GetTable("Mails");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _doh.Reset();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].ColumnName.ToLower() != "id" && dt.Columns[j].ColumnName.ToLower() != "used")
                                    _doh.AddFieldItem(dt.Columns[j].ColumnName.ToLower(), dt.Rows[i][j].ToString());
                            }
                            _doh.Insert("jcms_email_smtpserver");
                        }
                    }
                    dt.Clear();
                    dt.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

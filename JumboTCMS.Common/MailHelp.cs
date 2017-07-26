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
using System.Configuration;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
namespace JumboTCMS.Common
{
    ///E:/swf/ <summary>
    ///E:/swf/ 发送邮件类
    ///E:/swf/ </summary>
    public static class MailHelp
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailTimeCycle">单个邮箱发信的间隔周期</param>
        ///E:/swf/ <returns></returns>
        public static JumboTCMS.Entity.MailServer MailServer(int MailTimeCycle)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            DataTable dtTemp = XmlTool.GetTable("Mails");
            XmlTool.Dispose();
            System.Collections.IList _FromAddresss = new System.Collections.ArrayList();
            System.Collections.IList _FromNames = new System.Collections.ArrayList();
            System.Collections.IList _FromPwds = new System.Collections.ArrayList();
            System.Collections.IList _SmtpHosts = new System.Collections.ArrayList();
            System.Collections.IList _SmtpPorts = new System.Collections.ArrayList();
            System.Collections.IList _Useds = new System.Collections.ArrayList();
            if (dtTemp == null)
            {
                return null;
            }
            if (dtTemp.Rows.Count == 0)
            {
                return null;
            }
            dtTemp.DefaultView.Sort = "Used ASC";
            DataTable dt = dtTemp.DefaultView.ToTable();
            dtTemp.Clear();
            dtTemp.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.CompareOrdinal(dt.Rows[i]["Used"].ToString(), DateTime.Now.AddSeconds(0 - MailTimeCycle).ToString("yyyy-MM-dd HH:mm:ss")) <= 0)//确保周期内只发送一次
                {
                    _FromAddresss.Add(dt.Rows[i]["FromAddress"].ToString());
                    _FromNames.Add(dt.Rows[i]["FromName"].ToString());
                    _FromPwds.Add(dt.Rows[i]["FromPwd"].ToString());
                    _SmtpHosts.Add(dt.Rows[i]["SmtpHost"].ToString());
                    _SmtpPorts.Add(dt.Rows[i]["SmtpPort"].ToString());
                    _Useds.Add(dt.Rows[i]["Used"].ToString());
                }
            }
            dt.Clear();
            dt.Dispose();

            JumboTCMS.Entity.MailServer _MailServer = new JumboTCMS.Entity.MailServer();
            _MailServer.FromAddresss = _FromAddresss;
            _MailServer.FromNames = _FromNames;
            _MailServer.FromPwds = _FromPwds;
            _MailServer.SmtpHosts = _SmtpHosts;
            _MailServer.SmtpPorts = _SmtpPorts;
            _MailServer.Useds = _Useds;
            if (_MailServer.FromAddresss == null)
                return null;
            return _MailServer;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发送邮件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailTo">接收人用户名,单封邮件</param>
        ///E:/swf/ <param name="MailSubject">邮件主题</param>
        ///E:/swf/ <param name="MailBody">邮件内容</param>
        ///E:/swf/ <param name="IsHtml">邮件正文是否为HTML格式</param>
        ///E:/swf/ <param name="MailFrom">发件人邮箱地址</param>
        ///E:/swf/ <param name="MailFromName">发件人署名</param>
        ///E:/swf/ <param name="MailPwd">发件人邮箱密码</param>
        ///E:/swf/ <param name="MailSmtpHost">发件人邮箱Host,如"smtp.sina.com"</param>
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, bool IsHtml, string MailFrom, string MailFromName, string MailPwd, string MailSmtpHost, int MailSmtpPort)
        {
            JumboTCMS.Utils.Mail.MailMessage message = new JumboTCMS.Utils.Mail.MailMessage();
            message.MaxRecipientNum = 80;//最大收件人
            message.From = System.Configuration.ConfigurationManager.AppSettings["JumboTCMS:WebmasterEmail"];
            message.FromName = MailFromName;
            string[] _mail = MailTo.Split(',');
            for (int j = 0; j < _mail.Length; j++)
            {
                message.AddRecipients(_mail[j]);
            }
            message.Subject = MailSubject;
            if (IsHtml)
                message.BodyFormat = JumboTCMS.Utils.Mail.MailFormat.HTML;
            else
                message.BodyFormat = JumboTCMS.Utils.Mail.MailFormat.Text;
            message.Priority = JumboTCMS.Utils.Mail.MailPriority.Normal;
            message.Body = MailBody;
            JumboTCMS.Utils.Mail.SmtpClient smtp = new JumboTCMS.Utils.Mail.SmtpClient(MailSmtpHost, MailSmtpPort);
            if (smtp.Send(message, MailFrom, MailPwd))
                return true;
            else
            {
                SaveErrLog(MailTo, MailFrom, MailFromName, MailSmtpHost, smtp.ErrMsg);
                return false;
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 发送邮件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailTo">接收人用户名,单封邮件</param>
        ///E:/swf/ <param name="MailSubject">邮件主题</param>
        ///E:/swf/ <param name="MailBody">邮件内容</param>
        ///E:/swf/ <param name="MailAttach">邮件附件</param>
        ///E:/swf/ <param name="IsHtml">邮件正文是否为HTML格式</param>
        ///E:/swf/ <param name="_MailServer"></param>
        ///E:/swf/ <returns></returns>
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, string MailAttach, bool IsHtml, JumboTCMS.Entity.MailServer _MailServer)
        {
            if (_MailServer == null)
                return false;
            bool _SendOK = false;
            string _WebmasterEmail = System.Configuration.ConfigurationManager.AppSettings["JumboTCMS:WebmasterEmail"];
            for (int i = 0; i < _MailServer.FromAddresss.Count; i++)
            {
                string MailFrom = _MailServer.FromAddresss[i].ToString();
                string MailFromName = _MailServer.FromNames[i].ToString();
                string MailPwd = _MailServer.FromPwds[i].ToString();
                string MailSmtpHost = _MailServer.SmtpHosts[i].ToString();
                int MailSmtpPort = Convert.ToInt16(_MailServer.SmtpPorts[i].ToString());
                JumboTCMS.Utils.Mail.MailMessage message = new JumboTCMS.Utils.Mail.MailMessage();
                message.MaxRecipientNum = 80;//最大收件人
                message.From = _WebmasterEmail;
                message.FromName = MailFromName;
                string[] _mail = MailTo.Split(',');
                for (int j = 0; j < _mail.Length; j++)
                {
                    message.AddRecipients(_mail[j]);
                }
                message.Subject = MailSubject;
                if (IsHtml)
                    message.BodyFormat = JumboTCMS.Utils.Mail.MailFormat.HTML;
                else
                    message.BodyFormat = JumboTCMS.Utils.Mail.MailFormat.Text;
                message.Priority = JumboTCMS.Utils.Mail.MailPriority.Normal;
                message.Body = MailBody;
                if (MailAttach != "") message.Attachments.Add(System.Web.HttpContext.Current.Server.MapPath(MailAttach));
                JumboTCMS.Utils.Mail.SmtpClient smtp = new JumboTCMS.Utils.Mail.SmtpClient(MailSmtpHost, MailSmtpPort);
                if (smtp.Send(message, MailFrom, MailPwd))
                {
                    string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
                    JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                    XmlTool.Update("Mails/Mail[FromAddress=\"" + MailFrom + "\"]/Used", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    XmlTool.Save();
                    XmlTool.Dispose();
                    _SendOK = true;
                    SaveSucLog(MailTo, MailFrom, MailFromName, MailSmtpHost);
                    break;//跳出循环
                }
                else
                {
                    SaveErrLog(MailTo, MailFrom, MailFromName, MailSmtpHost, smtp.ErrMsg + "\r\n当前共有：" + _MailServer.FromAddresss.Count + "个发件人在队列中.");
                    continue;
                }
            }
            return _SendOK;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发送邮件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailTo">接收人用户名,单封邮件</param>
        ///E:/swf/ <param name="MailSubject">邮件主题</param>
        ///E:/swf/ <param name="MailBody">邮件内容</param>
        ///E:/swf/ <param name="IsHtml">邮件正文是否为HTML格式</param>
        ///E:/swf/ <param name="_MailServer"></param>
        ///E:/swf/ <returns></returns>
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, bool IsHtml, JumboTCMS.Entity.MailServer _MailServer)
        {
            return SendOK(MailTo, MailSubject, MailBody, "", IsHtml, _MailServer);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存正确日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailFrom"></param>
        ///E:/swf/ <param name="MailFromName"></param>
        ///E:/swf/ <param name="MailSmtpHost"></param>
        private static void SaveSucLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/mailsuccess_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, System.Text.Encoding.UTF8);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("\t收 信 人：" + MailTo);
            sw.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            sw.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            sw.WriteLine("---------------------------------------------------------------------------------------------------");
            sw.Close();
            sw.Dispose();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存错误日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="MailFrom"></param>
        ///E:/swf/ <param name="MailFromName"></param>
        ///E:/swf/ <param name="MailSmtpHost"></param>
        ///E:/swf/ <param name="ErrMsg"></param>
        private static void SaveErrLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost, string ErrMsg)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, System.Text.Encoding.UTF8);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("\t收 信 人：" + MailTo);
            sw.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            sw.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            sw.WriteLine("\t错误信息：\r\n" + ErrMsg);
            sw.WriteLine("---------------------------------------------------------------------------------------------------");
            sw.Close();
            sw.Dispose();
        }
    }
}

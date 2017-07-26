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
using System.Collections;
namespace JumboTCMS.Entity
{
    public class MailServer
    {
        private IList m_FromAddresss;
        private IList m_FromNames;
        private IList m_FromPwds;
        private IList m_SmtpHosts;
        private IList m_SmtpPorts;
        private IList m_Useds;
        public MailServer()
        {
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发件人地址
        ///E:/swf/ </summary>
        public IList FromAddresss
        {
            get { return m_FromAddresss; }
            set { m_FromAddresss = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发件人称呼
        ///E:/swf/ </summary>
        public IList FromNames
        {
            get { return m_FromNames; }
            set { m_FromNames = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发件人密码
        ///E:/swf/ </summary>
        public IList FromPwds
        {
            get { return m_FromPwds; }
            set { m_FromPwds = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发件服务器smtp
        ///E:/swf/ </summary>
        public IList SmtpHosts
        {
            get { return m_SmtpHosts; }
            set { m_SmtpHosts = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发件服务器端口
        ///E:/swf/ </summary>
        public IList SmtpPorts
        {
            get { return m_SmtpPorts; }
            set { m_SmtpPorts = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 成功发送次数
        ///E:/swf/ </summary>
        public IList Useds
        {
            get { return m_Useds; }
            set { m_Useds = value; }
        }
    }
}

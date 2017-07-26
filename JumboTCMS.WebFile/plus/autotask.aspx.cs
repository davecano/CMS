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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _autotask : JumboTCMS.UI.BasicPage
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        private string _spliter = "'";
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            if (base.DBType == "0") _spliter = "#";
            switch (this._operType)
            {
                case "DeleteNotice":
                    DeleteNotice();
                    break;
                case "DeleteUnactivedUser":
                    DeleteUnactivedUser();
                    break;
                case "CreateDefaultPage":
                    CreateDefaultPage();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            this._response = "未知操作";
        }
        private void DeleteNotice()
        {
            string _password = q("password");
            if (_password != System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"])
            {
                this._response = "密码错误";
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "[State]=1 AND [ReadTime]<=" + _spliter + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + _spliter;
            int _doCount = doh.Delete("jcms_normal_user_notice");
            doh.Reset();
            doh.ConditionExpress = "[State]=1 AND [ReadTime]<=" + _spliter + System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + _spliter;
            int _doCount2 = doh.Delete("jcms_normal_user_message");
            this._response = "有" + _doCount + "条已阅读的站内信被删除；有" + _doCount2 + "条已阅读的通知被删除";
        }
        private void DeleteUnactivedUser()
        {
            string _password = q("password");
            if (_password != System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"])
            {
                this._response = "密码错误";
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "[State]=0 AND [RegTime]<=" + _spliter + System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + _spliter;
            int _doCount = doh.Delete("jcms_normal_user");
            this._response = "有" + _doCount + "个未激活的会员被删除";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成首页
        ///E:/swf/ </summary>
        private void CreateDefaultPage()
        {
            string _password = q("password");
            if (_password != System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"])
            {
                this._response = "密码错误";
                return;
            }
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.CreateDefaultFile();
            this._response = "成功生成";
        }
    }
}
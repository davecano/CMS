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
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;

namespace JumboTCMS.WebFile
{
    public class Global : System.Web.HttpApplication
    {
        public static HttpContext myContext = HttpContext.Current;
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
            aTimer.Interval = 1000;// 设置引发时间的时间间隔　此处设置为１秒
            aTimer.Enabled = true;
        }
        private void TimeEvent(object source, ElapsedEventArgs e)
        {
            // 得到 hour minute second　如果等于某个值就开始执行某个程序。
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            #region 每天的准点开始生成一次首页
            //假设web服务异常或正常的重启，则强行执行一次任务（目的是为防止遗漏）
            //if ((intMinute == 0 && intSecond == 0) || myContext.Application["AutoTask_1"] == null)
            if ((intMinute == 0 && intSecond == 0))
            {
                string _url = ServerUrl() + "/plus/autotask.aspx?oper=CreateDefaultPage&password=" + System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"];
                System.IO.StreamWriter sw = new System.IO.StreamWriter(myContext.Request.PhysicalApplicationPath + "\\_data\\log\\autotask_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true, System.Text.Encoding.UTF8);
                sw.WriteLine(e.SignalTime.ToString());
                sw.WriteLine(JumboTCMS.Utils.HttpHelper.Get_Http(_url, 10000, System.Text.Encoding.UTF8));
                sw.Close();
                sw.Dispose();
                myContext.Application.Lock();
                myContext.Application["AutoTask_1"] = "true";
                myContext.Application.UnLock();
            }
            #endregion
        }
        private string ServerUrl()
        {
            return "http://" + myContext.Request.ServerVariables["HTTP_HOST"].ToString();
        }
        private string AppPath()
        {
            string _ApplicationPath = myContext.Request.ApplicationPath;
            if (_ApplicationPath != "/")
                _ApplicationPath += "/";
            return _ApplicationPath;
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }
        protected void Application_End(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);
            //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start  
            try
            {
                string url = ServerUrl() + "/loading.html";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  
            }
            catch
            {
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        /*
        protected void Application_Error(object sender, EventArgs e)
        {
            if (!Request.Browser.Crawler)//如果不是爬虫
            {
                Exception objExp = HttpContext.Current.Server.GetLastError();
                CYQ.Data.Log.WriteLogToTxt("\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message);
            }
            this.FileNotFound_Error();

        }
        ///E:/swf/ <summary> 
        ///E:/swf/ 404错误处理 
        ///E:/swf/ </summary> 
        private void FileNotFound_Error()
        {
            HttpException erroy = Server.GetLastError() as HttpException;
            if (erroy != null && erroy.GetHttpCode() == 404)
            {
                Server.ClearError();
                string path = "~/404.aspx";
                Server.Transfer(path);
            }
        }
         * */
        protected void Session_End(object sender, EventArgs e)
        {

        }
    }
}
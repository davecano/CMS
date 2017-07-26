using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DBUtility.UI
{
    ///E:/swf/ <summary>
    ///E:/swf/ WebPage的通用基类。实现了一些常用操作。
    ///E:/swf/ </summary>
    public abstract class PageUI : System.Web.UI.Page
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 覆盖系统默认的错误页
        ///E:/swf/ </summary>
        protected override void OnError(EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            Exception exception = ctx.Server.GetLastError();
            string errorInfo =
                "\r\n<pre>Offending URL: " + ctx.Request.Url.ToString() +
                "\r\nSource: " + exception.Source +
                "\r\nMessage: " + exception.Message +
                "\r\nStack trace: " + exception.StackTrace + "</pre>";

            ctx.Response.Write(errorInfo);
            ctx.Server.ClearError();
            base.OnError(e);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 表示数据库访问对象。通常需要另外一层继承来实现站点相关的通用操作后再在页面中使用。
        ///E:/swf/ </summary>
        public JumboTCMS.DBUtility.DbOperHandler doh;
        ///E:/swf/ <summary>
        ///E:/swf/ 待实现的连接数据库函数。
        ///E:/swf/ </summary>
        public abstract void ConnectDb();
        ///E:/swf/ <summary>
        ///E:/swf/ 连接Sql Server数据库。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="serverName">服务器地址。</param>
        ///E:/swf/ <param name="userName">用户名。</param>
        ///E:/swf/ <param name="password">密码。</param>
        ///E:/swf/ <param name="dataBaseName">数据库名称。</param>
        public void ConnectDb(string serverName, string userName, string password, string dataBaseName)
        {
            System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection("server='" + serverName + "';uid=" + userName + ";pwd=" + password + ";database=" + dataBaseName);
            doh = new JumboTCMS.DBUtility.SqlDbOperHandler(sqlConn);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 连接到一个Access数据库。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="dataBase">数据库名称。</param>
        public void ConnectDb(string dataBase)
        {
            System.Data.OleDb.OleDbConnection oleConn = new System.Data.OleDb.OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=" + this.Server.MapPath(dataBase));
            doh = new JumboTCMS.DBUtility.OleDbOperHandler(oleConn);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 页面初始化的通用操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            JbInit();
            base.OnInit(e);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 页面初始化
        ///E:/swf/ </summary>
        protected virtual void JbInit()
        {
            this.Unload += new EventHandler(Jbpage_Unload);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 在客户端显示弹出对话框。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="msg">要显示的信息。</param>
        public void Alert(string msg)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script language=\"javascript\">alert('" + msg + "')</script>");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 在客户端显示弹出对话框。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name">脚本块标识。当同一页面要调用两个弹出框时需不同的标识，否则后者会覆盖前者。</param>
        ///E:/swf/ <param name="msg">要显示的信息。</param>
        public void Alert(string name, string msg)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), name, "<script language=\"javascript\">alert('" + msg + "');</script>");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得两个日期的间隔
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="DateTime1">日期一。</param>
        ///E:/swf/ <param name="DateTime2">日期二。</param>
        ///E:/swf/ <returns>日期间隔TimeSpan。</returns>
        public TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 当页面从内存卸载时发生，关闭数据库连接
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender"></param>
        ///E:/swf/ <param name="e"></param>
        private void Jbpage_Unload(object sender, EventArgs e)
        {
            if (doh != null)
            {
                doh.Dispose();
            }
        }
    }
}

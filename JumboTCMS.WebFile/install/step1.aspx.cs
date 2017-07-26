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
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using ADOX;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Install
{
    public partial class _step1 : System.Web.UI.Page
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            Step1();
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV7_dbType"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7_dbPath"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7"] = null;
            System.Web.HttpContext.Current.Application.UnLock();

            Response.Write(this._response);
        }
        private void Step1()
        {
            string dbConnString = "";
            if (q("dbtype") == "0")
            {
                if (!JumboTCMS.Utils.DirFile.FileExists("~/install/scripts/access/step1.sql"))
                {
                    this._response = "脚本文件不存在";
                    return;
                }
                string DBPath = q("databasepath");
                if (JumboTCMS.Utils.DirFile.FileExists(DBPath))
                {
                    this._response = "目标数据库已存在";
                    return;
                }
                else
                {
                    dbConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(DBPath);
                    JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(Server.MapPath(DBPath)));
                    ADOX.CatalogClass cat = new ADOX.CatalogClass();
                    cat.Create(dbConnString);
                    //保存配置文件
                    try
                    {
                        string _connFile = Server.MapPath("~/_data/config/conn.config");
                        string _connText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Root>\r\n\t<dbType>0</dbType>\r\n\t<dbPath>" + DBPath + "</dbPath>\r\n\t<dbConnStr></dbConnStr>\r\n</Root>";
                        StreamWriter sw = new StreamWriter(_connFile, false, Encoding.UTF8);
                        sw.Write(_connText);
                        sw.Close();
                        if (!JumboTCMS.Utils.ExecuteSqlBlock.Go("0", dbConnString, Server.MapPath("~/install/scripts/access/step1.sql")))
                        {
                            this._response = "Access版本的step1.sql执行失败";
                            return;
                        }
                    }
                    catch
                    {
                        this._response = "未知错误";
                    }

                }
            }
            else
            {
                if (!JumboTCMS.Utils.DirFile.FileExists("~/install/scripts/sqlserver/step1.sql"))
                {
                    this._response = "脚本文件不存在";
                    return;
                }
                string dbConnStr = "Data Source=" + q("servername") + ";Initial Catalog=" + q("databasename") + ";User ID=" + q("username") + ";Password=" + q("userpass") + ";Pooling=true";
                dbConnString = dbConnStr;
                if (ConnOK(dbConnString))
                {
                    //保存配置文件
                    try
                    {
                        string _connFile = Server.MapPath("~/_data/config/conn.config");
                        string _connText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Root>\r\n\t<dbType>1</dbType>\r\n\t<dbServerIP>" + q("servername") + "</dbServerIP>\r\n\t<dbLoginName>" + q("username") + "</dbLoginName>\r\n\t<dbLoginPass>" + q("userpass") + "</dbLoginPass>\r\n\t<dbName>" + q("databasename") + "</dbName>\r\n</Root>";
                        StreamWriter sw = new StreamWriter(_connFile, false, Encoding.UTF8);
                        sw.Write(_connText);
                        sw.Close();
                        if (!JumboTCMS.Utils.ExecuteSqlBlock.Go("1", dbConnString, Server.MapPath("~/install/scripts/sqlserver/step1.sql")))
                        {
                            this._response = "Sql Server版本的step1.sql执行失败";
                            return;
                        }
                    }
                    catch
                    {
                        this._response = "未知错误";
                    }

                }
                else
                {
                    this._response = "Sql Server数据库配置有误";
                    return;
                }
            }
            this._response = "ok";
        }
        private bool ConnOK(string connectionString)
        {
            try
            {
                SqlConnection conn1 = new SqlConnection(connectionString);
                conn1.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取querystring
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数名</param>
        ///E:/swf/ <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboTCMS.Utils.Strings.SafetyQueryS(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }
    }
}
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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _dbaccessajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxCompactAccess":
                    ajaxCompactAccess();
                    break;
                case "ajaxBackupAccess":
                    ajaxBackupAccess();
                    break;
                case "ajaxRestoreAccess":
                    ajaxRestoreAccess();
                    break;
                case "ajaxBackupMssql":
                    ajaxBackupMssql();
                    break;
                case "ajaxRestoreMssql":
                    ajaxRestoreMssql();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 压缩Access
        ///E:/swf/ </summary>
        private void ajaxCompactAccess()
        {
            doh.Dispose();
            string tempPath = Server.MapPath(Application["jcmsV7_dbPath"].ToString());
            string _dbPath = JumboTCMS.Utils.DirFile.GetFolderPath(tempPath);
            string _dbName = JumboTCMS.Utils.DirFile.GetFileName(tempPath);
            if (!System.IO.File.Exists(tempPath))
            {
                this._response = JsonResult(0, "目标数据库不存在");
                return;
            }
            try
            {
                string temp = _dbPath + DateTime.Now.Year.ToString("yyyyMMddHHmmssffff") + ".bak";
                string temp2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + temp;
                string _dbPath2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tempPath;
                JRO.JetEngineClass jt = new JRO.JetEngineClass();
                jt.CompactDatabase(_dbPath2, temp2);
                System.IO.File.Copy(temp, tempPath, true);
                System.IO.File.Delete(temp);
            }
            catch (Exception)
            {
                this._response = JsonResult(0, "其它用户连接数据库");
                return;
            }
            this._response = JsonResult(1, "数据库压缩成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 备份Access
        ///E:/swf/ </summary>
        private void ajaxBackupAccess()
        {
            string dbFileName = f("dbname").Replace("'", "").Replace(";", "");
            doh.Dispose();
            string tempPath = Server.MapPath(Application["jcmsV7_dbPath"].ToString());
            string _dbPath = JumboTCMS.Utils.DirFile.GetFolderPath(tempPath);
            string _dbName = JumboTCMS.Utils.DirFile.GetFileName(tempPath);
            try
            {
                System.IO.File.Copy(_dbPath + "\\" + _dbName, _dbPath + "\\..\\databackup\\" + dbFileName, true);
            }
            catch (Exception)
            {
                this._response = JsonResult(0, "数据库正在被使用");
                return;
            }
            this._response = JsonResult(1, "数据库备份成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 还原Access
        ///E:/swf/ </summary>
        private void ajaxRestoreAccess()
        {
            string dbFileName = f("dbname").Replace("'", "").Replace(";", "");
            doh.Dispose();
            string tempPath = Server.MapPath(Application["jcmsV7_dbPath"].ToString());
            string _dbPath = JumboTCMS.Utils.DirFile.GetFolderPath(tempPath);
            string _dbName = JumboTCMS.Utils.DirFile.GetFileName(tempPath);
            if (System.IO.File.Exists(_dbPath + "\\..\\databackup\\" + dbFileName))
            {
                try
                {
                    System.IO.File.Copy(_dbPath + "\\..\\databackup\\" + dbFileName, _dbPath + "\\" + _dbName, true);
                }
                catch (Exception)
                {
                    this._response = JsonResult(0, "数据库正在被使用");
                    return;
                }
            }
            else
            {
                this._response = JsonResult(0, "原始数据库不存在");
                return;
            }
            this._response = JsonResult(1, "数据库恢复成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 备份MSSQL
        ///E:/swf/ </summary>
        private void ajaxBackupMssql()
        {
            string dbFileName = f("dbname").Replace("'", "").Replace(";", "");
            if (!dbFileName.EndsWith(".bak"))
            {
                dbFileName += ".bak";
            }
            string _path = Server.MapPath("~/_data/databackup/" + dbFileName);
            if (System.IO.File.Exists(_path))
            {
                this._response = JsonResult(0, "目标文件已存在");
                return;
            }
            else
            {
                string dbServerIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                string dbLoginName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                string dbLoginPass = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                string dbName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                if (SQLBACK(dbServerIP, dbLoginName, dbLoginPass, dbName, _path))
                    this._response = JsonResult(1, "数据库备份成功");
                else
                    this._response = JsonResult(0, "数据库备份失败");

            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 还原MSSQL
        ///E:/swf/ </summary>
        private void ajaxRestoreMssql()
        {
            string dbFileName = f("dbname").Replace("'", "").Replace(";", "");
            if (!dbFileName.EndsWith(".bak"))
            {
                dbFileName += ".bak";
            }
            string _path = Server.MapPath("~/_data/databackup/" + dbFileName);
            if (!System.IO.File.Exists(_path))
            {
                this._response = JsonResult(0, "源文件不存在");
                return;
            }
            else
            {
                string dbServerIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                string dbLoginName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                string dbLoginPass = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                string dbName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                if (SQLDbRestore(dbServerIP, dbLoginName, dbLoginPass, dbName, _path))
                    this._response = JsonResult(1, "数据库恢复成功");
                else
                    this._response = JsonResult(0, "数据库恢复失败");

            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ SQL数据库备份
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ServerIP">SQL服务器IP或(Localhost)</param>
        ///E:/swf/ <param name="LoginName">数据库登录名</param>
        ///E:/swf/ <param name="LoginPass">数据库登录密码</param>
        ///E:/swf/ <param name="DBName">数据库名</param>
        ///E:/swf/ <param name="BackPath">备份到的路径</param>
        public static bool SQLBACK(string ServerIP, string LoginName, string LoginPass, string DBName, string BackPath)
        {
            SQLDMO.Backup oBackup = new SQLDMO.BackupClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(ServerIP, LoginName, LoginPass);
                oBackup.Database = DBName;
                oBackup.Files = BackPath;
                oBackup.BackupSetName = DBName;
                oBackup.BackupSetDescription = "数据库备份";
                oBackup.Initialize = true;
                oBackup.SQLBackup(oSQLServer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ SQL恢复数据库
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ServerIP">SQL服务器IP或(Localhost)</param>
        ///E:/swf/ <param name="LoginName">数据库登录名</param>
        ///E:/swf/ <param name="LoginPass">数据库登录密码</param>
        ///E:/swf/ <param name="DBName">要还原的数据库名</param>
        ///E:/swf/ <param name="BackPath">数据库备份的路径</param>
        public static bool SQLDbRestore(string ServerIP, string LoginName, string LoginPass, string DBName, string BackPath)
        {

            SQLDMO.Restore orestore = new SQLDMO.RestoreClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(ServerIP, LoginName, LoginPass);
                orestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                orestore.Database = DBName;
                orestore.Files = BackPath;
                orestore.FileNumber = 1;
                orestore.ReplaceDatabase = true;
                orestore.SQLRestore(oSQLServer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }
    }
}
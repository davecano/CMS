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
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
namespace JumboTCMS.Utils
{
    public static class ExecuteSqlBlock
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 执行Sql脚本块
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="dbType">0为access,1为sqlserver</param>
        ///E:/swf/ <param name="connectionString">数据库连接</param>
        ///E:/swf/ <param name="pathToScriptFile">脚本路径，物理路径</param>
        ///E:/swf/ <returns></returns>
        public static bool Go(string dbType, string connectionString, string pathToScriptFile)
        {
            StreamReader _reader = null;
            Stream stream = null;
            if (!System.IO.File.Exists(pathToScriptFile))
                return false;
            try
            {

                string sql = "";
                stream = System.IO.File.OpenRead(pathToScriptFile);
                _reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                if (dbType == "0")//执行access
                {
                    using (OleDbConnection conn0 = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand command0 = new OleDbCommand())
                        {
                            conn0.Open();
                            command0.Connection = conn0;
                            command0.CommandType = System.Data.CommandType.Text;
                            while (null != (sql = ReadNextStatementFromStream(_reader)))
                            {
                                command0.CommandText = sql;
                                command0.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else//执行sqlserver
                {
                    using (SqlConnection conn1 = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command1 = new SqlCommand())
                        {
                            conn1.Open();
                            command1.Connection = conn1;
                            command1.CommandType = System.Data.CommandType.Text;
                            while (null != (sql = ReadNextStatementFromStream(_reader)))
                            {
                                command1.CommandText = sql;
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _reader.Close();
                _reader.Dispose();
                stream.Close();
                stream.Dispose();
            }
        }
        private static string ReadNextStatementFromStream(StreamReader _reader)
        {
            StringBuilder sb = new StringBuilder();
            string lineOfText;
            while (true)
            {
                lineOfText = _reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                    {
                        return sb.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                if (lineOfText.TrimEnd().ToUpper() == "GO")
                {
                    break;
                }
                sb.AppendFormat("{0}\r\n", lineOfText);
            }
            return sb.ToString();
        }
    }
}

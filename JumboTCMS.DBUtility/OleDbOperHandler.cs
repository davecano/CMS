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

namespace JumboTCMS.DBUtility
{
    ///E:/swf/ <summary>
    ///E:/swf/ 本对象用与提供对OLEDB数据库的常用访问操作。
    ///E:/swf/ </summary>
    public class OleDbOperHandler : DbOperHandler
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数，接收一个OLEDB数据库连接对象OleDbConnection
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_conn"></param>
        public OleDbOperHandler(System.Data.OleDb.OleDbConnection _conn)
        {
            conn = _conn;
            dbType = DatabaseType.OleDb;
            conn.Open();
            cmd = conn.CreateCommand();
            da = new System.Data.OleDb.OleDbDataAdapter();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数，接收一个mdb文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="path"></param>
        public OleDbOperHandler(string path)
        {
            conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path);
            dbType = DatabaseType.OleDb;
            conn.Open();
            cmd = conn.CreateCommand();
            da = new System.Data.OleDb.OleDbDataAdapter();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产生OleDbCommand对象所需的查询参数。
        ///E:/swf/ </summary>
        protected override void GenParameters()
        {
            System.Data.OleDb.OleDbCommand oleCmd = (System.Data.OleDb.OleDbCommand)cmd;
            if (this.alFieldItems.Count > 0)
            {
                for (int i = 0; i < alFieldItems.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@para" + i.ToString(), ((DbKeyItem)alFieldItems[i]).fieldValue.ToString());
                }
            }

            if (this.alSqlCmdParameters.Count > 0)
            {
                for (int i = 0; i < this.alSqlCmdParameters.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@spara" + i.ToString(), ((DbKeyItem)alSqlCmdParameters[i]).fieldValue.ToString());
                }
            }
            if (this.alConditionParameters.Count > 0)
            {
                for (int i = 0; i < this.alConditionParameters.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@cpara" + i.ToString(), ((DbKeyItem)alConditionParameters[i]).fieldValue.ToString());
                }
            }
        }

    }
}

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
namespace JumboTCMS.DBUtility
{
    ///E:/swf/ <summary>
    ///E:/swf/ 本对象用与提供对SqlServer数据库的常用访问操作。
    ///E:/swf/ </summary>
    public class SqlDbOperHandler : DbOperHandler
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数，接收一个SqlServer数据库连接对象SqlConnection
        ///E:/swf/ </summary>
        public SqlDbOperHandler(System.Data.SqlClient.SqlConnection _conn)
        {
            conn = _conn;
            dbType = DatabaseType.SqlServer;

            conn.Open();
            cmd = conn.CreateCommand();
            da = new System.Data.SqlClient.SqlDataAdapter();

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产生SqlCommand对象所需的查询参数。
        ///E:/swf/ </summary>
        protected override void GenParameters()
        {
            System.Data.SqlClient.SqlCommand sqlCmd = (System.Data.SqlClient.SqlCommand)cmd;
            if (this.alFieldItems.Count > 0)
            {
                for (int i = 0; i < alFieldItems.Count; i++)
                {
                    sqlCmd.Parameters.AddWithValue("@para" + i.ToString(), ((DbKeyItem)alFieldItems[i]).fieldValue.ToString());
                }
            }

            if (this.alSqlCmdParameters.Count > 0)
            {
                for (int i = 0; i < this.alSqlCmdParameters.Count; i++)
                {
                    sqlCmd.Parameters.AddWithValue(((DbKeyItem)alSqlCmdParameters[i]).fieldName.ToString(), ((DbKeyItem)alSqlCmdParameters[i]).fieldValue.ToString());
                }
            }
            if (this.alConditionParameters.Count > 0)
            {
                for (int i = 0; i < this.alConditionParameters.Count; i++)
                {
                    sqlCmd.Parameters.AddWithValue(((DbKeyItem)alConditionParameters[i]).fieldName.ToString(), ((DbKeyItem)alConditionParameters[i]).fieldValue.ToString());
                }
            }
        }

    }
}
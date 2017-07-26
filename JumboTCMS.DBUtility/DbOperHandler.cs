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
    ///E:/swf/ 表示数据库连接类型。
    ///E:/swf/ </summary>
    public enum DatabaseType : byte { SqlServer, OleDb };
    ///E:/swf/ <summary>
    ///E:/swf/ DbOperHandler 的摘要说明。
    ///E:/swf/ </summary>
    public abstract class DbOperHandler : IDisposable
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 析构函数，释放申请的资源。
        ///E:/swf/ </summary>
        ~DbOperHandler()
        {
            conn.Close();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 表示数据库连接的类型，目前支持SqlServer和OLEDB
        ///E:/swf/ </summary>
        public DatabaseType dbType = DatabaseType.OleDb;
        ///E:/swf/ <summary>
        ///E:/swf/ 返回当前使用的数据库连接对象。
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public IDbConnection GetConnection()
        {
            return conn;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 条件表达式，用于在数据库操作时筛选记录，通常用于仅需指定表名称和某列名称的操作，如GetValue()，Delete()等，支持查询参数，由AddConditionParameters指定。。
        ///E:/swf/ </summary>
        public string ConditionExpress = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前的SQL语句。
        ///E:/swf/ </summary>
        public string SqlCmd = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前操作所涉及的数据表名称。
        ///E:/swf/ </summary>
        protected string tableName = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前操作所设计的字段名称。
        ///E:/swf/ </summary>
        protected string fieldName = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前所使用的数据库连接。
        ///E:/swf/ </summary>
        protected IDbConnection conn;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前所使用的命令对象。
        ///E:/swf/ </summary>
        protected IDbCommand cmd;
        ///E:/swf/ <summary>
        ///E:/swf/ 当前所使用的数据库适配器。
        ///E:/swf/ </summary>
        protected IDbDataAdapter da;
        ///E:/swf/ <summary>
        ///E:/swf/ 用于存储字段/值配对。
        ///E:/swf/ </summary>
        protected System.Collections.ArrayList alFieldItems = new System.Collections.ArrayList(10);
        ///E:/swf/ <summary>
        ///E:/swf/ 用于存储SQL语句中的查询参数。
        ///E:/swf/ </summary>
        protected System.Collections.ArrayList alSqlCmdParameters = new System.Collections.ArrayList(5);
        ///E:/swf/ <summary>
        ///E:/swf/ 用于存储条件表达式中的查询参数。
        ///E:/swf/ </summary>
        protected System.Collections.ArrayList alConditionParameters = new System.Collections.ArrayList(5);
        ///E:/swf/ <summary>
        ///E:/swf/ 重置该对象，使之恢复到构造时的状态。
        ///E:/swf/ </summary>
        public void Reset()
        {
            this.alFieldItems.Clear();
            this.alSqlCmdParameters.Clear();
            this.alConditionParameters.Clear();
            this.ConditionExpress = string.Empty;
            this.SqlCmd = string.Empty;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.cmd.CommandType = CommandType.Text;//默认非存储过程执行
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加一个字段/值对到数组中。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <param name="_fieldValue">字段值。</param>
        public void AddFieldItem(string _fieldName, object _fieldValue)
        {

            for (int i = 0; i < this.alFieldItems.Count; i++)
            {
                if (((DbKeyItem)this.alFieldItems[i]).fieldName == _fieldName)
                {
                    throw new ArgumentException(_fieldName + "不能重复赋值!");
                }
            }
            this.alFieldItems.Add(new DbKeyItem(_fieldName, _fieldValue));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 批量添加字段/值对到数组中。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_vFields">用一个2行X列的二维数组表示。[0, X]为字段名名，[1, X]为字段的值</param>
        public void AddFieldItems(object[,] _vFields)
        {
            if ((!Object.Equals(_vFields, null)) && (_vFields.GetUpperBound(0) == 1) && (_vFields.Rank == 2))
                for (int i = 0; i <= _vFields.GetUpperBound(1); i++)
                    if (!Object.Equals(_vFields[0, i], null)) AddFieldItem(_vFields[0, i].ToString(), _vFields[1, i]);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加条件表达式中的查询参数到数组中。注意：当数据库连接为SqlServer时，参数名称必须和SQL语句匹配。其它则必须保持添加顺序和ConditionExpress中参数顺序一致，否则会失效。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_conditionName">条件名称。</param>
        ///E:/swf/ <param name="_conditionValue">条件值。</param>
        public void AddConditionParameter(string _conditionName, object _conditionValue)
        {
            for (int i = 0; i < this.alConditionParameters.Count; i++)
            {
                if (((DbKeyItem)this.alConditionParameters[i]).fieldName == _conditionName)
                {
                    throw new ArgumentException("条件参数名\"" + _conditionName + "\"不能重复赋值!");
                }
            }
            this.alConditionParameters.Add(new DbKeyItem(_conditionName, _conditionValue));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 批量添加条件表达式中的查询参数到数组中。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_vParameters">用一个2行X列的二维数组表示。[0, X]为Parameter名，[1, X]为Parameter的值</param>
        public void AddConditionParameters(object[,] _vParameters)
        {
            if ((!Object.Equals(_vParameters, null)) && (_vParameters.GetUpperBound(0) == 1) && (_vParameters.Rank == 2))
                for (int i = 0; i <= _vParameters.GetUpperBound(1); i++)
                    if (!Object.Equals(_vParameters[0, i], null)) AddConditionParameter(_vParameters[0, i].ToString(), _vParameters[1, i]);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 返回满足条件的记录数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tableName">要查询的数据表名</param>
        ///E:/swf/ <returns>是/否</returns>
        public int Count(string tableName)
        {
            return Convert.ToInt32(this.GetField(tableName, "count(*)", false).ToString());
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断记录是否存在
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tableName">要查询的数据表名</param>
        ///E:/swf/ <returns>是/否</returns>
        public bool Exist(string tableName)
        {
            return this.GetField(tableName, "count(*)", false).ToString() != "0";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得最大id
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tableName">要查询的数据表名</param>
        public int MaxId(string tableName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "max(id)", false).ToString());
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得最小值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tableName">要查询的数据表名</param>
        public int MinValue(string tableName, string fieldName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "min(" + fieldName + ")", false).ToString());
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得最大值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tableName">要查询的数据表名</param>
        public int MaxValue(string tableName, string fieldName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "max(" + fieldName + ")", false).ToString());
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 抽象函数。用于产生Command对象所需的参数。
        ///E:/swf/ </summary>
        protected abstract void GenParameters();
        ///E:/swf/ <summary>
        ///E:/swf/ 根据当前alFieldItem数组中存储的字段/值向指定表中添加一条数据。在该表无触发器的情况下返回添加数据所获得的自动增长id值。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">要插入数据的表名称。</param>
        ///E:/swf/ <returns>返回本数据连接上产生的最后一个自动增长id值。</returns>
        public int Insert(string _tableName)
        {

            this.tableName = _tableName;
            this.fieldName = string.Empty;
            this.SqlCmd = "insert into [" + this.tableName + "](";
            string tempValues = " values(";
            for (int i = 0; i < this.alFieldItems.Count - 1; i++)
            {
                this.SqlCmd += "[" + ((DbKeyItem)alFieldItems[i]).fieldName + "]";
                this.SqlCmd += ",";

                tempValues += "@para";
                tempValues += i.ToString();

                tempValues += ",";
            }
            this.SqlCmd += "[" + ((DbKeyItem)alFieldItems[alFieldItems.Count - 1]).fieldName + "]";
            this.SqlCmd += ") ";

            tempValues += "@para";
            tempValues += (alFieldItems.Count - 1).ToString();

            tempValues += ")";
            this.SqlCmd += tempValues;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            cmd.ExecuteNonQuery();
            int autoId = 0;
            try
            {
                this.cmd.CommandText = "select @@identity as id";
                autoId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception) { }
            return autoId;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 根据当前alFieldItem数组中存储的字段/值和条件表达式所指定的条件来更新数据库中的记录，返回所影响的行数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">要更新的数据表名称。</param>
        ///E:/swf/ <returns>返回此次操作所影响的数据行数。</returns>
        public int Update(string _tableName)
        {
            this.tableName = _tableName;
            this.fieldName = string.Empty;
            this.SqlCmd = "UPDATE [" + this.tableName + "] SET ";
            for (int i = 0; i < this.alFieldItems.Count - 1; i++)
            {
                this.SqlCmd += "[" + ((DbKeyItem)alFieldItems[i]).fieldName + "]";
                this.SqlCmd += "=";

                this.SqlCmd += "@para";
                this.SqlCmd += i.ToString();

                this.SqlCmd += ",";
            }
            this.SqlCmd += "[" + ((DbKeyItem)alFieldItems[alFieldItems.Count - 1]).fieldName + "]";
            this.SqlCmd += "=";

            this.SqlCmd += "@para";
            this.SqlCmd += (alFieldItems.Count - 1).ToString();
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd += " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            int effectedLines = this.cmd.ExecuteNonQuery();
            return effectedLines;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 执行SqlCmd中的SQL语句，参数由AddSqlCmdParameters指定，与ConditionExpress无关。
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>返回此次操作所影响的数据行数。</returns>
        public int ExecuteSqlNonQuery()
        {
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteNonQuery();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除表
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName"></param>
        ///E:/swf/ <returns></returns>
        public bool DropTable(string _tableName)
        {
            try
            {
                this.cmd.CommandText = "drop table [" + _tableName + "]";
                this.cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断表是否存在
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName"></param>
        ///E:/swf/ <returns></returns>
        public bool ExistTable(string _tableName)
        {
            try
            {
                this.cmd.CommandText = "select top 1 * from [" + _tableName + "]";
                this.cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取指定表，指定列，指定条件的第一个符合条件的值。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <param name="_isField">是否纯字段名？</param>
        ///E:/swf/ <returns>获取的值。如果为空则返回null。</returns>
        ///E:/swf/ 
        public object GetField(string _tableName, string _fieldName, bool _isField)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            if (_isField)
                this.SqlCmd = "select [" + this.fieldName + "] from [" + this.tableName + "]";
            else
                this.SqlCmd = "select " + this.fieldName + " from [" + this.tableName + "]";
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            object ret = cmd.ExecuteScalar();
            if (ret == null) ret = (object)string.Empty;
            return ret;
        }
        public object GetField(string _tableName, string _fieldName)
        {
            return GetField(_tableName, _fieldName, true);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取指定表，指定列，指定条件的第一行中符合条件的值的集合。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldNames">字段名称,以逗号隔开。</param>
        ///E:/swf/ <returns>获取的值。如果为空则返回null。</returns>
        public object[] GetFields(string _tableName, string _fieldNames)
        {
            this.SqlCmd = "select " + _fieldNames + " from " + _tableName;
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            System.Data.DataSet ds = new System.Data.DataSet();
            this.da.SelectCommand = this.cmd;
            this.da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                object[] _obj = new object[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                    _obj[i] = dt.Rows[0][i];
                return _obj;
            }
            return null;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取指定表，指定列，指定条件的记录数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <returns>获取的记录数</returns>
        public int GetCount(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            this.SqlCmd = "select count(" + this.fieldName + ") from [" + this.tableName + "]";
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return (int)cmd.ExecuteScalar();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 根据当前指定的SqlCmd获取DataTable。如果ConditionExpress不为空则会将其清空，所以条件表达式需要包含在SqlCmd中。
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>返回查询结果DataTable。</returns>
        public DataTable GetDataTable()
        {
            DataSet ds = this.GetDataSet();
            return ds.Tables[0];
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 根据当前指定的SqlCmd获取DataSet。如果ConditionExpress不为空则会将其清空，所以条件表达式需要包含在SqlCmd中。
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>返回查询结果DataSet。</returns>
        public DataSet GetDataSet()
        {
            this.alConditionParameters.Clear();
            this.ConditionExpress = string.Empty;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            DataSet ds = new DataSet();
            this.da.SelectCommand = this.cmd;
            this.da.Fill(ds);
            return ds;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 对指定表，指定字段执行加一计数，返回计数后的值。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <returns>返回计数后的值。</returns>
        public int Add(string _tableName, string _fieldName)
        {
            return Add(_tableName, _fieldName, 1);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 对指定表，指定字段执行增加计数，返回计数后的值。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <param name="_num">减少的值。</param>
        ///E:/swf/ <returns>返回计数后的值。</returns>
        public int Add(string _tableName, string _fieldName, int _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            int count = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName));
            count = count + _num;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, count);
            this.Update(this.tableName);
            return count;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 对指定表，指定字段执行减一计数，返回计数后的值。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <returns>返回计数后的值。</returns>
        public int Deduct(string _tableName, string _fieldName)
        {
            return Deduct(_tableName, _fieldName, 1);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 对指定表，指定字段执行减少计数，返回计数后的值。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <param name="_num">减少的值。</param>
        ///E:/swf/ <returns>返回计数后的值。</returns>
        public int Deduct(string _tableName, string _fieldName, int _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            int count = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName));
            if (count > 0)
            {
                count = count - _num;
                if (count < 0) count = 0;
            }
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, count);
            this.Update(this.tableName);
            return count;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 对指定表，返回字段值的总和。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <returns>返回字段值的总和。</returns>
        public int Sum(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            int sum = Convert.ToInt32("0" + this.GetField(this.tableName, "sum(" + this.fieldName + ")", false));
            return sum;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 根据ConditionExpress指定的条件在指定表中删除记录。返回删除的记录数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">指定的表名称。</param>
        ///E:/swf/ <returns>返回删除的记录数。</returns>
        public int Delete(string _tableName)
        {
            this.tableName = _tableName;
            this.SqlCmd = "delete from [" + this.tableName + "]";
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return cmd.ExecuteNonQuery();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 审核函数。将指定表，指定字段的值进行翻转，如：1->0或0->1。条件由ConditionExpress指定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tableName">表名称。</param>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <returns>返回影响的行数。</returns>
        public int Audit(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            this.SqlCmd = "UPDATE [" + this.tableName + "] SET [" + this.fieldName + "]=1-" + this.fieldName;
            if (this.ConditionExpress != string.Empty)
            {
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return cmd.ExecuteNonQuery();
        }
        public DataRow GetSP_Row(string ProcedureName)
        {
            this.cmd.CommandText = ProcedureName;
            this.cmd.CommandType = CommandType.StoredProcedure;//指定是存储过程
            this.GenParameters();
            System.Data.SqlClient.SqlDataAdapter ada = new System.Data.SqlClient.SqlDataAdapter();
            ada.SelectCommand = (System.Data.SqlClient.SqlCommand)cmd;
            DataTable dt = new DataTable();
            ada.Fill(dt);
            ada.Dispose();
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public DataRowCollection GetSP_Rows(string ProcedureName)
        {
            this.cmd.CommandText = ProcedureName;
            this.cmd.CommandType = CommandType.StoredProcedure;//指定是存储过程
            this.GenParameters();
            System.Data.SqlClient.SqlDataAdapter ada = new System.Data.SqlClient.SqlDataAdapter();
            ada.SelectCommand = (System.Data.SqlClient.SqlCommand)cmd;
            DataTable dt = new DataTable();
            ada.Fill(dt);
            ada.Dispose();
            return dt.Rows;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 释放资源
        ///E:/swf/ </summary>
        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }

    }

    ///E:/swf/ <summary>
    ///E:/swf/ 数据表中的字段属性，包括字段名，字段值。
    ///E:/swf/ 常用于保存要提交的数据。
    ///E:/swf/ </summary>
    public class DbKeyItem
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_fieldName">字段名称。</param>
        ///E:/swf/ <param name="_fieldValue">字段值。</param>
        public DbKeyItem(string _fieldName, object _fieldValue)
        {
            this.fieldName = _fieldName;
            this.fieldValue = _fieldValue.ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 字段名称。
        ///E:/swf/ </summary>
        public string fieldName;
        ///E:/swf/ <summary>
        ///E:/swf/ 字段值。
        ///E:/swf/ </summary>
        public string fieldValue;
    }
}

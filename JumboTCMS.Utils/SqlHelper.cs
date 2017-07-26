﻿/*
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.IO;
using System.Net;

namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ sql语句生成代码
    ///E:/swf/ </summary>
    public class SqlHelper
    {
        #region sql语句生成代码
        ///E:/swf/ <summary>
        ///E:/swf/ 分页sql语句，支持多个字段
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="SelectFields">输出字段，以逗号隔开（记住：一定要将排序所用到的字段一并输出）</param>
        ///E:/swf/ <param name="TblName">表名</param>
        ///E:/swf/ <param name="TotalCount">总记录数</param>
        ///E:/swf/ <param name="PageSize">每页记录数</param>
        ///E:/swf/ <param name="PageIndex">页码</param>
        ///E:/swf/ <param name="Order">排序方式</param>
        ///E:/swf/ <param name="whereStr">搜索条件</param>
        ///E:/swf/ <returns></returns>
        public static string GetSql1(string SelectFields, string TblName, int TotalCount, int PageSize, int PageIndex, NameValueCollection Order, string whereStr)
        {
            //排序字段 
            string orderList = "";//用户期望的排序 
            string orderList2 = "";//对用户期望的排序的反排序 
            string orderList3 = "";//用户期望的排序,去掉了前缀.复合查询里的外层的排序不能是类似这样的table1.id,要去掉table1.。 
            if (Order.Count > 0)
            {
                string[] str = Order.AllKeys;
                foreach (string s in str)
                {
                    string direction = "asc";//默认一个方向 
                    if (Order[s].ToString() == "asc")
                        direction = "desc";
                    //if (Order[s].ToString() == "") direction = "";
                    //去掉前缀的字段名称 
                    string s2 = "";
                    int index = s.IndexOf(".") + 1;
                    s2 = s.Substring(index);
                    orderList = orderList + s + " " + Order[s] + ",";
                    orderList2 = orderList2 + s2 + " " + direction + ",";
                    orderList3 = orderList3 + s2 + " " + Order[s] + ",";
                }
                //去掉最后的,号 
                orderList = orderList.Substring(0, orderList.Length - 1);
                orderList2 = orderList2.Substring(0, orderList2.Length - 1);
                orderList3 = orderList3.Substring(0, orderList3.Length - 1);
            }
            //形成SQL 
            string strTemp;
            //判断是不是最后一页
            if ((TotalCount > 0) && (TotalCount % PageSize > 0) && (PageIndex > (TotalCount / PageSize)))
            {
                strTemp = "select * from ( select top {5} {0} from {1} ";
                if (whereStr != "")
                    strTemp = strTemp + " where {2} ";
                if (orderList != "")
                    strTemp = strTemp + " order by {4})  as tmp order by {3}";
                strTemp = string.Format(strTemp, SelectFields, TblName, whereStr, orderList, orderList2, TotalCount % PageSize);
            }
            else
            {
                strTemp = "select * from ( select top {7} * from ( select top {6} {0} from {1} ";
                if (whereStr != "")
                    strTemp = strTemp + " where {2} ";
                if (orderList != "")
                    strTemp = strTemp + " order by {3} ) as tmp order by {4} ) as tmp2 order by {5} ";
                strTemp = string.Format(strTemp, SelectFields, TblName, whereStr, orderList, orderList2, orderList3, PageIndex * PageSize, PageSize);
            }

            return strTemp;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 分页sql语句生成代码
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="SelectFields"></param>
        ///E:/swf/ <param name="TblName"></param>
        ///E:/swf/ <param name="FldName">排序字段,唯一性</param>
        ///E:/swf/ <param name="PageSize"></param>
        ///E:/swf/ <param name="PageIndex"></param>
        ///E:/swf/ <param name="OrderType"></param>
        ///E:/swf/ <param name="whereStr"></param>
        ///E:/swf/ <returns></returns>

        public static string GetSql0(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
        {
            string StrTemp = "";
            string StrSql = "";
            string StrOrder = "";
            //根据排序方式生成相关代码
            if (OrderType.ToUpper() == "ASC")
            {
                StrTemp = "> (SELECT MAX(" + FldName + ")";
                StrOrder = " ORDER BY " + FldName + " ASC";
            }
            else
            {
                StrTemp = "< (SELECT MIN(" + FldName + ")";
                StrOrder = " ORDER BY " + FldName + " DESC";
            }
            PageIndex = JumboTCMS.Utils.Validator.StrToInt(PageIndex.ToString(), 0);
            PageIndex = PageIndex == 0 ? 1 : PageIndex;
            //若是第1页则无须复杂的语句
            if (PageIndex == 1)
            {
                StrTemp = "";
                if (whereStr != "")
                    StrTemp = " Where " + whereStr;
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " From " + TblName + "" + StrTemp + StrOrder;
            }
            else
            {
                //若不是第1页，构造sql语句
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " From " + TblName + " WHERE " + FldName + "" + StrTemp + " From (SELECT TOP " + (PageIndex - 1) * PageSize + " " + FldName + " From " + TblName + "";
                if (whereStr != "")
                    StrSql += " Where " + whereStr;
                StrSql += StrOrder + ") As Tbltemp)";
                if (whereStr != "")
                    StrSql += " And " + whereStr;
                StrSql += StrOrder;
            }
            //返回sql语句
            return StrSql;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 连表分页sql语句生成代码
        ///E:/swf/ <param name="SelectFields">连表查询的字段</param>
        ///E:/swf/ <param name="TblNameA">表1</param>
        ///E:/swf/ <param name="TblNameB">表2</param>
        ///E:/swf/ <param name="FldName">表1的排序字段名</param>
        ///E:/swf/ <param name="PageSize">每页记录数</param>
        ///E:/swf/ <param name="PageIndex">当前页</param>
        ///E:/swf/ <param name="OrderType">排序方式：DESC ASC</param>
        ///E:/swf/ <param name="joinStr">连接条件</param>
        ///E:/swf/ <param name="whereStr1">外围条件带A.</param>
        ///E:/swf/ <param name="whereStr2">分页条件:不带A.</param>
        ///E:/swf/ </summary>

        public static string GetSql0(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2)
        {
            string StrTemp = "";
            string StrSql = "";
            string StrOrder1 = "";
            string StrOrder2 = "";
            //根据排序方式生成相关代码
            if (OrderType.ToUpper() == "ASC")
            {
                StrTemp = "> (SELECT MAX(" + FldName + ")";
                StrOrder1 = " ORDER BY A." + FldName + " ASC";
                StrOrder2 = " ORDER BY " + FldName + " ASC";
            }
            else
            {
                StrTemp = "< (SELECT MIN(" + FldName + ")";
                StrOrder1 = " ORDER BY A." + FldName + " DESC";
                StrOrder2 = " ORDER BY " + FldName + " DESC";
            }
            PageIndex = JumboTCMS.Utils.Validator.StrToInt(PageIndex.ToString(), 0);
            PageIndex = PageIndex == 0 ? 1 : PageIndex;
            //若是第1页则无须复杂的语句
            if (PageIndex == 1)
            {
                StrTemp = "";
                if (whereStr1 != "")
                    StrTemp = " WHERE " + whereStr1;
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A LEFT JOIN [" + TblNameB + "] B on " + joinStr + " " + StrTemp + StrOrder1;
            }
            else
            {
                //若不是第1页，构造sql语句
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A LEFT JOIN [" + TblNameB + "] B on " + joinStr + " WHERE A." + FldName + "" + StrTemp + " From (SELECT TOP " + (PageIndex - 1) * PageSize + " " + FldName + " From [" + TblNameA + "] ";
                if (whereStr2 != "")
                    StrSql += " Where " + whereStr2;
                StrSql += StrOrder2 + ") As Tbltemp)";
                if (whereStr1 != "")
                    StrSql += " And " + whereStr1;
                StrSql += StrOrder1;
            }
            //返回sql语句
            return StrSql;
        }
        #endregion
    }
}
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
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
namespace JumboTCMS.Utils
{
    public static class csvHelper
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 导出报表文件为csv格式
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="dt">DataTable</param>
        ///E:/swf/ <param name="strFilePath">物理路径</param>
        ///E:/swf/ <param name="tableheader">表头</param>
        ///E:/swf/ <param name="columname">字段标题，如：用户ID,用户名称,用户密码</param>
        ///E:/swf/ <returns></returns>
        public static bool dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string  strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);//声明写入流对象
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine +=  dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 将CSV读入DataTable
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="filePath">csv文件路径</param>
        ///E:/swf/ <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        ///E:/swf/ <param name="dt"></param>
        ///E:/swf/ <returns></returns>
        public static DataTable csv2dt(string filePath, int n, DataTable dt)
        {
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}

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
using System.Web.UI;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    public class Module_ArticleCollFilterDAL : Common
    {
        public Module_ArticleCollFilterDAL()
        {
            base.SetupSystemDate();
        }
        public string FilterBody(string PageBody, DataTable dtCollItemFilters)
        {
            for (int i = 0; i < dtCollItemFilters.Rows.Count; i++)
            {
                string fId = dtCollItemFilters.Rows[i]["Id"].ToString();
                string fType = dtCollItemFilters.Rows[i]["Filter_Type"].ToString();
                string fContent = dtCollItemFilters.Rows[i]["Filter_Content"].ToString();
                string fsString = dtCollItemFilters.Rows[i]["FisString"].ToString();
                string foString = dtCollItemFilters.Rows[i]["FioString"].ToString();
                string fReplace = dtCollItemFilters.Rows[i]["Filter_Rep"].ToString();
                if (fType == "0")//简单替换
                    PageBody = PageBody.Replace(fContent, fReplace);
                else
                {
                    JumboTCMS.Common.NewsCollection nc = new JumboTCMS.Common.NewsCollection();
                    System.Collections.ArrayList replaceArray = nc.GetArray(PageBody, fsString, foString);
                    if (replaceArray.Count == 0)
                        continue;
                    if (replaceArray[0].ToString() == "$StartFalse$" || replaceArray[0].ToString() == "$EndFalse$" || replaceArray[0].ToString() == "$NoneBody$")
                        continue;
                    for (int j = 0; j < replaceArray.Count; j++)
                    {
                        PageBody = PageBody.Replace(replaceArray[j].ToString(), fReplace);
                    }
                }
            }
            return PageBody;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 过滤内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="doh"></param>
        ///E:/swf/ <param name="PageBody">原文内容</param>
        ///E:/swf/ <param name="ItemId">项目ID</param>
        ///E:/swf/ <param name="FilterObject">Title或Body</param>
        ///E:/swf/ <returns></returns>
        public string FilterBody(string PageBody, string ItemId, string FilterObject)
        {
            DataTable dtCollItemFilters = GetFilterDT(ItemId, FilterObject);
            PageBody = FilterBody(PageBody, dtCollItemFilters);
            dtCollItemFilters.Clear();
            dtCollItemFilters.Dispose();
            return PageBody;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 或得过滤规则表
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ItemId">项目ID</param>
        ///E:/swf/ <param name="FilterObject">title表示标题，body表示内容</param>
        ///E:/swf/ <returns></returns>
        public DataTable GetFilterDT(string ItemId, string FilterObject)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string sqlStr = "Select * FROM [jcms_module_article_collfilters] WHERE ([ItemId]=" + ItemId + " OR [PublicTf]=1)";
                if (FilterObject.ToLower() == "title")
                    sqlStr += " AND [Filter_Object]=0";
                else
                    sqlStr += " AND [Filter_Object]=1";
                sqlStr += " AND [Flag]=1 ORDER BY Id";
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                return _doh.GetDataTable();
            }
        }
    }
}

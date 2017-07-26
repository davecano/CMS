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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 友情链接插件
    ///E:/swf/ </summary>
    public class Extends_QQOnlineDAL : Common
    {
        public Extends_QQOnlineDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到列表
        ///E:/swf/ </summary>
        public List<Extends_QQOnline> QQOnlineList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                List<Extends_QQOnline> qqonlines;
                qqonlines = new List<Extends_QQOnline>();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_extends_qqonline] Where State=1 ORDER BY OrderNum Desc,Id Desc";
                DataTable dtQQOnline = _doh.GetDataTable();
                if (dtQQOnline.Rows.Count > 0)
                {
                    for (int i = 0; i < dtQQOnline.Rows.Count; i++)
                    {
                        qqonlines.Add(new Extends_QQOnline(dtQQOnline.Rows[i]["Id"].ToString(),
                            dtQQOnline.Rows[i]["QQID"].ToString(),
                            dtQQOnline.Rows[i]["Title"].ToString(),
                            dtQQOnline.Rows[i]["TColor"].ToString(),
                            dtQQOnline.Rows[i]["face"].ToString()
                            ));
                    }
                }
                dtQQOnline.Clear();
                dtQQOnline.Dispose();
                return qqonlines;
            }
        }
    }
}

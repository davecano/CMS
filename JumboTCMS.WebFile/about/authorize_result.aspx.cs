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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.About
{
    public partial class _authorize_result : JumboTCMS.UI.FrontHtml
    {
        public string Domain, WebName,AccreditType, AccreditTypeName, AddTime, UseInBusiness, DeleteCopyright, Validity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _domain = JumboTCMS.Utils.Strings.FilterSymbol(q("domain"));
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.[AccreditType]=B.Id";
            string whereStr1 = "A.[State]=1 and A.[Domain]='" + _domain + "'";
            string whereStr2 = "[State]=1 and [Domain]='" + _domain + "'";
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_official_authorization");
            if (totalCount == 0)
            {
                Response.Write("<script>alert('此域名未得到官方授权');window.close();</script>");
                Response.End();
            }
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("a.*,b.Title as AccreditTypeName,b.UseInBusiness,b.DeleteCopyright", "jcms_official_authorization", "jcms_official_authorization_type", "Id", 1, 1, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            Domain = dt.Rows[0]["Domain"].ToString();
            WebName = dt.Rows[0]["WebName"].ToString();
            AccreditType = dt.Rows[0]["AccreditType"].ToString();
            AccreditTypeName = dt.Rows[0]["AccreditTypeName"].ToString();
            UseInBusiness = dt.Rows[0]["UseInBusiness"].ToString();
            DeleteCopyright = dt.Rows[0]["DeleteCopyright"].ToString();
            AddTime = Convert.ToDateTime(dt.Rows[0]["AddTime"].ToString()).ToString("yyyy-MM-dd");
            Validity = Convert.ToDateTime(dt.Rows[0]["Validity"].ToString()).ToString("yyyy-MM-dd");

            dt.Clear();
            dt.Dispose();
        }
    }
}

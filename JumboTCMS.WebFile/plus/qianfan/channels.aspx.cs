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
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Plus.QianFan
{
    public partial class _channels : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Unload(object sender, EventArgs e)
        {
            SavePageLog(1);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            Response.Charset = "utf-8";
            Response.ContentType = "text/xml";
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            StringBuilder strCode = new StringBuilder();
            strCode.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            strCode.Append("<cms>");
            strCode.Append("<channels>");
            doh.Reset();
            doh.SqlCmd = "Select [Id],[Title] FROM [jcms_normal_channel] WHERE [CanCollect]=1 AND [ClassDepth]>0 ORDER BY pid";
            DataTable dt = doh.GetDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string channelId = dt.Rows[i]["Id"].ToString();
                string channelName = dt.Rows[i]["Title"].ToString();
                strCode.Append("<channel name=\"" + channelName + "\">");
                strCode.Append("<value>#</value>");
                strCode.Append(GetChildChannel(channelId, "0"));
                strCode.Append("</channel>");
            }

            strCode.Append("</channels>\r\n");
            strCode.Append("</cms>\r\n");
            dt.Clear();
            dt.Dispose();
            Response.Write(strCode.ToString());
        }
        private string GetChildChannel(string _channelid, string _parentid)
        {
            StringBuilder strCode = new StringBuilder();
            doh.Reset();
            doh.SqlCmd = "Select a.[Id],a.[Title],(select count(id) FROM [jcms_normal_class] WHERE parentid=a.id) as childcount FROM jcms_normal_class a WHERE a.[IsOut]=0 AND a.[ChannelID]=" + _channelid + " AND a.[ParentID]=" + _parentid + " ORDER BY a.CODE";
            DataTable dt = doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string classId = dt.Rows[i]["Id"].ToString();
                string className = dt.Rows[i]["Title"].ToString();
                int childcount = Str2Int(dt.Rows[i]["childcount"].ToString());
                if (childcount > 0)
                {
                    strCode.Append("<channel name=\"" + className + "\">");
                    strCode.Append("<value>ChannelId=" + classId + "</value>");
                    strCode.Append(GetChildChannel(_channelid, classId));
                    strCode.Append("</channel>");
                }
                else
                {
                    strCode.Append("<channel name=\"" + className + "\">ChannelId=" + classId + "</channel>");
                }
            }
            dt.Clear();
            dt.Dispose();
            return strCode.ToString();
        }
    }
}

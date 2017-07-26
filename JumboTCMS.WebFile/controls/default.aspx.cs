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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Controls
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();
            string ChannelId = (this.lblChannelId.Text == "{$ChannelId}") ? Str2Str(q("ChannelId")) : Str2Str(this.lblChannelId.Text);
            string TxtStr = string.Empty;
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
            TxtStr = teDAL.GetSiteChannelPage(CurrentPage);
            teDAL.ReplaceSHTMLTag(ref TxtStr);
            teDAL.ReplaceUserTag(ref TxtStr);
            Response.Write(TxtStr);//直接输出
        }
    }
}

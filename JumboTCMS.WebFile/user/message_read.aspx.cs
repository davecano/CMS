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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _message_read : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            id = Str2Str(q("id"));
            //标识为已读
            doh.Reset();
            doh.ConditionExpress = "id=@id and [State]=0 and ReceiveUserId=" + UserId;
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("ReadTime", System.DateTime.Now.ToString());
            doh.Update("jcms_normal_user_message");
        }
    }
}

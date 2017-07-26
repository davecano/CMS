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
namespace JumboTCMS.UI
{
    public class FrontAjax : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!CheckFormUrl())//不可直接在url下访问
            {
                Response.End();
            }
            CheckClientIP();
        }
    }
}

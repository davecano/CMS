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
    public interface IModule
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容页地址
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <param name="_ishtml"></param>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <returns></returns>
        string GetContentLink(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile);
        ///E:/swf/ <summary>
        ///E:/swf/ 生成内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        ///E:/swf/ <param name="_CurrentPage"></param>
        void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage);
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        ///E:/swf/ <param name="_CurrentPage"></param>
        string GetContent(string _ChannelId, string _ContentId, int _CurrentPage);
        ///E:/swf/ <summary>
        ///E:/swf/ 删除内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        void DeleteContent(string _ChannelId, string _ContentId);
    }
}

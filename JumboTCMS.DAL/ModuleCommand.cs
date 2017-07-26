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
namespace JumboTCMS.DAL
{
    public class ModuleCommand
    {
        public static IModule IMD;
        static ModuleCommand()
        {

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容页地址
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <param name="_ishtml"></param>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <returns></returns>
        public static string GetContentLink(string _module, int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            return IMD.GetContentLink(_page, _ishtml, _channelid, _contentid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        ///E:/swf/ <param name="_CurrentPage"></param>
        public static void CreateContent(string _module, string _ChannelId, string _ContentId, int _CurrentPage)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            IMD.CreateContent(_ChannelId, _ContentId, _CurrentPage);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        ///E:/swf/ <param name="_CurrentPage"></param>
        public static string GetContent(string _module, string _ChannelId, string _ContentId, int _CurrentPage)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            return IMD.GetContent(_ChannelId, _ContentId, _CurrentPage);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        public static void DeleteContent(string _module, string _ChannelId, string _ContentId)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            IMD.DeleteContent(_ChannelId, _ContentId);
        }
    }
}

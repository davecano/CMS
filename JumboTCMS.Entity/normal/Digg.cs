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
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 顶客-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Digg
    {
        public Normal_Digg()
        { }

        private string _id;
        private int _contentid;
        private string _channeltype;
        private int _diggnum;
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ChannelType
        {
            set { _channeltype = value; }
            get { return _channeltype; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int DiggNum
        {
            set { _diggnum = value; }
            get { return _diggnum; }
        }


    }
}


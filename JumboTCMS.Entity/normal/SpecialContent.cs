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
    ///E:/swf/ 专题内容-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_SpecialContent
    {
        public Normal_SpecialContent()
        { }

        private string _id;
        private string _title;
        private int _sid;
        private int _channelid;
        private int _contentid;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 内容标题
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 所属专题ID
        ///E:/swf/ </summary>
        public int sId
        {
            set { _sid = value; }
            get { return _sid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 所属频道ID
        ///E:/swf/ </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 指向内容的ID
        ///E:/swf/ </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }


    }
}


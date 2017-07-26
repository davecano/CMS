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
using System.Data;
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 标签-------表映射实体
    ///E:/swf/ </summary>
    public class Normal_Tags
    {
        public Normal_Tags()
        { }
        public List<Normal_Tag> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Normal_Tag>(_dt);
        }
    }
    public class Normal_Tag
    {
        public Normal_Tag()
        { }

        private string _id;
        private int _channelid;
        private string _title;
        private int _clicktimes;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道ID
        ///E:/swf/ </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 标签名
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 点击数
        ///E:/swf/ </summary>
        public int ClickTimes
        {
            set { _clicktimes = value; }
            get { return _clicktimes; }
        }


    }
}


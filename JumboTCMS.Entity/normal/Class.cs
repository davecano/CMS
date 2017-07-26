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
    ///E:/swf/ 栏目-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Class
    {
        public Normal_Class()
        { }

        private string _id;
        private int _channelid;
        private int _parentid;
        private string _title;
        private string _info;
        private string _img;
        private string _keywords;
        private string _content;
        private string _filepath;
        private string _code;
        private bool _ispost;
        private bool _istop;
        private int _topicnum;
        private string _themeid;
        private string _contenttemp;
        private int _pagesize;
        private bool _isout;
        private string _firstpage;
        private string _aliaspage;
        private int _readgroup;
        private int _islastclass;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
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
        ///E:/swf/ 父级ID
        ///E:/swf/ </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目名称
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 封面图
        ///E:/swf/ </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目简介
        ///E:/swf/ </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目关键词
        ///E:/swf/ </summary>
        public string Keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目详情
        ///E:/swf/ </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 栏目目录
        ///E:/swf/ </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目代码，用其来关联父子和兄弟关系
        ///E:/swf/ </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否会员可投稿
        ///E:/swf/ </summary>
        public bool IsPost
        {
            set { _ispost = value; }
            get { return _ispost; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否导航
        ///E:/swf/ </summary>
        public bool IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int TopicNum
        {
            set { _topicnum = value; }
            get { return _topicnum; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 模板ID
        ///E:/swf/ </summary>
        public string ThemeId
        {
            set { _themeid = value; }
            get { return _themeid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 内容页模板ID
        ///E:/swf/ </summary>
        public string ContentTheme
        {
            set { _contenttemp = value; }
            get { return _contenttemp; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 每页记录数
        ///E:/swf/ </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 外部栏目
        ///E:/swf/ </summary>
        public bool IsOut
        {
            set { _isout = value; }
            get { return _isout; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 外部链接地址
        ///E:/swf/ </summary>
        public string FirstPage
        {
            set { _firstpage = value; }
            get { return _firstpage; }
        }
        public string AliasPage
        {
            set { _aliaspage = value; }
            get { return _aliaspage; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最低阅读会员组
        ///E:/swf/ </summary>
        public int ReadGroup
        {
            set { _readgroup = value; }
            get { return _readgroup; }
        }
        public int IsLastClass
        {
            set { _islastclass = value; }
            get { return _islastclass; }
        }

    }
}


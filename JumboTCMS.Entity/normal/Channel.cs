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
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 频道-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Channel
    {
        public Normal_Channel()
        { }

        private string _id = "0";
        private string _title = string.Empty;
        private string _info = string.Empty;
        private int _classdepth = 0;
        private string _dir = string.Empty;
        private string _subdomain = string.Empty;
        private int _pid = 0;
        private string _itemname = string.Empty;
        private string _itemunit = string.Empty;
        private int _themeid = 0;
        private string _type = "system";
        private bool _enabled = false;
        private bool _checksametitle = false;
        private int _defaultthumbs = 0;
        private bool _ispost = false;
        private bool _ishtml = false;
        private bool _istop = false;
        private int _pagesize;
        private string _uploadpath;
        private string _uploadtype;
        private int _uploadsize;
        private string _languagecode;
        private bool _cancollect;

        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道名称
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道简介
        ///E:/swf/ </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目深度
        ///E:/swf/ </summary>
        public int ClassDepth
        {
            set { _classdepth = value; }
            get { return _classdepth; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 路径
        ///E:/swf/ </summary>
        public string Dir
        {
            set { _dir = value; }
            get { return _dir; }
        }
        public string SubDomain
        {
            set { _subdomain = value; }
            get { return _subdomain; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 权值
        ///E:/swf/ </summary>
        public int pId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 项目名称
        ///E:/swf/ </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 项目单位
        ///E:/swf/ </summary>
        public string ItemUnit
        {
            set { _itemunit = value; }
            get { return _itemunit; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 模板ID
        ///E:/swf/ </summary>
        public int ThemeId
        {
            set { _themeid = value; }
            get { return _themeid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 模板模型：article/soft/photo/video，system表示外部频道
        ///E:/swf/ </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否启用
        ///E:/swf/ </summary>
        public bool Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 限制重复标题
        ///E:/swf/ </summary>
        public bool CheckSameTitle
        {
            set { _checksametitle = value; }
            get { return _checksametitle; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 默认缩略图Id
        ///E:/swf/ </summary>
        public int DefaultThumbs
        {
            set { _defaultthumbs = value; }
            get { return _defaultthumbs; }
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
        ///E:/swf/ 每页记录数
        ///E:/swf/ </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否静态
        ///E:/swf/ </summary>
        public bool IsHtml
        {
            set { _ishtml = value; }
            get { return _ishtml; }
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
        ///E:/swf/ 附件存放目录(已经过滤标签)
        ///E:/swf/ </summary>
        public string UploadPath
        {
            set { _uploadpath = value; }
            get { return _uploadpath; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 附件上传类型
        ///E:/swf/ </summary>
        public string UploadType
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 附件大小限制
        ///E:/swf/ </summary>
        public int UploadSize
        {
            set { _uploadsize = value; }
            get { return _uploadsize; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 语言包
        ///E:/swf/ </summary>
        public string LanguageCode
        {
            set { _languagecode = value; }
            get { return _languagecode; }
        }
        public bool CanCollect
        {
            set { _cancollect = value; }
            get { return _cancollect; }
        }
    }
}


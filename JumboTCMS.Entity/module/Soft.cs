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
    ///E:/swf/ 软件实体列表
    ///E:/swf/ </summary>
    public class Module_Softs
    {
        public Module_Softs()
        { }
        public List<Module_Soft> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Module_Soft>(_dt);
        }
    }
    ///E:/swf/ <summary>
    ///E:/swf/ 软件-------表映射实体
    ///E:/swf/ </summary>

    public class Module_Soft
    {
        public Module_Soft()
        { }

        private string _id;
        private string _channelid;
        private string _channelishtml = "0";
        private string _classid;
        private string _title;
        private string _tcolor;
        private DateTime _adddate;
        private string _summary;
        private string _editor;
        private string _author;
        private string _tags;
        private int _viewnum;
        private int _ispass;
        private int _isimg;
        private string _img;
        private int _istop;
        private int _isfocus;
        private int _ishead;
        private int _userid;
        private int _readgroup;
        private string _sourcefrom;
        private string _version;
        private string _operatingsystem;
        private string _unzippass;
        private string _demourl;
        private string _regurl;
        private string _ssize;
        private int _points;
        private string _softurl;
        private int _downnum;
        private string _firstpage;
        private string _aliaspage;
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
        public string ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        public string ChannelIsHtml
        {
            set { _channelishtml = value; }
            get { return _channelishtml; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string TColor
        {
            set { _tcolor = value; }
            get { return _tcolor; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Author
        {
            set { _author = value; }
            get { return _author; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ViewNum
        {
            set { _viewnum = value; }
            get { return _viewnum; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int IsImg
        {
            set { _isimg = value; }
            get { return _isimg; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否推荐
        ///E:/swf/ </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否焦点
        ///E:/swf/ </summary>
        public int IsFocus
        {
            set { _isfocus = value; }
            get { return _isfocus; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否置顶
        ///E:/swf/ </summary>
        public int IsHead
        {
            set { _ishead = value; }
            get { return _ishead; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ReadGroup
        {
            set { _readgroup = value; }
            get { return _readgroup; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SourceFrom
        {
            set { _sourcefrom = value; }
            get { return _sourcefrom; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string OperatingSystem
        {
            set { _operatingsystem = value; }
            get { return _operatingsystem; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string UnZipPass
        {
            set { _unzippass = value; }
            get { return _unzippass; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string DemoUrl
        {
            set { _demourl = value; }
            get { return _demourl; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string RegUrl
        {
            set { _regurl = value; }
            get { return _regurl; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SSize
        {
            set { _ssize = value; }
            get { return _ssize; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SoftUrl
        {
            set { _softurl = value; }
            get { return _softurl; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int DownNum
        {
            set { _downnum = value; }
            get { return _downnum; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
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
    }
}


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
    ///E:/swf/ 模型-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Modules
    {
        public Normal_Modules()
        { }

        private string _id;
        private string _title;
        private string _type;
        private int _pid;
        private int _enabled;
        private int _locked;
        private string _searchfieldvalues;
        private string _searchfieldtexts;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Locked
        {
            set { _locked = value; }
            get { return _locked; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SearchFieldValues
        {
            set { _searchfieldvalues = value; }
            get { return _searchfieldvalues; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string SearchFieldTexts
        {
            set { _searchfieldtexts = value; }
            get { return _searchfieldtexts; }
        }


    }
}


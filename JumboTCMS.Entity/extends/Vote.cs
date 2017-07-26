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
    ///E:/swf/ 投票-------表映射实体
    ///E:/swf/ </summary>
    public class Extends_VoteItem
    {
        private string _itemtext;
        private int _itemclicks;
        public Extends_VoteItem()
        { }
        public Extends_VoteItem(string itemtext, int itemclicks)
        {
            this._itemtext = itemtext;
            this._itemclicks = itemclicks;
        }
        public string ItemText
        {
            set { _itemtext = value; }
            get { return _itemtext; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ItemClicks
        {
            set { _itemclicks = value; }
            get { return _itemclicks; }
        }
    }
    public class Extends_Votes
    {
        public Extends_Votes()
        { }
        public List<Extends_Vote> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Extends_Vote>(_dt);
        }
    }
    public class Extends_Vote
    {
        public Extends_Vote()
        { }
        private string _id;
        private string _title;
        private List<Extends_VoteItem> _item;
        private int _votetotal;
        private int _type;
        private int _lock;
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
        public List<Extends_VoteItem> Item
        {
            set { _item = value; }
            get { return _item; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int VoteTotal
        {
            set { _votetotal = value; }
            get { return _votetotal; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Lock
        {
            set { _lock = value; }
            get { return _lock; }
        }
    }
}


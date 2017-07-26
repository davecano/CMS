using System;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.API.Discuz.Toolkit
{
	///E:/swf/ <summary>
	///E:/swf/ 附件信息描述类
	///E:/swf/ </summary>
	public class AttachmentInfo
	{
		
		private int m_aid;	//附件aid
		private int m_uid;	//对应的帖子书posterid
		private int m_tid;	//对应的主题tid
		private int m_pid;	//对应的帖子pid
		private string m_postdatetime;	//发布时间
		private int m_readperm;	//所需阅读权限
		private string m_filename;	//存储文件名
		private string m_description;	//描述
		private string m_filetype;	//文件类型
		private long m_filesize;	//文件尺寸
		private string m_attachment;	//附件原始文件名
		private int m_downloads;	//下载次数
        private int m_attachprice;    //附件的售价

		private int m_sys_index;  //非数据库字段,用来代替上传文件所对应上传组件(file)的Index
		private string m_sys_noupload; //非数据库字段,用来存放未被上传的文件名

        private int m_getattachperm; //下载附件权限
        private int m_attachimgpost; //附件是否为图片
        private int m_allowread; //附件是否允许读取
        private string m_preview = string.Empty; //预览信息
        private int m_isbought = 0;//附件是否被买卖
        private int m_inserted = 0; //是否已插入到内容
        ///E:/swf/ <summary>
        ///E:/swf/ 下载附件权限
        ///E:/swf/ </summary>
        [JsonProperty("download_perm")]
        [XmlElement("download_perm")]
        public int Getattachperm
        {
            get { return m_getattachperm; }
            set { m_getattachperm = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 附件是否为图片
        ///E:/swf/ </summary>
        [JsonProperty("is_image")]
        [XmlElement("is_image")]
        public int Attachimgpost
        {
            get { return m_attachimgpost; }
            set { m_attachimgpost = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 附件是否允许读取
        ///E:/swf/ </summary>
        [JsonProperty("allow_read")]
        [XmlElement("allow_read")]
        public int Allowread
        {
            get { return m_allowread; }
            set { m_allowread = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 预览信息
        ///E:/swf/ </summary>
        [JsonProperty("preview")]
        [XmlElement("preview")]
        public string Preview
        {
            get { return m_preview; }
            set { m_preview = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 附件是否被买卖
        ///E:/swf/ </summary>
        [JsonProperty("is_bought")]
        [XmlElement("is_bought")]
        public int Isbought
        {
            get { return m_isbought; }
            set { m_isbought = value; }
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 是否已插入到内容
        ///E:/swf/ </summary>
        [JsonProperty("inserted")]
        [XmlElement("inserted")]
        public int Inserted
        {
            get { return m_inserted; }
            set { m_inserted = value; }
        }

		///E:/swf/<summary>
		///E:/swf/附件aid
		///E:/swf/</summary>
        [JsonProperty("aid")]
        [XmlElement("aid")]
		public int Aid
		{
			get { return m_aid;}
			set { m_aid = value;}
		}
		///E:/swf/<summary>
		///E:/swf/对应的帖子posterid
		///E:/swf/</summary>
        [JsonProperty("uid")]
        [XmlElement("uid")]
        public int Uid
		{
			get { return m_uid;}
			set { m_uid = value;}
		}
		///E:/swf/<summary>
		///E:/swf/对应的主题tid
		///E:/swf/</summary>
        [JsonProperty("tid")]
        [XmlElement("tid")]
        public int Tid
		{
			get { return m_tid;}
			set { m_tid = value;}
		}
		///E:/swf/<summary>
		///E:/swf/对应的帖子pid
		///E:/swf/</summary>
        [JsonProperty("pid")]
        [XmlElement("pid")]
        public int Pid
		{
			get { return m_pid;}
			set { m_pid = value;}
		}
		///E:/swf/<summary>
		///E:/swf/发布时间
		///E:/swf/</summary>
        [JsonProperty("post_date_time")]
        [XmlElement("post_date_time")]
        public string Postdatetime
		{
			get { return m_postdatetime;}
			set { m_postdatetime = value;}
		}
		///E:/swf/<summary>
		///E:/swf/所需阅读权限
		///E:/swf/</summary>
        [JsonProperty("read_perm")]
        [XmlElement("read_perm")]
        public int Readperm
		{
			get { return m_readperm;}
			set { m_readperm = value;}
		}
		///E:/swf/<summary>
		///E:/swf/存储文件名
		///E:/swf/</summary>
        [JsonProperty("file_name")]
        [XmlElement("file_name")]
        public string Filename
		{
			get { return m_filename;}
			set { m_filename = value;}
		}
		///E:/swf/<summary>
		///E:/swf/描述
		///E:/swf/</summary>
        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description
		{
			get { return m_description;}
			set { m_description = value;}
		}
		///E:/swf/<summary>
		///E:/swf/文件类型
		///E:/swf/</summary>
        [JsonProperty("file_type")]
        [XmlElement("file_type")]
        public string Filetype
		{
			get { return m_filetype;}
			set { m_filetype = value;}
		}
		///E:/swf/<summary>
		///E:/swf/文件尺寸
		///E:/swf/</summary>
        [JsonProperty("file_size")]
        [XmlElement("file_size")]
        public long Filesize
		{
			get { return m_filesize;}
			set { m_filesize = value;}
		}
		///E:/swf/<summary>
		///E:/swf/附件原始文件名
		///E:/swf/</summary>
        [JsonProperty("original_file_name")]
        [XmlElement("original_file_name")]
        public string Attachment
		{
			get { return m_attachment;}
			set { m_attachment = value;}
		}
		///E:/swf/<summary>
		///E:/swf/下载次数
		///E:/swf/</summary>
        [JsonProperty("download_count")]
        [XmlElement("download_count")]
        public int Downloads
		{
			get { return m_downloads;}
			set { m_downloads = value;}
		}

        ///E:/swf/ <summary>
        ///E:/swf/ 附件的售价
        ///E:/swf/ </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public int Attachprice    
        {
			get { return m_attachprice;}
            set { m_attachprice = value; }
		}
        

		///E:/swf/<summary>
		///E:/swf/非数据库字段,用来代替上传文件所对应上传组件(file)的Index
		///E:/swf/</summary>
        [JsonIgnore]
        [XmlIgnore]
        public int Sys_index
		{
			get { return m_sys_index;}
			set { m_sys_index = value;}
		}

		///E:/swf/<summary>
		///E:/swf/非数据库字段,用来存放未被上传的文件名
		///E:/swf/</summary>
        [JsonIgnore]
        [XmlIgnore]
        public string Sys_noupload
		{
			get { return m_sys_noupload;}
			set { m_sys_noupload = value;}
		}

	}
}

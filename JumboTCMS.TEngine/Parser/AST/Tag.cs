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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace JumboTCMS.TEngine.Parser.AST
{


	public class Tag : Element
	{
		string name;
		List<TagAttribute> attribs;
		List<Element> innerElements;
		TagClose closeTag;
		bool isClosed;	// set to true if tag ends with />

		public Tag(int line, int col, string name)
			:base(line, col)
		{
			this.name = name;
			this.attribs = new List<TagAttribute>();
			this.innerElements = new List<Element>();
		}

		public List<TagAttribute> Attributes
		{
			get { return this.attribs; }
		}

		public Expression AttributeValue(string name)
		{
			foreach (TagAttribute attrib in attribs)
				if (string.Compare(attrib.Name, name, true) == 0)
					return attrib.Expression;

			return null;
		}

		public List<Element> InnerElements
		{
			get { return this.innerElements; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public TagClose CloseTag
		{
			get { return this.closeTag; }
			set { this.closeTag = value; }
		}

		public bool IsClosed
		{
			get { return this.isClosed; }
			set { this.isClosed = value; }
		}


	}
}

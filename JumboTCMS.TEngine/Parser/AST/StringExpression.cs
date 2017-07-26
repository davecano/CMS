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
	public class StringExpression : Expression
	{
		List<Expression> exps;

		public StringExpression(int line, int col)
			:base(line, col)
		{
			exps = new List<Expression>();
		}

		public int ExpCount
		{
			get { return exps.Count; }
		}

		public void Add(Expression exp)
		{
			exps.Add(exp);
		}

		public Expression this[int index]
		{
			get { return exps[index]; }
		}

		public IEnumerable<Expression> Expressions
		{
			get
			{
				for (int i = 0; i < exps.Count; i++)
					yield return exps[i];
			}
		}
	}
}

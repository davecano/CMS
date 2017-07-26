/*
 * ��������: JumboTCMS(�������ݹ���ϵͳͨ�ð�)
 * 
 * ����汾: 7.x
 * 
 * ��������: ��ľ���� (QQ��791104444@qq.com��������ҵ����)
 * 
 * ��Ȩ����: http://www.jumbotcms.net/about/copyright.html
 * 
 * ��������: http://forum.jumbotcms.net/
 * 
 */

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace JumboTCMS.TEngine.Parser.AST
{
    public class BinaryExpression : Expression
    {
        Expression lhs;
        Expression rhs;

        TokenKind op;

        public BinaryExpression(int line, int col, Expression lhs, TokenKind op, Expression rhs)
            : base(line, col)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public Expression Lhs
        {
            get { return this.lhs; }
        }

        public Expression Rhs
        {
            get { return this.rhs; }
        }

        public TokenKind Operator
        {
            get { return this.op; }
        }

    }
}
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
using System.Text;

namespace JumboTCMS.TEngine
{
    public class VariableScope
    {
        VariableScope parent;
        Dictionary<string, object> values;

        public VariableScope()
            : this(null)
        {
        }

        public VariableScope(VariableScope parent)
        {
            this.parent = parent;
            this.values = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ clear all variables from this scope
        ///E:/swf/ </summary>
        public void Clear()
        {
            values.Clear();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ gets the parent scope for this scope
        ///E:/swf/ </summary>
        public VariableScope Parent
        {
            get { return parent; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ returns true if variable name is defined
        ///E:/swf/ otherwise returns parents isDefined if parent != null
        ///E:/swf/ </summary>
        public bool IsDefined(string name)
        {
            if (values.ContainsKey(name))
                return true;
            else if (parent != null)
                return parent.IsDefined(name);
            else
                return false;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ returns value of variable name
        ///E:/swf/ If name is not in this scope and parent != null
        ///E:/swf/ parents this[name] is called
        ///E:/swf/ </summary>
        public object this[string name]
        {
            get
            {
                if (!values.ContainsKey(name))
                {
                    if (parent != null)
                        return parent[name];
                    else
                        return null;
                }
                else
                    return values[name];
            }
            set { values[name] = value; }
        }
    }
}

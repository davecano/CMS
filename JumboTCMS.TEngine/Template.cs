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

using JumboTCMS.TEngine.Parser;
using JumboTCMS.TEngine.Parser.AST;

namespace JumboTCMS.TEngine
{
    public class Template
    {
        string name;
        List<Element> elements;
        Template parent;

        Dictionary<string, Template> templates;

        public Template(string name, List<Element> elements)
        {
            this.name = name;
            this.elements = elements;
            this.parent = null;

            InitTemplates();
        }

        public Template(string name, List<Element> elements, Template parent)
        {
            this.name = name;
            this.elements = elements;
            this.parent = parent;

            InitTemplates();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ load template from file
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name">name of template</param>
        ///E:/swf/ <param name="filename">file from which to load template</param>
        ///E:/swf/ <returns></returns>
        public static Template FromFile(string name, string filename)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
            {
                string data = reader.ReadToEnd();
                return Template.FromString(name, data);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ load template from string
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name">name of template</param>
        ///E:/swf/ <param name="data">string containg code for template</param>
        ///E:/swf/ <returns></returns>
        public static Template FromString(string name, string data)
        {
            TemplateLexer lexer = new TemplateLexer(data);
            TemplateParser parser = new TemplateParser(lexer);
            List<Element> elems = parser.Parse();

            TagParser tagParser = new TagParser(elems);
            elems = tagParser.CreateHierarchy();

            return new Template(name, elems);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ go thru all tags and see if they are template tags and add
        ///E:/swf/ them to this.templates collection
        ///E:/swf/ </summary>
        private void InitTemplates()
        {
            this.templates = new Dictionary<string, Template>(StringComparer.InvariantCultureIgnoreCase);

            foreach (Element elem in elements)
            {
                if (elem is Tag)
                {
                    Tag tag = (Tag)elem;
                    if (string.Compare(tag.Name, "template", true) == 0)
                    {
                        Expression ename = tag.AttributeValue("name");
                        string tname;
                        if (ename is StringLiteral)
                            tname = ((StringLiteral)ename).Content;
                        else
                            tname = "?";

                        Template template = new Template(tname, tag.InnerElements, this);
                        templates[tname] = template;
                    }
                }
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ gets a list of elements for this template
        ///E:/swf/ </summary>
        public List<Element> Elements
        {
            get { return this.elements; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ gets the name of this template
        ///E:/swf/ </summary>
        public string Name
        {
            get { return this.name; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ returns true if this template has parent template
        ///E:/swf/ </summary>
        public bool HasParent
        {
            get { return parent != null; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ gets parent template of this template
        ///E:/swf/ </summary>
        ///E:/swf/ <value></value>
        public JumboTCMS.TEngine.Template Parent
        {
            get { return this.parent; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ finds template matching name. If this template does not
        ///E:/swf/ contain template called name, and parent != null then
        ///E:/swf/ FindTemplate is called on the parent
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="name"></param>
        ///E:/swf/ <returns></returns>
        public virtual Template FindTemplate(string name)
        {
            if (templates.ContainsKey(name))
                return templates[name];
            else if (parent != null)
                return parent.FindTemplate(name);
            else
                return null;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ gets dictionary of templates defined in this template
        ///E:/swf/ </summary>
        public System.Collections.Generic.Dictionary<string, JumboTCMS.TEngine.Template> Templates
        {
            get { return this.templates; }
        }
    }
}

using System;

namespace JumboTCMS.TEngine
{
    ///E:/swf/ <summary>
    ///E:/swf/ StaticTypeReference is used by TemplateManager to hold references to types.
    ///E:/swf/ When invoking methods, or accessing properties of this object, it will actually
    ///E:/swf/ do static methods of the type
    ///E:/swf/ </summary>
    class StaticTypeReference
    {
        readonly Type type;

        public StaticTypeReference(Type type)
        {
            this.type = type;
        }

        public Type Type
        {
            get { return type; }
        }
    }
}

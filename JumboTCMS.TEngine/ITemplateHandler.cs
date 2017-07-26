using System;
using System.Collections.Generic;
using System.Text;

namespace JumboTCMS.TEngine
{
    public interface ITemplateHandler
    {
        ///E:/swf/ <summary>
        ///E:/swf/ this method will be called before any processing
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="manager">manager doing the execution</param>
        void BeforeProcess(TemplateManager manager);

        ///E:/swf/ <summary>
        ///E:/swf/ this method will be called after all processing is done
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="manager">manager doing the execution</param>
        void AfterProcess(TemplateManager manager);
    }
}

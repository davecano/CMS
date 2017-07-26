#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

using JumboTCMS.TEngine.Parser.AST;

namespace JumboTCMS.TEngine
{
    ///E:/swf/ <summary>
    ///E:/swf/ ITagHandler is used to execute custom tags.
    ///E:/swf/ You register handler with TemplateManager with RegisterTagHandler(string tagName, ITagHandler handler) method.
    ///E:/swf/ A handler is called twice. Once before the content of the tag is executed,
    ///E:/swf/ and once after. 
    ///E:/swf/ </summary>
    public interface ITagHandler
    {
        ///E:/swf/ <summary>
        ///E:/swf/		This method is called at the beginning of processing
        ///E:/swf/	of the tag.
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="manager">manager executing the tag</param>
        ///E:/swf/ <param name="tag">tag being executed</param>
        ///E:/swf/ <param name="processInnerElements">instructs manager if it should process
        ///E:/swf/		inner elements of the tag. If this value will be set to false,
        ///E:/swf/		then manager will not execute inner content. 
        ///E:/swf/		Default value is true.
        ///E:/swf/ </param>
        ///E:/swf/ <param name="captureInnerContent">
        ///E:/swf/		instructs manager if inner content should be sent to default
        ///E:/swf/		output, or custom output. If this value is set to false, all
        ///E:/swf/		output will be sent to current writer. If set to true
        ///E:/swf/		then output will be called and passed as string to TagEndProcess.
        ///E:/swf/		Default value is false.
        ///E:/swf/ </param>
        void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent);

        ///E:/swf/ <summary>
        ///E:/swf/ this tag is called at the end of processing the content.
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="innerContent">If captureinnerContent was set true, 
        ///E:/swf/		then this is the output that was generated when inside of this tag.
        ///E:/swf/ </param>
        void TagEndProcess(TemplateManager manager, Tag tag, string innerContent);
    }
}

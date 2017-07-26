using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JumboTCMS.API.Discuz.Toolkit
{
    ///E:/swf/ <summary>
    ///E:/swf/ 好友类
    ///E:/swf/ </summary>
    public class Friend : SessionWrapper
    {
        [XmlElement("uid")]
        public long UId;

        public Friend(long UId, DiscuzSession session)
        {
            this.UId = UId;
            this.Session = session;
        }

        public Friend()
        { }

        public User GetUserInfo()
        {
            User[] users = Session.GetUserInfo(new long[] { UId }, User.FIELDS);

            return users[0];
        }
    }

    ///E:/swf/ <summary>
    ///E:/swf/ 当前用户类
    ///E:/swf/ </summary>
    public class Me : Friend
    {
        public Me(long uid, DiscuzSession session)
            : base(uid, session)
        { }
    }
}

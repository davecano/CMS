using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JumboTCMS.DAL.OAuthServer
{
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string userid { get; set; }
        public Token(string _access_token, string _refresh_token, int _expires_in,string _userid)
        {
            expires_in = _expires_in;
            access_token = _access_token;
            refresh_token = _refresh_token;
            userid = _userid;
        }
    }
}
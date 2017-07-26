using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JumboTCMS.DAL.OAuthServer
{
    public class ClientData
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }

        public ClientData(string _client_id, string _client_secret, string _redirect_uri)
        {
            client_id = _client_id;
            client_secret = _client_secret;
            redirect_uri = _redirect_uri;
        }
    }
}

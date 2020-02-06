using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Models
{
   public class ServerDataModel:BaseEntity
    {
        public string sip_username { get; set; }
        public string sip_password { get; set; }
        public string sip_server { get; set; }
        public SipTransportModel sip_transport { get; set; }
        public string display_name { get; set; }
        public string user_domain { get; set; }
        public string auth_name { get; set; }
        public string stun_server { get; set; }
        public string stun_server_port { get; set; }
        public string srtp { get; set; }
        public string extension { get; set; }
        public string default_transport { get; set; }
    }
}

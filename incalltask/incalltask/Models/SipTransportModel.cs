using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Models
{
   public class SipTransportModel:BaseEntity
    {
        public int udp { get; set; }
        public int tcp { get; set; }
        public int tls { get; set; }
    }
}

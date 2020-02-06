using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Helper
{
  public static  class Constants
    {
        // public static readonly string service_provider_key = "0698a6fd-60f8-4e4f-9804-e10a962f6030";
        //public static readonly string service_provider_key_Api = "http://204.48.21.29:81/auth/serviceprovider/api/informations";
        // this for test 
        public static readonly string service_provider_key = "d61ff7da-0f7b-4a38-8d56-92cb5ff83e6fa";
        public static readonly string service_provider_key_Api = "http://api.sip.twasal.co/api/v1/information";
        public static bool UDP_Enabled { get; set; }
        public static bool TLS_Enabled { get; set; }
        public static bool TCP_Enabled { get; set; }

    }
}

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
        public static readonly string service_provider_key = "4e56baf1-087e-4dd9-b80f-2332482054a6";
        public static readonly string service_provider_key_Api = "http://micros.twasal.co:81/customer/sip/provisioning";
        public static bool UDP_Enabled { get; set; }
        public static bool TLS_Enabled { get; set; }
        public static bool TCP_Enabled { get; set; }

    }
}

using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Communication.Request
{
   public class LoginRequestModel
    {
        [AliasAs("service_provider_key")]
        public string service_provider_key { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}

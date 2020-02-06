using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Communication.Request
{
  public  class DataServerRequestModel
    {
        [AliasAs("uuid")]
        public string uuid { get; set; }
    }
}

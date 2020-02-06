using incalltask.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Communication.Response
{
    public class ResponseBaseModel
    {
        public int status { get; set; }
        public bool success { get; set; }
        public ErrorModel error { get; set; }

    }


  


}

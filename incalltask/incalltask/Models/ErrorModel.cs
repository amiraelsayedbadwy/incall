using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Models
{
   public class ErrorModel:BaseEntity
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}

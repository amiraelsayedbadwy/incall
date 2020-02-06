using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Models
{
  public  class HistoryModel:BaseEntity
    {
        public string Number { get; set; }
        public string CallDate { get; set; }
        public bool makecall { get; set; }
        public string Image { get; set; }
    
        public string CallType { get; set; }
    }
}

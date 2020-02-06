using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Models
{
   
    public class ServiceProviderInformationModel:BaseEntity
    {
        public bool auth { get; set; }
        public string login_type { get; set; }
        public string login_url { get; set; }
        public string operator_status { get; set; }
        public Design_Options design_options { get; set; }
    }
}

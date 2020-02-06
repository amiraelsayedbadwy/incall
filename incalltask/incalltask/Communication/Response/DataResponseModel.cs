using incalltask.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Communication.Response
{
    public class DataResponseModel:ResponseBaseModel
    {
        public ServiceProviderInformationModel data { get; set; }
    }
}

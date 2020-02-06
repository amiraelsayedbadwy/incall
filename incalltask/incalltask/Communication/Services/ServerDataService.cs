
using incalltask.Communication.Request;
using incalltask.Communication.Response;
using incalltask.Helper;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incalltask.Communication.Services
{
    public class ServerDataService : IServerDataService
    {
        
        public async Task<DataResponseModel> ServerData(DataServerRequestModel uuid)
        {
            var api = RestService.For<IInComeCallApi>(Helper.Constants.service_provider_key_Api);
           
            var ServiceProviderInformation = await api.GetServerData(uuid);

            return ServiceProviderInformation;
        }
    }
}

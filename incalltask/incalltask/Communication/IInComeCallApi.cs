using incalltask.Communication.Request;
using incalltask.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incalltask.Communication
{
   
    public interface IInComeCallApi
    {
       
        [Post("/api/v1/information")]
        Task <DataResponseModel >GetServerData([Body]DataServerRequestModel uuid);
        [Post("/api/v1/tenant/extension/auth/login")]
        Task<LoginResponseModel> Login([Body]LoginRequestModel requestModel);
    }
}

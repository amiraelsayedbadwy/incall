using incalltask.Communication.Request;
using incalltask.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incalltask.Communication.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponseModel> Login(LoginRequestModel request)
        {
            var api = RestService.For<IInComeCallApi>(Helper.Constants.service_provider_key_Api);

            var serverData = await api.Login(request);
            return serverData;
        }
    }
}

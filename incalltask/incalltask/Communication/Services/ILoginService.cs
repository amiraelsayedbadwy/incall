﻿using incalltask.Communication.Request;
using incalltask.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incalltask.Communication.Services
{
  public  interface ILoginService
    {
        Task<LoginResponseModel> Login(LoginRequestModel request);
    }
}

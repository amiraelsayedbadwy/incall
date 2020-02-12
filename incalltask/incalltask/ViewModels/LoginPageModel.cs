using FreshMvvm;
using FreshMvvm.Popups;
using incalltask.Communication;
using incalltask.Communication.Request;
using incalltask.Communication.Services;
using incalltask.Enums;
using incalltask.Helper;
using incalltask.Interface;
using incalltask.Models;
using incalltask.Pages;
using PortSip.AndroidSample;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace incalltask.ViewModels
{
   public class LoginPageModel: FreshMvvm.FreshBasePageModel
    {
        #region service
        public readonly IServerDataService serverDataService;
        public readonly ILoginService loginService;
        public readonly IPortSIPEvents portSIPEvents;
        public readonly IService service;
        public PortSipLib _portSipLibsdk;
        #endregion
        #region properties
      
        public string Email { get; set; }
        public string Password { get; set; }
        #endregion
        #region Lists
        public static Dictionary<string, int> TransportList { get; set; }
        #endregion
        public DropDownMenuModel LanguageList { get; set; }
   
        public ICommand ChooseLanguageCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public LoginPageModel(IServerDataService _serverDataService,ILoginService login,IPortSIPEvents portSIP,IService _service)
        {
            service = _service;
            TransportList = new Dictionary<string, int>();
            serverDataService = _serverDataService;
            loginService = login;
            portSIPEvents = portSIP;
            ChooseLanguageCommand = new Command(ChooseLanguageCommandExcute);
            LoginCommand = new Command(LoginCommandExcute);
            DummyData();
        }

        private async void LoginCommandExcute(object obj)
        {
            try
            {
                if(!String.IsNullOrEmpty(Email)&& !String.IsNullOrEmpty(Password))
                {
                    var request = new LoginRequestModel
                    {
                        email = Email,
                        password = Password,
                        service_provider_key = Helper.Constants.service_provider_key
                    };
                    var serverDataResult = await loginService.Login(request);
                    if (serverDataResult.success)
                    {
                        #region save server data and register server
                        SaveUserData(serverDataResult.data);
                       // loadTabbedPage();
                       PortSdkData();
                        #endregion

                        
                    }
                    else
                    {
                        await CoreMethods.PushPopupPageModel<WrongUserPopupPageModel>("credentials error");
                    }
                }
                else
                {
                   
                    await CoreMethods.PushPopupPageModel<WrongUserPopupPageModel>("Please enter user name");
                }


            }
            catch (Exception e)
            {

                throw e;
            }


            // await CoreMethods.PushPageModel<MainHomePageModel>();
          
        }
        private void SaveUserData(ServerDataModel serverData)
        {
            Settings.Extension = serverData.extension;
            #region save data
            Settings.FirstLogin = false;
            Settings.UserDomain = serverData.user_domain;
            Settings.DisplayName = serverData.display_name;
            Settings.UserName = serverData.sip_username;
            Settings.Password = serverData.sip_password;
            Settings.AuthName = serverData.auth_name;
            Settings.SipServer = serverData.sip_server;
            Settings.STUNServer = serverData.stun_server;
            //Settings.STUNServerPort = int.Parse(serverDataResult.data.stun_server_port);
            Settings.SRTPPolicy = serverData.srtp;
            Settings.DefaultTransport = serverData.default_transport;
            // make it list key wa value 
            if (serverData.default_transport.ToLower().Equals(TransportType.TCP.ToString().ToLower()))
            {
                Settings.SipServerPort = serverData.sip_transport.tcp;
                Settings.SipServerType = TransportType.TCP.ToString();
            }
            else if (serverData.default_transport.ToLower().Equals(TransportType.UDP.ToString().ToLower()))
            {
                Settings.SipServerPort = serverData.sip_transport.udp;
                Settings.SipServerType = TransportType.UDP.ToString();
            }
            else
            {
                Settings.SipServerPort = serverData.sip_transport.tls;
                Settings.SipServerType = TransportType.TLS.ToString();
            }
            TransportList.Add("TCP", serverData.sip_transport.tcp);
            TransportList.Add("UDP", serverData.sip_transport.udp);
            TransportList.Add("TLS", serverData.sip_transport.tls);
           
            
            #endregion
           
        }
        private void PortSdkData()
        {
            // lasa ha3ml validation 3lhom
            var intiliaztePortSdkresult = portSIPEvents.IntiliaztePortSdk(Settings.SipServerType);
            var keylicenceresult= portSIPEvents.LicenceKey();
            portSIPEvents.SetUserData(Settings.UserName, Settings.DisplayName, 
                                    Settings.AuthName, Settings.Password, Settings.UserDomain, Settings.SipServer,
                                    Settings.SipServerPort, Settings.STUNServer, Settings.STUNServerPort);
                                var result = portSIPEvents.RegisterToServer(Settings.SRTPPolicy);
            if(result=="0")
            {
                loadTabbedPage();
               // service.Start();
            }
         
        }
      private void loadTabbedPage()
        {
            var tabbedNavigation = new FreshTabbedNavigationContainer();
            tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            tabbedNavigation.BackgroundColor = Color.FromHex("#F9F9F9");
            tabbedNavigation.AddTab<MainHomePageModel>("", "callblue.png");
            tabbedNavigation.AddTab<HistoryListPageModel>("", "history.png");
            App.Current.MainPage = tabbedNavigation;
        }
      
        private void ChooseLanguageCommandExcute(object obj)
        {
            //throw new NotImplementedException();
           
      
        }
        public void DummyData()
        {
            LanguageList = new DropDownMenuModel
            {
                HeaderName = "Choose Language",
                DropMenuList = new ObservableCollection<LanguagesModel>
                {
                    new LanguagesModel { Name = "Arabic (Saudi Arabia)" },
                    new LanguagesModel { Name = "English (United States)" }
                }

            };
           
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            try
            {
                var key = new DataServerRequestModel { uuid = Helper.Constants.service_provider_key };
                var ServiceProviderInformation = await serverDataService.ServerData(key);
                if (ServiceProviderInformation.success)
                {
                    // what data i need  to save from this data
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}

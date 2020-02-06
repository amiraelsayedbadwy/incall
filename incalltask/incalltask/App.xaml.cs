using FreshMvvm;
using incalltask.ViewModels;
using incalltask.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using incalltask.Communication.Services;
using incalltask.Communication.Request;
using Refit;
using incalltask.Communication;
using incalltask.Helper;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
using incalltask.Interface;
using PortSip.AndroidSample;

namespace incalltask
{
    public partial class App : Application
    {
        public App()
        {
           
            InitializeComponent();
            try
            {
                IntIOC();
                if (Settings.FirstLogin)
                {
                    MainPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                }
                else
                {
                    LoadTabPage();
                } 
               // MainPage = new LoginPage();
               
            }
            catch(Exception e)
            {
                throw e;
            }
       
            
        }
    public void IntIOC()
        {
            FreshIOC.Container.Register<IServerDataService, ServerDataService>();
            FreshIOC.Container.Register<ILoginService, LoginService>();
           

        }
       public void LoadTabPage()
        {
            var tabbedNavigation = new FreshTabbedNavigationContainer();
            tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            tabbedNavigation.BackgroundColor = Color.FromHex("#F9F9F9");
            tabbedNavigation.AddTab<MainHomePageModel>("", "callblue.png");
            tabbedNavigation.AddTab<HistoryListPageModel>("", "history.png");
            MainPage = tabbedNavigation;
        }
       
        protected override void OnStart()
        {
            // Handle when your app starts
            
          
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

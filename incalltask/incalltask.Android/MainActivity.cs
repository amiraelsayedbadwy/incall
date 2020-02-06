using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Essentials;
using incalltask.Interface;
using incalltask.Droid.Service;
using PortSip.AndroidSample;
using Android.Content;
using Android;

namespace incalltask.Droid
{
    [Activity(Label = "incalltask", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public PortMessageReciver receiver=null;
        private const int REQ_DANGERS_PERMISSION = 2;
        private const int REQ_ACTION_MANAGE_WRITE_SETTINGS = 6;
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Instance = this;

            FreshMvvm.FreshIOC.Container.Register<IPortSIPEvents, PortSipService>();
            try
            {
               
                IntentFilter filter = new IntentFilter();
                filter.AddAction(PortSipService.REGISTER_CHANGE_ACTION);
                filter.AddAction(PortSipService.CALL_CHANGE_ACTION);
                filter.AddAction(PortSipService.PRESENCE_CHANGE_ACTION);

                RegisterReceiver(new PortMessageReciver(), filter);

                Intent onLineIntent = new Intent(this, typeof(PortSipService));
              //  onLineIntent.SetAction(PortSipService.ACTION_SIP_REGIEST);
                StartService(onLineIntent);
            }
            catch (Exception e)
            {

                throw;
            }


            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
          
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    

      
    }
}
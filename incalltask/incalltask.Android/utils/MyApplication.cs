using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PortSip.AndroidSample;

namespace incalltask.Droid.utils
{
    [Application]
    public class MyApplication : Application
    {
        public Boolean SdkInit { set; get; }
        public Boolean Conference { set; get; }
        public PortSipLib Engine { private set; get; }

        public bool UseFrontCamera { set; get; }

        public MyApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            UseFrontCamera = true;
            Engine = new PortSipLib(this);
        }

    }
}
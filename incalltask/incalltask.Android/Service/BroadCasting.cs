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
using incalltask.Interface;

namespace incalltask.Droid.Service
{
    public class BroadCasting : IBroadCast
    {
        public PortMessageReciver receiver = null;
        public void BroadCastStart()
        {
            IntentFilter filter = new IntentFilter();
            filter.AddAction(PortSipService.REGISTER_CHANGE_ACTION);
            filter.AddAction(PortSipService.CALL_CHANGE_ACTION);
            filter.AddAction(PortSipService.PRESENCE_CHANGE_ACTION);

            Intent onLineIntent = new Intent(MainActivity.Instance, typeof(PortSipService));
            
            MainActivity.Instance.StartService(onLineIntent);
        }
    }
}
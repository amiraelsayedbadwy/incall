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
using incalltask.Droid.Service;
using incalltask.Interface;
using Xamarin.Forms;

namespace incalltask.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class PortMessageReciver : BroadcastReceiver,IBroadCast
    {
        public delegate void OnBroadcastReceiver(Intent intent);
        public OnBroadcastReceiver broadcastReceiver;

        public override void OnReceive(Context context, Intent intent)
        {
             broadcastReceiver?.Invoke(intent);
            //if(intent.Action.Equals(PortSipService.CALL_CHANGE_ACTION))
            //{
            //    MessagingCenter.Send<IBroadCast, string>(this, "receiveBarcode", "incoming call");
            //}
            //else if(intent.Action.Equals(PortSipService.REGISTER_CHANGE_ACTION))
            //{
            //    MessagingCenter.Send<IBroadCast, string>(this, "receiveBarcode", "incoming call");
            //}
            
        }
    }
}
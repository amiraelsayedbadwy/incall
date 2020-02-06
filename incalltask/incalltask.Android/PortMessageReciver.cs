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

namespace incalltask.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class PortMessageReciver : BroadcastReceiver
    {
        public delegate void OnBroadcastReceiver(Intent intent);
        public OnBroadcastReceiver broadcastReceiver;

        public override void OnReceive(Context context, Intent intent)
        {
            broadcastReceiver?.Invoke(intent);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace incalltask.Droid.Service
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseMessageService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            notifyTokenRefresh(p0);
        }


        public override void OnMessageReceived(RemoteMessage p0)
        {
            Log.Debug(TAG, "From: " + p0.From);
            string message_Tips = "";
            if (p0.Data != null)
            {
                string type = p0.Data["msg_type"];
                if ("audio".Equals(type) || "video".Equals(type))
                {
                    Intent srvIntent = new Intent(this, typeof(PortSipService));
                    srvIntent.SetAction(PortSipService.ACTION_PUSH_MESSAGE);
                    StartService(srvIntent);
                }
                if ("im".Equals(type))
                {
                    //string content = p0.Data["msg_content"];
                    //string from = p0.Data["send_from"];
                    //string to = p0.Data["send_to"];
                    //string pushid = p0.Data["x-push-id"];
                    Intent srvIntent = new Intent(this, typeof(PortSipService));
                    srvIntent.SetAction(PortSipService.ACTION_PUSH_MESSAGE);
                    StartService(srvIntent);
                }
                message_Tips = "received a " + type + " message";
            }

            SendNotification(message_Tips);
        }

        private void notifyTokenRefresh(string token)
        {
            Intent intent = new Intent(this, typeof(PortSipService));
            intent.SetAction(PortSipService.ACTION_PUSH_TOKEN);
            intent.PutExtra(PortSipService.EXTRA_PUSHTOKEN, token);
            StartService(intent);
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);


            var notificationManager = NotificationManager.FromContext(this);
            Notification notification;
            if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.O)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                var notificationBuilder = new Notification.Builder(this)
                    .SetSmallIcon(Resource.Drawable.income)
                    .SetContentTitle("FCM Message")
                    .SetContentText(messageBody)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);

                notification = notificationBuilder.Build();

#pragma warning restore CS0618 // Type or member is obsolete
            }
            else
            {
                var pushChannel = BaseContext.PackageName;
                const string channelName = "Messages from Push";

                NotificationChannel channel;
                channel = notificationManager.GetNotificationChannel(pushChannel);
                if (channel == null)
                {
                    channel = new NotificationChannel(pushChannel, channelName, NotificationImportance.High);
                    channel.EnableVibration(true);
                    channel.EnableLights(true);
                    channel.SetSound(
                        RingtoneManager.GetDefaultUri(RingtoneType.Notification),
                        new AudioAttributes.Builder().SetUsage(AudioUsageKind.Notification).Build()
                    );
                    channel.LockscreenVisibility = NotificationVisibility.Public;
                    notificationManager.CreateNotificationChannel(channel);
                }
                channel?.Dispose();

                notification = new Notification.Builder(BaseContext)
                                                            .SetChannelId(pushChannel)
                                                            .SetSmallIcon(Resource.Drawable.income)
                                                            .SetContentTitle("FCM Message")
                                                            .SetContentText(messageBody)
                                                            .SetAutoCancel(true)
                                                            .SetContentIntent(pendingIntent)
                                                            .Build();
            }
            notificationManager.Notify(1331, notification);
            notification.Dispose();
        }

    }
}
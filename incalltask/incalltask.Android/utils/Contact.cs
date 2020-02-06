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

namespace incalltask.Droid.utils
{
    public class Contact : Java.Lang.Object
    {
        public string SubRequestDescription { set; get; }//对方的出席状态
        public enum SUBSCRIBE_STATE_FLAG
        {
            UNSETTLLED = 0,//未被远端定阅，未处理
            ACCEPTED = 1,//未被远端定阅，已拒绝
            REJECTED = 2,//未被远端定阅，已拒绝
            UNSUBSCRIBE = 3,//未被远端定阅
        }
        public string SipAddr { set; get; }
        public string SubDescription { set; get; }//对方的出席状态
        public bool SubscribRemote { set; get; }//是否订阅对方

        public long SubId { set; get; }//if SubId >0 means received remote subscribe
        public SUBSCRIBE_STATE_FLAG state { set; get; } // weigher accepte remote subscribe

        public string currentStatusToString()
        {
            string status = "";

            status += "订阅对方：" + SubscribRemote;
            status += "  对方出席状态为：" + SubDescription;


            status += " 受到对方订阅:(" + SubRequestDescription + ")";
            switch (state)
            {
                case SUBSCRIBE_STATE_FLAG.ACCEPTED:
                    status += "已接受";
                    break;
                case SUBSCRIBE_STATE_FLAG.REJECTED:
                    status += "已拒绝";
                    break;
                case SUBSCRIBE_STATE_FLAG.UNSETTLLED:
                    status += "未决";
                    break;
                case SUBSCRIBE_STATE_FLAG.UNSUBSCRIBE:
                    status += "未订阅";
                    break;
            }

            return status;
        }
        public Contact()
        {
            state = SUBSCRIBE_STATE_FLAG.UNSUBSCRIBE;//未被订阅
            SubscribRemote = false;//未订阅对方
            SubId = 0;
        }

    }
}
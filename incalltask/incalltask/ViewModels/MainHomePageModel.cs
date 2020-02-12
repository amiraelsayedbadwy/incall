using Com.Portsip;
using FreshMvvm;
using FreshMvvm.Popups;
using incalltask.Helper;
using incalltask.Interface;
using incalltask.Pages;
using PortSip.AndroidSample;
using PortSIP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IPortSIPLib = incalltask.Interface.IPortSIPLib;

namespace incalltask.ViewModels
{
   public class MainHomePageModel:FreshMvvm.FreshBasePageModel
    {
        public readonly IPortSIPLib _portSipLibsdk;
        public readonly IService _service;

        public readonly Interface.IPortSIPEvents portSIPEvents;
        //  public PortSipLib PortSipLi  b;
        public string Extension { get; set; }
        public string UserStatus { get; set; }
        #region call
       public string CallTo { get; set; }
        public long sessionid { get; set; }
        public string CountryCode { get; set; }
        public long currentLineSessionId;
       // Session currentLineForCalling;

        #endregion
        public ICommand NumPadCommand { get; set; }
        public ICommand CallCommand { get; set; }
        public ICommand OpenPopUpCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public MainHomePageModel(Interface.IPortSIPEvents _portSIPEvents,IService service)
        {
            _service = service;
            portSIPEvents = _portSIPEvents;
            LogoutCommand = new Command(LogoutCommandExcute);      
            OpenPopUpCommand = new Command(OpenPopUpCommandExcute);
            CallCommand = new Command(CallCommandExcute);
            NumPadCommand = new Command(NumPadCommandExcute);
            _service.Start();
        }

        private void NumPadCommandExcute(object obj)
        {
            var digit = obj as string;
            switch (digit)
            {
                case "0":
                    CallTo += digit;
                    portSIPEvents.KeySound(digit, sessionid);
                    break;
                case "1":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;

                    break;
                case "2":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "3":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "4":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "5":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "6":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "7":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "8":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "9":
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                case "*":
                    portSIPEvents.KeySound(digit, sessionid);
                    // 10 for *
                    CallTo += digit;
                    break;
                case "#":
                    // for #
                    portSIPEvents.KeySound(digit, sessionid);
                    CallTo += digit;
                    break;
                default:
                    break;
            }

        }

        private void CallCommandExcute(object obj)
        {
            CheckCall(CallTo);
          sessionid = portSIPEvents.Call(CallTo, false, true);

            if (sessionid <= 0)
            {
                Console.WriteLine("Call failure, session null");
                //SwitchPanel("DialCallFailure");
                portSIPEvents.onInviteFailure(sessionid, "FAILED", 4, "wrong session id");
                return;
            }


        }
        public string CheckCall(string callTo)
        {
            if (callTo.Length <= 0)
            {
                //showTips("The phone number is empty.");
                //PopupTitle.Text = "Alert";
                //PopupMessage.Text = GetString(Resource.String.toast_entermobilenumber);
                //PopupLayout.Visibility = ViewStates.Visible;
                // here we will show popup

            }
            if (callTo.Length >= 5)
            {
                if (CountryCode != "Other")
                {
                    callTo = "00" + CountryCode + " " + callTo;
                }
               
            }
            
            return callTo;

        }

        private   void LogoutCommandExcute(object obj)
        {
            portSIPEvents.UnRegisterServer();
           // PortSipLib.unInitialize();
            App.Current.MainPage= FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
        }

        private async void OpenPopUpCommandExcute(object obj)
        {
            //  Navigation.p
            await CoreMethods.PushPopupPageModel<OfflinePopPageModel>();
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Extension = Settings.Extension;
            UserStatus = Settings.UserStatus;
        }
        public long makeCall(string callee, bool videoCall)
        {
            //if (ActiveSessionId >= 0)
            //{
            //    //var alert = new UIAlertView();
            //    //alert.Title = "Warning";
            //    //alert.Message = "Current line is busy now, please switch a line";
            //    //alert.Delegate = null;
            //    //alert.AddButton("OK");

            //    //alert.Show();
            //    return -1;
            //}

            ////long newSessionId = CallManager.MakeCall(callee, videoCall);
            ////if (newSessionId >= 0)
            ////{
            ////    numpadViewController.setStatusText("Calling:" + callee + " on line " + ActiveLine);
            ////    ActiveSessionId = newSessionId;

            ////}
            ////else
            ////{
            ////    numpadViewController.setStatusText("make call failure ErrorCode =" + newSessionId);

            ////}
            return 1;
        }
        #region for make sound when click 
        private void AddNumber(string v)
        {
            try
            {
                string number = v;
               CallTo += number;
               //// HighLightNumberClick(v);
               // if (CallManager.Instance.regist && currentLineForCalling.state == CALL_STATE_FLAG.CONNECTED)
               // {
               //     if (number == "*")
               //     {
               //         portSIPLib.sendDtmf(currentLineForCalling.SessionID, PortSipEnumDefine.EnumDtmfMothodRfc2833, 10,
               //                 160, true);
               //         Ring.getInstance(activity).startKeypadTone(10);
               //         return;
               //     }
               //     if (number == "#")
               //     {
               //         portSipSdk.sendDtmf(currentLineForCalling.SessionID, PortSipEnumDefine.EnumDtmfMothodRfc2833, 11,
               //                 160, true);
               //         Ring.getInstance(activity).startKeypadTone(11);
               //         return;
               //     }
               //     int sum = Convert.ToInt32(number);// 0~9
               //     Ring.getInstance(activity).startKeypadTone(sum);
               //     portSipSdk.sendDtmf(currentLineForCalling.SessionID, PortSipEnumDefine.EnumDtmfMothodRfc2833, sum,
               //             160, true);
               //     //etSipNum.SetSelection(etSipNum.Length());
               // }
               // else
               // {
               //     if (number == "*")
               //     {
               //         Ring.getInstance(activity).startKeypadTone(10);
               //         return;
               //     }
               //     if (number == "#")
               //     {
               //         Ring.getInstance(activity).startKeypadTone(11);
               //         return;
               //     }
               //     int sum = Convert.ToInt32(number);// 0~9
               //     Ring.getInstance(activity).startKeypadTone(sum);
               // }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion


    }
}

using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Portsip;
using Firebase.Iid;
using incalltask.Droid.utils;
using incalltask.Enums;
using incalltask.Interface;
using incalltask.ViewModels;
using PortSip.AndroidSample;
using PortSIP;
using IPortSIPEvents = incalltask.Interface.IPortSIPEvents;


namespace incalltask.Droid.Service
{
    [Service]
    [IntentFilter(new string[] { "com.companyname.incalltask.PortSipService" })]
    [IntentFilter(new string[] { ACTION_PUSH_MESSAGE })]
    [IntentFilter(new string[] { ACTION_PUSH_TOKEN })]
    [IntentFilter(new string[] { ACTION_SIP_REGIEST })]
    [IntentFilter(new string[] { ACTION_SIP_UNREGIEST })]
    [IntentFilter(new string[] { ACTION_SIP_UNINITIALIZE })]
    public class PortSipService : Android.App.Service, IPortSIPEvents, IService
    {
        #region for broad cast
        public const string ACTION_SIP_REGIEST = "com.companyname.incalltask.REGIEST";
        public const string ACTION_SIP_UNREGIEST = "com.companyname.incalltask.UNREGIEST";
        public const string ACTION_SIP_UNINITIALIZE = "com.companyname.incalltask.ACTION_SIP_UNINITIALIZE";
        public const string REGISTER_CHANGE_ACTION = "com.companyname.incalltask.RegisterStatusChagnge";
        public const string CALL_CHANGE_ACTION = "com.companyname.incalltask.CallStatusChagnge";
        public const string PRESENCE_CHANGE_ACTION = "com.companyname.incalltask.PRESENCEStatusChagnge";
        public const string ACTION_PUSH_MESSAGE = "com.companyname.incalltask.PushMessageIncoming";
        public const string ACTION_PUSH_TOKEN = "com.companyname.incalltask.PushToken";
        public static string EXTRA_REGISTER_STATE = "RegisterStatus";
        public static string EXTRA_CALL_SEESIONID = "SessionID";
        public static string EXTRA_CALL_DESCRIPTION = "Description";
        private const string APPID = "com.companyname.incalltask";
        public static string EXTRA_PUSHTOKEN = "token";
        private string PushToken;
        private bool NeedPush = true;

        #endregion
        public Context context;
        public TRANSPORT_TYPE transType;
        public SRTP_POLICY srtptype;
        public PortSipLib mEngine { private set; get; }
        IPortSIPEvents sipevent;
        public Boolean Conference { set; get; }
        #region interface implmentation functions
        public PortSipService() 
        {
            
            mEngine = new PortSipLib(MainActivity.Instance);
            mEngine.SetPortSIPEventsCallback((PortSIP.IPortSIPEvents)sipevent);
         
            Console.WriteLine("PortSipService: {0} ", "Create Service");
        }

        public void OnComplete(Task task)
        {
            if (task.IsComplete && task.IsSuccessful)
            {
                PushToken = FirebaseInstanceId.Instance.Token;
                Console.WriteLine("google Token:" + PushToken);
            }
        }
        public void onPlayAudioFileFinished(long sessionId, string fileName) { }

        public void onPlayVideoFileFinished(long sessionId) { }

        public void onReceivedRTPPacket(long sessionId, bool isAudio, byte[] RTPPacket, int packetSize) { }

        public void onSendingRtpPacket(long sessionId, bool isAudio, byte[] RTPPacket, int packetSize) { }


        public void onAudioRawCallback(long sessionId, int callbackType, byte[] data, int dataLength, int samplingFreqHz) { }

        public int OnVideoRawCallback(long sessionId, int callbackType, int width, int height, byte[] data, int dataLength) { return 0; }
        public void onVideoDecoderCallback(long sessionId, int width, int height, int framerate, int bitrate) { }
        public int onVideoRawCallback(long sessionId, int callbackType, int width, int height, byte[] data, int dataLength) { return 0; }
        public void onInviteFailure(long sessionId, string reason, int code, string sipMessage)
        {
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);
            if (session != null)
            {
                session.state = CALL_STATE_FLAG.FAILED;
                session.SessionID = sessionId;
                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
                String description = session.LineName + " onInviteFailure";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);

                sendPortSipMessage(description, broadIntent);
                Console.WriteLine(description);
                }

            Ring.getInstance(MainActivity.Instance).stopRingBackTone();
            Ring.getInstance(MainActivity.Instance).stopcalleeRingBackTone();
        }

       

        public void onInviteConnected(long sessionId)
        {
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);
            if (session != null)
            {
                session.state = CALL_STATE_FLAG.CONNECTED;
                session.SessionID = sessionId;
                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
                String description = session.LineName + " OnInviteConnected";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);

                sendPortSipMessage(description, broadIntent);

            }
                Ring.getInstance(MainActivity.Instance).stopRingTone();
                Ring.getInstance(MainActivity.Instance).stopcalleeRingBackTone();
                Console.WriteLine("{0} onInviteConnected", sessionId);
        }

        public void onInviteBeginingForward(string forwardTo)
        {
            Console.WriteLine("{0} onInviteBeginingForward", forwardTo);
        }

        public void onInviteClosed(long sessionId)
        {
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);
            if (session != null)
            {
                session.state = CALL_STATE_FLAG.CLOSED;
                session.SessionID = sessionId;

                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
                String description = session.LineName + " OnInviteClosed";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);
                Console.WriteLine(description);
                sendPortSipMessage(description, broadIntent);
            }
            Ring.getInstance(MainActivity.Instance).stopRingTone();
          
            Ring.getInstance(MainActivity.Instance).stopRingBackTone();
            Ring.getInstance(MainActivity.Instance).stopcalleeRingBackTone();

        }

        public void onDialogStateUpdated(string BLFMonitoredUri, string BLFDialogState, string BLFDialogId, string BLFDialogDirection)
        {
            string text = "The user ";
            text += BLFMonitoredUri;
            text += " dialog state is updated: ";
            text += BLFDialogState;
            text += ", dialog id: ";
            text += BLFDialogId;
            text += ", direction: ";
            text += BLFDialogDirection;

            Console.WriteLine("{0} onDialogStateUpdated", text);
        }

        public void onRemoteUnHold(long sessionId, string audioCodecNames, string videoCodecNames, bool existsAudio, bool existsVideo)
        {
            Console.WriteLine("{0} onRemoteUnHold", sessionId);
        }

        public void onRemoteHold(long sessionId)
        {
            Console.WriteLine("{0} hold", sessionId);
        }
        //public void S(long sessionId,
        // string callerDisplayName,
        // string caller,
        // string calleeDisplayName,
        // string callee,
        // string audioCodecNames,
        // string videoCodecNames,
        // bool existsAudio,
        // bool existsVideo,
        // string sipMessage)
        //{
        //    Console.WriteLine("Incoming call ({4}) from {0} {1} to {2} {3}", caller, callerDisplayName, callee, calleeDisplayName, sessionId);

        //    var a = betweenStrings(caller, ":", "@");

        //    Session session = CallManager.Instance.FindIdleSession();
        //    session.state = CALL_STATE_FLAG.INCOMING;
        //    session.HasVideo = existsVideo;
        //    session.SessionID = sessionId;
        //    session.Remote = caller;
        //    session.DisplayName = a;

        //    Ring.getInstance(MainActivity.Instance).startRingTone();
        //}
        public static String betweenStrings(String text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        public void onInviteTrying(long sessionId)
        {
            Console.WriteLine("onInviteTrying:{0}", sessionId);
           
        }
        public void onInviteSessionProgress(
            long sessionId,
            string audioCodecNames,
            string videoCodecNames,
            bool existsEarlyMedia,
            bool existsAudio,
            bool existsVideo,
            string sipMessage)
        { }
        public void onInviteRinging(long sessionId, string statusText, int statusCode, string sipMessage)
        {
           Ring.getInstance(MainActivity.Instance).startcalleeRingBackTone();
        }

        public void onInviteAnswered(long sessionId,
            string callerDisplayName,
            string caller,
            string calleeDisplayName,
            string callee,
            string audioCodecNames,
            string videoCodecNames,
            bool existsAudio,
            bool existsVideo,
            string sipMessage)
        {
            Console.WriteLine("{0} answered", sessionId);
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);


            if (session != null)
            {
                session.state = CALL_STATE_FLAG.CONNECTED;
                session.HasVideo = existsVideo;

                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);

                String description = session.LineName + " onInviteAnswered";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);
                Console.WriteLine(description);
                sendPortSipMessage(description, broadIntent);
            }

            Ring.getInstance(MainActivity.Instance).stopRingBackTone();
            Ring.getInstance(MainActivity.Instance).stopcalleeRingBackTone();
        }

       
        public void onInviteUpdated(long sessionId, string audioCodecNames, string videoCodecNames, bool existsAudio, bool existsVideo, string sipMessage)
        {
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);

            if (session != null)
            {
                session.state = CALL_STATE_FLAG.CONNECTED;
                session.HasVideo = existsVideo;

                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
                String description = session.LineName + " OnInviteUpdated";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);
                Console.WriteLine(description);
                sendPortSipMessage(description, broadIntent);
            }
            }

        //update offline status
        public void onPresenceOffline(string fromDisplayName, string from)
        {
            Contact contact = ContactManager.Instatnce.FindContactBySipAddr(from);
            if (contact == null)
            {

            }
            else
            {
                contact.SubDescription = "Offline";
            }
            //hena lasa 3yza a8yer da
            Intent broadIntent = new Intent(PRESENCE_CHANGE_ACTION);
            sendPortSipMessage("OnPresenceRecvSubscribe", broadIntent);
        }
        //update online status
        public void onPresenceOnline(string fromDisplayName, string from, string stateText)
        {
            Contact contact = ContactManager.Instatnce.FindContactBySipAddr(from);
            if (contact == null)
            {

            }
            else
            {
                contact.SubDescription = stateText;
            }
            // lasa 3yza a3mlha


            Intent broadIntent = new Intent(PRESENCE_CHANGE_ACTION);
            sendPortSipMessage("OnPresenceRecvSubscribe", broadIntent);
        }

        public void onPresenceRecvSubscribe(long subscribeId, string fromDisplayName, string from, string subject)
        {
            Contact contact = ContactManager.Instatnce.FindContactBySipAddr(from);
            if (contact == null)
            {
                contact = new Contact();
                contact.SipAddr = from;
                ContactManager.Instatnce.AddContact(contact);
            }

            contact.SubRequestDescription = subject;
            contact.SubId = subscribeId;
            switch (contact.state)
            {
                case Contact.SUBSCRIBE_STATE_FLAG.ACCEPTED://This subscribe has accepted
                   mEngine.presenceAcceptSubscribe(subscribeId);
                    break;
                case Contact.SUBSCRIBE_STATE_FLAG.REJECTED://This subscribe has rejected
                   mEngine.presenceRejectSubscribe(subscribeId);
                    break;
                case Contact.SUBSCRIBE_STATE_FLAG.UNSETTLLED:
                    break;
                case Contact.SUBSCRIBE_STATE_FLAG.UNSUBSCRIBE:
                    contact.state = Contact.SUBSCRIBE_STATE_FLAG.UNSETTLLED;
                    break;
            }
            Intent broadIntent = new Intent(PRESENCE_CHANGE_ACTION);
            sendPortSipMessage("OnPresenceRecvSubscribe", broadIntent);
        }

        public void onRecvMessage(long sessionId, string mimeType, string subMimeType, byte[] messageData, int messageDataLength)
        {
            throw new NotImplementedException();
        }

        public void onRecvNotifyOfSubscription(long sessionId, string notifyMessage, byte[] messageData, int messageDataLength)
        {
            throw new NotImplementedException();
        }
        public void onRecvOutOfDialogMessage(string fromDisplayName, string from, string toDisplayName, string to, string mimeType, string subMimeType, byte[] messageData, int messageDataLength, string sipMessage)
        {
            string message = System.Text.Encoding.UTF8.GetString(messageData);
            Console.WriteLine("onRecvOutOfDialogMessage:from{0} {1}", from, message);
        }

        public void onRegisterFailure(string statusText, int statusCode, string sipMessage)
        {
            Intent broadIntent = new Intent(REGISTER_CHANGE_ACTION);
            broadIntent.PutExtra(EXTRA_REGISTER_STATE, statusText);
           sendPortSipMessage("onRegisterFailure" + statusCode, broadIntent);
            CallManager.Instance.regist = false;
            CallManager.Instance.ResetAll();

            Helper.Settings.UserStatus = "OffLine";
            UnRegisterServer();
           
            LoginPageModel.TransportList.Remove(Helper.Settings.SipServerType);
            Helper.Settings.SipServerType = LoginPageModel.TransportList.FirstOrDefault().Key;
            Helper.Settings.SipServerPort = LoginPageModel.TransportList.FirstOrDefault(t => t.Key == Helper.Settings.SipServerType).Value;
            AutoRegister();
        }
        public void AutoRegister()
        {
            IntiliaztePortSdk(Helper.Settings.SipServerType);
            LicenceKey();
            SetUserData(Helper.Settings.UserName, Helper.Settings.DisplayName,
                       Helper.Settings.AuthName, Helper.Settings.Password, Helper.Settings.UserDomain, Helper.Settings.SipServer,
                      Helper.Settings.SipServerPort, Helper.Settings.STUNServer, Helper.Settings.STUNServerPort);
            RegisterToServer(Helper.Settings.SRTPPolicy);
        }
        public void onRegisterSuccess(string statusText, int statusCode, string sipMessage)
        {
            Console.WriteLine("Register success -> {0}", statusText);
            Helper.Settings.UserStatus = "Online";
            CallManager.Instance.regist = true;
            Intent broadIntent = new Intent(REGISTER_CHANGE_ACTION);
            broadIntent.PutExtra(EXTRA_REGISTER_STATE, statusText);
            //Intent onLineIntent = new Intent(MainActivity.Instance, typeof(PortSipService));
            //onLineIntent.SetAction(PortSipService.REGISTER_CHANGE_ACTION);
            //  MainActivity.Instance.StartService(onLineIntent);
            sendPortSipMessage("onRegisterSuccess", broadIntent);
          
            //publish presence status to online.
            mEngine.setPresenceStatus(0, "online");
        }
        public void onSendingSignaling(long sessionId, string signaling)
        {
            throw new NotImplementedException();
        }

        public void onSendMessageFailure(long sessionId, long messageId, string reason, int code)
        {
            throw new NotImplementedException();
        }

        public void onSendMessageSuccess(long sessionId, long messageId)
        {
            throw new NotImplementedException();
        }

        public void onSendOutOfDialogMessageFailure(long messageId, string fromDisplayName, string from, string toDisplayName, string to, string reason, int code)
        {
            throw new NotImplementedException();
        }

        public void onSendOutOfDialogMessageSuccess(long messageId, string fromDisplayName, string from, string toDisplayName, string to)
        {
            throw new NotImplementedException();
        }

        public void onSubscriptionFailure(long subscribeId, int statusCode)
        {
            throw new NotImplementedException();
        }

        public void onSubscriptionTerminated(long subscribeId)
        {
            throw new NotImplementedException();
        }
        public void onReceivedRefer(long sessionId, long referId, string to, string referFrom, string referSipMessage)
        {
            Console.WriteLine("{0} onReceivedRefer", sessionId);
        }

        public void onReferAccepted(long sessionId)
        {
            Session session = CallManager.Instance.FindSessionBySessionID(sessionId);
            if (session != null)
            {
                session.state = CALL_STATE_FLAG.CLOSED;
                session.SessionID = sessionId;

                Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
                broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
                String description = session.LineName + " OnInviteClosed";
                broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);

                sendPortSipMessage(description, broadIntent);
            }
            Ring.getInstance(MainActivity.Instance).stopRingTone();
        }

        public void onReferRejected(long sessionId, string reason, int code) { }

        public void onTransferTrying(long sessionId) { }

        public void onTransferRinging(long sessionId) { }

        public void onACTVTransferSuccess(long sessionId)
        {
            Console.WriteLine("onACTVTransferSuccess:{0}", sessionId);

            //Transfer has success, hangup call.
            mEngine.hangUp(sessionId);
        }

        public void onACTVTransferFailure(long sessionId, string reason, int code) { }

        public void onReceivedSignaling(long sessionId, string signaling) { }
       
        public void onWaitingVoiceMessage(string messageAccount, int urgentNewMessageCount, int urgentOldMessageCount, int newMessageCount, int oldMessageCount) { }

        public void onWaitingFaxMessage(string messageAccount, int urgentNewMessageCount, int urgentOldMessageCount, int newMessageCount, int oldMessageCount) { }

        public void onRecvDtmfTone(long sessionId, int tone) { }

        public void onRecvOptions(string optionsMessage) { }

        public void onRecvInfo(string infoMessage) { }
        private TRANSPORT_TYPE GetTransType(int select)
        {
            switch (select)
            {
                case 0: return TRANSPORT_TYPE.TRANSPORT_UDP;
                case 1: return TRANSPORT_TYPE.TRANSPORT_TLS;
                case 2: return TRANSPORT_TYPE.TRANSPORT_TCP;
                case 3: return TRANSPORT_TYPE.TRANSPORT_PERS_UDP;
                case 4: return TRANSPORT_TYPE.TRANSPORT_PERS_TCP;
            }
            return TRANSPORT_TYPE.TRANSPORT_UDP;
        }
        #endregion
        //private TRANSPORT_TYPE GetTransType(string _transtype)
        //{
        //    if (_transtype.ToUpper().Equals(TransportType.UDP))
        //    {
        //        return TRANSPORT_TYPE.TRANSPORT_UDP;
        //    }

        //    else if (_transtype.ToUpper().Equals(TransportType.TLS))
        //    {
        //      return  TRANSPORT_TYPE.TRANSPORT_TLS;
        //    }
        //    else if (_transtype.ToUpper().Equals(TransportType.TCP))
        //    {
        //        return TRANSPORT_TYPE.TRANSPORT_TCP;
        //    }
        //    return TRANSPORT_TYPE.TRANSPORT_UDP;
        //}
        String getInstanceID()
        {
      

            string insanceid = "";
            if (String.IsNullOrEmpty(insanceid))
            {
                insanceid = new Guid().ToString();
               
            }
            return insanceid;
        }
        private SRTP_POLICY GetSRTPPolicy(string _srtptype)
        {

            if (_srtptype.ToUpper().Equals(SRTPTypes.NONE))
            {
                srtptype = SRTP_POLICY.SRTP_POLICY_NONE;
            }

            else if (_srtptype.ToUpper().Equals(SRTPTypes.FORCE))
            {
                srtptype = SRTP_POLICY.SRTP_POLICY_FORCE;
            }
            else if (_srtptype.ToUpper().Equals(SRTPTypes.PREFER))
            {
                srtptype = SRTP_POLICY.SRTP_POLICY_PREFER;
            }
            return srtptype;

        }
        public string IntiliaztePortSdk(string siptype)
        {
            string registerTips="";
            int result = -1;
            #region  portsipsdk.intlialize
            String dataPath = "";
            var FilesDir = Application.Context.GetExternalFilesDir(null);
            if (FilesDir != null)
            {
                dataPath = FilesDir.AbsolutePath;
            }

            Random rm = new Random();
            int localport = rm.Next(5060, 65535);
            result = mEngine.initialize(GetTransType(2), "0.0.0.0", localport,

                PORTSIP_LOG_LEVEL.PORTSIP_LOG_DEBUG, dataPath,
                8, "InnoCalls Softphone Beta", 0, 0, dataPath, "", false);
            if (result != 0)
            {
                registerTips = "initialize failed";
               // onRegisterFailure(registerTips, result, null);
                
            }
            #endregion
            return registerTips;
        }
        public string LicenceKey()
        {
            string registerTips= "";
            int result = -1;
            #region  setlicencekey
            result = mEngine.setLicenseKey("1AND0R0yQjM4OTcwRTYzOEM1ODk1QzRFQTEzMEM3QjlBQjdGMkBBOEVDQzA1ODAzRjJCMjc3OEYyQTlEMkNDNEMyRUNDMEA5NzFGOEY4NjQxMTc0QjA1REU2OTFBQTlBMEZCN0M5OUA4NzczOUVDODI0NTlGOEE0NTkxQUYzNUYxNzI2RTIxRQ");
            if (result == 0)
            {
                Console.WriteLine("This Official version SDK .");
            }
            else if (result == (int)ERROR_CODES.ECoreTrialVersionLicenseKey)
            {
                Console.WriteLine("This trial version SDK just allows short conversation, you can't heairng anyting after 2-3 minutes, contact us: sales@portsip.com to buy official version.");
            }
            else if (result == (int)ERROR_CODES.ECoreWrongLicenseKey)
            {
                registerTips = "Wrong License Key";
               // onRegisterFailure(registerTips, result, null);
            
            }
            else
            {
                registerTips = "Unknow Error, contact us: sales@portsip.com";
               // onRegisterFailure(registerTips, result, null);
                
            }
            return registerTips;
            #endregion
        }
        public string SetUserData(String username, String displayname, String authname, String password, 
            String userdomain, String sipserver, int serverport, String stunserver, int stunport)
        {
       
            string registerTips="";
            int result = -1;    
          
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)
                || String.IsNullOrEmpty(sipserver) || String.IsNullOrEmpty(serverport.ToString()))
            {
                registerTips = "Invalidate user info";
               
                return registerTips;
            }
            result = mEngine.setUser(username, displayname, authname, password,
               userdomain, sipserver, serverport, stunserver,3478, null, 0);
            if(result==0)
            {
                registerTips = "0";
              //  onRegisterSuccess(registerTips, result, null);
                return registerTips;
            }
           else if (result != 0)
            {
                registerTips = "setUser failed"; 
                return registerTips;
            }
           

            return registerTips;
        }
        public string RegisterToServer(string srtptype)
        {
            int result = -1;
            string registerTips = "";
            SetSettings(srtptype);
            mEngine.setInstanceId(getInstanceID());
            result = mEngine.registerServer(120, 0);
            
            if(result==0)
            {
                CallManager.Instance.regist = true;
                   registerTips = "0";
                Helper.Settings.UserStatus = "Online";
                onRegisterSuccess(registerTips, result, null);
           
            }
            if (result != 0)
            {
                registerTips = "registerServer failed";

                onRegisterFailure(registerTips, result, null);
             
              
            }
            return registerTips;
        }
        public void SetSettings(string srtptype)
        {
          
            mEngine.setLoudspeakerStatus(false);
      
            mEngine.getAudioDevices();

            //set srtp
            mEngine.setSrtpPolicy(GetSRTPPolicy(srtptype));
            //Set Presence Agent Mode
            mEngine.setPresenceMode(1);
            
       
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G722);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
              
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);
         
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_GSM);
          
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_ILBC);
           
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_AMR);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_AMRWB);

              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_SPEEX);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_SPEEXWB);
             
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_ISACWB);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_ISACSWB);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G7221);
            
              mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_OPUS);
          
            mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
            mEngine.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);
            mEngine.addVideoCodec(VIDEOCODEC_TYPE.VIDEO_CODEC_H264);
            mEngine.enableAEC(true);
            mEngine.enableAGC(true);
            mEngine.enableCNG(true);
            mEngine.enableVAD(true);
            mEngine.enableANS(false);
            mEngine.setVideoBitrate(-1, 300);
            mEngine.setVideoFrameRate(-1, 10);
            mEngine.setVideoResolution(352, 288);
            mEngine.setAudioSamples(20, 60);//ptime 20 maxptime 60
                                            //enable video nack
            mEngine.setVideoNackStatus(true);

            //1 - FrontCamra 0 - BackCamra
            mEngine.setVideoDeviceId(1);

            //disable 3Gpp Tags
            mEngine.enable3GppTags(false);

            //disable rport
            mEngine.enableRport(false);
            //added PUSH Header with PortPBX, to support Android FCM PUSH

          //  setPushHeader(NeedPush);





        }

        public void UnRegisterServer()
        {
    //setPushHeader(false);

            //Set Presence status
            mEngine.setPresenceStatus(0, "unpublish");
            mEngine.unRegisterServer();
          
            mEngine.unInitialize();
            CallManager.Instance.regist = false;
          
        }

      

        void SendTokenRefreshToServer(string token)
        {
            Intent srvIntent = new Intent(this, typeof(PortSipService));
            srvIntent.SetAction("com.companyname.incalltask.PushToken");
            srvIntent.PutExtra("TOKEN", token);
            StartService(srvIntent);
            // Add custom implementation, as needed.
        }
        public long Call(string callee, bool sendSdp, bool videoCall)
        {
         return   mEngine.call(callee, sendSdp, videoCall);
        }
        public void KeySound(string digit,long sessionid)
        {
            int sum = Convert.ToInt32(digit);
            Ring.getInstance(MainActivity.Instance).startKeypadTone(sum);
            mEngine.sendDtmf(sessionid, PortSipEnumDefine.EnumDtmfMothodRfc2833, sum,
                160, true);
            if (digit == "*")
            {
                mEngine.sendDtmf(sessionid, PortSipEnumDefine.EnumDtmfMothodRfc2833, 10,
                        160, true);
                return;
            }
            if (digit == "#")
            {
                mEngine.sendDtmf(sessionid, PortSipEnumDefine.EnumDtmfMothodRfc2833, 11,
                        160, true);
                return;
            }
        }
        public void onInviteIncoming(long sessionId, string callerDisplayName, string caller, string calleeDisplayName, string callee, string audioCodecNames, string videoCodecNames, bool existsAudio, bool existsVideo, string sipMessage)
        {
            Console.WriteLine("Incoming call ({4}) from {0} {1} to {2} {3}", caller, callerDisplayName, callee, calleeDisplayName, sessionId);

            var a = betweenStrings(caller, ":", "@");

            Session session = CallManager.Instance.FindIdleSession();
            session.state = CALL_STATE_FLAG.INCOMING;
            session.HasVideo = existsVideo;
            session.SessionID = sessionId;
            session.Remote = caller;
            session.DisplayName = a;

            String description = session.LineName + " onInviteIncoming";
            Intent broadIntent = new Intent(CALL_CHANGE_ACTION);
            broadIntent.PutExtra(EXTRA_CALL_SEESIONID, sessionId);
            broadIntent.PutExtra(EXTRA_CALL_DESCRIPTION, description);

            sendPortSipMessage(description, broadIntent);
            Ring.getInstance(MainActivity.Instance).startRingTone();
   
           
            Console.WriteLine(description);


        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Console.WriteLine("PortSipService: {0} ", "Start Service");
            if (intent != null)
            {
                if (ACTION_PUSH_MESSAGE.Equals(intent.Action) && !CallManager.Instance.regist)
                {
                    registerToServer();
                }
                else if (ACTION_SIP_REGIEST.Equals(intent.Action) && !CallManager.Instance.regist)
                {
                    registerToServer();
                }
                else if (ACTION_SIP_UNREGIEST.Equals(intent.Action) && CallManager.Instance.regist)
                {
                    UnRegisterServer();
                }
                else if (ACTION_PUSH_TOKEN.Equals(intent.Action))
                {
                    PushToken = intent.GetStringExtra(EXTRA_PUSHTOKEN);

                    if (CallManager.Instance.regist)
                    {
                        setPushHeader(NeedPush);
                    }

                    //to do. token changed，in order to receive push message. if not online ,according to your strategy，maybe you need to regist .
                }
            }
            return StartCommandResult.Sticky;
        }

        private void registerToServer()
        {
            IntiliaztePortSdk("TCP");
            LicenceKey();
            SetUserData("7481122", "", "", "rB9*E6CcC-dOQ2N*", "", "142.93.121.73",5060, "",3478);
            RegisterToServer("NONE");
        }

        public void sendPortSipMessage(String message, Intent broadIntent)
        {
            try
            {
                //NotificationManager nm = NotificationManager.FromContext(MainActivity.Instance);
                //Intent intent = new Intent(MainActivity.Instance, typeof(MainActivity));
                //PendingIntent contentIntent = PendingIntent.GetActivity(MainActivity.Instance, 0, intent, 0);

                //Notification notification = new Notification.Builder(MainActivity.Instance)
                //    .SetSmallIcon(Resource.Drawable.income)
                //    .SetContentTitle("Sip Notify")
                //    .SetContentText(message)
                //    .SetContentIntent(contentIntent)
                //    .Build();// getNotification()

                //nm.Notify(1, notification);

                MainActivity.Instance.SendBroadcast(broadIntent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        private void setPushHeader(bool support)
        {
            if (!String.IsNullOrEmpty(PushToken))
            {
                String pushMessage = "device-os=android;device-uid=" + PushToken + ";allow-call-push=" + support + ";allow-message-push=" + support + ";app-id=" + APPID;
                mEngine.addSipMessageHeader(-1, "REGISTER", 1, "x-p-push", pushMessage);
            }

        }
        public void Start()
        {
            Intent onLineIntent = new Intent(MainActivity.Instance, typeof(PortSipService));
            onLineIntent.SetAction(PortSipService.ACTION_SIP_REGIEST);
            MainActivity.Instance.StartService(onLineIntent);
        }
    }
}
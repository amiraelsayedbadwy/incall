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
using Com.Portsip;
using incalltask.Interface;
using PortSIP;

namespace incalltask.Droid.Service
{
    public class PortSIPLib : Java.Lang.Object, Interface.IPortSIPLib, IOnPortSIPEvent
    {
        private PortSipSdk sdk = new PortSipSdk();
        private Interface.IPortSIPEvents events;
        Context appContext;

        public PortSIPLib(Context applicationContext, Interface.IPortSIPEvents sipEvents)
        {
           
            appContext = applicationContext;
            events = sipEvents;
        }
        public PortSIPLib(Context applicationContext)
        {
            
            appContext = applicationContext;
            events = null;
        }
        public void SetPortSIPEventsCallback(Interface.IPortSIPEvents sipEvents)
        {
            events = sipEvents;
        }

        ~PortSIPLib()
        {
            unInitialize();
        }

        public int initialize(
            TRANSPORT_TYPE transportType,
            string localIp,
            int port,
            PORTSIP_LOG_LEVEL logLevel,
            string logFilePath,
            int maxCallLines,
            string sipAgent,
            int audioDeviceLayer,
            int videoDeviceLayer,
            string tlsCertificatesRootPath,
            string tlsCipherList,
            bool verifyTLSCertificate)
        {
            try
            {
                sdk.CreateCallManager(appContext);
            }
            catch (Exception e)
            {

                throw e;
            }
           
            sdk.SetOnPortSIPEvent(this);
            string dnsServer = "";
            return sdk.Initialize((int)transportType, localIp, port,
                                  (int)logLevel, logFilePath, maxCallLines, sipAgent,
                                  audioDeviceLayer, videoDeviceLayer,
                                  tlsCertificatesRootPath, tlsCipherList, verifyTLSCertificate, dnsServer);
        }



        public void unInitialize()
        {
            sdk.DeleteCallManager();
            //sdk.Dispose();
        }


        public int setUser(string userName,
            string displayName,
            string authName,
            string password,
            string userDomain,
            string sipServer,
            int sipServerPort,
            string stunServer,
            int stunServerPort,
            string outboundServer,
            int outboundServerPort)
        {
            return sdk.SetUser(userName, displayName, authName, password,
                userDomain, sipServer, sipServerPort, stunServer, stunServerPort, outboundServer, outboundServerPort);
        }


        public int registerServer(int expires, int retryTimes)
        {
            return sdk.RegisterServer(expires, retryTimes);
        }

        public int unRegisterServer()
        {
            return sdk.UnRegisterServer();
        }

        public int setInstanceId(string instanceId)
        {
            return sdk.SetInstanceId(instanceId);
        }

        public int setLicenseKey(string key)
        {
            return sdk.SetLicenseKey(key);
        }


        public int addAudioCodec(AUDIOCODEC_TYPE codecType)
        {
            return sdk.AddAudioCodec((int)codecType);
        }

        public int addVideoCodec(VIDEOCODEC_TYPE codecType)
        {
            return sdk.AddVideoCodec((int)codecType);
        }


        public bool isAudioCodecEmpty()
        {
            return sdk.IsAudioCodecEmpty;
        }


        public bool isVideoCodecEmpty()
        {
            return sdk.IsVideoCodecEmpty;
        }


        public int setAudioCodecPayloadType(AUDIOCODEC_TYPE codecType, int payloadType)
        {
            return sdk.SetAudioCodecPayloadType((int)codecType, payloadType);
        }

        public int setVideoCodecPayloadType(VIDEOCODEC_TYPE codecType, int payloadType)
        {
            return sdk.SetVideoCodecPayloadType((int)codecType, payloadType);
        }


        public void clearAudioCodec()
        {
            sdk.ClearAudioCodec();
        }


        public void clearVideoCodec()
        {
            sdk.ClearVideoCodec();
        }


        public int setAudioCodecParameter(AUDIOCODEC_TYPE codecType, string parameter)
        {
            return sdk.SetAudioCodecParameter((int)codecType, parameter);
        }

        public int setVideoCodecParameter(VIDEOCODEC_TYPE codecType, string parameter)
        {
            return sdk.SetVideoCodecParameter((int)codecType, parameter);
        }


        public String getVersion()
        {
            return "16.3";
        }

        public int enableRport(bool enable)
        {
            return sdk.EnableRport(enable);
        }

        public int enableEarlyMedia(bool enable)
        {
            return sdk.EnableEarlyMedia(enable);
        }

        public int enableReliableProvisional(bool enable)
        {
            return sdk.EnableReliableProvisional(enable);
        }


        public int enable3GppTags(bool enable)
        {
            return sdk.Enable3GppTags(enable);
        }

        public void enableCallbackSignaling(bool enableSending, bool enableReceived)
        {
            sdk.EnableCallbackSignaling(enableSending, enableReceived);
        }



        public int setSrtpPolicy(SRTP_POLICY srtpPolicy)
        {
            sdk.SetSrtpPolicy((int)srtpPolicy);
            return 0;
        }



        public int setRtpPortRange(int minimumRtpAudioPort, int maximumRtpAudioPort, int minimumRtpVideoPort, int maximumRtpVideoPort)
        {
            return sdk.SetRtpPortRange(minimumRtpAudioPort, maximumRtpAudioPort, minimumRtpVideoPort, maximumRtpVideoPort);
        }


        public int setRtcpPortRange(int minimumRtcpAudioPort, int maximumRtcpAudioPort, int minimumRtcpVideoPort, int maximumRtcpVideoPort)
        {
            return sdk.SetRtcpPortRange(minimumRtcpAudioPort, maximumRtcpAudioPort, minimumRtcpVideoPort, maximumRtcpVideoPort);
        }



        public int enableCallForward(bool forBusyOnly, string forwardTo)
        {
            return sdk.EnableCallForward(forBusyOnly, forwardTo);
        }

        public int disableCallForward()
        {
            return sdk.DisableCallForward();
        }



        public int enableSessionTimer(int timerSeconds, SESSION_REFRESH_MODE refreshMode)
        {
            return sdk.EnableSessionTimer(timerSeconds);
        }


        public int disableSessionTimer()
        {
            sdk.DisableSessionTimer();
            return 0;
        }


        public void setDoNotDisturb(bool state)
        {
            sdk.SetDoNotDisturb(state);
        }


        //public int detectMwi()
        //{
        //	return sdk.DetectMwi ();
        //}


        //public int enableCheckMwi(bool state)
        //{
        //	return sdk.EnableCheckMwi (state);
        //}

        public int setRtpKeepAlive(bool state, int keepAlivePayloadType, int deltaTransmitTimeMS)
        {
            return sdk.SetRtpKeepAlive(state, keepAlivePayloadType, deltaTransmitTimeMS);
        }


        public int setKeepAliveTime(int keepAliveTime)
        {
            return sdk.SetKeepAliveTime(keepAliveTime);
        }



        public int setAudioSamples(int ptime, int maxPtime)
        {
            return sdk.SetAudioSamples(ptime, maxPtime);
        }


        public int addSupportedMimeType(string methodName, string mimeType, string subMimeType)
        {
            return sdk.AddSupportedMimeType(methodName, mimeType, subMimeType);
        }


        public int getSipMessageHeaderValue(string sipMessage, string headerName, out string headerValue, int headerValueLength)
        {
            headerValue = sdk.GetSipMessageHeaderValue(sipMessage, headerName);
            return 0;
        }


        public long addSipMessageHeader(long sessionId, string methodName, int msgType, string headerName, string headerValue)
        {
            return sdk.AddSipMessageHeader(sessionId, methodName, msgType, headerName, headerValue);
        }

        public int removeAddedSipMessageHeader(int addedSipMessageId)
        {
            return sdk.RemoveAddedSipMessageHeader(addedSipMessageId);
        }

        public void clearAddedSipMessageHeaders()
        {
            sdk.ClearAddedSipMessageHeaders();
        }

        public long modifySipMessageHeader(long sessionId, string methodName, int msgType, string headerName, string headerValue)
        {
            return sdk.ModifySipMessageHeader(sessionId, methodName, msgType, headerName, headerValue);
        }

        public int removeModifiedSipMessageHeader(int addedSipMessageId)
        {
            return sdk.RemoveModifiedSipMessageHeader(addedSipMessageId);
        }

        public void clearModifiedSipMessageHeaders()
        {
            sdk.ClearModifiedSipMessageHeaders();
        }


        public int setVideoDeviceId(int deviceId)
        {
            return sdk.SetVideoDeviceId(deviceId);
        }


        public int setVideoResolution(int width, int height)
        {
            return sdk.SetVideoResolution(width, height);
        }

        public int setVideoCropAndScale(bool enable)
        {
            return sdk.SetVideoCropAndScale(enable);
        }

        public int setAudioBitrate(long sessionId, AUDIOCODEC_TYPE codecType, int bitrateKbps)
        {
            return sdk.SetAudioBitrate(sessionId, (int)codecType, bitrateKbps);
        }

        public int setVideoBitrate(long sessionId, int bitrateKbps)
        {
            return sdk.SetVideoBitrate(sessionId, bitrateKbps);
        }


        public int setVideoFrameRate(long sessionId, int frameRate)
        {
            return sdk.SetVideoFrameRate(sessionId, frameRate);
        }


        public int sendVideo(long sessionId, bool sendState)
        {
            return sdk.SendVideo(sessionId, sendState);
        }


        public ICollection<PortSipEnumDefine.AudioDevice> getAudioDevices()
        {
            return sdk.AudioDevices;
        }

        public int setAudioDevice(PortSipEnumDefine.AudioDevice device)
        {
            return sdk.SetAudioDevice(device);
        }

        public int getAudioStatistics(long sessionId,
                               out int sendBytes,
                               out int sendPackets,
                               out int sendPacketsLost,
                               out int sendFractionLost,
                               out int sendRttMS,
                               out int sendCodecType,
                               out int sendJitterMS,
                               out int sendAudioLevel,
                               out int recvBytes,
                               out int recvPackets,
                               out int recvPacketsLost,
                               out int recvFractionLost,
                               out int recvCodecType,
                               out int recvJitterMS,
                               out int recvAudioLevel)
        {
            sendBytes = 0;
            sendPackets = 0;
            sendPacketsLost = 0;
            sendFractionLost = 0;
            sendRttMS = 0;
            sendCodecType = 0;
            sendJitterMS = 0;
            sendAudioLevel = 0;
            recvBytes = 0;
            recvPackets = 0;
            recvPacketsLost = 0;
            recvFractionLost = 0;
            recvCodecType = 0;
            recvJitterMS = 0;
            recvAudioLevel = 0;

            int[] statistics = new int[15];
            int ret = sdk.GetAudioStatistics(sessionId, statistics);
            if (ret == 0)
            {
                sendBytes = statistics[0];
                sendPackets = statistics[1];
                sendPacketsLost = statistics[2];
                sendFractionLost = statistics[3];
                sendRttMS = statistics[4];
                sendCodecType = statistics[5];
                sendJitterMS = statistics[6];
                sendAudioLevel = statistics[7];
                recvBytes = statistics[8];
                recvPackets = statistics[9];
                recvPacketsLost = statistics[10];
                recvFractionLost = statistics[11];
                recvCodecType = statistics[12];
                recvJitterMS = statistics[13];
                recvAudioLevel = statistics[14];
            }
            return ret;
        }


        public int getVideoStatistics(long sessionId,
                                      out int sendBytes,
                                      out int sendPackets,
                                      out int sendPacketsLost,
                                      out int sendFractionLost,
                                      out int sendRttMS,
                                      out int sendCodecType,
                                      out int sendFrameWidth,
                                      out int sendFrameHeight,
                                      out int sendBitrateBPS,
                                      out int sendFramerate,
                                      out int recvBytes,
                                      out int recvPackets,
                                      out int recvPacketsLost,
                                      out int recvFractionLost,
                                      out int recvCodecType,
                                      out int recvFrameWidth,
                                      out int recvFrameHeight,
                                      out int recvBitrateBPS,
                                      out int recvFramerate)
        {
            sendBytes = 0;
            sendPackets = 0;
            sendPacketsLost = 0;
            sendFractionLost = 0;
            sendRttMS = 0;
            sendCodecType = 0;
            sendFrameWidth = 0;
            sendFrameHeight = 0;
            sendBitrateBPS = 0;
            sendFramerate = 0;
            recvBytes = 0;
            recvPackets = 0;
            recvPacketsLost = 0;
            recvFractionLost = 0;
            recvCodecType = 0;
            recvFrameWidth = 0;
            recvFrameHeight = 0;
            recvBitrateBPS = 0;
            recvFramerate = 0;

            int[] statistics = new int[19];
            int ret = sdk.GetVideoStatistics(sessionId, statistics);
            if (ret == 0)
            {
                sendBytes = statistics[0];
                sendPackets = statistics[1];
                sendPacketsLost = statistics[2];
                sendFractionLost = statistics[3];
                sendRttMS = statistics[4];
                sendCodecType = statistics[5]; ;
                sendFrameWidth = statistics[6];
                sendFrameHeight = statistics[7];
                sendBitrateBPS = statistics[8];
                sendFramerate = statistics[9];
                recvBytes = statistics[10];
                recvPackets = statistics[11];
                recvPacketsLost = statistics[12];
                recvFractionLost = statistics[13];
                recvCodecType = statistics[14];
                recvFrameWidth = statistics[15];
                recvFrameHeight = statistics[16];
                recvBitrateBPS = statistics[17];
                recvFramerate = statistics[18];
            }
            return ret;
        }


        //*/
        /*
        public void setLocalVideoWindow(IntPtr localVideoWindow)
        {
            sdk.SetLocalVideoWindow (localVideoWindow);
        }

        public int setRemoteVideoWindow(long sessionId, IntPtr remoteVideoWindow)
        {
            return sdk.SetRemoteVideoWindow (sessionId, remoteVideoWindow);
        }
        */
        ///*
        public int displayLocalVideo(bool state)
        {
            sdk.DisplayLocalVideo(state);
            return 0;
        }


        public int setVideoNackStatus(bool state)
        {
            return sdk.SetVideoNackStatus(state);
        }

        public int setLoudspeakerStatus(bool enable)
        {
            return 0;
        }


        public int setChannelOutputVolumeScaling(long sessionId, int scaling)
        {
            return sdk.SetChannelOutputVolumeScaling(sessionId, scaling);
        }

        public int setChannelInputVolumeScaling(long sessionId, int scaling)
        {
            return sdk.SetChannelInputVolumeScaling(sessionId, scaling);
        }

        public long call(string callee, bool sendSdp, bool videoCall)
        {
            return sdk.Call(callee, sendSdp, videoCall);
        }


        public int rejectCall(long sessionId, int code)
        {
            return sdk.RejectCall(sessionId, code);
        }


        public int hangUp(long sessionId)
        {
            return sdk.HangUp(sessionId);
        }

        public int answerCall(long sessionId, bool videoCall)
        {
            return sdk.AnswerCall(sessionId, videoCall);
        }


        public int updateCall(long sessionId, bool enableAudio, bool enableVideo)
        {
            return sdk.UpdateCall(sessionId, enableAudio, enableVideo);
        }


        public int hold(long sessionId)
        {
            return sdk.Hold(sessionId);
        }


        public int unHold(long sessionId)
        {
            return sdk.UnHold(sessionId);
        }


        public int muteSession(long sessionId,
            bool muteIncomingAudio,
            bool muteOutgoingAudio,
            bool muteIncomingVideo,
            bool muteOutgoingVideo)
        {
            return sdk.MuteSession(sessionId, muteIncomingAudio, muteOutgoingAudio, muteIncomingVideo, muteOutgoingVideo);
        }


        public int forwardCall(long sessionId, string forwardTo)
        {
            return sdk.ForwardCall(sessionId, forwardTo);
        }


        public int sendDtmf(long sessionId, DTMF_METHOD dtmfMethod, int code, int dtmfDuration, bool playDtmfTone)
        {
            return sdk.SendDtmf(sessionId, (int)dtmfMethod, code, dtmfDuration, playDtmfTone);
        }


        public int refer(long sessionId, string referTo)
        {
            return sdk.Refer(sessionId, referTo);
        }


        public int attendedRefer(long sessionId, long replaceSessionId, string referTo)
        {
            return sdk.AttendedRefer(sessionId, replaceSessionId, referTo);
        }

        public int attendedRefer2(long sessionId, int replaceSessionId, string replaceMethod, string target, string referTo)
        {
            return sdk.AttendedRefer2((long)sessionId, (long)replaceSessionId, replaceMethod, target, referTo);
        }

        int outOfDialogRefer(int replaceSessionId, string replaceMethod, string target, string referTo)
        {
            return sdk.OutOfDialogRefer(replaceSessionId, replaceMethod, target, referTo);
        }

        public long acceptRefer(int referId, string referSignalingMessage)
        {
            return sdk.AcceptRefer(referId, referSignalingMessage);
        }


        public int rejectRefer(int referId)
        {
            return sdk.RejectRefer(referId);
        }

        public int enableSendPcmStreamToRemote(long sessionId, bool state, int streamSamplesPerSec)
        {
            return sdk.EnableSendPcmStreamToRemote(sessionId, state, streamSamplesPerSec);
        }


        public int sendPcmStreamToRemote(long sessionId, byte[] data, int dataLength)
        {
            return sdk.SendPcmStreamToRemote(sessionId, data, dataLength);
        }


        public int enableSendVideoStreamToRemote(long sessionId, bool state)
        {
            return sdk.EnableSendVideoStreamToRemote(sessionId, state);
        }


        public int sendVideoStreamToRemote(long sessionId, byte[] data, int dataLength, int width, int height)
        {
            return sdk.SendVideoStreamToRemote(sessionId, data, dataLength, width, height);
        }

        public int setRtpCallback(bool enable)
        {
            sdk.SetRtpCallback(enable);
            return 0;
        }

        public int enableAudioStreamCallback(long sessionId, bool enable, AUDIOSTREAM_CALLBACK_MODE callbackMode)
        {
            sdk.EnableAudioStreamCallback(sessionId, enable, (int)callbackMode);
            return 0;
        }


        public int enableVideoStreamCallback(long sessionId, VIDEOSTREAM_CALLBACK_MODE callbackMode)
        {
            sdk.EnableVideoStreamCallback(sessionId, (int)callbackMode);
            return 0;
        }

        public int startRecord(long sessionId,
            string recordFilePath,
            string recordFileName,
            bool appendTimestamp,
            AUDIO_RECORDING_FILEFORMAT audioFileFormat,
            RECORD_MODE audioRecordMode,
            VIDEOCODEC_TYPE videoFileCodecType,
            RECORD_MODE videoRecordMode)
        {
            return sdk.StartRecord(sessionId,
                recordFilePath,
                recordFileName,
                appendTimestamp,
                (int)audioFileFormat,
                (int)audioRecordMode,
                (int)videoFileCodecType,
                (int)videoRecordMode);
        }


        public int stopRecord(long sessionId)
        {
            return sdk.StopRecord(sessionId);
        }


        public int playVideoFileToRemote(long sessionId, string fileName, bool loop, bool playAudio)
        {
            return sdk.PlayVideoFileToRemote(sessionId, fileName, loop, playAudio);
        }

        public int stopPlayVideoFileToRemote(long sessionId)
        {
            return sdk.StopPlayVideoFileToRemote(sessionId);
        }


        public int playAudioFileToRemote(long sessionId, string fileName, int fileSamplesPerSec, bool loop)
        {
            return sdk.PlayAudioFileToRemote(sessionId, fileName, fileSamplesPerSec, loop);
        }



        public int stopPlayAudioFileToRemote(long sessionId)
        {
            return sdk.StopPlayAudioFileToRemote(sessionId);
        }


        public int playAudioFileToRemoteAsBackground(long sessionId, string fileName, int fileSamplesPerSec)
        {
            return sdk.PlayAudioFileToRemoteAsBackground(sessionId, fileName, fileSamplesPerSec);
        }


        public int stopPlayAudioFileToRemoteAsBackground(long sessionId)
        {
            return sdk.StopPlayAudioFileToRemoteAsBackground(sessionId);
        }


        public void destroyConference()
        {
            sdk.DestroyConference();
        }

        public int joinToConference(long sessionId)
        {
            return sdk.JoinToConference(sessionId);
        }


        public int removeFromConference(long sessionId)
        {
            return sdk.RemoveFromConference(sessionId);
        }

        public int setAudioRtcpBandwidth(long sessionId,
            int BitsRR,
            int BitsRS,
            int KBitsAS)
        {
            return sdk.SetAudioRtcpBandwidth(sessionId, BitsRR, BitsRS, KBitsAS);
        }


        public int setVideoRtcpBandwidth(long sessionId,
            int BitsRR,
            int BitsRS,
            int KBitsAS)
        {
            return sdk.SetVideoRtcpBandwidth(sessionId, BitsRR, BitsRS, KBitsAS);
        }


        public int enableAudioQos(bool state)
        {
            return sdk.EnableAudioQos(state);
        }


        public int enableVideoQos(bool state)
        {
            return sdk.EnableVideoQos(state);
        }

        public int setVideoMTU(int mtu)
        {
            return sdk.SetVideoMTU(mtu);
        }


        public void enableVAD(bool state)
        {
            sdk.EnableVAD(state);
        }


        public void enableAEC(bool state)
        {
            sdk.EnableAEC(state);
        }

        public void removeUser()
        {
            sdk.RemoveUser();
        }

        public void enableCNG(bool state)
        {
            sdk.EnableCNG(state);
        }


        public void enableAGC(bool state)
        {
            sdk.EnableAGC(state);
        }

        public void enableANS(bool state)
        {
            sdk.EnableANS(state);
        }

        public int sendOptions(string to, string sdp)
        {
            return sdk.SendOptions(to, sdp);
        }


        public int sendInfo(long sessionId, string mimeType, string subMimeType, string infoContents)
        {
            return sdk.SendInfo(sessionId, mimeType, subMimeType, infoContents);
        }

        public long sendMessage(long sessionId, string mimeType, string subMimeType, byte[] message, int messageLength)
        {
            return sdk.SendMessage(sessionId, mimeType, subMimeType, message, messageLength);
        }


        public long sendOutOfDialogMessage(string to, string mimeType, string subMimeType, bool isSMS, byte[] message, int messageLength)
        {
            return sdk.SendOutOfDialogMessage(to, mimeType, subMimeType, isSMS, message, messageLength);
        }


        public long presenceSubscribe(string contact, string subject)
        {
            return sdk.PresenceSubscribe(contact, subject);
        }


        public int presenceRejectSubscribe(long subscribeId)
        {
            return sdk.PresenceRejectSubscribe(subscribeId);
        }


        public int presenceAcceptSubscribe(long subscribeId)
        {
            return sdk.PresenceAcceptSubscribe(subscribeId);
        }

        public int setPresenceStatus(long subscribeId, string stateText)
        {
            return sdk.SetPresenceStatus(subscribeId, stateText);
        }

        public int refreshRegistration(int expires)
        {
            return sdk.RefreshRegistration(expires);
        }

        public int enableAutoCheckMwi(bool state)
        {
            sdk.EnableAutoCheckMwi(state);
            return 0;
        }

        public int outOfDialogRefer(long replaceSessionId, string replaceMethod, string target, string referTo)
        {
            return sdk.OutOfDialogRefer(replaceSessionId, replaceMethod, target, referTo);
        }

        public int attendedRefer2(long sessionId, long replaceSessionId, string replaceMethod, string target, string referTo)
        {
            return sdk.AttendedRefer2(sessionId, replaceSessionId, replaceMethod, target, referTo);
        }

        public long sendSubscription(string to, string eventName)
        {
            return sdk.SendSubscription(to, eventName);
        }
        public int terminateSubscription(long subscribeId)
        {
            return sdk.TerminateSubscription(subscribeId);
        }
        public long setDefaultSubscriptionTime(int secs)
        {
            return sdk.SetDefaultSubscriptionTime(secs);
        }

        public long setDefaultPublicationTime(int secs)
        {
            return sdk.SetDefaultPublicationTime(secs);
        }

        public long setPresenceMode(int mode)
        {
            return sdk.SetPresenceMode(mode);
        }

        public int presenceTerminateSubscribe(long subscribeId)
        {
            return sdk.PresenceTerminateSubscribe(subscribeId);
        }


        public long pickupBLFCall(string replaceDialogId, bool videoCall)
        {
            return sdk.PickupBLFCall(replaceDialogId, videoCall);
        }

        public void audioPlayLoopbackTest(bool enable)
        {
            sdk.AudioPlayLoopbackTest(enable);
        }

        //Android functions
        public void setLocalVideoWindow(PortSIPVideoRenderer localVideoWindow)
        {
            sdk.SetLocalVideoWindow(localVideoWindow);
        }

        public int setRemoteVideoWindow(long sessionId, PortSIPVideoRenderer remoteVideoWindow)
        {
            return sdk.SetRemoteVideoWindow(sessionId, remoteVideoWindow);
        }

        public int createVideoConference(PortSIPVideoRenderer conferenceVideoWindow, int width, int height, bool displayLocalVideoInConference)
        {
            return sdk.CreateVideoConference(conferenceVideoWindow, width, height, displayLocalVideoInConference);
        }

        public int createAudioConference()
        {
            return sdk.CreateAudioConference();
        }

        public int setConferenceVideoWindow(PortSIPVideoRenderer videoWindow)
        {
            return sdk.SetConferenceVideoWindow(videoWindow);
        }



        public void OnACTVTransferFailure(long sessionId, string reason, int code)
        {
            if (events != null) events.onACTVTransferFailure(sessionId, reason, code);
        }

        public void OnACTVTransferSuccess(long sessionId)
        {
            if (events != null) events.onACTVTransferSuccess(sessionId);
        }

        public void OnAudioRawCallback(long sessionId, int enum_audioCallbackMode, byte[] data, int dataLength, int samplingFreqHz)
        {
            if (events != null) events.onAudioRawCallback(sessionId, enum_audioCallbackMode, data, dataLength, samplingFreqHz);
        }

        public void OnInviteAnswered(long sessionId, string callerDisplayName, string caller, string calleeDisplayName, string callee, string audioCodecs, string videoCodecs, bool existsAudio, bool existsVideo, string message)
        {
            if (events != null) events.onInviteAnswered(sessionId, callerDisplayName, caller, calleeDisplayName, callee, audioCodecs, videoCodecs, existsAudio, existsVideo, message);
        }

        public void OnInviteBeginingForward(string forwardTo)
        {
            if (events != null) events.onInviteBeginingForward(forwardTo);
        }

        public void OnInviteClosed(long sessionId)
        {
            if (events != null) events.onInviteClosed(sessionId);
        }

        public void OnDialogStateUpdated(string BLFMonitoredUri, string BLFDialogState, string BLFDialogId, string BLFDialogDirection)
        {
            if (events != null) events.onDialogStateUpdated(BLFMonitoredUri, BLFDialogState, BLFDialogId, BLFDialogDirection);
        }

        public void OnInviteConnected(long sessionId)
        {
            if (events != null) events.onInviteConnected(sessionId);
        }

        public void OnInviteFailure(long sessionId, string reason, int code, string sipMessage)
        {
            if (events != null) events.onInviteFailure(sessionId, reason, code, sipMessage);
        }

        public void OnInviteIncoming(long sessionId, string callerDisplayName, string caller, string calleeDisplayName, string callee, string audioCodecs, string videoCodecs, bool existsAudio, bool existsVideo, string message)
        {
            if (events != null) events.onInviteIncoming(sessionId, callerDisplayName, caller, calleeDisplayName, callee, audioCodecs, videoCodecs, existsAudio, existsVideo, message);
        }

        public void OnInviteRinging(long sessionId, string statusText, int statusCode, string message)
        {
            if (events != null) events.onInviteRinging(sessionId, statusText, statusCode, message);
        }

        public void OnInviteSessionProgress(long sessionId, string audioCodecs, string videoCodecs, bool existsEarlyMedia, bool existsAudio, bool existsVideo, string message)
        {
            if (events != null) events.onInviteSessionProgress(sessionId, audioCodecs, videoCodecs, existsEarlyMedia, existsAudio, existsVideo, message);
        }

        public void OnInviteTrying(long sessionId)
        {
            if (events != null) events.onInviteTrying(sessionId);
        }

        public void OnInviteUpdated(long sessionId, string audioCodecs, string videoCodecs, bool existsAudio, bool existsVideo, string message)
        {
            if (events != null) events.onInviteUpdated(sessionId, audioCodecs, videoCodecs, existsAudio, existsVideo, message);
        }

        public void OnPlayAudioFileFinished(long sessionId, string fileName)
        {
            if (events != null) events.onPlayAudioFileFinished(sessionId, fileName);
        }

        public void OnPlayVideoFileFinished(long sessionId)
        {
            if (events != null) events.onPlayVideoFileFinished(sessionId);
        }

        public void OnPresenceOffline(string fromDisplayName, string from)
        {
            if (events != null) events.onPresenceOffline(fromDisplayName, from);
        }

        public void OnPresenceOnline(string fromDisplayName, string from, string stateText)
        {
            if (events != null) events.onPresenceOnline(fromDisplayName, from, stateText);
        }

        public void OnPresenceRecvSubscribe(long subscribeId, string fromDisplayName, string from, string subject)
        {
            if (events != null) events.onPresenceRecvSubscribe(subscribeId, fromDisplayName, from, subject);
        }

        public void OnReceivedRTPPacket(long sessionId, bool isAudio, byte[] RTPPacket, int packetSize)
        {
            //do nothing
            if (events != null) events.onReceivedRTPPacket(sessionId, isAudio, RTPPacket, packetSize);
        }

        public void OnReceivedRefer(long sessionId, long referId, string to, string from, string referSipMessage)
        {
            if (events != null) events.onReceivedRefer(sessionId, referId, to, from, referSipMessage);
        }

        public void OnReceivedSignaling(long sessionId, string message)
        {
            if (events != null) events.onReceivedSignaling(sessionId, message);
        }

        public void OnRecvDtmfTone(long sessionId, int tone)
        {
            if (events != null) events.onRecvDtmfTone(sessionId, tone);
        }

        public void OnRecvInfo(string infoMessage)
        {
            if (events != null) events.onRecvInfo(infoMessage);
        }

        public void OnRecvNotifyOfSubscription(long sessionId, string notifyMessage, byte[] messageData, int messageDataLength)
        {
            if (events != null) events.onRecvNotifyOfSubscription(sessionId, notifyMessage, messageData, messageDataLength);
        }

        public void OnRecvMessage(long sessionId, string mimeType, string subMimeType, byte[] messageData, int messageDataLength)
        {
            if (events != null) events.onRecvMessage(sessionId, mimeType, subMimeType, messageData, messageDataLength);
        }

        public void OnRecvOptions(string optionsMessage)
        {
            if (events != null) events.onRecvOptions(optionsMessage);
        }

        public void OnRecvOutOfDialogMessage(string fromDisplayName, string from, string toDisplayName, string to, string mimeType, string subMimeType, byte[] messageData, int messageDataLength,
            string sipMessage)
        {
            if (events != null) events.onRecvOutOfDialogMessage(fromDisplayName, from, toDisplayName, to, mimeType, subMimeType, messageData, messageDataLength, sipMessage);
        }

        public void OnReferAccepted(long sessionId)
        {
            if (events != null) events.onReferAccepted(sessionId);
        }

        public void OnReferRejected(long sessionId, string reason, int code)
        {
            if (events != null) events.onReferRejected(sessionId, reason, code);
        }

        public void OnRegisterFailure(string reason, int code, string sipMessage)
        {
            if (events != null) events.onRegisterFailure(reason, code, sipMessage);
        }

        public void OnRegisterSuccess(string reason, int code, string sipMessage)
        {
            if (events != null) events.onRegisterSuccess(reason, code, sipMessage);
        }

        public void OnRemoteHold(long sessionId)
        {
            if (events != null) events.onRemoteHold(sessionId);
        }

        public void OnRemoteUnHold(long sessionId, string audioCodecs, string videoCodecs, bool existsAudio, bool existsVideo)
        {
            if (events != null) events.onRemoteUnHold(sessionId, audioCodecs, videoCodecs, existsAudio, existsVideo);
        }

        public void OnSendMessageFailure(long sessionId, long messageId, string reason, int code)
        {
            if (events != null) events.onSendMessageFailure(sessionId, messageId, reason, code);
        }

        public void OnSendMessageSuccess(long sessionId, long messageId)
        {
            if (events != null) events.onSendMessageSuccess(sessionId, messageId);
        }

        public void OnSubscriptionFailure(long subscribeId, int statusCode)
        {
            if (events != null) events.onSubscriptionFailure(subscribeId, statusCode);
        }

        public void OnSubscriptionTerminated(long subscribeId)
        {
            if (events != null) events.onSubscriptionTerminated(subscribeId);
        }

        public void OnSendOutOfDialogMessageFailure(long messageId, string fromDisplayName, string from, string toDisplayName, string to, string reason, int code)
        {
            if (events != null) events.onSendOutOfDialogMessageFailure(messageId, fromDisplayName, from, toDisplayName, to, reason, code);
        }

        public void OnSendOutOfDialogMessageSuccess(long messageId, string fromDisplayName, string from, string toDisplayName, string to)
        {
            if (events != null) events.onSendOutOfDialogMessageSuccess(messageId, fromDisplayName, from, toDisplayName, to);
        }

        public void OnSendingRTPPacket(long sessionId, bool isAudio, byte[] RTPPacket, int packetSize)
        {
            //do nothing
            //if(events != null)events.onSendingRTPPacket(sessionId, isAudio, RTPPacket, packetSize);
        }

        public void OnSendingSignaling(long sessionId, string message)
        {
            if (events != null) events.onSendingSignaling(sessionId, message);
        }

        public void OnTransferRinging(long sessionId)
        {
            if (events != null) events.onTransferRinging(sessionId);
        }

        public void OnTransferTrying(long sessionId)
        {
            if (events != null) events.onTransferTrying(sessionId);
        }

        public void OnVideoRawCallback(long sessionId, int enum_videoCallbackMode, int width, int height, byte[] data, int dataLength)
        {
            if (events != null) events.onVideoRawCallback(sessionId, enum_videoCallbackMode, width, height, data, dataLength);
        }

        public void OnWaitingFaxMessage(string messageAccount, int urgentNewMessageCount, int urgentOldMessageCount, int newMessageCount, int oldMessageCount)
        {
            if (events != null) events.onWaitingFaxMessage(messageAccount, urgentNewMessageCount, urgentOldMessageCount, newMessageCount, oldMessageCount);
        }

        public void OnWaitingVoiceMessage(string messageAccount, int urgentNewMessageCount, int urgentOldMessageCount, int newMessageCount, int oldMessageCount)
        {
            if (events != null) events.onWaitingVoiceMessage(messageAccount, urgentNewMessageCount, urgentOldMessageCount, newMessageCount, oldMessageCount);
        }

       

     
       

     
        //*/
    }

}
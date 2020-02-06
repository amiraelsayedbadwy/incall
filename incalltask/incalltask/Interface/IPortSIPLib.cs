using PortSIP;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Interface
{
    public interface IPortSIPLib
    {
        int initialize(TRANSPORT_TYPE transport,
                       string localIP,
                       int localSIPPort,
                       PORTSIP_LOG_LEVEL logLevel,
                       string logFilePath,
                       int maxCallLines,
                       string sipAgent,
                       int audioDeviceLayer,
                       int videoDeviceLayer,
                       string tlsCertificatesRootPath,
                       string tlsCipherList,
                       bool verifyTLSCertificate);

        void unInitialize();

        int setUser(string userName,
                    string displayName,
                       string authName,
                       string password,
                     string userDomain,
                      string sipServer,
                    int sipServerPort,
                     string stunServer,
                     int stunServerPort,
                     string outboundServer,
                    int outboundServerPort);

        void removeUser();

        int registerServer(int expires, int retryTimes);

        int refreshRegistration(int expires);

        int unRegisterServer();

        int setInstanceId(string instanceId);

        int setLicenseKey(string key);

        int addAudioCodec(AUDIOCODEC_TYPE codecType);

        int addVideoCodec(VIDEOCODEC_TYPE codecType);


        bool isAudioCodecEmpty();


        bool isVideoCodecEmpty();


        int setAudioCodecPayloadType(AUDIOCODEC_TYPE codecType, int payloadType);

        int setVideoCodecPayloadType(VIDEOCODEC_TYPE codecType, int payloadType);


        void clearAudioCodec();


        void clearVideoCodec();


        int setAudioCodecParameter(AUDIOCODEC_TYPE codecType, string parameter);

        int setVideoCodecParameter(VIDEOCODEC_TYPE codecType, string parameter);

        String getVersion();

        int enableRport(bool enable);

        int enableEarlyMedia(bool enable);

        int enableReliableProvisional(bool enable);


        int enable3GppTags(bool enable);

        void enableCallbackSignaling(bool enableSending, bool enableReceived);



        int setSrtpPolicy(SRTP_POLICY srtpPolicy);



        int setRtpPortRange(int minimumRtpAudioPort, int maximumRtpAudioPort, int minimumRtpVideoPort, int maximumRtpVideoPort);


        int setRtcpPortRange(int minimumRtcpAudioPort, int maximumRtcpAudioPort, int minimumRtcpVideoPort, int maximumRtcpVideoPort);



        int enableCallForward(bool forBusyOnly, string forwardTo);

        int disableCallForward();



        int enableSessionTimer(int timerSeconds, SESSION_REFRESH_MODE refreshMode);


        int disableSessionTimer();


        void setDoNotDisturb(bool state);

        int enableAutoCheckMwi(bool state);

        int setRtpKeepAlive(bool state, int keepAlivePayloadType, int deltaTransmitTimeMS);


        int setKeepAliveTime(int keepAliveTime);



        int setAudioSamples(int ptime, int maxPtime);


        int addSupportedMimeType(string methodName, string mimeType, string subMimeType);


        int getSipMessageHeaderValue(string sipMessage, string headerName, out string headerValue, int headerValueLength);


        long addSipMessageHeader(long sessionId, string methodName, int msgType, string headerName, string headerValue);

        int removeAddedSipMessageHeader(int addedSipMessageId);

        void clearAddedSipMessageHeaders();

        long modifySipMessageHeader(long sessionId, string methodName, int msgType, string headerName, string headerValue);

        int removeModifiedSipMessageHeader(int modifiedSipMessageId);

        void clearModifiedSipMessageHeaders();

        //int setAudioDeviceId(int recordingDeviceId, int playoutDeviceId);


        int setVideoDeviceId(int deviceId);


        int setVideoResolution(int width, int height);

        int setVideoCropAndScale(bool enable);

        int setAudioBitrate(long sessionId, AUDIOCODEC_TYPE codecType, int bitrateKbps);

        int setVideoBitrate(long sessionId, int bitrateKbps);

        int setVideoFrameRate(long sessionId, int frameRate);

        int sendVideo(long sessionId, bool sendState);



        //void setLocalVideoWindow(IntPtr localVideoWindow);

        //int setRemoteVideoWindow(long sessionId, IntPtr remoteVideoWindow);

        int displayLocalVideo(bool state);


        int setVideoNackStatus(bool state);

        int setLoudspeakerStatus(bool enable);

        int setChannelOutputVolumeScaling(long sessionId, int scaling);

        int setChannelInputVolumeScaling(long sessionId, int scaling);


        long call(string callee, bool sendSdp, bool videoCall);


        int rejectCall(long sessionId, int code);



        int hangUp(long sessionId);



        int answerCall(long sessionId, bool videoCall);


        int updateCall(long sessionId, bool enableAudio, bool enableVideo);


        int hold(long sessionId);


        int unHold(long sessionId);


        int muteSession(long sessionId,
                                bool muteIncomingAudio,
                                bool muteOutgoingAudio,
                                bool muteIncomingVideo,
                                bool muteOutgoingVideo);


        int forwardCall(long sessionId, string forwardTo);


        int sendDtmf(long sessionId, DTMF_METHOD dtmfMethod, int code, int dtmfDuration, bool playDtmfTone);


        int refer(long sessionId, string referTo);


        int outOfDialogRefer(long replaceSessionId,
                               string replaceMethod,
                              string target,
                             string referTo);


        int attendedRefer(long sessionId, long replaceSessionId, string referTo);

        int attendedRefer2(long sessionId,
                            long replaceSessionId,
                           string replaceMethod,
                             string target,
                           string referTo);


        long acceptRefer(int referId, string referSignalingMessage);


        int rejectRefer(int referId);

        int enableSendPcmStreamToRemote(long sessionId, bool state, int streamSamplesPerSec);


        int sendPcmStreamToRemote(long sessionId, byte[] data, int dataLength);


        int enableSendVideoStreamToRemote(long sessionId, bool state);


        int sendVideoStreamToRemote(long sessionId, byte[] data, int dataLength, int width, int height);

        int setRtpCallback(bool enable);

        int enableAudioStreamCallback(long sessionId, bool enable, AUDIOSTREAM_CALLBACK_MODE callbackMode);


        int enableVideoStreamCallback(long sessionId, VIDEOSTREAM_CALLBACK_MODE callbackMode);


        int startRecord(long sessionId,
                                string recordFilePath,
                                string recordFileName,
                                bool appendTimestamp,
                                AUDIO_RECORDING_FILEFORMAT audioFileFormat,
                                RECORD_MODE audioRecordMode,
                                VIDEOCODEC_TYPE videoFileCodecType,
                                RECORD_MODE videoRecordMode);


        int stopRecord(long sessionId);


        int playVideoFileToRemote(long sessionId, string fileName, bool loop, bool playAudio);

        int stopPlayVideoFileToRemote(long sessionId);


        int playAudioFileToRemote(long sessionId, string fileName, int fileSamplesPerSec, bool loop);



        int stopPlayAudioFileToRemote(long sessionId);


        int playAudioFileToRemoteAsBackground(long sessionId, string fileName, int fileSamplesPerSec);


        int stopPlayAudioFileToRemoteAsBackground(long sessionId);

        //int createVideoConference(IntPtr conferenceVideoWindow,
        //		  					int videoWidth,
        //		 					int videoHeight,
        //							bool displayLocalVideoInConference);

        int createAudioConference();

        void destroyConference();


        int joinToConference(long sessionId);


        int removeFromConference(long sessionId);

        int setAudioRtcpBandwidth(long sessionId,
                                           int BitsRR,
                                           int BitsRS,
                                           int KBitsAS);


        int setVideoRtcpBandwidth(long sessionId,
                                          int BitsRR,
                                          int BitsRS,
                                          int KBitsAS);


        int enableAudioQos(bool state);


        int enableVideoQos(bool state);

        int setVideoMTU(int mtu);


        int getAudioStatistics(long sessionId,
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
                               out int recvAudioLevel);


        int getVideoStatistics(long sessionId,
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
                                      out int recvFramerate);


        void enableVAD(bool state);


        void enableAEC(bool state);



        void enableCNG(bool state);


        void enableAGC(bool state);



        void enableANS(bool state);

        int sendOptions(string to, string sdp);


        int sendInfo(long sessionId, string mimeType, string subMimeType, string infoContents);

        long sendMessage(long sessionId, string mimeType, string subMimeType, byte[] message, int messageLength);


        long sendOutOfDialogMessage(string to, string mimeType, string subMimeType, bool isSMS, byte[] message, int messageLength);


        long presenceSubscribe(string contact, string subject);


        int presenceRejectSubscribe(long subscribeId);


        int presenceAcceptSubscribe(long subscribeId);


        int setPresenceStatus(long subscribeId, string stateText);

        long sendSubscription(string to, string eventName);

        int terminateSubscription(long subscribeId);

        long setDefaultSubscriptionTime(int secs);

        long setDefaultPublicationTime(int secs);

        long setPresenceMode(int mode);

        int presenceTerminateSubscribe(long subscribeId);

        long pickupBLFCall(String replaceDialogId, bool videoCall);

        void audioPlayLoopbackTest(bool enable);
    }
}

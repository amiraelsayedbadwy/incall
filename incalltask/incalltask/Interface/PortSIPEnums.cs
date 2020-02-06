using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Interface
{
    public enum ERROR_CODES
    {
        INVALID_SESSION_ID = -1,



        ECoreAlreadyInitialized = -60000,
        ECoreNotInitialized = -60001,
        ECoreSDKObjectNull = -60002,
        ECoreArgumentNull = -60003,
        ECoreInitializeWinsockFailure = -60004,
        ECoreUserNameAuthNameEmpty = -60005,
        ECoreInitiazeStackFailure = -60006,
        ECorePortOutOfRange = -60007,
        ECoreAddTcpTransportFailure = -60008,
        ECoreAddTlsTransportFailure = -60009,
        ECoreAddUdpTransportFailure = -60010,
        ECoreMiniAudioPortOutOfRange = -60011,
        ECoreMaxAudioPortOutOfRange = -60012,
        ECoreMiniVideoPortOutOfRange = -60013,
        ECoreMaxVideoPortOutOfRange = -60014,
        ECoreMiniAudioPortNotEvenNumber = -60015,
        ECoreMaxAudioPortNotEvenNumber = -60016,
        ECoreMiniVideoPortNotEvenNumber = -60017,
        ECoreMaxVideoPortNotEvenNumber = -60018,
        ECoreAudioVideoPortOverlapped = -60019,
        ECoreAudioVideoPortRangeTooSmall = -60020,
        ECoreAlreadyRegistered = -60021,
        ECoreSIPServerEmpty = -60022,
        ECoreExpiresValueTooSmall = -60023,
        ECoreCallIdNotFound = -60024,
        ECoreNotRegistered = -60025,
        ECoreCalleeEmpty = -60026,
        ECoreInvalidUri = -60027,
        ECoreAudioVideoCodecEmpty = -60028,
        ECoreNoFreeDialogSession = -60029,
        ECoreCreateAudioChannelFailed = -60030,
        ECoreSessionTimerValueTooSmall = -60040,
        ECoreAudioHandleNull = -60041,
        ECoreVideoHandleNull = -60042,
        ECoreCallIsClosed = -60043,
        ECoreCallAlreadyHold = -60044,
        ECoreCallNotEstablished = -60045,
        ECoreCallNotHold = -60050,
        ECoreSipMessaegEmpty = -60051,
        ECoreSipHeaderNotExist = -60052,
        ECoreSipHeaderValueEmpty = -60053,
        ECoreSipHeaderBadFormed = -60054,
        ECoreBufferTooSmall = -60055,
        ECoreSipHeaderValueListEmpty = -60056,
        ECoreSipHeaderParserEmpty = -60057,
        ECoreSipHeaderValueListNull = -60058,
        ECoreSipHeaderNameEmpty = -60059,
        ECoreAudioSampleNotmultiple = -60060,//	The audio sample should be multiple of 10
        ECoreAudioSampleOutOfRange = -60061,	//	The audio sample ranges from 10 = - 60
        ECoreInviteSessionNotFound = -60062,
        ECoreStackException = -60063,
        ECoreMimeTypeUnknown = -60064,
        ECoreDataSizeTooLarge = -60065,
        ECoreSessionNumsOutOfRange = -60066,
        ECoreNotSupportCallbackMode = -60067,
        ECoreNotFoundSubscribeId = -60068,
        ECoreCodecNotSupport = -60069,
        ECoreCodecParameterNotSupport = -60070,
        ECorePayloadOutofRange = -60071,//  Dynamic Payload ranges from 96 = - 127
        ECorePayloadHasExist = -60072,//  Duplicate Payload values are not allowed.
        ECoreFixPayloadCantChange = -60073,
        ECoreCodecTypeInvalid = -60074,
        ECoreCodecWasExist = -60075,
        ECorePayloadTypeInvalid = -60076,
        ECoreArgumentTooLong = -60077,
        ECoreMiniRtpPortMustIsEvenNum = -60078,
        ECoreCallInHold = -60079,
        ECoreNotIncomingCall = -60080,
        ECoreCreateMediaEngineFailure = -60081,
        ECoreAudioCodecEmptyButAudioEnabled = -60082,
        ECoreVideoCodecEmptyButVideoEnabled = -60083,
        ECoreNetworkInterfaceUnavailable = -60084,
        ECoreWrongDTMFTone = -60085,
        ECoreWrongLicenseKey = -60086,
        ECoreTrialVersionLicenseKey = -60087,
        ECoreOutgoingAudioMuted = -60088,
        ECoreOutgoingVideoMuted = -60089,
        ECoreFailedCreateSdp = -60090,
        ECoreTrialVersionExpired = -60091,
        ECoreStackFailure = -60092,
        ECoreTransportExists = -60093,
        ECoreUnsupportTransport = -60094,
        ECoreAllowOnlyOneUser = -60095,
        ECoreUserNotFound = -60096,
        ECoreTransportsIncorrect = -60097,
        ECoreCreateTransportFailure = -60098,
        ECoreTransportNotSet = -60099,
        ECoreECreateSignalingFailure = -60100,
        ECoreArgumentIncorrect = -60101,

        // IVR
        ECoreIVRObjectNull = -61001,
        ECoreIVRIndexOutOfRange = -61002,
        ECoreIVRReferFailure = -61003,
        ECoreIVRWaitingTimeOut = -61004,

        // Audio

        EAudioFileNameEmpty = -70000,
        EAudioChannelNotFound = -70001,
        EAudioStartRecordFailure = -70002,
        EAudioRegisterRecodingFailure = -70003,
        EAudioRegisterPlaybackFailure = -70004,
        EAudioGetStatisticsFailure = -70005,
        EAudioPlayFileAlreadyEnable = -70006,
        EAudioPlayObjectNotExist = -70007,
        EAudioPlaySteamNotEnabled = -70008,
        EAudioRegisterCallbackFailure = -70009,
        EAudioCreateAudioConferenceFailure = -70010,
        EAudioOpenPlayFileFailure = -70011,
        EAudioPlayFileModeNotSupport = -70012,
        EAudioPlayFileFormatNotSupport = -70013,
        EAudioPlaySteamAlreadyEnabled = -70014,
        EAudioCreateRecordFileFailure = -70015,
        EAudioCodecNotSupport = -70016,
        EAudioPlayFileNotEnabled = -70017,
        EAudioPlayFileGetPositionFailure = -70018,
        EAudioCantSetDeviceIdDuringCall = -70019,
        EAudioVolumeOutOfRange = -70020,
        // Video

        EVideoFileNameEmpty = -80000,
        EVideoGetDeviceNameFailure = -80001,
        EVideoGetDeviceIdFailure = -80002,
        EVideoStartCaptureFailure = -80003,
        EVideoChannelNotFound = -80004,
        EVideoStartSendFailure = -80005,
        EVideoGetStatisticsFailure = -80006,
        EVideoStartPlayAviFailure = -80007,
        EVideoSendAviFileFailure = -80008,
        EVideoRecordUnknowCodec = -80009,
        EVideoCantSetDeviceIdDuringCall = -80010,
        EVideoUnsupportCaptureRotate = -80011,
        EVideoUnsupportCaptureResolution = -80012,
        ECameraSwitchTooOften = -80013,
        EMTUOutOfRange = -80014,

        // Device

        EDeviceGetDeviceNameFailure = -90001,



    }


    public enum AUDIOCODEC_TYPE
    {
        AUDIOCODEC_NONE = -1,
        AUDIOCODEC_G729 = 18,	    ///< G729 8KHZ 8kbit/s
		AUDIOCODEC_PCMA = 8,	    ///< PCMA/G711 A-law 8KHZ 64kbit/s
		AUDIOCODEC_PCMU = 0,	    ///< PCMU/G711 μ-law 8KHZ 64kbit/s
		AUDIOCODEC_GSM = 3,	        ///< GSM 8KHZ 13kbit/s
		AUDIOCODEC_G722 = 9,	    ///< G722 16KHZ 64kbit/s
		AUDIOCODEC_ILBC = 97,	    ///< iLBC 8KHZ 30ms-13kbit/s 20 ms-15kbit/s
		AUDIOCODEC_AMR = 98,	    ///< Adaptive Multi-Rate (AMR) 8KHZ (4.75,5.15,5.90,6.70,7.40,7.95,10.20,12.20)kbit/s
		AUDIOCODEC_AMRWB = 99,	    ///< Adaptive Multi-Rate Wideband (AMR-WB)16KHZ (6.60,8.85,12.65,14.25,15.85,18.25,19.85,23.05,23.85)kbit/s
		AUDIOCODEC_SPEEX = 100,	    ///< SPEEX 8KHZ (2-24)kbit/s
		AUDIOCODEC_SPEEXWB = 102,	///< SPEEX 16KHZ (4-42)kbit/s
		AUDIOCODEC_ISACWB = 103,	///< internet Speech Audio Codec(iSAC) 16KHZ (32-54)kbit/s
		AUDIOCODEC_ISACSWB = 104,	///< internet Speech Audio Codec(iSAC) 16KHZ (32-160)kbit/s
		AUDIOCODEC_G7221 = 121,	    ///< G722.1 16KHZ (16,24,32)kbit/s
		AUDIOCODEC_OPUS = 105,	    ///< OPUS 48KHZ 32kbit/s
		AUDIOCODEC_DTMF = 101	    ///< DTMF RFC 2833
	}

    public enum VIDEOCODEC_TYPE
    {
        VIDEO_CODE_NONE = -1,	        ///< Not use Video codec
		VIDEO_CODEC_I420 = 113,	        ///< I420/YUV420 Raw Video format, just use with startRecord 
		VIDEO_CODEC_H264 = 125,	        ///< H264 video codec
		VIDEO_CODEC_VP8 = 120,	        ///< VP8 video code
        VIDEO_CODEC_VP9 = 122,           ///< VP9 video code
	}

    /// The audio record file format
    public enum AUDIO_RECORDING_FILEFORMAT
    {
        FILEFORMAT_WAVE = 1,	///<	The record audio file is WAVE format. 
		FILEFORMAT_AMR,			///<	The record audio file is AMR format - all voice data is compressed by AMR codec. 
	}


    ///The audio/Video record mode
    public enum RECORD_MODE
    {
        RECORD_NONE = 0,		///<	Not Record. 
		RECORD_RECV = 1,		///<	Only record the received data. 
		RECORD_SEND,			///<	Only record send data. 
        RECORD_BOTH				///<	Record both received and sent data. 
	}


    public enum MediaTypes
    {
        _NONE = 0,
        _AUDIO = 1,
        _VIDEO = 2,
        _AUDIOVIDEO = 3
    }



    ///The audio stream callback mode
    public enum AUDIOSTREAM_CALLBACK_MODE
    {
        AUDIOSTREAM_NONE = 0,
        AUDIOSTREAM_LOCAL_PER_CHANNEL,		///<  Callback the audio stream from microphone for one channel base on the session ID
		AUDIOSTREAM_REMOTE_PER_CHANNEL,		///<  Callback the received audio stream for one channel base on the session ID.
        AUDIOSTREAM_BOTH,                   ///<  Callback both of local and remote audio stream on the session ID.
	}


    ///The video stream callback mode
    public enum VIDEOSTREAM_CALLBACK_MODE
    {
        VIDEOSTREAM_NONE = 0,	///< Disable video stream callback
		VIDEOSTREAM_LOCAL,		///< Local video stream callback
		VIDEOSTREAM_REMOTE,		///< Remote video stream callback
		VIDEOSTREAM_BOTH,		///< Both of local and remote video stream callback
	}



    /// Log level
    public enum PORTSIP_LOG_LEVEL
    {
        PORTSIP_LOG_NONE = -1,
        PORTSIP_LOG_ERROR = 1,
        PORTSIP_LOG_WARNING = 2,
        PORTSIP_LOG_INFO = 3,
        PORTSIP_LOG_DEBUG = 4
    }

    /// SRTP Policy
    public enum SRTP_POLICY
    {
        SRTP_POLICY_NONE = 0,	///< No use SRTP, The SDK can receive the encrypted call(SRTP) and unencrypted call both, but can't place outgoing encrypted call. 
		SRTP_POLICY_FORCE,		///< All calls must use SRTP, The SDK just allows receive encrypted Call and place outgoing encrypted call only.
		SRTP_POLICY_PREFER		///< Top priority to use SRTP, The SDK allows receive encrypted and decrypted call, and allows place outgoing encrypted call and unencrypted call.
	}

    /// Transport for SIP signaling.
    public enum TRANSPORT_TYPE
    {
        TRANSPORT_UDP = 0,	///< UDP Transport
		TRANSPORT_TLS,		///< Tls Transport
		TRANSPORT_TCP,		///< TCP Transport
		TRANSPORT_PERS_UDP,		///< PERS is the PortSIP private transport for anti the SIP blocking, it must using with the PERS Server http://www.portsip.com/pers.html.
        TRANSPORT_PERS_TCP,     ///< PERS is the PortSIP private transport for anti the SIP blocking, it must using with the PERS Server http://www.portsip.com/pers.html.
	}


    ///The session refresh by UAC or UAS
    public enum SESSION_REFRESH_MODE
    {
        SESSION_REFERESH_UAC = 0,	///< The session refresh by UAC
		SESSION_REFERESH_UAS		///< The session refresh by UAS
	}

    ///send DTMF tone with two methods
    public enum DTMF_METHOD
    {
        DTMF_RFC2833 = 0,	///<	send DTMF tone with RFC 2833, recommend.
		DTMF_INFO = 1	    ///<	send DTMF tone with SIP INFO.
	}
}

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
using PortSip.AndroidSample;

namespace incalltask.Droid.utils
{
    public class CallManager : Java.Lang.Object
    {
        public const int MAX_LINES = 10;
        private static CallManager mInstance;
        private static System.Object locker = new System.Object();
        Session[] sessions;
        public int CurrentLine { set; get; }
        public bool regist { get; set; }

        public static CallManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (locker)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new CallManager();
                        }
                    }
                }

                return mInstance;
            }
        }
        private CallManager()
        {
            CurrentLine = 0;
            sessions = new Session[MAX_LINES];
            for (int i = 0; i < sessions.Length; i++)
            {
                sessions[i] = new Session();
                sessions[i].LineName = "line - " + i;

            }
        }

        public void HangupAllCalls(ref PortSipLib sipLib)
        {

            foreach (Session session in sessions)
            {
                if (session.SessionID > Session.INVALID_SESSION_ID)
                {
                    sipLib.hangUp(session.SessionID);
                }
            }
        }

        public bool HasActiveSession()
        {

            foreach (Session session in sessions)
            {
                if (session.SessionID > Session.INVALID_SESSION_ID)
                {
                    return true;
                }
            }

            return false;
        }

        public Session FindSessionBySessionID(long SessionID)
        {
            foreach (Session session in sessions)
            {
                if (session.SessionID == SessionID)
                {
                    return session;
                }
            }
            return null;
        }

        public Session FindIdleSession()
        {
            foreach (Session session in sessions)
            {
                if (session.IsIdle())
                {
                    return session;
                }
            }
            return null;
        }

        public Session GetCurrentSession()
        {
            if (CurrentLine >= 0 && CurrentLine <= sessions.Length)
            {

                return sessions[CurrentLine];

            }
            return null;
        }

        public Session FindSessionByIndex(int index)
        {
            if (index >= 0 && index <= sessions.Length)
            {

                return sessions[index];

            }
            return null;
        }

        public void AddActiveSessionToConfrence(PortSipLib sipLib)
        {
            foreach (Session session in sessions)
            {
                if (session.state == CALL_STATE_FLAG.CONNECTED)
                {
                    sipLib.setRemoteVideoWindow(session.SessionID, null);
                    sipLib.joinToConference(session.SessionID);
                    sipLib.sendVideo(session.SessionID, true);
                }
            }
        }

        public void ResetAll()
        {
            foreach (Session session in sessions)
            {
                session.Reset();
            }
        }

        public Session FindIncomingCall()
        {
            foreach (Session session in sessions)
            {
                if (session.SessionID != Session.INVALID_SESSION_ID && session.state == CALL_STATE_FLAG.INCOMING)
                {
                    return session;
                }
            }

            return null;
        }

        public void setRemoteVideoWindow(PortSipLib sipLib, long sessionid, PortSIPVideoRenderer renderer)
        {
            sipLib.setConferenceVideoWindow(null);
            foreach (Session session in sessions)
            {
                if (session.state == CALL_STATE_FLAG.CONNECTED && sessionid != session.SessionID)
                {
                    sipLib.setRemoteVideoWindow(session.SessionID, null);
                }
            }
            sipLib.setRemoteVideoWindow(sessionid, renderer);
        }

        public void setConferenceVideoWindow(PortSipLib sipLib, PortSIPVideoRenderer renderer)
        {
            foreach (Session session in sessions)
            {
                if (session.state == CALL_STATE_FLAG.CONNECTED)
                {
                    sipLib.setRemoteVideoWindow(session.SessionID, null);
                }
            }
            sipLib.setConferenceVideoWindow(renderer);
        }
    }
}
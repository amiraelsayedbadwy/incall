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
	public enum CALL_STATE_FLAG
	{
		INCOMING = 1,
		TRYING = 2,
		CONNECTED = 3,
		FAILED = 4,
		CLOSED = 5,
	}

	public class Session : Java.Lang.Object
	{
		public static int INVALID_SESSION_ID = -1;
		public string Remote { get; set; }
		public string DisplayName { get; set; }
		public bool HasVideo { get; set; }
		public long SessionID { get; set; }
		public bool Hold { get; set; }

		public bool Mute { get; set; }
		public string LineName { get; set; }
		public CALL_STATE_FLAG state { get; set; }

		public bool IsIdle()
		{
			return state == CALL_STATE_FLAG.FAILED || state == CALL_STATE_FLAG.CLOSED;
		}
		public Session()
		{
			Remote = null;
			DisplayName = null;
			HasVideo = false;
			SessionID = INVALID_SESSION_ID;
			state = CALL_STATE_FLAG.CLOSED;
		}

		public void Reset()
		{
			Remote = null;
			DisplayName = null;
			HasVideo = false;
			SessionID = INVALID_SESSION_ID;
			state = CALL_STATE_FLAG.CLOSED;
		}
	}
}
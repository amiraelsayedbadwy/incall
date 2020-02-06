using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace incalltask.Droid.utils
{
	public class Ring
	{

		private static int TONE_RELATIVE_VOLUME = 70;
		private ToneGenerator mRingbackPlayer;
		private ToneGenerator mRingbackPlayercallee;

		private ToneGenerator mKeypadTone;

		protected Ringtone mRingtonePlayer;
		int ringRef = 0;
		private Context mContext;

		private static Ring single = null;

		public static Ring getInstance(Context context)
		{
			if (single == null)
			{
				single = new Ring(context);
			}
			return single;
		}

		private Ring(Context context)
		{
			mContext = context;
		}

		public bool stop()
		{
			stopRingBackTone();
			stopRingTone();

			return true;
		}


		public void startRingTone()
		{
			if (mRingtonePlayer != null && mRingtonePlayer.IsPlaying)
			{
				ringRef++;
				return;
			}

			if (mRingtonePlayer == null && mContext != null)
			{
				try
				{
					mRingtonePlayer = RingtoneManager.GetRingtone(mContext, RingtoneManager.GetDefaultUri(RingtoneType.Ringtone));
				}
				catch (Java.Lang.Exception e)
				{

					Console.WriteLine("startRingTone: {0} ", e.ToString());
					return;
				}
			}

			if (mRingtonePlayer != null)
			{
				lock (mRingtonePlayer)
				{
					ringRef++;
					mRingtonePlayer.Play();
				}
			}
		}

		public void stopRingTone()
		{
			if (mRingtonePlayer != null)
			{
				lock (mRingtonePlayer)
				{

					if (--ringRef <= 0)
					{
						mRingtonePlayer.Stop();
						mRingtonePlayer = null;
					}
				}
			}
		}

		public void startRingBackTone()
		{
			if (mRingbackPlayer == null)
			{
				try
				{
					mRingbackPlayer = new ToneGenerator(Stream.Ring, TONE_RELATIVE_VOLUME);
				}
				catch (RuntimeException e)
				{
					Console.WriteLine("startRingBackTone: {0} ", e.ToString());
					mRingbackPlayer = null;
				}
			}

			if (mRingbackPlayer != null)
			{
				lock (mRingbackPlayer)
				{
					mRingbackPlayer.StartTone(Tone.SupDial);
				}
			}
		}

		public void stopRingBackTone()
		{
			if (mRingbackPlayer != null)
			{
				lock (mRingbackPlayer)
				{
					mRingbackPlayer.StopTone();
					mRingbackPlayer = null;
				}
			}
		}


		public void startcalleeRingBackTone()
		{
			if (mRingbackPlayercallee == null)
			{
				try
				{
					mRingbackPlayercallee = new ToneGenerator(Stream.VoiceCall, 100);
				}
				catch (RuntimeException e)
				{
					Console.WriteLine("startcalleeRingBackTone: {0} ", e.ToString());
					mRingbackPlayercallee = null;
				}
			}

			if (mRingbackPlayercallee != null)
			{
				lock (mRingbackPlayercallee)
				{
					mRingbackPlayercallee.StartTone(Tone.SupRingtone);
				}
			}
		}

		public void startKeypadTone(int number)
		{
			if (mKeypadTone == null)
			{
				try
				{
					mKeypadTone = new ToneGenerator(Stream.Dtmf, 70);
				}
				catch (RuntimeException e)
				{
					Console.WriteLine("startcalleeRingBackTone: {0} ", e.ToString());
					mKeypadTone = null;
				}
			}

			if (mKeypadTone != null)
			{
				lock (mKeypadTone)
				{
					var toneNumber = (Android.Media.Tone)number;
					mKeypadTone.StartTone(toneNumber, 100);
				}
			}
		}

		public void stopcalleeRingBackTone()
		{
			if (mRingbackPlayercallee != null)
			{
				lock (mRingbackPlayercallee)
				{
					mRingbackPlayercallee.StopTone();
					mRingbackPlayercallee = null;
				}
			}
		}

	}
}
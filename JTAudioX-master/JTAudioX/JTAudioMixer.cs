using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using PortAudioSharp;

namespace JTAudioX
{
	public class JTAudioMixer : IDisposable
	{

		const int OutputRate = 8000;
		const int InputChannels = 1;
		const int OutputChannels = 1;
		const int FramesPerBuffer = 256;
		const int MAX_CHANELS = 32;

		private List<ConcurrentQueue<float>> _channels;

		Audio _audioHandle;
		
		public int MaxChannels{get{return MAX_CHANELS;}}

		private PortAudio.PaStreamCallbackResult myPaStreamCallback(
			IntPtr input,
			IntPtr output,
			uint frameCount,
			ref PortAudio.PaStreamCallbackTimeInfo timeInfo,
			PortAudio.PaStreamCallbackFlags statusFlags,
			IntPtr userData)
		{
			float[] mBuffer = new float[frameCount];


			for (int i = 0; i < frameCount; i++)
			{

				for (int j = 0; j < MAX_CHANELS; j++)
				{
					float f = 0;
					if (_channels[j].TryDequeue(out  f))
					{
						mBuffer[i] = mBuffer[i] + f;
					}
				}
			}
			Marshal.Copy(mBuffer, 0, output, (int)frameCount);
			return PortAudio.PaStreamCallbackResult.paContinue;
		}

		public JTAudioMixer()
		{
			_channels = new List<ConcurrentQueue<float>>();
			for (int i = 0; i < MAX_CHANELS; i++)
				_channels.Add(new ConcurrentQueue<float>());

			_audioHandle = new Audio(
				InputChannels,
				OutputChannels,
				OutputRate,
				FramesPerBuffer,
				new PortAudio.PaStreamCallbackDelegate(myPaStreamCallback));

			_audioHandle.Start();
		}


		public void Dispose()
		{
			_audioHandle.Stop();
		}

		public void SetData(int channel, float[] value)
		{
			for(int i =0; i < value.Length; i++)
				_channels[channel].Enqueue(value[i]);
		}
	}
}

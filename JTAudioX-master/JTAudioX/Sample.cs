using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JTAudioX
{
    public class Sample : List<SubSample>
    {
        /// <summary>
        /// Length of sample per second.
        /// </summary>
        public float Durration { get; private set; }

        /// <summary>
        /// Samples per second.
        /// </summary>
        public int SampleRate { get; private set; }

        /// <summary>
        /// Create a sample with a fixed durration and sample rate
        /// </summary>
        /// <param name="durration"></param>
        /// <param name="sampleRate"></param>
        public Sample(float durration, int sampleRate)
            : base(((int)Math.Ceiling(durration * sampleRate)))
        {
            Durration = durration;
            SampleRate = sampleRate;
            for (int i = 0; i < (int)Math.Ceiling(durration * sampleRate); i++)
            {
                this.Add(new SubSample() { Frequency = 0, Volume = 0 });
            }
        }
    }
}

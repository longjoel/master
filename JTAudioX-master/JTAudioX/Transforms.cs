using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JTAudioX
{
    public static class Transforms
    {

        public static float[] GenerateSineWaveform(Sample input)
        {
            var result = new float[input.Count];

            for (int i = 0; i < input.Count; i++)
            {

                float v = (float)Math.Sin(
                    ((float)(Math.PI * 2.0) * (float)(i) * input[i].Frequency)
                    / (float)(input.SampleRate))
                    * input[i].Volume;

                result[i] = v;
            }

            return result;
        }


        public static float[] GenerateSquareWaveform(Sample input)
        {
            var result = new float[input.Count];

            for (int i = 0; i < input.Count; i++)
            {

                float v = (float)Math.Sign( Math.Sin(
                    ((float)(Math.PI * 2.0) * (float)(i) * input[i].Frequency)
                    / (float)(input.SampleRate)))
                    * input[i].Volume;

                result[i] = v;
            }

            return result;
        }

        public static float[] GenerateSawtoothWaveform(Sample input)
        {
            var result = new float[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                double a = ((double)i) / (input.SampleRate / input[i].Frequency);
                var t = ((a % 1.0) * 2.0 - 1.0) * input[i].Volume;
            
                result[i] = (float)t;
            }

            return result;
        }

        public static float[] GenerateTriangleWaveform(Sample input)
        {
            var result = new float[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                double a = (2 * 3.14159) * ((double)i) / (input.SampleRate / input[i].Frequency);
                double v = Math.Asin(Math.Sin(a)) * input[i].Volume;


                result[i] = (float)v;
            }

            return result;
        }

    }
}

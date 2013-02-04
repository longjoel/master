using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using JTAudioX;

namespace JTAudioX.Test
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); 

        [STAThread]
        static void Main(string[] args)
        {
            var s = new Sample(0.25f, 8000);
            for (int i = 0; i < s.Count; i++)
            {
                s[i].Frequency = 220.0f;
                s[i].Volume = 0.50f;
            }

            var s2 = new Sample(0.25f, 8000);
            for (int i = 0; i < s2.Count; i++)
            {
                s2[i].Frequency = 150.0f;
                s2[i].Volume = 0.90f;
            }


            var s3 = new Sample(0.25f, 8000);
            for (int i = 0; i < s3.Count; i++)
            {
                s3[i].Frequency = 250.0f;
                s3[i].Volume = 0.50f;
            }

            var transformedSample = Transforms.GenerateSquareWaveform(s);
            var transformedSample2 = Transforms.GenerateSineWaveform(s2);
            var transformedSample3 = Transforms.GenerateSawtoothWaveform(s3);

            int j = 0;
            using (var m = new JTAudioMixer())
            {
                while (true)
                {
                    if(GetAsyncKeyState( System.Windows.Forms.Keys.A) < 0)
                        m.SetData(j++, transformedSample);

                    if (GetAsyncKeyState(System.Windows.Forms.Keys.S) < 0)
                        m.SetData(j++, transformedSample2);

                    if (GetAsyncKeyState(System.Windows.Forms.Keys.D) < 0)
                        m.SetData(j++, transformedSample3);

                    System.Threading.Thread.Sleep(10);
                    
                    if(j >=15)j = 0;
                }
              
               
                
               
            }

           
        }
    }
}

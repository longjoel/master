/*
 * Created by SharpDevelop.
 * User: Joel
 * Date: 1/5/2013
 * Time: 4:04 PM
 * 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

using System.Linq;
using System.Linq.Expressions;

using System.Net;
using System.IO;

using System.Threading;
using System.Threading.Tasks;

using JTAudioX;


namespace JTAudioX.HTTPServer
{
    class ClientInstruments
    {
        public List<float[]> Samples { get; set; }
        public List<bool> States { get; set; }

        public ClientInstruments()
        {
            Samples = new List<float[]>();
            States = new List<bool>();

            var r = new Random();

            for (int i = 0; i < 16; i++)
            {
                States.Add(false);

                float vol = (float)r.NextDouble();
                float freq = (float)r.Next(500)+100.0f;

                var s = new Sample(0.25f, 8000);
                for (int j = 0; j < s.Count; j++)
                {
                    
                    s[j].Volume = vol;
                    s[j].Frequency = freq;
                }

                var transform = r.Next(4);
                if (transform == 0)
                    Samples.Add(Transforms.GenerateSawtoothWaveform(s));
                if (transform == 1)
                    Samples.Add(Transforms.GenerateSineWaveform(s));
                if (transform == 2)
                    Samples.Add(Transforms.GenerateSquareWaveform(s));
                if (transform == 3)
                    Samples.Add(Transforms.GenerateTriangleWaveform(s));
            }
        }


    }

    class Program
    {



        [STAThread]
        public static void Main(string[] args)
        {
            var instrumentSampleMappings = new ConcurrentDictionary<IPAddress, ClientInstruments>();
            

            Task.Factory.StartNew(() =>
            {
                using (var httpServer = new HttpListener())
                {
                    httpServer.Prefixes.Add("http://*:8989/");
                    httpServer.Start();
                    
                    while (true)
                    {
                        var c = httpServer.GetContext();
                        var expression = new StreamReader(c.Request.InputStream).ReadToEnd().Split(',');
                      
                        string message = "";
                        try
                        {
                            int instrumentId = int.Parse( c.Request.QueryString["instrument"]);
                            bool state = c.Request.QueryString["state"] == "1";

                            var endpoint = c.Request.RemoteEndPoint;


                            ClientInstruments ci = null;
                            if (!instrumentSampleMappings.TryGetValue(endpoint.Address, out ci))
                            {
                                ci = new ClientInstruments();
                                instrumentSampleMappings[endpoint.Address] = ci;
                            }

                            ci.States[instrumentId] = state;

                            message = "OK";
                        }

                        catch
                        {
                            message = "Bad request. Expecting format `<instrument #>,<1|0>`.";
                        }

                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);

                        var resp = c.Response;
                        
                        System.IO.Stream output = resp.OutputStream;
                        var writer = new StreamWriter(output);
                        writer.WriteLine(message);
                        output.Close();
                    }


                }
            });


            using (var mixer = new JTAudioMixer())
            {

                int activeChannelIndex = 0;
                while (true)
                {
                    foreach (var instrument in instrumentSampleMappings.Values)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            if (instrument.States[i])
                                mixer.SetData(activeChannelIndex++, instrument.Samples[i]);

                            if (activeChannelIndex >= mixer.MaxChannels)
                                activeChannelIndex = 0;
                        }
                    }

                    // Sleep so we don't flood the mixer
                    Thread.Sleep(20);
                }
            }
        }
    }
}
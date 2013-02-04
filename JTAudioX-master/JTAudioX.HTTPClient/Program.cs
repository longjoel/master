/*
 * Created by SharpDevelop.
 * User: Joel
 * Date: 1/5/2013
 * Time: 4:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.IO;

namespace JTAudioX.HTTPClient
{
    class Program
    {
        public static void SetSound(int channel, bool state)
        {
            string strCh = channel.ToString();
            string strState = "0";
            if (state) strState = "1";
            var req = HttpWebRequest.Create(new Uri(string.Format("http://localhost:8989?instrument={0}&state={1}", strCh, strState)));
            req.Method = "GET";
            req.GetResponse();
        }

        public static void Main(string[] args)
        {

            SetSound(0, true);
            SetSound(1, true);
            SetSound(2, true);
            SetSound(3, true);
            System.Threading.Thread.Sleep(1000);

            SetSound(0, false);
            SetSound(1, false);
            SetSound(2, false);

            System.Threading.Thread.Sleep(3000);

            SetSound(3, false);
        }
    }
}
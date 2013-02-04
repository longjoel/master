using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

using JTAudioX;

namespace JTAudioX.ToySynth
{
    public partial class Form1 : Form
    {
        bool isRunning = false;

       // JTAudioMixer mixer = null;

        List<float[]> bakedSamples = null;

       // Task audioTask = null;

        public Form1()
        {
            InitializeComponent();
        }

        void PlaybackTask()
        {

        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {

            if (!isRunning)
            {
                bakedSamples = new List<float[]>();

                foreach (Instrument instrument in ChannelContainerPanel.Controls)
                {
                }

            }
            else
            {
            }
        }
       
    }
}

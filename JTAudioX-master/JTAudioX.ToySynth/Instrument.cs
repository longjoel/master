using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JTAudioX.ToySynth
{
    public partial class Instrument : UserControl
    {
        public Instrument()
        {
            InitializeComponent();
        }

        public float Frequency { get { return (float)FrequencySlider.Value; } set { FrequencySlider.Value = (int)value; } }
        public float Volume { get { return (float)VolumeSlider.Value / (float)VolumeSlider.Maximum; } set { VolumeSlider.Value = (int)(value * (float)VolumeSlider.Maximum); } }
        public bool Sine { get { return SineRadioButton.Checked; } }
        public bool Square { get { return SquareRadioButton.Checked; } }
        public bool Saw { get { return SawRadioButton.Checked; } }
        public Keys Key { get { return (Keys)(byte)(char.ToUpper(KeyComboBox.Text[0])); } }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CogitaLoggingEngine
{
    public partial class LoggingFormItemControl : UserControl
    {
        public LoggingFormItemControl()
        {
            InitializeComponent();
        }

        public LoggingFormItemControl(LogItemSeverity severity, string text)
        {
            InitializeComponent();

            this.LoggingMessageText.Text = text;
            this.MessageContainerGroupBox.Text = DateTime.Now.ToLongTimeString() + " --" + severity.ToString();
        }
    }
}

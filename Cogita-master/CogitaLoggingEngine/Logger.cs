using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CogitaLoggingEngine
{


    public partial class Logger : Form
    {

        public static ConcurrentQueue<LogItem> Messages = null;

        private static void LogMessage(LogItemSeverity severity, string text)
        {
            if (Messages != null)
                Messages.Enqueue(new LogItem(severity, text));


        }

        public static void Data(string text)
        {
            LogMessage(LogItemSeverity.Data, text);
        }

        public static void Warning(string text)
        {
            LogMessage(LogItemSeverity.Warning, text);
        }

        public static void Error(string text)
        {
            LogMessage(LogItemSeverity.Error, text);
        }

        public Logger()
        {
            InitializeComponent();
            Messages = new ConcurrentQueue<LogItem>();

        }

        private void LogItemsFlowLayoutPanel_ControlSetChanged(object sender, ControlEventArgs e)
        {
            while (LogItemsFlowLayoutPanel.Controls.Count > 1000)
            {
                LogItemsFlowLayoutPanel.Controls.RemoveAt(0);
            }
            LogItemsFlowLayoutPanel.VerticalScroll.Value = LogItemsFlowLayoutPanel.VerticalScroll.Maximum;

            foreach (LoggingFormItemControl li in LogItemsFlowLayoutPanel.Controls)
            {
                li.Width = LogItemsFlowLayoutPanel.Width - 40;
            }
        }

        private void LogReaderTimer_Tick(object sender, EventArgs e)
        {
            LogItem logItem = null;

            while (Logger.Messages.TryDequeue(out logItem))
            {
                var lic = new LoggingFormItemControl(logItem.Severity, logItem.Text);
                LogItemsFlowLayoutPanel.Controls.Add(lic);

                LogItemsFlowLayoutPanel.Update();
            }

        }

        private void LoggingForm_Load(object sender, EventArgs e)
        {
            LogReaderTimer.Start();
        }


    }

    public enum LogItemSeverity
    {
        Data,
        Warning,
        Error
    }
    public class LogItem
    {
        public LogItemSeverity Severity { get; private set; }
        public string Text { get; private set; }

        public LogItem(LogItemSeverity severity, string text)
        {
            Severity = severity;
            Text = text;
        }
    }
}

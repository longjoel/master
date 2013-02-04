using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Drawing;

namespace HotScramble
{
    class MenuScreen : BaseScreen
    {

        Dictionary<string, Action> _actionItems;

        int _selected = 0;
        Font _drawFont;
        public MenuScreen()
        {
            Title = "Options";
            _actionItems = new Dictionary<string, Action>();

            _actionItems["Install Windows Shell"] = new Action(() => { EmbedSoftware(Environment.GetEnvironmentVariable("WINDIR") + "explorer.exe"); });
            _actionItems["Install Hot Scramble Shell"] = new Action(() => { EmbedSoftware(System.Reflection.Assembly.GetExecutingAssembly().Location); });
            _actionItems["Reboot"] = new Action(() => { System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0"); });
            _actionItems["Shut Down"] = new Action(() => { System.Diagnostics.Process.Start("shutdown.exe", "-s -t 0"); });
            _actionItems["Quit"] = new Action(() => { Environment.Exit(0); });

            _drawFont = new Font("Fixedsys", 24, FontStyle.Bold);

        }

        void EmbedSoftware(string shell)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            regKey.SetValue("Shell", shell);
            regKey.Close();
        }

        public override void Draw(System.Drawing.Rectangle drawRegion, Tricycle.GameWindow gw)
        {
               

            for(int i = 0; i < _actionItems.Count; i++)
            {
                if(i == _selected)
                {
                     gw.DrawString(drawRegion.X, drawRegion.Y + (i*(_drawFont.Height + 2)), Color.Red, _drawFont, _actionItems.Keys.ToList()[i]);
                }
                else
                {
                    gw.DrawString(drawRegion.X+20, drawRegion.Y+ (i * (_drawFont.Height + 2)), Color.Black, _drawFont, _actionItems.Keys.ToList()[i]);

                }
            }

        }

        public override bool Process(Tricycle.GameWindow gw)
        {
            var settings = Properties.Settings.Default;
            var key = gw.Keyboard;

            if (key[settings.NextKey] || StateGlobals.EvaluateGamepad(gw, settings.NextGamePad))
            {
                _selected++;
                while (StateGlobals.EvaluateGamepad(gw, settings.NextGamePad)) ;
            }
            if (key[settings.PrevKey] || StateGlobals.EvaluateGamepad(gw, settings.PrevGamePad))
            {
                _selected--;
                while (StateGlobals.EvaluateGamepad(gw, settings.PrevGamePad)) ;
            }

            if (key[settings.SelectKey] || StateGlobals.EvaluateGamepad(gw, settings.SelectGamepadCombo))
            {
                var k = _actionItems.Keys.ToList()[_selected];
                var v = _actionItems[k];

                v();
            }

            if (_selected < 0) _selected = _actionItems.Count - 1;
            if(_selected >= _actionItems.Count) _selected = 0;

        

            return true;
        }
    }
}

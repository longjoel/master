using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Diagnostics;

using Tricycle;

namespace HotScramble
{
    class RomLauncher : BaseScreen
    {
        List<string> _romList;

        int _selected;
        Font _drawFont;
        int filesPerPage;
        int maxPages;

        public RomLauncher()
        {
            var settings = Properties.Settings.Default;
            Title = "Rom Launcher";
            _romList = Directory.EnumerateFiles(settings.RomPath, settings.RomFilter, SearchOption.TopDirectoryOnly).ToList();

            _selected = 0;
            _drawFont = new Font("Fixedsys", 24, FontStyle.Bold);
        }


        public override void Draw(System.Drawing.Rectangle drawRegion, Tricycle.GameWindow gw)
        {

            filesPerPage = (drawRegion.Height / (_drawFont.Height + 2)) - 2;

            maxPages = (_romList.Count / filesPerPage);

            int yOffset = (drawRegion.Height - (filesPerPage * (_drawFont.Height + 2))) / 2;

            int page = _selected / filesPerPage;

            var basePage = page * filesPerPage;

            gw.DrawString(drawRegion.X + drawRegion.Width - (60), drawRegion.Y , Color.Red, _drawFont, "{0}|{1}", (page + 1).ToString(), maxPages.ToString());

            for (int i = page * filesPerPage; i < Math.Min((page + 1) * filesPerPage, _romList.Count()); i++)
            {
                var pathStr = Path.GetFileNameWithoutExtension(_romList[i]);

                pathStr = pathStr.Substring(0, Math.Min(30, pathStr.Length));
                if (pathStr.Length >= 30)
                    pathStr = pathStr.Remove(pathStr.Length - 1) + "…";
                if (i == _selected)
                {
                    gw.DrawString(drawRegion.X, drawRegion.Y + ((i - basePage) * (_drawFont.Height + 2)), Color.Red, _drawFont, pathStr);
                }
                else
                {
                    gw.DrawString(drawRegion.X + 20, drawRegion.Y + ((i - basePage) * (_drawFont.Height + 2)), Color.Black, _drawFont, pathStr);

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
            if (key[settings.PrevPageKey] || StateGlobals.EvaluateGamepad(gw, settings.PrevPageGamepad))
            {

                _selected -= filesPerPage;
                while (StateGlobals.EvaluateGamepad(gw, settings.PrevPageGamepad)) ;
            }
            if (key[settings.NextPageKey] || StateGlobals.EvaluateGamepad(gw, settings.NextPageGamepad))
            {
                _selected += filesPerPage;
                while (StateGlobals.EvaluateGamepad(gw, settings.NextPageGamepad)) ;
            }
            if (key[settings.SelectKey] || StateGlobals.EvaluateGamepad(gw, settings.SelectGamepadCombo))
            {
                gw.Suspend();
                var p = new Process();
                p.StartInfo.FileName = settings.EmuPath;
                p.StartInfo.Arguments = settings.Parameters.Replace("%rom", "\"" + _romList[_selected] + "\"");


                p.Start();

                System.Threading.Thread.Sleep(5000);


                while (true)
                {


                    if (gw.Keyboard.GetGlobalKeyState(settings.KillKey) || StateGlobals.EvaluateGamepad(gw, settings.KillGamepadCombo))
                    {
                        p.CloseMainWindow();
                        break;
                    }

                    System.Threading.Thread.Sleep(0);

                }
                p.Close();
                p.Dispose();

                gw.Resume();
                System.Threading.Thread.Sleep(1000);

            }

          

            _selected = _selected % _romList.Count;
            if (_selected < 0)
            {
                _selected = (maxPages * filesPerPage - (filesPerPage + _selected)) - 1;
            }

            return true;

        }
    }
}

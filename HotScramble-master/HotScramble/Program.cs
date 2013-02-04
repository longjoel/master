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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = Properties.Settings.Default;

            //
            //System.Windows.SystemParameters.PrimaryScreenHeight

            settings.WindowWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            settings.WindowHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            
            var fx = new Font("Fixedsys", 16, FontStyle.Bold);



            var gw = new GameWindow(settings.WindowWidth, settings.WindowHeight);
            var drawRect = new Rectangle(400, 80, settings.WindowWidth - 480, settings.WindowHeight - 120);

            var screens = new List<BaseScreen>();
            screens.Add(new RomLauncher());
            screens.Add(new MenuScreen());

            var drawFont = new Font("Fixedsys", 32, FontStyle.Bold);

            int activeScreenIndex = 0;

            while (gw.IsRunning)
            {
                gw.GamePads.RefreshGamePadEnumeration();

                if (gw.Keyboard[settings.QuitKey] || StateGlobals.EvaluateGamepad(gw, settings.QuitGamepadCombo))
                    break;

                if (gw.Keyboard[settings.CyclePageKey] || StateGlobals.EvaluateGamepad(gw, settings.CyclePageGamepadCombo))
                {
                    activeScreenIndex = (activeScreenIndex + 1) % screens.Count;

                    while (StateGlobals.EvaluateGamepad(gw, settings.CyclePageGamepadCombo)) ;
                }

                gw.Clear(Color.White);
                gw.DrawString(0, 0, "Hot Scramble Game Launcher");

                //if (gw.GamePads.Count() > 0)
                //{

                for(int i = 0; i <gw.GamePads.Count(); i++)
                {
                    gw.DrawString(0, settings.WindowHeight - (fx.Height*i), Color.Black, fx, (string.Format("DPad State : {0}", gw.GamePads[i].DirectionalPad.ToString())).PadRight(30, ' ').Substring(0, 30) 
                        + string.Format(" ButtonState: {0}", string.Join("-", 
                       Enumerable
                        .Range(0,16).ToList()
                        .Select( b => {
                            if(gw.GamePads[i].Buttons[b]) 
                                return b.ToString(); 
                            return "";
                        }))));

                        //gw.GamePads[0].Buttons.SelectMany(b => { if (b) return b.ToString(); return "-"; }))));
                }

               

                for (int i = 0; i < screens.Count; i++)
                {
                    if (activeScreenIndex == i)
                    {
                        gw.DrawString(40, 80 + (i * drawFont.Height), Color.Red, drawFont, screens[i].Title);
                    }
                    else
                    {
                        gw.DrawString(40, 80 + (i * drawFont.Height), Color.Black, drawFont, screens[i].Title);
                    }
                }

                screens[activeScreenIndex].Draw(drawRect, gw);

                gw.Update();

                if (!screens[activeScreenIndex].Process(gw))
                    break;


            }

            gw.Dispose();
        }
    }
}

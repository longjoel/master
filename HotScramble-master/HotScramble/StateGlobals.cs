using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricycle;

namespace HotScramble
{
    class StateGlobals
    {
        //public static bool isRunningGame { get; set; }

        public static bool EvaluateGamepad(GameWindow gw, string buttonCombo)
        {

            for (int i = 0; i < gw.GamePads.Count(); i++)
            {
                var buttons = gw.GamePads[0].Buttons;

                bool innerPatternFound = true;
                foreach (int j in buttonCombo.Split(',').Select(x => int.Parse(x)))
                {

                    if (!buttons[j])
                    {
                        innerPatternFound = false;
                        break;
                    }

                }

                if (innerPatternFound)
                    return true;

            }
            return false;



        }

        public static bool EvaluateGamepad(GameWindow gw, DPadValues dpad)
        {

            for (int i = 0; i < gw.GamePads.Count(); i++)
            {
                var ipad = gw.GamePads[i].DirectionalPad;

                if (ipad == dpad)
                    return true;

            }
            return false;



        }
    }
}

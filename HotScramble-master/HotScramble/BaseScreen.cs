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
    abstract class BaseScreen
    {
        public string Title { get; protected set; }

        public abstract void Draw(Rectangle drawRegion, GameWindow gw);

        public abstract bool Process(GameWindow gw);
        
    }
}

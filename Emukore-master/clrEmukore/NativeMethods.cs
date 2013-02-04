using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime;
using System.Runtime.InteropServices;

namespace clrEmukore
{
    class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);


        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);


        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
}

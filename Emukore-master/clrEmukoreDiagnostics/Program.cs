using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using clrEmukore;


namespace clrEmukoreDiagnostics
{
    class Program
    {
        static void Main(string[] args)
        {
            var core = new clrEmukore.EmukoreDLLInterface(@"F:\Emukore\Debug\Emukore.NullImplementation.dll");

           

            Console.WriteLine( core.GetEmulatorName());

            Console.WriteLine();

            foreach (var i in core.GetFunctionsImplemented())
                Console.WriteLine(i);

            Console.WriteLine();

            foreach (var i in core.GetFunctionsNotImplemented())
                Console.WriteLine(i);

            Console.WriteLine();

            foreach (var i in core.EnumerateSysCalls())
                Console.WriteLine(i);

            Console.WriteLine();

            foreach (var i in core.EnumerateInputs())
                Console.WriteLine(i);



            Console.ReadLine();
        }
    }
}

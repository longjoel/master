using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime;

using System.Runtime.InteropServices;

using System.Reflection;

namespace clrEmukore
{
    class EmukoreDelegates
    {

        //[return: MarshalAs(UnmanagedType.LPStr)]
        public delegate IntPtr EK_GetEmulatorName();

        //[return: MarshalAs(UnmanagedType.LPStr)]
        public delegate IntPtr EK_GetSystemsEmulated();

        // Get the available commands you can send to the emulator


        public delegate int EK_GetNumberControllers();

        //[return: MarshalAs(UnmanagedType.LPStr)]
        public delegate IntPtr EK_EnumerateInputs();

        //[return: MarshalAs(UnmanagedType.LPStr)]
        public delegate IntPtr EK_EnumerateSysCalls();

        public delegate int EK_EnumerateScreenCount();
        public delegate VidTechFlags EK_EnumerateVidTech();


        public delegate int EK_SetOGLSurfaceTarget(int screen, uint texture, uint width, uint height);

        public delegate int EK_SetOGLDirectTarget(int screen,
                                        uint xpos, uint ypos,
                                        uint rectWidth, uint rectHeight,
                                        uint screenWidth, uint screenHeight);

        public delegate int EK_SetSDLSurfaceTarget(int screen, ref SDL_Surface target, int width, int height);



        public delegate int EK_SetGDIRenderTarget(int screen, IntPtr bitmap, int width, int height);



        public delegate int EK_SetD3DDeviceTarget(int screen, ref dynamic device, int width, int height);
        public delegate int EK_SetD3DSurfaceTarget(int screen, ref dynamic surface, int width, int height);


        // input functions

        public delegate void EK_SendInput(int controller, string command, string value);
        public delegate void EK_SendSysCall(string command, string data);

        // file i/o functions

        public delegate int EK_LoadRom(string romPath);
        public delegate int EK_UnloadRom();

        public delegate int EK_SaveState(string saveStatePath);
        public delegate int EK_LoadState(string saveStatePath);

        public delegate int EK_SaveMemory(string saveMemoryPath);
        public delegate int EK_LoadMemory(string saveMemoryPath);

        // start and stop emulation

        public delegate int EK_StartEmulation();
        public delegate int EK_StopEmulation();
        public delegate int EK_StepEmulation(double dt);

        public delegate int EK_Init();
        public delegate int EK_Shutdown();


    }
}

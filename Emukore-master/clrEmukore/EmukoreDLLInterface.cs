using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime;

using System.Runtime.InteropServices;

using System.Reflection;

namespace clrEmukore
{
    [Flags]
    public enum VidTechFlags : int
    {
        EK_OGL_DIRECT = 0x1,
        EK_OGL_FRAMEBUFFER_OBJECT = 0x2,
        EK_SDL = 0x4,
        EK_VOUTPUT_RESIZABLE = 0x8,
        EK_HBITMAP = 0x0F,
        EK_D3D_TEXTURE = 0x10,
        EK_D3D_DEVICE = 0x11


    }



    public class EmukoreDLLInterface : IDisposable
    {

        bool _isDisposed;

        IntPtr _dllHandle;

        private EmukoreDelegates.EK_EnumerateInputs _enumerateInputs;
        private EmukoreDelegates.EK_EnumerateScreenCount _enumerateScreenCount;
        private EmukoreDelegates.EK_EnumerateSysCalls _enumerateSysCalls;
        private EmukoreDelegates.EK_EnumerateVidTech _enumerateVidTech;

        private EmukoreDelegates.EK_GetEmulatorName _getEmulatorName;
        private EmukoreDelegates.EK_GetNumberControllers _getNumberControllers;
        private EmukoreDelegates.EK_GetSystemsEmulated _getSystemsEmulated;

        private EmukoreDelegates.EK_Init _init;
        private EmukoreDelegates.EK_LoadMemory _loadMemory;
        private EmukoreDelegates.EK_LoadRom _loadRom;
        private EmukoreDelegates.EK_LoadState _loadState;

        private EmukoreDelegates.EK_SaveMemory _saveMemory;
        private EmukoreDelegates.EK_SaveState _saveState;

        private EmukoreDelegates.EK_SendInput _sendInput;
        private EmukoreDelegates.EK_SendSysCall _sendSysCall;

        private EmukoreDelegates.EK_SetD3DDeviceTarget _setD3DDeviceTarget;
        private EmukoreDelegates.EK_SetD3DSurfaceTarget _setD3DSurfaceTarget;
        private EmukoreDelegates.EK_SetGDIRenderTarget _setGDIRenderTarget;
        private EmukoreDelegates.EK_SetOGLDirectTarget _setOGLDirectTarget;
        private EmukoreDelegates.EK_SetOGLSurfaceTarget _setOGLSurfaceTarget;
        private EmukoreDelegates.EK_SetSDLSurfaceTarget _setSDLSurfaceTarget;

        private EmukoreDelegates.EK_Shutdown _shutdown;
        private EmukoreDelegates.EK_StartEmulation _startEmulation;
        private EmukoreDelegates.EK_StepEmulation _stepEmulation;
        private EmukoreDelegates.EK_StopEmulation _stopEmulation;
        private EmukoreDelegates.EK_UnloadRom _unloadRom;



        public EmukoreDLLInterface(string emulatorPath)
        {

            _isDisposed = false;

            _dllHandle = NativeMethods.LoadLibrary(emulatorPath);
            if (_dllHandle == IntPtr.Zero)
                throw new Exception("Unable to load library. Please ensure the path is correct.");


            var ptrEnumInputs = NativeMethods.GetProcAddress(_dllHandle, "EK_EnumerateInputs");
            if (ptrEnumInputs != IntPtr.Zero)
                _enumerateInputs = (EmukoreDelegates.EK_EnumerateInputs)Marshal.GetDelegateForFunctionPointer(ptrEnumInputs, typeof(EmukoreDelegates.EK_EnumerateInputs));

            var ptrEnumScreenCount = NativeMethods.GetProcAddress(_dllHandle, "EK_EnumerateScreenCount");
            if (ptrEnumScreenCount != IntPtr.Zero)
                _enumerateScreenCount = (EmukoreDelegates.EK_EnumerateScreenCount)Marshal.GetDelegateForFunctionPointer(ptrEnumScreenCount, typeof(EmukoreDelegates.EK_EnumerateScreenCount));

            var ptrEnumSysCalls = NativeMethods.GetProcAddress(_dllHandle, "EK_EnumerateSysCalls");
            if (ptrEnumSysCalls != IntPtr.Zero)
                _enumerateSysCalls = (EmukoreDelegates.EK_EnumerateSysCalls)Marshal.GetDelegateForFunctionPointer(ptrEnumSysCalls, typeof(EmukoreDelegates.EK_EnumerateSysCalls));

            var ptrEnumVidTech = NativeMethods.GetProcAddress(_dllHandle, "EK_EnumerateVidTech");
            if (ptrEnumVidTech != IntPtr.Zero)
                _enumerateVidTech = (EmukoreDelegates.EK_EnumerateVidTech)Marshal.GetDelegateForFunctionPointer(ptrEnumVidTech, typeof(EmukoreDelegates.EK_EnumerateVidTech));

            var ptrGetEmulatorName = NativeMethods.GetProcAddress(_dllHandle, "EK_GetEmulatorName");
            if (ptrGetEmulatorName != IntPtr.Zero)
                _getEmulatorName = (EmukoreDelegates.EK_GetEmulatorName)Marshal.GetDelegateForFunctionPointer(ptrGetEmulatorName, typeof(EmukoreDelegates.EK_GetEmulatorName));

            var ptrGetNumberControllers = NativeMethods.GetProcAddress(_dllHandle, "EK_GetNumberControllers");
            if (ptrGetNumberControllers != IntPtr.Zero)
                _getNumberControllers = (EmukoreDelegates.EK_GetNumberControllers)Marshal.GetDelegateForFunctionPointer(ptrGetNumberControllers, typeof(EmukoreDelegates.EK_GetNumberControllers));

            var ptrGetSystemsEmulated = NativeMethods.GetProcAddress(_dllHandle, "EK_GetSystemsEmulated");
            if (ptrGetSystemsEmulated != IntPtr.Zero)
                _getSystemsEmulated = (EmukoreDelegates.EK_GetSystemsEmulated)Marshal.GetDelegateForFunctionPointer(ptrGetNumberControllers, typeof(EmukoreDelegates.EK_GetSystemsEmulated));


            var ptrInit = NativeMethods.GetProcAddress(_dllHandle, "EK_Init");
            if (ptrInit != IntPtr.Zero)
                _init = (EmukoreDelegates.EK_Init)Marshal.GetDelegateForFunctionPointer(ptrInit, typeof(EmukoreDelegates.EK_Init));

            var ptrLoadMemory = NativeMethods.GetProcAddress(_dllHandle, "EK_LoadMemory");
            if (ptrLoadMemory != IntPtr.Zero)
                _loadMemory = (EmukoreDelegates.EK_LoadMemory)Marshal.GetDelegateForFunctionPointer(ptrLoadMemory, typeof(EmukoreDelegates.EK_LoadMemory));

            var ptrLoadRom = NativeMethods.GetProcAddress(_dllHandle, "EK_LoadRom");
            if (ptrLoadRom != IntPtr.Zero)
                _loadRom = (EmukoreDelegates.EK_LoadRom)Marshal.GetDelegateForFunctionPointer(ptrLoadRom, typeof(EmukoreDelegates.EK_LoadRom));

            var ptrLoadState = NativeMethods.GetProcAddress(_dllHandle, "EK_LoadState");
            if (ptrLoadState != IntPtr.Zero)
                _loadState = (EmukoreDelegates.EK_LoadState)Marshal.GetDelegateForFunctionPointer(ptrLoadState, typeof(EmukoreDelegates.EK_LoadState));

            var ptrSaveMemory = NativeMethods.GetProcAddress(_dllHandle, "EK_SaveMemory");
            if (ptrSaveMemory != IntPtr.Zero)
                _saveMemory = (EmukoreDelegates.EK_SaveMemory)Marshal.GetDelegateForFunctionPointer(ptrSaveMemory, typeof(EmukoreDelegates.EK_SaveMemory));

            var ptrSaveState = NativeMethods.GetProcAddress(_dllHandle, "EK_SaveState");
            if (ptrSaveState != IntPtr.Zero)
                _saveState = (EmukoreDelegates.EK_SaveState)Marshal.GetDelegateForFunctionPointer(ptrSaveState, typeof(EmukoreDelegates.EK_SaveState));


            var ptrSendInput = NativeMethods.GetProcAddress(_dllHandle, "EK_SendInput");
            if (ptrSendInput != IntPtr.Zero)
                _sendInput = (EmukoreDelegates.EK_SendInput)Marshal.GetDelegateForFunctionPointer(ptrSendInput, typeof(EmukoreDelegates.EK_SendInput));


            var ptrSendSysCall = NativeMethods.GetProcAddress(_dllHandle, "EK_SendSysCall");
            if (ptrSendSysCall != IntPtr.Zero)
                _sendSysCall = (EmukoreDelegates.EK_SendSysCall)Marshal.GetDelegateForFunctionPointer(ptrSendSysCall, typeof(EmukoreDelegates.EK_SendSysCall));


            var ptrSetD3dDeviceTarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetD3DDeviceTarget");
            if (ptrSetD3dDeviceTarget != IntPtr.Zero)
                _setD3DDeviceTarget = (EmukoreDelegates.EK_SetD3DDeviceTarget)Marshal.GetDelegateForFunctionPointer(ptrSetD3dDeviceTarget, typeof(EmukoreDelegates.EK_SetD3DDeviceTarget));


            var ptrSetD3Dsurfacetarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetD3DSurfaceTarget");
            if (ptrSetD3dDeviceTarget != IntPtr.Zero)
                _setD3DSurfaceTarget = (EmukoreDelegates.EK_SetD3DSurfaceTarget)Marshal.GetDelegateForFunctionPointer(ptrSetD3dDeviceTarget, typeof(EmukoreDelegates.EK_SetD3DSurfaceTarget));

            var ptrSetGDItarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetGDIRenderTarget");
            if (ptrSetGDItarget != IntPtr.Zero)
                _setGDIRenderTarget = (EmukoreDelegates.EK_SetGDIRenderTarget)Marshal.GetDelegateForFunctionPointer(ptrSetGDItarget, typeof(EmukoreDelegates.EK_SetGDIRenderTarget));

            var ptrSetOGLDirectTarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetOGLDirectTarget");
            if (ptrSetOGLDirectTarget != IntPtr.Zero)
                _setOGLDirectTarget = (EmukoreDelegates.EK_SetOGLDirectTarget)Marshal.GetDelegateForFunctionPointer(ptrSetOGLDirectTarget, typeof(EmukoreDelegates.EK_SetOGLDirectTarget));

            var ptrSetOGLSurfaceTarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetOGLSurfaceTarget");
            if (ptrSetOGLSurfaceTarget != IntPtr.Zero)
                _setOGLSurfaceTarget = (EmukoreDelegates.EK_SetOGLSurfaceTarget)Marshal.GetDelegateForFunctionPointer(ptrSetOGLSurfaceTarget, typeof(EmukoreDelegates.EK_SetOGLSurfaceTarget));

            var ptrSetSDLSurfaceTarget = NativeMethods.GetProcAddress(_dllHandle, "EK_SetSDLSurfaceTarget");
            if (ptrSetSDLSurfaceTarget != IntPtr.Zero)
                _setSDLSurfaceTarget = (EmukoreDelegates.EK_SetSDLSurfaceTarget)Marshal.GetDelegateForFunctionPointer(ptrSetSDLSurfaceTarget, typeof(EmukoreDelegates.EK_SetSDLSurfaceTarget));


            var ptrShutdown = NativeMethods.GetProcAddress(_dllHandle, "EK_Shutdown");
            if (ptrShutdown != IntPtr.Zero)
                _shutdown = (EmukoreDelegates.EK_Shutdown)Marshal.GetDelegateForFunctionPointer(ptrShutdown, typeof(EmukoreDelegates.EK_Shutdown));

            var ptrStartEmulation = NativeMethods.GetProcAddress(_dllHandle, "EK_StartEmulation");
            if (ptrStartEmulation != IntPtr.Zero)
                _startEmulation = (EmukoreDelegates.EK_StartEmulation)Marshal.GetDelegateForFunctionPointer(ptrStartEmulation, typeof(EmukoreDelegates.EK_StartEmulation));

            var ptrStepEmulation = NativeMethods.GetProcAddress(_dllHandle, "EK_StepEmulation");
            if (ptrStepEmulation != IntPtr.Zero)
                _stepEmulation = (EmukoreDelegates.EK_StepEmulation)Marshal.GetDelegateForFunctionPointer(ptrStepEmulation, typeof(EmukoreDelegates.EK_StepEmulation));

            var ptrStopEmulation = NativeMethods.GetProcAddress(_dllHandle, "EK_StopEmulation");
            if (ptrStopEmulation != IntPtr.Zero)
                _stopEmulation = (EmukoreDelegates.EK_StopEmulation)Marshal.GetDelegateForFunctionPointer(ptrStopEmulation, typeof(EmukoreDelegates.EK_StopEmulation));


          

            var ptrUnloadRom = NativeMethods.GetProcAddress(_dllHandle, "EK_UnloadRom");
            if (ptrUnloadRom != IntPtr.Zero)
                _unloadRom = (EmukoreDelegates.EK_UnloadRom)Marshal.GetDelegateForFunctionPointer(ptrUnloadRom, typeof(EmukoreDelegates.EK_UnloadRom));


        }

        ~EmukoreDLLInterface()
        {
            if (!_isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                if (_dllHandle != IntPtr.Zero)
                    NativeMethods.FreeLibrary(_dllHandle);

                _isDisposed = true;
            }
        }


        public string GetEmulatorName()
        {
            if (!_isDisposed)
            {
                if (_getEmulatorName != null)
                    return Marshal.PtrToStringAnsi(_getEmulatorName());

                throw new Exception("Get Emulator Name is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        public string[] GetSystemsEmulated()
        {
            if (!_isDisposed)
            {
                if (_getSystemsEmulated != null)
                    return Marshal.PtrToStringAnsi( _getSystemsEmulated()).Split(',');

                throw new Exception("Get systems emulated is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        public int GetNumberOfControllers()
        {
            if (!_isDisposed)
            {
                if (_getNumberControllers != null)
                    return _getNumberControllers();

                throw new Exception("Get number of controllers is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        public string[] EnumerateInputs()
        {
            if (!_isDisposed)
            {
                if (_enumerateInputs != null)
                {
                  
                    return Marshal.PtrToStringAnsi(_enumerateInputs()).Split(',');
                }

                throw new Exception("Enumerate inputs is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        public string[] EnumerateSysCalls()
        {
            if (!_isDisposed)
            {
                if (_enumerateSysCalls != null)
                {
                    return Marshal.PtrToStringAnsi(_enumerateSysCalls()).Split(',');

                    //return _enumerateSysCalls().Split(',');
                }
                throw new Exception("Enumerate SysCalls is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }


        public int EnumerateScreenCount()
        {
            if (!_isDisposed)
            {
                if (_enumerateScreenCount != null)
                    return _enumerateScreenCount();

                throw new Exception("Enumerate screen count is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }


        public VidTechFlags EnumerateVidTech()
        {
            if (!_isDisposed)
            {
                if (_enumerateVidTech != null)
                    return (VidTechFlags)_enumerateVidTech();

                throw new Exception("Enumerate SysCalls is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }


        public bool SetOGLSurfaceTarget(int screen, uint texture, uint width, uint height)
        {
            if (!_isDisposed)
            {
                if (_setOGLSurfaceTarget != null)
                    return _setOGLSurfaceTarget(screen, texture, width, height) == 1;

                throw new Exception("Set OpenGL Surface Target is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        public bool SetOGLDirectTarget(int screen, uint x, uint y, uint areaWidth, uint areaHeight, uint destWidth, uint destHeight)
        {
            if (!_isDisposed)
            {
                if (_setOGLDirectTarget != null)
                    return _setOGLDirectTarget(screen, x, y, areaWidth, areaHeight, destWidth, destHeight) == 1;

                throw new Exception("Set OpenGL Direct Target is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        //EK_SetSDLSurfaceTarget(int screen, SDL_Surface* target, int width, int height);
        public bool SetSDLSurfaceTarget(int screen, ref SDL_Surface target, int width, int height)
        {
            if (!_isDisposed)
            {
                if (_setSDLSurfaceTarget != null)
                    return _setSDLSurfaceTarget(screen, ref target, width, height) == 1;

                throw new Exception("Set SDL Surface Target is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        //EK_SetGDIRenderTarget(int screen, HBITMAP *bitmap, int width, int height);
        public bool SetGDIRenderTarget(int screen, IntPtr bitmap, int width, int height)
        {
            if (!_isDisposed)
            {
                if (_setGDIRenderTarget != null)
                    return _setGDIRenderTarget(screen, bitmap, width, height) == 1;

                throw new Exception("Set SDL Surface Target is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        //DLLH		EK_BOOL			EK_SetD3DDeviceTarget(int screen, IDirect3DDevice9 *device, int width, int height);

        public bool SetD3DDeviceTarget(int screen, ref dynamic device, int width, int height)
        {
            if (!_isDisposed)
            {
                if (_setD3DDeviceTarget != null)
                    return _setD3DDeviceTarget(screen, ref device, width, height) == 1;

                throw new Exception("Set D3D Device Render Target not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }


        //DLLH		EK_BOOL			EK_SETD3DSurfaceTarget(int screen, IDirect3DSurface9 *surface, int width, int height);

        public bool SetD3DSurfaceTarget(int screen, ref dynamic surface, int width, int height)
        {
            if (!_isDisposed)
            {
                if (_setD3DSurfaceTarget != null)
                    return _setD3DSurfaceTarget(screen, ref surface, width, height) == 1;

                throw new Exception("Set D3D Surface Target not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }


        //        DLLH		void			EK_SendInput(int controller, char* command, char* value);
        public void SendInput(int controller, string command, string value)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_sendInput == null) throw new Exception("Send Input not implemented.");
            _sendInput(controller, command, value);

        }

        //DLLH		void			EK_SendSysCall(char* command, char* data);

        public void SendSyscall(string command, string value)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_sendSysCall == null) throw new Exception("Send Syscall not implemented.");
            _sendSysCall(command, value);

        }



        //DLLH		EK_BOOL			EK_LoadRom(char* romPath);

        public bool LoadRom(string romPath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_loadRom == null) throw new Exception("Load Rom is not implemented.");
            return _loadRom(romPath) == 1;

        }


        //DLLH		EK_BOOL			EK_UnloadRom();

        public bool UnloadRom(string romPath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_unloadRom == null) throw new Exception("Unload Rom is not implemented.");
            return _unloadRom() == 1;

        }


        //DLLH		EK_BOOL			EK_SaveState(char* saveStatePath);
        public bool SaveState(string saveStatePath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_saveState == null) throw new Exception("Save State is not implemented.");
            return _saveState(saveStatePath) == 1;

        }


        //DLLH		EK_BOOL			EK_LoadState(char* saveStatePath);

        public bool LoadState(string saveStatePath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_loadState == null) throw new Exception("Load State is not implemented.");
            return _loadState(saveStatePath) == 1;

        }

        //DLLH		EK_BOOL			EK_SaveMemory(char* saveMemoryPath);

        public bool SaveMemory(string saveMemoryPath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_saveMemory == null) throw new Exception("Save Memory is not implemented.");
            return _saveMemory(saveMemoryPath) == 1;

        }

        //DLLH		EK_BOOL			EK_LoadMemory(char* saveMemoryPath);

        public bool LoadMemory(string saveMemoryPath)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_loadMemory == null) throw new Exception("Load Memory is not implemented.");
            return _loadMemory(saveMemoryPath) == 1;

        }


        //// start and stop emulation

        //DLLH		EK_BOOL			EK_StartEmulation();

        public bool StartEmulation()
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_startEmulation == null) throw new Exception("Start Emulation is not implemented.");
            return _startEmulation() == 1;
        }


        //DLLH		EK_BOOL			EK_StopEmulation();

        public bool StopEmulation()
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_stopEmulation == null) throw new Exception("Stop Emulation is not implemented.");
            return _stopEmulation() == 1;
        }

        //DLLH		EK_BOOL			EK_StepEmulation(double dt);

        public bool StepEmulation(double dt)
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            if (_stepEmulation == null) throw new Exception("Step Emulation is not implemented.");
            return _stepEmulation(dt) == 1;
        }

        //DLLH		EK_BOOL			EK_Init();


        public bool Init()
        {
            if (!_isDisposed)
            {
                if (_init != null)
                    return _init() == 1;

                throw new Exception("Init is not implemented.");
            }

            throw new Exception("Attempt to call method on a disposed object.");
        }

        //DLLH		EK_BOOL			EK_Shutdown();

        public bool Shutdown()
        {


            if (_shutdown == null) throw new Exception("Shutdown is not implemented.");
            return _shutdown() == 1;
        }

        public string[] GetFunctionsImplemented()
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            var impTypes = new List<string>();

         
            var fields = typeof(EmukoreDLLInterface).GetFields(BindingFlags.NonPublic| BindingFlags.Instance );
            foreach (var f in fields)
            {
                if (f.GetValue(this) != null)
                {
                    var gt = f.FieldType.Name.ToString();

                    if(gt.Contains("EK_"))
                        impTypes.Add(gt);
                }

            }

            return impTypes.ToArray();
        }

        public string[] GetFunctionsNotImplemented()
        {
            if (_isDisposed) throw new Exception("Attempt to call method on a disposed object.");

            var impTypes = new List<string>();


            var fields = typeof(EmukoreDLLInterface).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var f in fields)
            {
                if (f.GetValue(this) == null)
                {
                    var gt = f.FieldType.Name.ToString();

                    if (gt.Contains("EK_"))
                        impTypes.Add(gt);
                }

            }

            return impTypes.ToArray();
        }

    }
}

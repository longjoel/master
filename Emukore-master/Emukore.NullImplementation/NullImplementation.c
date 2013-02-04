#include "Emukore.h"



DLLC		char*			EK_GetEmulatorName()
{
	return "Null Implementation for NES Emulator";

}

char *systems = {"NES,FAMICOM"};
DLLC		char*			EK_GetSystemsEmulated()
{
	

	return systems;

}

DLLC		int				EK_GetNumberControllers()
{
	return 4;
}

char *inputs = "LEFT %b,RIGHT %b,UP %b,DOWN %b,SELECT %b,START %b,B %b,A %b";

DLLC		char*		EK_EnumerateInputs()
{
	

	return inputs;
}

char *syscalls = "POWER_OFF,POWER_ON,CART_EJECT,CART_INSERT,CART_BLOW,RESET_PRESS";

DLLC		char*			EK_EnumerateSysCalls()
{
	return syscalls;
}

DLLC		int				EK_EnumerateScreenCount()
{
	return 1;
}

DLLC		EK_VidTechFlags	EK_EnumerateVidTech()
{
	EK_VidTechFlags flags = EK_SDL;
}



DLLC		EK_BOOL			EK_SetSDLSurfaceTarget(int screen, SDL_Surface* target, int width, int height)
{
	return EK_FALSE;
}


DLLC		void			EK_SendInput(int controller, char* command, char* value)
{

}


DLLC		void			EK_SendSysCall(char* command, char* data)
{

}


DLLC		EK_BOOL			EK_LoadRom(char* romPath)
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_UnloadRom()
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_SaveState(char* saveStatePath)
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_LoadState(char* saveStatePath)
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_SaveMemory(char* saveMemoryPath)
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_LoadMemory(char* saveMemoryPath)
{
	return EK_FALSE;
}


DLLC		EK_BOOL			EK_StartEmulation()
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_StopEmulation()
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_StepEmulation(double dt)
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_Init()
{
	return EK_FALSE;
}

DLLC		EK_BOOL			EK_Shutdown()
{
	return EK_FALSE;
}

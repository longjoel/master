/*
		Emukore.h - Joel Longanecker
		2011

		Licese to be determined

*/

#ifndef	__EMUKORE__
#define	__EMUKORE__

#define		EK_BOOL			unsigned int
#define		EK_TRUE			1
#define		EK_FALSE		0

#define		GLuint			unsigned int

#ifdef __cplusplus

#define		CPP				extern "C"

#else

#define		CPP

#endif

#ifdef WIN32

#define		DLLH			CPP _declspec(dllimport)
#define		DLLC			CPP _declspec(dllexport)

#else

#define		DLLH			CPP
#define		DLLC			CPP

#endif


#ifdef WIN32

#include <Windows.h>

#endif

#ifdef USE_D3D9

#include <d3d9.h>

#endif



typedef enum 
{
    EK_OGL_DIRECT = 0x1,
    EK_OGL_FRAMEBUFFER_OBJECT = 0x2,
    EK_SDL = 0x4,
    EK_VOUTPUT_RESIZABLE = 0x8

#ifdef WIN32

	,EK_HBITMAP = 0x0F
#endif

#ifdef USE_D3D9

	,EK_D3D_TEXTURE = 0x10,
    EK_D3D_DEVICE = 0x11

#endif
	
} EK_VidTechFlags;

// if sdl.h hasn't been included emulate the data structure for surface
#ifndef _SDL_H

typedef struct{
  unsigned char r;
  unsigned char g;
  unsigned char b;
  unsigned char unused;
} SDL_Color;

typedef struct{
  int ncolors;
  SDL_Color *colors;
} SDL_Palette;

typedef struct SDL_PixelFormat {

	SDL_Palette *palette;
  unsigned char  BitsPerPixel;
  unsigned char  BytesPerPixel;
  unsigned char  Rloss, Gloss, Bloss, Aloss;
  unsigned char  Rshift, Gshift, Bshift, Ashift;
  unsigned char Rmask, Gmask, Bmask, Amask;
  unsigned char colorkey;
  unsigned char  alpha;

} SDL_PixelFormat;


typedef struct{
  short x, y;
  short w, h;
} SDL_Rect;


typedef struct SDL_Surface {
    unsigned int flags;                           /* Read-only */
    SDL_PixelFormat *format;                /* Read-only */
    int w, h;                               /* Read-only */
    unsigned short pitch;                           /* Read-only */
    void *pixels;                           /* Read-write */
    SDL_Rect clip_rect;                     /* Read-only */
    int refcount;                           /* Read-mostly */

  /* This structure also contains private fields not shown here */
} SDL_Surface;
#endif

// Get the name of the emulator

DLLH		char*			EK_GetEmulatorName();
DLLH		char*			EK_GetSystemsEmulated();

// Get the available commands you can send to the emulator
DLLH		int				EK_GetNumberControllers();
DLLH		char*			EK_EnumerateInputs();
DLLH		char*			EK_EnumerateSysCalls();

DLLH		int				EK_EnumerateScreenCount();
DLLH		EK_VidTechFlags	EK_EnumerateVidTech();


DLLH		EK_BOOL			EK_SetOGLSurfaceTarget(int screen, GLuint texture, GLuint width, GLuint height);
DLLH		EK_BOOL			EK_SetOGLDirectTarget(int screen, 
								GLuint xpos, GLuint ypos, 
								GLuint rectWidth, GLuint rectHeight,
								GLuint screenWidth, GLuint screenHeight);

DLLH		EK_BOOL			EK_SetSDLSurfaceTarget(int screen, SDL_Surface* target, int width, int height);

#ifdef WIN32

DLLH		EK_BOOL			EK_SetGDIRenderTarget(int screen, HBITMAP *bitmap, int width, int height);

#endif

#ifdef USE_D3D9

DLLH		EK_BOOL			EK_SetD3DDeviceTarget(int screen, IDirect3DDevice9 *device, int width, int height);
DLLH		EK_BOOL			EK_SETD3DSurfaceTarget(int screen, IDirect3DSurface9 *surface, int width, int height);

#endif


// input functions

DLLH		void			EK_SendInput(int controller, char* command, char* value);
DLLH		void			EK_SendSysCall(char* command, char* data);

// file i/o functions

DLLH		EK_BOOL			EK_LoadRom(char* romPath);
DLLH		EK_BOOL			EK_UnloadRom();

DLLH		EK_BOOL			EK_SaveState(char* saveStatePath);
DLLH		EK_BOOL			EK_LoadState(char* saveStatePath);

DLLH		EK_BOOL			EK_SaveMemory(char* saveMemoryPath);
DLLH		EK_BOOL			EK_LoadMemory(char* saveMemoryPath);

// start and stop emulation

DLLH		EK_BOOL			EK_StartEmulation();
DLLH		EK_BOOL			EK_StopEmulation();
DLLH		EK_BOOL			EK_StepEmulation(double dt);

DLLH		EK_BOOL			EK_Init();
DLLH		EK_BOOL			EK_Shutdown();

#endif
/*

Contort version 1.0

Joel Longanecker


Command line parameters

	--emupath=[path]			|	--emupath=["path"]				-- __required__ path to emulator
	--rompath=[path]			|	--rompath=["path"]				-- optional path to rom
	--options=[option]			|	--options=["options"]			-- optional options specific to the emulator
	--runFromDirectory=[path]	|	--runFromDirectory=["path"]		-- directory to run emulator from

	--2P_SH_VSPLIT | --2P_SH_HSPLIT | --2P_SS_VSPLIT | --2P_SS_HSPLIT		
			-- options on how to split the screen (can only use one)

	--disableTaskbar			
			-- disable task bar (usefull for windows 7 / Vista)

	--quitSequence=[comma seperated hex values for windows virtual keys]	
			-- keys used to exit the program (default is escape)



  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.


*/

// constants

#define		CT_NORMAL						0x000
#define		CT_2P_SHARESCREEN_VSPLIT		0x001
#define		CT_2P_SHARESCREEN_HSPLIT		0x002
#define		CT_2P_SPLITSCREEN_VSPLIT		0x003
#define		CT_2P_SPLITSCREEN_HSPLIT		0x004

// Libraries
#pragma comment (lib, "opengl32")


// Header files
#include <Windows.h>
#include <WinBase.h>

#include <gl/gl.h>
#include <gl/glu.h>
#include <string.h>
#include <wchar.h>
#include <math.h>


// Function Prototypes

BOOL CreateChildProcess(LPSTR emuPath, LPSTR romPath, LPSTR options, LPPROCESS_INFORMATION procInfo);

HWND CreateMainWindow(HINSTANCE hInstance);

BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM param);

//LRESULT CALLBACK KeyboardProc(int code, WPARAM wParam, LPARAM lParam);


void EnableOpenGL(HWND hWnd, HDC * hDC, HGLRC * hRC);
void DisableOpenGL(HWND hWnd, HDC hDC, HGLRC hRC);
void UpdateOpenGLWindow(HWND hWnd);

int NextPowerOfTwo(int x);


// Global Variables

int screen_width;
int screen_height;

RECT client_window_position;

int client_window_width;
int client_window_height;

int client_window_width_np2;
int client_window_height_np2;

double texture_ratio_width;
double texture_ratio_height;

HWND client_hWnd;
GLuint texture_handle;

int render_mode;

int client_bitmap_size;
int *client_bitmap_bytes;

BOOL disable_taskbar;

//

BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM param)
{
	DWORD id = GetWindowThreadProcessId(hwnd, NULL);
	if (id == (DWORD)param)
	{
		client_hWnd = hwnd;
		return FALSE;
	}
	return TRUE;

}

//LRESULT CALLBACK KeyboardProc(int code, WPARAM wParam, LPARAM lParam)
//{
//
//}

BOOL CreateChildProcess(LPCWSTR emuPath, LPCWSTR romPath, LPCWSTR options, LPCWSTR runFromDirectory, LPPROCESS_INFORMATION procInfo)
{
	WCHAR combinedOptions[2048];
	wsprintf(combinedOptions, TEXT("\"%s\" \"%s\" %s"), emuPath, romPath, options);


	STARTUPINFOW si;


	ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);

    ZeroMemory( procInfo, sizeof(procInfo) );	

	if(!CreateProcess(NULL, 
		combinedOptions,
		NULL,
		NULL,
		FALSE,
		0,
		NULL,
		runFromDirectory,
		&si,
		procInfo))
	{
		return false;
	}

	while(EnumWindows(EnumWindowsProc, procInfo->dwThreadId))
		Sleep(10);

	Sleep(100);

	GetClientRect(client_hWnd, &client_window_position);

	client_window_width = client_window_position.right - client_window_position.left;
	client_window_height = client_window_position.bottom - client_window_position.top;

	client_window_width_np2 = NextPowerOfTwo(client_window_width);
	client_window_height_np2 = NextPowerOfTwo(client_window_height);

	texture_ratio_width = (double)client_window_width / (double)client_window_width_np2;
	texture_ratio_height = (double)client_window_height / (double)client_window_height_np2;

	client_bitmap_size = 4 * client_window_width * client_window_height;
	client_bitmap_bytes = new int[client_bitmap_size];
	
	return true;
}

HWND CreateMainWindow(HINSTANCE hInstance)
{
	WNDCLASS wc;
	wc.style = CS_HREDRAW | CS_VREDRAW,
	wc.lpfnWndProc = DefWindowProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon( NULL, IDI_APPLICATION );
	wc.hCursor = LoadCursor( NULL, IDC_ARROW );
	wc.hbrBackground = (HBRUSH)GetStockObject( BLACK_BRUSH );
	wc.lpszMenuName = NULL;
	wc.lpszClassName = TEXT("Contort");
	
	RegisterClass( &wc );

	screen_width = GetSystemMetrics(SM_CXSCREEN);
	screen_height = GetSystemMetrics(SM_CYSCREEN);



	HWND hWnd = CreateWindowEx(
		WS_EX_TOPMOST|WS_EX_TOOLWINDOW, 
		TEXT("Contort"), 
		TEXT("Contort Window"), 		
		WS_POPUP|WS_VISIBLE|WS_SYSMENU,
		0, 0, screen_width, screen_height,
		NULL, NULL, hInstance, NULL );

	SetWindowLong(hWnd, GWL_EXSTYLE,
		GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT); 

	SetLayeredWindowAttributes(hWnd, 0,255, LWA_ALPHA);

	if(disable_taskbar)
	{
		HWND taskBar = FindWindow(TEXT("Shell_TrayWnd"), TEXT(""));
        HWND button = FindWindowEx(NULL, NULL, (LPCWSTR)0xC017, TEXT("Start"));
        ShowWindow(taskBar, SW_HIDE);
        ShowWindow(button,SW_HIDE);
		
	}

	ShowCursor(FALSE);

	return hWnd;
}



int NextPowerOfTwo(int x)
{
	int v = x-1; 
	v |= v >> 1;
	v |= v >> 2;
	v |= v >> 4;
	v |= v >> 8;
	v |= v >> 16;
	return v+1;
}


void EnableOpenGL(HWND hWnd, HDC * hDC, HGLRC * hRC)
{
    PIXELFORMATDESCRIPTOR pfd;
    int iFormat;

    *hDC = GetDC( hWnd );
    ZeroMemory( &pfd, sizeof( pfd ) );

    pfd.nSize = sizeof( pfd );
    pfd.nVersion = 1;
    pfd.dwFlags = PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL |
                  PFD_DOUBLEBUFFER;
    pfd.iPixelType = PFD_TYPE_RGBA;
    pfd.cColorBits = 24;
    pfd.cDepthBits = 16;
    pfd.iLayerType = PFD_MAIN_PLANE;

    iFormat = ChoosePixelFormat( *hDC, &pfd );
    SetPixelFormat( *hDC, iFormat, &pfd );

  
    *hRC = wglCreateContext( *hDC );
    wglMakeCurrent( *hDC, *hRC );

	glEnable(GL_TEXTURE_2D);
	glDisable(GL_DEPTH_TEST| GL_LIGHTING | GL_CULL_FACE);

	glGenTextures(1, &texture_handle);

	glBindTexture(GL_TEXTURE_2D, texture_handle);

	
}



void DisableOpenGL(HWND hWnd, HDC hDC, HGLRC hRC)
{
	delete [] client_bitmap_bytes;

    wglMakeCurrent( NULL, NULL );
    wglDeleteContext( hRC );
    ReleaseDC( hWnd, hDC );

	if(disable_taskbar)
	{
		HWND taskBar = FindWindow(TEXT("Shell_TrayWnd"), TEXT(""));
        HWND button = FindWindowEx(NULL, NULL, (LPCWSTR)0xC017, TEXT("Start"));
        ShowWindow(taskBar, SW_SHOW);
        ShowWindow(button,SW_SHOW);
	}

	ShowCursor(TRUE);
}

void UpdateOpenGLWindow(HWND hWnd, HDC hDC)
{
	HDC hDCClient = GetDC(client_hWnd);
	HDC memDC = CreateCompatibleDC(hDCClient);
	
	HBITMAP memBM = CreateCompatibleBitmap(hDCClient, 
		client_window_width_np2, 
		client_window_height_np2);
	
	SelectObject(memDC, memBM );
	
	BitBlt(memDC, 
		0, 0,
		client_window_width, client_window_height,
		hDCClient,
		client_window_position.left, client_window_position.top, 
		SRCCOPY);

	GetBitmapBits(memBM, client_bitmap_size, client_bitmap_bytes );

	

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR); 

	glTexImage2D(GL_TEXTURE_2D, 
		0, 3, 
		client_window_width_np2, client_window_height_np2, 
		0, 
		GL_BGRA_EXT, 
		GL_UNSIGNED_BYTE, client_bitmap_bytes);

	ReleaseDC(0, hDCClient);

	glClearColor(0,0,0,255);
	glClear(GL_COLOR_BUFFER_BIT);

	double s = texture_ratio_width;
    double t = texture_ratio_height;

	switch(render_mode)
	{
	
	
	case CT_2P_SHARESCREEN_HSPLIT:

		glPushMatrix();

		glBegin(GL_QUADS);

			glTexCoord2d(0, t); glVertex2d(-1, -1);
            glTexCoord2d(s, t); glVertex2d(1, -1);
            glTexCoord2d(s, 0); glVertex2d(1, 0);
            glTexCoord2d(0, 0); glVertex2d(-1,0);

		glEnd();

		glRotated(180,0,0,1);
		
		glBegin(GL_QUADS);

            glTexCoord2d(0, t);glVertex2d(-1, -1);
            glTexCoord2d(s, t);glVertex2d(1, -1);
            glTexCoord2d(s, 0);glVertex2d(1, 0);
            glTexCoord2d(0, 0);glVertex2d(-1,0);

		glEnd();

		glPopMatrix();

		break;

	case CT_2P_SHARESCREEN_VSPLIT:

		glPushMatrix();
		
		glRotated(90,0,0,1);

		glBegin(GL_QUADS);

			glTexCoord2d(0, t); glVertex2d(-1, -1);
            glTexCoord2d(s, t); glVertex2d(1, -1);
            glTexCoord2d(s, 0); glVertex2d(1, 0);
            glTexCoord2d(0, 0); glVertex2d(-1,0);

		glEnd();

		glRotated(180,0,0,1);
		
		glBegin(GL_QUADS);

            glTexCoord2d(0, t);glVertex2d(-1, -1);
            glTexCoord2d(s, t);glVertex2d(1, -1);
            glTexCoord2d(s, 0);glVertex2d(1, 0);
            glTexCoord2d(0, 0);glVertex2d(-1,0);

		glEnd();

		glPopMatrix();

		break;

		case CT_2P_SPLITSCREEN_HSPLIT:

			glPushMatrix();

		glBegin(GL_QUADS);

			
		glTexCoord2d(0,0);		glVertex2d(-1,-1);
		glTexCoord2d(s/2,0);	glVertex2d(0,-1);
		glTexCoord2d(s/2,t);	glVertex2d(0,1);
		glTexCoord2d(0,t);		glVertex2d(-1,1);

		glEnd();

		glRotated(180,0,0,1);
		
		glBegin(GL_QUADS);

            glTexCoord2d(s/2,0);		glVertex2d(-1,-1);
            glTexCoord2d(s,0);			glVertex2d(0,-1);
            glTexCoord2d(s,t);			glVertex2d(0,1);
            glTexCoord2d(s/2,t);		glVertex2d(-1,1);

		glEnd();

		glPopMatrix();

		break;

		case CT_2P_SPLITSCREEN_VSPLIT:

			glPushMatrix();

			
		glBegin(GL_QUADS);
	
			glTexCoord2d(0,t/2);		glVertex2d(-1,-1);
			glTexCoord2d(s,t/2);		glVertex2d(0,-1);
			glTexCoord2d(s,0);			glVertex2d(0,1);
			glTexCoord2d(0,0);			glVertex2d(-1,1);

		glEnd();

		
		
		glBegin(GL_QUADS);

            glTexCoord2d(0,t);			glVertex2d(0,-1);
            glTexCoord2d(s,t);			glVertex2d(1,-1);
            glTexCoord2d(s,t/2);		glVertex2d(1,1);
            glTexCoord2d(0,t/2);		glVertex2d(0,1);

		glEnd();

		glPopMatrix();

		break;

	case CT_NORMAL:
	default:
	
		glBegin(GL_QUADS);

            glTexCoord2d(0, t); glVertex2d(-1, -1);
            glTexCoord2d(s, t); glVertex2d(1, -1);
            glTexCoord2d(s, 0); glVertex2d(1, 1);
            glTexCoord2d(0, 0); glVertex2d(-1, 1);

        glEnd();
	
		break;
		
	}

	SwapBuffers(hDC);

	BLENDFUNCTION bfunc;
		bfunc.AlphaFormat = 0;
		bfunc.BlendFlags = 0;
		bfunc.BlendOp = AC_SRC_OVER;
		bfunc.SourceConstantAlpha = 255;

		POINT ptPos = {0, 0};
		SIZE ptSize = {screen_width, screen_height};
		

	UpdateLayeredWindow(hWnd, 
		NULL, NULL, &ptSize, hDC, &ptPos, 0,
		&bfunc, ULW_ALPHA);

	

}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, 
				   LPSTR lpCmdLine, int iCmdShow)
{
	LPWSTR *szArgList;
    int argCount;

	LPCWSTR pathToEmulator = NULL;
	LPCWSTR pathToRom = NULL;
	LPCWSTR cmdLineOptions = NULL;
	LPCWSTR runFromDirectory = NULL;

	szArgList = CommandLineToArgvW(GetCommandLine(), &argCount);

	int virtualKeyQuitSequence[8];
	int vKeyCount = 0;

	for(int i = 0; i < argCount; i++)
	{
		LPCWSTR s = szArgList[i];

		// emulation parameters

		if(wcsstr( s, TEXT("--emupath=")) > 0)
		{
			pathToEmulator = wcsstr( s, TEXT("="))+1;
			continue;
		}

		if(wcsstr( s, TEXT("--rompath=")) > 0)
		{
			pathToRom = wcsstr( s, TEXT("="))+1;
			continue;
		}

		if(wcsstr( s, TEXT("--options=")) > 0)
		{
			cmdLineOptions = wcsstr( s, TEXT("="))+1;
			continue;
		}

		if(wcsstr( s, TEXT("--runFromDirectory=")) > 0)
		{
			runFromDirectory = wcsstr( s, TEXT("="))+1;
			continue;
		}


		if(wcsstr( s, TEXT("--2P_SH_VSPLIT")) > 0)
		{
			render_mode = CT_2P_SHARESCREEN_VSPLIT;
			continue;
		}

		
		if(wcsstr( s, TEXT("--2P_SH_HSPLIT")) > 0)
		{
			render_mode = CT_2P_SHARESCREEN_HSPLIT;
			continue;
		}

		if(wcsstr( s, TEXT("--2P_SS_VSPLIT")) > 0)
		{
			render_mode = CT_2P_SPLITSCREEN_VSPLIT;
			continue;
		}

		if(wcsstr( s, TEXT("--2P_SS_HSPLIT")) > 0)
		{
			render_mode = CT_2P_SPLITSCREEN_HSPLIT;
			continue;
		}

		if(wcsstr( s, TEXT("--disableTaskbar")) > 0)
		{
			disable_taskbar = TRUE;
			continue;
		}

		if(wcsstr( s,  TEXT("--quitSequence=")) > 0)
		{
			const WCHAR * original = wcsstr( s, TEXT("="))+1;
			WCHAR * ptrOriginal =(WCHAR*) original;

			WCHAR * str = wcstok( ptrOriginal , TEXT(","));
		

			while( str != NULL && vKeyCount <8 )
			{
				virtualKeyQuitSequence[vKeyCount++] = (int)wcstol(str, NULL,16);
				str = wcstok(NULL, TEXT(","));
			}

			continue;
		}
	}


	// make sure atleast the path to the emulator is valid.

	if (pathToEmulator == NULL)
		PostQuitMessage(1);


	// create window
	HWND hWnd = CreateMainWindow(hInstance);

	PROCESS_INFORMATION pInfo;
	
	// create child process
	if (!CreateChildProcess(pathToEmulator, pathToRom, cmdLineOptions, runFromDirectory, &pInfo))
		PostQuitMessage(2);

	HDC hdc;
	HGLRC hrc;

	EnableOpenGL(hWnd, &hdc, &hrc);

	for(;;)
	{
		int keyCount = 0;
		for(int i = 0; i <= vKeyCount; i++)

		{

			if(GetAsyncKeyState(virtualKeyQuitSequence[i]) )
			{
				keyCount+=1;
				
			}
		}

		if(keyCount > 0)
		{
			if(keyCount == vKeyCount)
			{
				SendMessage(client_hWnd, WM_CLOSE, 0,0);
					break;
			}
		}

		DWORD exCode;
		if(!GetExitCodeProcess(pInfo.hProcess, &exCode) || exCode != STILL_ACTIVE)
		{
			break;
		}

		MSG msg;

		if ( PeekMessage( &msg, NULL, 0, 0, PM_REMOVE )  )
		{
			
			// handle or dispatch messages
			if ( msg.message == WM_QUIT ) 
			{
				break;
			} 
			else 
			{
				TranslateMessage( &msg );
				DispatchMessage( &msg );
			}
			
		} 
		else
		{
			UpdateOpenGLWindow(hWnd, hdc);

		}


	}


	DisableOpenGL(hWnd, hdc, hrc);
}
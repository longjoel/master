#include <Windows.h>

#include <gl/gl.h>
#include <gl/glu.h>

#include <string.h>
#include <wchar.h>

#include <math.h>


LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
void EnableOpenGL(HWND hWnd, HDC * hDC, HGLRC * hRC);
void DisableOpenGL(HWND hWnd, HDC hDC, HGLRC hRC);
void UpdateGLWindow(HWND hWnd);
void RegisterWindowClass(HINSTANCE hInstance);
void Draw(HWND hWnd, GLuint texID);


HWND clientHwnd;



int NextPowerOfTwo(int x)
{
	unsigned int v = x; // compute the next highest power of 2 of 32-bit v

v--;
v |= v >> 1;
v |= v >> 2;
v |= v >> 4;
v |= v >> 8;
v |= v >> 16;
v++;

return v;
}


BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM param)
{
	DWORD id = GetWindowThreadProcessId(hwnd, NULL);
	if (id == (DWORD)param)
	{
		clientHwnd = hwnd;
		return false;
	}
	return true;

}




int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, 
				   LPSTR lpCmdLine, int iCmdShow)
{

	STARTUPINFO si;
    PROCESS_INFORMATION pi;

	


	WCHAR combinedOptions[2048];
	HWND hWnd;
	HDC hDC;
	HGLRC hRC;
	
	MSG msg;
	BOOL quit = FALSE;


	LPWSTR *szArgList;
    int argCount;

	LPWSTR pathToEmulator = NULL;
	LPWSTR pathToRom = NULL;
	LPWSTR cmdLineOptions = NULL;

	bool splitScreen = false;

	GLuint texID;


    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );	


    szArgList = CommandLineToArgvW(GetCommandLine(), &argCount);


	for(int i = 0; i < argCount; i++)
	{
		LPWSTR s = szArgList[i];

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

	}

	if(pathToEmulator == NULL)
	{
		exit(1);
	}

	wsprintf(combinedOptions, TEXT("\"%s\" \"%s\" %s"), pathToEmulator, pathToRom, cmdLineOptions);

	


	
	RegisterWindowClass(hInstance);

	hWnd = CreateWindowEx(
		WS_EX_TOPMOST|WS_EX_TOOLWINDOW , 
		TEXT("GLSample"), 
		TEXT("OpenGL Sample"), 
		
		 WS_POPUP|WS_VISIBLE|WS_SYSMENU,
		0, 0, 512, 512,
		NULL, NULL, hInstance, NULL );

	SetWindowLong(hWnd, GWL_EXSTYLE,
    GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT); 

	
	
	
	// enable OpenGL for the window
	EnableOpenGL( hWnd, &hDC, &hRC );

	SetLayeredWindowAttributes(hWnd, 0,255, LWA_ALPHA);


	// init openGL

	glEnable(GL_TEXTURE_2D);

	glDisable(GL_DEPTH_TEST| GL_LIGHTING | GL_CULL_FACE);

	glGenTextures(1, &texID);

	if(!CreateProcess(NULL, 
			combinedOptions,
		NULL,
		NULL,
		FALSE,
		0,
		NULL,
		NULL,
		&si,
		&pi))
	{
		DWORD error = GetLastError();
		exit(error);
	}

	

	while(EnumWindows(EnumWindowsProc, pi.dwThreadId))
		Sleep(100);


	// program main loop
	while ( !quit )
	{
		if(GetAsyncKeyState( VK_ESCAPE) < 0)
		{
			SendMessage(clientHwnd, WM_CLOSE, 0,0);
				break;
		}
		

		// check the child process

		DWORD exCode;

		if(!GetExitCodeProcess(pi.hProcess, &exCode) || exCode != STILL_ACTIVE)
		{
			break;
		}

		
		// check for messages
		if ( PeekMessage( &msg, NULL, 0, 0, PM_REMOVE )  )
		{
			
			// handle or dispatch messages
			if ( msg.message == WM_QUIT ) 
			{
				quit = TRUE;
			} 
			else 
			{
				TranslateMessage( &msg );
				DispatchMessage( &msg );
			}
			
		} 
		else 
		{
			Draw(clientHwnd, texID);

			SwapBuffers( hDC );

			UpdateGLWindow(hWnd);
			
		}
		
	}
	
	// shutdown OpenGL
	DisableOpenGL( hWnd, hDC, hRC );
	
	// destroy the window explicitly
	DestroyWindow( hWnd );
	
	return msg.wParam;
	
}


void RegisterWindowClass(HINSTANCE hInstance)
{
	WNDCLASS wc;
	wc.style = CS_HREDRAW | CS_VREDRAW,
	wc.lpfnWndProc = WndProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon( NULL, IDI_APPLICATION );
	wc.hCursor = LoadCursor( NULL, IDC_ARROW );
	wc.hbrBackground = (HBRUSH)GetStockObject( BLACK_BRUSH );
	wc.lpszMenuName = NULL;
	wc.lpszClassName = TEXT("GLSample");
	
	RegisterClass( &wc );
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	
	switch (message)
	{
		
	case WM_CREATE:
		return 0;
		
	case WM_CLOSE:
		PostQuitMessage( 0 );
		return 0;
		
	case WM_DESTROY:
		return 0;
		
	case WM_KEYDOWN:
		switch ( wParam )
		{
			
		case VK_ESCAPE:
			PostQuitMessage(0);
			return 0;
			
		}
		return 0;



	
	default:
		return DefWindowProc( hWnd, message, wParam, lParam );
			
	}
	
}



void EnableOpenGL(HWND hWnd, HDC * hDC, HGLRC * hRC)
{
    PIXELFORMATDESCRIPTOR pfd;
    int iFormat;

    // get the device context (DC)
    *hDC = GetDC( hWnd );

    // set the pixel format for the DC
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

    // create and enable the render context (RC)
    *hRC = wglCreateContext( *hDC );
    wglMakeCurrent( *hDC, *hRC );
}

void DisableOpenGL(HWND hWnd, HDC hDC, HGLRC hRC)
{
    wglMakeCurrent( NULL, NULL );
    wglDeleteContext( hRC );
    ReleaseDC( hWnd, hDC );
}



void UpdateGLWindow(HWND hWnd)
{
		HDC hdc = GetWindowDC(hWnd);

			BLENDFUNCTION bfunc;
			bfunc.AlphaFormat = 0;
			bfunc.BlendFlags = 0;
			bfunc.BlendOp = AC_SRC_OVER;
			bfunc.SourceConstantAlpha = 255;

			POINT ptPos = {128,128};
			SIZE ptSize = {512,512} ;
		

		 UpdateLayeredWindow(hWnd, NULL, NULL, &ptSize, hdc, &ptPos, 0,
            &bfunc, ULW_ALPHA);

				ReleaseDC(hWnd, hdc);

	}


void Draw(HWND hWnd, GLuint texID)
{
	RECT rc;

GetClientRect(hWnd, &rc); 

int w = rc.right-rc.left;
int h = rc.bottom-rc.top;

int npw = NextPowerOfTwo(w);
int nph = NextPowerOfTwo(h);

HDC hDC = GetDC(hWnd);
HDC memDC = CreateCompatibleDC(hDC);
HBITMAP memBM = CreateCompatibleBitmap(hDC, NextPowerOfTwo( w), NextPowerOfTwo(h));
SelectObject(memDC, memBM );
BitBlt(memDC, 0, 0,w,h , hDC, rc.left, rc.top , SRCCOPY );

int size = 4 * w * h;
int *lpBits = new int[size];

GetBitmapBits(memBM, size, lpBits );

 glBindTexture(GL_TEXTURE_2D, texID);

			 //glPixelStorei(GL_UNPACK_ALIGNMENT,3);


			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR); 

			 glTexImage2D(GL_TEXTURE_2D, 0, 3, npw, nph , 0, GL_BGRA_EXT, GL_UNSIGNED_BYTE, lpBits);

delete [] lpBits; 

glClearColor(0,0,0,255);

			glClear(GL_COLOR_BUFFER_BIT);

			
			
			
			 double s = (double)(w) / (double)NextPowerOfTwo( w);
             double t = (double)(h) / (double)NextPowerOfTwo( h);

			 glBegin(GL_QUADS);

                    glTexCoord2d(0, t); glVertex2d(-.9, -.9);
                    glTexCoord2d(s, t); glVertex2d(.9, -.9);
                    glTexCoord2d(s, 0); glVertex2d(.9, .9);
                    glTexCoord2d(0, 0); glVertex2d(-.9, .9);

             glEnd();



ReleaseDC( 0, hDC );



}
/*

Copyright (c) 2012, Joel Longanecker (Joel.Longanecker@gmail.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


*/





#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

#include "SDL/SDL.h"
#include "main.h"

/* Add function definitions here */

void*				sunfish_init_video			(int width, int height)
{
	SDL_Init(SDL_INIT_VIDEO);	
	SDL_Surface* screen = SDL_SetVideoMode(width, height, 32, SDL_SWSURFACE | SDL_DOUBLEBUF | SDL_NOFRAME);
	SDL_ShowCursor(0);
	
	return (void*) screen;	
}

void*				sunfish_decode_videoptr 	(void* surface)
{
	SDL_Surface* s = (SDL_Surface*) surface;
	
	return (void*) s->pixels;
}

void				sunfish_swap_buffers		(void* surface)
{
	SDL_Surface* s = (SDL_Surface*) surface;
	SDL_Flip(s);
}

void				sunfish_poll_events			(void)
{
	SDL_Event e;
	SDL_PollEvent(&e);
	if(e.type == SDL_QUIT)
		exit(0);
}

void				sunfish_poll_mouse_state	(int* mouse_x, int* mouse_y, int* mouse_buttons)
{
	SDL_PumpEvents();
	*mouse_buttons = SDL_GetMouseState(mouse_x, mouse_y);
}

void				sunfish_warp_mouse			(int mouse_x, int mouse_y)
{
	SDL_WarpMouse(mouse_x, mouse_y);
}


void				sunfish_poll_keyboard_State	(unsigned char * ref_key_array)
{
	SDL_PumpEvents();
	Uint8 *keystate = SDL_GetKeyState(NULL);
	memmove ((void*)ref_key_array, (void*)keystate, sizeof(unsigned char*)*320);

}
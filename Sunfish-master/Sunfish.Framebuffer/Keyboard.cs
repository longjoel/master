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



using System;
using System.Runtime.InteropServices;
using Sunfish.Common;

namespace Sunfish.Framebuffer
{
	/// <summary>
	/// Keyboard.
	/// </summary>
	public class Keyboard
	{

        [DllImport("SDL.DLL")]
        private static extern IntPtr SDL_GetKeyState(out int numkeys);

		//[DllImport("libSunfish_Interop.so")]
		//static extern void sunfish_poll_keyboard_State	( [MarshalAs(UnmanagedType.LPArray)] byte[] ref_key_array);

		private byte[] _keystates;

		internal Keyboard ()
		{
			 _keystates = new byte[512];
		}

		/// <summary>
		/// Gets the <see cref="Sunfish.Framebuffer.Keyboard"/> with the specified k.
		/// </summary>
		/// <param name='k'>
		/// If set to <c>true</c> k.
		/// </param>
		public bool this [Keys k] 
		{
			get 
			{ 
                int keysCopied;
                IntPtr keys = SDL_GetKeyState( out keysCopied);
                Marshal.Copy(keys, _keystates, 0, keysCopied);
				//sunfish_poll_keyboard_State(  _keystates);
				return (_keystates[(int)k] == 1);
			}
		}


	}
}


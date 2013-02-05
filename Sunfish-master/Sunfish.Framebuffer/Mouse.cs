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

namespace Sunfish.Framebuffer
{
    /// <summary>
    /// Mouse.
    /// </summary>
    public class Mouse
    {

        [DllImport("SDL.DLL")]
        public static extern byte SDL_GetMouseState(out int x, out int y);


        [DllImport("SDL.DLL")]
        public static extern void SDL_WarpMouse(short x, short y);

        [DllImport("SDL.DLL")]
        public static extern int SDL_ShowCursor(int toggle);

        internal Mouse()
        {
            SDL_ShowCursor(0);
        }


        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X
        {
            get
            {
                int x, y;

                SDL_GetMouseState(out x, out y);

                return x;
            }
        }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y
        {
            get
            {
                int x, y;

                SDL_GetMouseState(out x, out y);

                //sunfish_poll_mouse_state(out x, out y, out buttons);

                return y;
            }
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="Sunfish.Framebuffer.Mouse"/> left button.
        /// </summary>
        /// <value>
        /// <c>true</c> if left button; otherwise, <c>false</c>.
        /// </value>
        public bool LeftButton
        {
            get
            {
                int x, y, buttons;

                //sunfish_poll_mouse_state(out x, out y, out buttons);
                buttons = (int)SDL_GetMouseState(out x, out y);
                return ((buttons & 1) == 1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Sunfish.Framebuffer.Mouse"/> right button.
        /// </summary>
        /// <value>
        /// <c>true</c> if right button; otherwise, <c>false</c>.
        /// </value>
        public bool RightButton
        {
            get
            {
                int x, y, buttons;

                buttons = (int)SDL_GetMouseState(out x, out y);
                //sunfish_poll_mouse_state(out x, out y, out buttons);

                return ((buttons & 4) == 4);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Sunfish.Framebuffer.Mouse"/> middle button.
        /// </summary>
        /// <value>
        /// <c>true</c> if middle button; otherwise, <c>false</c>.
        /// </value>
        public bool MiddleButton
        {
            get
            {
                int x, y, buttons;

                buttons = (int)SDL_GetMouseState(out x, out y);
                //sunfish_poll_mouse_state(out x, out y, out buttons);

                return ((buttons & 2) == 2);
            }
        }



        /// <summary>
        /// Moves to.
        /// </summary>
        /// <param name='x'>
        /// X.
        /// </param>
        /// <param name='y'>
        /// Y.
        /// </param>
        public void MoveTo(int x, int y)
        {
            SDL_WarpMouse((short)x, (short)y);
            //sunfish_warp_mouse(x,y);
        }

    }
}


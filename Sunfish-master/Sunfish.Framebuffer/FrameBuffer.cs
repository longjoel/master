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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunfish.Framebuffer
{
	/// <summary>
	/// Frame buffer.
	/// </summary>
	public class FrameBuffer
	{

		[DllImport("libSunfish_Interop.so")]
		static extern IntPtr sunfish_init_video (int width, int height);

		[DllImport("libSunfish_Interop.so")]
		static extern void sunfish_swap_buffers (IntPtr screen);

		[DllImport("libSunfish_Interop.so")]
		static extern IntPtr sunfish_decode_videoptr (IntPtr screen);

		[DllImport("libSunfish_Interop.so")]
		static extern void sunfish_poll_events ();



		[DllImport ("libc.so.6")]
		public static extern IntPtr memcpy (IntPtr dest, IntPtr src, UIntPtr count);

		IntPtr _sdlSurface;
		IntPtr _pixelAddress;
		Bitmap _backbuffer;
		private Graphics _context;


		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		public int Width{ get; private set; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		public int Height { get; private set; }

		/// <summary>
		/// Gets the mouse.
		/// </summary>
		/// <value>
		/// The mouse.
		/// </value>
		public Mouse Mouse{ get; private set; }

		/// <summary>
		/// Gets the keyboard.
		/// </summary>
		/// <value>
		/// The keyboard.
		/// </value>
		public Keyboard Keyboard { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Sunfish.Framebuffer.FrameBuffer"/> class.
		/// </summary>
		/// <param name='width'>
		/// Width.
		/// </param>
		/// <param name='height'>
		/// Height.
		/// </param>
		public FrameBuffer (int width, int height)
		{

			_sdlSurface = sunfish_init_video (width, height);
			_pixelAddress = sunfish_decode_videoptr (_sdlSurface);
			_backbuffer = new Bitmap (width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

			Width = width;
			Height = height;

			Mouse = new Mouse();
			Keyboard = new Keyboard();
		}

		/// <summary>
		/// Gets the context.
		/// </summary>
		/// <value>
		/// The context.
		/// </value>
		public Graphics Context {
			get {
				if(_context == null)
					_context = Graphics.FromImage(_backbuffer);

				return _context;
			}
		}


		/// <summary>
		/// Swaps the buffers.
		/// </summary>
		public void SwapBuffers ()
		{
			var bd = _backbuffer.LockBits (new Rectangle (0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

			memcpy (_pixelAddress, bd.Scan0, new UIntPtr ((ulong)(Width * Height * 4)));

			_backbuffer.UnlockBits (bd);

			sunfish_swap_buffers (_sdlSurface);
		}

		/// <summary>
		/// Dos the events.
		/// </summary>
		public void DoEvents ()
		{
			sunfish_poll_events();
		}


	}
}


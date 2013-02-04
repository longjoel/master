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
using System.Drawing;
using System.Drawing.Drawing2D;

using Sunfish.Framebuffer;

using Sunfish.Common;
using Sunfish.Messaging;

namespace Sunfish.FrameBuffer_Test
{
	public class Program
	{
		public static void Main ()
		{
			var f = new FrameBuffer (640, 480);
			
			int sizeRect = 5;

			var messageQueue = new MessageHub ("xxbasexx");

			bool messageRecieved = false;

			messageQueue.RegisterRoute (typeof(TestMessage), (m) => {
				messageRecieved = !messageRecieved;});


			while (true) {

				if (messageRecieved)
					f.Context.Clear (Color.White);
				else
					f.Context.Clear (Color.Bisque);

				f.Context.DrawString ("Hello!", SystemFonts.DefaultFont, new SolidBrush (Color.Red), 20, 20);

				f.Context.DrawRectangle (Pens.Black, new Rectangle (f.Mouse.X, f.Mouse.Y, sizeRect, sizeRect));

				if (f.Keyboard [Keys.a]) {
					MessageHub.SendMessage("xxbasexx", new TestMessage());
				}

				f.SwapBuffers ();

				f.DoEvents ();


				var m = messageQueue.PollMessage();
				if(m != null)
					messageQueue.DispatchMessage(m);


			}

		}

	}

}


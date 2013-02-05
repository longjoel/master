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


namespace Sunfish.FrameBuffer_Test
{
    public class Program
    {
        public static void Main()
        {
            var frameBuffer = new FrameBuffer(640, 480);
            var clockFont = new Font(FontFamily.GenericMonospace, 72, FontStyle.Bold);
            var backgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, 480), Color.Black, Color.White);
            var foregroundBrush = new LinearGradientBrush(new Point(0, 280), new Point(0, 360), Color.White, Color.FromArgb(120, Color.Black));

            var px = new Pen(
                new LinearGradientBrush(new Point(0, 0), new Point(640, 480), Color.FromArgb(128, Color.PeachPuff),
                    Color.FromArgb(128, Color.DarkGreen)
                    ), 40.0f);


            while (true)
            {
                frameBuffer.Context.Clear(Color.Black);

                frameBuffer.Context.FillRectangle(backgroundBrush,
                    new Rectangle(0, 0, 640, 480));

                var time = DateTime.Now;

                var tSeconds = ((float)time.Second / 60.0f) * 360.0f - 90.0f;

                var tMSeconds = ((float)time.Millisecond / 1000.0f) * 360.0f;


                frameBuffer.Context.DrawArc(px,
                  new Rectangle(100, 15, 460, 450), tSeconds - 10.0f, 15.0f);

                frameBuffer.Context.DrawArc(px,
                   new Rectangle(100, 15, 460, 450), tMSeconds + tSeconds, 360.0f - 180.0f);

                frameBuffer.Context.DrawString(time.ToLongTimeString(),
                    clockFont,
                    foregroundBrush,
                   frameBuffer.Width / 2 - frameBuffer.Context.MeasureString(time.ToLongTimeString(), clockFont).Width / 2,
                frameBuffer.Height / 2 - clockFont.Height / 2);






                frameBuffer.SwapBuffers();

                frameBuffer.DoEvents();
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace CogitaGameEntities
{
    public static class Util
    {
        public static int GenTexture(string path)
        {
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);

            var texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texture);

            Bitmap bx = new Bitmap(path);

            BitmapData data = bx.LockBits(new Rectangle(0, 0, bx.Width, bx.Height),
               ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

           

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            bx.UnlockBits(data);
            bx.Dispose();

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return texture;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using System.Drawing;


namespace Unicorn21.OpenTKRenderer
{
    public class ContentManager
    {

        private static ContentManager _instance;
        public static ContentManager Instance { get { if (_instance == null) _instance = new ContentManager(); return _instance; } }

        private ContentManager()
        {
            _collection = new Dictionary<string, GLTexInfo>();
        }

        private Dictionary<string, GLTexInfo> _collection;

        public GLTexInfo LoadTexture(string s)
        {
            return this[s];
        }

        public void ClearTextures()
        {
            foreach (var i in _collection.Values)
            {
                GL.DeleteTexture(i.glID);
            }

            this._collection = new Dictionary<string, GLTexInfo>();
        }

        public GLTexInfo this[string s]
        {
            get
            {
                if (!_collection.Keys.Contains(s))
                {
                    if (!System.IO.File.Exists("Content/" + s + ".png")) throw new ApplicationException("Texture not Found <" + s + ".png>.");

                    Bitmap b = new Bitmap("Content/" + s + ".png");

                    System.Drawing.Imaging.BitmapData _textureData =
                        b.LockBits(
                        new System.Drawing.Rectangle(0, 0, b.Width, b.Height),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    var t = new GLTexInfo();

                    int texture;

                    GL.GenTextures(1, out texture);

                    t.glID = texture;
                    t.Width = b.Width;
                    t.Height = b.Height;

                    GL.BindTexture(TextureTarget.Texture2D, t.glID);


                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);



                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, b.Width, b.Height, 0,
        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _textureData.Scan0);

                    b.UnlockBits(_textureData);

                   

                    _collection[s] = t;


                   
                }

                return _collection[s];
            }

        }
    }
}

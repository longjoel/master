using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace clrEmukore
{

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SDL_Rect
    {
        /// <summary>
        /// x position of the upper-left corner of the rectangle.
        /// </summary>
        public short x;
        /// <summary>
        /// y position of the upper-left corner of the rectangle. 
        /// </summary>
        public short y;
        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public short w;
        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public short h;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public SDL_Rect(short x, short y, short w, short h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "x: " + x + ", y: " + y + ", w: " + w + ", h: " + h;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SDL_Color
    {
        /// <summary>
        /// Red Intensity
        /// </summary>
        public byte r;
        /// <summary>
        /// Green Intensity
        /// </summary>
        public byte g;
        /// <summary>
        /// Blue Intensity
        /// </summary>
        public byte b;
        /// <summary>
        /// Alpha Channel
        /// Currently unused
        /// </summary>
        public byte unused;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public SDL_Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.unused = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public SDL_Color(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.unused = a;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SDL_Palette
    {
        /// <summary>
        /// Number of colors used in this palette
        /// </summary>
        public int ncolors;
        /// <summary>
        /// Array of <see cref="SDL_Color"/> 
        /// structures that make up the palette.
        /// </summary>
        public SDL_Color[] colors;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SDL_PixelFormat
    {
        /// <summary>
        /// Pointer to the palette, or NULL if the BitsPerPixel>8
        /// Pointer to <see cref="SDL_Palette"/>
        /// </summary>
        public IntPtr palette;
        /// <summary>
        /// The number of bits used to represent each pixel in a surface. 
        /// Usually 8, 16, 24 or 32.
        /// </summary>
        public byte BitsPerPixel;
        /// <summary>
        /// The number of bytes used to represent each pixel in a surface. 
        /// Usually one to four.
        /// </summary>
        public byte BytesPerPixel;
        /// <summary>
        /// Precision loss of each color component (2[RGBA]loss)
        /// </summary>
        public byte Rloss;
        /// <summary>
        /// Precision loss of each color component (2[RGBA]loss)
        /// </summary>
        public byte Gloss;
        /// <summary>
        /// Precision loss of each color component (2[RGBA]loss)
        /// </summary>
        public byte Bloss;
        /// <summary>
        /// Precision loss of each color component (2[RGBA]loss)
        /// </summary>
        public byte Aloss;
        /// <summary>
        /// Binary left shift of each color component in the pixel value
        /// </summary>
        public byte Rshift;
        /// <summary>
        /// Binary left shift of each color component in the pixel value
        /// </summary>
        public byte Gshift;
        /// <summary>
        /// Binary left shift of each color component in the pixel value
        /// </summary>
        public byte Bshift;
        /// <summary>
        /// Binary left shift of each color component in the pixel value
        /// </summary>
        public byte Ashift;
        /// <summary>
        /// Binary mask used to retrieve individual color values
        /// </summary>
        public int Rmask;
        /// <summary>
        /// Binary mask used to retrieve individual color values
        /// </summary>
        public int Gmask;
        /// <summary>
        /// Binary mask used to retrieve individual color values
        /// </summary>
        public int Bmask;
        /// <summary>
        /// Binary mask used to retrieve individual color values
        /// </summary>
        public int Amask;
        /// <summary>
        /// Pixel value of transparent pixels
        /// </summary>
        public int colorkey;
        /// <summary>
        /// Overall surface alpha value
        /// </summary>
        public byte alpha;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="palette"></param>
        /// <param name="BitsPerPixel"></param>
        /// <param name="BytesPerPixel"></param>
        /// <param name="Rloss"></param>
        /// <param name="Gloss"></param>
        /// <param name="Bloss"></param>
        /// <param name="Aloss"></param>
        /// <param name="Rshift"></param>
        /// <param name="Gshift"></param>
        /// <param name="Bshift"></param>
        /// <param name="Ashift"></param>
        /// <param name="Rmask"></param>
        /// <param name="Gmask"></param>
        /// <param name="Bmask"></param>
        /// <param name="Amask"></param>
        /// <param name="colorkey"></param>
        /// <param name="alpha"></param>
        public SDL_PixelFormat(IntPtr palette, byte BitsPerPixel,
            byte BytesPerPixel, byte Rloss, byte Gloss,
            byte Bloss, byte Aloss, byte Rshift, byte Gshift,
            byte Bshift, byte Ashift, int Rmask, int Gmask,
            int Bmask, int Amask, int colorkey, byte alpha
            )
        {
            if (BitsPerPixel > 8)
            {
                this.palette = IntPtr.Zero;
            }
            else
            {
                this.palette = palette;
            }
            this.BitsPerPixel = BitsPerPixel;
            this.BytesPerPixel = BytesPerPixel;
            this.Rloss = Rloss;
            this.Gloss = Gloss;
            this.Bloss = Bloss;
            this.Aloss = Aloss;
            this.Rshift = Rshift;
            this.Gshift = Gshift;
            this.Bshift = Bshift;
            this.Ashift = Ashift;
            this.Rmask = Rmask;
            this.Gmask = Gmask;
            this.Bmask = Bmask;
            this.Amask = Amask;
            this.colorkey = colorkey;
            this.alpha = alpha;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SDL_Surface
    {
        /// <summary>
        /// Surface flags
        /// </summary>
        public int flags;
        /// <summary>
        /// Pixel format
        /// Pointer to SDL_PixelFormat
        /// </summary>
        public IntPtr format;
        /// <summary>
        /// Width of the surface
        /// </summary>
        public int w;
        /// <summary>
        /// Height of the surface
        /// </summary>
        public int h;
        /// <summary>
        /// Length of a surface scanline in bytes
        /// </summary>
        public short pitch;
        /// <summary>
        /// Pointer to the actual pixel data
        /// Void pointer.
        /// </summary>
        public IntPtr pixels;
        /// <summary>
        /// 
        /// </summary>
        public int offset;
        /// <summary>
        /// Hardware-specific surface info
        /// </summary>
        public IntPtr hwdata;
        /// <summary>
        /// surface clip rectangle
        /// </summary>
        public SDL_Rect clip_rect;
        /// <summary>
        /// 
        /// </summary>
        public int unused1;
        /// <summary>
        /// Allow recursive locks
        /// </summary>
        public int locked;
        /// <summary>
        /// info for fast blit mapping to other surfaces
        /// </summary>
        public IntPtr map;
        /// <summary>
        /// format version, bumped at every change to invalidate blit maps
        /// </summary>
        public int format_version;
        /// <summary>
        /// Reference count -- used when freeing surface
        /// </summary>
        public int refcount;
    }

}

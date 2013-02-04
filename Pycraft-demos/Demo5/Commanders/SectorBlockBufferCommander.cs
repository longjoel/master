using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Pycraft.Commanders
{
    public static class SectorBlockBufferCommander
    {
        public static Entities.SectorBlockBuffer CreateBlockBufferFromSectorByBlock(Entities.Sector s, byte block)
        {


            var nsbb = new Entities.SectorBlockBuffer();


            nsbb.IsDisposed = false;

            nsbb.VertexBufferArrays = new int[6];
            nsbb.VertexBufferCounts = new int[6];

            nsbb.TexBufferArrays = new int[6];
            nsbb.TexBufferCounts = new int[6];

            GL.GenBuffers(6, nsbb.VertexBufferArrays);
            GL.GenBuffers(6, nsbb.TexBufferArrays);

            for (int i = 0; i < 6; i++)
            {
                var sectorBlocks = SectorBlockCommander.GetSectorBlocksFromSector(s, block);
                var faceBlocks = (from x in sectorBlocks where x.Faces.Contains((Entities.SectorBlockFaces)i) select x).ToList();

                var points = new List<double>();
                var coords = new List<double>();

                foreach (var fb in faceBlocks)
                    points.AddRange((from x in SectorBlockCommander.GetVerticiesByFace(fb, (Entities.SectorBlockFaces)i) select x).ToList());

                nsbb.VertexBufferCounts[i] = points.Count / 3;

                foreach (var fb in faceBlocks)
                    coords.AddRange((from x in SectorBlockCommander.GetTexCoordsByFace((Entities.SectorBlockFaces)i) select x).ToList());

                nsbb.TexBufferCounts[i] = coords.Count / 2;

                GL.BindBuffer(BufferTarget.ArrayBuffer, nsbb.VertexBufferArrays[i]);

                GL.BufferData(BufferTarget.ArrayBuffer,
                    new IntPtr(sizeof(double) * nsbb.VertexBufferCounts[i] * 3),
                    points.ToArray(),
                    BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, nsbb.TexBufferArrays[i]);

                GL.BufferData(BufferTarget.ArrayBuffer,
                    new IntPtr(sizeof(double) * nsbb.TexBufferCounts[i] * 2),
                    coords.ToArray(),
                    BufferUsageHint.StaticDraw);
            }

            return nsbb;
        }

        public static void DisposeSectorBlockBuffer(Entities.SectorBlockBuffer sbb)
        {
            lock (sbb)
            {
                if (!sbb.IsDisposed)
                {
                    sbb.IsDisposed = true;
                    GL.Flush();
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.DeleteBuffers(6, sbb.TexBufferArrays);
                    GL.DeleteBuffers(6, sbb.VertexBufferArrays);
                }
            }
        }

        

        public static void DrawSectorBlockBuffer(Entities.SectorBlockBuffer b, Entities.SectorBlockFaces face)
        {
            lock (b)
            {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.TextureCoordArray);

                GL.BindBuffer(BufferTarget.ArrayBuffer, b.VertexBufferArrays[(int)face]);
                GL.VertexPointer(3, VertexPointerType.Double, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, b.TexBufferArrays[(int)face]);
                GL.TexCoordPointer(2, TexCoordPointerType.Double, 0, 0);


                GL.DrawArrays(BeginMode.Quads, 0, b.VertexBufferCounts[(int)face]);

                GL.DisableClientState(ArrayCap.TextureCoordArray);
                GL.DisableClientState(ArrayCap.VertexArray);


                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
        }

    }
}

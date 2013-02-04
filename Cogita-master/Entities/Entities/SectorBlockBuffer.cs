using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace CogitaTerrainObjects.Entities
{
    public class SectorBlockBuffer:IDisposable
    {
        public int[] VertexBufferArrays { get; set; }
        public int[] VertexBufferCounts { get; set; }
        public int[] TexBufferArrays { get; set; }
        public int[] TexBufferCounts { get; set; }
        public bool IsDisposed { get; set; }

        public BrickTypeEnum BrickType { get; set; }

        public SectorBlockBuffer (Entities.Sector s, BrickTypeEnum block)
        {
            var nsbb = this;

            BrickType = block;

            nsbb.IsDisposed = false;

            nsbb.VertexBufferArrays = new int[6];
            nsbb.VertexBufferCounts = new int[6];

            nsbb.TexBufferArrays = new int[6];
            nsbb.TexBufferCounts = new int[6];

            GL.GenBuffers(6, nsbb.VertexBufferArrays);
            GL.GenBuffers(6, nsbb.TexBufferArrays);

            for (int i = 0; i < 6; i++)
            {
                var sectorBlocks = s.GetSectorBlocksFromSector((byte) block);
                var faceBlocks = (from x in sectorBlocks where x.Faces.Contains((Entities.SectorBlockFaces)i) select x).ToList();

                var points = new List<double>();
                var coords = new List<double>();

                foreach (var fb in faceBlocks)
                    points.AddRange((from x in fb.GetVerticiesByFace( (Entities.SectorBlockFaces)i) select x).ToList());

                nsbb.VertexBufferCounts[i] = points.Count / 3;

                foreach (var fb in faceBlocks)
                    coords.AddRange((from x in SectorBlock.GetTexCoordsByFace((Entities.SectorBlockFaces)i) select x).ToList());

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

           
        }

        public  void Dispose()
        {
            var sbb = this;
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



        public void DrawSectorBlockBuffer( Entities.SectorBlockFaces face)
        {
            var b = this;
            lock (b)
            {

                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.TextureCoordArray);

                GL.BindBuffer(BufferTarget.ArrayBuffer, b.VertexBufferArrays[(int)face]);
                GL.VertexPointer(3, VertexPointerType.Double, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, b.TexBufferArrays[(int)face]);
                GL.TexCoordPointer(2, TexCoordPointerType.Double, 0, 0);

                GL.DrawArrays(BeginMode.Quads, 0, b.VertexBufferCounts[(int)face]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.DisableClientState(ArrayCap.TextureCoordArray);
                GL.DisableClientState(ArrayCap.VertexArray);


                
            }
        }
    }
}

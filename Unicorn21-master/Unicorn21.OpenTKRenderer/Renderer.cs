using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.GameObjects;

using Unicorn21.Geometry;

using OpenTK.Graphics.OpenGL;
//using OpenTK.Graphics;
using OpenTK;

using System.Drawing;


namespace Unicorn21.OpenTKRenderer
{
  public class Renderer
  {

    public bool DrawShadows { get; set; }

    private GameInstance _gameInstance;

    public Renderer(GameInstance gameInstance)
    {
      _gameInstance = gameInstance;

      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.Texture2D);

      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


      GL.Enable(EnableCap.Lighting);
      GL.Enable(EnableCap.Light0);

      //GL.Enable(EnableCap.Normalize);

      float[] ambient = { .6f, .6f, .6f, 1.0f };
      GL.Light(LightName.Light0, LightParameter.Ambient, ambient);

      float[] diffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
      GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse);

      float[] specular = { 0.5f, 0.5f, 0.5f, 1.0f };
      GL.Light(LightName.Light0, LightParameter.Specular, specular);

      //GL.ShadeModel(ShadingModel.Flat);


    }


    // master draw command
    //private void DrawGameObject(GameObject go)
    //{
    //    if (go is LevelChunk) DrawLevelChunk(go as LevelChunk);
    //}


    private void DrawDynamicGameObject(DynamicGameObject go)
    {

    }



    #region LevelComponentDrawing
    public void DrawLevelChunk(LevelChunk l)
    {
      if (l is Wall) DrawWall(l as Wall);
      if (l is Corridor) DrawCorridor(l as Corridor);
      if (l is Platform) DrawPlatform(l as Platform);
    }

    private void DrawWall(Wall w)
    {
      var wTex = ContentManager.Instance[w.WallTexture.TextureTag];
      GL.BindTexture(TextureTarget.Texture2D, wTex.glID);

      double total = 0;
      foreach (var wx in w.Area.Lines) total += wx.Length;

      double runningTotal = 0;

      var wt = w.WallTexture;

      SetupTexture(wt);

      var c = w.Area.Center;

      GL.Begin(BeginMode.Quads);
      foreach (var wall in w.Area.Lines)
      {
        var l1 = new Line2D(c, wall.A);
        var l2 = new Line2D(c, wall.B);

        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);

        GL.TexCoord2((runningTotal / total), _gameInstance.CurrentLevel.BaseFloorHeight / wTex.Height);
        GL.Vertex3(wall.A.X, _gameInstance.CurrentLevel.BaseFloorHeight, wall.A.Y);


        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);

        GL.TexCoord2(((wall.Length + runningTotal) / total), _gameInstance.CurrentLevel.BaseFloorHeight / wTex.Height);
        GL.Vertex3(wall.B.X, _gameInstance.CurrentLevel.BaseFloorHeight, wall.B.Y);


        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);

        GL.TexCoord2(((wall.Length + runningTotal) / total), _gameInstance.CurrentLevel.BaseCeilingHeight / wTex.Height);
        GL.Vertex3(wall.B.X, _gameInstance.CurrentLevel.BaseCeilingHeight, wall.B.Y);


        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);

        GL.TexCoord2((runningTotal / total), _gameInstance.CurrentLevel.BaseCeilingHeight / wTex.Height);
        GL.Vertex3(wall.A.X, _gameInstance.CurrentLevel.BaseCeilingHeight, wall.A.Y);

        runningTotal += wall.Length;
      }
      GL.End();
    }

    private static void SetupTexture(LevelTextureAttribute wt)
    {
      GL.MatrixMode(MatrixMode.Texture);
      GL.PushMatrix();
      GL.LoadIdentity();


      GL.Translate(wt.xOffset, wt.yOffset, 0);
      GL.Scale(-wt.xScale, wt.yScale, 1);
      GL.Rotate(wt.Rotation, 0, 0, 1);
      GL.PopMatrix();
      GL.MatrixMode(MatrixMode.Modelview);
    }

    private void DrawCorridor(Corridor c)
    {

      var floorTex = ContentManager.Instance[c.FloorTexture.TextureTag];
      var ceilingTex = ContentManager.Instance[c.CeilingTexture.TextureTag];
      var cWallTex = ContentManager.Instance[c.CeilingWallTexture.TextureTag];
      var cFloorTex = ContentManager.Instance[c.FloorWallTexture.TextureTag];


      var area = c.Area.Triangulate();

      SetupTexture(c.FloorTexture);
      GL.BindTexture(TextureTarget.Texture2D, floorTex.glID);
      GL.Begin(BeginMode.Triangles);

      var center = c.Area.Center;

      foreach (var renderTriangle in area)
      {
        GL.Normal3(0, 1, 0);

        GL.TexCoord2(renderTriangle.Points[0].X / floorTex.Width, renderTriangle.Points[0].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[0].X, c.FloorHeight, renderTriangle.Points[0].Y);

        GL.TexCoord2(renderTriangle.Points[1].X / floorTex.Width, renderTriangle.Points[1].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[1].X, c.FloorHeight, renderTriangle.Points[1].Y);

        GL.TexCoord2(renderTriangle.Points[2].X / floorTex.Width, renderTriangle.Points[2].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[2].X, c.FloorHeight, renderTriangle.Points[2].Y);
      }
      GL.End();


      SetupTexture(c.CeilingTexture);
      GL.BindTexture(TextureTarget.Texture2D, ceilingTex.glID);
      GL.Begin(BeginMode.Triangles);

      foreach (var renderTriangle in area)
      {
        GL.Normal3(0, -1, 0);

        GL.TexCoord2(renderTriangle.Points[0].X / ceilingTex.Width, renderTriangle.Points[0].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[0].X, c.CeilingHeight, renderTriangle.Points[0].Y);

        GL.TexCoord2(renderTriangle.Points[2].X / ceilingTex.Width, renderTriangle.Points[2].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[2].X, c.CeilingHeight, renderTriangle.Points[2].Y);

        GL.TexCoord2(renderTriangle.Points[1].X / ceilingTex.Width, renderTriangle.Points[1].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[1].X, c.CeilingHeight, renderTriangle.Points[1].Y);

      }

      GL.End();

      SetupTexture(c.CeilingWallTexture);


      GL.BindTexture(TextureTarget.Texture2D, cWallTex.glID);

      GL.Begin(BeginMode.Quads);

      double total = 0;
      foreach (var wx in c.Area.Lines) total += wx.Length;

      double runningTotal = 0;

      foreach (var wall in c.Area.Lines)
      {
        var l1 = new Line2D(center, wall.A);
        var l2 = new Line2D(center, wall.B);

        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);

        GL.TexCoord2((runningTotal / total), _gameInstance.CurrentLevel.BaseFloorHeight / cWallTex.Height);
        GL.Vertex3(wall.A.X, _gameInstance.CurrentLevel.BaseFloorHeight, wall.A.Y);


        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);
        GL.TexCoord2((wall.Length + runningTotal) / total, _gameInstance.CurrentLevel.BaseFloorHeight / cWallTex.Height);
        GL.Vertex3(wall.B.X, _gameInstance.CurrentLevel.BaseFloorHeight, wall.B.Y);


        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);
        GL.TexCoord2((wall.Length + runningTotal) / total, c.FloorHeight / cWallTex.Height);
        GL.Vertex3(wall.B.X, c.FloorHeight, wall.B.Y);


        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);
        GL.TexCoord2(runningTotal / total, c.FloorHeight / cWallTex.Height);
        GL.Vertex3(wall.A.X, c.FloorHeight, wall.A.Y);
      }
      GL.End();


      SetupTexture(c.FloorWallTexture);

      GL.BindTexture(TextureTarget.Texture2D, cFloorTex.glID);

      GL.Begin(BeginMode.Quads);
      foreach (var wall in c.Area.Lines)
      {
        var l1 = new Line2D(center, wall.A);
        var l2 = new Line2D(center, wall.B);

        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);

        GL.TexCoord2((runningTotal) / total, c.CeilingHeight / cFloorTex.Height);
        GL.Vertex3(wall.A.X, c.CeilingHeight, wall.A.Y);



        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);

        GL.TexCoord2((wall.Length + runningTotal) / total, c.CeilingHeight / cFloorTex.Height);
        GL.Vertex3(wall.B.X, c.CeilingHeight, wall.B.Y);


        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);

        GL.TexCoord2((wall.Length + runningTotal) / total, _gameInstance.CurrentLevel.BaseCeilingHeight / cFloorTex.Height);
        GL.Vertex3(wall.B.X, _gameInstance.CurrentLevel.BaseCeilingHeight, wall.B.Y);

        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);
        GL.TexCoord2((runningTotal) / total, _gameInstance.CurrentLevel.BaseCeilingHeight / cFloorTex.Height);
        GL.Vertex3(wall.A.X, _gameInstance.CurrentLevel.BaseCeilingHeight, wall.A.Y);

        runningTotal += wall.Length;

      }
      GL.End();
    }


    private void DrawPlatform(Platform p)
    {

      var floorTex = ContentManager.Instance[p.FloorTexture.TextureTag];
      var ceilingTex = ContentManager.Instance[p.CeilingTexture.TextureTag];
      var wallTex = ContentManager.Instance[p.WallTexture.TextureTag];

      var area = p.Area.Triangulate();

      var center = p.Area.Center;

      SetupTexture(p.FloorTexture);


      GL.BindTexture(TextureTarget.Texture2D, floorTex.glID);
      GL.Begin(BeginMode.Triangles);



      foreach (var renderTriangle in area)
      {
        GL.Normal3(0, 1, 0);

        GL.TexCoord2(renderTriangle.Points[0].X / floorTex.Width, renderTriangle.Points[0].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[0].X, p.FloorHeight, renderTriangle.Points[0].Y);

        GL.TexCoord2(renderTriangle.Points[1].X / floorTex.Width, renderTriangle.Points[1].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[1].X, p.FloorHeight, renderTriangle.Points[1].Y);

        GL.TexCoord2(renderTriangle.Points[2].X / floorTex.Width, renderTriangle.Points[2].Y / floorTex.Height);
        GL.Vertex3(renderTriangle.Points[2].X, p.FloorHeight, renderTriangle.Points[2].Y);
      }
      GL.End();

      SetupTexture(p.CeilingTexture);


      GL.BindTexture(TextureTarget.Texture2D, ceilingTex.glID);
      GL.Begin(BeginMode.Triangles);
      foreach (var renderTriangle in area)
      {
        GL.Normal3(0, -1, 0);

        GL.TexCoord2(renderTriangle.Points[0].X / ceilingTex.Width, renderTriangle.Points[0].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[0].X, p.CeilingHeight, renderTriangle.Points[0].Y);

        GL.TexCoord2(renderTriangle.Points[2].X / ceilingTex.Width, renderTriangle.Points[2].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[2].X, p.CeilingHeight, renderTriangle.Points[2].Y);

        GL.TexCoord2(renderTriangle.Points[1].X / ceilingTex.Width, renderTriangle.Points[1].Y / ceilingTex.Height);
        GL.Vertex3(renderTriangle.Points[1].X, p.CeilingHeight, renderTriangle.Points[1].Y);

      }

      GL.End();


      SetupTexture(p.WallTexture);

      GL.BindTexture(TextureTarget.Texture2D, ContentManager.Instance[p.WallTexture.TextureTag].glID);
      GL.Begin(BeginMode.Quads);

      double total = 0;
      foreach (var wx in p.Area.Lines) total += wx.Length;

      double runningTotal = 0;



      foreach (var wall in p.Area.Lines)
      {
        var l1 = new Line2D(center, wall.A);
        var l2 = new Line2D(center, wall.B);
        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);

        GL.TexCoord2((runningTotal / total), p.FloorHeight / wallTex.Height);
        GL.Vertex3(wall.A.X, p.FloorHeight, wall.A.Y);

        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);
        GL.TexCoord2((wall.Length + runningTotal) / total, p.FloorHeight / wallTex.Height);
        GL.Vertex3(wall.B.X, p.FloorHeight, wall.B.Y);

        GL.Normal3(l2.Normal.X, 0, l2.Normal.Y);
        GL.TexCoord2((wall.Length + runningTotal) / total, p.CeilingHeight / wallTex.Height);
        GL.Vertex3(wall.B.X, p.CeilingHeight, wall.B.Y);

        GL.Normal3(l1.Normal.X, 0, l1.Normal.Y);
        GL.TexCoord2((runningTotal / total), p.CeilingHeight / wallTex.Height);
        GL.Vertex3(wall.A.X, p.CeilingHeight, wall.A.Y);

        runningTotal += wall.Length;

      }
      GL.End();
    }

    #endregion


    public void RenderScene(Camera c)
    {

      c.SetupCamera();
      c.UseCamera();

      
      foreach (var g in _gameInstance.CurrentLevel.Chunks)
      {
        DrawLevelChunk(g);
      }

      GL.PushMatrix();

      GL.Disable(EnableCap.Lighting);

      foreach (
        var g in
        (from g1 in
         (from g2 in _gameInstance.CurrentLevel.StaticGameObjects select g2 as GameObject).Union(
           (from g3 in _gameInstance.LivingGameObjects select g3 as GameObject)
          )
         orderby new Line2D(c.Position, g1.Position).Length descending
         where g1.IsVisible
         select g1))
      {



        if (g is DynamicGameObject)
        {


        }
        if (g is StaticGameObject)
        {
          // BillboardCheatSphericalBegin();


          GL.BindTexture(TextureTarget.Texture2D, ContentManager.Instance[g.TextureHandle].glID);

          GL.PushMatrix();

          GL.Translate(g.X, g.Z, g.Y);

          GL.Rotate(-c.Angle+90, 0, 1, 0);

          // GL.Disable(EnableCap.Texture2D);
          GL.Begin(BeginMode.Quads);

          

          GL.TexCoord2(1, 0);
          GL.Vertex2(.5, 1);

          GL.TexCoord2(0, 0);
          GL.Vertex2(-.5, 1);

          GL.TexCoord2(0, 1);
          GL.Vertex2(-.5, 0);

          GL.TexCoord2(1, 1);
          GL.Vertex2(.5, 0);



          GL.End();
          //GL.Enable(EnableCap.Texture2D);

          // BillboardEnd();

          GL.PopMatrix();
        }
      }

      GL.Enable(EnableCap.Lighting);
      GL.PopMatrix();
    }


    private void BillboardCheatSphericalBegin()
    {

      float[] modelview = new float[16];
      int i, j;

      // save the current modelview matrix
      GL.PushMatrix();

      // get the current modelview matrix
      GL.GetFloat(GetPName.ModelviewMatrix, modelview);

      // undo all rotations
      // beware all scaling is lost as well
      //for (i = 0; i < 3; i++)
      //    for (j = 0; j < 3; j++)
      //    {
      //        if (i == j)
      //            modelview[i * 4 + j] = 1.0f;
      //        else
      //            modelview[i * 4 + j] = 0.0f;
      //    }

      for (i = 0; i < 3; i += 2)
        for (j = 0; j < 3; j++)
      {
        if (i == j)
          modelview[i * 4 + j] = 1.0f;
        else
          modelview[i * 4 + j] = 0.0f;
      }


      // set the modelview with no rotations
      GL.LoadMatrix(modelview);
    }



    private void BillboardEnd()
    {

      // restore the previously
      // stored modelview matrix
      GL.PopMatrix();
    }


  }
}


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tricycle;

namespace Breakout
{
  
  internal sealed class Program
  {
    enum GameResourceType
    {
      Background,
      Paddle,
      Ball,
      Brick
    }
    
    public class Rectangle
    {
      public double X{get;set;}
      public double Y{get;set;}
      public double W{get;set;}
      public double H{get;set;}
      
      public static bool IsOverlapped(Rectangle a, Rectangle b)
      {
        var ra = new System.Drawing.Rectangle((int)a.X,(int)a.Y,(int)a.W,(int)a.H);
        var rb = new System.Drawing.Rectangle((int)b.X,(int)b.Y,(int)b.W,(int)b.H);
        return ra.IntersectsWith(rb);
      }
    }
    
    abstract class BaseGameObject:Rectangle
    {
      public GameResourceType ResType{get; protected set;}
      public Rectangle Position {get;set;}
      public bool IsDead {get; protected set;}
      public BaseGameObject()
      {
        Position = new Program.Rectangle();
        IsDead = false;
      }
      
    }
    
    class Paddle:BaseGameObject
    {
      public Paddle():base()
      {
        ResType = GameResourceType.Paddle;
        
        W = 128;
        H = 16;
        
        Y = 600-(H*3);
      }
      
    }
    
    class Ball: BaseGameObject
    {
      public double VX{get;set;}
      public double VY{get;set;}
      
      static Random r;
      
      public Ball():base()
      {
        this.W =16;
        this.H = 16;
        ResType = GameResourceType.Ball;
        
        if(r == null)
          r = new Random();
        
        X = 400;
        Y = 500;
        
        var angle = r.NextDouble() * Math.PI*2;
        
        
        VX = Math.Cos(angle)*r.NextDouble()*5.0;
        VY = Math.Sin(angle)*r.NextDouble()*5.0;
        
      }
      
      public void Process(double dt)
      {
        if(!IsDead){
          X += dt * VX;
          Y += dt * VY;
          
          if(X < 0)
          {
            X = 0; VX = -VX;
          }
          
          if(X > 800)
          {
            X = 800; VX = -VX;
          }
          
          if(Y < 0)
          {
            Y= 0; VY = -VY;
          }
          
          if(Y > 800)
          {
            IsDead = true;
          }
        }
      }
    }
    
    
    private static void Main(string[] args)
    {
      using (var w = new GameWindow(800,600))
      {
        
       
        
        w.Mouse.IsVisible = false;


        
        var imageDict = new Dictionary<GameResourceType, Bitmap>();
        imageDict[GameResourceType.Background] = new Bitmap("..\\Content\\background.png");
        imageDict[GameResourceType.Ball] = new Bitmap("..\\Content\\ball1.png");
        imageDict[GameResourceType.Paddle] = new Bitmap("..\\Content\\paddle.png");
        imageDict[GameResourceType.Brick] = new Bitmap("..\\Content\\brick.png");
        
        var ballList = new List <Ball>();
        
        System.Threading.Tasks.Task.Factory.StartNew(
          ()=>{for(int j = 1; j < 100; j++)
            {
              lock(ballList)
              {
                for(int i = 0; i < 150; i++)
                {
                  ballList.Add(new Ball());
                }
              }
              System.Threading.Thread.Sleep(1750);
            }});
        var paddle = new Paddle();
        
        while (w.IsRunning) {
          
          paddle.X = w.Mouse.X - 64;
          
          w.DrawBitmap(imageDict[GameResourceType.Background],0,0);
        
          
          
          w.DrawBitmap(imageDict[GameResourceType.Paddle], (int)paddle.X, (int)paddle.Y);
         

          lock (ballList)
          {
            foreach (var b in ballList.Where(x => !x.IsDead))
            {
              
              b.Process(1);
              w.DrawBitmap(imageDict[GameResourceType.Ball], (int)b.X, (int)b.Y);
             
              
              if(Rectangle.IsOverlapped(b as Rectangle, paddle as Rectangle)|| Rectangle.IsOverlapped(paddle, b))
              {
                b.Y = paddle.Y-b.H;
                b.VY = -b.VY;
              }
              
              
            }
          }
          if(w.Keyboard[System.Windows.Forms.Keys.Escape])
            break;
          
          
          w.Update();
        }

      }
      
    }
    
  }
}

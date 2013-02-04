using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using OpenTK.Input;

namespace Pycraft
{

    class UIThreadQueue
    {
        private static readonly Lazy<UIThreadQueue> _instance = new Lazy<UIThreadQueue>(() => new UIThreadQueue());

        public static UIThreadQueue Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        Queue<Action<GameWindow>> _actionQueue;
        SemaphoreSlim _semaphore;

        private UIThreadQueue()
        {
            _actionQueue = new Queue<Action<GameWindow>>();
            _semaphore = new SemaphoreSlim(5);
        }

        public void QueueAction(Action<GameWindow> a)
        {
            if (_semaphore.Wait(1000))
            {
                lock (_actionQueue)
                {
                    _actionQueue.Enqueue(a);
                }
            }
        }

        public Action<GameWindow> Dequeue()
        {
            Action<GameWindow> a;
            lock (_actionQueue)
            {
                a = _actionQueue.Dequeue();
            }
            _semaphore.Release();
            return a;
        }

        public bool Any()
        {
            bool isAny = true;
            lock (_actionQueue)
            {
                isAny = _actionQueue.Any();
            }
            return isAny;
        }


    }

    class Drawing
    {
        public static void Clear()
        {
            UIThreadQueue.Instance.QueueAction((w) =>
            {
                GL.ClearColor(Color4.Chocolate);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            });
        }

        public static void SwapBuffers()
        {
            UIThreadQueue.Instance.QueueAction((w) =>
            {
                w.SwapBuffers();
            });
        }

    }

    class Program
    {


        static void Main(string[] args)
        {
            bool isRunning = true;

            Task.Factory.StartNew(() =>
            {
                var win = new GameWindow(800, 600);

                win.RenderFrame += new EventHandler<FrameEventArgs>((o, e) =>
                {
                    if (UIThreadQueue.Instance.Any())
                    {
                        var a = UIThreadQueue.Instance.Dequeue();
                        a(win);
                    }
                });

                win.UpdateFrame += new EventHandler<FrameEventArgs>((o, e) =>
                {
                    if (!isRunning)
                        win.Close();
                });

                win.Closed += (o, e) => { isRunning = false; };

                win.Run();
            });

            var map = Pycraft.Commanders.MapCommander.CreateMap();
            Commanders.MapCommander.SetBlock(map, 0, 0, 0, 0);

            var mapCursor = Commanders.MapCursorCommander.CreateMapCursorForMap(map, 0, 0, 0);
            
            

            while (isRunning)
            {

                Drawing.Clear();

                Commanders.MapCursorCommander.DrawCursor(mapCursor);    

                Drawing.SwapBuffers();

            };

        }
    }
}

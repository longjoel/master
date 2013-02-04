/// <summary>
///  This software is provided 'as-is', without any express or implied
///  warranty.  In no event will the authors be held liable for any damages
///  arising from the use of this software.
///
///  Permission is granted to anyone to use this software for any purpose,
///  including commercial applications, and to alter it and redistribute it
///  freely, subject to the following restrictions:
///
///  1. The origin of this software must not be misrepresented; you must not
///     claim that you wrote the original software. If you use this software
///     in a product, an acknowledgment in the product documentation would be
///     appreciated but is not required.
///  2. Altered source versions must be plainly marked as such, and must not be
///     misrepresented as being the original software.
///  3. This notice may not be removed or altered from any source distribution.
///
///  Joel Longanecker Joel.Longanecker@gmail.com
/// 
/// </summary>
namespace Tricycle
{
    using System;
    using System.Threading;

    using System.Windows;
    using System.Windows.Forms;

    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using System.Drawing;

    using System.Runtime.InteropServices;

    /// <summary>
    /// a form that allows us to hide the cursor.
    /// </summary>

    class zForm : Form
    {
        public zForm()
            : base()
        {
            DoubleBuffered = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        internal void ShowCursor(bool s)
        {
            if (s == true)
                Cursor.Show();
            else
                Cursor.Hide();
        }
    }

    /// <summary>
    /// Input handling for the mouse
    /// </summary>
    public class Mouse
    {
        /// <summary>
        /// The XY coordinates of the mouse
        /// </summary>
        internal Point Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int X { get { return Location.X; } }

        /// <summary>
        /// 
        /// </summary>
        public int Y { get { return Location.Y; } }


        /// <summary>
        /// Track if the left mouse button is pressed in
        /// </summary>
        public bool LeftMouseButton { get; internal set; }

        /// <summary>
        /// Track if the right mouse button is pressed in
        /// </summary>
        public bool RightMouseBotton { get; internal set; }

        /// <summary>
        /// Determine if the mouse is visible on the window
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Create the mouse.
        /// </summary>
        internal Mouse()
        {
            Location = new Point(0, 0);
            LeftMouseButton = false;
            RightMouseBotton = false;
            IsVisible = true;
        }
    }

    /// <summary>
    /// Input handling for the keyboard
    /// </summary>
    public class Keyboard
    {
        Queue<Keys> _keys;

        internal Keyboard()
        {
            _keys = new Queue<Keys>();
        }

        internal void EnqueueKey(Keys k)
        {
            _keys.Enqueue(k);
        }

        /// <summary>
        /// Check and see if there is a key in the queue
        /// </summary>
        /// <returns></returns>
        public Keys GetKey()
        {
            if (_keys.Count == 0)
                return Keys.None;

            return _keys.Dequeue();

        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        public bool GetGlobalKeyState(Keys k)
        {
            return GetAsyncKeyState(k) != 0;
        }

        /// <summary>
        /// scan through the queue and see if a given key is inside. Clears the queue if found.
        /// </summary>
        public bool this[Keys key]
        {
            get
            {
                if (_keys.Contains(key))
                {
                    _keys.Clear();
                    return true;
                }

                return false;
            }

        }

        public bool Tap(Keys key)
        {
            if (this[key])
            {
                while (this[key]) ;
                return true; }
            return false;

        }


    }

    public enum DPadValues : ushort
    {
        None = ushort.MaxValue,
        Up = 0,
        Down = 18000,
        Left = 27000,
        Right = 9000,
        UpLeft = 31500,
        UpRight = 4500,
        DownLeft = 22500,
        DownRight = 13500

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Joycaps
    {
        /// <summary>
        ///     Manufacturer identifier. Manufacturer identifiers are defined in Manufacturer and Product Identifiers.
        /// </summary>
        //[CLSCompliant(false)]
        public ushort WMid;
        /// <summary>
        ///     Product identifier. Product identifiers are defined in Manufacturer and Product Identifiers.
        /// </summary>
        //[CLSCompliant(false)]
        public ushort WPid;
        /// <summary>
        ///     Null-terminated string containing the joystick product name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public String SzPname;
        /// <summary>
        ///     Minimum X-coordinate.
        /// </summary>
        public Int32 WXmin;
        /// <summary>
        ///     Maximum X-coordinate.
        /// </summary>
        public Int32 WXmax;
        /// <summary>
        /// Minimum Y-coordinate.
        /// </summary>
        public Int32 WYmin;
        /// <summary>
        ///     Maximum Y-coordinate.
        /// </summary>
        public Int32 WYmax;
        /// <summary>
        ///     Minimum Z-coordinate.
        /// </summary>
        public Int32 WZmin;
        /// <summary>
        ///     Maximum Z-coordinate.
        /// </summary>
        public Int32 WZmax;
        /// <summary>
        ///     Number of joystick buttons.
        /// </summary>
        public Int32 WNumButtons;
        /// <summary>
        ///     Smallest polling frequency supported when captured by the  function.
        /// </summary>
        public Int32 WPeriodMin;

        public Int32 WPeriodMax;
        /// <summary>
        ///     Minimum rudder value. The rudder is a fourth axis of movement.
        /// </summary>
        public Int32 WRmin;
        /// <summary>
        ///     Maximum rudder value. The rudder is a fourth axis of movement.
        /// </summary>
        public Int32 WRmax;
        /// <summary>
        ///     Minimum u-coordinate (fifth axis) values.
        /// </summary>
        public Int32 WUmin;
        /// <summary>
        ///     Maximum u-coordinate (fifth axis) values.
        /// </summary>
        public Int32 WUmax;
        /// <summary>
        ///     Minimum v-coordinate (sixth axis) values.
        /// </summary>
        public Int32 WVmin;
        /// <summary>
        ///     Maximum v-coordinate (sixth axis) values.
        /// </summary>
        public Int32 WVmax;
        /// <summary>
        ///     Joystick capabilities The following flags define individual capabilities that a joystick might have:
        /// </summary>

        public Int32 WCaps;
        /// <summary>
        ///     Maximum number of axes supported by the joystick.
        /// </summary>
        public Int32 WMaxAxes;
        /// <summary>
        ///     Number of axes currently in use by the joystick.
        /// </summary>
        public Int32 WNumAxes;
        /// <summary>
        ///     Maximum number of buttons supported by the joystick.
        /// </summary>
        public Int32 WMaxButtons;
        /// <summary>
        ///     Null-terminated string containing the registry key for the joystick.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public String SzRegKey;
        /// <summary>
        ///     Null-terminated string identifying the joystick driver OEM.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public String SzOemvxD;
    }

    public class GamePad
    {
        [DllImport("winmm.dll")]
        internal static extern int joyGetPosEx(int uJoyID, ref JOYINFOEX pji);

        [DllImport("winmm.dll")]
        internal static extern Int32 joyGetDevCaps(int uJoyID, ref Joycaps pjc, Int32 cbjc);

        int padId;

        Joycaps caps;

        internal GamePad(int joyId)
        {
            padId = joyId;

            caps = new Joycaps();

            joyGetDevCaps(joyId, ref caps, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Joycaps)));


        }

        public DPadValues DirectionalPad
        {
            get
            {
                if ((caps.WCaps & 16) == 16)
                {
                    JOYINFOEX state = new JOYINFOEX();
                    state.dwFlags = 64; // POV
                    state.dwSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(JOYINFOEX));
                    joyGetPosEx(padId, ref state);
                    return (DPadValues)state.dwPOV;
                }
                else
                {
                    JOYINFOEX state = new JOYINFOEX();
                    state.dwFlags = 3; // POV
                    state.dwSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(JOYINFOEX));
                    joyGetPosEx(padId, ref state);

                    if (state.dwXpos == caps.WXmin)
                    {
                        return DPadValues.Left;

                    }
                    else
                        if (state.dwXpos == caps.WXmax)
                        {
                            return DPadValues.Right;

                        }
                        else
                            if (state.dwXpos == caps.WXmin)
                            {
                                return DPadValues.Left;

                            }
                            else
                                if (state.dwYpos == caps.WYmin)
                                {
                                    return DPadValues.Up;

                                }
                                else
                                    if (state.dwYpos == caps.WYmax)
                                    {
                                        return DPadValues.Down;

                                    }



                    return DPadValues.None;
                }


            }
        }

        public bool[] Buttons
        {
            get
            {
                JOYINFOEX state = new JOYINFOEX();
                state.dwFlags = 128; // buttons!
                state.dwSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(JOYINFOEX));
                joyGetPosEx(padId, ref state);

                var bList = new bool[16];

                for (int i = 0; i < 16; i++)
                {
                    var mask = (int)Math.Pow(2, i);

                    if ((state.dwButtons & mask) == mask)
                        bList[i] = true;
                    else
                        bList[i] = false;
                }

                return bList;
            }
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    struct JOYINFOEX
    {
        public int dwSize;
        public int dwFlags;
        public int dwXpos;
        public int dwYpos;
        public int dwZpos;
        public int dwRpos;
        public int dwUpos;
        public int dwVpos;
        public int dwButtons;
        public int dwButtonNumber;
        public int dwPOV;
        public int dwReserved1;
        public int dwReserved2;
    }

    enum JoyError : int
    {
        JOYERR_BASE = 160,
        JOYERR_PARMS = (JOYERR_BASE + 5),
        JOYERR_UNPLUGGED = (JOYERR_BASE + 7),
        MMSYSERR_BASE = 0,
        MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2),
        MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11)
    }

    public class GamePadCollection
    {
        [DllImport("winmm.dll")]
        static extern short joyGetNumDevs();

        static int GetGamepadCount()
        {
            // determine number of joysticks installed in Windows 95

            JOYINFOEX info = new JOYINFOEX();      // extended information

            info.dwFlags = 128; // buttons!
            info.dwSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(JOYINFOEX));


            Int32 dwResult;   // examine return values

            // Loop through all possible joystick IDs until we get the error
            // JOYERR_PARMS. Count the number of times we get JOYERR_NOERROR
            // indicating an installed joystick driver with a joystick currently
            // attached to the port.
            for (int i = 0; i < joyGetNumDevs(); i++)
            {
                dwResult = GamePad.joyGetPosEx(i, ref info);
                if (dwResult == (int)JoyError.JOYERR_UNPLUGGED || dwResult == (int)JoyError.JOYERR_PARMS)
                    return i;

            }
            return -1;

        } // JoysticksConnected

        private List<GamePad> gamePads;
        private int gpCount;

        internal GamePadCollection()
        {

            RefreshGamePadEnumeration();
        }

        public void RefreshGamePadEnumeration()
        {
            gamePads = new List<GamePad>();
            gpCount = (int)GetGamepadCount();

            for (int i = 0; i < gpCount; i++)
            {
                gamePads.Add(new GamePad(i));
            }
        }

        public int Count()
        {
            return gpCount;
        }

        public GamePad this[int i]
        {
            get
            {
                return gamePads[i];
            }
        }

    }

    /// <summary>
    /// The core of library, the game window. Does all the drawing and input processing.
    /// </summary>
    public class GameWindow : IDisposable
    {
        private Thread _thread;
        private Queue<Action<Form, Graphics, Bitmap>> _updateEvents;
        private System.Diagnostics.Stopwatch _eventsTimer;
        private long _lastRecordedTime;
        private bool _isPaused;

        /// <summary>
        /// Determine if the game window is still processing events, or not.
        /// </summary>
        public bool IsRunning { get; protected set; }

        public void Suspend()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        /// <summary>
        /// Accessor for the keyboard.
        /// </summary>
        public Keyboard Keyboard { get; protected set; }

        /// <summary>
        /// Accessor for the mouse.
        /// </summary>
        public Mouse Mouse { get; protected set; }

        public GamePadCollection GamePads { get; protected set; }

        /// <summary>
        /// Create a new game window with a given width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameWindow(int width, int height)
        {
            this.Keyboard = new Keyboard();
            this.Mouse = new Mouse();
            this.GamePads = new GamePadCollection();

            _eventsTimer = new System.Diagnostics.Stopwatch();
            _eventsTimer.Start();
            _lastRecordedTime = 0;
            _isPaused = false;

            IsRunning = true;

            _updateEvents = new Queue<Action<Form, Graphics, Bitmap>>();

            _thread = new Thread(
                () =>
                {

                    var form = new zForm();

                    form.Width = width;
                    form.Height = height;
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.FormBorderStyle = FormBorderStyle.FixedSingle;
                    form.ControlBox = false;

                    form.Closed += delegate(object sender, EventArgs e)
                    {
                        IsRunning = false;
                    };

                    form.MouseMove += delegate(object sender, MouseEventArgs e)
                    {
                        this.Mouse.Location = new Point(e.X, e.Y);
                        if (Mouse.IsVisible) form.ShowCursor(true);
                        else
                            form.ShowCursor(false);

                    };

                    form.MouseDown += delegate(object sender, MouseEventArgs e)
                    {
                        if (e.Button == MouseButtons.Left)
                            this.Mouse.LeftMouseButton = true;
                        if (e.Button == MouseButtons.Right)
                            this.Mouse.RightMouseBotton = true;

                    };

                    form.MouseUp += delegate(object sender, MouseEventArgs e)
                    {
                        if (e.Button == MouseButtons.Left)
                            this.Mouse.LeftMouseButton = false;
                        if (e.Button == MouseButtons.Right)
                            this.Mouse.RightMouseBotton = false;

                    };


                    form.PreviewKeyDown += delegate(object sender, PreviewKeyDownEventArgs e)
                    {
                        this.Keyboard.EnqueueKey(e.KeyCode);
                    };


                    form.Update();
                    form.Show();

                    var g = form.CreateGraphics();
                    var b = new Bitmap(width, height);

                    while (IsRunning)
                    {
                        if (!_isPaused)
                        {
                            using (var writelock = new ReaderWriterLockSlim())
                            {
                                while (_updateEvents.Count > 0 && IsRunning)
                                {
                                    // do event looping inside
                                    Application.DoEvents();

                                    var a = _updateEvents.Dequeue();

                                    a(form, g, b);
                                }

                            }
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }

                    if (IsRunning)
                        form.Close();

                });

            _thread.SetApartmentState(System.Threading.ApartmentState.STA);
            _thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        internal void Invoke(Action<Form, Graphics, Bitmap> a)
        {
            while (_updateEvents.Count != 0 && IsRunning) Thread.Sleep(0);

            using (var readlock = new ReaderWriterLockSlim())
            {

                _updateEvents.Enqueue(a);
            }
        }

        /// <summary>
        /// Clear the back buffer
        /// </summary>
        /// <param name="c">System Drawing Color</param>
        public void Clear(Color c)
        {
            Invoke((form, context, backBuffer) => { Graphics.FromImage(backBuffer).Clear(c); });
        }

        /// <summary>
        /// Draw a bitmap to the backbuffer
        /// </summary>
        /// <param name="srcBitmap">source for the blit</param>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public void DrawBitmap(Bitmap srcBitmap, int x, int y)
        {
            Invoke((form, context, backBuffer) => { Graphics.FromImage(backBuffer).DrawImage(srcBitmap, new PointF(x, y)); });
        }


        public void DrawBitmap(Bitmap srcBitmap, int srcX, int srcY, int dstX, int dstY, int width, int height)
        {
            Invoke((form, context, backBuffer) =>
            {
                Graphics.FromImage(backBuffer)
                    .DrawImage(srcBitmap,

                    new Rectangle(dstX, dstY, width, height),
                    new Rectangle(srcX, srcX, width, height),

                    GraphicsUnit.Pixel);
            });
        }

        public void DrawRect(int x, int y, int w, int h, Color c)
        {
            Invoke((form, context, backBuffer) => { Graphics.FromImage(backBuffer).FillRectangle(new SolidBrush(c), x, y, w, h); });
        }

        /// <summary>
        /// Copy the back buffer to the front buffer. Frame rate is locked at 60 FPS
        /// </summary>
        public void Update()
        {
            var currentTime = _eventsTimer.ElapsedMilliseconds;
            var elapsedTime = currentTime - _lastRecordedTime;



            _lastRecordedTime = currentTime;

            Invoke((form, context, backBuffer) =>
                   {
                       context.DrawImage(backBuffer, 0, 0);

                   }
                  );

            if (elapsedTime < 17)
                Thread.Sleep(17 - (int)elapsedTime);
        }

        /// <summary>
        /// Draw a string to the window.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <param name="formatString"></param>
        /// <param name="values"></param>
        public void DrawString(int x, int y, Color c, string formatString, params string[] values)
        {
            var font = new Font("Fixedsys", 32, FontStyle.Bold);
            Invoke((form, context, backBuffer) =>
                   {
                       Graphics.FromImage(backBuffer)
                           .DrawString(string.Format(formatString, values),
                                       font,
                                       new SolidBrush(c),
                                       x,
                                       y);
                   });
        }

        public void DrawString(int x, int y, Color c, Font f, string formatString, params string[] values)
        {
            var font = f;
            Invoke((form, context, backBuffer) =>
            {
                Graphics.FromImage(backBuffer)
                    .DrawString(string.Format(formatString, values),
                                font,
                                new SolidBrush(c),
                                x,
                                y);
            });
        }

        /// <summary>
        /// Draw a string to the window.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="formatString"></param>
        /// <param name="values"></param>
        public void DrawString(int x, int y, string formatString, params string[] values)
        {
            DrawString(x, y, Color.Black, formatString, values);
        }

        public void AddControl(int x, int y, Control c)
        {
            Invoke((f, g, b) =>
                   {
                       f.Controls.Add(c);
                       c.Top = y;
                       c.Left = x;

                   });
        }

        public void RemoveControl(Control c)
        {
            Invoke((f, g, b) => f.Controls.Remove(c));
        }

        public void MoveControl(Control c, int x, int y)
        {
            Invoke((f, g, b) => { c.Top = y; c.Left = x; });
        }


        /// <summary>
        /// Mop up.
        /// </summary>
        public void Dispose()
        {
            IsRunning = false;
            _thread.Join();
        }
    }
}

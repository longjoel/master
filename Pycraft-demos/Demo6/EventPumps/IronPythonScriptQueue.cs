using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;


namespace Pycraft.EventPumps
{
    public class IronPythonScriptQueue
    {
        private static readonly Lazy<IronPythonScriptQueue> _instance = new Lazy<IronPythonScriptQueue>(new Func<IronPythonScriptQueue>(() => new IronPythonScriptQueue()));

        public static IronPythonScriptQueue Instance { get { return _instance.Value; } }


        private Queue<string> _queuedScripts;
        private BackgroundWorker _bgWorker;
        private Entities.Map _map;

        private IronPythonScriptQueue()
        {
            _queuedScripts = new Queue<string>();
            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += new DoWorkEventHandler(DoWork);
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {


            while (!_bgWorker.CancellationPending)
            {
                if (_queuedScripts.Count == 0)
                    continue;

                //System.Threading.Tasks.Task.Factory.StartNew(new Action(() =>
                //   {
                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();

                scope.SetVariable("set_block", new Action<long, long, long, byte>((x, y, z, b) =>
                {
                    Commanders.MapCommander.SetBlock(_map, x, y, z, b);
                }));

                var script = engine.CreateScriptSourceFromString(_queuedScripts.Dequeue());

                try
                {
                    script.Compile();
                    script.Execute(scope);
                }
                catch (Exception ex)
                {
                    UIThreadEventPump.Instance.Add(new Action(() =>
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to Run Script. Reason: " + ex.Message);
                    }));


                }
                // }));
            }


        }

        public void Start()
        {
            _bgWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            _bgWorker.CancelAsync();
        }

        public void SetMap(Entities.Map map)
        {
            _map = map;
        }

        public void EnqueueScript(string script)
        {
            _queuedScripts.Enqueue(script);
        }
    }
}

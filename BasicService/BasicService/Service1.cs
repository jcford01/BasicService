using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BasicService
{
    public partial class Service1 : ServiceBase
    {
        int x = 0;
        private bool isrunning = true;
        string _path = @"C:\junk\abc123.txt";
        TimeSpan ts = new TimeSpan(0, 5, 0);
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            string _onStart = (string.Format("Service has begun {0}", DateTime.Now));
            if (File.Exists(_path))
            {
                File.Delete(_path);
                File.WriteAllText(_path, _onStart);
            }
            else
            {
                File.WriteAllText(_path, _onStart);
            }
            Task.Delay(ts).Wait();
            Execute();
        }
        protected override void OnStop()
        {
            x = 1;

            while (isrunning)
            {

            }

            string _onStop = ("\r\nService has ended " + DateTime.Now);
            File.AppendAllText(_path, _onStop);
            Stop();
        }
        public void Execute()
        {
            while (x == 0)
            {
                string _onInterval = ("\r\nCheck complete " + DateTime.Now);
                File.AppendAllText(_path, _onInterval);
                Task.Delay(ts).Wait();
            };

            isrunning = false;
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }
    }
}

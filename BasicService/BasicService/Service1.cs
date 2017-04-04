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
        string _path = @"C:\junk\abc123.txt";
        string _onStart = ("Service has begun " + DateTime.Now);
        string _onStop = ("\r\nService has ended " + DateTime.Now);
        string _onInterval = ("\r\nCheck complete " + DateTime.Now);
        TimeSpan ts = new TimeSpan(0, 5, 0);
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
                File.WriteAllText(_path, _onStart);
            }
            else
            {
                File.WriteAllText(_path, _onStart);
            }
            Execute();
        }
        protected override void OnStop()
        {
            File.AppendAllText(_path, _onStop);
            Stop();
        }
        public void Execute()
        {
            do
            {
                File.AppendAllText(_path, _onInterval);
                Task.Delay(ts);
            }
            while (x == 0);
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }
    }
}

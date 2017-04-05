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
using System.Timers;

namespace BasicService
{
    public partial class Service1 : ServiceBase
    {
        private bool _isRunning = true;
        string _path = Path.Combine(Directory.GetCurrentDirectory() + @"Log\abc123.txt");
        public Service1()
        {
            InitializeComponent();
            System.Timers.Timer _aTimer = new System.Timers.Timer(15000);
            _aTimer.AutoReset = true;
            _aTimer.Elapsed += Execute;
            _aTimer.Start();
        }
        protected override void OnStart(string[] args)
        {
            string _onStart = (string.Format("Service has begun {0}", DateTime.Now));
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
            File.WriteAllText(_path, _onStart);
            
        }
        protected override void OnStop()
        {
            while (_isRunning)
            {
            }
            string _onStop = ("\r\nService has ended " + DateTime.Now);
            File.AppendAllText(_path, _onStop);
        }
        public void Execute(object sender, ElapsedEventArgs e)
        {
            _isRunning = true;
            string _onInterval = ("\r\nCheck complete " + DateTime.Now);
            File.AppendAllText(_path, _onInterval);
            _isRunning = false;
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }
    }
}

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
        string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Log", "abc123.txt");
        string _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Log");
        System.Timers.Timer _aTimer = new System.Timers.Timer(15000);
        public Service1()
        {
            InitializeComponent();
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }
        protected override void OnStart(string[] args)
        {
            string _onStart = (string.Format("Service has begun {0}", DateTime.Now));
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            File.WriteAllText(_filePath, _onStart);
            _aTimer.AutoReset = true;
            _aTimer.Elapsed += Execute;
            _aTimer.Start();
        }
        protected override void OnStop()
        {
            _aTimer.Stop();
            while (_isRunning)
            {
            }
            string _onStop = ("\r\nService has ended " + DateTime.Now);
            File.AppendAllText(_filePath, _onStop);
        }
        public void Execute(object sender, ElapsedEventArgs e)
        {
            _isRunning = true;
            string _onInterval = ("\r\nCheck complete " + DateTime.Now);
            File.AppendAllText(_filePath, _onInterval);
            _isRunning = false;
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }
    }
}

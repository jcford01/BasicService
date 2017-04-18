using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SchedulerService
{
    class Scheduler : ServiceBase
    {
        System.Timers.Timer _aTimer = new System.Timers.Timer(15000);
        public Scheduler()
        {
            Initialise();
            DateTime date = DateTime.Today;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = date.AddMonths(1).AddDays(-1);
        }
        protected virtual void OnStart(string[] args)
        {
            _aTimer.AutoReset = true;
            _aTimer.Elapsed += WriteName;
            _aTimer.Start();
        }
        protected virtual void OnStop()
        {
            _aTimer.Stop();
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }

        public void WriteName(ICollection<ScheduledEvents> _events, ElapsedEventArgs args)
        {
            foreach (ScheduledEvents _event in _events)
            {
                Console.WriteLine(_event.Name);
            }
            Console.ReadLine();
        }
        public void WriteName(ScheduledEvents _event, ElapsedEventArgs args)
        {
            Console.WriteLine(_event.Name);
            Console.ReadLine();
        }
        public void Initialise()
        {
            this.CanPauseAndContinue = true;
            this.ServiceName = "SchedulerService";
        }
    }
}

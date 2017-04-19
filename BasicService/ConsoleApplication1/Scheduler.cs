using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SchedulerService
{
    class Scheduler : ServiceBase
    {
        System.Timers.Timer _aTimer = new System.Timers.Timer(60000);
        List<ScheduledEvents> _scheduledEvents = new List<ScheduledEvents>();
        public Scheduler()
        {
            Initialise();
            DateTime date = DateTime.Today;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            _scheduledEvents.Add(new ScheduledEvents
            {
                Id = 1,
                Name = "DoThing1",
                ScheduledOffSetPoint = OffSetPoint.EndOfMonth,
                DayOffSet = -1,
                MinutesFromMidNight = 2 * 30,
                IsRunning = false
            });
            _scheduledEvents.Add(new ScheduledEvents
            {
                Id = 1,
                Name = "DoThing2",
                ScheduledOffSetPoint = OffSetPoint.EndOfMonth,
                DayOffSet = -2,
                MinutesFromMidNight = 4 * 30,
                IsRunning = false
            });
            _scheduledEvents.Add(new ScheduledEvents
            {
                Id = 1,
                Name = "DoThing3",
                ScheduledOffSetPoint = OffSetPoint.EndOfMonth,
                DayOffSet = -3,
                MinutesFromMidNight = 6 * 30,
                IsRunning = false
            });
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
            while (_scheduledEvents.Any(x => x.IsRunning == true))
            {
            }
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }

        public void WriteName(object sender, ElapsedEventArgs args)
        {
            foreach (ScheduledEvents _event in _scheduledEvents)
            {
                DoWork(_event);
            }
            Console.ReadLine();
        }

        private void DoWork(ScheduledEvents _event)
        {
            _event.IsRunning = true;
            Console.WriteLine("Starting {0}", _event.Name);
            Thread.Sleep(5000);
            Console.WriteLine("{} Finished!", _event.Name);
        }

        public void Initialise()
        {
            this.CanPauseAndContinue = true;
            this.ServiceName = "Armstrong.SchedulerService";
        }
    }
}

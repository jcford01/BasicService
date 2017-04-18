using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService
{
    public enum OffSetPoint
    {
        StartOfMonth,
        EndOfMonth
    }

    public class ScheduledEvents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OffSetPoint ScheduledOffSetPoint { get; set; }
        public int DayOffSet { get; set; }
        public int MinutesFromMidNight { get; set; }
        public bool IsRunning { get; set; }
    }
}

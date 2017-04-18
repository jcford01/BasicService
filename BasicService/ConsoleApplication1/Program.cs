using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Scheduler())
                {
                    ServiceBase.Run(service);
                }
            else
            {
                // running as console app
                using (var service = new Scheduler())
                {
                    service.Start(new string[0]);
                    Console.WriteLine(service.ServiceName + " Running... Press any key to stop");
                    Console.ReadLine();
                    service.Stop();
                }
            }
        }
    }
}

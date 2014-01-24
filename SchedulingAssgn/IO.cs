using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    class IO
    {
        private bool BusyOrNO = false;
        IOQueu MyProcessQueue;

        public IO(IOQueu ProcessQueue)
        {
            MyProcessQueue = ProcessQueue;
        }

        public void DoIOFor(SpecialProcess Process)
        {
            Console.WriteLine("Did IO for process: " + Process.GetName());
            Process.TotalTimeInIO++;
            Process.WaitingTimeCurrentIOLeft--;
            BusyOrNO = true;
        }

        public void KickOut(SpecialProcess Process)
        {
            BusyOrNO = false;
        }

        public bool IsBusy()
        {
            return BusyOrNO;
        }

    }
}

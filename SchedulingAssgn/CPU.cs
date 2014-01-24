using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    class CPU
    {
        private bool BusyOrNot = false;

        public void AttachTo(SpecialProcess Process)
        {
            Console.WriteLine("\nCPU now attached to " + Process.GetName());
            BusyOrNot = true;
        }

        public bool IsBusy()
        {
            return BusyOrNot;
        }

        public void DoWorkOn(SpecialProcess Process)
        {
            Process.CurrentTimeInCPU++;
            Process.TotalTimeInCPU++;
            Console.WriteLine("CPU worked on process: " + Process.GetName());
        }

        public void KickOut(SpecialProcess Process)
        {
            Process.CurrentTimeInCPU = 0;
            BusyOrNot = false;
        }

    }
}
